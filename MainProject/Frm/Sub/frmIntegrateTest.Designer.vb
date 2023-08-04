<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIntegrateTest
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIntegrateTest))
        Me.btnQuit = New iTVisionService.ButtonLib.CButton()
        Me.bkUpdate = New System.ComponentModel.BackgroundWorker()
        Me.tabIntegrateTest = New iTVisionService.iTVControl.CSharpTabControl()
        Me.tabIO = New System.Windows.Forms.TabPage()
        Me.usr8In8Out = New iTVisionService.iTVControl.usrIn8Out8()
        Me.tabHandshake = New System.Windows.Forms.TabPage()
        Me.layoutHandshake = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvMarkInfo = New System.Windows.Forms.DataGridView()
        Me.BinCodeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResultDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSourceMarkInfo = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigatorMarkInfo = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.pgdMarkInfo = New System.Windows.Forms.PropertyGrid()
        Me.pgdHandshake = New System.Windows.Forms.PropertyGrid()
        Me.dgvProduct = New System.Windows.Forms.DataGridView()
        Me.LotIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubstrateIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DimensionXDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DimensionYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecipeIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ErrorCodeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ErrorTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSourceProduct = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigatorHandshake = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripButtonRead = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBoxHandshakeType = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparatorHandshakeLine1St = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonSendLotInfoACK = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonSendStripMapDownloadACK = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonSendStripMapUpload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparatorHandshakeLine2Nd = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonLinkProductList = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonUnlinkProductList = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparatorHandshakeLine3Rd = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonAddProduct = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonClearProduct = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparatorHandshakeLine4Th = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonLoadProductConfig = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonSaveProductConfig = New System.Windows.Forms.ToolStripButton()
        Me.tabCamera = New System.Windows.Forms.TabPage()
        Me.layoutCamera = New System.Windows.Forms.TableLayoutPanel()
        Me.usrStatusCamera = New RecipeLib.CStatusCameraForMain()
        Me.picCameraView = New iTVisionService.usrDisplay()
        Me.panCamera = New System.Windows.Forms.Panel()
        Me.btnCameraCodeReader = New iTVisionService.ButtonLib.CButton()
        Me.rtxtCamera = New System.Windows.Forms.RichTextBox()
        Me.btnCameraSaveImage = New iTVisionService.ButtonLib.CButton()
        Me.btnCameraGain = New iTVisionService.ButtonLib.CButton()
        Me.btnCameraExposure = New iTVisionService.ButtonLib.CButton()
        Me.txtCameraCurrentExpos = New System.Windows.Forms.TextBox()
        Me.lblCameraExporse = New System.Windows.Forms.Label()
        Me.txtCameraGain = New System.Windows.Forms.TextBox()
        Me.txtCameraExpos = New System.Windows.Forms.TextBox()
        Me.lblCameraFocus = New System.Windows.Forms.Label()
        Me.txtCameraFocus = New System.Windows.Forms.TextBox()
        Me.txtCameraCurrentGain = New System.Windows.Forms.TextBox()
        Me.lblCameraMaxExpos = New System.Windows.Forms.Label()
        Me.txtCameraMinExpos = New System.Windows.Forms.TextBox()
        Me.lblCameraMinExpos = New System.Windows.Forms.Label()
        Me.txtCameraMaxExpos = New System.Windows.Forms.TextBox()
        Me.lblCameraGain = New System.Windows.Forms.Label()
        Me.tabCodeReaderCamera = New System.Windows.Forms.TabPage()
        Me.layoutCodeReaderCamera = New System.Windows.Forms.TableLayoutPanel()
        Me.usrStatusCodeReaderCamera = New RecipeLib.CStatusCameraForMain()
        Me.picCodeReaderCameraView = New iTVisionService.usrDisplay()
        Me.panCodeReaderCamera = New System.Windows.Forms.Panel()
        Me.btnCodeReader = New iTVisionService.ButtonLib.CButton()
        Me.rtxtCodeReaderCamera = New System.Windows.Forms.RichTextBox()
        Me.btnCodeReaderCameraSaveImage = New iTVisionService.ButtonLib.CButton()
        Me.btnCodeReaderCameraGain = New iTVisionService.ButtonLib.CButton()
        Me.btnCodeReaderCameraExposure = New iTVisionService.ButtonLib.CButton()
        Me.txtCodeReaderCameraCurrentExpos = New System.Windows.Forms.TextBox()
        Me.lblCodeReaderCameraExporse = New System.Windows.Forms.Label()
        Me.txtCodeReaderCameraGain = New System.Windows.Forms.TextBox()
        Me.txtCodeReaderCameraExpos = New System.Windows.Forms.TextBox()
        Me.lblCodeReaderCameraFocus = New System.Windows.Forms.Label()
        Me.txtCodeReaderCameraFocus = New System.Windows.Forms.TextBox()
        Me.txtCodeReaderCameraCurrentGain = New System.Windows.Forms.TextBox()
        Me.lblCodeReaderCameraMaxExpos = New System.Windows.Forms.Label()
        Me.txtCodeReaderCameraMinExpos = New System.Windows.Forms.TextBox()
        Me.lblCodeReaderCameraMinExpos = New System.Windows.Forms.Label()
        Me.txtCodeReaderCameraMaxExpos = New System.Windows.Forms.TextBox()
        Me.lblCodeReaderCameraGain = New System.Windows.Forms.Label()
        Me.layoutIntegrateTest = New System.Windows.Forms.TableLayoutPanel()
        Me.flowLayoutIntegrateTest = New System.Windows.Forms.FlowLayoutPanel()
        Me.bkCommand = New System.ComponentModel.BackgroundWorker()
        Me.tabIntegrateTest.SuspendLayout()
        Me.tabIO.SuspendLayout()
        Me.tabHandshake.SuspendLayout()
        Me.layoutHandshake.SuspendLayout()
        CType(Me.dgvMarkInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceMarkInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigatorMarkInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigatorMarkInfo.SuspendLayout()
        CType(Me.dgvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigatorHandshake, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigatorHandshake.SuspendLayout()
        Me.tabCamera.SuspendLayout()
        Me.layoutCamera.SuspendLayout()
        Me.panCamera.SuspendLayout()
        Me.tabCodeReaderCamera.SuspendLayout()
        Me.layoutCodeReaderCamera.SuspendLayout()
        Me.panCodeReaderCamera.SuspendLayout()
        Me.layoutIntegrateTest.SuspendLayout()
        Me.flowLayoutIntegrateTest.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnQuit
        '
        Me.btnQuit.Corners.All = 13
        Me.btnQuit.Corners.LowerLeft = 13
        Me.btnQuit.Corners.LowerRight = 13
        Me.btnQuit.Corners.UpperLeft = 13
        Me.btnQuit.Corners.UpperRight = 13
        Me.btnQuit.DesignerSelected = False
        Me.btnQuit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnQuit.ImageIndex = 0
        Me.btnQuit.Location = New System.Drawing.Point(3, 3)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(134, 38)
        Me.btnQuit.TabIndex = 118
        Me.btnQuit.Text = "離開 Quit"
        '
        'bkUpdate
        '
        Me.bkUpdate.WorkerReportsProgress = True
        Me.bkUpdate.WorkerSupportsCancellation = True
        '
        'tabIntegrateTest
        '
        Me.tabIntegrateTest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabIntegrateTest.Controls.Add(Me.tabIO)
        Me.tabIntegrateTest.Controls.Add(Me.tabHandshake)
        Me.tabIntegrateTest.Controls.Add(Me.tabCamera)
        Me.tabIntegrateTest.Controls.Add(Me.tabCodeReaderCamera)
        Me.tabIntegrateTest.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.tabIntegrateTest.ItemSize = New System.Drawing.Size(200, 28)
        Me.tabIntegrateTest.Location = New System.Drawing.Point(3, 53)
        Me.tabIntegrateTest.Name = "tabIntegrateTest"
        Me.tabIntegrateTest.Padding = New System.Drawing.Point(9, 0)
        Me.tabIntegrateTest.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabIntegrateTest.SelectedIndex = 0
        Me.tabIntegrateTest.Size = New System.Drawing.Size(1278, 697)
        Me.tabIntegrateTest.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabIntegrateTest.TabIndex = 3
        Me.tabIntegrateTest.UnSelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'tabIO
        '
        Me.tabIO.BackColor = System.Drawing.Color.Transparent
        Me.tabIO.Controls.Add(Me.usr8In8Out)
        Me.tabIO.Location = New System.Drawing.Point(4, 32)
        Me.tabIO.Name = "tabIO"
        Me.tabIO.Size = New System.Drawing.Size(1270, 661)
        Me.tabIO.TabIndex = 11
        Me.tabIO.Text = "輸出入監控 I / O"
        '
        'usr8In8Out
        '
        Me.usr8In8Out.BackColor = System.Drawing.Color.Black
        Me.usr8In8Out.Dock = System.Windows.Forms.DockStyle.Fill
        Me.usr8In8Out.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usr8In8Out.Location = New System.Drawing.Point(0, 0)
        Me.usr8In8Out.Margin = New System.Windows.Forms.Padding(4)
        Me.usr8In8Out.MinimumSize = New System.Drawing.Size(126, 369)
        Me.usr8In8Out.Name = "usr8In8Out"
        Me.usr8In8Out.Size = New System.Drawing.Size(1270, 661)
        Me.usr8In8Out.TabIndex = 16
        '
        'tabHandshake
        '
        Me.tabHandshake.BackColor = System.Drawing.Color.Transparent
        Me.tabHandshake.Controls.Add(Me.layoutHandshake)
        Me.tabHandshake.Location = New System.Drawing.Point(4, 32)
        Me.tabHandshake.Name = "tabHandshake"
        Me.tabHandshake.Size = New System.Drawing.Size(1270, 913)
        Me.tabHandshake.TabIndex = 12
        Me.tabHandshake.Text = "交握 Handshake"
        '
        'layoutHandshake
        '
        Me.layoutHandshake.ColumnCount = 2
        Me.layoutHandshake.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutHandshake.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.layoutHandshake.Controls.Add(Me.dgvMarkInfo, 1, 1)
        Me.layoutHandshake.Controls.Add(Me.BindingNavigatorMarkInfo, 1, 0)
        Me.layoutHandshake.Controls.Add(Me.pgdMarkInfo, 1, 3)
        Me.layoutHandshake.Controls.Add(Me.pgdHandshake, 0, 2)
        Me.layoutHandshake.Controls.Add(Me.dgvProduct, 0, 1)
        Me.layoutHandshake.Controls.Add(Me.BindingNavigatorHandshake, 0, 0)
        Me.layoutHandshake.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutHandshake.Location = New System.Drawing.Point(0, 0)
        Me.layoutHandshake.Name = "layoutHandshake"
        Me.layoutHandshake.RowCount = 4
        Me.layoutHandshake.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.layoutHandshake.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutHandshake.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.layoutHandshake.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.layoutHandshake.Size = New System.Drawing.Size(1270, 913)
        Me.layoutHandshake.TabIndex = 1
        '
        'dgvMarkInfo
        '
        Me.dgvMarkInfo.AllowUserToAddRows = False
        Me.dgvMarkInfo.AllowUserToDeleteRows = False
        Me.dgvMarkInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMarkInfo.AutoGenerateColumns = False
        Me.dgvMarkInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMarkInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BinCodeDataGridViewTextBoxColumn, Me.ResultDataGridViewTextBoxColumn})
        Me.dgvMarkInfo.DataSource = Me.BindingSourceMarkInfo
        Me.dgvMarkInfo.Location = New System.Drawing.Point(923, 43)
        Me.dgvMarkInfo.Name = "dgvMarkInfo"
        Me.dgvMarkInfo.ReadOnly = True
        Me.dgvMarkInfo.RowHeadersVisible = False
        Me.layoutHandshake.SetRowSpan(Me.dgvMarkInfo, 2)
        Me.dgvMarkInfo.RowTemplate.Height = 24
        Me.dgvMarkInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMarkInfo.Size = New System.Drawing.Size(344, 717)
        Me.dgvMarkInfo.TabIndex = 119
        '
        'BinCodeDataGridViewTextBoxColumn
        '
        Me.BinCodeDataGridViewTextBoxColumn.DataPropertyName = "BinCode"
        Me.BinCodeDataGridViewTextBoxColumn.HeaderText = "Bin Code"
        Me.BinCodeDataGridViewTextBoxColumn.Name = "BinCodeDataGridViewTextBoxColumn"
        Me.BinCodeDataGridViewTextBoxColumn.ReadOnly = True
        Me.BinCodeDataGridViewTextBoxColumn.Width = 120
        '
        'ResultDataGridViewTextBoxColumn
        '
        Me.ResultDataGridViewTextBoxColumn.DataPropertyName = "Result"
        Me.ResultDataGridViewTextBoxColumn.HeaderText = "結果"
        Me.ResultDataGridViewTextBoxColumn.Name = "ResultDataGridViewTextBoxColumn"
        Me.ResultDataGridViewTextBoxColumn.ReadOnly = True
        Me.ResultDataGridViewTextBoxColumn.Width = 180
        '
        'BindingSourceMarkInfo
        '
        Me.BindingSourceMarkInfo.DataSource = GetType(DefectLib.CMyMarkInfo)
        '
        'BindingNavigatorMarkInfo
        '
        Me.BindingNavigatorMarkInfo.AddNewItem = Nothing
        Me.BindingNavigatorMarkInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BindingNavigatorMarkInfo.BindingSource = Me.BindingSourceMarkInfo
        Me.BindingNavigatorMarkInfo.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigatorMarkInfo.DeleteItem = Nothing
        Me.BindingNavigatorMarkInfo.Dock = System.Windows.Forms.DockStyle.None
        Me.BindingNavigatorMarkInfo.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2})
        Me.BindingNavigatorMarkInfo.Location = New System.Drawing.Point(923, 3)
        Me.BindingNavigatorMarkInfo.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.BindingNavigatorMarkInfo.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigatorMarkInfo.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigatorMarkInfo.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigatorMarkInfo.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigatorMarkInfo.Name = "BindingNavigatorMarkInfo"
        Me.BindingNavigatorMarkInfo.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigatorMarkInfo.Size = New System.Drawing.Size(344, 37)
        Me.BindingNavigatorMarkInfo.TabIndex = 10
        Me.BindingNavigatorMarkInfo.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(24, 34)
        Me.BindingNavigatorCountItem.Text = "/{0}"
        Me.BindingNavigatorCountItem.ToolTipText = "項目總數"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 34)
        Me.BindingNavigatorMoveFirstItem.Text = "移到最前面"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 34)
        Me.BindingNavigatorMovePreviousItem.Text = "移到上一個"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 37)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "位置"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 22)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "目前的位置"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 37)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 34)
        Me.BindingNavigatorMoveNextItem.Text = "移到下一個"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 34)
        Me.BindingNavigatorMoveLastItem.Text = "移到最後面"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 37)
        '
        'pgdMarkInfo
        '
        Me.pgdMarkInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdMarkInfo.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdMarkInfo.HelpVisible = False
        Me.pgdMarkInfo.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdMarkInfo.Location = New System.Drawing.Point(923, 763)
        Me.pgdMarkInfo.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pgdMarkInfo.Name = "pgdMarkInfo"
        Me.pgdMarkInfo.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdMarkInfo.Size = New System.Drawing.Size(344, 147)
        Me.pgdMarkInfo.TabIndex = 11
        Me.pgdMarkInfo.ToolbarVisible = False
        '
        'pgdHandshake
        '
        Me.pgdHandshake.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdHandshake.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdHandshake.HelpVisible = False
        Me.pgdHandshake.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgdHandshake.Location = New System.Drawing.Point(3, 413)
        Me.pgdHandshake.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pgdHandshake.Name = "pgdHandshake"
        Me.pgdHandshake.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.layoutHandshake.SetRowSpan(Me.pgdHandshake, 2)
        Me.pgdHandshake.Size = New System.Drawing.Size(914, 497)
        Me.pgdHandshake.TabIndex = 8
        Me.pgdHandshake.ToolbarVisible = False
        '
        'dgvProduct
        '
        Me.dgvProduct.AllowUserToAddRows = False
        Me.dgvProduct.AllowUserToDeleteRows = False
        Me.dgvProduct.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvProduct.AutoGenerateColumns = False
        Me.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProduct.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LotIDDataGridViewTextBoxColumn, Me.SubstrateIDDataGridViewTextBoxColumn, Me.DimensionXDataGridViewTextBoxColumn, Me.DimensionYDataGridViewTextBoxColumn, Me.RecipeIDDataGridViewTextBoxColumn, Me.ErrorCodeDataGridViewTextBoxColumn, Me.ErrorTextDataGridViewTextBoxColumn})
        Me.dgvProduct.DataSource = Me.BindingSourceProduct
        Me.dgvProduct.Location = New System.Drawing.Point(3, 43)
        Me.dgvProduct.Name = "dgvProduct"
        Me.dgvProduct.ReadOnly = True
        Me.dgvProduct.RowHeadersVisible = False
        Me.dgvProduct.RowTemplate.Height = 24
        Me.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProduct.Size = New System.Drawing.Size(914, 367)
        Me.dgvProduct.TabIndex = 120
        '
        'LotIDDataGridViewTextBoxColumn
        '
        Me.LotIDDataGridViewTextBoxColumn.DataPropertyName = "LotID"
        Me.LotIDDataGridViewTextBoxColumn.HeaderText = "Lot ID"
        Me.LotIDDataGridViewTextBoxColumn.Name = "LotIDDataGridViewTextBoxColumn"
        Me.LotIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.LotIDDataGridViewTextBoxColumn.Width = 200
        '
        'SubstrateIDDataGridViewTextBoxColumn
        '
        Me.SubstrateIDDataGridViewTextBoxColumn.DataPropertyName = "SubstrateID"
        Me.SubstrateIDDataGridViewTextBoxColumn.HeaderText = "Substrate ID"
        Me.SubstrateIDDataGridViewTextBoxColumn.Name = "SubstrateIDDataGridViewTextBoxColumn"
        Me.SubstrateIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.SubstrateIDDataGridViewTextBoxColumn.Width = 250
        '
        'DimensionXDataGridViewTextBoxColumn
        '
        Me.DimensionXDataGridViewTextBoxColumn.DataPropertyName = "DimensionX"
        Me.DimensionXDataGridViewTextBoxColumn.HeaderText = "Dimension X"
        Me.DimensionXDataGridViewTextBoxColumn.Name = "DimensionXDataGridViewTextBoxColumn"
        Me.DimensionXDataGridViewTextBoxColumn.ReadOnly = True
        Me.DimensionXDataGridViewTextBoxColumn.Width = 160
        '
        'DimensionYDataGridViewTextBoxColumn
        '
        Me.DimensionYDataGridViewTextBoxColumn.DataPropertyName = "DimensionY"
        Me.DimensionYDataGridViewTextBoxColumn.HeaderText = "Dimension Y"
        Me.DimensionYDataGridViewTextBoxColumn.Name = "DimensionYDataGridViewTextBoxColumn"
        Me.DimensionYDataGridViewTextBoxColumn.ReadOnly = True
        Me.DimensionYDataGridViewTextBoxColumn.Width = 160
        '
        'RecipeIDDataGridViewTextBoxColumn
        '
        Me.RecipeIDDataGridViewTextBoxColumn.DataPropertyName = "RecipeID"
        Me.RecipeIDDataGridViewTextBoxColumn.HeaderText = "Recipe ID"
        Me.RecipeIDDataGridViewTextBoxColumn.Name = "RecipeIDDataGridViewTextBoxColumn"
        Me.RecipeIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.RecipeIDDataGridViewTextBoxColumn.Width = 180
        '
        'ErrorCodeDataGridViewTextBoxColumn
        '
        Me.ErrorCodeDataGridViewTextBoxColumn.DataPropertyName = "ErrorCode"
        Me.ErrorCodeDataGridViewTextBoxColumn.HeaderText = "錯誤代碼"
        Me.ErrorCodeDataGridViewTextBoxColumn.Name = "ErrorCodeDataGridViewTextBoxColumn"
        Me.ErrorCodeDataGridViewTextBoxColumn.ReadOnly = True
        Me.ErrorCodeDataGridViewTextBoxColumn.Width = 120
        '
        'ErrorTextDataGridViewTextBoxColumn
        '
        Me.ErrorTextDataGridViewTextBoxColumn.DataPropertyName = "ErrorText"
        Me.ErrorTextDataGridViewTextBoxColumn.HeaderText = "錯誤內容"
        Me.ErrorTextDataGridViewTextBoxColumn.Name = "ErrorTextDataGridViewTextBoxColumn"
        Me.ErrorTextDataGridViewTextBoxColumn.ReadOnly = True
        Me.ErrorTextDataGridViewTextBoxColumn.Width = 350
        '
        'BindingSourceProduct
        '
        Me.BindingSourceProduct.DataSource = GetType(DefectLib.CMyProduct)
        '
        'BindingNavigatorHandshake
        '
        Me.BindingNavigatorHandshake.AddNewItem = Nothing
        Me.BindingNavigatorHandshake.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BindingNavigatorHandshake.CountItem = Nothing
        Me.BindingNavigatorHandshake.DeleteItem = Nothing
        Me.BindingNavigatorHandshake.Dock = System.Windows.Forms.DockStyle.None
        Me.BindingNavigatorHandshake.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonRead, Me.ToolStripTextBoxHandshakeType, Me.ToolStripSeparatorHandshakeLine1St, Me.ToolStripButtonSendLotInfoACK, Me.ToolStripButtonSendStripMapDownloadACK, Me.ToolStripButtonSendStripMapUpload, Me.ToolStripSeparatorHandshakeLine2Nd, Me.ToolStripButtonLinkProductList, Me.ToolStripButtonUnlinkProductList, Me.ToolStripSeparatorHandshakeLine3Rd, Me.ToolStripButtonAddProduct, Me.ToolStripButtonClearProduct, Me.ToolStripSeparatorHandshakeLine4Th, Me.ToolStripButtonLoadProductConfig, Me.ToolStripButtonSaveProductConfig})
        Me.BindingNavigatorHandshake.Location = New System.Drawing.Point(3, 3)
        Me.BindingNavigatorHandshake.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.BindingNavigatorHandshake.MoveFirstItem = Nothing
        Me.BindingNavigatorHandshake.MoveLastItem = Nothing
        Me.BindingNavigatorHandshake.MoveNextItem = Nothing
        Me.BindingNavigatorHandshake.MovePreviousItem = Nothing
        Me.BindingNavigatorHandshake.Name = "BindingNavigatorHandshake"
        Me.BindingNavigatorHandshake.PositionItem = Nothing
        Me.BindingNavigatorHandshake.Size = New System.Drawing.Size(914, 37)
        Me.BindingNavigatorHandshake.TabIndex = 9
        Me.BindingNavigatorHandshake.Text = "BindingNavigator1"
        '
        'ToolStripButtonRead
        '
        Me.ToolStripButtonRead.AutoSize = False
        Me.ToolStripButtonRead.BackColor = System.Drawing.Color.Maroon
        Me.ToolStripButtonRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonRead.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripButtonRead.ForeColor = System.Drawing.Color.LightCyan
        Me.ToolStripButtonRead.Image = CType(resources.GetObject("ToolStripButtonRead.Image"), System.Drawing.Image)
        Me.ToolStripButtonRead.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRead.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonRead.Name = "ToolStripButtonRead"
        Me.ToolStripButtonRead.Size = New System.Drawing.Size(60, 24)
        Me.ToolStripButtonRead.Text = "讀取"
        '
        'ToolStripTextBoxHandshakeType
        '
        Me.ToolStripTextBoxHandshakeType.AutoSize = False
        Me.ToolStripTextBoxHandshakeType.BackColor = System.Drawing.Color.SeaShell
        Me.ToolStripTextBoxHandshakeType.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripTextBoxHandshakeType.ForeColor = System.Drawing.Color.MidnightBlue
        Me.ToolStripTextBoxHandshakeType.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripTextBoxHandshakeType.Name = "ToolStripTextBoxHandshakeType"
        Me.ToolStripTextBoxHandshakeType.ReadOnly = True
        Me.ToolStripTextBoxHandshakeType.Size = New System.Drawing.Size(150, 27)
        Me.ToolStripTextBoxHandshakeType.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ToolStripSeparatorHandshakeLine1St
        '
        Me.ToolStripSeparatorHandshakeLine1St.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripSeparatorHandshakeLine1St.Name = "ToolStripSeparatorHandshakeLine1St"
        Me.ToolStripSeparatorHandshakeLine1St.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButtonSendLotInfoACK
        '
        Me.ToolStripButtonSendLotInfoACK.AutoSize = False
        Me.ToolStripButtonSendLotInfoACK.BackColor = System.Drawing.Color.Maroon
        Me.ToolStripButtonSendLotInfoACK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonSendLotInfoACK.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripButtonSendLotInfoACK.ForeColor = System.Drawing.Color.LightCyan
        Me.ToolStripButtonSendLotInfoACK.Image = CType(resources.GetObject("ToolStripButtonSendLotInfoACK.Image"), System.Drawing.Image)
        Me.ToolStripButtonSendLotInfoACK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSendLotInfoACK.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonSendLotInfoACK.Name = "ToolStripButtonSendLotInfoACK"
        Me.ToolStripButtonSendLotInfoACK.Size = New System.Drawing.Size(120, 24)
        Me.ToolStripButtonSendLotInfoACK.Text = "回復 Lot Info"
        '
        'ToolStripButtonSendStripMapDownloadACK
        '
        Me.ToolStripButtonSendStripMapDownloadACK.AutoSize = False
        Me.ToolStripButtonSendStripMapDownloadACK.BackColor = System.Drawing.Color.Maroon
        Me.ToolStripButtonSendStripMapDownloadACK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonSendStripMapDownloadACK.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripButtonSendStripMapDownloadACK.ForeColor = System.Drawing.Color.LightCyan
        Me.ToolStripButtonSendStripMapDownloadACK.Image = CType(resources.GetObject("ToolStripButtonSendStripMapDownloadACK.Image"), System.Drawing.Image)
        Me.ToolStripButtonSendStripMapDownloadACK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSendStripMapDownloadACK.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonSendStripMapDownloadACK.Name = "ToolStripButtonSendStripMapDownloadACK"
        Me.ToolStripButtonSendStripMapDownloadACK.Size = New System.Drawing.Size(150, 24)
        Me.ToolStripButtonSendStripMapDownloadACK.Text = "回復下載 Strip Map"
        Me.ToolStripButtonSendStripMapDownloadACK.Visible = False
        '
        'ToolStripButtonSendStripMapUpload
        '
        Me.ToolStripButtonSendStripMapUpload.AutoSize = False
        Me.ToolStripButtonSendStripMapUpload.BackColor = System.Drawing.Color.Maroon
        Me.ToolStripButtonSendStripMapUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonSendStripMapUpload.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripButtonSendStripMapUpload.ForeColor = System.Drawing.Color.LightCyan
        Me.ToolStripButtonSendStripMapUpload.Image = CType(resources.GetObject("ToolStripButtonSendStripMapUpload.Image"), System.Drawing.Image)
        Me.ToolStripButtonSendStripMapUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSendStripMapUpload.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonSendStripMapUpload.Name = "ToolStripButtonSendStripMapUpload"
        Me.ToolStripButtonSendStripMapUpload.Size = New System.Drawing.Size(120, 24)
        Me.ToolStripButtonSendStripMapUpload.Text = "上傳 Strip Map"
        '
        'ToolStripSeparatorHandshakeLine2Nd
        '
        Me.ToolStripSeparatorHandshakeLine2Nd.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripSeparatorHandshakeLine2Nd.Name = "ToolStripSeparatorHandshakeLine2Nd"
        Me.ToolStripSeparatorHandshakeLine2Nd.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButtonLinkProductList
        '
        Me.ToolStripButtonLinkProductList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonLinkProductList.Image = CType(resources.GetObject("ToolStripButtonLinkProductList.Image"), System.Drawing.Image)
        Me.ToolStripButtonLinkProductList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonLinkProductList.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonLinkProductList.Name = "ToolStripButtonLinkProductList"
        Me.ToolStripButtonLinkProductList.Size = New System.Drawing.Size(23, 34)
        Me.ToolStripButtonLinkProductList.Text = "連結產品列表 Link Product List"
        Me.ToolStripButtonLinkProductList.ToolTipText = "連結產品列表 Link Product List"
        '
        'ToolStripButtonUnlinkProductList
        '
        Me.ToolStripButtonUnlinkProductList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonUnlinkProductList.Image = CType(resources.GetObject("ToolStripButtonUnlinkProductList.Image"), System.Drawing.Image)
        Me.ToolStripButtonUnlinkProductList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonUnlinkProductList.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonUnlinkProductList.Name = "ToolStripButtonUnlinkProductList"
        Me.ToolStripButtonUnlinkProductList.Size = New System.Drawing.Size(23, 34)
        Me.ToolStripButtonUnlinkProductList.Text = "取消連結產品列表 Unlink Product List"
        Me.ToolStripButtonUnlinkProductList.ToolTipText = "取消連結產品列表 Unlink Product List"
        '
        'ToolStripSeparatorHandshakeLine3Rd
        '
        Me.ToolStripSeparatorHandshakeLine3Rd.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripSeparatorHandshakeLine3Rd.Name = "ToolStripSeparatorHandshakeLine3Rd"
        Me.ToolStripSeparatorHandshakeLine3Rd.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButtonAddProduct
        '
        Me.ToolStripButtonAddProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonAddProduct.Image = CType(resources.GetObject("ToolStripButtonAddProduct.Image"), System.Drawing.Image)
        Me.ToolStripButtonAddProduct.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonAddProduct.Name = "ToolStripButtonAddProduct"
        Me.ToolStripButtonAddProduct.RightToLeftAutoMirrorImage = True
        Me.ToolStripButtonAddProduct.Size = New System.Drawing.Size(23, 34)
        Me.ToolStripButtonAddProduct.Text = "加入新的"
        '
        'ToolStripButtonClearProduct
        '
        Me.ToolStripButtonClearProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonClearProduct.Image = CType(resources.GetObject("ToolStripButtonClearProduct.Image"), System.Drawing.Image)
        Me.ToolStripButtonClearProduct.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonClearProduct.Name = "ToolStripButtonClearProduct"
        Me.ToolStripButtonClearProduct.RightToLeftAutoMirrorImage = True
        Me.ToolStripButtonClearProduct.Size = New System.Drawing.Size(23, 34)
        Me.ToolStripButtonClearProduct.Text = "刪除"
        '
        'ToolStripSeparatorHandshakeLine4Th
        '
        Me.ToolStripSeparatorHandshakeLine4Th.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripSeparatorHandshakeLine4Th.Name = "ToolStripSeparatorHandshakeLine4Th"
        Me.ToolStripSeparatorHandshakeLine4Th.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButtonLoadProductConfig
        '
        Me.ToolStripButtonLoadProductConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonLoadProductConfig.Image = CType(resources.GetObject("ToolStripButtonLoadProductConfig.Image"), System.Drawing.Image)
        Me.ToolStripButtonLoadProductConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonLoadProductConfig.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonLoadProductConfig.Name = "ToolStripButtonLoadProductConfig"
        Me.ToolStripButtonLoadProductConfig.Size = New System.Drawing.Size(23, 34)
        Me.ToolStripButtonLoadProductConfig.Text = "載入產品資料 (INI) Load Product Information"
        Me.ToolStripButtonLoadProductConfig.ToolTipText = "載入產品資料 (INI) Load Product Information"
        '
        'ToolStripButtonSaveProductConfig
        '
        Me.ToolStripButtonSaveProductConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonSaveProductConfig.Image = CType(resources.GetObject("ToolStripButtonSaveProductConfig.Image"), System.Drawing.Image)
        Me.ToolStripButtonSaveProductConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSaveProductConfig.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonSaveProductConfig.Name = "ToolStripButtonSaveProductConfig"
        Me.ToolStripButtonSaveProductConfig.Size = New System.Drawing.Size(23, 34)
        Me.ToolStripButtonSaveProductConfig.Text = "儲存產品資料 (INI) Save Product Information"
        Me.ToolStripButtonSaveProductConfig.ToolTipText = "儲存產品資料 (INI) Save Product Information"
        '
        'tabCamera
        '
        Me.tabCamera.BackColor = System.Drawing.Color.Transparent
        Me.tabCamera.Controls.Add(Me.layoutCamera)
        Me.tabCamera.Location = New System.Drawing.Point(4, 32)
        Me.tabCamera.Name = "tabCamera"
        Me.tabCamera.Size = New System.Drawing.Size(1270, 661)
        Me.tabCamera.TabIndex = 10
        Me.tabCamera.Text = "相機 Camera"
        '
        'layoutCamera
        '
        Me.layoutCamera.ColumnCount = 2
        Me.layoutCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450.0!))
        Me.layoutCamera.Controls.Add(Me.usrStatusCamera, 0, 1)
        Me.layoutCamera.Controls.Add(Me.picCameraView, 0, 0)
        Me.layoutCamera.Controls.Add(Me.panCamera, 1, 0)
        Me.layoutCamera.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutCamera.Location = New System.Drawing.Point(0, 0)
        Me.layoutCamera.Name = "layoutCamera"
        Me.layoutCamera.RowCount = 2
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutCamera.Size = New System.Drawing.Size(1270, 661)
        Me.layoutCamera.TabIndex = 8
        '
        'usrStatusCamera
        '
        Me.usrStatusCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.usrStatusCamera.Dock = System.Windows.Forms.DockStyle.None
        Me.usrStatusCamera.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrStatusCamera.Location = New System.Drawing.Point(0, 631)
        Me.usrStatusCamera.Name = "usrStatusCamera"
        Me.usrStatusCamera.Size = New System.Drawing.Size(820, 30)
        Me.usrStatusCamera.TabIndex = 26
        Me.usrStatusCamera.Text = "StatusStrip1"
        '
        'picCameraView
        '
        Me.picCameraView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picCameraView.DetectValueOnMouseLocation = True
        Me.picCameraView.Location = New System.Drawing.Point(6, 4)
        Me.picCameraView.Margin = New System.Windows.Forms.Padding(6, 4, 6, 4)
        Me.picCameraView.Name = "picCameraView"
        Me.picCameraView.Size = New System.Drawing.Size(808, 623)
        Me.picCameraView.TabIndex = 3
        '
        'panCamera
        '
        Me.panCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panCamera.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panCamera.Controls.Add(Me.btnCameraCodeReader)
        Me.panCamera.Controls.Add(Me.rtxtCamera)
        Me.panCamera.Controls.Add(Me.btnCameraSaveImage)
        Me.panCamera.Controls.Add(Me.btnCameraGain)
        Me.panCamera.Controls.Add(Me.btnCameraExposure)
        Me.panCamera.Controls.Add(Me.txtCameraCurrentExpos)
        Me.panCamera.Controls.Add(Me.lblCameraExporse)
        Me.panCamera.Controls.Add(Me.txtCameraGain)
        Me.panCamera.Controls.Add(Me.txtCameraExpos)
        Me.panCamera.Controls.Add(Me.lblCameraFocus)
        Me.panCamera.Controls.Add(Me.txtCameraFocus)
        Me.panCamera.Controls.Add(Me.txtCameraCurrentGain)
        Me.panCamera.Controls.Add(Me.lblCameraMaxExpos)
        Me.panCamera.Controls.Add(Me.txtCameraMinExpos)
        Me.panCamera.Controls.Add(Me.lblCameraMinExpos)
        Me.panCamera.Controls.Add(Me.txtCameraMaxExpos)
        Me.panCamera.Controls.Add(Me.lblCameraGain)
        Me.panCamera.Location = New System.Drawing.Point(823, 3)
        Me.panCamera.Name = "panCamera"
        Me.layoutCamera.SetRowSpan(Me.panCamera, 2)
        Me.panCamera.Size = New System.Drawing.Size(444, 655)
        Me.panCamera.TabIndex = 11
        '
        'btnCameraCodeReader
        '
        Me.btnCameraCodeReader.Corners.All = 10
        Me.btnCameraCodeReader.Corners.LowerLeft = 10
        Me.btnCameraCodeReader.Corners.LowerRight = 10
        Me.btnCameraCodeReader.Corners.UpperLeft = 10
        Me.btnCameraCodeReader.Corners.UpperRight = 10
        Me.btnCameraCodeReader.DesignerSelected = False
        Me.btnCameraCodeReader.Font = New System.Drawing.Font("微軟正黑體", 20.25!)
        Me.btnCameraCodeReader.ImageIndex = 0
        Me.btnCameraCodeReader.Location = New System.Drawing.Point(3, 737)
        Me.btnCameraCodeReader.Name = "btnCameraCodeReader"
        Me.btnCameraCodeReader.Size = New System.Drawing.Size(200, 100)
        Me.btnCameraCodeReader.TabIndex = 27
        Me.btnCameraCodeReader.Text = "讀取條碼                      Code Reader"
        '
        'rtxtCamera
        '
        Me.rtxtCamera.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtCamera.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtCamera.Location = New System.Drawing.Point(3, 428)
        Me.rtxtCamera.Name = "rtxtCamera"
        Me.rtxtCamera.ReadOnly = True
        Me.rtxtCamera.Size = New System.Drawing.Size(432, 197)
        Me.rtxtCamera.TabIndex = 25
        Me.rtxtCamera.Text = ""
        Me.rtxtCamera.WordWrap = False
        '
        'btnCameraSaveImage
        '
        Me.btnCameraSaveImage.Corners.All = 10
        Me.btnCameraSaveImage.Corners.LowerLeft = 10
        Me.btnCameraSaveImage.Corners.LowerRight = 10
        Me.btnCameraSaveImage.Corners.UpperLeft = 10
        Me.btnCameraSaveImage.Corners.UpperRight = 10
        Me.btnCameraSaveImage.DesignerSelected = True
        Me.btnCameraSaveImage.Font = New System.Drawing.Font("微軟正黑體", 20.25!)
        Me.btnCameraSaveImage.ImageIndex = 0
        Me.btnCameraSaveImage.Location = New System.Drawing.Point(3, 631)
        Me.btnCameraSaveImage.Name = "btnCameraSaveImage"
        Me.btnCameraSaveImage.Size = New System.Drawing.Size(200, 100)
        Me.btnCameraSaveImage.TabIndex = 23
        Me.btnCameraSaveImage.Text = "儲存影像                      Save Image"
        '
        'btnCameraGain
        '
        Me.btnCameraGain.Corners.All = 10
        Me.btnCameraGain.Corners.LowerLeft = 10
        Me.btnCameraGain.Corners.LowerRight = 10
        Me.btnCameraGain.Corners.UpperLeft = 10
        Me.btnCameraGain.Corners.UpperRight = 10
        Me.btnCameraGain.DesignerSelected = False
        Me.btnCameraGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnCameraGain.ImageIndex = 0
        Me.btnCameraGain.Location = New System.Drawing.Point(3, 342)
        Me.btnCameraGain.Name = "btnCameraGain"
        Me.btnCameraGain.Size = New System.Drawing.Size(205, 80)
        Me.btnCameraGain.TabIndex = 22
        Me.btnCameraGain.Text = "修改增益                      Gain"
        '
        'btnCameraExposure
        '
        Me.btnCameraExposure.Corners.All = 10
        Me.btnCameraExposure.Corners.LowerLeft = 10
        Me.btnCameraExposure.Corners.LowerRight = 10
        Me.btnCameraExposure.Corners.UpperLeft = 10
        Me.btnCameraExposure.Corners.UpperRight = 10
        Me.btnCameraExposure.DesignerSelected = False
        Me.btnCameraExposure.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnCameraExposure.ImageIndex = 0
        Me.btnCameraExposure.Location = New System.Drawing.Point(3, 256)
        Me.btnCameraExposure.Name = "btnCameraExposure"
        Me.btnCameraExposure.Size = New System.Drawing.Size(205, 80)
        Me.btnCameraExposure.TabIndex = 21
        Me.btnCameraExposure.Text = "修改曝光時間                      Eposure Time"
        '
        'txtCameraCurrentExpos
        '
        Me.txtCameraCurrentExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCameraCurrentExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCameraCurrentExpos.Location = New System.Drawing.Point(235, 158)
        Me.txtCameraCurrentExpos.Name = "txtCameraCurrentExpos"
        Me.txtCameraCurrentExpos.ReadOnly = True
        Me.txtCameraCurrentExpos.Size = New System.Drawing.Size(205, 43)
        Me.txtCameraCurrentExpos.TabIndex = 18
        Me.txtCameraCurrentExpos.Text = "0"
        '
        'lblCameraExporse
        '
        Me.lblCameraExporse.BackColor = System.Drawing.Color.Transparent
        Me.lblCameraExporse.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCameraExporse.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCameraExporse.Location = New System.Drawing.Point(8, 164)
        Me.lblCameraExporse.Name = "lblCameraExporse"
        Me.lblCameraExporse.Size = New System.Drawing.Size(221, 29)
        Me.lblCameraExporse.TabIndex = 17
        Me.lblCameraExporse.Text = "目前曝光時間"
        Me.lblCameraExporse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCameraGain
        '
        Me.txtCameraGain.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCameraGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCameraGain.Location = New System.Drawing.Point(214, 362)
        Me.txtCameraGain.Name = "txtCameraGain"
        Me.txtCameraGain.Size = New System.Drawing.Size(221, 43)
        Me.txtCameraGain.TabIndex = 13
        Me.txtCameraGain.Text = "0"
        '
        'txtCameraExpos
        '
        Me.txtCameraExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCameraExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCameraExpos.Location = New System.Drawing.Point(214, 277)
        Me.txtCameraExpos.Name = "txtCameraExpos"
        Me.txtCameraExpos.Size = New System.Drawing.Size(226, 43)
        Me.txtCameraExpos.TabIndex = 12
        Me.txtCameraExpos.Text = "0"
        '
        'lblCameraFocus
        '
        Me.lblCameraFocus.BackColor = System.Drawing.Color.Transparent
        Me.lblCameraFocus.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCameraFocus.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCameraFocus.Location = New System.Drawing.Point(8, 17)
        Me.lblCameraFocus.Name = "lblCameraFocus"
        Me.lblCameraFocus.Size = New System.Drawing.Size(221, 29)
        Me.lblCameraFocus.TabIndex = 1
        Me.lblCameraFocus.Text = "清晰度"
        Me.lblCameraFocus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCameraFocus
        '
        Me.txtCameraFocus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCameraFocus.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCameraFocus.Location = New System.Drawing.Point(235, 11)
        Me.txtCameraFocus.Name = "txtCameraFocus"
        Me.txtCameraFocus.ReadOnly = True
        Me.txtCameraFocus.Size = New System.Drawing.Size(205, 43)
        Me.txtCameraFocus.TabIndex = 0
        Me.txtCameraFocus.Text = "0"
        '
        'txtCameraCurrentGain
        '
        Me.txtCameraCurrentGain.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCameraCurrentGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCameraCurrentGain.Location = New System.Drawing.Point(235, 207)
        Me.txtCameraCurrentGain.Name = "txtCameraCurrentGain"
        Me.txtCameraCurrentGain.ReadOnly = True
        Me.txtCameraCurrentGain.Size = New System.Drawing.Size(205, 43)
        Me.txtCameraCurrentGain.TabIndex = 8
        Me.txtCameraCurrentGain.Text = "0"
        '
        'lblCameraMaxExpos
        '
        Me.lblCameraMaxExpos.BackColor = System.Drawing.Color.Transparent
        Me.lblCameraMaxExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCameraMaxExpos.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCameraMaxExpos.Location = New System.Drawing.Point(8, 66)
        Me.lblCameraMaxExpos.Name = "lblCameraMaxExpos"
        Me.lblCameraMaxExpos.Size = New System.Drawing.Size(221, 29)
        Me.lblCameraMaxExpos.TabIndex = 2
        Me.lblCameraMaxExpos.Text = "最大曝光時間"
        Me.lblCameraMaxExpos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCameraMinExpos
        '
        Me.txtCameraMinExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCameraMinExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCameraMinExpos.Location = New System.Drawing.Point(235, 109)
        Me.txtCameraMinExpos.Name = "txtCameraMinExpos"
        Me.txtCameraMinExpos.ReadOnly = True
        Me.txtCameraMinExpos.Size = New System.Drawing.Size(205, 43)
        Me.txtCameraMinExpos.TabIndex = 7
        Me.txtCameraMinExpos.Text = "0"
        '
        'lblCameraMinExpos
        '
        Me.lblCameraMinExpos.BackColor = System.Drawing.Color.Transparent
        Me.lblCameraMinExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCameraMinExpos.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCameraMinExpos.Location = New System.Drawing.Point(8, 115)
        Me.lblCameraMinExpos.Name = "lblCameraMinExpos"
        Me.lblCameraMinExpos.Size = New System.Drawing.Size(221, 29)
        Me.lblCameraMinExpos.TabIndex = 3
        Me.lblCameraMinExpos.Text = "最小曝光時間"
        Me.lblCameraMinExpos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCameraMaxExpos
        '
        Me.txtCameraMaxExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCameraMaxExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCameraMaxExpos.Location = New System.Drawing.Point(235, 60)
        Me.txtCameraMaxExpos.Name = "txtCameraMaxExpos"
        Me.txtCameraMaxExpos.ReadOnly = True
        Me.txtCameraMaxExpos.Size = New System.Drawing.Size(205, 43)
        Me.txtCameraMaxExpos.TabIndex = 6
        Me.txtCameraMaxExpos.Text = "0"
        '
        'lblCameraGain
        '
        Me.lblCameraGain.BackColor = System.Drawing.Color.Transparent
        Me.lblCameraGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCameraGain.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCameraGain.Location = New System.Drawing.Point(8, 213)
        Me.lblCameraGain.Name = "lblCameraGain"
        Me.lblCameraGain.Size = New System.Drawing.Size(221, 29)
        Me.lblCameraGain.TabIndex = 4
        Me.lblCameraGain.Text = "目前增益"
        Me.lblCameraGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabCodeReaderCamera
        '
        Me.tabCodeReaderCamera.BackColor = System.Drawing.Color.Transparent
        Me.tabCodeReaderCamera.Controls.Add(Me.layoutCodeReaderCamera)
        Me.tabCodeReaderCamera.Location = New System.Drawing.Point(4, 32)
        Me.tabCodeReaderCamera.Name = "tabCodeReaderCamera"
        Me.tabCodeReaderCamera.Size = New System.Drawing.Size(1270, 913)
        Me.tabCodeReaderCamera.TabIndex = 13
        Me.tabCodeReaderCamera.Text = "條碼相機 Code"
        '
        'layoutCodeReaderCamera
        '
        Me.layoutCodeReaderCamera.ColumnCount = 2
        Me.layoutCodeReaderCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCodeReaderCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450.0!))
        Me.layoutCodeReaderCamera.Controls.Add(Me.usrStatusCodeReaderCamera, 0, 1)
        Me.layoutCodeReaderCamera.Controls.Add(Me.picCodeReaderCameraView, 0, 0)
        Me.layoutCodeReaderCamera.Controls.Add(Me.panCodeReaderCamera, 1, 0)
        Me.layoutCodeReaderCamera.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutCodeReaderCamera.Location = New System.Drawing.Point(0, 0)
        Me.layoutCodeReaderCamera.Name = "layoutCodeReaderCamera"
        Me.layoutCodeReaderCamera.RowCount = 2
        Me.layoutCodeReaderCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCodeReaderCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutCodeReaderCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutCodeReaderCamera.Size = New System.Drawing.Size(1270, 913)
        Me.layoutCodeReaderCamera.TabIndex = 10
        '
        'usrStatusCodeReaderCamera
        '
        Me.usrStatusCodeReaderCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.usrStatusCodeReaderCamera.Dock = System.Windows.Forms.DockStyle.None
        Me.usrStatusCodeReaderCamera.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrStatusCodeReaderCamera.Location = New System.Drawing.Point(0, 883)
        Me.usrStatusCodeReaderCamera.Name = "usrStatusCodeReaderCamera"
        Me.usrStatusCodeReaderCamera.Size = New System.Drawing.Size(820, 30)
        Me.usrStatusCodeReaderCamera.TabIndex = 26
        Me.usrStatusCodeReaderCamera.Text = "StatusStrip1"
        '
        'picCodeReaderCameraView
        '
        Me.picCodeReaderCameraView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picCodeReaderCameraView.DetectValueOnMouseLocation = True
        Me.picCodeReaderCameraView.Location = New System.Drawing.Point(6, 4)
        Me.picCodeReaderCameraView.Margin = New System.Windows.Forms.Padding(6, 4, 6, 4)
        Me.picCodeReaderCameraView.Name = "picCodeReaderCameraView"
        Me.picCodeReaderCameraView.Size = New System.Drawing.Size(808, 875)
        Me.picCodeReaderCameraView.TabIndex = 3
        '
        'panCodeReaderCamera
        '
        Me.panCodeReaderCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panCodeReaderCamera.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panCodeReaderCamera.Controls.Add(Me.btnCodeReader)
        Me.panCodeReaderCamera.Controls.Add(Me.rtxtCodeReaderCamera)
        Me.panCodeReaderCamera.Controls.Add(Me.btnCodeReaderCameraSaveImage)
        Me.panCodeReaderCamera.Controls.Add(Me.btnCodeReaderCameraGain)
        Me.panCodeReaderCamera.Controls.Add(Me.btnCodeReaderCameraExposure)
        Me.panCodeReaderCamera.Controls.Add(Me.txtCodeReaderCameraCurrentExpos)
        Me.panCodeReaderCamera.Controls.Add(Me.lblCodeReaderCameraExporse)
        Me.panCodeReaderCamera.Controls.Add(Me.txtCodeReaderCameraGain)
        Me.panCodeReaderCamera.Controls.Add(Me.txtCodeReaderCameraExpos)
        Me.panCodeReaderCamera.Controls.Add(Me.lblCodeReaderCameraFocus)
        Me.panCodeReaderCamera.Controls.Add(Me.txtCodeReaderCameraFocus)
        Me.panCodeReaderCamera.Controls.Add(Me.txtCodeReaderCameraCurrentGain)
        Me.panCodeReaderCamera.Controls.Add(Me.lblCodeReaderCameraMaxExpos)
        Me.panCodeReaderCamera.Controls.Add(Me.txtCodeReaderCameraMinExpos)
        Me.panCodeReaderCamera.Controls.Add(Me.lblCodeReaderCameraMinExpos)
        Me.panCodeReaderCamera.Controls.Add(Me.txtCodeReaderCameraMaxExpos)
        Me.panCodeReaderCamera.Controls.Add(Me.lblCodeReaderCameraGain)
        Me.panCodeReaderCamera.Location = New System.Drawing.Point(823, 3)
        Me.panCodeReaderCamera.Name = "panCodeReaderCamera"
        Me.layoutCodeReaderCamera.SetRowSpan(Me.panCodeReaderCamera, 2)
        Me.panCodeReaderCamera.Size = New System.Drawing.Size(444, 907)
        Me.panCodeReaderCamera.TabIndex = 11
        '
        'btnCodeReader
        '
        Me.btnCodeReader.Corners.All = 10
        Me.btnCodeReader.Corners.LowerLeft = 10
        Me.btnCodeReader.Corners.LowerRight = 10
        Me.btnCodeReader.Corners.UpperLeft = 10
        Me.btnCodeReader.Corners.UpperRight = 10
        Me.btnCodeReader.DesignerSelected = False
        Me.btnCodeReader.Font = New System.Drawing.Font("微軟正黑體", 20.25!)
        Me.btnCodeReader.ImageIndex = 0
        Me.btnCodeReader.Location = New System.Drawing.Point(3, 737)
        Me.btnCodeReader.Name = "btnCodeReader"
        Me.btnCodeReader.Size = New System.Drawing.Size(200, 100)
        Me.btnCodeReader.TabIndex = 26
        Me.btnCodeReader.Text = "讀取條碼                      Code Reader"
        '
        'rtxtCodeReaderCamera
        '
        Me.rtxtCodeReaderCamera.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtCodeReaderCamera.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtCodeReaderCamera.Location = New System.Drawing.Point(3, 428)
        Me.rtxtCodeReaderCamera.Name = "rtxtCodeReaderCamera"
        Me.rtxtCodeReaderCamera.ReadOnly = True
        Me.rtxtCodeReaderCamera.Size = New System.Drawing.Size(432, 197)
        Me.rtxtCodeReaderCamera.TabIndex = 25
        Me.rtxtCodeReaderCamera.Text = ""
        Me.rtxtCodeReaderCamera.WordWrap = False
        '
        'btnCodeReaderCameraSaveImage
        '
        Me.btnCodeReaderCameraSaveImage.Corners.All = 10
        Me.btnCodeReaderCameraSaveImage.Corners.LowerLeft = 10
        Me.btnCodeReaderCameraSaveImage.Corners.LowerRight = 10
        Me.btnCodeReaderCameraSaveImage.Corners.UpperLeft = 10
        Me.btnCodeReaderCameraSaveImage.Corners.UpperRight = 10
        Me.btnCodeReaderCameraSaveImage.DesignerSelected = False
        Me.btnCodeReaderCameraSaveImage.Font = New System.Drawing.Font("微軟正黑體", 20.25!)
        Me.btnCodeReaderCameraSaveImage.ImageIndex = 0
        Me.btnCodeReaderCameraSaveImage.Location = New System.Drawing.Point(3, 631)
        Me.btnCodeReaderCameraSaveImage.Name = "btnCodeReaderCameraSaveImage"
        Me.btnCodeReaderCameraSaveImage.Size = New System.Drawing.Size(200, 100)
        Me.btnCodeReaderCameraSaveImage.TabIndex = 23
        Me.btnCodeReaderCameraSaveImage.Text = "儲存影像                      Save Image"
        '
        'btnCodeReaderCameraGain
        '
        Me.btnCodeReaderCameraGain.Corners.All = 10
        Me.btnCodeReaderCameraGain.Corners.LowerLeft = 10
        Me.btnCodeReaderCameraGain.Corners.LowerRight = 10
        Me.btnCodeReaderCameraGain.Corners.UpperLeft = 10
        Me.btnCodeReaderCameraGain.Corners.UpperRight = 10
        Me.btnCodeReaderCameraGain.DesignerSelected = False
        Me.btnCodeReaderCameraGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnCodeReaderCameraGain.ImageIndex = 0
        Me.btnCodeReaderCameraGain.Location = New System.Drawing.Point(3, 342)
        Me.btnCodeReaderCameraGain.Name = "btnCodeReaderCameraGain"
        Me.btnCodeReaderCameraGain.Size = New System.Drawing.Size(205, 80)
        Me.btnCodeReaderCameraGain.TabIndex = 22
        Me.btnCodeReaderCameraGain.Text = "修改增益                      Gain"
        '
        'btnCodeReaderCameraExposure
        '
        Me.btnCodeReaderCameraExposure.Corners.All = 10
        Me.btnCodeReaderCameraExposure.Corners.LowerLeft = 10
        Me.btnCodeReaderCameraExposure.Corners.LowerRight = 10
        Me.btnCodeReaderCameraExposure.Corners.UpperLeft = 10
        Me.btnCodeReaderCameraExposure.Corners.UpperRight = 10
        Me.btnCodeReaderCameraExposure.DesignerSelected = False
        Me.btnCodeReaderCameraExposure.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnCodeReaderCameraExposure.ImageIndex = 0
        Me.btnCodeReaderCameraExposure.Location = New System.Drawing.Point(3, 256)
        Me.btnCodeReaderCameraExposure.Name = "btnCodeReaderCameraExposure"
        Me.btnCodeReaderCameraExposure.Size = New System.Drawing.Size(205, 80)
        Me.btnCodeReaderCameraExposure.TabIndex = 21
        Me.btnCodeReaderCameraExposure.Text = "修改曝光時間                      Eposure Time"
        '
        'txtCodeReaderCameraCurrentExpos
        '
        Me.txtCodeReaderCameraCurrentExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCodeReaderCameraCurrentExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCodeReaderCameraCurrentExpos.Location = New System.Drawing.Point(235, 158)
        Me.txtCodeReaderCameraCurrentExpos.Name = "txtCodeReaderCameraCurrentExpos"
        Me.txtCodeReaderCameraCurrentExpos.ReadOnly = True
        Me.txtCodeReaderCameraCurrentExpos.Size = New System.Drawing.Size(205, 43)
        Me.txtCodeReaderCameraCurrentExpos.TabIndex = 18
        Me.txtCodeReaderCameraCurrentExpos.Text = "0"
        '
        'lblCodeReaderCameraExporse
        '
        Me.lblCodeReaderCameraExporse.BackColor = System.Drawing.Color.Transparent
        Me.lblCodeReaderCameraExporse.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCodeReaderCameraExporse.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCodeReaderCameraExporse.Location = New System.Drawing.Point(8, 164)
        Me.lblCodeReaderCameraExporse.Name = "lblCodeReaderCameraExporse"
        Me.lblCodeReaderCameraExporse.Size = New System.Drawing.Size(221, 29)
        Me.lblCodeReaderCameraExporse.TabIndex = 17
        Me.lblCodeReaderCameraExporse.Text = "目前曝光時間"
        Me.lblCodeReaderCameraExporse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodeReaderCameraGain
        '
        Me.txtCodeReaderCameraGain.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCodeReaderCameraGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCodeReaderCameraGain.Location = New System.Drawing.Point(214, 362)
        Me.txtCodeReaderCameraGain.Name = "txtCodeReaderCameraGain"
        Me.txtCodeReaderCameraGain.Size = New System.Drawing.Size(221, 43)
        Me.txtCodeReaderCameraGain.TabIndex = 13
        Me.txtCodeReaderCameraGain.Text = "0"
        '
        'txtCodeReaderCameraExpos
        '
        Me.txtCodeReaderCameraExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCodeReaderCameraExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCodeReaderCameraExpos.Location = New System.Drawing.Point(214, 277)
        Me.txtCodeReaderCameraExpos.Name = "txtCodeReaderCameraExpos"
        Me.txtCodeReaderCameraExpos.Size = New System.Drawing.Size(226, 43)
        Me.txtCodeReaderCameraExpos.TabIndex = 12
        Me.txtCodeReaderCameraExpos.Text = "0"
        '
        'lblCodeReaderCameraFocus
        '
        Me.lblCodeReaderCameraFocus.BackColor = System.Drawing.Color.Transparent
        Me.lblCodeReaderCameraFocus.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCodeReaderCameraFocus.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCodeReaderCameraFocus.Location = New System.Drawing.Point(8, 17)
        Me.lblCodeReaderCameraFocus.Name = "lblCodeReaderCameraFocus"
        Me.lblCodeReaderCameraFocus.Size = New System.Drawing.Size(221, 29)
        Me.lblCodeReaderCameraFocus.TabIndex = 1
        Me.lblCodeReaderCameraFocus.Text = "清晰度"
        Me.lblCodeReaderCameraFocus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodeReaderCameraFocus
        '
        Me.txtCodeReaderCameraFocus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCodeReaderCameraFocus.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCodeReaderCameraFocus.Location = New System.Drawing.Point(235, 11)
        Me.txtCodeReaderCameraFocus.Name = "txtCodeReaderCameraFocus"
        Me.txtCodeReaderCameraFocus.ReadOnly = True
        Me.txtCodeReaderCameraFocus.Size = New System.Drawing.Size(205, 43)
        Me.txtCodeReaderCameraFocus.TabIndex = 0
        Me.txtCodeReaderCameraFocus.Text = "0"
        '
        'txtCodeReaderCameraCurrentGain
        '
        Me.txtCodeReaderCameraCurrentGain.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCodeReaderCameraCurrentGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCodeReaderCameraCurrentGain.Location = New System.Drawing.Point(235, 207)
        Me.txtCodeReaderCameraCurrentGain.Name = "txtCodeReaderCameraCurrentGain"
        Me.txtCodeReaderCameraCurrentGain.ReadOnly = True
        Me.txtCodeReaderCameraCurrentGain.Size = New System.Drawing.Size(205, 43)
        Me.txtCodeReaderCameraCurrentGain.TabIndex = 8
        Me.txtCodeReaderCameraCurrentGain.Text = "0"
        '
        'lblCodeReaderCameraMaxExpos
        '
        Me.lblCodeReaderCameraMaxExpos.BackColor = System.Drawing.Color.Transparent
        Me.lblCodeReaderCameraMaxExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCodeReaderCameraMaxExpos.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCodeReaderCameraMaxExpos.Location = New System.Drawing.Point(8, 66)
        Me.lblCodeReaderCameraMaxExpos.Name = "lblCodeReaderCameraMaxExpos"
        Me.lblCodeReaderCameraMaxExpos.Size = New System.Drawing.Size(221, 29)
        Me.lblCodeReaderCameraMaxExpos.TabIndex = 2
        Me.lblCodeReaderCameraMaxExpos.Text = "最大曝光時間"
        Me.lblCodeReaderCameraMaxExpos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodeReaderCameraMinExpos
        '
        Me.txtCodeReaderCameraMinExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCodeReaderCameraMinExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCodeReaderCameraMinExpos.Location = New System.Drawing.Point(235, 109)
        Me.txtCodeReaderCameraMinExpos.Name = "txtCodeReaderCameraMinExpos"
        Me.txtCodeReaderCameraMinExpos.ReadOnly = True
        Me.txtCodeReaderCameraMinExpos.Size = New System.Drawing.Size(205, 43)
        Me.txtCodeReaderCameraMinExpos.TabIndex = 7
        Me.txtCodeReaderCameraMinExpos.Text = "0"
        '
        'lblCodeReaderCameraMinExpos
        '
        Me.lblCodeReaderCameraMinExpos.BackColor = System.Drawing.Color.Transparent
        Me.lblCodeReaderCameraMinExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCodeReaderCameraMinExpos.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCodeReaderCameraMinExpos.Location = New System.Drawing.Point(8, 115)
        Me.lblCodeReaderCameraMinExpos.Name = "lblCodeReaderCameraMinExpos"
        Me.lblCodeReaderCameraMinExpos.Size = New System.Drawing.Size(221, 29)
        Me.lblCodeReaderCameraMinExpos.TabIndex = 3
        Me.lblCodeReaderCameraMinExpos.Text = "最小曝光時間"
        Me.lblCodeReaderCameraMinExpos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodeReaderCameraMaxExpos
        '
        Me.txtCodeReaderCameraMaxExpos.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCodeReaderCameraMaxExpos.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCodeReaderCameraMaxExpos.Location = New System.Drawing.Point(235, 60)
        Me.txtCodeReaderCameraMaxExpos.Name = "txtCodeReaderCameraMaxExpos"
        Me.txtCodeReaderCameraMaxExpos.ReadOnly = True
        Me.txtCodeReaderCameraMaxExpos.Size = New System.Drawing.Size(205, 43)
        Me.txtCodeReaderCameraMaxExpos.TabIndex = 6
        Me.txtCodeReaderCameraMaxExpos.Text = "0"
        '
        'lblCodeReaderCameraGain
        '
        Me.lblCodeReaderCameraGain.BackColor = System.Drawing.Color.Transparent
        Me.lblCodeReaderCameraGain.Font = New System.Drawing.Font("微軟正黑體", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblCodeReaderCameraGain.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblCodeReaderCameraGain.Location = New System.Drawing.Point(8, 213)
        Me.lblCodeReaderCameraGain.Name = "lblCodeReaderCameraGain"
        Me.lblCodeReaderCameraGain.Size = New System.Drawing.Size(221, 29)
        Me.lblCodeReaderCameraGain.TabIndex = 4
        Me.lblCodeReaderCameraGain.Text = "目前增益"
        Me.lblCodeReaderCameraGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'layoutIntegrateTest
        '
        Me.layoutIntegrateTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.layoutIntegrateTest.ColumnCount = 1
        Me.layoutIntegrateTest.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutIntegrateTest.Controls.Add(Me.flowLayoutIntegrateTest, 0, 0)
        Me.layoutIntegrateTest.Controls.Add(Me.tabIntegrateTest, 0, 1)
        Me.layoutIntegrateTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutIntegrateTest.Location = New System.Drawing.Point(0, 0)
        Me.layoutIntegrateTest.Name = "layoutIntegrateTest"
        Me.layoutIntegrateTest.RowCount = 2
        Me.layoutIntegrateTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutIntegrateTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutIntegrateTest.Size = New System.Drawing.Size(1284, 753)
        Me.layoutIntegrateTest.TabIndex = 2
        '
        'flowLayoutIntegrateTest
        '
        Me.flowLayoutIntegrateTest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flowLayoutIntegrateTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.flowLayoutIntegrateTest.Controls.Add(Me.btnQuit)
        Me.flowLayoutIntegrateTest.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.flowLayoutIntegrateTest.Location = New System.Drawing.Point(3, 3)
        Me.flowLayoutIntegrateTest.Name = "flowLayoutIntegrateTest"
        Me.flowLayoutIntegrateTest.Size = New System.Drawing.Size(1278, 44)
        Me.flowLayoutIntegrateTest.TabIndex = 0
        '
        'bkCommand
        '
        Me.bkCommand.WorkerReportsProgress = True
        Me.bkCommand.WorkerSupportsCancellation = True
        '
        'frmIntegrateTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1284, 753)
        Me.Controls.Add(Me.layoutIntegrateTest)
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIntegrateTest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "銓發科技股份有限公司 - 整合測試"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tabIntegrateTest.ResumeLayout(False)
        Me.tabIO.ResumeLayout(False)
        Me.tabHandshake.ResumeLayout(False)
        Me.layoutHandshake.ResumeLayout(False)
        Me.layoutHandshake.PerformLayout()
        CType(Me.dgvMarkInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceMarkInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigatorMarkInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigatorMarkInfo.ResumeLayout(False)
        Me.BindingNavigatorMarkInfo.PerformLayout()
        CType(Me.dgvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigatorHandshake, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigatorHandshake.ResumeLayout(False)
        Me.BindingNavigatorHandshake.PerformLayout()
        Me.tabCamera.ResumeLayout(False)
        Me.layoutCamera.ResumeLayout(False)
        Me.layoutCamera.PerformLayout()
        Me.panCamera.ResumeLayout(False)
        Me.panCamera.PerformLayout()
        Me.tabCodeReaderCamera.ResumeLayout(False)
        Me.layoutCodeReaderCamera.ResumeLayout(False)
        Me.layoutCodeReaderCamera.PerformLayout()
        Me.panCodeReaderCamera.ResumeLayout(False)
        Me.panCodeReaderCamera.PerformLayout()
        Me.layoutIntegrateTest.ResumeLayout(False)
        Me.flowLayoutIntegrateTest.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnQuit As iTVisionService.ButtonLib.CButton
    Friend WithEvents LedLTM17Initialed As iTVisionService.iTVControl.LEDLable
    Friend WithEvents LedLPClampIsoff1 As iTVisionService.iTVControl.LEDLable
    Friend WithEvents CButton40 As iTVisionService.ButtonLib.CButton
    Friend WithEvents CButton41 As iTVisionService.ButtonLib.CButton
    Friend WithEvents CButton42 As iTVisionService.ButtonLib.CButton
    Friend WithEvents CButton43 As iTVisionService.ButtonLib.CButton
    Friend WithEvents LedAsBlyM8Initialed As iTVisionService.iTVControl.LEDLable
    Friend WithEvents LedAsBlyM8On As iTVisionService.iTVControl.LEDLable
    Friend WithEvents LedLable93 As iTVisionService.iTVControl.LEDLable
    Friend WithEvents LedLable94 As iTVisionService.iTVControl.LEDLable
    Friend WithEvents CBtnJogPositon34 As iTVisionService.iTVControl.CBtnJogPositon
    Friend WithEvents bkUpdate As System.ComponentModel.BackgroundWorker
    Friend WithEvents tabIntegrateTest As iTVisionService.iTVControl.CSharpTabControl
    Private WithEvents layoutIntegrateTest As System.Windows.Forms.TableLayoutPanel
    Private WithEvents flowLayoutIntegrateTest As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents RobotMoveDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RobotMoveDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RobotMoveDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RobotPointNameDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MotorMoveDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RobotPointNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MotorMoveDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RobotPointNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MotorMoveDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bkCommand As System.ComponentModel.BackgroundWorker
    Friend WithEvents tabCamera As System.Windows.Forms.TabPage
    Friend WithEvents layoutCamera As System.Windows.Forms.TableLayoutPanel
    Public WithEvents picCameraView As iTVisionService.usrDisplay
    Friend WithEvents panCamera As System.Windows.Forms.Panel
    Friend WithEvents rtxtCamera As System.Windows.Forms.RichTextBox
    Friend WithEvents btnCameraSaveImage As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnCameraGain As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnCameraExposure As iTVisionService.ButtonLib.CButton
    Friend WithEvents txtCameraCurrentExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCameraExporse As System.Windows.Forms.Label
    Friend WithEvents txtCameraGain As System.Windows.Forms.TextBox
    Friend WithEvents txtCameraExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCameraFocus As System.Windows.Forms.Label
    Friend WithEvents txtCameraFocus As System.Windows.Forms.TextBox
    Friend WithEvents txtCameraCurrentGain As System.Windows.Forms.TextBox
    Friend WithEvents lblCameraMaxExpos As System.Windows.Forms.Label
    Friend WithEvents txtCameraMinExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCameraMinExpos As System.Windows.Forms.Label
    Friend WithEvents txtCameraMaxExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCameraGain As System.Windows.Forms.Label
    Friend WithEvents tabIO As System.Windows.Forms.TabPage
    Friend WithEvents usr8In8Out As iTVisionService.iTVControl.usrIn8Out8
    Friend WithEvents tabHandshake As System.Windows.Forms.TabPage
    Friend WithEvents layoutHandshake As System.Windows.Forms.TableLayoutPanel
    Private WithEvents pgdHandshake As System.Windows.Forms.PropertyGrid
    Friend WithEvents BindingNavigatorHandshake As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripButtonSendLotInfoACK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonRead As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonSendStripMapDownloadACK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonSendStripMapUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripTextBoxHandshakeType As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparatorHandshakeLine1St As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMarkInfo As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents pgdMarkInfo As System.Windows.Forms.PropertyGrid
    Friend WithEvents BindingSourceMarkInfo As System.Windows.Forms.BindingSource
    Friend WithEvents dgvMarkInfo As System.Windows.Forms.DataGridView
    Friend WithEvents YDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents XDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripSeparatorHandshakeLine2Nd As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButtonLoadProductConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonSaveProductConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents IsProcessDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgvProduct As System.Windows.Forms.DataGridView
    Friend WithEvents BindingSourceProduct As System.Windows.Forms.BindingSource
    Friend WithEvents ToolStripButtonLinkProductList As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonUnlinkProductList As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparatorHandshakeLine3Rd As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButtonAddProduct As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonClearProduct As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparatorHandshakeLine4Th As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents usrStatusCamera As RecipeLib.CStatusCameraForMain
    Friend WithEvents tabCodeReaderCamera As System.Windows.Forms.TabPage
    Friend WithEvents layoutCodeReaderCamera As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents usrStatusCodeReaderCamera As RecipeLib.CStatusCameraForMain
    Public WithEvents picCodeReaderCameraView As iTVisionService.usrDisplay
    Friend WithEvents panCodeReaderCamera As System.Windows.Forms.Panel
    Friend WithEvents btnCodeReader As iTVisionService.ButtonLib.CButton
    Friend WithEvents rtxtCodeReaderCamera As System.Windows.Forms.RichTextBox
    Friend WithEvents btnCodeReaderCameraSaveImage As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnCodeReaderCameraGain As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnCodeReaderCameraExposure As iTVisionService.ButtonLib.CButton
    Friend WithEvents txtCodeReaderCameraCurrentExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCodeReaderCameraExporse As System.Windows.Forms.Label
    Friend WithEvents txtCodeReaderCameraGain As System.Windows.Forms.TextBox
    Friend WithEvents txtCodeReaderCameraExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCodeReaderCameraFocus As System.Windows.Forms.Label
    Friend WithEvents txtCodeReaderCameraFocus As System.Windows.Forms.TextBox
    Friend WithEvents txtCodeReaderCameraCurrentGain As System.Windows.Forms.TextBox
    Friend WithEvents lblCodeReaderCameraMaxExpos As System.Windows.Forms.Label
    Friend WithEvents txtCodeReaderCameraMinExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCodeReaderCameraMinExpos As System.Windows.Forms.Label
    Friend WithEvents txtCodeReaderCameraMaxExpos As System.Windows.Forms.TextBox
    Friend WithEvents lblCodeReaderCameraGain As System.Windows.Forms.Label
    Friend WithEvents BinCodeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResultDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubstrateIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DimensionXDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DimensionYDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecipeIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ErrorCodeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ErrorTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnCameraCodeReader As iTVisionService.ButtonLib.CButton
End Class
