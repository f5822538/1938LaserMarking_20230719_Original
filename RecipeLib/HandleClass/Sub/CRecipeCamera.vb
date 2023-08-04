Public Class CRecipeCamera ': Inherits CConfigBase

    '' Augustin 220726 Bypass for Wafer Map
    'Private msParentName As String

    Private Class SR
        Public Const RefXOfAlignMark1 As String = "定位參考值 X1"
        Public Const RefYOfAlignMark1 As String = "定位參考值 Y1"
        Public Const RefXOfAlignMark2 As String = "定位參考值 X2"
        Public Const RefYOfAlignMark2 As String = "定位參考值 Y2"
    End Class

    '' Augustin 220726 Bypass for Wafer Map
    '<Browsable(False)> Public Property Locate1 As CRecipeLocate
    '<Browsable(False)> Public Property Locate2 As CRecipeLocate
    '<Browsable(False)> Public Property CodeReader As CRecipeCodeReader
    '<Browsable(False)> Public Property CodeReaderForInspect As CRecipeCodeReader
    '<Browsable(False)> Public Property CodeReaderForInspect2 As CRecipeCodeReader
    '<Browsable(False)> Public Property RecipeModelDiff As CRecipeModelDiff
    '<Browsable(False)> Public Property RecipeWaferMap As CRecipeWaferMap 

    '<Index2Display(10, SR.RefXOfAlignMark1)> Public Property RefXOfAlignMark1 As Double = 0.0
    '<Index2Display(11, SR.RefYOfAlignMark1)> Public Property RefYOfAlignMark1 As Double = 0.0
    '<Index2Display(12, SR.RefXOfAlignMark2)> Public Property RefXOfAlignMark2 As Double = 0.0
    '<Index2Display(13, SR.RefYOfAlignMark2)> Public Property RefYOfAlignMark2 As Double = 0.0

    '<Browsable(False)> Public Property RecipeInspectCount As Integer = 0
    '<Browsable(False)> Public Property RecipeMarkExtendCount As Integer = 0
    '<Browsable(False)> Public Property RecipeDefectJudgeCount As Integer = 0

    '<Browsable(False)> Public Property TempleteImagePath As String = ""
    '<Browsable(False)> Public Property CodeReaderImagePath As String = ""
    '<Browsable(False)> Public Property ImageBeenLoad As Boolean = True

    'Public Sub New(sParentName As String, oSetting As II_Setting, bIsDefectSizeAnd As Boolean, dPixelSize As Double)
    '    MyBase.New(oSetting)
    '    msParentName = sParentName

    '    Locate1 = New CRecipeLocate(sParentName, moSetting)
    '    Locate2 = New CRecipeLocate(sParentName, moSetting)
    '    CodeReader = New CRecipeCodeReader(sParentName, moSetting)
    '    CodeReaderForInspect = New CRecipeCodeReader(sParentName, moSetting)
    '    CodeReaderForInspect2 = New CRecipeCodeReader(sParentName, moSetting)
    '    RecipeModelDiff = New CRecipeModelDiff(sParentName, moSetting, bIsDefectSizeAnd, dPixelSize)
    '    RecipeWaferMap = New CRecipeWaferMap(sParentName, moSetting) 
    'End Sub
End Class