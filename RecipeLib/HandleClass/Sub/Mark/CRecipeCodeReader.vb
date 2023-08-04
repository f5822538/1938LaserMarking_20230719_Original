Public Class CRecipeCodeReader : Inherits CConfigBase

    Private Class SR
        Public Const CodeReaderExposureTime1 As String = "條碼相機曝光時間 1 (us)"
        Public Const CodeReaderExposureTime2 As String = "條碼相機曝光時間 2 (us)"
        Public Const SearchRange As String = "搜尋範圍 (X,Y,W,H)"

        Public Const Foreground As String = "前景顏色"
        Public Const CellSizeMin As String = "單元大小 (最小)"
        Public Const CellSizeMax As String = "單元大小 (最大)"
        Public Const CellNumberMin As String = "單元數量 (最小)"
        Public Const CellNumberMax As String = "單元數量 (最大)"
    End Class

    <Index2Display(10, SR.CodeReaderExposureTime1)> Public Property CodeReaderExposureTime1 As Double = 40000
    <Index2Display(11, SR.CodeReaderExposureTime2)> Public Property CodeReaderExposureTime2 As Double = 70000
    <Index2Display(12, SR.SearchRange)> Public Property SearchRange As Rectangle = Rectangle.Empty

    <Index2Display(20, SR.Foreground)> Public Property Foreground As CodeReaderForeground = CodeReaderForeground.White
    <Index2Display(21, SR.CellSizeMin)> Public Property CellSizeMin As Double = 6
    <Index2Display(22, SR.CellSizeMax)> Public Property CellSizeMax As Double = 7
    <Index2Display(23, SR.CellNumberMin)> Public Property CellNumberMin As Double = 16
    <Index2Display(24, SR.CellNumberMax)> Public Property CellNumberMax As Double = 16

    Private msParentName As String
    Public CodeZone As Rectangle = Rectangle.Empty

    Public Sub New(sParentName As String, oSetting As II_Setting)
        MyBase.New(oSetting)
        msParentName = sParentName
    End Sub
End Class