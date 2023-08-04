Partial Class CMyDefectRectangle
    Public Sub Offset(aOffset As Point)
        Left = Left + aOffset.X
        Right = Right + aOffset.X

        Top = Top + aOffset.Y
        Bottom = Bottom + aOffset.Y
    End Sub

    Public Function GetRatioRectangle(nRatio As Integer) As Rectangle
        Dim oRect As Rectangle = New Rectangle(New Point(Left \ nRatio, Top \ nRatio), New Size(Math.Max(1, Width \ nRatio), Math.Max(1, Height \ nRatio)))

        Return oRect
    End Function
End Class