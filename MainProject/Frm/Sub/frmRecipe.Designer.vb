<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecipe
    Inherits Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipe))
        Me.picView = New iTVisionService.usrDisplay()
        Me.layoutCamera = New System.Windows.Forms.TableLayoutPanel()
        Me.tabFunction = New iTVisionService.iTVControl.CSharpTabControl()
        Me.tabRecipeMark = New System.Windows.Forms.TabPage()
        Me.layoutMark = New System.Windows.Forms.TableLayoutPanel()
        Me.pgdMain = New System.Windows.Forms.PropertyGrid()
        Me.usrRecipeMark1 = New iTVisionService.iTVControl.CBorderGroupBox()
        Me.pgdMark1 = New System.Windows.Forms.PropertyGrid()
        Me.usrRecipeMark2 = New iTVisionService.iTVControl.CBorderGroupBox()
        Me.pgdMark2 = New System.Windows.Forms.PropertyGrid()
        Me.layoutRotate = New System.Windows.Forms.TableLayoutPanel()
        Me.labRotateAngle = New System.Windows.Forms.Label()
        Me.nudRotateAngle = New System.Windows.Forms.NumericUpDown()
        Me.btnRotate = New iTVisionService.ButtonLib.CButton()
        Me.usrRecipeCode = New iTVisionService.iTVControl.CBorderGroupBox()
        Me.pgdCode = New System.Windows.Forms.PropertyGrid()
        Me.usrRecipeCode2 = New iTVisionService.iTVControl.CBorderGroupBox()
        Me.pgdCode2 = New System.Windows.Forms.PropertyGrid()
        Me.tabModelDiff = New System.Windows.Forms.TabPage()
        Me.layoutModelDiff = New System.Windows.Forms.TableLayoutPanel()
        Me.pgdModelDiff = New System.Windows.Forms.PropertyGrid()
        Me.dgvModelDiff = New System.Windows.Forms.DataGridView()
        Me.BindingNavigatorModelDiff = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripButtonFindModel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonBuildMarkPosition = New System.Windows.Forms.ToolStripButton()
        Me.labMarkPitch = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButtonMarkPitch = New System.Windows.Forms.ToolStripTextBox()
        Me.labMarkXCount = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButtonMarkXCount = New System.Windows.Forms.ToolStripTextBox()
        Me.labMarkYCount = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButtonMarkYCount = New System.Windows.Forms.ToolStripTextBox()
        Me.usrStatusCamera = New RecipeLib.CStatusCameraForMain()
        Me.pnlCommand = New System.Windows.Forms.Panel()
        Me.btnCancel = New iTVisionService.ButtonLib.CButton()
        Me.btnSave = New iTVisionService.ButtonLib.CButton()
        Me.btnQuit = New iTVisionService.ButtonLib.CButton()
        Me.PnlDisplay = New System.Windows.Forms.Panel()
        Me.mnuMark = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMark1PatternROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMark1FindModelROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMark1ClearROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMark2PatternROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMark2FindModelROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMark2ClearROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkLine1St = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCodeSearchROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCodeClearROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModelDiff = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuApplyModelRegion1St = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearModelRegion1St = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModelDiffLine1St = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuApplySearchRegion = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearSearchRegion = New System.Windows.Forms.ToolStripMenuItem()
        Me.layoutCamera.SuspendLayout()
        Me.tabFunction.SuspendLayout()
        Me.tabRecipeMark.SuspendLayout()
        Me.layoutMark.SuspendLayout()
        Me.usrRecipeMark1.SuspendLayout()
        Me.usrRecipeMark2.SuspendLayout()
        Me.layoutRotate.SuspendLayout()
        CType(Me.nudRotateAngle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.usrRecipeCode.SuspendLayout()
        Me.usrRecipeCode2.SuspendLayout()
        Me.tabModelDiff.SuspendLayout()
        Me.layoutModelDiff.SuspendLayout()
        CType(Me.dgvModelDiff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigatorModelDiff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigatorModelDiff.SuspendLayout()
        Me.pnlCommand.SuspendLayout()
        Me.PnlDisplay.SuspendLayout()
        Me.mnuMark.SuspendLayout()
        Me.mnuModelDiff.SuspendLayout()
        Me.SuspendLayout()
        '
        'picView
        '
        Me.picView.BackColor = System.Drawing.Color.Transparent
        Me.picView.DetectValueOnMouseLocation = True
        Me.picView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picView.Location = New System.Drawing.Point(0, 0)
        Me.picView.Name = "picView"
        Me.picView.Size = New System.Drawing.Size(778, 683)
        Me.picView.TabIndex = 3
        '
        'layoutCamera
        '
        Me.layoutCamera.ColumnCount = 2
        Me.layoutCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500.0!))
        Me.layoutCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCamera.Controls.Add(Me.tabFunction, 0, 1)
        Me.layoutCamera.Controls.Add(Me.usrStatusCamera, 1, 2)
        Me.layoutCamera.Controls.Add(Me.pnlCommand, 0, 0)
        Me.layoutCamera.Controls.Add(Me.PnlDisplay, 1, 0)
        Me.layoutCamera.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutCamera.Location = New System.Drawing.Point(0, 0)
        Me.layoutCamera.Margin = New System.Windows.Forms.Padding(5)
        Me.layoutCamera.Name = "layoutCamera"
        Me.layoutCamera.RowCount = 4
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutCamera.Size = New System.Drawing.Size(1284, 749)
        Me.layoutCamera.TabIndex = 4
        '
        'tabFunction
        '
        Me.tabFunction.Controls.Add(Me.tabRecipeMark)
        Me.tabFunction.Controls.Add(Me.tabModelDiff)
        Me.tabFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabFunction.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.tabFunction.ItemSize = New System.Drawing.Size(200, 20)
        Me.tabFunction.Location = New System.Drawing.Point(3, 83)
        Me.tabFunction.Name = "tabFunction"
        Me.tabFunction.Padding = New System.Drawing.Point(9, 0)
        Me.layoutCamera.SetRowSpan(Me.tabFunction, 2)
        Me.tabFunction.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabFunction.SelectedIndex = 0
        Me.tabFunction.Size = New System.Drawing.Size(494, 633)
        Me.tabFunction.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabFunction.TabIndex = 4
        Me.tabFunction.UnSelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'tabRecipeMark
        '
        Me.tabRecipeMark.BackColor = System.Drawing.Color.Transparent
        Me.tabRecipeMark.Controls.Add(Me.layoutMark)
        Me.tabRecipeMark.Location = New System.Drawing.Point(4, 24)
        Me.tabRecipeMark.Name = "tabRecipeMark"
        Me.tabRecipeMark.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRecipeMark.Size = New System.Drawing.Size(486, 605)
        Me.tabRecipeMark.TabIndex = 1
        Me.tabRecipeMark.Text = "定位參數 Locate Recipe"
        '
        'layoutMark
        '
        Me.layoutMark.ColumnCount = 1
        Me.layoutMark.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMark.Controls.Add(Me.pgdMain, 0, 1)
        Me.layoutMark.Controls.Add(Me.usrRecipeMark1, 0, 4)
        Me.layoutMark.Controls.Add(Me.usrRecipeMark2, 0, 5)
        Me.layoutMark.Controls.Add(Me.layoutRotate, 0, 0)
        Me.layoutMark.Controls.Add(Me.usrRecipeCode, 0, 2)
        Me.layoutMark.Controls.Add(Me.usrRecipeCode2, 0, 3)
        Me.layoutMark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutMark.Location = New System.Drawing.Point(3, 3)
        Me.layoutMark.Name = "layoutMark"
        Me.layoutMark.RowCount = 6
        Me.layoutMark.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.layoutMark.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.layoutMark.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143!))
        Me.layoutMark.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143!))
        Me.layoutMark.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.42857!))
        Me.layoutMark.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.42857!))
        Me.layoutMark.Size = New System.Drawing.Size(480, 599)
        Me.layoutMark.TabIndex = 0
        '
        'pgdMain
        '
        Me.pgdMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdMain.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdMain.CommandsForeColor = System.Drawing.Color.White
        Me.pgdMain.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdMain.HelpVisible = False
        Me.pgdMain.LineColor = System.Drawing.Color.Blue
        Me.pgdMain.Location = New System.Drawing.Point(3, 83)
        Me.pgdMain.Name = "pgdMain"
        Me.pgdMain.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdMain.Size = New System.Drawing.Size(474, 144)
        Me.pgdMain.TabIndex = 29
        Me.pgdMain.ToolbarVisible = False
        Me.pgdMain.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdMain.ViewForeColor = System.Drawing.Color.White
        '
        'usrRecipeMark1
        '
        Me.usrRecipeMark1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.usrRecipeMark1.BorderColor = System.Drawing.Color.White
        Me.usrRecipeMark1.Controls.Add(Me.pgdMark1)
        Me.usrRecipeMark1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrRecipeMark1.ForeColor = System.Drawing.Color.White
        Me.usrRecipeMark1.LineCount = 2
        Me.usrRecipeMark1.Location = New System.Drawing.Point(3, 443)
        Me.usrRecipeMark1.Name = "usrRecipeMark1"
        Me.usrRecipeMark1.Size = New System.Drawing.Size(474, 73)
        Me.usrRecipeMark1.TabIndex = 0
        Me.usrRecipeMark1.TabStop = False
        Me.usrRecipeMark1.Text = "Mark1"
        '
        'pgdMark1
        '
        Me.pgdMark1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdMark1.CommandsForeColor = System.Drawing.Color.White
        Me.pgdMark1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdMark1.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdMark1.HelpVisible = False
        Me.pgdMark1.LineColor = System.Drawing.Color.Blue
        Me.pgdMark1.Location = New System.Drawing.Point(3, 25)
        Me.pgdMark1.Name = "pgdMark1"
        Me.pgdMark1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdMark1.Size = New System.Drawing.Size(468, 45)
        Me.pgdMark1.TabIndex = 27
        Me.pgdMark1.ToolbarVisible = False
        Me.pgdMark1.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdMark1.ViewForeColor = System.Drawing.Color.White
        '
        'usrRecipeMark2
        '
        Me.usrRecipeMark2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.usrRecipeMark2.BorderColor = System.Drawing.Color.White
        Me.usrRecipeMark2.Controls.Add(Me.pgdMark2)
        Me.usrRecipeMark2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrRecipeMark2.ForeColor = System.Drawing.Color.White
        Me.usrRecipeMark2.LineCount = 2
        Me.usrRecipeMark2.Location = New System.Drawing.Point(3, 522)
        Me.usrRecipeMark2.Name = "usrRecipeMark2"
        Me.usrRecipeMark2.Size = New System.Drawing.Size(474, 74)
        Me.usrRecipeMark2.TabIndex = 1
        Me.usrRecipeMark2.TabStop = False
        Me.usrRecipeMark2.Text = "Mark2"
        '
        'pgdMark2
        '
        Me.pgdMark2.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdMark2.CommandsForeColor = System.Drawing.Color.White
        Me.pgdMark2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdMark2.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdMark2.HelpVisible = False
        Me.pgdMark2.LineColor = System.Drawing.Color.Blue
        Me.pgdMark2.Location = New System.Drawing.Point(3, 25)
        Me.pgdMark2.Name = "pgdMark2"
        Me.pgdMark2.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdMark2.Size = New System.Drawing.Size(468, 46)
        Me.pgdMark2.TabIndex = 28
        Me.pgdMark2.ToolbarVisible = False
        Me.pgdMark2.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdMark2.ViewForeColor = System.Drawing.Color.White
        '
        'layoutRotate
        '
        Me.layoutRotate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutRotate.ColumnCount = 4
        Me.layoutRotate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutRotate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.layoutRotate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.layoutRotate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutRotate.Controls.Add(Me.labRotateAngle, 1, 0)
        Me.layoutRotate.Controls.Add(Me.nudRotateAngle, 2, 0)
        Me.layoutRotate.Controls.Add(Me.btnRotate, 2, 1)
        Me.layoutRotate.Location = New System.Drawing.Point(3, 3)
        Me.layoutRotate.Name = "layoutRotate"
        Me.layoutRotate.RowCount = 2
        Me.layoutRotate.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutRotate.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutRotate.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutRotate.Size = New System.Drawing.Size(474, 74)
        Me.layoutRotate.TabIndex = 2
        '
        'labRotateAngle
        '
        Me.labRotateAngle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labRotateAngle.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labRotateAngle.ForeColor = System.Drawing.Color.Honeydew
        Me.labRotateAngle.Location = New System.Drawing.Point(107, 0)
        Me.labRotateAngle.Name = "labRotateAngle"
        Me.labRotateAngle.Size = New System.Drawing.Size(59, 30)
        Me.labRotateAngle.TabIndex = 51
        Me.labRotateAngle.Text = "角度："
        Me.labRotateAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nudRotateAngle
        '
        Me.nudRotateAngle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudRotateAngle.DecimalPlaces = 2
        Me.nudRotateAngle.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudRotateAngle.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nudRotateAngle.Location = New System.Drawing.Point(172, 3)
        Me.nudRotateAngle.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nudRotateAngle.Minimum = New Decimal(New Integer() {180, 0, 0, -2147483648})
        Me.nudRotateAngle.Name = "nudRotateAngle"
        Me.nudRotateAngle.Size = New System.Drawing.Size(194, 25)
        Me.nudRotateAngle.TabIndex = 52
        '
        'btnRotate
        '
        Me.btnRotate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRotate.Corners.All = 10
        Me.btnRotate.Corners.LowerLeft = 10
        Me.btnRotate.Corners.LowerRight = 10
        Me.btnRotate.Corners.UpperLeft = 10
        Me.btnRotate.Corners.UpperRight = 10
        Me.btnRotate.DesignerSelected = False
        Me.btnRotate.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnRotate.ImageIndex = 0
        Me.btnRotate.Location = New System.Drawing.Point(172, 33)
        Me.btnRotate.Name = "btnRotate"
        Me.btnRotate.Size = New System.Drawing.Size(194, 38)
        Me.btnRotate.TabIndex = 53
        Me.btnRotate.Text = "轉正"
        '
        'usrRecipeCode
        '
        Me.usrRecipeCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.usrRecipeCode.BorderColor = System.Drawing.Color.White
        Me.usrRecipeCode.Controls.Add(Me.pgdCode)
        Me.usrRecipeCode.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrRecipeCode.ForeColor = System.Drawing.Color.White
        Me.usrRecipeCode.LineCount = 2
        Me.usrRecipeCode.Location = New System.Drawing.Point(3, 233)
        Me.usrRecipeCode.Name = "usrRecipeCode"
        Me.usrRecipeCode.Size = New System.Drawing.Size(474, 99)
        Me.usrRecipeCode.TabIndex = 10
        Me.usrRecipeCode.TabStop = False
        Me.usrRecipeCode.Text = "Code1"
        '
        'pgdCode
        '
        Me.pgdCode.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdCode.CommandsForeColor = System.Drawing.Color.White
        Me.pgdCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdCode.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdCode.HelpVisible = False
        Me.pgdCode.LineColor = System.Drawing.Color.Blue
        Me.pgdCode.Location = New System.Drawing.Point(3, 25)
        Me.pgdCode.Name = "pgdCode"
        Me.pgdCode.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdCode.Size = New System.Drawing.Size(468, 71)
        Me.pgdCode.TabIndex = 30
        Me.pgdCode.ToolbarVisible = False
        Me.pgdCode.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdCode.ViewForeColor = System.Drawing.Color.White
        '
        'usrRecipeCode2
        '
        Me.usrRecipeCode2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.usrRecipeCode2.BorderColor = System.Drawing.Color.White
        Me.usrRecipeCode2.Controls.Add(Me.pgdCode2)
        Me.usrRecipeCode2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrRecipeCode2.ForeColor = System.Drawing.Color.White
        Me.usrRecipeCode2.LineCount = 2
        Me.usrRecipeCode2.Location = New System.Drawing.Point(3, 338)
        Me.usrRecipeCode2.Name = "usrRecipeCode2"
        Me.usrRecipeCode2.Size = New System.Drawing.Size(474, 99)
        Me.usrRecipeCode2.TabIndex = 10
        Me.usrRecipeCode2.TabStop = False
        Me.usrRecipeCode2.Text = "Code2"
        '
        'pgdCode2
        '
        Me.pgdCode2.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdCode2.CommandsForeColor = System.Drawing.Color.White
        Me.pgdCode2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgdCode2.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdCode2.HelpVisible = False
        Me.pgdCode2.LineColor = System.Drawing.Color.Blue
        Me.pgdCode2.Location = New System.Drawing.Point(3, 25)
        Me.pgdCode2.Name = "pgdCode2"
        Me.pgdCode2.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdCode2.Size = New System.Drawing.Size(468, 71)
        Me.pgdCode2.TabIndex = 30
        Me.pgdCode2.ToolbarVisible = False
        Me.pgdCode2.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdCode2.ViewForeColor = System.Drawing.Color.White
        '
        'tabModelDiff
        '
        Me.tabModelDiff.BackColor = System.Drawing.Color.Transparent
        Me.tabModelDiff.Controls.Add(Me.layoutModelDiff)
        Me.tabModelDiff.Location = New System.Drawing.Point(4, 24)
        Me.tabModelDiff.Name = "tabModelDiff"
        Me.tabModelDiff.Padding = New System.Windows.Forms.Padding(3)
        Me.tabModelDiff.Size = New System.Drawing.Size(486, 605)
        Me.tabModelDiff.TabIndex = 3
        Me.tabModelDiff.Text = "樣板比對 ModelDiff Recipe"
        '
        'layoutModelDiff
        '
        Me.layoutModelDiff.ColumnCount = 1
        Me.layoutModelDiff.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutModelDiff.Controls.Add(Me.pgdModelDiff, 0, 2)
        Me.layoutModelDiff.Controls.Add(Me.dgvModelDiff, 0, 1)
        Me.layoutModelDiff.Controls.Add(Me.BindingNavigatorModelDiff, 0, 0)
        Me.layoutModelDiff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutModelDiff.Location = New System.Drawing.Point(3, 3)
        Me.layoutModelDiff.Name = "layoutModelDiff"
        Me.layoutModelDiff.RowCount = 3
        Me.layoutModelDiff.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutModelDiff.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0.0!))
        Me.layoutModelDiff.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutModelDiff.Size = New System.Drawing.Size(480, 599)
        Me.layoutModelDiff.TabIndex = 1
        '
        'pgdModelDiff
        '
        Me.pgdModelDiff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdModelDiff.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdModelDiff.CommandsForeColor = System.Drawing.Color.White
        Me.pgdModelDiff.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdModelDiff.HelpVisible = False
        Me.pgdModelDiff.LineColor = System.Drawing.Color.Blue
        Me.pgdModelDiff.Location = New System.Drawing.Point(3, 33)
        Me.pgdModelDiff.Name = "pgdModelDiff"
        Me.pgdModelDiff.Size = New System.Drawing.Size(474, 563)
        Me.pgdModelDiff.TabIndex = 28
        Me.pgdModelDiff.ToolbarVisible = False
        Me.pgdModelDiff.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdModelDiff.ViewForeColor = System.Drawing.Color.White
        '
        'dgvModelDiff
        '
        Me.dgvModelDiff.AllowUserToAddRows = False
        Me.dgvModelDiff.AllowUserToDeleteRows = False
        Me.dgvModelDiff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvModelDiff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvModelDiff.Location = New System.Drawing.Point(3, 33)
        Me.dgvModelDiff.Name = "dgvModelDiff"
        Me.dgvModelDiff.ReadOnly = True
        Me.dgvModelDiff.RowHeadersVisible = False
        Me.dgvModelDiff.RowTemplate.Height = 24
        Me.dgvModelDiff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvModelDiff.Size = New System.Drawing.Size(474, 1)
        Me.dgvModelDiff.TabIndex = 29
        '
        'BindingNavigatorModelDiff
        '
        Me.BindingNavigatorModelDiff.AddNewItem = Nothing
        Me.BindingNavigatorModelDiff.CountItem = Nothing
        Me.BindingNavigatorModelDiff.DeleteItem = Nothing
        Me.BindingNavigatorModelDiff.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonFindModel, Me.ToolStripButtonBuildMarkPosition, Me.labMarkPitch, Me.ToolStripButtonMarkPitch, Me.labMarkXCount, Me.ToolStripButtonMarkXCount, Me.labMarkYCount, Me.ToolStripButtonMarkYCount})
        Me.BindingNavigatorModelDiff.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigatorModelDiff.MoveFirstItem = Nothing
        Me.BindingNavigatorModelDiff.MoveLastItem = Nothing
        Me.BindingNavigatorModelDiff.MoveNextItem = Nothing
        Me.BindingNavigatorModelDiff.MovePreviousItem = Nothing
        Me.BindingNavigatorModelDiff.Name = "BindingNavigatorModelDiff"
        Me.BindingNavigatorModelDiff.PositionItem = Nothing
        Me.BindingNavigatorModelDiff.Size = New System.Drawing.Size(480, 28)
        Me.BindingNavigatorModelDiff.TabIndex = 30
        Me.BindingNavigatorModelDiff.Text = "BindingNavigator1"
        '
        'ToolStripButtonFindModel
        '
        Me.ToolStripButtonFindModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFindModel.Image = CType(resources.GetObject("ToolStripButtonFindModel.Image"), System.Drawing.Image)
        Me.ToolStripButtonFindModel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFindModel.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonFindModel.Name = "ToolStripButtonFindModel"
        Me.ToolStripButtonFindModel.Size = New System.Drawing.Size(23, 25)
        Me.ToolStripButtonFindModel.Text = "尋找樣本"
        '
        'ToolStripButtonBuildMarkPosition
        '
        Me.ToolStripButtonBuildMarkPosition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonBuildMarkPosition.Image = CType(resources.GetObject("ToolStripButtonBuildMarkPosition.Image"), System.Drawing.Image)
        Me.ToolStripButtonBuildMarkPosition.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonBuildMarkPosition.Margin = New System.Windows.Forms.Padding(10, 1, 10, 2)
        Me.ToolStripButtonBuildMarkPosition.Name = "ToolStripButtonBuildMarkPosition"
        Me.ToolStripButtonBuildMarkPosition.Size = New System.Drawing.Size(23, 25)
        Me.ToolStripButtonBuildMarkPosition.Text = "創建標記位置"
        '
        'labMarkPitch
        '
        Me.labMarkPitch.Name = "labMarkPitch"
        Me.labMarkPitch.Size = New System.Drawing.Size(67, 25)
        Me.labMarkPitch.Text = "雷刻間距："
        '
        'ToolStripButtonMarkPitch
        '
        Me.ToolStripButtonMarkPitch.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripButtonMarkPitch.Margin = New System.Windows.Forms.Padding(0, 1, 10, 2)
        Me.ToolStripButtonMarkPitch.Name = "ToolStripButtonMarkPitch"
        Me.ToolStripButtonMarkPitch.Size = New System.Drawing.Size(50, 25)
        Me.ToolStripButtonMarkPitch.Text = "60"
        Me.ToolStripButtonMarkPitch.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolStripButtonMarkPitch.ToolTipText = "雷刻間距"
        '
        'labMarkXCount
        '
        Me.labMarkXCount.Name = "labMarkXCount"
        Me.labMarkXCount.Size = New System.Drawing.Size(78, 25)
        Me.labMarkXCount.Text = "X 標記數量："
        '
        'ToolStripButtonMarkXCount
        '
        Me.ToolStripButtonMarkXCount.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripButtonMarkXCount.Margin = New System.Windows.Forms.Padding(0, 1, 10, 2)
        Me.ToolStripButtonMarkXCount.Name = "ToolStripButtonMarkXCount"
        Me.ToolStripButtonMarkXCount.Size = New System.Drawing.Size(40, 25)
        Me.ToolStripButtonMarkXCount.Text = "0"
        Me.ToolStripButtonMarkXCount.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'labMarkYCount
        '
        Me.labMarkYCount.Name = "labMarkYCount"
        Me.labMarkYCount.Size = New System.Drawing.Size(77, 25)
        Me.labMarkYCount.Text = "Y 標記數量："
        '
        'ToolStripButtonMarkYCount
        '
        Me.ToolStripButtonMarkYCount.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ToolStripButtonMarkYCount.Margin = New System.Windows.Forms.Padding(0, 1, 10, 2)
        Me.ToolStripButtonMarkYCount.Name = "ToolStripButtonMarkYCount"
        Me.ToolStripButtonMarkYCount.Size = New System.Drawing.Size(40, 25)
        Me.ToolStripButtonMarkYCount.Text = "0"
        Me.ToolStripButtonMarkYCount.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'usrStatusCamera
        '
        Me.usrStatusCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.usrStatusCamera.Dock = System.Windows.Forms.DockStyle.None
        Me.usrStatusCamera.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrStatusCamera.Location = New System.Drawing.Point(500, 689)
        Me.usrStatusCamera.Name = "usrStatusCamera"
        Me.usrStatusCamera.Size = New System.Drawing.Size(784, 30)
        Me.usrStatusCamera.TabIndex = 1
        Me.usrStatusCamera.Text = "StatusStrip1"
        '
        'pnlCommand
        '
        Me.pnlCommand.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlCommand.Controls.Add(Me.btnCancel)
        Me.pnlCommand.Controls.Add(Me.btnSave)
        Me.pnlCommand.Controls.Add(Me.btnQuit)
        Me.pnlCommand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCommand.Location = New System.Drawing.Point(3, 3)
        Me.pnlCommand.Name = "pnlCommand"
        Me.pnlCommand.Size = New System.Drawing.Size(494, 74)
        Me.pnlCommand.TabIndex = 5
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.Corners.All = 10
        Me.btnCancel.Corners.LowerLeft = 10
        Me.btnCancel.Corners.LowerRight = 10
        Me.btnCancel.Corners.UpperLeft = 10
        Me.btnCancel.Corners.UpperRight = 10
        Me.btnCancel.DesignerSelected = False
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.Location = New System.Drawing.Point(235, 9)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(107, 62)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "取消       Cancel"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.Transparent
        Me.btnSave.Corners.All = 10
        Me.btnSave.Corners.LowerLeft = 10
        Me.btnSave.Corners.LowerRight = 10
        Me.btnSave.Corners.UpperLeft = 10
        Me.btnSave.Corners.UpperRight = 10
        Me.btnSave.DesignerSelected = False
        Me.btnSave.ImageIndex = 0
        Me.btnSave.Location = New System.Drawing.Point(9, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 62)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "儲存參數       Save Recpe"
        '
        'btnQuit
        '
        Me.btnQuit.BackColor = System.Drawing.Color.Transparent
        Me.btnQuit.Corners.All = 10
        Me.btnQuit.Corners.LowerLeft = 10
        Me.btnQuit.Corners.LowerRight = 10
        Me.btnQuit.Corners.UpperLeft = 10
        Me.btnQuit.Corners.UpperRight = 10
        Me.btnQuit.DesignerSelected = False
        Me.btnQuit.ImageIndex = 0
        Me.btnQuit.Location = New System.Drawing.Point(122, 9)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(107, 62)
        Me.btnQuit.TabIndex = 7
        Me.btnQuit.Text = "載入並離開 Quit"
        '
        'PnlDisplay
        '
        Me.PnlDisplay.BackColor = System.Drawing.Color.Transparent
        Me.PnlDisplay.Controls.Add(Me.picView)
        Me.PnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlDisplay.Location = New System.Drawing.Point(503, 3)
        Me.PnlDisplay.Name = "PnlDisplay"
        Me.layoutCamera.SetRowSpan(Me.PnlDisplay, 2)
        Me.PnlDisplay.Size = New System.Drawing.Size(778, 683)
        Me.PnlDisplay.TabIndex = 6
        '
        'mnuMark
        '
        Me.mnuMark.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuMark.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMark1PatternROI, Me.mnuMark1FindModelROI, Me.mnuMark1ClearROI, Me.mnuMark2PatternROI, Me.mnuMark2FindModelROI, Me.mnuMark2ClearROI, Me.mnuMarkLine1St, Me.mnuCodeSearchROI, Me.mnuCodeClearROI})
        Me.mnuMark.Name = "MenuDisplay"
        Me.mnuMark.Size = New System.Drawing.Size(242, 202)
        '
        'mnuMark1PatternROI
        '
        Me.mnuMark1PatternROI.Name = "mnuMark1PatternROI"
        Me.mnuMark1PatternROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuMark1PatternROI.Text = "Mark 1 - 樣本 ROI"
        '
        'mnuMark1FindModelROI
        '
        Me.mnuMark1FindModelROI.Name = "mnuMark1FindModelROI"
        Me.mnuMark1FindModelROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuMark1FindModelROI.Text = "Mark 1 - 尋找範圍 ROI"
        '
        'mnuMark1ClearROI
        '
        Me.mnuMark1ClearROI.Name = "mnuMark1ClearROI"
        Me.mnuMark1ClearROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuMark1ClearROI.Text = "Mark 1 - 清除 ROI"
        '
        'mnuMark2PatternROI
        '
        Me.mnuMark2PatternROI.Name = "mnuMark2PatternROI"
        Me.mnuMark2PatternROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuMark2PatternROI.Text = "Mark 2 - 樣本 ROI"
        '
        'mnuMark2FindModelROI
        '
        Me.mnuMark2FindModelROI.Name = "mnuMark2FindModelROI"
        Me.mnuMark2FindModelROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuMark2FindModelROI.Text = "Mark 2 - 尋找範圍 ROI"
        '
        'mnuMark2ClearROI
        '
        Me.mnuMark2ClearROI.Name = "mnuMark2ClearROI"
        Me.mnuMark2ClearROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuMark2ClearROI.Text = "Mark 2 - 清除 ROI"
        '
        'mnuMarkLine1St
        '
        Me.mnuMarkLine1St.Name = "mnuMarkLine1St"
        Me.mnuMarkLine1St.Size = New System.Drawing.Size(238, 6)
        '
        'mnuCodeSearchROI
        '
        Me.mnuCodeSearchROI.Name = "mnuCodeSearchROI"
        Me.mnuCodeSearchROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuCodeSearchROI.Text = "Code - 尋找範圍 ROI"
        '
        'mnuCodeClearROI
        '
        Me.mnuCodeClearROI.Name = "mnuCodeClearROI"
        Me.mnuCodeClearROI.Size = New System.Drawing.Size(241, 24)
        Me.mnuCodeClearROI.Text = "Code - 清除 ROI"
        '
        'mnuModelDiff
        '
        Me.mnuModelDiff.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuModelDiff.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuApplyModelRegion1St, Me.mnuClearModelRegion1St, Me.mnuModelDiffLine1St, Me.mnuApplySearchRegion, Me.mnuClearSearchRegion})
        Me.mnuModelDiff.Name = "MenuCompareLib"
        Me.mnuModelDiff.Size = New System.Drawing.Size(175, 106)
        '
        'mnuApplyModelRegion1St
        '
        Me.mnuApplyModelRegion1St.Name = "mnuApplyModelRegion1St"
        Me.mnuApplyModelRegion1St.Size = New System.Drawing.Size(174, 24)
        Me.mnuApplyModelRegion1St.Text = "套用樣板區域"
        '
        'mnuClearModelRegion1St
        '
        Me.mnuClearModelRegion1St.Name = "mnuClearModelRegion1St"
        Me.mnuClearModelRegion1St.Size = New System.Drawing.Size(174, 24)
        Me.mnuClearModelRegion1St.Text = "清除樣板區域"
        '
        'mnuModelDiffLine1St
        '
        Me.mnuModelDiffLine1St.Name = "mnuModelDiffLine1St"
        Me.mnuModelDiffLine1St.Size = New System.Drawing.Size(171, 6)
        '
        'mnuApplySearchRegion
        '
        Me.mnuApplySearchRegion.Name = "mnuApplySearchRegion"
        Me.mnuApplySearchRegion.Size = New System.Drawing.Size(174, 24)
        Me.mnuApplySearchRegion.Text = "套用比對區域"
        '
        'mnuClearSearchRegion
        '
        Me.mnuClearSearchRegion.Name = "mnuClearSearchRegion"
        Me.mnuClearSearchRegion.Size = New System.Drawing.Size(174, 24)
        Me.mnuClearSearchRegion.Text = "清除比對區域"
        '
        'frmRecipe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1284, 749)
        Me.Controls.Add(Me.layoutCamera)
        Me.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRecipe"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "銓發科技股份有限公司 - 參數設定"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.layoutCamera.ResumeLayout(False)
        Me.layoutCamera.PerformLayout()
        Me.tabFunction.ResumeLayout(False)
        Me.tabRecipeMark.ResumeLayout(False)
        Me.layoutMark.ResumeLayout(False)
        Me.usrRecipeMark1.ResumeLayout(False)
        Me.usrRecipeMark2.ResumeLayout(False)
        Me.layoutRotate.ResumeLayout(False)
        CType(Me.nudRotateAngle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.usrRecipeCode.ResumeLayout(False)
        Me.usrRecipeCode2.ResumeLayout(False)
        Me.tabModelDiff.ResumeLayout(False)
        Me.layoutModelDiff.ResumeLayout(False)
        Me.layoutModelDiff.PerformLayout()
        CType(Me.dgvModelDiff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigatorModelDiff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigatorModelDiff.ResumeLayout(False)
        Me.BindingNavigatorModelDiff.PerformLayout()
        Me.pnlCommand.ResumeLayout(False)
        Me.PnlDisplay.ResumeLayout(False)
        Me.mnuMark.ResumeLayout(False)
        Me.mnuModelDiff.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picView As iTVisionService.usrDisplay
    Friend WithEvents layoutCamera As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tabFunction As iTVisionService.iTVControl.CSharpTabControl
    Friend WithEvents tabRecipeMark As System.Windows.Forms.TabPage
    Friend WithEvents pnlCommand As System.Windows.Forms.Panel
    Private WithEvents btnSave As iTVisionService.ButtonLib.CButton
    Private WithEvents btnQuit As iTVisionService.ButtonLib.CButton
    Friend WithEvents layoutMark As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents usrRecipeMark1 As iTVisionService.iTVControl.CBorderGroupBox
    Friend WithEvents pgdMark1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents mnuMark As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents usrRecipeMark2 As iTVisionService.iTVControl.CBorderGroupBox
    Friend WithEvents pgdMark2 As System.Windows.Forms.PropertyGrid
    Friend WithEvents LineCountDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefectSizeRangeMaxDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AverageGrey1stDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AverageGrey2ndDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SearchIndentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NeedSearchDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents usrStatusCamera As RecipeLib.CStatusCameraForMain
    Friend WithEvents tabModelDiff As System.Windows.Forms.TabPage
    Friend WithEvents layoutModelDiff As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pgdModelDiff As System.Windows.Forms.PropertyGrid
    Friend WithEvents dgvModelDiff As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TopLeftDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModelSizeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SearchRangeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefectSizeMinDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents mnuModelDiff As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuApplyModelRegion1St As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClearModelRegion1St As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HorZoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HorZoneDirectionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HorZoneIndexDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VerZoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VerZoneDirectionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VerZoneIndexDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefXOfAlignMarkDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefYOfAlignMarkDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuMark1PatternROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMark1FindModelROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMark2PatternROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMark2FindModelROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMark1ClearROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMark2ClearROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TopLeftDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EdgeToleranceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiffTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DarkThresholdDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BrightThresholdDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DarkEdgeToleranceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BrightEdgeToleranceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsGatherStandardDeviationDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents IsUseNormalizeDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NormalizeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TopLeftDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents btnCancel As iTVisionService.ButtonLib.CButton
    Friend WithEvents layoutRotate As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents labRotateAngle As System.Windows.Forms.Label
    Friend WithEvents nudRotateAngle As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnRotate As iTVisionService.ButtonLib.CButton
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TopLeftDataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DarkThresholdDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BrightThresholdDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ModelDiffTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TopLeftDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DarkThresholdDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BrightThresholdDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents InspectSummationSquareCountDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TopLeftDataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuModelDiffLine1St As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuApplySearchRegion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClearSearchRegion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pgdMain As System.Windows.Forms.PropertyGrid
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModelScoreDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DarkThresholdDataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BrightThresholdDataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaseDarkThresholdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaseBrightThresholdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn5 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModelTopLeft1StDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModelTopLeft2NdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModelTopLeft3RdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn6 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents BindingNavigatorModelDiff As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripButtonFindModel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonBuildMarkPosition As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonMarkPitch As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents pgdCode As System.Windows.Forms.PropertyGrid
    Friend WithEvents mnuMarkLine1St As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCodeSearchROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCodeClearROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents usrRecipeCode As iTVisionService.iTVControl.CBorderGroupBox
    Friend WithEvents labMarkPitch As System.Windows.Forms.ToolStripLabel
    Friend WithEvents labMarkXCount As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButtonMarkXCount As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripButtonMarkYCount As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents labMarkYCount As System.Windows.Forms.ToolStripLabel
    Friend WithEvents usrRecipeCode2 As iTVisionService.iTVControl.CBorderGroupBox
    Friend WithEvents pgdCode2 As System.Windows.Forms.PropertyGrid
End Class
