Imports System.Drawing
Imports iTVisionService.iTVImageLib
Imports RecipeLib
Imports Matrox.MatroxImagingLibrary
Imports iTVisionService

Public Class frmRecipeCodeReader

    Private moMyEquipment As CMyEquipment
    Private moRecipe As CMainRecipe
    Private moRecipeCodeReader As CRecipeCodeReader

    Private moImageID As MIL_ID
    Private moCurrentBitmap As Bitmap
    Private moCanvas As II_iTVCanvas
    Private moLog As II_LogTraceExtend

    Public Sub New(oMyEquipment As CMyEquipment, oLog As II_LogTraceExtend, oCurrentBitmap As Bitmap)
        InitializeComponent()

        moMyEquipment = oMyEquipment
        moRecipe = oMyEquipment.MainRecipe
        moCurrentBitmap = oCurrentBitmap
        moLog = oLog
        moCanvas = picView
        moRecipeCodeReader = oMyEquipment.MainRecipe.RecipeCamera.CodeReader
        moImageID = oMyEquipment.RecipeID
    End Sub

    Private Sub frmRecipeCamera_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        pgdCode.SelectedObject = moRecipeCodeReader

        moCanvas.IsDrawDefineRecipeCase0 = False
        moCanvas.IsDrawDefineRecipeCase1 = False
        moCanvas.IsDrawDefineRecipeCase2 = True
        moCanvas.IsDrawDefineRecipeCase3 = False
        moCanvas.IsDrawDefineRecipeCase4 = False
        moCanvas.IsDrawSelectedRecipeDefineIndex1 = True

        picView.ContextMenuStrip = mnuMark

        moCanvas.SetDisplayImage(moCurrentBitmap)
        moCanvas.SetCanvasDrawing(moRecipe.RecipeCamera)
        moCanvas.UpdateCanvas()
    End Sub

    Private Sub btnSave_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnSave.ClickButtonArea
        If moRecipeCodeReader.SearchRange.IsEmpty Then
            MsgBox("搜尋區域未設定！", MsgBoxStyle.OkOnly, "警告")
            Exit Sub
        End If

        moRecipe.SaveConfig(moRecipe.RecipeID)

        On Error Resume Next
        GC.Collect()
        moCurrentBitmap.Save(moRecipe.RecipeCamera.CodeReaderImagePath, Imaging.ImageFormat.Bmp)
        Call Me.Close()
    End Sub

    Private Sub btnQuit_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnQuit.ClickButtonArea
        moRecipe.LoadConfig(moRecipe.RecipeID)
        DeleteModel(moRecipe.RecipeCamera.RecipeModelDiff)

        If File.Exists(moRecipe.RecipeCamera.CodeReaderImagePath) = False Then
            Dim sInformation As String = "無標準影像！"
            Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moLog.LogError(sInformation)
        End If
        Call Me.Close()
    End Sub

    Private Sub btnCancel_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCancel.ClickButtonArea
        Call Me.Close()
    End Sub

    Private Sub mnuMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCodeSearchROI.Click, mnuCodeClearROI.Click
        Dim aRectangle As Rectangle = CType(moCanvas.SelectStructure(RecipeDrawType.DrawZone), Rectangle)

        Select Case True
            Case sender Is mnuCodeSearchROI
                moRecipeCodeReader.SearchRange = aRectangle
            Case sender Is mnuCodeClearROI
                If MsgBox("是否確定刪除 Code？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
                    moRecipeCodeReader.SearchRange = Rectangle.Empty
                End If
        End Select

        Call pgdCode.Refresh()
        Call moCanvas.UpdateCanvas()
    End Sub

    Private Sub usrCamera_SendMouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs, cGrey As System.Drawing.Color) Handles picView.SendMouseMove
        usrStatusCamera.MyLable.Text = String.Format("X = {0:d6}, Y = {1:d6} C = {2:d3}", e.X, e.Y, cGrey.G)
    End Sub
End Class