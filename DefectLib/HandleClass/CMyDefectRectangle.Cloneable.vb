Partial Class CMyDefectRectangle : Implements ICloneable
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return Me.MemberwiseClone
    End Function
End Class