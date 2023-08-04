Imports iTVisionService

Partial Class CRecipeWaferMap : Inherits CConfigBase
    Public Sub New(sParentName As String, oSetting As II_Setting)
        MyBase.New(oSetting)
        msParentName = sParentName
    End Sub

    <Index2Display(10, SR.ColorInvalidDie)> Public Property ColorInvalidDie As Color = Color.White
    <Index2Display(11, SR.ColorEffectiveDie)> Public Property ColorEffectiveDie As Color = Color.PaleTurquoise
    <Index2Display(12, SR.ColorProcessingDie)> Public Property ColorProcessingDie As Color = Color.Orange
    <Index2Display(13, SR.ColorOKDie)> Public Property ColorOKDie As Color = Color.Green
    <Index2Display(14, SR.ColorNGDie)> Public Property ColorNGDie As Color = Color.Red
    <Index2Display(15, SR.ColorDieRectangle)> Public Property ColorDieRectangle As Color = Color.Blue
    <Index2Display(16, SR.ColorMapCircle)> Public Property ColorMapCircle As Color = Color.Thistle
    <Index2Display(17, SR.ColorSelectedDie)> Public Property ColorSelectedDie As Color = Color.Yellow
    <Index2Display(18, SR.ColorSelectedDefect)> Public Property ColorSelectedDefect As Color = Color.Maroon
    <Index2Display(19, SR.ColorDefectCircle)> Public Property ColorDefectCircle As Color = Color.MediumPurple
    <Index2Display(20, SR.ColorSelectedDefectCircle)> Public Property ColorSelectedDefectCircle As Color = Color.Purple
    <Index2Display(21, SR.ColorMapIndex)> Public Property ColorMapIndex As Color = Color.Indigo
    <Index2Display(22, SR.FontMapIndex)> Public Property FontMapIndex As Font = New Font("Arial", 12, FontStyle.Underline, GraphicsUnit.Point)

    <Index2Display(30, SR.MapIndexBase)> Public Property MapIndexBase As Integer = 1
    <Index2Display(31, SR.IsReverseMapIndexColumn)> Public Property IsReverseMapIndexColumn As Boolean = True
    <Index2Display(32, SR.IsReverseMapIndexRow)> Public Property IsReverseMapIndexRow As Boolean = False
    <Index2Display(33, SR.IsDrawNGDie)> Public Property IsDrawNGDie As Boolean = False
    <Index2Display(34, SR.IsDrawNGFeature)> Public Property IsDrawNGFeature As Boolean = False

End Class