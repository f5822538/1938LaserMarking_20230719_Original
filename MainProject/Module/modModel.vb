Module modModel

    Public Sub UpdateModelList(oRecipeModelDiff As CRecipeModelDiff, nImageHeader As MIL_ID, Optional RecipeID As String = "", Optional IsClear As Boolean = True)
        Call UpdateModel(oRecipeModelDiff, nImageHeader, IsClear)
        If RecipeID <> "" Then
            Call LoadStandardDeviationModel(oRecipeModelDiff, Application.StartupPath & "\Recipe", RecipeID)
        End If
    End Sub

    Public Sub UpdateModel(oRecipeModelDiff As CRecipeModelDiff, nImageHeader As MIL_ID, Optional IsClear As Boolean = True)
        With oRecipeModelDiff
            If .TemplateID1St <> 0 Then
                MIL.MbufFree(.TemplateID1St)
                .TemplateID1St = MIL.M_NULL
            End If

            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, .ModelSize.Width, .ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, .TemplateID1St)
            Dim oChild As MIL_ID
            MIL.MbufChild2d(nImageHeader, .ModelTopLeft.X, .ModelTopLeft.Y, .ModelSize.Width, .ModelSize.Height, oChild)
            MIL.MbufCopy(oChild, .TemplateID1St)
            MIL.MbufFree(oChild)
            oChild = MIL.M_NULL

            Dim oResult As MIL_ID
            MIL.MimAllocResult(MIL.M_DEFAULT_HOST, 1, MIL.M_STAT_LIST, oResult)
            MIL.MimStat(oRecipeModelDiff.TemplateID1St, oResult, MIL.M_MEAN, MIL.M_NULL, MIL.M_NULL, MIL.M_NULL)
            MIL.MimGetResult(oResult, MIL.M_MEAN + MIL.M_TYPE_LONG, oRecipeModelDiff.MeanGray)
            MIL.MimFree(oResult)
            oResult = MIL.M_NULL
            oRecipeModelDiff.MeanGray = oRecipeModelDiff.MeanGray + CInt(oRecipeModelDiff.MeanGray * 0.5)
            Call oRecipeModelDiff.SaveMeanGray()

            If .ModelTopLeft.X <> .ModelRectangle.X OrElse .ModelTopLeft.Y <> .ModelRectangle.Y OrElse .ModelSize.Width <> .ModelRectangle.Width OrElse .ModelSize.Height <> .ModelRectangle.Height Then
                If IsClear = True Then
                    .SummationSquareCount = 0

                    If .SummationID <> 0 Then
                        MIL.MbufFree(.SummationID)
                        .SummationID = MIL.M_NULL
                    End If

                    MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, .ModelSize.Width, .ModelSize.Height, 32 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, .SummationID)
                    MIL.MimArith(.SummationID, 0, .SummationID, MIL.M_MULT_CONST)

                    If .SummationSquareID <> 0 Then
                        MIL.MbufFree(.SummationSquareID)
                        .SummationSquareID = MIL.M_NULL
                    End If

                    MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, .ModelSize.Width, .ModelSize.Height, 32 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, .SummationSquareID)
                    MIL.MimArith(.SummationSquareID, 0, .SummationSquareID, MIL.M_MULT_CONST)
                End If

                .ModelRectangle.X = .ModelTopLeft.X
                .ModelRectangle.Y = .ModelTopLeft.Y
                .ModelRectangle.Width = .ModelSize.Width
                .ModelRectangle.Height = .ModelSize.Height
            End If
            AddModel(oRecipeModelDiff, .TemplateID1St)
        End With
    End Sub

    Public Sub DeleteModel(oRecipeModelDiff As CRecipeModelDiff)
        With oRecipeModelDiff
            If .TemplateID1St <> 0 Then MIL.MbufFree(.TemplateID1St)
            .TemplateID1St = MIL.M_NULL
        End With
    End Sub
End Module