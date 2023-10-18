Public Class CMyHandshake

    Private moMyEquipment As CMyEquipment
    Private moHandshakeConfig As CHandshakeConfig
    Public HandshakeThread As CThreadServerExtend

    Public Sub New(oMyEquipment As CMyEquipment)
        '-------------------------20231019-開始--------------------------
        SynchronizationContext.SetSynchronizationContext(frmMain.moSync)
        '-------------------------20231019-結束--------------------------

        moMyEquipment = oMyEquipment
        moHandshakeConfig = oMyEquipment.HardwareConfig.HandshakeConfig
    End Sub
End Class