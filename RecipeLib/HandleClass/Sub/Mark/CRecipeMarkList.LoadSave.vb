Partial Class CRecipeMarkList : Implements II_LoadSave

    Public Sub LoadConfig(sName As String) Implements II_LoadSave.LoadConfig
        Dim oResult As Tuple(Of Boolean, String()) = moSetting.GetSectionValue(sName)

        If oResult.Item1 = True Then
            RecipeMarkCount = moSetting.GetKeyValue(sName, "RecipeMarkCount", 0)
            MarkListString = oResult.Item2(1).Replace("MarkListString=", "")

            RecipeMarkList.Clear()
            Dim sMarkList As String() = Split(MarkListString, ",")
            For nMarkIndex = 0 To RecipeMarkCount - 1
                Dim oRecipeMark As New CRecipeMark(moSetting)
                Dim nIndex As Integer = nMarkIndex * 6 '我是nIndex
                oRecipeMark.MarkX = CInt(sMarkList(nIndex + 0))
                oRecipeMark.MarkY = CInt(sMarkList(nIndex + 1))
                oRecipeMark.PositionCenterX = CInt(sMarkList(nIndex + 2))
                oRecipeMark.PositionCenterY = CInt(sMarkList(nIndex + 3))
                oRecipeMark.PositionX = CInt(sMarkList(nIndex + 4))
                oRecipeMark.PositionY = CInt(sMarkList(nIndex + 5))
                RecipeMarkList.Add(oRecipeMark)
            Next
        End If
    End Sub

    Public Sub SaveConfig(sName As String) Implements II_LoadSave.SaveConfig
        RecipeMarkCount = RecipeMarkList.Count
        MarkListString = ""
        MarkListString = String.Format("{0},{1},{2},{3},{4},{5}", RecipeMarkList.Item(0).MarkX, RecipeMarkList.Item(0).MarkY, RecipeMarkList.Item(0).PositionCenterX, RecipeMarkList.Item(0).PositionCenterY, RecipeMarkList.Item(0).PositionX, RecipeMarkList.Item(0).PositionY)
        For nIndex = 1 To RecipeMarkCount - 1 '我是nIndex
            MarkListString = String.Format("{0},{1},{2},{3},{4},{5},{6}", MarkListString, RecipeMarkList.Item(nIndex).MarkX, RecipeMarkList.Item(nIndex).MarkY, RecipeMarkList.Item(nIndex).PositionCenterX, RecipeMarkList.Item(nIndex).PositionCenterY, RecipeMarkList.Item(nIndex).PositionX, RecipeMarkList.Item(nIndex).PositionY)
        Next

        moSetting.SimpleSet(sName, Me)
    End Sub
End Class