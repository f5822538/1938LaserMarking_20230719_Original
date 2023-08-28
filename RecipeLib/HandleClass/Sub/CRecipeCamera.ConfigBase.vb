Imports iTVisionService

Partial Class CRecipeCamera : Inherits CConfigBase

    Private msParentName As String

    Public Sub New(sParentName As String, oSetting As II_Setting, bIsDefectSizeAnd As Boolean, dPixelSize As Double)
        MyBase.New(oSetting)
        msParentName = sParentName

        Locate1 = New CRecipeLocate(sParentName, moSetting)
        Locate2 = New CRecipeLocate(sParentName, moSetting)
        CodeReader = New CRecipeCodeReader(sParentName, moSetting)
        CodeReaderForInspect = New CRecipeCodeReader(sParentName, moSetting)
        CodeReaderForInspect2 = New CRecipeCodeReader(sParentName, moSetting)

        RecipeModelDiff = New CRecipeModelDiff(sParentName, moSetting, bIsDefectSizeAnd, dPixelSize)
        RecipeWaferMap = New CRecipeWaferMap(sParentName, moSetting)
    End Sub

    <Browsable(False)> Public Property Locate1 As CRecipeLocate
    <Browsable(False)> Public Property Locate2 As CRecipeLocate
    <Browsable(False)> Public Property CodeReader As CRecipeCodeReader
    <Browsable(False)> Public Property CodeReaderForInspect As CRecipeCodeReader
    <Browsable(False)> Public Property CodeReaderForInspect2 As CRecipeCodeReader
    <Browsable(False)> Public Property RecipeModelDiff As CRecipeModelDiff
    <Browsable(False)> Public Property RecipeWaferMap As CRecipeWaferMap

    <Index2Display(10, SR.RefXOfAlignMark1)> Public Property RefXOfAlignMark1 As Double = 0.0
    <Index2Display(11, SR.RefYOfAlignMark1)> Public Property RefYOfAlignMark1 As Double = 0.0
    <Index2Display(12, SR.RefXOfAlignMark2)> Public Property RefXOfAlignMark2 As Double = 0.0
    <Index2Display(13, SR.RefYOfAlignMark2)> Public Property RefYOfAlignMark2 As Double = 0.0

    <Browsable(False)> Public Property RecipeInspectCount As Integer = 0
    <Browsable(False)> Public Property RecipeMarkExtendCount As Integer = 0
    <Browsable(False)> Public Property RecipeDefectJudgeCount As Integer = 0

    <Browsable(False)> Public Property TempleteImagePath As String = ""
    <Browsable(False)> Public Property CodeReaderImagePath As String = ""
    <Browsable(False)> Public Property ImageBeenLoad As Boolean = True '是否載入原設定圖檔

    '<Index2Display(20, SR.MapZone)> Public Property MapZone As Rectangle = Rectangle.Empty
    '<Index2Display(21, SR.ColumnCount), Range(0, Integer.MaxValue)> Public Property ColumnCount As Integer = 10
    '<Index2Display(22, SR.RowCount), Range(0, Integer.MaxValue)> Public Property RowCount As Integer = 10

    '<Index2Display(30, SR.ColorInvalidDie)> Public Property ColorInvalidDie As Color = Color.White
    '<Index2Display(31, SR.ColorEffectiveDie)> Public Property ColorEffectiveDie As Color = Color.PaleTurquoise
    '<Index2Display(32, SR.ColorProcessingDie)> Public Property ColorProcessingDie As Color = Color.Orange
    '<Index2Display(33, SR.ColorOKDie)> Public Property ColorOKDie As Color = Color.Green
    '<Index2Display(34, SR.ColorNGDie)> Public Property ColorNGDie As Color = Color.Red
    '<Index2Display(35, SR.ColorDieRectangle)> Public Property ColorDieRectangle As Color = Color.Blue
    '<Index2Display(36, SR.ColorMapCircle)> Public Property ColorMapCircle As Color = Color.Thistle
    '<Index2Display(37, SR.ColorSelectedDie)> Public Property ColorSelectedDie As Color = Color.Yellow
    '<Index2Display(38, SR.ColorSelectedDefect)> Public Property ColorSelectedDefect As Color = Color.Maroon
    '<Index2Display(39, SR.ColorDefectCircle)> Public Property ColorDefectCircle As Color = Color.MediumPurple
    '<Index2Display(40, SR.ColorSelectedDefectCircle)> Public Property ColorSelectedDefectCircle As Color = Color.Purple
    '<Index2Display(41, SR.ColorMapIndex)> Public Property ColorMapIndex As Color = Color.Indigo
    '<Index2Display(42, SR.FontMapIndex)> Public Property FontMapIndex As Font = New Font("Arial", 5, FontStyle.Underline, GraphicsUnit.Point)
    '<Index2Display(43, SR.IsDrawMapIndex)> Public Property IsDrawMapIndex As Boolean = True
    '<Index2Display(44, SR.IsReverseMapIndexColumn)> Public Property IsReverseMapIndexColumn As Boolean = False
    '<Index2Display(45, SR.IsReverseMapIndexRow)> Public Property IsReverseMapIndexRow As Boolean = False
    '<Index2Display(46, SR.IsDrawMapCircle)> Public Property IsDrawMapCircle As Boolean = True
    '<Index2Display(47, SR.CircleIndentation)> Public Property CircleIndentation As Double = 0.5

    '<Index2Display(50, SR.CheckDefectThresholdHigh), Range(0, 255)> Public Property CheckDefectThresholdHigh As Integer = 100
    '<Index2Display(51, SR.CheckDefectThresholdLow), Range(0, 255)> Public Property CheckDefectThresholdLow As Integer = 200
    '<Index2Display(52, SR.DieSize)> Public Property DieSize As Size = New Size(1, 1)
    '<Index2Display(53, SR.MapIndexBase)> Public Property MapIndexBase As Integer = 0
    '<Index2Display(54, SR.NothingDie)> Public Property NothingDie As Integer = 0

End Class