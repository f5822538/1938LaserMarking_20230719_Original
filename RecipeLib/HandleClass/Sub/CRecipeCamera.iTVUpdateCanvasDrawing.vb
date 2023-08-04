Partial Class CRecipeCamera : Implements II_iTVUpdateCanvasDrawing

    Public ModelList As New List(Of Rectangle)
    Public DefectList As New List(Of CMyDefect)
    Public ModelCenterListStart As New List(Of Point)
    Public ModelCenterListEnd As New List(Of Point)

    Public Sub DrawImage(sHandle As String, oCanvas As iTVisionService.II_iTVCanvas) Implements iTVisionService.II_iTVUpdateCanvasDrawing.DrawImage
        Try
            Select Case True
                Case sHandle = oCanvas.DrawDefineRecipeCase0
                    Call DrawImageCase1(oCanvas)
                    Call DrawImageCase2(oCanvas)
                Case sHandle = oCanvas.DrawDefineRecipeCase1
                    Call DrawImageCase1(oCanvas)
                Case sHandle = oCanvas.DrawDefineRecipeCase2
                    Call DrawImageCase2(oCanvas)
                Case sHandle = oCanvas.DrawDefineRecipeCase3
                    Call DrawImageCase3(oCanvas)
                Case sHandle = oCanvas.DrawDefineRecipeCase4
                    Call DrawImageCase4(oCanvas)
                Case sHandle = oCanvas.DrawDefineRecipeCase5
                    Call DrawImageCase5(oCanvas)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DrawImageCase1(oCanvas As iTVisionService.II_iTVCanvas)
        Call oCanvas.DrawSelectColorRectangle(Color.DarkCyan, Locate1.PatternZone)
        Call oCanvas.DrawSelectColorRectangle(Color.DarkCyan, Locate2.PatternZone)
        Call oCanvas.DrawSelectColorRectangle(Color.DodgerBlue, Locate1.FindModelZone)
        Call oCanvas.DrawSelectColorRectangle(Color.DodgerBlue, Locate2.FindModelZone)
        Dim nSP As PointF = New PointF(CSng(RefXOfAlignMark1 - 50), CSng(RefYOfAlignMark1))
        Dim nEP As PointF = New PointF(CSng(RefXOfAlignMark1 + 50), CSng(RefYOfAlignMark1))
        Call oCanvas.DrawLine(Pens.Pink, Point.Round(nSP), Point.Round(nEP))
        nSP = New PointF(CSng(RefXOfAlignMark1), CSng(RefYOfAlignMark1 - 50))
        nEP = New PointF(CSng(RefXOfAlignMark1), CSng(RefYOfAlignMark1 + 50))
        Call oCanvas.DrawLine(Pens.Pink, Point.Round(nSP), Point.Round(nEP))
        nSP = New PointF(CSng(RefXOfAlignMark2 - 50), CSng(RefYOfAlignMark2))
        nEP = New PointF(CSng(RefXOfAlignMark2 + 50), CSng(RefYOfAlignMark2))
        Call oCanvas.DrawLine(Pens.SeaShell, Point.Round(nSP), Point.Round(nEP))
        nSP = New PointF(CSng(RefXOfAlignMark2), CSng(RefYOfAlignMark2 - 50))
        nEP = New PointF(CSng(RefXOfAlignMark2), CSng(RefYOfAlignMark2 + 50))
        Call oCanvas.DrawLine(Pens.SeaShell, Point.Round(nSP), Point.Round(nEP))
    End Sub

    Private Sub DrawImageCase2(oCanvas As iTVisionService.II_iTVCanvas)
        Call oCanvas.DrawSelectColorRectangle(Color.Lavender, CodeReader.SearchRange)
    End Sub

    Private Sub DrawImageCase3(oCanvas As iTVisionService.II_iTVCanvas)
        Using oPenModel As Pen = New Pen(Color.Green, 1), oPenSearch As Pen = New Pen(Color.DarkOliveGreen, 1)
            Call oCanvas.DrawRectangle(oPenModel, New Rectangle(RecipeModelDiff.ModelTopLeft.X, RecipeModelDiff.ModelTopLeft.Y, RecipeModelDiff.ModelSize.Width, RecipeModelDiff.ModelSize.Height))
            Call oCanvas.DrawRectangle(oPenSearch, RecipeModelDiff.SearchRange)
        End Using
    End Sub

    Private Sub DrawImageCase4(oCanvas As iTVisionService.II_iTVCanvas)
        Using oPenOffsetRecipeModel As New Pen(Color.Pink, 1)
            Call oCanvas.DrawLineList(oPenOffsetRecipeModel, RecipeModelDiff.ModelCenterDrawListStart.ToArray, RecipeModelDiff.ModelCenterDrawListEnd.ToArray)
        End Using
    End Sub

    Private Sub DrawImageCase5(oCanvas As iTVisionService.II_iTVCanvas)
        Call oCanvas.DrawSelectColorRectangle(Color.Lavender, CodeReaderForInspect.SearchRange)
    End Sub

    Public Sub DrawDefect(sHandle As String, oCanvas As iTVisionService.II_iTVCanvas) Implements iTVisionService.II_iTVUpdateCanvasDrawing.DrawDefect
        Try
            Select Case True
                Case sHandle = oCanvas.DrawDefectCase0
                    Call DrawDefectCase0(oCanvas)
                Case sHandle = oCanvas.DrawDefectCase1
                    Call DrawDefectCase1(oCanvas)
                Case sHandle = oCanvas.DrawDefectCase2
                    Call DrawDefectCase2(oCanvas)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DrawDefectCase0(oCanvas As iTVisionService.II_iTVCanvas)
        Dim Bright2Way As New List(Of Rectangle)
        Dim Dark2Way As New List(Of Rectangle)
        Dim Bright4Way As New List(Of Rectangle)
        Dim Dark4Way As New List(Of Rectangle)
        Dim DirectModel As New List(Of Rectangle)
        Dim BrightModel As New List(Of Rectangle)
        Dim DarkModel As New List(Of Rectangle)
        Dim OffsetModel As New List(Of Rectangle)
        Dim NormalDefect As New List(Of Rectangle)

        For i As Integer = 0 To DefectList.Count - 1
            With DefectList(i)
                Select Case True
                    Case .InpsectMethod = Comp_Inspect_Method.Comp_Define1
                        Call DirectModel.Add(.DefectBoundary.GetRatioRectangle(1))
                    Case .InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        If .DefectType = Comp_InsperrorType.Comp_Bright Then
                            Call BrightModel.Add(.DefectBoundary.GetRatioRectangle(1))
                        ElseIf .DefectType = Comp_InsperrorType.Comp_Dark Then
                            Call DarkModel.Add(.DefectBoundary.GetRatioRectangle(1))
                        ElseIf .DefectType = Comp_InsperrorType.Comp_Corner Then
                            Call OffsetModel.Add(.DefectBoundary.GetRatioRectangle(1))
                        End If
                    Case .InpsectMethod = Comp_Inspect_Method.Comp_4Way OrElse .InpsectMethod = Comp_Inspect_Method.Comp_HV
                        If .DefectType = Comp_InsperrorType.Comp_Bright Then
                            Call Bright4Way.Add(.DefectBoundary.GetRatioRectangle(1))
                        Else
                            Call Dark4Way.Add(.DefectBoundary.GetRatioRectangle(1))
                        End If
                    Case .InpsectMethod = Comp_Inspect_Method.Comp_Vertical OrElse .InpsectMethod = Comp_Inspect_Method.Comp_Horizontal
                        If .DefectType = Comp_InsperrorType.Comp_Bright Then
                            Call Bright2Way.Add(.DefectBoundary.GetRatioRectangle(1))
                        Else
                            Call Dark2Way.Add(.DefectBoundary.GetRatioRectangle(1))
                        End If
                    Case Else
                        Call NormalDefect.Add(.DefectBoundary.GetRatioRectangle(1))
                End Select
            End With
        Next

        Using oPenBright2 As New Pen(Color.Lavender, 2), oPenBright4 As New Pen(Color.LightBlue, 2), oPenDark2 As New Pen(Color.Blue, 2), oPenDark4 As New Pen(Color.DarkBlue, 2), oPenDirect As New Pen(Color.OrangeRed, 2), oPenBrightModel As New Pen(Color.YellowGreen, 2), oPenDarkModel As New Pen(Color.DarkGreen, 2), oPenOffsetModel As New Pen(Color.Red, 2), oPenpm As New Pen(Color.MistyRose, 2)
            Call oCanvas.DrawRectangle(oPenBright2, CodeReader.CodeZone)
            Call oCanvas.DrawRectangleList(oPenBright2, Bright2Way.ToArray)
            Call oCanvas.DrawRectangleList(oPenBright4, Bright4Way.ToArray)
            Call oCanvas.DrawRectangleList(oPenDark2, Dark2Way.ToArray)
            Call oCanvas.DrawRectangleList(oPenDark4, Dark4Way.ToArray)
            Call oCanvas.DrawRectangleList(oPenDirect, DirectModel.ToArray)
            Call oCanvas.DrawRectangleList(oPenBrightModel, BrightModel.ToArray)
            Call oCanvas.DrawRectangleList(oPenDarkModel, DarkModel.ToArray)
            Call oCanvas.DrawRectangleList(oPenOffsetModel, OffsetModel.ToArray)
            Call oCanvas.DrawRectangleList(oPenpm, NormalDefect.ToArray)
        End Using
    End Sub

    Private Sub DrawDefectCase1(oCanvas As iTVisionService.II_iTVCanvas)
        Using oPenModel As New Pen(Color.Fuchsia, 1)
            Call oCanvas.DrawRectangleList(oPenModel, ModelList.ToArray)
        End Using
    End Sub

    Private Sub DrawDefectCase2(oCanvas As iTVisionService.II_iTVCanvas)
        Using oPenModelCenter As New Pen(Color.Red, 1)
            Call oCanvas.DrawLineList(oPenModelCenter, ModelCenterListStart.ToArray, ModelCenterListEnd.ToArray)
        End Using
    End Sub
End Class