Partial Class CHandshakeConfig : Implements II_LoadSave

    Public Sub LoadConfig(sName As String) Implements II_LoadSave.LoadConfig
        moSetting.SimpleGet(sName, Me)
    End Sub

    Public Sub SaveConfig(sName As String) Implements II_LoadSave.SaveConfig
        moSetting.SimpleSet(sName, Me)
    End Sub
End Class