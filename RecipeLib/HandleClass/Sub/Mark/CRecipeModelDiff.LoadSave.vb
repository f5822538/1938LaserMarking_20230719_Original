Partial Class CRecipeModelDiff : Implements II_LoadSave

    Public Sub LoadConfig(sName As String) Implements II_LoadSave.LoadConfig
        moAtts.SetReadOnly(Me, FindName(Function() IsGatherStandardDeviation), False)
        moSetting.SimpleGet(msParentName & sName, Me)
        moAtts.SetReadOnly(Me, FindName(Function() IsGatherStandardDeviation), True)

        Call BuildModelCenterDraw()
    End Sub

    Public Sub SaveConfig(sName As String) Implements II_LoadSave.SaveConfig
        moSetting.SimpleSet(msParentName & sName, Me)
    End Sub

    Public Sub LoadSummationSquareCount()
        SummationSquareCount = moSetting.GetKeyValue(msParentName & "RecipeModelDiff", "SummationSquareCount", 0)
    End Sub

    Public Sub SaveSummationSquareCount()
        moSetting.SetKeyValue(msParentName & "RecipeModelDiff", "SummationSquareCount", SummationSquareCount)
    End Sub

    Public Sub SaveIsGatherStandardDeviation()
        moSetting.SetKeyValue(msParentName & "RecipeModelDiff", "IsGatherStandardDeviation", IsGatherStandardDeviation)
    End Sub

    Public Sub SaveMeanGray()
        moSetting.SetKeyValue(msParentName & "RecipeModelDiff", "MeanGray", MeanGray)
    End Sub

    Public Sub BuildModelCenterDraw()
        ModelCenterDrawListStart.Clear()
        ModelCenterDrawListEnd.Clear()

        For nIndex As Integer = 0 To RecipeMarkList.RecipeMarkList.Count - 1
            If RecipeMarkList.RecipeMarkList.Item(nIndex).IsEmpty = True Then Continue For
            Dim pStart As Point = New Point(CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterX - 20), CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterY))
            Dim pEnd As Point = New Point(CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterX + 20), CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterY))
            ModelCenterDrawListStart.Add(pStart)
            ModelCenterDrawListEnd.Add(pEnd)
            pStart = New Point(CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterX), CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterY - 20))
            pEnd = New Point(CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterX), CInt(RecipeMarkList.RecipeMarkList.Item(nIndex).PositionCenterY + 20))
            ModelCenterDrawListStart.Add(pStart)
            ModelCenterDrawListEnd.Add(pEnd)
        Next
    End Sub
End Class