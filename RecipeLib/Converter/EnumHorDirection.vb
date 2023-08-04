Public Class EnumHorDirection : Inherits CEnumConvecter

    Public Shared EnumTypeList As List(Of ITV_Direction)

    Public Sub New(ByVal oEnumType As Type)

        Call MyBase.New(oEnumType)

        If EnumTypeList Is Nothing Then
            EnumTypeList = New List(Of ITV_Direction)

            EnumTypeList.Add(ITV_Direction.TopToBottom)
            EnumTypeList.Add(ITV_Direction.BottomToTop)

            Dim oAtts As New CAttributeChange
        End If

    End Sub

    Public Overrides Function GetStandardValues(context As ITypeDescriptorContext) As TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(EnumTypeList)
    End Function

End Class