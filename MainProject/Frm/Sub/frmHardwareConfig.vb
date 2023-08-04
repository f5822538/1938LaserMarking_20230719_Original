''' <summary>frmHardwareConfig</summary>
''' <remarks>
''' </remarks>
Public Class frmHardwareConfig
    Private moMyEquipment As CMyEquipment
    Private moHardwareConfig As CHardwareConfig
    Private moLog As II_LogTraceExtend

    Public Sub New(oMyEquipment As CMyEquipment, ByRef oLog As II_LogTraceExtend)
        InitializeComponent()

        moMyEquipment = oMyEquipment
        moHardwareConfig = oMyEquipment.HardwareConfig
        moLog = oLog
    End Sub

    Private Sub frmHardwareConfig_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        pgdSystem.SelectedObject = moHardwareConfig
        pgdCamera.SelectedObject = moHardwareConfig.CameraConfig
        pgdCodeReaderCamera.SelectedObject = moHardwareConfig.CodeReaderCameraConfig
        pgdHandshake.SelectedObject = moHardwareConfig.HandshakeConfig
        pgdMisc.SelectedObject = moHardwareConfig.MiscConfig

        tabHardwareConfig.TabPages.Remove(tabProcess)
    End Sub

    Private Sub btnSaveSingle_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnSaveSingle.ClickButtonArea
        Call moHardwareConfig.SaveConfig()
        Call moLog.LogInformation(String.Format("儲存硬體設定檔案"))
        Call Me.Dispose()
    End Sub

    Private Sub btnLoad_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnLoad.ClickButtonArea
        Call moHardwareConfig.LoadConfig()
        Call moLog.LogInformation(String.Format("載入硬體設定"))
    End Sub

    Private Sub btnCancel_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCancel.ClickButtonArea
        Call moLog.LogInformation(String.Format("硬體設定離開"))
        Call Me.Dispose()
    End Sub
End Class