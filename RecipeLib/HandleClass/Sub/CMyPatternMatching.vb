Public Class CMyPatternMatching

    Private moModelID As MIL_ID = 0
    Private moResult As MIL_ID = 0
    Private moPatternMatchingType As PatternMatchingType = PatternMatchingType.PatternMatching1St

    Public Sub New(oPatternMatchingType As PatternMatchingType)
        moPatternMatchingType = oPatternMatchingType
        MIL.MpatAllocResult(MIL.M_DEFAULT_HOST, MIL.M_DEFAULT, moResult)
    End Sub

    Public Sub Close()
        If moModelID <> 0 Then
            MIL.MpatFree(moModelID)
            moModelID = MIL.M_NULL
        End If
        MIL.MpatFree(moResult)
        moResult = MIL.M_NULL
    End Sub

    Public Function AddModel(nImageHeader As MIL_ID, oROI As Rectangle) As Boolean
        If moModelID <> 0 Then
            MIL.MpatFree(moModelID)
            moModelID = MIL.M_NULL
        End If

        Try
            MIL.MpatAllocModel(MIL.M_DEFAULT_HOST, nImageHeader, oROI.X, oROI.Y, oROI.Width, oROI.Height, MIL.M_NORMALIZED, moModelID)
            MIL.MpatPreprocModel(nImageHeader, moModelID, MIL.M_DEFAULT)
        Catch ex As System.Exception
            Call MsgBox(ex.ToString, MsgBoxStyle.OkOnly, "¿ù»~")
        End Try

        Return moModelID <> 0
    End Function

    ''' <summary>
    ''' modLibrary.FindModelAll -> CMyPatternMatching.FindModelAll
    ''' </summary>
    ''' <param name="nImageHeader"></param>
    ''' <param name="oRecipe"></param>
    ''' <param name="nX"></param>
    ''' <param name="nY"></param>
    ''' <param name="nAngle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindModelAll(nImageHeader As MIL_ID, oRecipe As CRecipeModelDiff, ByRef nX As List(Of Double), ByRef nY As List(Of Double), ByRef nAngle As List(Of Double)) As Boolean
        Try
            nX.Clear()
            nY.Clear()
            nAngle.Clear()
            If moModelID = 0 Then Return False
            Dim nROI As MIL_ID
            MIL.MpatSetNumber(moModelID, MIL.M_ALL)
            MIL.MpatSetAngle(moModelID, MIL.M_SEARCH_ANGLE_MODE, MIL.M_ENABLE)
            MIL.MpatSetAngle(moModelID, MIL.M_SEARCH_ANGLE_DELTA_NEG, oRecipe.ModelAngle)
            MIL.MpatSetAngle(moModelID, MIL.M_SEARCH_ANGLE_DELTA_POS, oRecipe.ModelAngle)
            MIL.MpatSetAngle(moModelID, MIL.M_SEARCH_ANGLE_INTERPOLATION_MODE, MIL.M_BILINEAR)
            MIL.MbufChild2d(nImageHeader, oRecipe.SearchRange.X, oRecipe.SearchRange.Y, oRecipe.SearchRange.Width, oRecipe.SearchRange.Height, nROI)
            Select Case moPatternMatchingType
                Case PatternMatchingType.PatternMatching1St
                    MIL.MpatSetAcceptance(moModelID, oRecipe.ModelScore1St)
                Case PatternMatchingType.PatternMatching2Nd
                    MIL.MpatSetAcceptance(moModelID, oRecipe.ModelScore2Nd)
            End Select
            MIL.MpatFindModel(nROI, moModelID, moResult)
            MIL.MbufFree(nROI)
            nROI = MIL.M_NULL
            Dim nResultCount As Integer = CInt(MIL.MpatGetNumber(moResult))
            Dim nResultX(nResultCount - 1) As Double
            Dim nResultY(nResultCount - 1) As Double
            Dim nResultAngle(nResultCount - 1) As Double
            If nResultCount > 0 Then
                MIL.MpatGetResult(moResult, MIL.M_POSITION_X, nResultX)
                MIL.MpatGetResult(moResult, MIL.M_POSITION_Y, nResultY)
                MIL.MpatGetResult(moResult, MIL.M_ANGLE, nResultAngle)

                nX = nResultX.ToList()
                nY = nResultY.ToList()
                nAngle = nResultAngle.ToList()
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class