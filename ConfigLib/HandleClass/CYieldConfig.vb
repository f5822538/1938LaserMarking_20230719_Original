Public Class CYieldConfig : Inherits CConfigBase

    Private Class SR
        Public Const TotalCount As String = "總數"
        Public Const OKCount As String = "OK 數"
        Public Const TotalCount_Die As String = "Die 總數"
        Public Const OKCount_Die As String = "Die OK 數"
    End Class

    <Index2Display(10, SR.TotalCount)> Public Property TotalCount As Integer = 0
    <Index2Display(20, SR.OKCount)> Public Property OKCount As Integer = 0
    <Index2Display(30, SR.TotalCount)> Public Property TotalCount_Die As Integer = 0
    <Index2Display(40, SR.OKCount)> Public Property OKCount_Die As Integer = 0

    Private msFileName As String = ""

    Public Sub New(sPathName As String, sFileName As String, sExtendName As String)
        Call MyBase.New(CIniCreator.CreateSimpleIni(sPathName, sFileName, sExtendName))
        msFileName = sFileName
    End Sub
End Class