Public Class CMyUpdateStatus : Inherits CConfigBaseData

    Private mnLoadPercent As Integer = 0
    Private mnPercent As Integer = 0

    Public Sub New(oSync As SynchronizationContext)
        MyBase.New(Nothing, oSync)
    End Sub

    Public Property LoadPercent As Integer
        Get
            Return mnLoadPercent
        End Get
        Set(value As Integer)
            mnLoadPercent = value
            MyBase.OnPropertyChanged(FindName(Function() mnLoadPercent))
        End Set
    End Property

    Public Property Percent As Integer
        Get
            Return mnPercent
        End Get
        Set(value As Integer)
            mnPercent = value
            MyBase.OnPropertyChanged(FindName(Function() Percent))
        End Set
    End Property
End Class