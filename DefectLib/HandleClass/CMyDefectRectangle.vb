Public Class CMyDefectRectangle
    <XmlAttribute("L")> Public Left As Integer
    <XmlAttribute("R")> Public Bottom As Integer
    <XmlAttribute("T")> Public Top As Integer
    <XmlAttribute("B")> Public Right As Integer

    <XmlAttribute()> Public Width As Integer
    <XmlAttribute()> Public Height As Integer

    Public Sub New()

    End Sub

    Public Sub New(L As Integer, T As Integer, R As Integer, B As Integer)
        Left = L
        Bottom = B
        Top = T
        Right = R

        Width = R - L
        Height = B - T
    End Sub

    Public Function ToRectangle() As Rectangle
        '' Augustin 220726 Add for Wafer Map
        Return Rectangle.FromLTRB(Left, Top, Right, Bottom)
    End Function

End Class