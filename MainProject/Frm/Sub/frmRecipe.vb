Imports System.Drawing
Imports iTVisionService.iTVImageLib
Imports RecipeLib
Imports Matrox.MatroxImagingLibrary
Imports iTVisionService

''' <summary>frmRecipeCamera</summary>
''' <remarks></remarks>
Public Class frmRecipe

    Private moMyEquipment As CMyEquipment
    Private moRecipe As CMainRecipe
    Private moRecipeCamera As CRecipeCamera

    Private moImageID As MIL_ID
    Private moRotateImageID As MIL_ID
    Private moCurrentBitmap As Bitmap
    Private moCanvas As II_iTVCanvas
    Private moLog As II_LogTraceExtend

    Private moModelImageList As New List(Of CMyModelImage)
    Private mbSaveImage As Boolean = False
    Private mnMarkPitch As Integer = 60
    Private mnMarkXCount As Integer = 60
    Private mnMarkYCount As Integer = 60

    Public Sub New(oMyEquipment As CMyEquipment, oLog As II_LogTraceExtend, oCurrentBitmap As Bitmap)
        InitializeComponent()

        moMyEquipment = oMyEquipment
        moRecipe = oMyEquipment.MainRecipe
        moCurrentBitmap = oCurrentBitmap
        moLog = oLog
        moCanvas = picView
        moRecipeCamera = oMyEquipment.MainRecipe.RecipeCamera
        moImageID = oMyEquipment.RecipeID
    End Sub

    Private Sub frmRecipeCamera_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        pgdMain.SelectedObject = moRecipe
        pgdMark1.SelectedObject = moRecipeCamera.Locate1
        pgdMark2.SelectedObject = moRecipeCamera.Locate2
        pgdCode.SelectedObject = moRecipeCamera.CodeReaderForInspect
        pgdCode2.SelectedObject = moRecipeCamera.CodeReaderForInspect2
        pgdModelDiff.SelectedObject = moRecipeCamera.RecipeModelDiff

        moCanvas.IsDrawDefineRecipeCase0 = False
        moCanvas.IsDrawDefineRecipeCase1 = True
        moCanvas.IsDrawDefineRecipeCase2 = False
        moCanvas.IsDrawDefineRecipeCase3 = False
        moCanvas.IsDrawDefineRecipeCase4 = False
        moCanvas.IsDrawDefineRecipeCase5 = True
        moCanvas.IsDrawSelectedRecipeDefineIndex1 = True

        'layoutMark.RowStyles.Item(2).Height = 0
        'mnuMarkLine1St.Visible = False
        'mnuCodeSearchROI.Visible = False
        'mnuCodeClearROI.Visible = False

        picView.ContextMenuStrip = mnuMark

        ToolStripButtonMarkXCount.Text = CStr(moRecipeCamera.RecipeModelDiff.MarkXCount)
        ToolStripButtonMarkYCount.Text = CStr(moRecipeCamera.RecipeModelDiff.MarkYCount)

        MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, moCurrentBitmap.Width, moCurrentBitmap.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, moRotateImageID)
        MIL.MbufCopy(moImageID, moRotateImageID)

        moCanvas.SetDisplayImage(moCurrentBitmap)
        moCanvas.SetCanvasDrawing(moRecipeCamera)
        moCanvas.UpdateCanvas()
    End Sub

    ''' <summary>
    ''' 儲存參數       Save Recpe
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnSave.ClickButtonArea
        If moRecipeCamera.RecipeModelDiff.ModelTopLeft.IsEmpty OrElse moRecipeCamera.RecipeModelDiff.ModelSize.IsEmpty Then
            MsgBox("樣本比對區域未設定！", MsgBoxStyle.OkOnly, "警告")
            Exit Sub
        End If

        If moRecipeCamera.RecipeModelDiff.ModelSize.Width <> moRecipeCamera.RecipeModelDiff.ModelRectangle.Width OrElse moRecipeCamera.RecipeModelDiff.ModelSize.Height <> moRecipeCamera.RecipeModelDiff.ModelRectangle.Height Then
            moRecipeCamera.RecipeModelDiff.SummationSquareCount = 0
            ClearStandardDeviationModel(moRecipeCamera.RecipeModelDiff)
        End If
        SaveStandardDeviationModel(moRecipeCamera.RecipeModelDiff, Application.StartupPath & "\Recipe", moRecipe.RecipeID)

        If mbSaveImage = True Then '旋轉角度不為0
            moRecipeCamera.ImageBeenLoad = False '載入原設定圖檔-否
        End If

        moRecipe.SaveConfig(moRecipe.RecipeID)

        On Error Resume Next
        GC.Collect()
        If moRecipeCamera.ImageBeenLoad = False Then '載入原設定圖檔-否
            moCurrentBitmap.Save(moRecipeCamera.TempleteImagePath, Imaging.ImageFormat.Bmp)
        End If

        If moMyEquipment.BuildImageForLoad(moRecipeCamera.TempleteImagePath, moMyEquipment.ImageID, moMyEquipment.ImageHeader, -1, moMyEquipment.LogSystem) = False Then
            Dim sInformation As String = "圖檔載入失敗！"
            Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moLog.LogError(sInformation)
            Exit Sub
        End If

        If moRecipeCamera.Locate1.PatternZone = Rectangle.Empty AndAlso moRecipeCamera.Locate2.PatternZone = Rectangle.Empty OrElse moMyEquipment.AddModel(moMyEquipment.ImageID, moMyEquipment.ImageHeader, moRecipeCamera.Locate1.PatternZone, moRecipeCamera.Locate2.PatternZone) = False Then
            MsgBox("Camera Add Model 失敗！", MsgBoxStyle.OkOnly, "警告")
        End If

        UpdateModelList(moRecipe.RecipeCamera.RecipeModelDiff, moImageID)
        ''Augustin 220726 Add for Wafer Map
        Call moMyEquipment.WaferMapCreate()
        Call moMyEquipment.UpdateDefectROI()

        If moRotateImageID <> 0 Then MIL.MbufFree(moRotateImageID) : moRotateImageID = MIL.M_NULL
        Call Me.Close()
    End Sub

    Private Sub btnQuit_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnQuit.ClickButtonArea
        moRecipe.LoadConfig(moRecipe.RecipeID)
        DeleteModel(moRecipe.RecipeCamera.RecipeModelDiff)

        If File.Exists(moRecipeCamera.TempleteImagePath) Then
            GC.Collect()
            If moMyEquipment.BuildImageForLoad(moRecipeCamera.TempleteImagePath, moMyEquipment.ImageID, moMyEquipment.ImageHeader, -1, moMyEquipment.LogSystem) = True Then
                UpdateModelList(moRecipe.RecipeCamera.RecipeModelDiff, moMyEquipment.ImageID, moMyEquipment.MainRecipe.RecipeID)

                If moRecipeCamera.Locate1.PatternZone = Rectangle.Empty AndAlso moRecipeCamera.Locate2.PatternZone = Rectangle.Empty OrElse moMyEquipment.AddModel(moMyEquipment.ImageID, moMyEquipment.ImageHeader, moRecipeCamera.Locate1.PatternZone, moRecipeCamera.Locate2.PatternZone) = False Then
                    MsgBox("Camera Add Model 失敗！", MsgBoxStyle.OkOnly, "警告")
                End If
            Else
                Dim sInformation As String = "圖檔載入失敗！"
                Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                Call moLog.LogError(sInformation)
                DeleteModel(moRecipe.RecipeCamera.RecipeModelDiff)
            End If
        Else
            Dim sInformation As String = "無標準影像,將清除樣板比對資料！"
            Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moLog.LogError(sInformation)
            DeleteModel(moRecipe.RecipeCamera.RecipeModelDiff)
        End If

        If moRotateImageID <> 0 Then MIL.MbufFree(moRotateImageID) : moRotateImageID = MIL.M_NULL
        ''Augustin 220726 Add for Wafer Map
        Call moMyEquipment.WaferMapCreate()
        Call moMyEquipment.UpdateDefectROI()

        Call Me.Close()
    End Sub

    Private Sub btnCancel_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCancel.ClickButtonArea
        If moRotateImageID <> 0 Then MIL.MbufFree(moRotateImageID) : moRotateImageID = MIL.M_NULL
        Call Me.Close()
    End Sub

    ''' <summary>
    ''' FindModel-尋找樣本
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButtonFindModel_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonFindModel.Click
        Call UpdateModel(moRecipe.RecipeCamera.RecipeModelDiff, moImageID)
        Call Parallel.ForEach(moModelImageList, Sub(o)
                                                    CameraImageClear(o)
                                                End Sub)
        Call moModelImageList.Clear()
        Call FindModelAll(moImageID, moModelImageList, moRecipe.RecipeCamera.RecipeModelDiff, Nothing, Nothing, moLog, PatternMatchingType.PatternMatching1St, 0, False)
        Call pgdModelDiff.Refresh()
        Call MsgBox(String.Format("樣板數量：[{0}]", moModelImageList.Count()), MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
    End Sub

    ''' <summary>
    ''' BuildMarkPosition-創建標記位置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButtonBuildMarkPosition_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonBuildMarkPosition.Click
        If moModelImageList.Count < 1 Then
            Call MsgBox("樣板數量為 0，請先尋找樣本！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Exit Sub
        End If
        Dim nMinX As Integer = moMyEquipment.Camera.Camera.CameraWidth
        Dim nMinY As Integer = moMyEquipment.Camera.Camera.CameraHeight
        Dim nMaxX As Integer = 0
        Dim nMaxY As Integer = 0
        Dim MarkCount As Integer = 0

        With moRecipe.RecipeCamera.RecipeModelDiff
            Call .RecipeMarkList.RecipeMarkList.Clear()

            For nIndex As Integer = 0 To moModelImageList.Count - 1 '我是nIndex
                If nMinX > moModelImageList.Item(nIndex).PositionX Then nMinX = CInt(moModelImageList.Item(nIndex).PositionX)
                If nMaxX < moModelImageList.Item(nIndex).PositionX Then nMaxX = CInt(moModelImageList.Item(nIndex).PositionX)
                If nMinY > moModelImageList.Item(nIndex).PositionY Then nMinY = CInt(moModelImageList.Item(nIndex).PositionY)
                If nMaxY < moModelImageList.Item(nIndex).PositionY Then nMaxY = CInt(moModelImageList.Item(nIndex).PositionY)
            Next

            Dim OffsetX As Integer = (nMaxX - nMinX) \ (mnMarkXCount - 1)
            Dim OffsetY As Integer = (nMaxY - nMinY) \ (mnMarkYCount - 1)
            Dim nMarkPitchX As Integer = If(mnMarkPitch > OffsetX, OffsetX - 10, mnMarkPitch)
            Dim nMarkPitchY As Integer = If(mnMarkPitch > OffsetY, OffsetY - 10, mnMarkPitch)
            Dim oMarkList(moModelImageList.Count - 1) As Boolean

            For nIndexY As Integer = 0 To mnMarkYCount - 1
                For nIndexX As Integer = 0 To mnMarkXCount - 1
                    Dim nModelPositionMinX As Integer = 0
                    Dim nModelPositionMaxX As Integer = 0
                    Dim nModelPositionMinY As Integer = 0
                    Dim nModelPositionMaxY As Integer = 0
                    Dim bIsAddMark As Boolean = False
                    Dim nPreviousPosition As Integer = 0

                    If .RecipeMarkList.RecipeMarkList.Count > 0 Then
                        Select Case True
                            Case .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).IndexX = mnMarkXCount - 1
                                nModelPositionMinX = nMinX - nMarkPitchX
                                nModelPositionMaxX = nMinX + nMarkPitchX
                                nModelPositionMinY = CInt(.RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionY + OffsetY - nMarkPitchY)
                                nModelPositionMaxY = CInt(.RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionY + OffsetY + nMarkPitchY)
                            Case Else
                                nModelPositionMinX = CInt(.RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionX + OffsetX - nMarkPitchX)
                                nModelPositionMaxX = CInt(.RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionX + OffsetX + nMarkPitchX)
                                nModelPositionMinY = CInt(.RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionY - nMarkPitchY)
                                nModelPositionMaxY = CInt(.RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionY + nMarkPitchY)
                        End Select
                    Else
                        nModelPositionMinX = nMinX - nMarkPitchX
                        nModelPositionMaxX = nMinX + nMarkPitchX
                        nModelPositionMinY = nMinY - nMarkPitchY
                        nModelPositionMaxY = nMinY + nMarkPitchY
                    End If

                    For nIndex As Integer = 0 To moModelImageList.Count - 1 '我是nIndex
                        If oMarkList(nIndex) = True Then Continue For
                        If moModelImageList.Item(nIndex).PositionX > nModelPositionMinX AndAlso moModelImageList.Item(nIndex).PositionX < nModelPositionMaxX AndAlso moModelImageList.Item(nIndex).PositionY > nModelPositionMinY AndAlso moModelImageList.Item(nIndex).PositionY < nModelPositionMaxY Then
                            Dim oRecipeMark As CRecipeMark = .CreateRecipeMarkConfig()
                            oRecipeMark.IndexX = nIndexX
                            oRecipeMark.IndexY = nIndexY

                            Select Case True
                                Case .RecipeMarkList.RecipeMarkList.Count < 1
                                    oRecipeMark.MarkX = 0
                                    oRecipeMark.MarkY = 0
                                Case .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).IndexX = mnMarkXCount - 1
                                    oRecipeMark.MarkX = 0
                                    oRecipeMark.MarkY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).MarkY + 1
                                Case Else
                                    oRecipeMark.MarkX = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).MarkX + 1
                                    oRecipeMark.MarkY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).MarkY
                            End Select

                            oRecipeMark.PositionX = moModelImageList.Item(nIndex).PositionX
                            oRecipeMark.PositionY = moModelImageList.Item(nIndex).PositionY
                            oRecipeMark.PositionCenterX = oRecipeMark.PositionX + (.ModelSize.Width \ 2)
                            oRecipeMark.PositionCenterY = oRecipeMark.PositionY + (.ModelSize.Height \ 2)
                            .RecipeMarkList.RecipeMarkList.Add(oRecipeMark)
                            oMarkList(nIndex) = True
                            bIsAddMark = True
                            MarkCount += 1
                            Exit For
                        End If
                    Next
                    If bIsAddMark = False Then
                        .RecipeMarkList.RecipeMarkList.Add(.CreateRecipeMarkConfig())

                        .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).IsUse = False

                        Select Case True
                            Case .RecipeMarkList.RecipeMarkList.Count <= 1
                                moRecipe.RecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(0).IndexX = 0
                                moRecipe.RecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(0).IndexY = 0
                                moRecipe.RecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(0).MarkX = 0
                                moRecipe.RecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(0).MarkY = 0
                                moRecipe.RecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(0).PositionX = nMinX
                                moRecipe.RecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Item(0).PositionY = nMinY
                            Case .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).IndexX = mnMarkXCount - 1
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).IndexX = 0
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).IndexY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).IndexY + 1
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).MarkX = 0
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).MarkY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).MarkY + 1
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionX = nMinX
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).PositionY + OffsetY
                            Case Else
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).IndexX = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).IndexX + 1
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).IndexY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).IndexY
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).MarkX = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).MarkX
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).MarkY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).MarkY
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionX = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).PositionX + OffsetX
                                .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 1).PositionY = .RecipeMarkList.RecipeMarkList.Item(.RecipeMarkList.RecipeMarkList.Count - 2).PositionY
                        End Select
                    End If
                Next
            Next

            For nIndex As Integer = moRecipe.RecipeCamera.RecipeModelDiff.RecipeMarkList.RecipeMarkList.Count - 1 To 0 Step -1 '我是nIndex
                If .RecipeMarkList.RecipeMarkList.Item(nIndex).IsUse = False Then .RecipeMarkList.RecipeMarkList.RemoveAt(nIndex)
            Next
        End With

        Call moRecipe.RecipeCamera.RecipeModelDiff.BuildModelCenterDraw()
        Call moCanvas.UpdateCanvas()
        Call MsgBox(String.Format("樣板數量：[{0}]", MarkCount), MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
    End Sub

    ''' <summary>
    ''' Mark 1 - 樣本 ROI
    ''' Mark 1 - 尋找範圍 ROI
    ''' Mark 1 - 清除 ROI
    ''' Mark 2 - 樣本 ROI
    ''' Mark 2 - 尋找範圍 ROI
    ''' Mark 2 - 清除 ROI
    ''' Code - 尋找範圍 ROI
    ''' Code - 清除 ROI
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMark1PatternROI.Click, mnuMark1FindModelROI.Click, mnuMark1ClearROI.Click, mnuMark2PatternROI.Click, mnuMark2FindModelROI.Click, mnuMark2ClearROI.Click, mnuCodeSearchROI.Click, mnuCodeClearROI.Click
        Dim aRectangle As Rectangle = CType(moCanvas.SelectStructure(RecipeDrawType.DrawZone), Rectangle)
        Dim NewRectangle As Rectangle
        Select Case True
            Case sender Is mnuMark1PatternROI
                moRecipeCamera.Locate1.PatternZone = aRectangle
                '' Augustin 221228
                If moMyEquipment.Locater1.FindCircle(moCurrentBitmap, aRectangle) Then
                    moRecipeCamera.Locate1.CircleCenterX = moMyEquipment.Locater1.CircleLocationX
                    moRecipeCamera.Locate1.CircleCenterY = moMyEquipment.Locater1.CircleLocationY
                    moRecipeCamera.Locate1.CircleRadius = moMyEquipment.Locater1.CircleRadius

                    Dim RadiusMax As Double = moMyEquipment.Locater1.CircleRadius + moRecipeCamera.Locate1.CircleRadiusTolerance

                    NewRectangle = New Rectangle(CInt(moRecipeCamera.Locate1.CircleCenterX - RadiusMax), _
                                                 CInt(moRecipeCamera.Locate1.CircleCenterY - RadiusMax), _
                                                 CInt(RadiusMax * 2), _
                                                 CInt(RadiusMax * 2))
                    moRecipeCamera.Locate1.PatternZone = NewRectangle
                Else
                    MsgBox("請確認框框沒有切到圓，並重新框選")
                End If
            Case sender Is mnuMark1FindModelROI
                moRecipeCamera.Locate1.FindModelZone = aRectangle
            Case sender Is mnuMark1ClearROI
                If MsgBox("是否確定刪除 Mark 1？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
                    moRecipeCamera.Locate1.PatternZone = Rectangle.Empty
                    moRecipeCamera.Locate1.FindModelZone = Rectangle.Empty
                End If
            Case sender Is mnuMark2PatternROI
                moRecipeCamera.Locate2.PatternZone = aRectangle
                '' Augustin 221228
                If moMyEquipment.Locater2.FindCircle(moCurrentBitmap, aRectangle) Then
                    moRecipeCamera.Locate2.CircleCenterX = moMyEquipment.Locater2.CircleLocationX
                    moRecipeCamera.Locate2.CircleCenterY = moMyEquipment.Locater2.CircleLocationY
                    moRecipeCamera.Locate2.CircleRadius = moMyEquipment.Locater2.CircleRadius

                    Dim RadiusMax As Double = moMyEquipment.Locater2.CircleRadius + moRecipeCamera.Locate2.CircleRadiusTolerance

                    NewRectangle = New Rectangle(CInt(moRecipeCamera.Locate2.CircleCenterX - RadiusMax), _
                             CInt(moRecipeCamera.Locate2.CircleCenterY - RadiusMax), _
                             CInt(RadiusMax * 2), _
                             CInt(RadiusMax * 2))
                    moRecipeCamera.Locate2.PatternZone = NewRectangle
                Else
                    MsgBox("請確認框框沒有切到圓，並重新框選")
                End If
            Case sender Is mnuMark2FindModelROI
                moRecipeCamera.Locate2.FindModelZone = aRectangle
            Case sender Is mnuMark2ClearROI
                If MsgBox("是否確定刪除 Mark 2？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
                    moRecipeCamera.Locate2.PatternZone = Rectangle.Empty
                    moRecipeCamera.Locate2.FindModelZone = Rectangle.Empty
                End If
            Case sender Is mnuCodeSearchROI
                moRecipeCamera.CodeReaderForInspect.SearchRange = aRectangle
                moRecipeCamera.CodeReaderForInspect2.SearchRange = aRectangle
            Case sender Is mnuCodeClearROI
                If MsgBox("是否確定刪除 Code？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
                    moRecipeCamera.CodeReaderForInspect.SearchRange = Rectangle.Empty
                End If
        End Select

        Call pgdMark1.Refresh()
        Call pgdMark2.Refresh()
        Call pgdCode.Refresh()
        Call pgdCode2.Refresh()
        Call moCanvas.UpdateCanvas()
    End Sub

    ''' <summary>
    ''' btnRotate-轉正
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRotate_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnRotate.ClickButtonArea
        Dim nCurrentX As Double = moCurrentBitmap.Width / 2
        Dim nCurrentY As Double = moCurrentBitmap.Height / 2
        Dim nRotateAngle As Double = nudRotateAngle.Value
        Call MIL.MimRotate(moRotateImageID, moImageID, nRotateAngle, nCurrentX, nCurrentY, nCurrentX, nCurrentY, MIL.M_BILINEAR + MIL.M_OVERSCAN_CLEAR)

        If nudRotateAngle.Value = 0 Then '旋轉角度為0
            mbSaveImage = False '旋轉角度為0
        Else '旋轉角度不為0
            mbSaveImage = True '旋轉角度不為0
        End If

        Call moCanvas.UpdateCanvas() '更新-畫布
    End Sub

    Private Sub mnuApplyModelRegion1St_Click(sender As System.Object, e As System.EventArgs) Handles mnuApplyModelRegion1St.Click
        Dim aRectangle As Rectangle = CType(moCanvas.SelectStructure(RecipeDrawType.DrawZone), Rectangle)
        Dim tTopLeft As Point, tSize As Size, tCenter As Point

        tTopLeft = aRectangle.Location
        '' Augustin 230330 
        tSize.Height = moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.ModelSizeHum \ CInt(moMyEquipment.HardwareConfig.CameraConfig.PixelSize)
        tSize.Width = moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.ModelSizeWum \ CInt(moMyEquipment.HardwareConfig.CameraConfig.PixelSize)
        'tSize = aRectangle.Size

        '' Augustin 230428
        tTopLeft.X = CInt(tTopLeft.X - (tSize.Width / 2))
        tTopLeft.Y = CInt(tTopLeft.Y - (tSize.Height / 2))

        If tTopLeft.IsEmpty AndAlso tSize.IsEmpty Then
            Call MsgBox("未設區塊資料！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        End If

        moRecipe.RecipeCamera.RecipeModelDiff.ModelTopLeft = tTopLeft
        moRecipe.RecipeCamera.RecipeModelDiff.ModelSize = tSize

        dgvModelDiff.Refresh()
        pgdModelDiff.Refresh()

        Call moCanvas.UpdateCanvas()
    End Sub

    Private Sub mnuClearModelRegion1St_Click(sender As System.Object, e As System.EventArgs) Handles mnuClearModelRegion1St.Click
        With moRecipe.RecipeCamera.RecipeModelDiff
            .ModelTopLeft = Point.Empty
            .ModelSize = System.Drawing.Size.Empty
        End With
        dgvModelDiff.Refresh()
        pgdModelDiff.Refresh()
        Call moCanvas.UpdateCanvas()
    End Sub

    Private Sub mnuApplySearchRegion_Click(sender As System.Object, e As System.EventArgs) Handles mnuApplySearchRegion.Click
        Dim aRectangle As Rectangle = CType(moCanvas.SelectStructure(RecipeDrawType.DrawZone), Rectangle)

        If aRectangle.IsEmpty Then
            Call MsgBox("未設區塊資料！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        End If

        moRecipe.RecipeCamera.RecipeModelDiff.SearchRange = aRectangle

        dgvModelDiff.Refresh()
        pgdModelDiff.Refresh()

        Call moCanvas.UpdateCanvas()
    End Sub

    Private Sub mnuClearSearchRegion_Click(sender As System.Object, e As System.EventArgs) Handles mnuClearSearchRegion.Click
        With moRecipe.RecipeCamera.RecipeModelDiff
            .SearchRange = Rectangle.Empty
        End With
        dgvModelDiff.Refresh()
        pgdModelDiff.Refresh()
        Call moCanvas.UpdateCanvas()
    End Sub

    Private Sub pgdModelDiff_PropertyValueChanged(s As System.Object, e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgdModelDiff.PropertyValueChanged
        pgdModelDiff.Refresh()
    End Sub

    Private Sub TabRecipeList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tabFunction.SelectedIndexChanged
        moCanvas.IsDrawDefineRecipeCase0 = False
        moCanvas.IsDrawDefineRecipeCase1 = False
        moCanvas.IsDrawDefineRecipeCase2 = False
        moCanvas.IsDrawDefineRecipeCase3 = False
        moCanvas.IsDrawDefineRecipeCase4 = False
        moCanvas.IsDrawDefineRecipeCase5 = False
        moCanvas.IsDrawSelectedRecipeDefineIndex1 = False
        moCanvas.IsDrawSelectedRecipeDefineIndex2 = False
        moCanvas.IsDrawSelectedRecipeDefineIndex3 = False
        moCanvas.IsDrawSelectedRecipeDefineIndex4 = False

        Select Case True
            Case tabFunction.SelectedTab Is tabRecipeMark
                moCanvas.IsDrawDefineRecipeCase1 = True
                moCanvas.IsDrawDefineRecipeCase5 = True
                moCanvas.IsDrawSelectedRecipeDefineIndex1 = True
                picView.ContextMenuStrip = mnuMark
            Case tabFunction.SelectedTab Is tabModelDiff
                moCanvas.IsDrawDefineRecipeCase3 = True
                moCanvas.IsDrawDefineRecipeCase4 = True
                moCanvas.IsDrawSelectedRecipeDefineIndex3 = True
                picView.ContextMenuStrip = mnuModelDiff
        End Select

        moCanvas.UpdateCanvas()
    End Sub

    Private Sub usrCamera_SendMouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs, cGrey As System.Drawing.Color) Handles picView.SendMouseMove
        usrStatusCamera.MyLable.Text = String.Format("X = {0:d6}, Y = {1:d6} C = {2:d3}", e.X, e.Y, cGrey.G)
    End Sub

    Private Sub ToolStripButtonMarkPitch_TextChanged(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonMarkPitch.TextChanged
        Try
            mnMarkPitch = CInt(ToolStripButtonMarkPitch.Text)
        Catch ex As Exception
            ToolStripButtonMarkPitch.Text = mnMarkPitch.ToString()
        End Try
    End Sub

    Private Sub ToolStripButtonMarkXCount_TextChanged(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonMarkXCount.TextChanged
        Try
            mnMarkXCount = CInt(ToolStripButtonMarkXCount.Text)
        Catch ex As Exception
            ToolStripButtonMarkXCount.Text = mnMarkXCount.ToString()
        End Try
    End Sub

    Private Sub ToolStripButtonMarkYCount_TextChanged(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonMarkYCount.TextChanged
        Try
            mnMarkYCount = CInt(ToolStripButtonMarkYCount.Text)
        Catch ex As Exception
            ToolStripButtonMarkYCount.Text = mnMarkYCount.ToString()
        End Try
    End Sub

    Public Function GetCorner(ByVal oSource As ImageHeader, ByRef oROI1 As Rectangle, nDirection1 As ITV_Direction, ByVal nIndex1 As Integer, oROI2 As Rectangle, nDirection2 As ITV_Direction, ByVal nIndex2 As Integer, ByRef X As Double, ByRef Y As Double) As Integer
        X = 0
        Y = 0
        Dim oLine As New ITVLineDetector, oImage As New ITVImage, oROI As New ITVImageROI
        oImage.AssignToImageHeader(oSource)
        oROI.ApplyRegion(oROI1)
        oLine.FindLinesFull(oImage, nDirection1, 10, False, 0, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        'oLine.FindLinesWithoutAngle(oImage, nDirection1, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        If nIndex1 + 1 > oLine.NumberOfResult Then Return 0
        If nDirection1 = ITV_Direction.LeftToRight OrElse nDirection1 = ITV_Direction.RightToLeft Then X = oLine.Result(nIndex1).LocationX Else Y = oLine.Result(nIndex1).LocationY

        oROI.ApplyRegion(oROI2)
        oLine.FindLinesFull(oImage, nDirection2, 10, False, 0, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        'oLine.FindLinesWithoutAngle(oImage, nDirection2, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        If nIndex2 + 1 > oLine.NumberOfResult Then Return 0
        If nDirection2 = ITV_Direction.LeftToRight OrElse nDirection2 = ITV_Direction.RightToLeft Then X = oLine.Result(nIndex2).LocationX Else Y = oLine.Result(nIndex2).LocationY
        Return 1
    End Function

    Public Function AnalysisCorner(ByVal oSource As ImageHeader, ByRef oROI1 As Rectangle, nDirection1 As ITV_Direction, ByVal nIndex1 As Integer, oROI2 As Rectangle, nDirection2 As ITV_Direction, ByVal nIndex2 As Integer, ByRef nNum1 As Integer, ByRef nNum2 As Integer) As Integer
        Dim oLine As New ITVLineDetector, oImage As New ITVImage, oROI As New ITVImageROI
        oImage.AssignToImageHeader(oSource)

        oROI.ApplyRegion(oROI1)
        oLine.FindLinesWithoutAngle(oImage, nDirection1, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        nNum1 = oLine.NumberOfResult

        oROI.ApplyRegion(oROI2)
        oLine.FindLinesWithoutAngle(oImage, nDirection2, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        nNum2 = oLine.NumberOfResult
        If nNum1 = 0 OrElse nNum2 = 0 Then Return 0 Else Return 1
    End Function

End Class