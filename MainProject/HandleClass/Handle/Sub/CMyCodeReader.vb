Public Structure CodeResult
    Public Score As Double
    Public Code As String
    Public CodeZone As Rectangle

    Public Sub Clear()
        Score = 0.0
        Code = ""
        CodeZone = Rectangle.Empty
    End Sub
End Structure

Public Class CMyCodeReader

    Private moMyEquipment As CMyEquipment
    Private moResult As CodeResult

    Private moCodeReaderID As MIL_ID
    Private moCodeResultID As MIL_ID

    Public ReadOnly Property Result As CodeResult
        Get
            Return moResult
        End Get
    End Property

    Public Sub New(oMyEquipment As CMyEquipment)
        moMyEquipment = oMyEquipment
        Call MIL.McodeAlloc(MIL.M_DEFAULT_HOST, MIL.M_DEFAULT, MIL.M_DEFAULT, moCodeReaderID)
        Call MIL.McodeModel(moCodeReaderID, MIL.M_ADD, MIL.M_DATAMATRIX, MIL.M_NULL, MIL.M_DEFAULT, CType(MIL.M_NULL, IntPtr))
        Call MIL.McodeAllocResult(MIL.M_DEFAULT_HOST, MIL.M_DEFAULT, moCodeResultID)
    End Sub

    Public Function SetParameter(oRecipeCodeReader As CRecipeCodeReader) As Boolean
        If moCodeReaderID = 0 OrElse moCodeResultID = 0 Then Return False

        Try

            Call MIL.McodeControl(moCodeReaderID, MIL.M_NUMBER, 2)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_SEARCH_ANGLE_DELTA_POS, 3)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_SEARCH_ANGLE_DELTA_NEG, 3)

            'Call MIL.McodeControl(moCodeReaderID, MIL.M_NUMBER, 1)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_FOREGROUND_VALUE, oRecipeCodeReader.Foreground)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_THRESHOLD_MODE, MIL.M_ADAPTIVE)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_CELL_SIZE_MIN, oRecipeCodeReader.CellSizeMin)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_CELL_SIZE_MAX, oRecipeCodeReader.CellSizeMax)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_DISTORTION, MIL.M_UNEVEN_GRID_STEP)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_USE_PRESEARCH, MIL.M_FINDER_PATTERN_BASE)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_DOT_SPACING, 1)
            'Call MIL.McodeControl(moCodeReaderID, MIL.M_DATAMATRIX_SHAPE, MIL.M_SQUARE)
            '' 0924 SHAPE改為M_ANY 包含 RECTANGLE 、 SQUARE (主要是因為AOI相機可能會用到長條狀的DATAMATRIX)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_DATAMATRIX_SHAPE, MIL.M_ANY)

            Call MIL.McodeControl(moCodeReaderID, MIL.M_FINDER_PATTERN_EXHAUSTIVE_SEARCH, MIL.M_ENABLE)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_CELL_NUMBER_X_MIN, oRecipeCodeReader.CellNumberMin)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_CELL_NUMBER_Y_MIN, oRecipeCodeReader.CellNumberMin)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_CELL_NUMBER_X_MAX, oRecipeCodeReader.CellNumberMax)
            Call MIL.McodeControl(moCodeReaderID, MIL.M_CELL_NUMBER_Y_MAX, oRecipeCodeReader.CellNumberMax)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function Find(oImageID As MIL_ID, oRecipeCodeReader As CRecipeCodeReader) As Boolean
        If moCodeReaderID = 0 OrElse moCodeResultID = 0 OrElse oImageID = 0 Then Return False

        Try
            Dim oROI As MIL_ID
            Dim nResultCount As MIL_INT = 0

            Call moResult.Clear()
            Call MIL.MbufChild2d(oImageID, oRecipeCodeReader.SearchRange.X, oRecipeCodeReader.SearchRange.Y, oRecipeCodeReader.SearchRange.Width, oRecipeCodeReader.SearchRange.Height, oROI)
            Call MIL.McodeRead(moCodeReaderID, oROI, moCodeResultID)
            Call MIL.McodeGetResult(moCodeResultID, MIL.M_NUMBER + MIL.M_TYPE_MIL_INT, nResultCount)

            With moResult
                If nResultCount > 0 Then
                    Dim sStringBuilder As New StringBuilder(1024)
                    Dim nLeft As Double = 0.0
                    Dim nTop As Double = 0.0
                    Dim nRight As Double = 0.0
                    Dim nBottom As Double = 0.0

                    MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_SCORE, .Score)
                    MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_STRING, sStringBuilder)
                    MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_TOP_LEFT_X, nLeft)
                    MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_TOP_LEFT_Y, nTop)
                    MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_BOTTOM_RIGHT_X, nRight)
                    MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_BOTTOM_RIGHT_Y, nBottom)
                    .Code = sStringBuilder.ToString()
                    .CodeZone = New Rectangle(CInt(Math.Min(nLeft, nRight)) - 2, CInt(Math.Min(nTop, nBottom)) - 2, CInt(Math.Abs(nRight - nLeft)) + 4, CInt(Math.Abs(nBottom - nTop)) + 4)
                    .CodeZone.Offset(oRecipeCodeReader.SearchRange.Location)
                Else
                    Call MIL.McodeControl(moCodeReaderID, MIL.M_DOT_SPACING, 0)
                    Call MIL.McodeRead(moCodeReaderID, oROI, moCodeResultID)
                    Call MIL.McodeGetResult(moCodeResultID, MIL.M_NUMBER + MIL.M_TYPE_MIL_INT, nResultCount)

                    If nResultCount > 0 Then
                        Dim sStringBuilder As New StringBuilder(1024)
                        Dim nLeft As Double = 0.0
                        Dim nTop As Double = 0.0
                        Dim nRight As Double = 0.0
                        Dim nBottom As Double = 0.0

                        MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_SCORE, .Score)
                        MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_STRING, sStringBuilder)
                        MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_TOP_LEFT_X, nLeft)
                        MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_TOP_LEFT_Y, nTop)
                        MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_BOTTOM_RIGHT_X, nRight)
                        MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_BOTTOM_RIGHT_Y, nBottom)
                        .Code = sStringBuilder.ToString()
                        .CodeZone = New Rectangle(CInt(Math.Min(nLeft, nRight)) - 2, CInt(Math.Min(nTop, nBottom)) - 2, CInt(Math.Abs(nRight - nLeft)) + 4, CInt(Math.Abs(nBottom - nTop)) + 4)
                        .CodeZone.Offset(oRecipeCodeReader.SearchRange.Location)
                    Else
                        Call MIL.McodeControl(moCodeReaderID, MIL.M_DOT_SPACING, 2)
                        Call MIL.McodeRead(moCodeReaderID, oROI, moCodeResultID)
                        Call MIL.McodeGetResult(moCodeResultID, MIL.M_NUMBER + MIL.M_TYPE_MIL_INT, nResultCount)

                        If nResultCount > 0 Then
                            Dim sStringBuilder As New StringBuilder(1024)
                            Dim nLeft As Double = 0.0
                            Dim nTop As Double = 0.0
                            Dim nRight As Double = 0.0
                            Dim nBottom As Double = 0.0

                            MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_SCORE, .Score)
                            MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_STRING, sStringBuilder)
                            MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_TOP_LEFT_X, nLeft)
                            MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_TOP_LEFT_Y, nTop)
                            MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_BOTTOM_RIGHT_X, nRight)
                            MIL.McodeGetResultSingle(moCodeResultID, 0, MIL.M_BOTTOM_RIGHT_Y, nBottom)
                            .Code = sStringBuilder.ToString()
                            .CodeZone = New Rectangle(CInt(Math.Min(nLeft, nRight)) - 2, CInt(Math.Min(nTop, nBottom)) - 2, CInt(Math.Abs(nRight - nLeft)) + 4, CInt(Math.Abs(nBottom - nTop)) + 4)
                            .CodeZone.Offset(oRecipeCodeReader.SearchRange.Location)
                        End If
                    End If
                End If

                Call MIL.MbufFree(oROI)
                oROI = MIL.M_NULL
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function FindMore(oImageID As MIL_ID, oRecipeCodeReader As CRecipeCodeReader) As Boolean
        If moCodeReaderID = 0 OrElse moCodeResultID = 0 OrElse oImageID = 0 Then Return False

        Try
            Dim oROI As MIL_ID
            Dim nResultCount As MIL_INT = 0

            Call moResult.Clear()
            Call MIL.MbufChild2d(oImageID, oRecipeCodeReader.SearchRange.X, oRecipeCodeReader.SearchRange.Y, oRecipeCodeReader.SearchRange.Width, oRecipeCodeReader.SearchRange.Height, oROI)
            Call MIL.McodeRead(moCodeReaderID, oROI, moCodeResultID)
            Call MIL.McodeGetResult(moCodeResultID, MIL.M_NUMBER + MIL.M_TYPE_MIL_INT, nResultCount)

            With moResult
                If nResultCount > 0 Then
                    For i = 0 To nResultCount - 1
                        Dim sStringBuilder As New StringBuilder(1024)
                        Dim nLeft As Double = 0.0
                        Dim nTop As Double = 0.0
                        Dim nRight As Double = 0.0
                        Dim nBottom As Double = 0.0

                        MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_SCORE, .Score)
                        MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_STRING, sStringBuilder)
                        MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_TOP_LEFT_X, nLeft)
                        MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_TOP_LEFT_Y, nTop)
                        MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_BOTTOM_RIGHT_X, nRight)
                        MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_BOTTOM_RIGHT_Y, nBottom)
                        .Code = sStringBuilder.ToString()
                        .CodeZone = New Rectangle(CInt(Math.Min(nLeft, nRight)) - 2, CInt(Math.Min(nTop, nBottom)) - 2, CInt(Math.Abs(nRight - nLeft)) + 4, CInt(Math.Abs(nBottom - nTop)) + 4)
                        .CodeZone.Offset(oRecipeCodeReader.SearchRange.Location)
                        If sStringBuilder.ToString() <> "" Then
                            Exit For
                        End If
                    Next

                Else
                    Call MIL.McodeControl(moCodeReaderID, MIL.M_DOT_SPACING, 0)
                    Call MIL.McodeRead(moCodeReaderID, oROI, moCodeResultID)
                    Call MIL.McodeGetResult(moCodeResultID, MIL.M_NUMBER + MIL.M_TYPE_MIL_INT, nResultCount)

                    If nResultCount > 0 Then
                        For i = 0 To nResultCount - 1
                            Dim sStringBuilder As New StringBuilder(1024)
                            Dim nLeft As Double = 0.0
                            Dim nTop As Double = 0.0
                            Dim nRight As Double = 0.0
                            Dim nBottom As Double = 0.0

                            MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_SCORE, .Score)
                            MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_STRING, sStringBuilder)
                            MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_TOP_LEFT_X, nLeft)
                            MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_TOP_LEFT_Y, nTop)
                            MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_BOTTOM_RIGHT_X, nRight)
                            MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_BOTTOM_RIGHT_Y, nBottom)
                            .Code = sStringBuilder.ToString()
                            .CodeZone = New Rectangle(CInt(Math.Min(nLeft, nRight)) - 2, CInt(Math.Min(nTop, nBottom)) - 2, CInt(Math.Abs(nRight - nLeft)) + 4, CInt(Math.Abs(nBottom - nTop)) + 4)
                            .CodeZone.Offset(oRecipeCodeReader.SearchRange.Location)
                            If sStringBuilder.ToString() <> "" Then
                                Exit For
                            End If
                        Next

                    Else
                        Call MIL.McodeControl(moCodeReaderID, MIL.M_DOT_SPACING, 2)
                        Call MIL.McodeRead(moCodeReaderID, oROI, moCodeResultID)
                        Call MIL.McodeGetResult(moCodeResultID, MIL.M_NUMBER + MIL.M_TYPE_MIL_INT, nResultCount)

                        If nResultCount > 0 Then
                            For i = 0 To nResultCount - 1
                                Dim sStringBuilder As New StringBuilder(1024)
                                Dim nLeft As Double = 0.0
                                Dim nTop As Double = 0.0
                                Dim nRight As Double = 0.0
                                Dim nBottom As Double = 0.0

                                MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_SCORE, .Score)
                                MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_STRING, sStringBuilder)
                                MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_TOP_LEFT_X, nLeft)
                                MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_TOP_LEFT_Y, nTop)
                                MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_BOTTOM_RIGHT_X, nRight)
                                MIL.McodeGetResultSingle(moCodeResultID, i, MIL.M_BOTTOM_RIGHT_Y, nBottom)
                                .Code = sStringBuilder.ToString()
                                .CodeZone = New Rectangle(CInt(Math.Min(nLeft, nRight)) - 2, CInt(Math.Min(nTop, nBottom)) - 2, CInt(Math.Abs(nRight - nLeft)) + 4, CInt(Math.Abs(nBottom - nTop)) + 4)
                                .CodeZone.Offset(oRecipeCodeReader.SearchRange.Location)
                                If sStringBuilder.ToString() <> "" Then
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                Call MIL.MbufFree(oROI)
                oROI = MIL.M_NULL
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Sub Close()
        If moCodeReaderID <> 0 Then MIL.McodeFree(moCodeReaderID)
        If moCodeResultID <> 0 Then MIL.McodeFree(moCodeResultID)
        moCodeReaderID = MIL.M_NULL
        moCodeResultID = MIL.M_NULL
    End Sub
End Class