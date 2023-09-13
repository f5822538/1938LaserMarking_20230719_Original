Public Class CMyPatternMatching

    Private moModelID As MIL_ID = 0
    Private moResult As MIL_ID = 0

    Public Sub New()
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

    Public Function AddModel(nImageHeader As MIL_ID, oROI As Rectangle, nScore As Integer) As Boolean
        If moModelID <> 0 Then
            MIL.MpatFree(moModelID)
            moModelID = MIL.M_NULL
        End If

        Try
            MIL.MpatAllocModel(MIL.M_DEFAULT_HOST, nImageHeader, oROI.X, oROI.Y, oROI.Width, oROI.Height, MIL.M_NORMALIZED, moModelID)
            MIL.MpatSetAcceptance(moModelID, nScore)
            MIL.MpatPreprocModel(nImageHeader, moModelID, MIL.M_DEFAULT)
        Catch ex As System.Exception
            Call MsgBox(ex.ToString, MsgBoxStyle.OkOnly, "錯誤")
        End Try

        Return moModelID <> 0
    End Function

    ''' <summary>
    ''' 特徵比對算法
    ''' </summary>
    ''' <param name="nImageHeader"></param>
    ''' <param name="oROI"></param>
    ''' <param name="nX"></param>
    ''' <param name="nY"></param>
    ''' <param name="nScore"></param>
    ''' <param name="nCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindModel(nImageHeader As MIL_ID, oROI As Rectangle, ByRef nX As Double, ByRef nY As Double, ByRef nScore As Double, nCenter As Point) As Boolean
        nX = 0 : nY = 0
        If moModelID = 0 Then Return False
        Dim nROI As MIL_ID
        MIL.MbufChild2d(nImageHeader, oROI.X, oROI.Y, oROI.Width, oROI.Height, nROI)
        MIL.MpatFindModel(nROI, moModelID, moResult)
        MIL.MbufFree(nROI)
        nROI = MIL.M_NULL
        If MIL.MpatGetNumber(moResult) > 0 Then
            MIL.MpatGetResult(moResult, MIL.M_POSITION_X, nX)
            MIL.MpatGetResult(moResult, MIL.M_POSITION_Y, nY)
            MIL.MpatGetResult(moResult, MIL.M_SCORE, nScore)
            nX = nX + oROI.X
            nY = nY + oROI.Y
            Return True
        Else
            nX = nCenter.X
            nY = nCenter.Y
            nScore = 0
            Return False
        End If
    End Function
End Class