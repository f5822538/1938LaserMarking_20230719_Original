Imports DefectLib
Imports iTVisionService
Imports System.Collections

Partial Class CAutoRunThread

    Private moHtmlReport As CHtmlReport
    Private moProductProcess As New CMyProduct

    Public Sub UpdateImage()
        moRecipeCamera = moMyEquipment.MainRecipe.RecipeCamera
        moImageID = moMyEquipment.ImageID
        moImageHeader = moMyEquipment.ImageHeader
        moCodeReaderImageHeader = moMyEquipment.CodeReaderImageHeader
    End Sub

    Private Function LoadProduct() As AlarmCode
        If moMyEquipment.HardwareConfig.HandshakeBypass = True AndAlso moMyEquipment.CodeText <> "" Then
            If moMyEquipment.ProductList.Count <= 0 Then
                moMyEquipment.ProductList.Add(New CMyProduct())
            End If
            moMyEquipment.ProductList.Item(0).SubstrateID = moMyEquipment.CodeText
        End If

        '' Augustin Bypass Test
        'If moMyEquipment.ProductList.Count <= 0 Then
        '    moMyEquipment.ProductList.Add(New CMyProduct())
        '    moMyEquipment.ProductList.Item(0).SubstrateID = moMyEquipment.CodeText
        'End If

        If moMyEquipment.ProductList.Count > 0 Then
            moProductProcess = moMyEquipment.SelectProduct(moProductProcess, moMyEquipment.CodeText)
            If moProductProcess.SubstrateID = "" Then Return AlarmCode.IsNotProductInformation
            Return AlarmCode.IsOK
        Else
            Call moLog.LogError(String.Format("[{0:d4}] 無產品資訊", mnSequence))
            Return AlarmCode.IsNotProductInformation
        End If
        Return AlarmCode.IsOK
    End Function

    Private Function UpdateProduct() As AlarmCode
        If moMyEquipment.HardwareConfig.HandshakeBypass = False Then
            For nIndex As Integer = 0 To moMyEquipment.ProductList.Count - 1
                If moMyEquipment.ProductList.Item(nIndex).SubstrateID = moProductProcess.SubstrateID Then
                    moMyEquipment.ProductList.Item(nIndex) = moMyEquipment.CopyProduct(moProductProcess)
                    Return AlarmCode.IsOK
                End If
            Next
        Else
            Return AlarmCode.IsOK
        End If
        Return AlarmCode.IsOK
    End Function

    Public Sub FinishReoprt(oInspectSum As CInspectSum)
        Call moHtmlReport.OutPutReport(oInspectSum.InspectResult, oInspectSum.DefectList)
        Call SaveDMFile(oInspectSum.InspectResult, oInspectSum.DefectListDraw)
    End Sub

    Public Sub SaveDMFile(oResult As CInspectResult, oList As List(Of CMyDefect))
        Dim nRatio As Integer = 2

        With moImageHeader
            Dim nSizeX As Integer = CInt(.Width \ nRatio)
            Dim nSizeY As Integer = CInt(.Height \ nRatio)

            Dim oBitmap As Bitmap

            oBitmap = New Bitmap(.Width, .Height, .Stride, PixelFormat.Format8bppIndexed, .Ptr)
            Dim aPalette As Imaging.ColorPalette = oBitmap.Palette
            For nIndex As Integer = 0 To 255
                aPalette.Entries(nIndex) = Color.FromArgb(nIndex, nIndex, nIndex)
            Next
            oBitmap.Palette = aPalette

            Dim oSaveBitmap As New Bitmap(nSizeX, nSizeY)

            If moMyEquipment.HardwareConfig.MiscConfig.IsSaveSourceImage = True Then
                Call oBitmap.Save(String.Format("{0}\SourceImage.Bmp", oResult.GetPath()), ImageFormat.Bmp)
            End If

            Using oGC As Graphics = Graphics.FromImage(oSaveBitmap), oPenDark As Pen = New Pen(Color.LightBlue, 2), oPenBright As Pen = New Pen(Color.DarkBlue, 2), oPenOffset As Pen = New Pen(Color.Red, 2), oPenPass As Pen = New Pen(Color.LightGreen, 2), oPenIndistinct As Pen = New Pen(Color.Yellow, 2), oFont As Font = New Font("Arial", 32), oSolidBrush As SolidBrush = New SolidBrush(Color.White)
                Call oGC.DrawImage(oBitmap, New Rectangle(0, 0, nSizeX, nSizeY))
                'For nCount As Integer = 1 To Math.Min(moRecipeCameraDraw.RecipeInspectList.Count, 100)
                '    If oResult.AlignStatus = True Then
                '        With moRecipeCameraDraw.RecipeInspectList(nCount - 1)
                '            Call oGC.DrawRectangle(oPen, Rectangle.FromLTRB(.TopLeft.X \ nRatio, .TopLeft.Y \ nRatio, .BottomRight.X \ nRatio, .BottomRight.Y \ nRatio))
                '        End With
                '    Else
                '        With moRecipeCameraDraw.RecipeInspectList(nCount - 1)
                '            Call oGC.DrawRectangle(oPenDefect, Rectangle.FromLTRB(.TopLeft.X \ nRatio, .TopLeft.Y \ nRatio, .BottomRight.X \ nRatio, .BottomRight.Y \ nRatio))
                '        End With
                '    End If
                'Next
                ' ==========================
                ' 處理不同方法進行畫圖
                ' ==========================
                If oList.Count > 0 Then
                    Dim ROIDark As New List(Of Rectangle)
                    Dim ROIBright As New List(Of Rectangle)
                    Dim ROIOffset As New List(Of Rectangle)
                    'Dim ROIPass As New List(Of Rectangle)
                    Dim ROIIndistinct As New List(Of Rectangle)
                    For nIndex As Integer = 0 To oList.Count - 1
                        If oList(nIndex).ResultType = ResultType.NGBright Then ROIBright.Add(oList(nIndex).DefectBoundary.GetRatioRectangle(nRatio))
                        If oList(nIndex).ResultType = ResultType.NGDark Then ROIDark.Add(oList(nIndex).DefectBoundary.GetRatioRectangle(nRatio))
                        If oList(nIndex).ResultType = ResultType.Offset OrElse oList(nIndex).ResultType = ResultType.Lose Then ROIOffset.Add(oList(nIndex).DefectBoundary.GetRatioRectangle(nRatio))
                        'If oList(nIndex).ResultType = ResultType.Pass Then ROIPass.Add(oList(nIndex).DefectBoundary.GetRatioRectangle(nRatio))
                        If oList(nIndex).ResultType = ResultType.Indistinct Then ROIIndistinct.Add(oList(nIndex).DefectBoundary.GetRatioRectangle(nRatio))
                    Next
                    If ROIDark.Count > 0 Then oGC.DrawRectangles(oPenDark, ROIDark.ToArray)
                    If ROIBright.Count > 0 Then oGC.DrawRectangles(oPenBright, ROIBright.ToArray)
                    If ROIOffset.Count > 0 Then oGC.DrawRectangles(oPenOffset, ROIOffset.ToArray)
                    'If ROIPass.Count > 0 Then oGC.DrawRectangles(oPenPass, ROIPass.ToArray)
                    If ROIIndistinct.Count > 0 Then oGC.DrawRectangles(oPenIndistinct, ROIIndistinct.ToArray)
                End If

                Dim drawString As String = ""
                Dim drawRect As RectangleF
                Dim nListcount As Integer = moRecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Count - 1
                Dim nFinalX As Integer = moRecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList(nListcount).MarkX
                Dim nFinalY As Integer = moRecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList(nListcount).MarkY
                For Each oRecipeMark As CRecipeMark In moRecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList
                    If oRecipeMark.MarkX = 0 Then
                        drawString = CStr(oRecipeMark.MarkY + 1)
                        drawRect = New Rectangle(CInt(oRecipeMark.PositionCenterX) \ nRatio - 100, CInt(oRecipeMark.PositionCenterY) \ nRatio - 25, moRecipeCamera.RecipeModelDiff.ModelSize.Width \ nRatio, moRecipeCamera.RecipeModelDiff.ModelSize.Height \ nRatio)
                        Call oGC.DrawString(drawString, oFont, oSolidBrush, drawRect)
                    End If
                    If oRecipeMark.MarkX = nFinalX Then
                        drawString = CStr(oRecipeMark.MarkY + 1)
                        drawRect = New Rectangle(CInt(oRecipeMark.PositionCenterX) \ nRatio + 50, CInt(oRecipeMark.PositionCenterY) \ nRatio - 25, moRecipeCamera.RecipeModelDiff.ModelSize.Width \ nRatio, moRecipeCamera.RecipeModelDiff.ModelSize.Height \ nRatio)
                        Call oGC.DrawString(drawString, oFont, oSolidBrush, drawRect)
                    End If
                    If oRecipeMark.MarkY = 0 Then
                        drawString = CStr(nFinalX - oRecipeMark.MarkX)
                        If oRecipeMark.MarkX < 10 Then
                            drawRect = New Rectangle(CInt(oRecipeMark.PositionCenterX) \ nRatio - 10, CInt(oRecipeMark.PositionCenterY) \ nRatio - 100, moRecipeCamera.RecipeModelDiff.ModelSize.Width \ nRatio, moRecipeCamera.RecipeModelDiff.ModelSize.Height \ nRatio)
                        Else
                            drawRect = New Rectangle(CInt(oRecipeMark.PositionCenterX) \ nRatio - 30, CInt(oRecipeMark.PositionCenterY) \ nRatio - 100, moRecipeCamera.RecipeModelDiff.ModelSize.Width \ nRatio, moRecipeCamera.RecipeModelDiff.ModelSize.Height \ nRatio)
                        End If

                        Call oGC.DrawString(drawString, oFont, oSolidBrush, drawRect)
                    End If
                    If oRecipeMark.MarkY = nFinalY Then
                        drawString = CStr(nFinalX - oRecipeMark.MarkX)
                        If oRecipeMark.MarkX < 10 Then
                            drawRect = New Rectangle(CInt(oRecipeMark.PositionCenterX) \ nRatio - 18, CInt(oRecipeMark.PositionCenterY) \ nRatio + 50, moRecipeCamera.RecipeModelDiff.ModelSize.Width \ nRatio, moRecipeCamera.RecipeModelDiff.ModelSize.Height \ nRatio)
                        Else
                            drawRect = New Rectangle(CInt(oRecipeMark.PositionCenterX) \ nRatio - 30, CInt(oRecipeMark.PositionCenterY) \ nRatio + 50, moRecipeCamera.RecipeModelDiff.ModelSize.Width \ nRatio, moRecipeCamera.RecipeModelDiff.ModelSize.Height \ nRatio)
                        End If
                        Call oGC.DrawString(drawString, oFont, oSolidBrush, drawRect)
                    End If
                Next

                Try
                    oSaveBitmap.Save(oResult.GetDMFileName(), ImageFormat.Jpeg)
                Catch ex As System.Exception
                    Call moLog.LogInformation(ex.ToString)
                End Try
                Call oSaveBitmap.Dispose()

                Try
                    MIL.MbufExport(oResult.GetCode1FileName(), MIL.M_BMP, moCodeReaderImageID1)
                Catch ex As System.Exception
                    Call moLog.LogInformation(ex.ToString)
                End Try

                Try
                    MIL.MbufExport(oResult.GetCode2FileName(), MIL.M_BMP, moCodeReaderImageID2)
                Catch ex As System.Exception
                    Call moLog.LogInformation(ex.ToString)
                End Try
            End Using
        End With
    End Sub

    '' Augustin 220726 Add for Wafer Map
    Private Sub WaferMapAddDefect(oInspectSum As CInspectSum)
        Try
            Dim oDefectList(oInspectSum.DefectListDraw.Count - 1) As iTVisionService.DisplayLib.CMyDefect
            Parallel.For(0, oInspectSum.DefectListDraw.Count, CMyEquipment.ParallelOptions,
                Sub(nIndex As Integer)
                    With oInspectSum.DefectListDraw.Item(nIndex)
                        If .DefectCoordinate.X >= moMyEquipment.WaferMap.DieColumnCount OrElse .DefectCoordinate.Y >= moMyEquipment.WaferMap.DieRowCount Then Exit Sub
                        oDefectList(nIndex) = moMyEquipment.WaferMap.CreateDefect(True)
                        oDefectList(nIndex).TargetDieCoordinate = CType(.DefectCoordinate, Point)
                        oDefectList(nIndex).TargetDieFileName = oInspectSum.InspectResult.GetDMFileName
                        oDefectList(nIndex).DefectBoundary = .DefectBoundary.ToRectangle
                        oDefectList(nIndex).DefectCenter = CType(.DefectCenter, Point)
                        oDefectList(nIndex).Bin = DisplayLib.BinType.NGDie
                        oDefectList(nIndex).DefetDrawType = DisplayLib.DefetDrawType.Circle
                        oDefectList(nIndex).Area = .BodyArea
                        oDefectList(nIndex).ZoneName = .ZoneName
                        oDefectList(nIndex).ReviewFileName = .DefectFileName
                    End With
                End Sub)

            ''Augustin 220525 Bypass temp
            Call moMyEquipment.WaferMap.AddDefectListByTarget(oDefectList.ToList())
            Call moMyEquipment.WaferMap.UpdateView()
        Catch ex1 As Exception
            Dim msg As String = ex1.Message & Environment.NewLine & ex1.StackTrace
        End Try
    End Sub

    Public Sub CopyAIImageFile(sSourcePath As String, sDestinationPath As String, Keyword As String)
        Dim oFileInfoList As New List(Of FileInfo)

        If IO.Directory.Exists(sSourcePath) = False Then
            moLog.LogError(String.Format("[{0:d4}] 來源路徑不存在，Path：{1}", mnSequence, sSourcePath))
            Exit Sub
        End If

        If IO.Directory.Exists(sDestinationPath) = False Then
            Call Directory.CreateDirectory(sDestinationPath)
        End If

        Try
            oFileInfoList = (From o In New DirectoryInfo(sSourcePath).GetFiles(String.Format(Keyword)) Order By o.Name).ToList
        Catch ex As Exception
            moLog.LogInformation(ex.ToString)
        End Try

        Try
            If oFileInfoList.Count - 1 = 0 Then
                Call moLog.LogError(String.Format("[{0:d4}] 來源資料夾沒有檔案，Path：{1}", mnSequence, sSourcePath))
                Exit Sub
            End If

            For i = 0 To oFileInfoList.Count - 1
                oFileInfoList.Item(i).CopyTo(String.Format("{0}\{1}", sDestinationPath, oFileInfoList.Item(i).Name))
            Next

            Call moLog.LogInformation(String.Format("[{0:d4}] 複製 AI 影像完成！", mnSequence))
        Catch ex1 As Exception
            Call moLog.LogError(String.Format("[{0:d4}] 複製檔案錯誤，Path：{1}。Error：{2}", mnSequence, sSourcePath, ex1.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' 複製 AI 影像
    ''' </summary>
    ''' <param name="sSourcePath"></param>
    ''' <param name="oInspectResult"></param>
    ''' <param name="oInspectSum"></param>
    ''' <remarks></remarks>
    Public Sub CopyAIImageFileForMulti(sSourcePath As String, oInspectResult As CInspectResult, oInspectSum As CInspectSum)
        Dim oFileInfoNGArrayList As FileInfo()
        Dim oFileInfoNoDieList As FileInfo() 'No Die
        Dim oFileInfoOffsetList As FileInfo() '位移
        Dim oFileInfoLoseAndRotateList As FileInfo() '蓋印漏雷/蓋印轉置

        Dim oDirectoryNGInfoList As New ArrayList()
        Dim oDirectoryNoDieInfoList As New ArrayList() 'No Die
        Dim oDirectoryOffsetInfoList As New ArrayList() '位移
        Dim oDirectoryLoseAndRotateInfoList As New ArrayList() '蓋印漏雷/蓋印轉置
        Dim oDirectoryInfo As DirectoryInfo = New DirectoryInfo(sSourcePath)

        If IO.Directory.Exists(sSourcePath) = False Then
            moLog.LogError(String.Format("[{0:d4}] 來源路徑不存在，Path：{1}", mnSequence, sSourcePath))
            Exit Sub
        End If

        If IO.Directory.Exists(oInspectResult.AINGPath) = False Then
            Call Directory.CreateDirectory(oInspectResult.AINGPath)
        End If

        If IO.Directory.Exists(oInspectResult.AINODIEPath) = False Then
            Call Directory.CreateDirectory(oInspectResult.AINODIEPath)
        End If

        If IO.Directory.Exists(oInspectResult.AIOffsetPath) = False Then
            Call Directory.CreateDirectory(oInspectResult.AIOffsetPath)
        End If

        If IO.Directory.Exists(oInspectResult.AILoseAndRotatePath) = False Then
            Call Directory.CreateDirectory(oInspectResult.AILoseAndRotatePath)
        End If

        Try
            For i = 0 To oInspectSum.DefectList.DefectList.Count - 1
                Select Case oInspectSum.DefectList.DefectList(i).ResultType '瑕疵-判斷條件
                    Case ResultType.Offset
                        oDirectoryOffsetInfoList.AddRange(oDirectoryInfo.GetFiles(String.Format("*R{0:d3}*C{1:d3}*.bmp", oInspectSum.DefectList.DefectList(i).DefectIndex.X, oInspectSum.DefectList.DefectList(i).DefectIndex.Y)))

                    Case ResultType.NoDie 'No Die-標記
                        oDirectoryNoDieInfoList.AddRange(oDirectoryInfo.GetFiles(String.Format("*R{0:d3}*C{1:d3}*.bmp", oInspectSum.DefectList.DefectList(i).DefectIndex.X, oInspectSum.DefectList.DefectList(i).DefectIndex.Y)))

                        'oDirectoryNoDieInfoList.AddRange(oDirectoryInfo.GetFiles(String.Format("*R{0:d3}*C{1:d3}*.bmp", oInspectSum.ProductConfig.DimensionX - oInspectSum.DefectList.DefectList(i).DefectIndex.X, oInspectSum.DefectList.DefectList(i).DefectIndex.Y+1)))

                    Case ResultType.NA
                        Dim sNA As String = ""

                    Case ResultType.NGBright
                        oDirectoryNGInfoList.AddRange(oDirectoryInfo.GetFiles(String.Format("*R{0:d3}*C{1:d3}*.bmp", oInspectSum.DefectList.DefectList(i).DefectIndex.X, oInspectSum.DefectList.DefectList(i).DefectIndex.Y)))

                    Case ResultType.NGDark
                        oDirectoryNGInfoList.AddRange(oDirectoryInfo.GetFiles(String.Format("*R{0:d3}*C{1:d3}*.bmp", oInspectSum.DefectList.DefectList(i).DefectIndex.X, oInspectSum.DefectList.DefectList(i).DefectIndex.Y)))

                    Case ResultType.Indistinct
                        oDirectoryNGInfoList.AddRange(oDirectoryInfo.GetFiles(String.Format("*R{0:d3}*C{1:d3}*.bmp", oInspectSum.DefectList.DefectList(i).DefectIndex.X, oInspectSum.DefectList.DefectList(i).DefectIndex.Y)))

                    Case ResultType.Lose
                        oDirectoryLoseAndRotateInfoList.AddRange(oDirectoryInfo.GetFiles(String.Format("*R{0:d3}*C{1:d3}*.bmp", oInspectSum.DefectList.DefectList(i).DefectIndex.X, oInspectSum.DefectList.DefectList(i).DefectIndex.Y)))

                    Case ResultType.OK
                        Dim sOK As String = ""
                End Select
            Next
        Catch ex As Exception
            Dim msg = ex.Message & Environment.NewLine & ex.StackTrace
        End Try

        Try
            oFileInfoNGArrayList = CType(oDirectoryNGInfoList.ToArray(GetType(FileInfo)), FileInfo())
            If oFileInfoNGArrayList.Count > 0 Then
                For i = 0 To oFileInfoNGArrayList.Count - 1
                    oFileInfoNGArrayList(i).CopyTo(String.Format("{0}\{1}", oInspectResult.AINGPath, oFileInfoNGArrayList(i).Name))
                Next
            End If

            oFileInfoNoDieList = CType(oDirectoryNoDieInfoList.ToArray(GetType(FileInfo)), FileInfo())
            If oFileInfoNoDieList.Count > 0 Then
                For i = 0 To oFileInfoNoDieList.Count - 1
                    Dim sImageName As String = ""
                    sImageName = oFileInfoNoDieList(i).Name.Replace("_NG", "_NoDie")
                    oFileInfoNoDieList(i).CopyTo(String.Format("{0}\{1}", oInspectResult.AINODIEPath, sImageName))
                Next
            End If

            oFileInfoOffsetList = CType(oDirectoryOffsetInfoList.ToArray(GetType(FileInfo)), FileInfo())
            If oFileInfoOffsetList.Count > 0 Then
                For i = 0 To oFileInfoOffsetList.Count - 1
                    oFileInfoOffsetList(i).CopyTo(String.Format("{0}\{1}", oInspectResult.AIOffsetPath, oFileInfoOffsetList(i).Name))
                Next

                If oFileInfoOffsetList.Count > moMyEquipment.MaxOffsetCountForUpdateToFttp AndAlso
                    moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.IsUpLoadMarkShiftPicture = UpLoadMarkShiftImage.OPEN Then
                    moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.UpLoadMarkShiftPictureToIT = "ON"
                Else
                    moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.UpLoadMarkShiftPictureToIT = "OFF"
                End If
                'Dim sOffsetPercentForUpdateToFtpPath As String = ""
                'Dim sLotID As String = ""
                'moProductProcess.GetLotID(sLotID)
                'sOffsetPercentForUpdateToFtpPath = String.Format("{0}\{1}_{2}_{3}_{4}_{5}", oInspectResult.AIOffsetToFtpPath,
                '                                                 oInspectSum.ProductConfig.Prodline, sLotID, moMainRecipe.RecipeID,
                '                                                 oInspectSum.ProductConfig.EQPID, oInspectSum.InspectResult.CodeID)

                'If IO.Directory.Exists(sOffsetPercentForUpdateToFtpPath) = False Then
                '    Call Directory.CreateDirectory(sOffsetPercentForUpdateToFtpPath)
                'End If

                'For i = 0 To oFileInfoOffsetList.Count - 1
                '    oFileInfoOffsetList(i).CopyTo(String.Format("{0}\{1}", sOffsetPercentForUpdateToFtpPath, oFileInfoOffsetList(i).Name))
                'Next
            Else
                moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.UpLoadMarkShiftPictureToIT = "OFF"
            End If

            oFileInfoLoseAndRotateList = CType(oDirectoryLoseAndRotateInfoList.ToArray(GetType(FileInfo)), FileInfo())
            If oFileInfoLoseAndRotateList.Count > 0 Then
                For i = 0 To oFileInfoLoseAndRotateList.Count - 1
                    Dim sImageName As String = ""
                    sImageName = oFileInfoLoseAndRotateList(i).Name.Replace("_NG", "_LoseAndRotate")
                    oFileInfoLoseAndRotateList(i).CopyTo(String.Format("{0}\{1}", oInspectResult.AILoseAndRotatePath, sImageName))
                Next
            End If

            Call moLog.LogInformation(String.Format("[{0:d4}] 複製 AI 影像完成！", mnSequence))
        Catch ex1 As Exception
            Call moLog.LogError(String.Format("[{0:d4}] 複製檔案錯誤，Path：{1}。Error：{2}", mnSequence, sSourcePath, ex1.ToString()))
        End Try

    End Sub

End Class