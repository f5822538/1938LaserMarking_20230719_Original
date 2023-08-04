Partial Class CYieldConfig : Implements II_LoadSave

    Public Sub LoadConfig(sName As String) Implements II_LoadSave.LoadConfig
        msFileName = sName
        moSetting.SaveAs(sName)
        moSetting.SimpleGet("System", Me)
    End Sub

    Public Sub SaveConfig(sName As String) Implements II_LoadSave.SaveConfig
        moSetting.SaveAs(msFileName)
        moSetting.DeleteFile()
        moSetting.SimpleSet("System", Me)
    End Sub
End Class