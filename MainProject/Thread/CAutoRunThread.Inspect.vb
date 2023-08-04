Partial Class CAutoRunThread

    Private Const mnMaxDefect As Integer = 5000

    Public Event AutoRunFinished(sender As Object, e As CAutoRunFinished)
    Private moITV_INSPECT_DATA As ITV_INSPECT_DATA
    Private moLightFeedBack As New ITV_LIGHT_FEEDBACK
    Private DefectResultList(mnMaxDefect) As ITV_DEFECT_RESULT
    Private DefectResultListExtend(mnMaxDefect) As ITV_DEFECT_RESULT_EXTEND
    Private moComperHandle As IntPtr = IntPtr.Zero
    Private mnImageSize As Size = Size.Empty
    Public Shared ProcessModelCenter1StListLock As New Object
    Public Shared ProcessModelCenter2NdListLock As New Object
    Public Shared ProcessDefectListLock As New Object
    Private mnShiftX As Integer = 0

    Private Function Locate() As Boolean
        With moMyEquipment
            Try
                Dim oModelFinderShift As ModelFinderShift
                mnShiftX = 0

                .SucceedFind = If(.HardwareConfig.MiscConfig.IsUseModelFinder = True, .FindModel(moCamera.Camera.BitmapImage(True), mnSequence, moLog), .GetAlign(mnSequence, moLog))
                If .SucceedFind = True Then CalculationShift(oModelFinderShift, .FindMark1X, .FindMark1Y, .FindMark2X, .FindMark2Y)

                If .SucceedFind = True Then
                    Call moLog.LogInformation(String.Format("[{0:d4}] Shift：dX = {1}, dY = {2}, dAngle = {3}", mnSequence, oModelFinderShift.shiftX, oModelFinderShift.shiftY, oModelFinderShift.Angle))
                    mnShiftX = CInt(oModelFinderShift.shiftX)
                    If IsNumeric(oModelFinderShift.Angle) = True Then
                        If ShiftImageMIL(moImageID, oModelFinderShift) = True Then
                            mbAlignStatus = False
                        Else
                            mbAlignStatus = True
                            Call moLog.LogInformation(String.Format("[{0:d4}] Rotate Failed", mnSequence))
                        End If
                    Else
                        mbAlignStatus = True
                        Call moLog.LogInformation(String.Format("[{0:d4}] Calculation Angle Failed", mnSequence))
                    End If
                Else
                    mbAlignStatus = True
                    Call moLog.LogInformation(String.Format("[{0:d4}] Find Model Failed", mnSequence))
                End If

                Return .SucceedFind
            Catch ex As Exception
                Call moLog.LogError(String.Format("[{0:d4}] 定位錯誤，Error：{1}", mnSequence, ex.ToString()))
                Call moMyEquipment.LogAlarm.LogError("定位錯誤")
                Return False
            End Try
        End With
    End Function

    Private Function RunInspect() As AlarmCode
        Dim oAlarmCode As AlarmCode = AlarmCode.IsOK
        Dim oAlarmCodeWaitMap As AlarmCode = AlarmCode.IsOK

        With moMyEquipment
            Try
                Dim aDataTime As DateTime = DateTime.Now
                Dim sPath As String = String.Format("{0}\Report\{1:yyyy-MM}\{1:yyyy-MM-dd}\{1:HH_mm_ss_fff}", Application.StartupPath, aDataTime)
                Dim oInspectResult As New CInspectResult
                Dim oMyDefectList As New CMyDefectList
                Dim oInspectSum As CInspectSum
                Dim oProductConfig As CMyProductConfig
                Dim bIsOK As Boolean = False

                oInspectResult.RecipeID = moMainRecipe.RecipeID
                oInspectResult.CodeID = moMyEquipment.CodeText
                oMyDefectList.CodeID = moMyEquipment.CodeText
                oInspectResult.InspectPath = sPath

                ''0928
                '' Augustin 220503 Modify
                Dim sLotID As String = ""
                Dim sReportStripID = ""
                'Dim moProductProcess As New CMyProduct
                If moMyEquipment.HardwareConfig.Debug = False Then
                    Call moProductProcess.GetLotID(sLotID)
                    If moMyEquipment.CodeText <> "" AndAlso moProductProcess.SubstrateID <> "" Then
                        If moMyEquipment.CodeText <> moProductProcess.SubstrateID Then
                            sReportStripID = moMyEquipment.CodeText
                        Else
                            sReportStripID = moProductProcess.SubstrateID
                        End If
                    ElseIf moMyEquipment.CodeText = "" AndAlso moProductProcess.SubstrateID <> "" Then
                        sReportStripID = moProductProcess.SubstrateID
                    Else
                        moLog.LogInformation("Cannot read 2D code and no strip ID")
                        sReportStripID = moProductProcess.SubstrateID
                    End If
                Else
                    sLotID = "123"
                    If moProductProcess.SubstrateID = "" Then
                        moProductProcess.SubstrateID = "A123456789"
                    End If
                    sReportStripID = moProductProcess.SubstrateID
                End If
                oInspectResult.AIOKPath = String.Format("{0}\{1}\{2}\{3}\OK", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                oInspectResult.AINGPath = String.Format("{0}\{1}\{2}\{3}\NG", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                oInspectResult.AINODIEPath = String.Format("{0}\{1}\{2}\{3}\NO die", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                oInspectResult.AIOffsetPath = String.Format("{0}\{1}\{2}\{3}\Marking shift", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                oInspectResult.AILoseAndRotatePath = String.Format("{0}\{1}\{2}\{3}\Marking lost and rotate", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                'oInspectResult.AIOKPath = String.Format("{0}\{1}\{2}\{3}\OK", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AINGPath = String.Format("{0}\{1}\{2}\{3}\NG", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AINODIEPath = String.Format("{0}\{1}\{2}\{3}\NO die", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AIOffsetPath = String.Format("{0}\{1}\{2}\{3}\Marking shift", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AILoseAndRotatePath = String.Format("{0}\{1}\{2}\{3}\Marking rotate", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)

                'oInspectResult.AIOffsetToFtpPath = String.Format("{0}\{1}\{2}\{3}\marking shift", moMyEquipment.HardwareConfig.MiscConfig.UpLoadOffsetToFtpPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)

                '' Augustin 220407 IT HandShake Test
                oInspectResult.AIXMLFileName = String.Format("{0}\ExportXML", moMyEquipment.HardwareConfig.MiscConfig.AIPath)
                ''oInspectResult.AIXMLFileName = String.Format("{0}\{1}_{2:yyyyMMdd}", moMyEquipment.HardwareConfig.MiscConfig.AIPath, oInspectResult.CodeID, aDataTime)

                oInspectResult.Name = String.Format("{0:yyyy-MM-dd_HH_mm_ss_fff}", aDataTime)
                oInspectResult.Sequnce = mnSequence
                oInspectResult.AlignStatus = mbAlignStatus
                oProductConfig = New CMyProductConfig(sPath, oInspectResult.Name, "INI")
                oInspectSum = New CInspectSum(oInspectResult, aDataTime, oMyDefectList, oProductConfig)

                If Directory.Exists(oInspectResult.InspectPath) = False Then
                    Call Directory.CreateDirectory(oInspectResult.InspectPath)
                End If

                If moMyEquipment.HardwareConfig.MiscConfig.IsSaveAIOKImage = True Then
                    If Directory.Exists(oInspectResult.AIOKPath) = False Then
                        Call Directory.CreateDirectory(oInspectResult.AIOKPath)
                    End If
                End If

                If moMyEquipment.HardwareConfig.MiscConfig.IsSaveAINGImage = True Then
                    If Directory.Exists(oInspectResult.AINGPath) = False Then
                        Call Directory.CreateDirectory(oInspectResult.AINGPath)
                    End If

                    If Directory.Exists(oInspectResult.AINODIEPath) = False Then
                        Call Directory.CreateDirectory(oInspectResult.AINODIEPath)
                    End If

                    If Directory.Exists(oInspectResult.AIOffsetPath) = False Then
                        Call Directory.CreateDirectory(oInspectResult.AIOffsetPath)
                    End If

                    If Directory.Exists(oInspectResult.AILoseAndRotatePath) = False Then
                        Call Directory.CreateDirectory(oInspectResult.AILoseAndRotatePath)
                    End If

                    'If Directory.Exists(oInspectResult.AIOffsetToFtpPath) = False Then
                    '    Call Directory.CreateDirectory(oInspectResult.AIOffsetToFtpPath)
                    'End If

                End If

                'If .HardwareConfig.HandshakeBypass = True Then
                moProductProcess.DimensionX = moMainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount
                moProductProcess.DimensionY = moMainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount

                moProductProcess.MarkList.Clear()
                For nIndex As Integer = 0 To moRecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Count - 1
                    Dim oMarkInfo As New CMyMarkInfo
                    oMarkInfo.MarkX = moRecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(nIndex).MarkX
                    oMarkInfo.MarkY = moRecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(nIndex).MarkY
                    oMarkInfo.AfterInspectBinCode = .HardwareConfig.HandshakeConfig.IsGrayCode
                    'oMarkInfo.IsProcess = True
                    moProductProcess.MarkList.Add(oMarkInfo)
                Next
                'End If

                If moMyEquipment.HardwareConfig.HandshakeBypass = False Then
                    '測試用SubstrateID 為了測試讀取XML檔案
                    If moProductProcess.SubstrateID = "" Then
                        moProductProcess.SubstrateID = "A123456789"
                    End If
                    Call moMyEquipment.Handshake.BuildStripOriginalMapInfo(moMyEquipment.HardwareConfig.MiscConfig.ReadProductXmlPath, moProductProcess, moMyEquipment.LogHandshake)
                Else
                    '' Augustin 220407 IT HandShake Test
                    Call moMyEquipment.Handshake.BuildStripOriginalMapInfo(moMyEquipment.HardwareConfig.MiscConfig.ReadProductXmlPath, moProductProcess, moMyEquipment.LogHandshake)
                End If

                Call moProductProcess.GetEQPID(oProductConfig)
                bIsOK = ModelDiffForStandardDeviation(moImageID, moRecipeCamera.RecipeModelDiff, oInspectSum, moProductProcess, moMyEquipment, moLog, mnSequence, moMyEquipment.HardwareConfig.MiscConfig.IsSaveInspectImage, moMyEquipment.HardwareConfig.MiscConfig.DefectMaxCount)
                If bIsOK = False Then
                    oInspectSum.InspectResult.FindStatus = True
                    Call moLog.LogError(String.Format("[{0:d4}] Model Diff Failed", mnSequence))
                End If

                If moMyEquipment.HardwareConfig.HandshakeBypass = False Then
                    bIsOK = CompareOriginalAndInspectNoDieSection(oInspectSum, moProductProcess, moLog, moMyEquipment.HardwareConfig.MiscConfig.DefectMaxCount)
                    If bIsOK = False Then
                        oInspectSum.InspectResult.FindStatus = True
                        Call moLog.LogError(String.Format("[{0:d4}] CompareOriginalAndInspectNoDieSection Failed", mnSequence))
                    End If
                End If

                Call moProductProcess.CopyTo(oProductConfig)

                '' Augustin 220726 Add for Wafer Map
                Dim oWaferMapAddDefectTask As Task = Task.Factory.StartNew(Sub() WaferMapAddDefect(oInspectSum))

                '0928修改NG複製到分為NG、NODIE、蓋印偏移資料夾
                'If moMyEquipment.HardwareConfig.MiscConfig.IsSaveAINGImage = True Then
                '    Dim oCopyAIImageFileTask As Task = Task.Factory.StartNew(Sub() CopyAIImageFile(sPath, oInspectResult.AINGPath, String.Format("*R*C*.bmp")))
                'End If

                If moMyEquipment.HardwareConfig.MiscConfig.IsSaveAINGImage = True Then
                    Dim oCopyAIImageFileTask As Task = Task.Factory.StartNew(Sub() CopyAIImageFileForMulti(sPath, oInspectResult, oInspectSum))
                End If

                moMyEquipment.IsNotUpdateMap = False
                '-------------------------漏雷扣除No Die-開始--------------------------
                If moMyEquipment.HardwareConfig.HandshakeBypass = False Then
                    'Die上沒有雷刻字串(漏雷是異常, No Die不是異常)
                    If oInspectSum.InspectResult.ModleLoseStatus = True AndAlso _
                        (oInspectSum.InspectResult.DefectCount - oInspectSum.InspectResult.DefectNoDieCount) > 0 Then
                        oAlarmCode = AlarmCode.IsDieLoseLaser
                    End If

                    If (oInspectSum.InspectResult.DefectCount - oInspectSum.InspectResult.DefectNoDieCount) > moMyEquipment.MaxDefectCountForUpdateMap Then
                        oAlarmCode = AlarmCode.IsDieLoseLaser
                    End If

                    moMyEquipment.SetEroorOn(moLog) '輸出Error log
                    OutputFinalReport(oInspectSum)

                    Dim defectMsgText As String = "瑕疵數量：[{0}] (漏雷部分已扣除No Die), 請問是否要上報 Map?"
                    If MsgBox(String.Format(defectMsgText, oInspectSum.InspectResult.DefectCount - oInspectSum.InspectResult.DefectNoDieCount), MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.No Then
                        moMyEquipment.IsNotUpdateMap = True
                    End If
                End If
                '-------------------------漏雷扣除No Die-結束--------------------------

                '-------------------------資料上報 Map-開始--------------------------
                If moMyEquipment.HardwareConfig.HandshakeBypass = False AndAlso _
                    moMyEquipment.IsNotUpdateMap = False AndAlso moProductProcess.SubstrateID <> "" Then
                    oAlarmCode = moMyEquipment.SendStripMapUpload(moProductProcess, moMyEquipment.LogHandshake)

                    oAlarmCodeWaitMap = moMyEquipment.WaitMapUploadACK(moProductProcess, Function() moStopRun.IsSet() = True)

                    If moMyEquipment.HardwareConfig.AIHandshakeBypass = False Then
                        oAlarmCode = moMyEquipment.UpdateAIInfo(moProductProcess, oInspectSum, moMyEquipment.LogHandshake)
                    End If

                    If oAlarmCode <> AlarmCode.IsOK Then
                        Call moMyEquipment.SetEroorOn()
                        Return oAlarmCode
                    End If
                End If
                '-------------------------資料上報 Map-結束--------------------------

                If moMyEquipment.HardwareConfig.MiscConfig.IsAutoRemoveProduct = False Then UpdateProduct()
                Call moProductProcess.ClearMark()

                Call moLog.LogInformation(String.Format("[{0:d4}] Total Defect Count：{1}", mnSequence, oInspectSum.DefectListDraw.Count))

                If oInspectSum.InspectResult.ModleOffsetStatus = True Then moMyEquipment.SetEroorOn()

                If LastDate = Now.Date Then
                    If oInspectSum.InspectResult.ModleOffsetStatus = False AndAlso oInspectSum.InspectResult.AlignStatus = False AndAlso oInspectSum.InspectResult.CycleInspectStatus = False AndAlso oInspectSum.InspectResult.FindStatus = False AndAlso oInspectSum.InspectResult.ModleInspectStatus = False AndAlso oInspectSum.InspectResult.ModleLoseStatus = False Then
                        moMyEquipment.YieldConfig.OKCount += 1
                    End If
                    moMyEquipment.YieldConfig.OKCount_Die += moMainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount - moMainRecipe.RecipeCamera.DefectList.Count
                Else
                    LastDate = Now.Date
                    Dim sFileName As String = String.Format("{0:yyyy-MM-dd}", Date.Now)
                    moMyEquipment.YieldConfig.LoadConfig(sFileName)
                    moMyEquipment.YieldConfig.TotalCount = 1
                    moMyEquipment.YieldConfig.OKCount = 0
                    If oInspectSum.InspectResult.ModleOffsetStatus = False AndAlso oInspectSum.InspectResult.AlignStatus = False AndAlso oInspectSum.InspectResult.CycleInspectStatus = False AndAlso oInspectSum.InspectResult.FindStatus = False AndAlso oInspectSum.InspectResult.ModleInspectStatus = False AndAlso oInspectSum.InspectResult.ModleLoseStatus = False Then
                        moMyEquipment.YieldConfig.OKCount += 1
                    End If
                    moMyEquipment.YieldConfig.TotalCount_Die = moMainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount
                    moMyEquipment.YieldConfig.OKCount_Die = moMainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount - moMainRecipe.RecipeCamera.DefectList.Count
                End If
                moMyEquipment.YieldConfig.SaveConfig("")

                Call oProductConfig.SaveConfig()

                '-------------------------輸出檢測結果報表-開始--------------------------
                OutputFinalReport(oInspectSum)
                '-------------------------輸出檢測結果報表-結束--------------------------

                If oAlarmCodeWaitMap <> AlarmCode.IsOK Then
                    If oAlarmCodeWaitMap = AlarmCode.IsWaitHandshakeTimeout OrElse oAlarmCodeWaitMap = AlarmCode.IsReadCodeFailed Then
                        oAlarmCodeWaitMap = AlarmCode.IsOK
                        Return oAlarmCodeWaitMap
                    End If
                    Call moMyEquipment.SetEroorOn()
                    Return oAlarmCodeWaitMap
                End If
            Catch ex As Exception
                Call moLog.LogError(String.Format("[{0:d4}] 檢測錯誤，Error：{1}", mnSequence, ex.ToString()))
                Call moMyEquipment.LogAlarm.LogError("檢測錯誤")
                Return AlarmCode.IsInspectError
            End Try
        End With

        Return oAlarmCode
    End Function

    ''' <summary>
    ''' 輸出檢測結果報表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OutputFinalReport(ByRef oInspectSum As CInspectSum)
        Try
            FinishReoprt(oInspectSum)
            RaiseEvent AutoRunFinished(Me, New CAutoRunFinished(oInspectSum))
        Catch ex As Exception
            Dim msg As String = ex.Message & Environment.NewLine & ex.StackTrace
        End Try
    End Sub

    Private Sub CalculationShift(ByRef oModelFinderShift As ModelFinderShift, dFindModelX1 As Double, dFindModelY1 As Double, dFindModelX2 As Double, dFindModelY2 As Double)
        Dim dLineX1 As Double = 0.0
        Dim dLineY1 As Double = 0.0
        Dim dLineX2 As Double = 0.0
        Dim dLineY2 As Double = 0.0
        Dim dAngle1 As Double = 0.0
        Dim dAngle2 As Double = 0.0

        With moRecipeCamera
            oModelFinderShift.refX = dFindModelX1
            oModelFinderShift.refY = dFindModelY1
            oModelFinderShift.shiftX = .Locate1.PatternZone.X + (.Locate1.PatternZone.Width / 2) - dFindModelX1
            oModelFinderShift.shiftY = .Locate1.PatternZone.Y + (.Locate1.PatternZone.Height / 2) - dFindModelY1

            dLineX1 = (.Locate2.PatternZone.X + .Locate2.PatternZone.Width / 2) - (.Locate1.PatternZone.X + .Locate1.PatternZone.Width / 2)
            dLineY1 = (.Locate2.PatternZone.Y + .Locate2.PatternZone.Height / 2) - (.Locate1.PatternZone.Y + .Locate1.PatternZone.Height / 2)
        End With

        dLineX2 = dFindModelX2 - dFindModelX1
        dLineY2 = dFindModelY2 - dFindModelY1
        Dim m1 As Double = dLineY1 / dLineX1
        Dim m2 As Double = dLineY2 / dLineX2
        dAngle1 = Math.Atan(m1) - Math.Atan(m2)
        oModelFinderShift.Angle = dAngle1 * 180 / Math.PI
    End Sub

    Private Function ShiftImageMIL(ByVal oSource As MIL_ID, oModelFinderShift As ModelFinderShift) As Boolean
        moLog.LogInformation("旋轉補正開始!")
        MIL.MimRotate(oSource, oSource, -oModelFinderShift.Angle, oModelFinderShift.refX, oModelFinderShift.refY, oModelFinderShift.refX + oModelFinderShift.shiftX, oModelFinderShift.refY + oModelFinderShift.shiftY, MIL.M_BICUBIC + MIL.M_OVERSCAN_CLEAR)
        moLog.LogInformation("旋轉補正完成!")
        Return True
    End Function
End Class