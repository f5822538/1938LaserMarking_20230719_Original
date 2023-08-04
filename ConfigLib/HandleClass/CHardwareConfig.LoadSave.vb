Partial Class CHardwareConfig : Implements II_LoadSystem

    Public Sub LoadConfig() Implements II_LoadSystem.LoadConfig
        moSetting.SimpleGet(csSystemName, Me)
    End Sub

    Public Sub SaveConfig() Implements II_LoadSystem.SaveConfig
        moSetting.SimpleSet(csSystemName, Me)
    End Sub
End Class