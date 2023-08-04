Public Class CMyCamera

    Private moMyEquipment As CMyEquipment
    Private moCameraConfig As CCameraConfig
    Private moCamera As II_Camera
    Private moSnapContinus As II_SnapContinus
    Private moCameraLightControl As II_CameraLightControl
    Private moLog As II_LogTraceExtend
    Private moCameraSnapStart As New ManualResetEventSlim(False)
    Private moCameraSnapEnd As New ManualResetEventSlim(False)

    Public ReadOnly Property Camera As II_Camera
        Get
            Return moCamera
        End Get
    End Property

    Public ReadOnly Property CameraLightControl As II_CameraLightControl
        Get
            Return moCameraLightControl
        End Get
    End Property

    Public Sub New(oMyEquipment As CMyEquipment, oCameraConfig As CCameraConfig)
        moMyEquipment = oMyEquipment
        moLog = moMyEquipment.MyLog.LogControl
        moCameraConfig = oCameraConfig
    End Sub
End Class