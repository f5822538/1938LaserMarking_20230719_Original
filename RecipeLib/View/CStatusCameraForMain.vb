Public Class CStatusCameraForMain : Inherits StatusStrip

    Public WithEvents MyLable As ToolStripStatusLabel

    Public Sub New()
        MyLable = New ToolStripStatusLabel()

        MyLable.Name = "Color"
        MyLable.AutoSize = False
        MyLable.Size = New System.Drawing.Size(400, 17)
        MyLable.Text = "灰階"

        Me.Items.AddRange(New ToolStripItem() {MyLable})
    End Sub
End Class