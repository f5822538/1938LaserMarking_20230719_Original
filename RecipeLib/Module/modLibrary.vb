Public Module modLibrary
    Public Function SaveStandardDeviationModel(oRecipe As CRecipeModelDiff, sPath As String, sRecipeName As String) As Boolean
        Try
            oRecipe.SaveSummationSquareCount()
            MIL.MbufExport(String.Format("{0}\{1}Summation.mim", sPath, sRecipeName), MIL.M_MIL, oRecipe.SummationID)
            MIL.MbufExport(String.Format("{0}\{1}SummationSquare.mim", sPath, sRecipeName), MIL.M_MIL, oRecipe.SummationSquareID)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadStandardDeviationModel(oRecipe As CRecipeModelDiff, sPath As String, sRecipeName As String) As Boolean
        Try
            With oRecipe
                If .ModelSize.Width = .ModelRectangle.Width AndAlso .ModelSize.Height = .ModelRectangle.Height Then
                    oRecipe.LoadSummationSquareCount()

                    Dim oTemporaryImage As MIL_ID
                    MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 32 + MIL.M_FLOAT, MIL.M_IMAGE + MIL.M_PROC, oTemporaryImage)

                    MIL.MbufImport(String.Format("{0}\{1}Summation.mim", sPath, sRecipeName), MIL.M_MIL, MIL.M_RESTORE, MIL.M_DEFAULT_HOST, oTemporaryImage)
                    MIL.MimArith(.SummationID, 0, .SummationID, MIL.M_MULT_CONST)
                    MIL.MimArith(.SummationID, oTemporaryImage, .SummationID, MIL.M_ADD)

                    MIL.MbufImport(String.Format("{0}\{1}SummationSquare.mim", sPath, sRecipeName), MIL.M_MIL, MIL.M_RESTORE, MIL.M_DEFAULT_HOST, oTemporaryImage)
                    MIL.MimArith(.SummationSquareID, 0, .SummationSquareID, MIL.M_MULT_CONST)
                    MIL.MimArith(.SummationSquareID, oTemporaryImage, .SummationSquareID, MIL.M_ADD)

                    MIL.MbufFree(oTemporaryImage)
                    oTemporaryImage = MIL.M_NULL
                End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ClearStandardDeviationModel(oRecipe As CRecipeModelDiff) As Boolean
        Try
            With oRecipe
                If .ModelSize.Width = .ModelRectangle.Width AndAlso .ModelSize.Height = .ModelRectangle.Height Then
                    oRecipe.SummationSquareCount = 0

                    MIL.MimArith(.SummationID, 0, .SummationID, MIL.M_MULT_CONST)
                    MIL.MimArith(.SummationSquareID, 0, .SummationSquareID, MIL.M_MULT_CONST)
                End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Module