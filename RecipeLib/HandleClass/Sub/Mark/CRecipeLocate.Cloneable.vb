Partial Class CRecipeLocate : Implements ICloneable

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return CType(Me.MemberwiseClone, CRecipeLocate)
    End Function
End Class