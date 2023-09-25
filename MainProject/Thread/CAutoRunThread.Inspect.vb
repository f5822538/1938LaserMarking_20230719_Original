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

    ''' <summary>
    ''' 定位及旋轉補正-回傳-SucceedFind-的結果(True:成功, False:失敗)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Locate() As Boolean
        With moMyEquipment
            Try
                Dim oModelFinderShift As ModelFinderShift
                mnShiftX = 0

                '取得-FindModel-的結果
                '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                '-------------------------20230913-開始--------------------------
                If moMyEquipment.HardwareConfig.MiscConfig.IsUseModelFinder = True Then
                    moMyEquipment.SucceedFind = moMyEquipment.FindModel(moCamera.Camera.BitmapImage(True), mnSequence, moLog) '利用FindModel算法尋找定位孔
                Else
                    moMyEquipment.SucceedFind = moMyEquipment.GetAlign(mnSequence, moLog) '利用特徵比對算法尋找定位孔
                End If
                '-------------------------20230913-結束--------------------------
                '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

                If moMyEquipment.SucceedFind = True Then 'FindModel-成功
                    CalculationShift(oModelFinderShift, .FindMark1X, .FindMark1Y, .FindMark2X, .FindMark2Y) '計算旋轉補正角度
                    moLog.LogInformation(String.Format("[{0:d4}] Shift：dX = {1}, dY = {2}, dAngle = {3}", mnSequence, oModelFinderShift.shiftX, oModelFinderShift.shiftY, oModelFinderShift.Angle)) '定位重要訊息
                    mnShiftX = CInt(oModelFinderShift.shiftX)

                    If IsNumeric(oModelFinderShift.Angle) = True Then
                        '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                        If ShiftImageMIL(moImageID, oModelFinderShift) = True Then '旋轉補正
                            mbAlignStatus = False '設定-定位正常
                        Else
                            mbAlignStatus = True '設定-定位異常
                            moLog.LogInformation(String.Format("[{0:d4}] Rotate Failed", mnSequence))
                        End If
                        '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))
                    Else
                        mbAlignStatus = True '設定-定位異常
                        moLog.LogInformation(String.Format("[{0:d4}] Calculation Angle Failed", mnSequence))
                    End If
                Else 'FindModel-失敗
                    mbAlignStatus = True '設定-定位異常
                    moLog.LogInformation(String.Format("[{0:d4}] Find Model Failed", mnSequence))
                End If

                Return moMyEquipment.SucceedFind
            Catch ex As Exception
                moLog.LogError(String.Format("[{0:d4}] 定位錯誤:{1}", mnSequence, ex.Message & Environment.NewLine & ex.StackTrace))
                moMyEquipment.LogAlarm.LogError("定位錯誤")
                Return False
            End Try
        End With
    End Function

    ''' <summary>
    ''' CAutoRunThread.SingleRun -> CAutoRunThread.RunInspect
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RunInspect() As AlarmCode
        Dim oAlarmCode As AlarmCode = AlarmCode.IsOK
        Dim oAlarmCodeWaitMap As AlarmCode = AlarmCode.IsOK

        With moMyEquipment
            Try
                Dim aDataTime As DateTime = DateTime.Now
                Dim sPath As String = String.Format("{0}\Report\{1:yyyy-MM}\{1:yyyy-MM-dd}\{1:HH_mm_ss_fff}", Application.StartupPath, aDataTime) '報告-重要路徑
                Dim oInspectResult As New CInspectResult
                Dim oMyDefectList As New CMyDefectList
                Dim oInspectSum As CInspectSum
                Dim oProductConfig As CMyProductConfig
                Dim bIsOK As Boolean = False 'MIL-光學AOI檢測卡控用的Flag旗標

                oInspectResult.RecipeID = moMainRecipe.RecipeID
                oInspectResult.CodeID = moMyEquipment.CodeText
                oMyDefectList.CodeID = moMyEquipment.CodeText
                oInspectResult.InspectPath = sPath '報告-重要路徑

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
                '-------------------------20230906-開始--------------------------
                '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                oInspectResult.AINODIEPath = String.Format("{0}\{1}\{2}\{3}\NO die", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                oInspectResult.AIOffsetPath = String.Format("{0}\{1}\{2}\{3}\Marking shift", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                oInspectResult.AILoseAndRotatePath = String.Format("{0}\{1}\{2}\{3}\Marking lost and rotate", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, sReportStripID)
                '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))
                '-------------------------20230906-結束--------------------------

                'oInspectResult.AIOKPath = String.Format("{0}\{1}\{2}\{3}\OK", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AINGPath = String.Format("{0}\{1}\{2}\{3}\NG", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AINODIEPath = String.Format("{0}\{1}\{2}\{3}\NO die", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AIOffsetPath = String.Format("{0}\{1}\{2}\{3}\Marking shift", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)
                'oInspectResult.AILoseAndRotatePath = String.Format("{0}\{1}\{2}\{3}\Marking rotate", moMyEquipment.HardwareConfig.MiscConfig.AIPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)

                'oInspectResult.AIOffsetToFtpPath = String.Format("{0}\{1}\{2}\{3}\marking shift", moMyEquipment.HardwareConfig.MiscConfig.UpLoadOffsetToFtpPath, moMainRecipe.RecipeID, sLotID, moMyEquipment.CodeText)

                '' Augustin 220407 IT HandShake Test
                oInspectResult.AIXMLFileName = String.Format("{0}\ExportXML", moMyEquipment.HardwareConfig.MiscConfig.AIPath)
                ''oInspectResult.AIXMLFileName = String.Format("{0}\{1}_{2:yyyyMMdd}", moMyEquipment.HardwareConfig.MiscConfig.AIPath, oInspectResult.CodeID, aDataTime)

                oInspectResult.Name = String.Format("{0:yyyy-MM-dd_HH_mm_ss_fff}", aDataTime) '報告-重要路徑
                oInspectResult.Sequnce = mnSequence

                '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                oInspectResult.AlignStatus = mbAlignStatus '定位結果-狀態(True:異常, False:正常)
                '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

                oProductConfig = New CMyProductConfig(sPath, oInspectResult.Name, "INI") '報告-重要路徑
                oInspectSum = New CInspectSum(oInspectResult, aDataTime, oMyDefectList, oProductConfig) '報告-重要路徑

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
                    '判斷No Die: IsNoDieCode ------> OriginalType ------> ResultType ------> Result
                    moMyEquipment.Handshake.BuildStripOriginalMapInfo(moMyEquipment.HardwareConfig.MiscConfig.ReadProductXmlPath, moProductProcess, moMyEquipment.LogHandshake) '判斷No Die: IsNoDieCode ------> OriginalType ------> ResultType ------> Result
                Else
                    '' Augustin 220407 IT HandShake Test
                    '判斷No Die: IsNoDieCode ------> OriginalType ------> ResultType ------> Result
                    moMyEquipment.Handshake.BuildStripOriginalMapInfo(moMyEquipment.HardwareConfig.MiscConfig.ReadProductXmlPath, moProductProcess, moMyEquipment.LogHandshake) '判斷No Die: IsNoDieCode ------> OriginalType ------> ResultType ------> Result
                End If

                Call moProductProcess.GetEQPID(oProductConfig)

                '標準差,漏雷(((((((((((((((((((((((((((((((重要區塊-開始-Begin)))))))))))))))))))))))))))))) 'CMyPatternMatching.FindModelAll
                bIsOK = ModelDiffForStandardDeviation(moImageID, moRecipeCamera.RecipeModelDiff, oInspectSum, moProductProcess, moMyEquipment, moLog, mnSequence, moMyEquipment.HardwareConfig.MiscConfig.IsSaveInspectImage, moMyEquipment.HardwareConfig.MiscConfig.DefectMaxCount) '蓋印漏雷/蓋印轉置
                '標準差,漏雷(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

                If bIsOK = False Then
                    oInspectSum.InspectResult.FindStatus = True '瑕疵-Y(樣板異常)
                    Call moLog.LogError(String.Format("[{0:d4}] Model Diff Failed", mnSequence))
                End If

                If moMyEquipment.HardwareConfig.HandshakeBypass = False Then '交握-不要Bypass
                    'No Die(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                    bIsOK = CompareOriginalAndInspectNoDieSection(oInspectSum, moProductProcess, moLog, moMyEquipment.HardwareConfig.MiscConfig.DefectMaxCount)
                    'No Die(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))
                    If bIsOK = False Then
                        oInspectSum.InspectResult.FindStatus = True '瑕疵-Y(樣板異常)
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
                    Dim oCopyAIImageFileTask As Task = Task.Factory.StartNew(Sub() CopyAIImageFileForMulti(sPath, oInspectResult, oInspectSum)) '報告-重要路徑
                End If

                moMyEquipment.IsNotUpdateMap = False '預設值-資料上報
                '-------------------------漏雷扣除No Die-開始--------------------------
                If moMyEquipment.HardwareConfig.HandshakeBypass = False Then '交握-不要Bypass
                    'Die上沒有雷刻字串(漏雷是異常, No Die不是異常)

                    If (oInspectSum.InspectResult.DefectCount - oInspectSum.InspectResult.DefectNoDieCount) > moMyEquipment.MaxDefectCountForUpdateMap AndAlso _
                        oInspectSum.InspectResult.ModleLoseStatus = True Then '判斷條件
                        oAlarmCode = AlarmCode.IsDieLoseLaser '漏雷

                        Dim stTrace1 As StackTrace = New StackTrace(fNeedFileInfo:=True)
                        Dim stFrame1 As StackFrame = stTrace1.GetFrames(0)
                        Dim fileName1 As String = stFrame1.GetFileName
                        Dim fileLineNum1 As Integer = stFrame1.GetFileLineNumber
                        Dim fileColNum1 As Integer = stFrame1.GetFileColumnNumber
                        Dim fileMethodName1 As String = stFrame1.GetMethod().Name
                        moLog.LogError("FileName:" & fileName1)
                        moLog.LogError("FileLineNumber:" & fileLineNum1)
                        moLog.LogError("FileColumnNumber:" & fileColNum1)
                        moLog.LogError("MethodName:" & fileMethodName1)
                    End If

                    If oAlarmCode = AlarmCode.IsDieLoseLaser Then '漏雷

                        '-------------------------漏雷觸發Alarm-開始--------------------------
                        moMyEquipment.TriggerAlarm(oAlarmCode) '漏雷觸發Alarm
                        If moLog IsNot Nothing Then moLog.LogError(String.Format("[{0:d4}] 漏雷觸發Alarm", mnSequence)) 'Log 日誌(處理 Process)
                        moMyEquipment.LogAlarm.LogError(String.Format("[{0:d4}] 漏雷觸發Alarm", mnSequence)) 'Log 日誌(警報 Alarm)
                        moMyEquipment.SetEroorOn(moLog) '漏雷觸發Alarm(Error)
                        '-------------------------漏雷觸發Alarm-結束--------------------------

                        Dim subStr1 As String = "oInspectSum.InspectResult.DefectCount"
                        Dim subStr2 As String = "oInspectSum.InspectResult.DefectNoDieCount"
                        Dim subStr3 As String = "oInspectSum.InspectResult.ModleLoseStatus"
                        Dim subStr4 As String = "oInspectSum.InspectResult.NotDefectNoDieCount"
                        Dim finalStr1 = subStr1 & ":" & oInspectSum.InspectResult.DefectCount & Environment.NewLine &
                        subStr2 & ":" & oInspectSum.InspectResult.DefectNoDieCount & Environment.NewLine &
                        subStr3 & ":" & oInspectSum.InspectResult.ModleLoseStatus & Environment.NewLine &
                        subStr4 & ":" & oInspectSum.InspectResult.NotDefectNoDieCount

                        Dim defectMsgText As String = "瑕疵數量：[{0}] (漏雷部分已扣除No Die), 請問是否要上報 Map?"
                        If Debugger.IsAttached = True Then
                            If MsgBox(finalStr1, MsgBoxStyle.OkOnly, "漏雷資訊") = MsgBoxResult.Ok Then
                                If MsgBox(String.Format(defectMsgText, oInspectSum.InspectResult.DefectCount - oInspectSum.InspectResult.DefectNoDieCount), MsgBoxStyle.YesNo, "銓發科技") = MsgBoxResult.No Then
                                    moMyEquipment.IsNotUpdateMap = True '資料不上報
                                End If
                            End If
                        ElseIf Debugger.IsAttached = False Then
                            If MsgBox(String.Format(defectMsgText, oInspectSum.InspectResult.DefectCount - oInspectSum.InspectResult.DefectNoDieCount), MsgBoxStyle.YesNo, "銓發科技") = MsgBoxResult.No Then
                                moMyEquipment.IsNotUpdateMap = True '資料不上報
                            End If
                        End If

                    End If

                End If
                '-------------------------漏雷扣除No Die-結束--------------------------

                '-------------------------資料上報 Map-開始--------------------------
                If moMyEquipment.HardwareConfig.HandshakeBypass = False AndAlso _
                    moMyEquipment.IsNotUpdateMap = False AndAlso moProductProcess.SubstrateID <> "" Then '交握-不要Bypass
                    oAlarmCode = moMyEquipment.SendStripMapUpload(moProductProcess, moMyEquipment.LogHandshake) 'TCP 發送上傳產品分布

                    Dim stTrace1 As StackTrace = New StackTrace(fNeedFileInfo:=True)
                    Dim stFrame1 As StackFrame = stTrace1.GetFrames(0)
                    Dim fileName1 As String = stFrame1.GetFileName
                    Dim fileLineNum1 As Integer = stFrame1.GetFileLineNumber
                    Dim fileColNum1 As Integer = stFrame1.GetFileColumnNumber
                    Dim fileMethodName1 As String = stFrame1.GetMethod().Name
                    moLog.LogError("FileName:" & fileName1)
                    moLog.LogError("FileLineNumber:" & fileLineNum1)
                    moLog.LogError("FileColumnNumber:" & fileColNum1)
                    moLog.LogError("MethodName:" & fileMethodName1)

                    oAlarmCodeWaitMap = moMyEquipment.WaitMapUploadACK(moProductProcess, Function() moStopRun.IsSet() = True)

                    If moMyEquipment.HardwareConfig.AIHandshakeBypass = False Then 'AI交握-不要Bypass
                        oAlarmCode = moMyEquipment.UpdateAIInfo(moProductProcess, oInspectSum, moMyEquipment.LogHandshake)

                        stTrace1 = New StackTrace(fNeedFileInfo:=True)
                        stFrame1 = stTrace1.GetFrames(0)
                        fileName1 = stFrame1.GetFileName
                        fileLineNum1 = stFrame1.GetFileLineNumber
                        fileColNum1 = stFrame1.GetFileColumnNumber
                        fileMethodName1 = stFrame1.GetMethod().Name
                        moLog.LogError("FileName:" & fileName1)
                        moLog.LogError("FileLineNumber:" & fileLineNum1)
                        moLog.LogError("FileColumnNumber:" & fileColNum1)
                        moLog.LogError("MethodName:" & fileMethodName1)
                    End If

                    If oAlarmCode <> AlarmCode.IsOK Then
                        moMyEquipment.SetEroorOn() '停止機台動作
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
                OutputFinalReport(oInspectSum) '輸出檢測結果報表
                '-------------------------輸出檢測結果報表-結束--------------------------

                If oAlarmCodeWaitMap <> AlarmCode.IsOK Then
                    If oAlarmCodeWaitMap = AlarmCode.IsWaitHandshakeTimeout OrElse oAlarmCodeWaitMap = AlarmCode.IsReadCodeFailed Then
                        oAlarmCodeWaitMap = AlarmCode.IsOK '忽略-TCP 等待交握超時,忽略-讀取條碼失敗
                        Return oAlarmCodeWaitMap
                    End If
                    Call moMyEquipment.SetEroorOn()
                    Return oAlarmCodeWaitMap
                End If
            Catch ex As Exception
                moLog.LogError(String.Format("[{0:d4}] RunInspect-檢測錯誤:{1}", mnSequence, ex.Message))
                moMyEquipment.LogAlarm.LogError("RunInspect-檢測錯誤:" & ex.Message & Environment.NewLine & ex.StackTrace)
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

    ''' <summary>
    ''' 計算旋轉補正角度
    ''' </summary>
    ''' <param name="oModelFinderShift"></param>
    ''' <param name="dFindModelX1"></param>
    ''' <param name="dFindModelY1"></param>
    ''' <param name="dFindModelX2"></param>
    ''' <param name="dFindModelY2"></param>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' 旋轉補正
    ''' </summary>
    ''' <param name="oSource"></param>
    ''' <param name="oModelFinderShift"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShiftImageMIL(ByVal oSource As MIL_ID, oModelFinderShift As ModelFinderShift) As Boolean
        moLog.LogInformation("旋轉補正開始!")
        MIL.MimRotate(oSource, oSource, -oModelFinderShift.Angle, oModelFinderShift.refX, oModelFinderShift.refY, oModelFinderShift.refX + oModelFinderShift.shiftX, oModelFinderShift.refY + oModelFinderShift.shiftY, MIL.M_BICUBIC + MIL.M_OVERSCAN_CLEAR)
        moLog.LogInformation("旋轉補正完成!")
        Return True
    End Function
End Class