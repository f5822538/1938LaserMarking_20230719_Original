Imports iTVisionService.DisplayLib

Partial Class CMyEquipment

    Public WaferMap As New CMyMap
    Public SelectedDefect As iTVisionService.DisplayLib.CMyDefect
    Public FirstDieCoordinate As Point = Point.Empty
    Public LastDieCoordinate As Point = Point.Empty

    Public Sub WaferMapCreate()
        If WaferMap Is Nothing Then Exit Sub
        Dim DefectROI As Rectangle = Rectangle.Empty

        With moMainRecipe.RecipeCamera.RecipeModelDiff
            Call WaferMap.CreateMap(.MarkXCount, .MarkYCount, New Size(1600 \ .MarkXCount, 500 \ .MarkYCount), moMainRecipe.RecipeCamera.RecipeWaferMap.MapIndexBase, 2, DefectROI) '20201214 更新UI放大倍率(配合DieSize):Map(2)倍、Review(1)倍
        End With

        WaitHandle.WaitAll({WaferMap.IsCreate.WaitHandle}, 30000)
        Call WaferMap.UpdateDisplay()
    End Sub

    Public Sub UpdateDefectROI()
        If WaferMap Is Nothing OrElse WaferMap.IsCreate.IsSet() = False Then Exit Sub
        With moMainRecipe.RecipeCamera.RecipeModelDiff
            For Each oRecipeMark As CRecipeMark In .RecipeMarkList.RecipeMarkList
                If oRecipeMark.MarkX >= WaferMap.DieColumnCount OrElse oRecipeMark.MarkY >= WaferMap.DieRowCount Then Continue For
                WaferMap.DieList(oRecipeMark.MarkX, oRecipeMark.MarkY).UpdateDefectROI(New Rectangle(CInt(oRecipeMark.PositionX), CInt(oRecipeMark.PositionY), .ModelSize.Width, .ModelSize.Height))
            Next
        End With
    End Sub

    Public Sub WaferMapReset()
        If WaferMap Is Nothing OrElse WaferMap.IsCreate.IsSet() = False Then Exit Sub
        Call WaferMap.ResetMap()
        With moMainRecipe.RecipeCamera.RecipeModelDiff
            Dim nCenterX As Integer = .MarkXCount \ 2
            Dim nCenterY As Integer = .MarkYCount \ 2
            'Dim nRadius As Integer = .MarkXCount \ 2
            'Dim nRadiusPow2 As Integer = nRadius * nRadius

            For nPositionY As Integer = 0 To .MarkYCount - 1
                For nPositionX As Integer = 0 To .MarkXCount - 1
                    If nPositionX >= WaferMap.DieColumnCount OrElse nPositionY >= WaferMap.DieRowCount Then Continue For
                    Call WaferMap.DieList(nPositionX, nPositionY).ResetBin(BinType.EffectiveDie)
                Next
            Next

            Call WaferMap.UpdateView()
        End With
    End Sub

    Public Sub WaferMapSetColor()
        If WaferMap Is Nothing OrElse WaferMap.IsCreate.IsSet() = False Then Exit Sub
        With moMainRecipe.RecipeCamera.RecipeWaferMap
            Call WaferMap.SetBinColor(iTVisionService.DisplayLib.BinType.InvalidDie, .ColorInvalidDie)
            Call WaferMap.SetBinColor(iTVisionService.DisplayLib.BinType.EffectiveDie, .ColorEffectiveDie)
            Call WaferMap.SetBinColor(iTVisionService.DisplayLib.BinType.ProcessingDie, .ColorProcessingDie)
            Call WaferMap.SetBinColor(iTVisionService.DisplayLib.BinType.OKDie, .ColorOKDie)
            Call WaferMap.SetBinColor(iTVisionService.DisplayLib.BinType.NGDie, .ColorNGDie)
            Call WaferMap.SetMapIndexColorAndFont(.ColorMapIndex, .FontMapIndex)
        End With
    End Sub
End Class