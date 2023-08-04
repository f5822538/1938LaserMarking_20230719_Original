Public Class EnumInspectTypeConverter : Inherits CEnumConvecter

    Public Shared EnumPLCTypeList As List(Of Comp_Inspect_Method)

    Public Sub New(ByVal oEnumType As Type)

        Call MyBase.New(oEnumType)

        If EnumPLCTypeList Is Nothing Then
            EnumPLCTypeList = New List(Of Comp_Inspect_Method)

            EnumPLCTypeList.Add(Comp_Inspect_Method.Comp_Horizontal)
            EnumPLCTypeList.Add(Comp_Inspect_Method.Comp_Vertical)
            EnumPLCTypeList.Add(Comp_Inspect_Method.Comp_4Way)
            EnumPLCTypeList.Add(Comp_Inspect_Method.Comp_HV)
            EnumPLCTypeList.Add(Comp_Inspect_Method.Comp_AverageGrey1)
            EnumPLCTypeList.Add(Comp_Inspect_Method.Comp_AverageGrey2)

            Dim oAtts As New CAttributeChange
        End If

    End Sub

    Public Overrides Function GetStandardValues(context As ITypeDescriptorContext) As TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(EnumPLCTypeList)
    End Function

End Class