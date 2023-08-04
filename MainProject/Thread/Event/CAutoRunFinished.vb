Public Class CAutoRunFinished : Inherits EventArgs

    Private moInspectSum As CInspectSum

    Public Sub New(oInspectSum As CInspectSum)
        moInspectSum = oInspectSum
    End Sub

    Public ReadOnly Property InspectSum As CInspectSum
        Get
            Return moInspectSum
        End Get
    End Property
End Class