Partial Class CMyDefectRectangle
    Public Shared Narrowing Operator CType(ByVal tComp_IRect As iTVDefaultRect) As CMyDefectRectangle
        Dim oMyDefectRectangle As New CMyDefectRectangle

        oMyDefectRectangle.Left = tComp_IRect.Left
        oMyDefectRectangle.Right = tComp_IRect.Right
        oMyDefectRectangle.Top = tComp_IRect.Top
        oMyDefectRectangle.Bottom = tComp_IRect.Bottom

        oMyDefectRectangle.Width = tComp_IRect.Width
        oMyDefectRectangle.Height = tComp_IRect.Height

        Return oMyDefectRectangle
    End Operator

    Public Shared Narrowing Operator CType(ByVal oMyDefectRectangle As CMyDefectRectangle) As iTVDefaultRect
        Dim tComp_IRect As New iTVDefaultRect

        tComp_IRect.Left = oMyDefectRectangle.Left
        tComp_IRect.Right = oMyDefectRectangle.Right
        tComp_IRect.Top = oMyDefectRectangle.Top
        tComp_IRect.Bottom = oMyDefectRectangle.Bottom

        tComp_IRect.Width = oMyDefectRectangle.Width
        tComp_IRect.Height = oMyDefectRectangle.Height

        Return tComp_IRect
    End Operator
End Class