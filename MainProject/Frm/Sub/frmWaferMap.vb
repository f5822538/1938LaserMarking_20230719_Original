Imports iTVisionService.DisplayLib
Imports iTVisionService
Imports RecipeLib

Public Class frmWaferMap

    Private moMyEquipment As CMyEquipment
    Private moMap As CMyMap
    Private moMainRecipe As CMainRecipe
    Private moLog As II_LogTraceExtend

    Public Sub New(oMyEquipment As CMyEquipment)
        InitializeComponent()

        moMyEquipment = oMyEquipment
        moMap = moMyEquipment.WaferMap
        moMainRecipe = oMyEquipment.MainRecipe
        moLog = oMyEquipment.LogSystem
        pgdWaferMap.SelectedObject = oMyEquipment.MainRecipe.RecipeCamera.RecipeWaferMap
    End Sub

    Private Sub frmWaferMap_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call moMap.SetTemporaryMapView(mvwMapView)
        Call moMap.SetTemporaryListView(dlvMapDieList)
        Call moMap.UpdateDisplay()
        Call mvwMapView.SetMapZoom(frmMain.MapZoom)
        Call UpdateWaferMap()
    End Sub

    Private Sub frmWaferMap_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Call moMap.ResetTemporaryMapView()
        Call moMap.ResetTemporaryDieListView()
    End Sub

    Private Sub pgdWaferMap_PropertyValueChanged(s As System.Object, e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgdWaferMap.PropertyValueChanged
        If moMap.ColorInvalid <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorInvalidDie Then moMap.SetBinColor(BinType.InvalidDie, moMainRecipe.RecipeCamera.RecipeWaferMap.ColorInvalidDie)
        If moMap.ColorEffective <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorEffectiveDie Then moMap.SetBinColor(BinType.EffectiveDie, moMainRecipe.RecipeCamera.RecipeWaferMap.ColorEffectiveDie)
        If moMap.ColorProcessing <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorProcessingDie Then moMap.SetBinColor(BinType.ProcessingDie, moMainRecipe.RecipeCamera.RecipeWaferMap.ColorProcessingDie)
        If moMap.ColorOK <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorOKDie Then moMap.SetBinColor(BinType.OKDie, moMainRecipe.RecipeCamera.RecipeWaferMap.ColorOKDie)
        If moMap.ColorNG <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorNGDie Then moMap.SetBinColor(BinType.NGDie, moMainRecipe.RecipeCamera.RecipeWaferMap.ColorNGDie)
        If moMap.ColorMapIndex <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorMapIndex Then moMap.SetMapIndexColor(moMainRecipe.RecipeCamera.RecipeWaferMap.ColorMapIndex)
        If moMap.FontMapIndex IsNot moMainRecipe.RecipeCamera.RecipeWaferMap.FontMapIndex Then moMap.SetMapIndexFont(moMainRecipe.RecipeCamera.RecipeWaferMap.FontMapIndex)
        If mvwMapView.MapDieRectangleColor <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDieRectangle Then mvwMapView.MapDieRectangleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDieRectangle
        If mvwMapView.MapCircleColor <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorMapCircle Then mvwMapView.MapCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorMapCircle
        If mvwMapView.MapSelectedDieColor <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDie Then mvwMapView.MapSelectedDieColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDie
        If mvwMapView.MapSelectedDefectColor <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefect Then mvwMapView.MapSelectedDefectColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefect
        If mvwMapView.MapDefectCircleColor <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDefectCircle Then mvwMapView.MapDefectCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDefectCircle
        If mvwMapView.MapSelectedDefectCircleColor <> moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefectCircle Then mvwMapView.MapSelectedDefectCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefectCircle
        If mvwMapView.IsReverseMapIndexColumn <> moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexColumn Then mvwMapView.IsReverseMapIndexColumn = moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexColumn
        If mvwMapView.IsReverseMapIndexRow <> moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexRow Then mvwMapView.IsReverseMapIndexRow = moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexRow
        If mvwMapView.IsDrawNGDie <> moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGDie Then mvwMapView.IsDrawNGDie = moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGDie
        If mvwMapView.IsDrawNGFeature <> moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGFeature Then mvwMapView.IsDrawNGFeature = moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGFeature
    End Sub

    Private Sub btnSave_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnSave.ClickButtonArea
        moMainRecipe.SaveConfig(moMainRecipe.RecipeID)
        Call moMyEquipment.WaferMapCreate()
        Call moMyEquipment.UpdateDefectROI()
        Call Me.Close()
    End Sub

    Private Sub btnQuit_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnQuit.ClickButtonArea
        moMainRecipe.LoadConfig(moMainRecipe.RecipeID)
        Call moMyEquipment.WaferMapCreate()
        Call moMyEquipment.UpdateDefectROI()
        Call Me.Close()
    End Sub

    Private Sub btnCancel_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCancel.ClickButtonArea
        Call Me.Close()
    End Sub

    Private Sub mnuEffectiveToInvalid_Click(sender As System.Object, e As System.EventArgs) Handles mnuEffectiveToInvalid.Click
        Call dlvMapDieList.SelectedDieForListView.SetInvalidBin()
        Call UpdateWaferMap()
    End Sub

    Private Sub mnuInvalidToEffective_Click(sender As System.Object, e As System.EventArgs) Handles mnuInvalidToEffective.Click
        Call dlvMapDieList.SelectedDieForListView.SetEffectiveBin()
        Call UpdateWaferMap()
    End Sub

    Private Sub UpdateWaferMap()
        Call mvwMapView.UpdateMapImage()
        Call dlvMapDieList.Refresh()
    End Sub
End Class