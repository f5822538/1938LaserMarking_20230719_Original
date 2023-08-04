Public Class CHandshakeConfig : Inherits CConfigBase

    Public Class SR
        Public Const HandshakeIP As String = "交握通訊 IP"
        Public Const HandshakePort As String = "交握通訊 Port"
        Public Const HandshakeTimeout As String = "交握通訊超時時間 (s)"
        Public Const WaitLightTimeout As String = "等待燈源移動超時時間 (s)"

        'Public Const IsProcessCode As String = "需檢測之代碼"
        'Public Const IsNACode As String = "檢測 NA 之代碼"
        Public Const IsOKCode As String = "檢測 OK 之代碼"
        Public Const IsNGCode As String = "檢測 NG 之代碼"
        Public Const IsGrayCode As String = "檢測 Gray 之代碼"
        Public Const IsNoDieCode As String = "No Die 之代碼"
        'Public Const IsNGDarkCode As String = "檢測 NG (暗) 之代碼"
        'Public Const IsNGBrightCode As String = "檢測 NG (亮) 之代碼"
        'Public Const IsOffsetCode As String = "檢測 Offset 之代碼"
        'Public Const IsLoseCode As String = "檢測 Lose (角度) 之代碼"
        'Public Const IsPassCode As String = "檢測 Pass 之代碼"
        'Public Const IsIndistinctCode As String = "檢測蓋印不清之代碼"
    End Class

    <Index2Display(10, SR.HandshakeIP)> Public Property HandshakeIP As String = "192.168.10.1"
    <Index2Display(11, SR.HandshakePort)> Public Property HandshakePort As Integer = 5002
    <Index2Display(12, SR.HandshakeTimeout)> Public Property HandshakeTimeout As Integer = 5
    <Index2Display(13, SR.WaitLightTimeout)> Public Property WaitLightTimeout As Integer = 5

    '<Index2Display(20, SR.IsProcessCode)> Public Property IsProcessCode As String = "0100"
    <Index2Display(20, SR.IsOKCode)> Public Property IsOKCode As String = "0100"
    <Index2Display(21, SR.IsNGCode)> Public Property IsNGCode As String = "0100"
    <Index2Display(22, SR.IsGrayCode)> Public Property IsGrayCode As String = "0100"
    <Index2Display(23, SR.IsNoDieCode)> Public Property IsNoDieCode As String = "0000"
    '<Index2Display(21, SR.IsNACode)> Public Property IsNACode As String = "330F"
    '<Index2Display(23, SR.IsNGDarkCode)> Public Property IsNGDarkCode As String = "0100"
    '<Index2Display(24, SR.IsNGBrightCode)> Public Property IsNGBrightCode As String = "0100"
    '<Index2Display(25, SR.IsOffsetCode)> Public Property IsOffsetCode As String = "330F"
    '<Index2Display(26, SR.IsLoseCode)> Public Property IsLoseCode As String = "330F"
    '<Index2Display(27, SR.IsPassCode)> Public Property IsPassCode As String = "330F"
    '<Index2Display(27, SR.IsIndistinctCode)> Public Property IsIndistinctCode As String = "330F"

    Public Sub New(oSetting As II_Setting)
        MyBase.New(oSetting)
    End Sub
End Class