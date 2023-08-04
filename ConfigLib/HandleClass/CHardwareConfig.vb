Public Class CHardwareConfig : Inherits CConfigBase

    Private Class SR
        Public Const Title As String = "應用軟體名稱 Application Name"
        Public Const DebugMode As String = "是否為除錯模式 Debug Mode"
        Public Const IOBypass As String = "IO Bypass"
        Public Const TriggerBypass As String = "觸發 Bypass"
        Public Const HandshakeBypass As String = "交握 Bypass"
        Public Const ITBypass As String = "IT Bypass"
        Public Const AIHandshakeBypass As String = "AI交握 Bypass"
        Public Const InspectBypass As String = "檢測 Bypass"
        Public Const CodeReaderBypass As String = "條碼讀取 Bypass"
        Public Const OutputErrorBypass As String = "輸出警報 Bypass"
        Public Const BuzzerBypass As String = "蜂鳴器 Bypass"
        Public Const OffsetLimitUse As String = "Offset 限制"
        Public Const OffsetLimitValue As String = "限制最大位移量(um)"
    End Class

    Private moCameraConfig As CCameraConfig
    Private moCodeReaderCameraConfig As CCameraConfig
    Private moHandshakeConfig As CHandshakeConfig
    Private moMiscConfig As CMiscConfig

    <Index2Display(10, SR.Title)> Public Property Title As String = "銓發科技 - Laser Marking AOI"

    <Index2Display(20, SR.DebugMode)> Public Property Debug As Boolean = False
    <Index2Display(21, SR.IOBypass)> Public Property IOBypass As Boolean = False
    <Index2Display(22, SR.TriggerBypass)> Public Property TriggerBypass As Boolean = False
    <Index2Display(23, SR.HandshakeBypass)> Public Property HandshakeBypass As Boolean = False
    <Index2Display(24, SR.ITBypass)> Public Property ITBypass As Boolean = False
    <Index2Display(25, SR.AIHandshakeBypass)> Public Property AIHandshakeBypass As Boolean = False
    <Index2Display(26, SR.InspectBypass)> Public Property InspectBypass As Boolean = False
    <Index2Display(27, SR.CodeReaderBypass)> Public Property CodeReaderBypass As Boolean = False
    <Index2Display(28, SR.OutputErrorBypass)> Public Property OutputErrorBypass As Boolean = False
    <Index2Display(29, SR.BuzzerBypass)> Public Property BuzzerBypass As Boolean = False
    <Index2Display(30, SR.OffsetLimitUse)> Public Property OffsetLimitUse As Boolean = False
    <Index2Display(31, SR.OffsetLimitValue)> Public Property OffsetLimitValue As Integer = 3000

    <Browsable(False)> Public ReadOnly Property CameraConfig As CCameraConfig
        Get
            Return moCameraConfig
        End Get
    End Property

    <Browsable(False)> Public ReadOnly Property CodeReaderCameraConfig As CCameraConfig
        Get
            Return moCodeReaderCameraConfig
        End Get
    End Property

    <Browsable(False)> Public ReadOnly Property HandshakeConfig As CHandshakeConfig
        Get
            Return moHandshakeConfig
        End Get
    End Property

    <Browsable(False)> Public ReadOnly Property MiscConfig As CMiscConfig
        Get
            Return moMiscConfig
        End Get
    End Property

    Public Sub New(sPathName As String, sFileName As String, sExtendName As String)
        Call MyBase.New(CIniCreator.CreateSimpleIni(sPathName, sFileName, sExtendName))

        moCameraConfig = New CCameraConfig(moSetting)
        moCodeReaderCameraConfig = New CCameraConfig(moSetting)
        moHandshakeConfig = New CHandshakeConfig(moSetting)
        moMiscConfig = New CMiscConfig(moSetting)
    End Sub
End Class