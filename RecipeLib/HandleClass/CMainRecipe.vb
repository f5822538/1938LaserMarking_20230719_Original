Imports System.Drawing.Design

Public Class CMainRecipe : Inherits CConfigBase

    Public Property RecipeID As String
    Private InitialPath As String

    <Browsable(False)> Public Property RecipeCamera As CRecipeCamera

    Private Class SR
        Public Const ExposureTime As String = "�۾��n���ɶ� (us)"

        Public Const PositionDeafetBypass As String = "��m (����) �岫 Bypass"
        Public Const WordDeafetBypass As String = "�r�岫 Bypass"
        Public Const CompoundDeafetBypass As String = "Compound �岫 Bypass"
        Public Const BinarizePicturePath As String = "�G�ȤƹϤ����|"
        Public Const Threshold As String = "�w��զǶ����e�� (0 ~ 255)"  '' Augustin 230531

    End Class

    <Index2Display(10, SR.ExposureTime)> Public Property ExposureTime As Double = 5000

    <Index2Display(20, SR.PositionDeafetBypass)> Public Property PositionDeafetBypass As Boolean = False
    <Index2Display(21, SR.WordDeafetBypass)> Public Property WordDeafetBypass As Boolean = False
    <Index2Display(22, SR.CompoundDeafetBypass)> Public Property CompoundDeafetBypass As Boolean = False
    <Index2Display(30, SR.BinarizePicturePath), Editor(GetType(CUIPathNameTypeEditor), GetType(UITypeEditor))> Public Property BinarizePicturePath As String = "D:\Binarize.Bmp"
    <Index2Display(40, SR.Threshold), Range(0, 255)> Public Property Threshold As Integer = 10

    Public Sub New(ByVal sInitialPath As String, bIsDefectSizeAnd As Boolean, dPixelSize As Double)
        MyBase.New(CIniCreator.CreateSimpleIni(sInitialPath, "Default", "RCP"))

        RecipeID = "Default"
        InitialPath = sInitialPath
        RecipeCamera = New CRecipeCamera(FindName(Function() RecipeCamera), moSetting, bIsDefectSizeAnd, dPixelSize)

        LoadConfig(RecipeID)
    End Sub
End Class