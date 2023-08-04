Partial Class CMainRecipe : Implements II_LoadSave

    Public Sub LoadConfig(sName As String) Implements II_LoadSave.LoadConfig
        moSetting.SaveAs(sName)
        moSetting.SimpleGet("System", Me)
        RecipeCamera.TempleteImagePath = String.Format("{0}\{1}.bmp", InitialPath, RecipeID)
        RecipeCamera.CodeReaderImagePath = String.Format("{0}\{1}CodeReader.bmp", InitialPath, RecipeID)
        RecipeCamera.ObjectCopyToObject(RecipeCamera)
        RecipeCamera.Locate1.ObjectCopyToObject(RecipeCamera.Locate1)
        RecipeCamera.Locate2.ObjectCopyToObject(RecipeCamera.Locate2)
    End Sub

    Public Sub SaveConfig(sName As String) Implements II_LoadSave.SaveConfig
        RecipeID = sName
        moSetting.SaveAs(sName)
        moSetting.DeleteFile()
        moSetting.SimpleSet("System", Me)

        RecipeCamera.ObjectCopyToObject(RecipeCamera)
        RecipeCamera.Locate1.ObjectCopyToObject(RecipeCamera.Locate1)
        RecipeCamera.Locate2.ObjectCopyToObject(RecipeCamera.Locate2)
    End Sub
End Class