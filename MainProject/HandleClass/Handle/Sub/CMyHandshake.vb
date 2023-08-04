Public Class CMyHandshake

    Private moMyEquipment As CMyEquipment
    Private moHandshakeConfig As CHandshakeConfig
    Public HandshakeThread As CThreadServerExtend

    Public Sub New(oMyEquipment As CMyEquipment)
        moMyEquipment = oMyEquipment
        moHandshakeConfig = oMyEquipment.HardwareConfig.HandshakeConfig
    End Sub
End Class