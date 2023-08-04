<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrDefectView
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LayoutDefect = New System.Windows.Forms.TableLayoutPanel()
        Me.picDM = New System.Windows.Forms.PictureBox()
        Me.DefectViewCamera = New DefectLib.CDefectViewCamera()
        Me.LayoutPIC = New System.Windows.Forms.TableLayoutPanel()
        Me.PIC4 = New System.Windows.Forms.PictureBox()
        Me.PIC3 = New System.Windows.Forms.PictureBox()
        Me.PIC2 = New System.Windows.Forms.PictureBox()
        Me.PIC1 = New System.Windows.Forms.PictureBox()
        Me.LayoutDefect.SuspendLayout()
        CType(Me.picDM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutPIC.SuspendLayout()
        CType(Me.PIC4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PIC3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PIC2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PIC1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutDefect
        '
        Me.LayoutDefect.ColumnCount = 2
        Me.LayoutDefect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.LayoutDefect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.LayoutDefect.Controls.Add(Me.picDM, 0, 1)
        Me.LayoutDefect.Controls.Add(Me.DefectViewCamera, 0, 0)
        Me.LayoutDefect.Controls.Add(Me.LayoutPIC, 1, 1)
        Me.LayoutDefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutDefect.Location = New System.Drawing.Point(0, 0)
        Me.LayoutDefect.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutDefect.Name = "LayoutDefect"
        Me.LayoutDefect.RowCount = 2
        Me.LayoutDefect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.LayoutDefect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.LayoutDefect.Size = New System.Drawing.Size(1258, 280)
        Me.LayoutDefect.TabIndex = 0
        '
        'picDM
        '
        Me.picDM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDM.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.picDM.Location = New System.Drawing.Point(3, 171)
        Me.picDM.Name = "picDM"
        Me.picDM.Size = New System.Drawing.Size(497, 106)
        Me.picDM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picDM.TabIndex = 1
        Me.picDM.TabStop = False
        '
        'DefectViewCamera
        '
        Me.DefectViewCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LayoutDefect.SetColumnSpan(Me.DefectViewCamera, 2)
        Me.DefectViewCamera.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DefectViewCamera.FullRowSelect = True
        Me.DefectViewCamera.GridLines = True
        Me.DefectViewCamera.Location = New System.Drawing.Point(3, 3)
        Me.DefectViewCamera.Name = "DefectViewCamera"
        Me.DefectViewCamera.Size = New System.Drawing.Size(1252, 162)
        Me.DefectViewCamera.TabIndex = 0
        Me.DefectViewCamera.UseCompatibleStateImageBehavior = False
        Me.DefectViewCamera.View = System.Windows.Forms.View.Details
        '
        'LayoutPIC
        '
        Me.LayoutPIC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LayoutPIC.ColumnCount = 4
        Me.LayoutPIC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.LayoutPIC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.LayoutPIC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.LayoutPIC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.LayoutPIC.Controls.Add(Me.PIC4, 3, 0)
        Me.LayoutPIC.Controls.Add(Me.PIC3, 2, 0)
        Me.LayoutPIC.Controls.Add(Me.PIC2, 1, 0)
        Me.LayoutPIC.Controls.Add(Me.PIC1, 0, 0)
        Me.LayoutPIC.Location = New System.Drawing.Point(506, 171)
        Me.LayoutPIC.Name = "LayoutPIC"
        Me.LayoutPIC.RowCount = 1
        Me.LayoutPIC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.LayoutPIC.Size = New System.Drawing.Size(749, 106)
        Me.LayoutPIC.TabIndex = 0
        '
        'PIC4
        '
        Me.PIC4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PIC4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PIC4.Location = New System.Drawing.Point(564, 3)
        Me.PIC4.Name = "PIC4"
        Me.PIC4.Size = New System.Drawing.Size(182, 100)
        Me.PIC4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PIC4.TabIndex = 3
        Me.PIC4.TabStop = False
        '
        'PIC3
        '
        Me.PIC3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PIC3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PIC3.Location = New System.Drawing.Point(377, 3)
        Me.PIC3.Name = "PIC3"
        Me.PIC3.Size = New System.Drawing.Size(181, 100)
        Me.PIC3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PIC3.TabIndex = 2
        Me.PIC3.TabStop = False
        '
        'PIC2
        '
        Me.PIC2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PIC2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PIC2.Location = New System.Drawing.Point(190, 3)
        Me.PIC2.Name = "PIC2"
        Me.PIC2.Size = New System.Drawing.Size(181, 100)
        Me.PIC2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PIC2.TabIndex = 1
        Me.PIC2.TabStop = False
        '
        'PIC1
        '
        Me.PIC1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PIC1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PIC1.Location = New System.Drawing.Point(3, 3)
        Me.PIC1.Name = "PIC1"
        Me.PIC1.Size = New System.Drawing.Size(181, 100)
        Me.PIC1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PIC1.TabIndex = 0
        Me.PIC1.TabStop = False
        '
        'usrDefectView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Controls.Add(Me.LayoutDefect)
        Me.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "usrDefectView"
        Me.Size = New System.Drawing.Size(1258, 280)
        Me.LayoutDefect.ResumeLayout(False)
        CType(Me.picDM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutPIC.ResumeLayout(False)
        CType(Me.PIC4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PIC3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PIC2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PIC1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutDefect As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DefectViewCamera As DefectLib.CDefectViewCamera
    Friend WithEvents LayoutPIC As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PIC4 As System.Windows.Forms.PictureBox
    Friend WithEvents PIC3 As System.Windows.Forms.PictureBox
    Friend WithEvents PIC2 As System.Windows.Forms.PictureBox
    Friend WithEvents PIC1 As System.Windows.Forms.PictureBox
    Friend WithEvents picDM As System.Windows.Forms.PictureBox

End Class
