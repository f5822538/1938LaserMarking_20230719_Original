Public Class CRecipeMarkList : Inherits CConfigBase

    Public RecipeMarkList As New List(Of CRecipeMark)

    <Browsable(False)> Public Property RecipeMarkCount As Integer = 0
    <Browsable(False)> Public Property MarkListString As String = ""

    Public Sub New(oSetting As II_Setting)
        MyBase.New(oSetting)
    End Sub
End Class