Partial Class CRecipeModelDiff : Implements ICloneable

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return CType(Me.MemberwiseClone, CRecipeModelDiff)
    End Function

End Class