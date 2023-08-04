Public Class CExpandConvert(Of T) : Inherits ExpandableObjectConverter

    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
        If destinationType Is GetType(String) AndAlso TypeOf value Is T Then
            Dim oType As T = CType(value, T)
            Return ""
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

End Class