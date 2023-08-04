Partial Class CRecipeCamera : Implements ICloneable

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim oRecipeCamera As CRecipeCamera = CType(Me.MemberwiseClone, CRecipeCamera)

        oRecipeCamera.Locate1 = CType(Locate1.Clone, CRecipeLocate)
        oRecipeCamera.Locate2 = CType(Locate2.Clone, CRecipeLocate)

        oRecipeCamera.DefectList = New List(Of CMyDefect)

        Return oRecipeCamera
    End Function
End Class