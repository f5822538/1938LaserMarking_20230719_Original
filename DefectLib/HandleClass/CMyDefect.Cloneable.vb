Partial Class CMyDefect : Implements ICloneable
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim oClone As CMyDefect = CType(Me.MemberwiseClone, CMyDefect)

        oClone.DefectBoundary = CType(DefectBoundary.Clone, CMyDefectRectangle)
        oClone.DefectCenter = CType(DefectCenter.Clone, CITVPointWapper)
        oClone.DefectPosition = CType(DefectPosition.Clone, CITVPointWapper)
        oClone.DefectCoordinate = CType(DefectCoordinate.Clone, CITVPointWapper)  '' Augustin 220726 Add for Wafer Map
        oClone.DefectIndex = CType(DefectIndex.Clone, CITVPointWapper)
        oClone.DefectSize = CType(DefectSize.Clone, CITVPointWapper)

        Return oClone
    End Function
End Class