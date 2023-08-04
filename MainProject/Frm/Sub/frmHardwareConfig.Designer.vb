<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHardwareConfig
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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
        Me.components = New System.ComponentModel.Container()
        Me.layoutHardwareConfig = New System.Windows.Forms.TableLayoutPanel()
        Me.flowLayoutHardwareConfig = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnLoad = New iTVisionService.ButtonLib.CButton()
        Me.btnSaveSingle = New iTVisionService.ButtonLib.CButton()
        Me.btnCancel = New iTVisionService.ButtonLib.CButton()
        Me.tabHardwareConfig = New iTVisionService.iTVControl.CSharpTabControl()
        Me.tabSystem = New System.Windows.Forms.TabPage()
        Me.pgdSystem = New System.Windows.Forms.PropertyGrid()
        Me.tabCamera = New System.Windows.Forms.TabPage()
        Me.pgdCamera = New System.Windows.Forms.PropertyGrid()
        Me.tabHandshake = New System.Windows.Forms.TabPage()
        Me.pgdHandshake = New System.Windows.Forms.PropertyGrid()
        Me.tabProcess = New System.Windows.Forms.TabPage()
        Me.pgdProcess = New System.Windows.Forms.PropertyGrid()
        Me.tabMisc = New System.Windows.Forms.TabPage()
        Me.pgdMisc = New System.Windows.Forms.PropertyGrid()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.layoutCamera = New System.Windows.Forms.TableLayoutPanel()
        Me.pgdCodeReaderCamera = New System.Windows.Forms.PropertyGrid()
        Me.labCamera = New System.Windows.Forms.Label()
        Me.labCodeReaderCamera = New System.Windows.Forms.Label()
        Me.layoutHardwareConfig.SuspendLayout()
        Me.flowLayoutHardwareConfig.SuspendLayout()
        Me.tabHardwareConfig.SuspendLayout()
        Me.tabSystem.SuspendLayout()
        Me.tabCamera.SuspendLayout()
        Me.tabHandshake.SuspendLayout()
        Me.tabProcess.SuspendLayout()
        Me.tabMisc.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutCamera.SuspendLayout()
        Me.SuspendLayout()
        '
        'layoutHardwareConfig
        '
        Me.layoutHardwareConfig.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.layoutHardwareConfig.ColumnCount = 1
        Me.layoutHardwareConfig.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutHardwareConfig.Controls.Add(Me.flowLayoutHardwareConfig, 0, 0)
        Me.layoutHardwareConfig.Controls.Add(Me.tabHardwareConfig, 0, 1)
        Me.layoutHardwareConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutHardwareConfig.Location = New System.Drawing.Point(0, 0)
        Me.layoutHardwareConfig.Name = "layoutHardwareConfig"
        Me.layoutHardwareConfig.RowCount = 2
        Me.layoutHardwareConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.layoutHardwareConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutHardwareConfig.Size = New System.Drawing.Size(995, 1000)
        Me.layoutHardwareConfig.TabIndex = 0
        '
        'flowLayoutHardwareConfig
        '
        Me.flowLayoutHardwareConfig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flowLayoutHardwareConfig.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.flowLayoutHardwareConfig.Controls.Add(Me.btnLoad)
        Me.flowLayoutHardwareConfig.Controls.Add(Me.btnSaveSingle)
        Me.flowLayoutHardwareConfig.Controls.Add(Me.btnCancel)
        Me.flowLayoutHardwareConfig.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.flowLayoutHardwareConfig.Location = New System.Drawing.Point(3, 3)
        Me.flowLayoutHardwareConfig.Name = "flowLayoutHardwareConfig"
        Me.flowLayoutHardwareConfig.Size = New System.Drawing.Size(989, 144)
        Me.flowLayoutHardwareConfig.TabIndex = 0
        '
        'btnLoad
        '
        Me.btnLoad.Corners.All = 18
        Me.btnLoad.Corners.LowerLeft = 18
        Me.btnLoad.Corners.LowerRight = 18
        Me.btnLoad.Corners.UpperLeft = 18
        Me.btnLoad.Corners.UpperRight = 18
        Me.btnLoad.DesignerSelected = False
        Me.btnLoad.Font = New System.Drawing.Font("微軟正黑體", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ImageIndex = 0
        Me.btnLoad.Location = New System.Drawing.Point(3, 3)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(263, 135)
        Me.btnLoad.TabIndex = 13
        Me.btnLoad.Text = "載入          Load"
        '
        'btnSaveSingle
        '
        Me.btnSaveSingle.Corners.All = 18
        Me.btnSaveSingle.Corners.LowerLeft = 18
        Me.btnSaveSingle.Corners.LowerRight = 18
        Me.btnSaveSingle.Corners.UpperLeft = 18
        Me.btnSaveSingle.Corners.UpperRight = 18
        Me.btnSaveSingle.DesignerSelected = False
        Me.btnSaveSingle.Font = New System.Drawing.Font("微軟正黑體", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveSingle.ImageIndex = 0
        Me.btnSaveSingle.Location = New System.Drawing.Point(272, 3)
        Me.btnSaveSingle.Name = "btnSaveSingle"
        Me.btnSaveSingle.Size = New System.Drawing.Size(263, 135)
        Me.btnSaveSingle.TabIndex = 12
        Me.btnSaveSingle.Text = "儲存參數 Save Config"
        '
        'btnCancel
        '
        Me.btnCancel.Corners.All = 18
        Me.btnCancel.Corners.LowerLeft = 18
        Me.btnCancel.Corners.LowerRight = 18
        Me.btnCancel.Corners.UpperLeft = 18
        Me.btnCancel.Corners.UpperRight = 18
        Me.btnCancel.DesignerSelected = False
        Me.btnCancel.Font = New System.Drawing.Font("微軟正黑體", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.Location = New System.Drawing.Point(541, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(263, 135)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "取消          Cancel"
        '
        'tabHardwareConfig
        '
        Me.tabHardwareConfig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabHardwareConfig.Controls.Add(Me.tabSystem)
        Me.tabHardwareConfig.Controls.Add(Me.tabCamera)
        Me.tabHardwareConfig.Controls.Add(Me.tabHandshake)
        Me.tabHardwareConfig.Controls.Add(Me.tabProcess)
        Me.tabHardwareConfig.Controls.Add(Me.tabMisc)
        Me.tabHardwareConfig.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.tabHardwareConfig.ItemSize = New System.Drawing.Size(300, 38)
        Me.tabHardwareConfig.Location = New System.Drawing.Point(3, 153)
        Me.tabHardwareConfig.Name = "tabHardwareConfig"
        Me.tabHardwareConfig.Padding = New System.Drawing.Point(9, 0)
        Me.tabHardwareConfig.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabHardwareConfig.SelectedIndex = 0
        Me.tabHardwareConfig.Size = New System.Drawing.Size(989, 844)
        Me.tabHardwareConfig.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabHardwareConfig.TabIndex = 1
        Me.tabHardwareConfig.UnSelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'tabSystem
        '
        Me.tabSystem.BackColor = System.Drawing.Color.Transparent
        Me.tabSystem.Controls.Add(Me.pgdSystem)
        Me.tabSystem.Location = New System.Drawing.Point(4, 42)
        Me.tabSystem.Name = "tabSystem"
        Me.tabSystem.Size = New System.Drawing.Size(1356, 551)
        Me.tabSystem.TabIndex = 0
        Me.tabSystem.Text = "系統設定 System"
        '
        'pgdSystem
        '
        Me.pgdSystem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdSystem.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdSystem.HelpVisible = False
        Me.pgdSystem.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdSystem.Location = New System.Drawing.Point(0, 0)
        Me.pgdSystem.Name = "pgdSystem"
        Me.pgdSystem.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdSystem.Size = New System.Drawing.Size(1356, 551)
        Me.pgdSystem.TabIndex = 2
        Me.pgdSystem.ToolbarVisible = False
        '
        'tabCamera
        '
        Me.tabCamera.BackColor = System.Drawing.Color.Transparent
        Me.tabCamera.Controls.Add(Me.layoutCamera)
        Me.tabCamera.Location = New System.Drawing.Point(4, 42)
        Me.tabCamera.Name = "tabCamera"
        Me.tabCamera.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCamera.Size = New System.Drawing.Size(981, 798)
        Me.tabCamera.TabIndex = 5
        Me.tabCamera.Text = "相機設定 Camera"
        '
        'pgdCamera
        '
        Me.pgdCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdCamera.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdCamera.HelpVisible = False
        Me.pgdCamera.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdCamera.Location = New System.Drawing.Point(3, 53)
        Me.pgdCamera.Name = "pgdCamera"
        Me.pgdCamera.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdCamera.Size = New System.Drawing.Size(969, 340)
        Me.pgdCamera.TabIndex = 6
        Me.pgdCamera.ToolbarVisible = False
        '
        'tabHandshake
        '
        Me.tabHandshake.BackColor = System.Drawing.Color.Transparent
        Me.tabHandshake.Controls.Add(Me.pgdHandshake)
        Me.tabHandshake.Location = New System.Drawing.Point(4, 42)
        Me.tabHandshake.Name = "tabHandshake"
        Me.tabHandshake.Padding = New System.Windows.Forms.Padding(3)
        Me.tabHandshake.Size = New System.Drawing.Size(1356, 551)
        Me.tabHandshake.TabIndex = 9
        Me.tabHandshake.Text = "交握 Handshake"
        '
        'pgdHandshake
        '
        Me.pgdHandshake.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdHandshake.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdHandshake.HelpVisible = False
        Me.pgdHandshake.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdHandshake.Location = New System.Drawing.Point(3, 3)
        Me.pgdHandshake.Name = "pgdHandshake"
        Me.pgdHandshake.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdHandshake.Size = New System.Drawing.Size(1350, 545)
        Me.pgdHandshake.TabIndex = 7
        Me.pgdHandshake.ToolbarVisible = False
        '
        'tabProcess
        '
        Me.tabProcess.BackColor = System.Drawing.Color.Transparent
        Me.tabProcess.Controls.Add(Me.pgdProcess)
        Me.tabProcess.Location = New System.Drawing.Point(4, 42)
        Me.tabProcess.Name = "tabProcess"
        Me.tabProcess.Size = New System.Drawing.Size(1356, 551)
        Me.tabProcess.TabIndex = 8
        Me.tabProcess.Text = "處理 Process"
        '
        'pgdProcess
        '
        Me.pgdProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdProcess.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdProcess.HelpVisible = False
        Me.pgdProcess.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdProcess.Location = New System.Drawing.Point(0, 0)
        Me.pgdProcess.Name = "pgdProcess"
        Me.pgdProcess.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdProcess.Size = New System.Drawing.Size(1356, 551)
        Me.pgdProcess.TabIndex = 5
        Me.pgdProcess.ToolbarVisible = False
        '
        'tabMisc
        '
        Me.tabMisc.BackColor = System.Drawing.Color.Transparent
        Me.tabMisc.Controls.Add(Me.pgdMisc)
        Me.tabMisc.Location = New System.Drawing.Point(4, 42)
        Me.tabMisc.Name = "tabMisc"
        Me.tabMisc.Size = New System.Drawing.Size(1356, 551)
        Me.tabMisc.TabIndex = 3
        Me.tabMisc.Text = "其它設定 Misc"
        '
        'pgdMisc
        '
        Me.pgdMisc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdMisc.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdMisc.HelpVisible = False
        Me.pgdMisc.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdMisc.Location = New System.Drawing.Point(0, 0)
        Me.pgdMisc.Name = "pgdMisc"
        Me.pgdMisc.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdMisc.Size = New System.Drawing.Size(1356, 551)
        Me.pgdMisc.TabIndex = 4
        Me.pgdMisc.ToolbarVisible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.Location = New System.Drawing.Point(3, 381)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(616, 293)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Maroon
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MintCream
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(616, 37)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "結果影像 (第三次 - Camera１)"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox2.BackColor = System.Drawing.Color.Black
        Me.PictureBox2.Location = New System.Drawing.Point(3, 43)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(616, 292)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Maroon
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MintCream
        Me.Label2.Location = New System.Drawing.Point(3, 341)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(616, 37)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "結果影像 (第三次 - Camera２)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'layoutCamera
        '
        Me.layoutCamera.ColumnCount = 1
        Me.layoutCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCamera.Controls.Add(Me.labCodeReaderCamera, 0, 2)
        Me.layoutCamera.Controls.Add(Me.labCamera, 0, 0)
        Me.layoutCamera.Controls.Add(Me.pgdCamera, 0, 1)
        Me.layoutCamera.Controls.Add(Me.pgdCodeReaderCamera, 0, 3)
        Me.layoutCamera.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutCamera.Location = New System.Drawing.Point(3, 3)
        Me.layoutCamera.Name = "layoutCamera"
        Me.layoutCamera.RowCount = 4
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutCamera.Size = New System.Drawing.Size(975, 792)
        Me.layoutCamera.TabIndex = 7
        '
        'pgdCodeReaderCamera
        '
        Me.pgdCodeReaderCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdCodeReaderCamera.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdCodeReaderCamera.HelpVisible = False
        Me.pgdCodeReaderCamera.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdCodeReaderCamera.Location = New System.Drawing.Point(3, 449)
        Me.pgdCodeReaderCamera.Name = "pgdCodeReaderCamera"
        Me.pgdCodeReaderCamera.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdCodeReaderCamera.Size = New System.Drawing.Size(969, 340)
        Me.pgdCodeReaderCamera.TabIndex = 7
        Me.pgdCodeReaderCamera.ToolbarVisible = False
        '
        'labCamera
        '
        Me.labCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labCamera.BackColor = System.Drawing.Color.Maroon
        Me.labCamera.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labCamera.ForeColor = System.Drawing.Color.MintCream
        Me.labCamera.Location = New System.Drawing.Point(3, 3)
        Me.labCamera.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.labCamera.Name = "labCamera"
        Me.labCamera.Size = New System.Drawing.Size(969, 47)
        Me.labCamera.TabIndex = 12
        Me.labCamera.Text = "相機 (檢測)"
        Me.labCamera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labCodeReaderCamera
        '
        Me.labCodeReaderCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labCodeReaderCamera.BackColor = System.Drawing.Color.Maroon
        Me.labCodeReaderCamera.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labCodeReaderCamera.ForeColor = System.Drawing.Color.MintCream
        Me.labCodeReaderCamera.Location = New System.Drawing.Point(3, 399)
        Me.labCodeReaderCamera.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.labCodeReaderCamera.Name = "labCodeReaderCamera"
        Me.labCodeReaderCamera.Size = New System.Drawing.Size(969, 47)
        Me.labCodeReaderCamera.TabIndex = 13
        Me.labCodeReaderCamera.Text = "相機 (條碼)"
        Me.labCodeReaderCamera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmHardwareConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(995, 1000)
        Me.Controls.Add(Me.layoutHardwareConfig)
        Me.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHardwareConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "捷亨機台硬體參數設定"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.layoutHardwareConfig.ResumeLayout(False)
        Me.flowLayoutHardwareConfig.ResumeLayout(False)
        Me.tabHardwareConfig.ResumeLayout(False)
        Me.tabSystem.ResumeLayout(False)
        Me.tabCamera.ResumeLayout(False)
        Me.tabHandshake.ResumeLayout(False)
        Me.tabProcess.ResumeLayout(False)
        Me.tabMisc.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutCamera.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents layoutHardwareConfig As System.Windows.Forms.TableLayoutPanel
    Private WithEvents flowLayoutHardwareConfig As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents tabHardwareConfig As iTVisionService.iTVControl.CSharpTabControl
    Friend WithEvents tabSystem As System.Windows.Forms.TabPage
    Private WithEvents pgdSystem As System.Windows.Forms.PropertyGrid
    Friend WithEvents btnSaveSingle As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnLoad As iTVisionService.ButtonLib.CButton
    Friend WithEvents tabMisc As System.Windows.Forms.TabPage
    Private WithEvents pgdMisc As System.Windows.Forms.PropertyGrid
    Friend WithEvents tabCamera As System.Windows.Forms.TabPage
    Private WithEvents pgdCamera As System.Windows.Forms.PropertyGrid
    Friend WithEvents btnCancel As iTVisionService.ButtonLib.CButton
    Friend WithEvents tabProcess As System.Windows.Forms.TabPage
    Private WithEvents pgdProcess As System.Windows.Forms.PropertyGrid
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tabHandshake As System.Windows.Forms.TabPage
    Private WithEvents pgdHandshake As System.Windows.Forms.PropertyGrid
    Friend WithEvents layoutCamera As System.Windows.Forms.TableLayoutPanel
    Private WithEvents pgdCodeReaderCamera As System.Windows.Forms.PropertyGrid
    Friend WithEvents labCodeReaderCamera As System.Windows.Forms.Label
    Friend WithEvents labCamera As System.Windows.Forms.Label
End Class
