Public Class CRecipeLocate : Inherits CConfigBase

    Private msParentName As String

    Private Class SR
        Public Const PatternZone As String = "樣本區域 (X,Y,W,H)"
        Public Const FindModelZone As String = "尋找區域 (X,Y,W,H)"
        Public Const CircleCenterX As String = "定位點圓心X座標"  '' Augustin 221228
        Public Const CircleCenterY As String = "定位點圓心Y座標"  '' Augustin 221228
        Public Const CircleRadius As String = "定位點圓半徑"  '' Augustin 221228
        Public Const CircleRadiusTolerance As String = "定位點圓半徑容許誤差值"  '' Augustin 221228
        Public Const Score As String = "尋找分數 (1 ~ 100)"
        Public Const Smoth As String = "模糊化百分比 (0 ~ 100)"

        'Public Const Threshold As String = "定位孔灰階門檻值 (0 ~ 255)"  '' Augustin 230531
        Public Const Area As String = "定位孔最小面積"
    End Class

    <Index2Display(10, SR.PatternZone)> Public Property PatternZone As Rectangle = Rectangle.Empty
    <Index2Display(11, SR.FindModelZone)> Public Property FindModelZone As Rectangle = Rectangle.Empty
    <Index2Display(12, SR.CircleCenterX)> Public Property CircleCenterX As Double = 0.0  '' Augustin 221228
    <Index2Display(13, SR.CircleCenterY)> Public Property CircleCenterY As Double = 0.0  '' Augustin 221228
    <Index2Display(14, SR.CircleRadius)> Public Property CircleRadius As Double = 17.0  '' Augustin 221228
    <Index2Display(15, SR.CircleRadiusTolerance)> Public Property CircleRadiusTolerance As Double = 5.0  '' Augustin 221228
    <Index2Display(16, SR.Score), Range(1, 100)> Public Property Score As Integer = 60
    <Index2Display(17, SR.Smoth), Range(0, 100)> Public Property Smoth As Integer = 60

    '<Index2Display(20, SR.Threshold), Range(0, 255)> Public Property Threshold As Integer = 30  '' Augustin 230531
    <Index2Display(21, SR.Area), Range(0, Integer.MaxValue)> Public Property Area As Integer = 100

    Public Sub New(sParentName As String, oSetting As II_Setting)
        MyBase.New(oSetting)
        msParentName = sParentName
    End Sub
End Class