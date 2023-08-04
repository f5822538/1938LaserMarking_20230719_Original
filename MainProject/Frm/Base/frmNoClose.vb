Public Class frmNoClose : Inherits Form

    Private Const CP_NOCLOSE_BUTTON As Integer = &H200

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim oCreateParams As CreateParams = MyBase.CreateParams
            oCreateParams.ClassStyle = oCreateParams.ClassStyle Or CP_NOCLOSE_BUTTON
            Return oCreateParams
        End Get
    End Property

End Class