<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.bkUpdate = New System.ComponentModel.BackgroundWorker()
        Me.bkTime = New System.ComponentModel.BackgroundWorker()
        Me.menuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuHardwareConfig = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRecipe = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRecipeInspect = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRecipeWaferMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRecipeCodeReader = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUnitTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLogInOut = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRight = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabViewControl = New iTVisionService.iTVControl.CSharpTabControl()
        Me.TabMap = New System.Windows.Forms.TabPage()
        Me.layoutMap = New System.Windows.Forms.TableLayoutPanel()
        Me.mvwMapView = New iTVisionService.DisplayLib.usrMapView()
        Me.dlvMapDieList = New iTVisionService.DisplayLib.usrDieListView()
        Me.dlvMapDefectList = New iTVisionService.DisplayLib.usrDefectListView()
        Me.tabMain = New System.Windows.Forms.TabPage()
        Me.layoutView = New System.Windows.Forms.TableLayoutPanel()
        Me.labModelCount = New System.Windows.Forms.Label()
        Me.tabView = New iTVisionService.iTVControl.CSharpTabControl()
        Me.tabResult = New System.Windows.Forms.TabPage()
        Me.usrDefectView = New DefectLib.usrDefectView()
        Me.tabLocate = New System.Windows.Forms.TabPage()
        Me.layoutLocate = New System.Windows.Forms.TableLayoutPanel()
        Me.picLocate2 = New iTVisionService.usrDisplay()
        Me.picLocate1 = New iTVisionService.usrDisplay()
        Me.labGray = New System.Windows.Forms.Label()
        Me.picView = New iTVisionService.usrDisplay()
        Me.tabLog = New System.Windows.Forms.TabPage()
        Me.tabLogView = New iTVisionService.iTVControl.CSharpTabControl()
        Me.tabSystemLog = New System.Windows.Forms.TabPage()
        Me.rtxtSystem = New System.Windows.Forms.RichTextBox()
        Me.tabProcessLog = New System.Windows.Forms.TabPage()
        Me.rtxtProcess = New System.Windows.Forms.RichTextBox()
        Me.tabControlLog = New System.Windows.Forms.TabPage()
        Me.rtxtControl = New System.Windows.Forms.RichTextBox()
        Me.tabAlarmLog = New System.Windows.Forms.TabPage()
        Me.rtxtAlarm = New System.Windows.Forms.RichTextBox()
        Me.tabHandshakeLog = New System.Windows.Forms.TabPage()
        Me.rtxtHandshake = New System.Windows.Forms.RichTextBox()
        Me.tabManualLog = New System.Windows.Forms.TabPage()
        Me.rtxtManual = New System.Windows.Forms.RichTextBox()
        Me.tabHistory = New System.Windows.Forms.TabPage()
        Me.layoutHistory = New System.Windows.Forms.TableLayoutPanel()
        Me.lstInspectHistory = New DefectLib.CDefectViewCamera()
        Me.btnOpenHistory = New iTVisionService.ButtonLib.CButton()
        Me.gbxInformation = New iTVisionService.iTVControl.CBorderGroupBox()
        Me.CButton1 = New iTVisionService.ButtonLib.CButton()
        Me.UsrDisplay2 = New iTVisionService.usrDisplay()
        Me.labStatusTime = New System.Windows.Forms.Label()
        Me.labStatusDate = New System.Windows.Forms.Label()
        Me.labRecipeID = New System.Windows.Forms.Label()
        Me.usrExecute = New iTVisionService.iTVControl.usrConnectStatus()
        Me.btnClearAlarm = New iTVisionService.ButtonLib.CButton()
        Me.usrRunning = New iTVisionService.iTVControl.usrConnectStatus()
        Me.usrAlarm = New iTVisionService.iTVControl.usrConnectStatus()
        Me.btnSingleRun = New iTVisionService.ButtonLib.CButton()
        Me.btnContinusRun = New iTVisionService.ButtonLib.CButton()
        Me.btnTestRun = New iTVisionService.ButtonLib.CButton()
        Me.btnStop = New iTVisionService.ButtonLib.CButton()
        Me.btnRecipeManager = New iTVisionService.ButtonLib.CButton()
        Me.btnChangeModel = New iTVisionService.ButtonLib.CButton()
        Me.cbxGatherStandardDeviation = New System.Windows.Forms.CheckBox()
        Me.cbxIsAutoChangeModel = New System.Windows.Forms.CheckBox()
        Me.layoutLoadImage = New System.Windows.Forms.TableLayoutPanel()
        Me.tbxLoadImage = New System.Windows.Forms.TextBox()
        Me.btnLoadImage = New iTVisionService.ButtonLib.CButton()
        Me.dgvProduct = New System.Windows.Forms.DataGridView()
        Me.LotIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubstrateIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSourceProduct = New System.Windows.Forms.BindingSource(Me.components)
        Me.layoutMain = New System.Windows.Forms.TableLayoutPanel()
        Me.layoutMainControl = New System.Windows.Forms.TableLayoutPanel()
        Me.labYield = New System.Windows.Forms.Label()
        Me.labProductCount = New System.Windows.Forms.Label()
        Me.dgvCodeReadResult = New System.Windows.Forms.DataGridView()
        Me.mnuDrawImage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDrawRecipe = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawDefect = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawModel = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawModelCenter = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawImageLine1St = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSaveImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaveStandardDeviationModelImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadStandardDeviationModelImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearStandardDeviationModelImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProduct = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuClearProduct = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProductLine1St = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuClearAllProduct = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.menuMain.SuspendLayout
        Me.tabViewControl.SuspendLayout
        Me.TabMap.SuspendLayout
        Me.layoutMap.SuspendLayout
        Me.tabMain.SuspendLayout
        Me.layoutView.SuspendLayout
        Me.tabView.SuspendLayout
        Me.tabResult.SuspendLayout
        Me.tabLocate.SuspendLayout
        Me.layoutLocate.SuspendLayout
        Me.tabLog.SuspendLayout
        Me.tabLogView.SuspendLayout
        Me.tabSystemLog.SuspendLayout
        Me.tabProcessLog.SuspendLayout
        Me.tabControlLog.SuspendLayout
        Me.tabAlarmLog.SuspendLayout
        Me.tabHandshakeLog.SuspendLayout
        Me.tabManualLog.SuspendLayout
        Me.tabHistory.SuspendLayout
        Me.layoutHistory.SuspendLayout
        Me.gbxInformation.SuspendLayout
        Me.layoutLoadImage.SuspendLayout
        CType(Me.dgvProduct,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BindingSourceProduct,System.ComponentModel.ISupportInitialize).BeginInit
        Me.layoutMain.SuspendLayout
        Me.layoutMainControl.SuspendLayout
        CType(Me.dgvCodeReadResult,System.ComponentModel.ISupportInitialize).BeginInit
        Me.mnuDrawImage.SuspendLayout
        Me.mnuProduct.SuspendLayout
        Me.SuspendLayout
        '
        'bkUpdate
        '
        Me.bkUpdate.WorkerReportsProgress = true
        Me.bkUpdate.WorkerSupportsCancellation = true
        '
        'bkTime
        '
        Me.bkTime.WorkerReportsProgress = true
        Me.bkTime.WorkerSupportsCancellation = true
        '
        'menuMain
        '
        Me.menuMain.Font = New System.Drawing.Font("微軟正黑體", 12!)
        Me.menuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHardwareConfig, Me.mnuRecipe, Me.mnuRecipeCodeReader, Me.mnuUnitTest, Me.mnuUser, Me.mnuVersion})
        Me.menuMain.Location = New System.Drawing.Point(0, 0)
        Me.menuMain.Name = "menuMain"
        Me.menuMain.Size = New System.Drawing.Size(935, 28)
        Me.menuMain.TabIndex = 28
        Me.menuMain.Text = "MenuMain"
        '
        'mnuHardwareConfig
        '
        Me.mnuHardwareConfig.Name = "mnuHardwareConfig"
        Me.mnuHardwareConfig.Size = New System.Drawing.Size(176, 24)
        Me.mnuHardwareConfig.Text = "00 - 系統設定 System"
        '
        'mnuRecipe
        '
        Me.mnuRecipe.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRecipeInspect, Me.mnuRecipeWaferMap})
        Me.mnuRecipe.Name = "mnuRecipe"
        Me.mnuRecipe.Size = New System.Drawing.Size(172, 24)
        Me.mnuRecipe.Text = "10 - 參數設定 Recipe"
        '
        'mnuRecipeInspect
        '
        Me.mnuRecipeInspect.Name = "mnuRecipeInspect"
        Me.mnuRecipeInspect.Size = New System.Drawing.Size(262, 24)
        Me.mnuRecipeInspect.Text = "11 - 參數設定 Inspect"
        '
        'mnuRecipeWaferMap
        '
        Me.mnuRecipeWaferMap.Name = "mnuRecipeWaferMap"
        Me.mnuRecipeWaferMap.Size = New System.Drawing.Size(262, 24)
        Me.mnuRecipeWaferMap.Text = "12 - 參數設定 Wafer Map"
        '
        'mnuRecipeCodeReader
        '
        Me.mnuRecipeCodeReader.Name = "mnuRecipeCodeReader"
        Me.mnuRecipeCodeReader.Size = New System.Drawing.Size(194, 24)
        Me.mnuRecipeCodeReader.Text = "20 - 條碼參數設定 Code"
        '
        'mnuUnitTest
        '
        Me.mnuUnitTest.Name = "mnuUnitTest"
        Me.mnuUnitTest.Size = New System.Drawing.Size(153, 24)
        Me.mnuUnitTest.Text = "30 - 單元測試 Test"
        '
        'mnuUser
        '
        Me.mnuUser.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLogInOut, Me.mnuRight})
        Me.mnuUser.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.mnuUser.Name = "mnuUser"
        Me.mnuUser.Size = New System.Drawing.Size(102, 24)
        Me.mnuUser.Text = "40 - 使用者"
        '
        'mnuLogInOut
        '
        Me.mnuLogInOut.BackColor = System.Drawing.SystemColors.Control
        Me.mnuLogInOut.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuLogInOut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.mnuLogInOut.Name = "mnuLogInOut"
        Me.mnuLogInOut.Size = New System.Drawing.Size(301, 24)
        Me.mnuLogInOut.Text = "41 - 使用者登入 User Log In"
        '
        'mnuRight
        '
        Me.mnuRight.BackColor = System.Drawing.SystemColors.Control
        Me.mnuRight.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuRight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.mnuRight.Name = "mnuRight"
        Me.mnuRight.Size = New System.Drawing.Size(301, 24)
        Me.mnuRight.Text = "42 - 使用者管理 User Manager"
        '
        'mnuVersion
        '
        Me.mnuVersion.Name = "mnuVersion"
        Me.mnuVersion.Size = New System.Drawing.Size(179, 24)
        Me.mnuVersion.Text = "90 - 版本資訊 Version"
        '
        'tabViewControl
        '
        Me.tabViewControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabViewControl.Controls.Add(Me.TabMap)
        Me.tabViewControl.Controls.Add(Me.tabMain)
        Me.tabViewControl.Controls.Add(Me.tabLog)
        Me.tabViewControl.Controls.Add(Me.tabHistory)
        Me.tabViewControl.ItemSize = New System.Drawing.Size(200, 24)
        Me.tabViewControl.Location = New System.Drawing.Point(3, 3)
        Me.tabViewControl.Name = "tabViewControl"
        Me.tabViewControl.Padding = New System.Drawing.Point(9, 0)
        Me.tabViewControl.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabViewControl.SelectedIndex = 0
        Me.tabViewControl.Size = New System.Drawing.Size(579, 715)
        Me.tabViewControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabViewControl.TabIndex = 29
        Me.tabViewControl.UnSelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'TabMap
        '
        Me.TabMap.Controls.Add(Me.layoutMap)
        Me.TabMap.Location = New System.Drawing.Point(4, 28)
        Me.TabMap.Name = "TabMap"
        Me.TabMap.Padding = New System.Windows.Forms.Padding(3)
        Me.TabMap.Size = New System.Drawing.Size(571, 683)
        Me.TabMap.TabIndex = 6
        Me.TabMap.Text = "Map"
        Me.TabMap.UseVisualStyleBackColor = True
        '
        'layoutMap
        '
        Me.layoutMap.BackColor = System.Drawing.Color.Transparent
        Me.layoutMap.ColumnCount = 2
        Me.layoutMap.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutMap.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutMap.Controls.Add(Me.mvwMapView, 0, 0)
        Me.layoutMap.Controls.Add(Me.dlvMapDieList, 0, 1)
        Me.layoutMap.Controls.Add(Me.dlvMapDefectList, 1, 1)
        Me.layoutMap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutMap.Location = New System.Drawing.Point(3, 3)
        Me.layoutMap.Name = "layoutMap"
        Me.layoutMap.RowCount = 2
        Me.layoutMap.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.layoutMap.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.layoutMap.Size = New System.Drawing.Size(565, 677)
        Me.layoutMap.TabIndex = 0
        '
        'mvwMapView
        '
        Me.mvwMapView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mvwMapView.BackColor = System.Drawing.Color.Transparent
        Me.mvwMapView.CircleIndentation = 0.5R
        Me.layoutMap.SetColumnSpan(Me.mvwMapView, 2)
        Me.mvwMapView.Font = New System.Drawing.Font("微軟正黑體", 9.0!)
        Me.mvwMapView.IsDrawMapCircle = False
        Me.mvwMapView.IsDrawMapIndex = True
        Me.mvwMapView.IsDrawNGDie = True
        Me.mvwMapView.IsDrawNGFeature = False
        Me.mvwMapView.IsReverseMapIndexColumn = False
        Me.mvwMapView.IsReverseMapIndexRow = False
        Me.mvwMapView.IsViewDefectImage = False
        Me.mvwMapView.IsViewMapInformation = True
        Me.mvwMapView.Location = New System.Drawing.Point(0, 0)
        Me.mvwMapView.MapCircleColor = System.Drawing.Color.Thistle
        Me.mvwMapView.MapDefectCircleColor = System.Drawing.Color.MediumPurple
        Me.mvwMapView.MapDieRectangleColor = System.Drawing.Color.Blue
        Me.mvwMapView.MapImageContextMenuStrip = Nothing
        Me.mvwMapView.MapInformationFont = New System.Drawing.Font("微軟正黑體", 9.0!)
        Me.mvwMapView.MapInformationText = ""
        Me.mvwMapView.MapSelectedDefectCircleColor = System.Drawing.Color.Purple
        Me.mvwMapView.MapSelectedDefectColor = System.Drawing.Color.Maroon
        Me.mvwMapView.MapSelectedDieColor = System.Drawing.Color.Yellow
        Me.mvwMapView.Margin = New System.Windows.Forms.Padding(0)
        Me.mvwMapView.Name = "mvwMapView"
        Me.mvwMapView.Size = New System.Drawing.Size(565, 372)
        Me.mvwMapView.SourceImageContextMenuStrip = Nothing
        Me.mvwMapView.TabIndex = 0
        '
        'dlvMapDieList
        '
        Me.dlvMapDieList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dlvMapDieList.BackColor = System.Drawing.Color.Transparent
        Me.dlvMapDieList.DieListContextMenuStrip = Nothing
        Me.dlvMapDieList.DieListViewType = CType(iTVisionService.DisplayLib.DieListViewType.Type2Vertical, iTVisionService.DisplayLib.DieListViewType)
        Me.dlvMapDieList.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dlvMapDieList.IsViewDefectImage = False
        Me.dlvMapDieList.IsViewProperty = True
        Me.dlvMapDieList.Location = New System.Drawing.Point(0, 372)
        Me.dlvMapDieList.Margin = New System.Windows.Forms.Padding(0)
        Me.dlvMapDieList.Name = "dlvMapDieList"
        Me.dlvMapDieList.Size = New System.Drawing.Size(282, 305)
        Me.dlvMapDieList.TabIndex = 50
        '
        'dlvMapDefectList
        '
        Me.dlvMapDefectList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dlvMapDefectList.BackColor = System.Drawing.Color.Transparent
        Me.dlvMapDefectList.DefectListContextMenuStrip = Nothing
        Me.dlvMapDefectList.DefectListViewType = CType(iTVisionService.DisplayLib.DefectListViewType.Type2Vertical, iTVisionService.DisplayLib.DefectListViewType)
        Me.dlvMapDefectList.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dlvMapDefectList.IsTemporary = False
        Me.dlvMapDefectList.IsViewDefectImage = False
        Me.dlvMapDefectList.IsViewProperty = True
        Me.dlvMapDefectList.Location = New System.Drawing.Point(282, 372)
        Me.dlvMapDefectList.Margin = New System.Windows.Forms.Padding(0)
        Me.dlvMapDefectList.Name = "dlvMapDefectList"
        Me.dlvMapDefectList.Size = New System.Drawing.Size(283, 305)
        Me.dlvMapDefectList.TabIndex = 53
        '
        'tabMain
        '
        Me.tabMain.BackColor = System.Drawing.Color.Transparent
        Me.tabMain.Controls.Add(Me.layoutView)
        Me.tabMain.Location = New System.Drawing.Point(4, 28)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMain.Size = New System.Drawing.Size(571, 683)
        Me.tabMain.TabIndex = 0
        Me.tabMain.Text = "系統執行畫面 Main"
        '
        'layoutView
        '
        Me.layoutView.ColumnCount = 2
        Me.layoutView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.layoutView.Controls.Add(Me.labModelCount, 1, 1)
        Me.layoutView.Controls.Add(Me.tabView, 0, 2)
        Me.layoutView.Controls.Add(Me.labGray, 0, 1)
        Me.layoutView.Controls.Add(Me.picView, 0, 0)
        Me.layoutView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutView.Location = New System.Drawing.Point(3, 3)
        Me.layoutView.Margin = New System.Windows.Forms.Padding(0)
        Me.layoutView.Name = "layoutView"
        Me.layoutView.RowCount = 3
        Me.layoutView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.layoutView.Size = New System.Drawing.Size(565, 677)
        Me.layoutView.TabIndex = 29
        '
        'labModelCount
        '
        Me.labModelCount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labModelCount.BackColor = System.Drawing.Color.OldLace
        Me.labModelCount.ForeColor = System.Drawing.Color.Maroon
        Me.labModelCount.Location = New System.Drawing.Point(415, 307)
        Me.labModelCount.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.labModelCount.Name = "labModelCount"
        Me.labModelCount.Size = New System.Drawing.Size(147, 20)
        Me.labModelCount.TabIndex = 90
        Me.labModelCount.Text = "樣本數：0"
        Me.labModelCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabView
        '
        Me.tabView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutView.SetColumnSpan(Me.tabView, 2)
        Me.tabView.Controls.Add(Me.tabResult)
        Me.tabView.Controls.Add(Me.tabLocate)
        Me.tabView.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.tabView.ItemSize = New System.Drawing.Size(300, 24)
        Me.tabView.Location = New System.Drawing.Point(0, 327)
        Me.tabView.Margin = New System.Windows.Forms.Padding(0)
        Me.tabView.Name = "tabView"
        Me.tabView.Padding = New System.Drawing.Point(9, 0)
        Me.tabView.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabView.SelectedIndex = 0
        Me.tabView.Size = New System.Drawing.Size(565, 350)
        Me.tabView.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabView.TabIndex = 97
        Me.tabView.UnSelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'tabResult
        '
        Me.tabResult.BackColor = System.Drawing.Color.Transparent
        Me.tabResult.Controls.Add(Me.usrDefectView)
        Me.tabResult.Location = New System.Drawing.Point(4, 28)
        Me.tabResult.Margin = New System.Windows.Forms.Padding(0)
        Me.tabResult.Name = "tabResult"
        Me.tabResult.Size = New System.Drawing.Size(557, 318)
        Me.tabResult.TabIndex = 0
        Me.tabResult.Text = "結果 Result"
        '
        'usrDefectView
        '
        Me.usrDefectView.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.usrDefectView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.usrDefectView.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrDefectView.Location = New System.Drawing.Point(0, 0)
        Me.usrDefectView.Margin = New System.Windows.Forms.Padding(0)
        Me.usrDefectView.Name = "usrDefectView"
        Me.usrDefectView.Size = New System.Drawing.Size(557, 318)
        Me.usrDefectView.TabIndex = 30
        '
        'tabLocate
        '
        Me.tabLocate.BackColor = System.Drawing.Color.Transparent
        Me.tabLocate.Controls.Add(Me.layoutLocate)
        Me.tabLocate.Location = New System.Drawing.Point(4, 28)
        Me.tabLocate.Name = "tabLocate"
        Me.tabLocate.Size = New System.Drawing.Size(557, 318)
        Me.tabLocate.TabIndex = 6
        Me.tabLocate.Text = "定位 Locate"
        '
        'layoutLocate
        '
        Me.layoutLocate.ColumnCount = 2
        Me.layoutLocate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutLocate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutLocate.Controls.Add(Me.picLocate2, 1, 0)
        Me.layoutLocate.Controls.Add(Me.picLocate1, 0, 0)
        Me.layoutLocate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutLocate.Location = New System.Drawing.Point(0, 0)
        Me.layoutLocate.Name = "layoutLocate"
        Me.layoutLocate.RowCount = 1
        Me.layoutLocate.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutLocate.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 318.0!))
        Me.layoutLocate.Size = New System.Drawing.Size(557, 318)
        Me.layoutLocate.TabIndex = 0
        '
        'picLocate2
        '
        Me.picLocate2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picLocate2.DetectValueOnMouseLocation = True
        Me.picLocate2.Location = New System.Drawing.Point(278, 0)
        Me.picLocate2.Margin = New System.Windows.Forms.Padding(0)
        Me.picLocate2.Name = "picLocate2"
        Me.picLocate2.Size = New System.Drawing.Size(279, 318)
        Me.picLocate2.TabIndex = 98
        '
        'picLocate1
        '
        Me.picLocate1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picLocate1.DetectValueOnMouseLocation = True
        Me.picLocate1.Location = New System.Drawing.Point(0, 0)
        Me.picLocate1.Margin = New System.Windows.Forms.Padding(0)
        Me.picLocate1.Name = "picLocate1"
        Me.picLocate1.Size = New System.Drawing.Size(278, 318)
        Me.picLocate1.TabIndex = 97
        '
        'labGray
        '
        Me.labGray.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labGray.BackColor = System.Drawing.Color.OldLace
        Me.labGray.ForeColor = System.Drawing.Color.Maroon
        Me.labGray.Location = New System.Drawing.Point(3, 307)
        Me.labGray.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.labGray.Name = "labGray"
        Me.labGray.Size = New System.Drawing.Size(412, 20)
        Me.labGray.TabIndex = 89
        Me.labGray.Text = "X = 0 ， Y = 0 ， C = 0"
        Me.labGray.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picView
        '
        Me.picView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutView.SetColumnSpan(Me.picView, 2)
        Me.picView.DetectValueOnMouseLocation = True
        Me.picView.Location = New System.Drawing.Point(0, 0)
        Me.picView.Margin = New System.Windows.Forms.Padding(0)
        Me.picView.Name = "picView"
        Me.picView.Size = New System.Drawing.Size(565, 307)
        Me.picView.TabIndex = 1
        '
        'tabLog
        '
        Me.tabLog.BackColor = System.Drawing.Color.Transparent
        Me.tabLog.Controls.Add(Me.tabLogView)
        Me.tabLog.Location = New System.Drawing.Point(4, 28)
        Me.tabLog.Name = "tabLog"
        Me.tabLog.Size = New System.Drawing.Size(571, 683)
        Me.tabLog.TabIndex = 5
        Me.tabLog.Text = "Log 日誌"
        '
        'tabLogView
        '
        Me.tabLogView.Controls.Add(Me.tabSystemLog)
        Me.tabLogView.Controls.Add(Me.tabProcessLog)
        Me.tabLogView.Controls.Add(Me.tabControlLog)
        Me.tabLogView.Controls.Add(Me.tabAlarmLog)
        Me.tabLogView.Controls.Add(Me.tabHandshakeLog)
        Me.tabLogView.Controls.Add(Me.tabManualLog)
        Me.tabLogView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabLogView.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.tabLogView.ItemSize = New System.Drawing.Size(150, 24)
        Me.tabLogView.Location = New System.Drawing.Point(0, 0)
        Me.tabLogView.Margin = New System.Windows.Forms.Padding(0)
        Me.tabLogView.Name = "tabLogView"
        Me.tabLogView.Padding = New System.Drawing.Point(9, 0)
        Me.tabLogView.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabLogView.SelectedIndex = 0
        Me.tabLogView.Size = New System.Drawing.Size(571, 683)
        Me.tabLogView.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabLogView.TabIndex = 21
        Me.tabLogView.UnSelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'tabSystemLog
        '
        Me.tabSystemLog.BackColor = System.Drawing.Color.Transparent
        Me.tabSystemLog.Controls.Add(Me.rtxtSystem)
        Me.tabSystemLog.Location = New System.Drawing.Point(4, 28)
        Me.tabSystemLog.Margin = New System.Windows.Forms.Padding(0)
        Me.tabSystemLog.Name = "tabSystemLog"
        Me.tabSystemLog.Size = New System.Drawing.Size(563, 651)
        Me.tabSystemLog.TabIndex = 0
        Me.tabSystemLog.Text = "執行 Run Time"
        '
        'rtxtSystem
        '
        Me.rtxtSystem.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtSystem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtSystem.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtSystem.Location = New System.Drawing.Point(0, 0)
        Me.rtxtSystem.Name = "rtxtSystem"
        Me.rtxtSystem.ReadOnly = True
        Me.rtxtSystem.Size = New System.Drawing.Size(563, 651)
        Me.rtxtSystem.TabIndex = 16
        Me.rtxtSystem.Text = ""
        Me.rtxtSystem.WordWrap = False
        '
        'tabProcessLog
        '
        Me.tabProcessLog.BackColor = System.Drawing.Color.Transparent
        Me.tabProcessLog.Controls.Add(Me.rtxtProcess)
        Me.tabProcessLog.Location = New System.Drawing.Point(4, 28)
        Me.tabProcessLog.Name = "tabProcessLog"
        Me.tabProcessLog.Size = New System.Drawing.Size(563, 651)
        Me.tabProcessLog.TabIndex = 6
        Me.tabProcessLog.Text = "處理 Process"
        '
        'rtxtProcess
        '
        Me.rtxtProcess.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtProcess.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtProcess.Location = New System.Drawing.Point(0, 0)
        Me.rtxtProcess.Name = "rtxtProcess"
        Me.rtxtProcess.ReadOnly = True
        Me.rtxtProcess.Size = New System.Drawing.Size(563, 651)
        Me.rtxtProcess.TabIndex = 17
        Me.rtxtProcess.Text = ""
        Me.rtxtProcess.WordWrap = False
        '
        'tabControlLog
        '
        Me.tabControlLog.BackColor = System.Drawing.Color.Transparent
        Me.tabControlLog.Controls.Add(Me.rtxtControl)
        Me.tabControlLog.Location = New System.Drawing.Point(4, 28)
        Me.tabControlLog.Name = "tabControlLog"
        Me.tabControlLog.Size = New System.Drawing.Size(563, 651)
        Me.tabControlLog.TabIndex = 7
        Me.tabControlLog.Text = "控制 Control"
        '
        'rtxtControl
        '
        Me.rtxtControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtControl.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtControl.Location = New System.Drawing.Point(0, 0)
        Me.rtxtControl.Name = "rtxtControl"
        Me.rtxtControl.ReadOnly = True
        Me.rtxtControl.Size = New System.Drawing.Size(563, 651)
        Me.rtxtControl.TabIndex = 18
        Me.rtxtControl.Text = ""
        Me.rtxtControl.WordWrap = False
        '
        'tabAlarmLog
        '
        Me.tabAlarmLog.BackColor = System.Drawing.Color.Transparent
        Me.tabAlarmLog.Controls.Add(Me.rtxtAlarm)
        Me.tabAlarmLog.Location = New System.Drawing.Point(4, 28)
        Me.tabAlarmLog.Margin = New System.Windows.Forms.Padding(0)
        Me.tabAlarmLog.Name = "tabAlarmLog"
        Me.tabAlarmLog.Size = New System.Drawing.Size(563, 651)
        Me.tabAlarmLog.TabIndex = 3
        Me.tabAlarmLog.Text = "警報 Alarm"
        '
        'rtxtAlarm
        '
        Me.rtxtAlarm.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtAlarm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtAlarm.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtAlarm.Location = New System.Drawing.Point(0, 0)
        Me.rtxtAlarm.Name = "rtxtAlarm"
        Me.rtxtAlarm.ReadOnly = True
        Me.rtxtAlarm.Size = New System.Drawing.Size(563, 651)
        Me.rtxtAlarm.TabIndex = 18
        Me.rtxtAlarm.Text = ""
        Me.rtxtAlarm.WordWrap = False
        '
        'tabHandshakeLog
        '
        Me.tabHandshakeLog.BackColor = System.Drawing.Color.Transparent
        Me.tabHandshakeLog.Controls.Add(Me.rtxtHandshake)
        Me.tabHandshakeLog.Location = New System.Drawing.Point(4, 28)
        Me.tabHandshakeLog.Margin = New System.Windows.Forms.Padding(0)
        Me.tabHandshakeLog.Name = "tabHandshakeLog"
        Me.tabHandshakeLog.Size = New System.Drawing.Size(563, 651)
        Me.tabHandshakeLog.TabIndex = 8
        Me.tabHandshakeLog.Text = "交握 Handshake"
        '
        'rtxtHandshake
        '
        Me.rtxtHandshake.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtHandshake.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtHandshake.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtHandshake.Location = New System.Drawing.Point(0, 0)
        Me.rtxtHandshake.Name = "rtxtHandshake"
        Me.rtxtHandshake.ReadOnly = True
        Me.rtxtHandshake.Size = New System.Drawing.Size(563, 651)
        Me.rtxtHandshake.TabIndex = 19
        Me.rtxtHandshake.Text = ""
        Me.rtxtHandshake.WordWrap = False
        '
        'tabManualLog
        '
        Me.tabManualLog.BackColor = System.Drawing.Color.Transparent
        Me.tabManualLog.Controls.Add(Me.rtxtManual)
        Me.tabManualLog.Location = New System.Drawing.Point(4, 28)
        Me.tabManualLog.Name = "tabManualLog"
        Me.tabManualLog.Size = New System.Drawing.Size(563, 651)
        Me.tabManualLog.TabIndex = 5
        Me.tabManualLog.Text = "手動 Manual"
        '
        'rtxtManual
        '
        Me.rtxtManual.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtxtManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtManual.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtManual.Location = New System.Drawing.Point(0, 0)
        Me.rtxtManual.Name = "rtxtManual"
        Me.rtxtManual.ReadOnly = True
        Me.rtxtManual.Size = New System.Drawing.Size(563, 651)
        Me.rtxtManual.TabIndex = 18
        Me.rtxtManual.Text = ""
        Me.rtxtManual.WordWrap = False
        '
        'tabHistory
        '
        Me.tabHistory.BackColor = System.Drawing.Color.Transparent
        Me.tabHistory.Controls.Add(Me.layoutHistory)
        Me.tabHistory.Location = New System.Drawing.Point(4, 28)
        Me.tabHistory.Name = "tabHistory"
        Me.tabHistory.Size = New System.Drawing.Size(571, 683)
        Me.tabHistory.TabIndex = 4
        Me.tabHistory.Text = "歷史 History"
        '
        'layoutHistory
        '
        Me.layoutHistory.ColumnCount = 1
        Me.layoutHistory.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutHistory.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutHistory.Controls.Add(Me.lstInspectHistory, 0, 1)
        Me.layoutHistory.Controls.Add(Me.btnOpenHistory, 0, 0)
        Me.layoutHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutHistory.Location = New System.Drawing.Point(0, 0)
        Me.layoutHistory.Name = "layoutHistory"
        Me.layoutHistory.RowCount = 2
        Me.layoutHistory.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutHistory.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutHistory.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutHistory.Size = New System.Drawing.Size(571, 683)
        Me.layoutHistory.TabIndex = 4
        '
        'lstInspectHistory
        '
        Me.lstInspectHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstInspectHistory.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lstInspectHistory.FullRowSelect = True
        Me.lstInspectHistory.GridLines = True
        Me.lstInspectHistory.Location = New System.Drawing.Point(3, 53)
        Me.lstInspectHistory.Name = "lstInspectHistory"
        Me.lstInspectHistory.Size = New System.Drawing.Size(565, 883)
        Me.lstInspectHistory.TabIndex = 24
        Me.lstInspectHistory.UseCompatibleStateImageBehavior = False
        Me.lstInspectHistory.View = System.Windows.Forms.View.Details
        '
        'btnOpenHistory
        '
        Me.btnOpenHistory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenHistory.Corners.All = 18
        Me.btnOpenHistory.Corners.LowerLeft = 18
        Me.btnOpenHistory.Corners.LowerRight = 18
        Me.btnOpenHistory.Corners.UpperLeft = 18
        Me.btnOpenHistory.Corners.UpperRight = 18
        Me.btnOpenHistory.DesignerSelected = False
        Me.btnOpenHistory.Font = New System.Drawing.Font("微軟正黑體", 12.0!)
        Me.btnOpenHistory.ImageIndex = 0
        Me.btnOpenHistory.Location = New System.Drawing.Point(3, 3)
        Me.btnOpenHistory.Name = "btnOpenHistory"
        Me.btnOpenHistory.Size = New System.Drawing.Size(395, 44)
        Me.btnOpenHistory.TabIndex = 20
        Me.btnOpenHistory.Text = "打開歷史紀錄 Open History"
        '
        'gbxInformation
        '
        Me.gbxInformation.BorderColor = System.Drawing.Color.Navy
        Me.layoutMainControl.SetColumnSpan(Me.gbxInformation, 2)
        Me.gbxInformation.Controls.Add(Me.CButton1)
        Me.gbxInformation.Controls.Add(Me.UsrDisplay2)
        Me.gbxInformation.Controls.Add(Me.labStatusTime)
        Me.gbxInformation.Controls.Add(Me.labStatusDate)
        Me.gbxInformation.Controls.Add(Me.labRecipeID)
        Me.gbxInformation.Controls.Add(Me.usrExecute)
        Me.gbxInformation.Controls.Add(Me.btnClearAlarm)
        Me.gbxInformation.Controls.Add(Me.usrRunning)
        Me.gbxInformation.Controls.Add(Me.usrAlarm)
        Me.gbxInformation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbxInformation.ForeColor = System.Drawing.Color.Navy
        Me.gbxInformation.LineCount = 2
        Me.gbxInformation.Location = New System.Drawing.Point(3, 3)
        Me.gbxInformation.Name = "gbxInformation"
        Me.gbxInformation.Size = New System.Drawing.Size(338, 184)
        Me.gbxInformation.TabIndex = 28
        Me.gbxInformation.TabStop = False
        Me.gbxInformation.Text = "當站資訊 Station"
        '
        'CButton1
        '
        Me.CButton1.Corners.All = 12
        Me.CButton1.Corners.LowerLeft = 12
        Me.CButton1.Corners.LowerRight = 12
        Me.CButton1.Corners.UpperLeft = 12
        Me.CButton1.Corners.UpperRight = 12
        Me.CButton1.DesignerSelected = False
        Me.CButton1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.CButton1.ImageIndex = 0
        Me.CButton1.Location = New System.Drawing.Point(175, 54)
        Me.CButton1.Name = "CButton1"
        Me.CButton1.Size = New System.Drawing.Size(129, 25)
        Me.CButton1.TabIndex = 98
        Me.CButton1.Text = "交握測試按鈕"
        Me.CButton1.Visible = False
        '
        'UsrDisplay2
        '
        Me.UsrDisplay2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsrDisplay2.DetectValueOnMouseLocation = True
        Me.UsrDisplay2.Location = New System.Drawing.Point(0, 254)
        Me.UsrDisplay2.Margin = New System.Windows.Forms.Padding(0)
        Me.UsrDisplay2.Name = "UsrDisplay2"
        Me.UsrDisplay2.Size = New System.Drawing.Size(815, 17)
        Me.UsrDisplay2.TabIndex = 97
        '
        'labStatusTime
        '
        Me.labStatusTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labStatusTime.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labStatusTime.ForeColor = System.Drawing.Color.DarkGreen
        Me.labStatusTime.Location = New System.Drawing.Point(173, 24)
        Me.labStatusTime.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labStatusTime.Name = "labStatusTime"
        Me.labStatusTime.Size = New System.Drawing.Size(143, 25)
        Me.labStatusTime.TabIndex = 96
        Me.labStatusTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labStatusDate
        '
        Me.labStatusDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labStatusDate.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labStatusDate.ForeColor = System.Drawing.Color.DarkGreen
        Me.labStatusDate.Location = New System.Drawing.Point(12, 24)
        Me.labStatusDate.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labStatusDate.Name = "labStatusDate"
        Me.labStatusDate.Size = New System.Drawing.Size(157, 25)
        Me.labStatusDate.TabIndex = 95
        Me.labStatusDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labRecipeID
        '
        Me.labRecipeID.BackColor = System.Drawing.Color.Yellow
        Me.labRecipeID.Font = New System.Drawing.Font("微軟正黑體", 21.75!, System.Drawing.FontStyle.Bold)
        Me.labRecipeID.ForeColor = System.Drawing.Color.Maroon
        Me.labRecipeID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labRecipeID.Location = New System.Drawing.Point(12, 54)
        Me.labRecipeID.Margin = New System.Windows.Forms.Padding(5)
        Me.labRecipeID.Name = "labRecipeID"
        Me.labRecipeID.Size = New System.Drawing.Size(304, 49)
        Me.labRecipeID.TabIndex = 88
        Me.labRecipeID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'usrExecute
        '
        Me.usrExecute.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.usrExecute.ConnectText = "Trigger On"
        Me.usrExecute.DisConnectText = "Trigger Off"
        Me.usrExecute.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrExecute.ForeColor = System.Drawing.Color.Black
        Me.usrExecute.Location = New System.Drawing.Point(175, 111)
        Me.usrExecute.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.usrExecute.Name = "usrExecute"
        Me.usrExecute.Size = New System.Drawing.Size(141, 25)
        Me.usrExecute.TabIndex = 24
        Me.usrExecute.Text = "Trigger"
        '
        'btnClearAlarm
        '
        Me.btnClearAlarm.Corners.All = 12
        Me.btnClearAlarm.Corners.LowerLeft = 12
        Me.btnClearAlarm.Corners.LowerRight = 12
        Me.btnClearAlarm.Corners.UpperLeft = 12
        Me.btnClearAlarm.Corners.UpperRight = 12
        Me.btnClearAlarm.DesignerSelected = False
        Me.btnClearAlarm.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnClearAlarm.ImageIndex = 0
        Me.btnClearAlarm.Location = New System.Drawing.Point(175, 143)
        Me.btnClearAlarm.Name = "btnClearAlarm"
        Me.btnClearAlarm.Size = New System.Drawing.Size(145, 25)
        Me.btnClearAlarm.TabIndex = 32
        Me.btnClearAlarm.Text = "清除"
        '
        'usrRunning
        '
        Me.usrRunning.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.usrRunning.ConnectText = "Running"
        Me.usrRunning.DisConnectText = "Running"
        Me.usrRunning.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrRunning.ForeColor = System.Drawing.Color.Black
        Me.usrRunning.Location = New System.Drawing.Point(12, 110)
        Me.usrRunning.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.usrRunning.Name = "usrRunning"
        Me.usrRunning.Size = New System.Drawing.Size(157, 25)
        Me.usrRunning.TabIndex = 86
        Me.usrRunning.Text = "Running"
        '
        'usrAlarm
        '
        Me.usrAlarm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.usrAlarm.ConnectText = "Alarm"
        Me.usrAlarm.DisConnectText = "Alarm"
        Me.usrAlarm.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrAlarm.ForeColor = System.Drawing.Color.Black
        Me.usrAlarm.Location = New System.Drawing.Point(12, 143)
        Me.usrAlarm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.usrAlarm.Name = "usrAlarm"
        Me.usrAlarm.Size = New System.Drawing.Size(157, 25)
        Me.usrAlarm.TabIndex = 21
        Me.usrAlarm.Text = "Alarm"
        '
        'btnSingleRun
        '
        Me.btnSingleRun.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutMainControl.SetColumnSpan(Me.btnSingleRun, 2)
        Me.btnSingleRun.Corners.All = 14
        Me.btnSingleRun.Corners.LowerLeft = 14
        Me.btnSingleRun.Corners.LowerRight = 14
        Me.btnSingleRun.Corners.UpperLeft = 14
        Me.btnSingleRun.Corners.UpperRight = 14
        Me.btnSingleRun.DesignerSelected = False
        Me.btnSingleRun.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSingleRun.ImageIndex = 0
        Me.btnSingleRun.Location = New System.Drawing.Point(3, 195)
        Me.btnSingleRun.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnSingleRun.Name = "btnSingleRun"
        Me.btnSingleRun.Size = New System.Drawing.Size(338, 40)
        Me.btnSingleRun.TabIndex = 29
        Me.btnSingleRun.Text = "單次執行 Single Run"
        '
        'btnContinusRun
        '
        Me.btnContinusRun.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutMainControl.SetColumnSpan(Me.btnContinusRun, 2)
        Me.btnContinusRun.Corners.All = 14
        Me.btnContinusRun.Corners.LowerLeft = 14
        Me.btnContinusRun.Corners.LowerRight = 14
        Me.btnContinusRun.Corners.UpperLeft = 14
        Me.btnContinusRun.Corners.UpperRight = 14
        Me.btnContinusRun.DesignerSelected = False
        Me.btnContinusRun.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.btnContinusRun.ImageIndex = 0
        Me.btnContinusRun.Location = New System.Drawing.Point(3, 245)
        Me.btnContinusRun.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnContinusRun.Name = "btnContinusRun"
        Me.btnContinusRun.Size = New System.Drawing.Size(338, 40)
        Me.btnContinusRun.TabIndex = 30
        Me.btnContinusRun.Text = "連續執行 Continus Run"
        '
        'btnTestRun
        '
        Me.btnTestRun.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutMainControl.SetColumnSpan(Me.btnTestRun, 2)
        Me.btnTestRun.Corners.All = 14
        Me.btnTestRun.Corners.LowerLeft = 14
        Me.btnTestRun.Corners.LowerRight = 14
        Me.btnTestRun.Corners.UpperLeft = 14
        Me.btnTestRun.Corners.UpperRight = 14
        Me.btnTestRun.DesignerSelected = False
        Me.btnTestRun.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.btnTestRun.ImageIndex = 0
        Me.btnTestRun.Location = New System.Drawing.Point(3, 295)
        Me.btnTestRun.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnTestRun.Name = "btnTestRun"
        Me.btnTestRun.Size = New System.Drawing.Size(338, 40)
        Me.btnTestRun.TabIndex = 31
        Me.btnTestRun.Text = "測試執行 Test Run"
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutMainControl.SetColumnSpan(Me.btnStop, 2)
        Me.btnStop.Corners.All = 14
        Me.btnStop.Corners.LowerLeft = 14
        Me.btnStop.Corners.LowerRight = 14
        Me.btnStop.Corners.UpperLeft = 14
        Me.btnStop.Corners.UpperRight = 14
        Me.btnStop.DesignerSelected = False
        Me.btnStop.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.btnStop.ImageIndex = 0
        Me.btnStop.Location = New System.Drawing.Point(3, 345)
        Me.btnStop.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(338, 40)
        Me.btnStop.TabIndex = 33
        Me.btnStop.Text = "停止執行 Stop Run"
        '
        'btnRecipeManager
        '
        Me.btnRecipeManager.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutMainControl.SetColumnSpan(Me.btnRecipeManager, 2)
        Me.btnRecipeManager.Corners.All = 18
        Me.btnRecipeManager.Corners.LowerLeft = 18
        Me.btnRecipeManager.Corners.LowerRight = 18
        Me.btnRecipeManager.Corners.UpperLeft = 18
        Me.btnRecipeManager.Corners.UpperRight = 18
        Me.btnRecipeManager.DesignerSelected = False
        Me.btnRecipeManager.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.btnRecipeManager.ImageIndex = 0
        Me.btnRecipeManager.Location = New System.Drawing.Point(3, 427)
        Me.btnRecipeManager.Name = "btnRecipeManager"
        Me.btnRecipeManager.Size = New System.Drawing.Size(338, 40)
        Me.btnRecipeManager.TabIndex = 34
        Me.btnRecipeManager.Text = "製程管理 Recipe Manager"
        '
        'btnChangeModel
        '
        Me.btnChangeModel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutMainControl.SetColumnSpan(Me.btnChangeModel, 2)
        Me.btnChangeModel.Corners.All = 18
        Me.btnChangeModel.Corners.LowerLeft = 18
        Me.btnChangeModel.Corners.LowerRight = 18
        Me.btnChangeModel.Corners.UpperLeft = 18
        Me.btnChangeModel.Corners.UpperRight = 18
        Me.btnChangeModel.DesignerSelected = False
        Me.btnChangeModel.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.btnChangeModel.ImageIndex = 0
        Me.btnChangeModel.Location = New System.Drawing.Point(3, 477)
        Me.btnChangeModel.Name = "btnChangeModel"
        Me.btnChangeModel.Size = New System.Drawing.Size(338, 40)
        Me.btnChangeModel.TabIndex = 20
        Me.btnChangeModel.Text = "更換樣本 Change Model"
        '
        'cbxGatherStandardDeviation
        '
        Me.layoutMainControl.SetColumnSpan(Me.cbxGatherStandardDeviation, 2)
        Me.cbxGatherStandardDeviation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbxGatherStandardDeviation.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cbxGatherStandardDeviation.Location = New System.Drawing.Point(3, 540)
        Me.cbxGatherStandardDeviation.Margin = New System.Windows.Forms.Padding(3, 20, 3, 3)
        Me.cbxGatherStandardDeviation.Name = "cbxGatherStandardDeviation"
        Me.cbxGatherStandardDeviation.Size = New System.Drawing.Size(338, 22)
        Me.cbxGatherStandardDeviation.TabIndex = 38
        Me.cbxGatherStandardDeviation.Text = "是否收集標準差影像 Camera"
        Me.cbxGatherStandardDeviation.UseVisualStyleBackColor = True
        '
        'cbxIsAutoChangeModel
        '
        Me.layoutMainControl.SetColumnSpan(Me.cbxIsAutoChangeModel, 2)
        Me.cbxIsAutoChangeModel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbxIsAutoChangeModel.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cbxIsAutoChangeModel.Location = New System.Drawing.Point(3, 585)
        Me.cbxIsAutoChangeModel.Margin = New System.Windows.Forms.Padding(3, 20, 3, 3)
        Me.cbxIsAutoChangeModel.Name = "cbxIsAutoChangeModel"
        Me.cbxIsAutoChangeModel.Size = New System.Drawing.Size(338, 22)
        Me.cbxIsAutoChangeModel.TabIndex = 40
        Me.cbxIsAutoChangeModel.Text = "是否自動更換樣本"
        Me.cbxIsAutoChangeModel.UseVisualStyleBackColor = True
        '
        'layoutLoadImage
        '
        Me.layoutLoadImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutLoadImage.ColumnCount = 1
        Me.layoutLoadImage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutLoadImage.Controls.Add(Me.tbxLoadImage, 0, 1)
        Me.layoutLoadImage.Controls.Add(Me.btnLoadImage, 0, 0)
        Me.layoutLoadImage.Location = New System.Drawing.Point(3, 613)
        Me.layoutLoadImage.Name = "layoutLoadImage"
        Me.layoutLoadImage.RowCount = 2
        Me.layoutLoadImage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.layoutLoadImage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutLoadImage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutLoadImage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutLoadImage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutLoadImage.Size = New System.Drawing.Size(204, 74)
        Me.layoutLoadImage.TabIndex = 36
        Me.layoutLoadImage.Visible = False
        '
        'tbxLoadImage
        '
        Me.tbxLoadImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxLoadImage.Location = New System.Drawing.Point(3, 43)
        Me.tbxLoadImage.Name = "tbxLoadImage"
        Me.tbxLoadImage.Size = New System.Drawing.Size(198, 25)
        Me.tbxLoadImage.TabIndex = 35
        '
        'btnLoadImage
        '
        Me.btnLoadImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadImage.Corners.All = 18
        Me.btnLoadImage.Corners.LowerLeft = 18
        Me.btnLoadImage.Corners.LowerRight = 18
        Me.btnLoadImage.Corners.UpperLeft = 18
        Me.btnLoadImage.Corners.UpperRight = 18
        Me.btnLoadImage.DesignerSelected = False
        Me.btnLoadImage.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnLoadImage.ImageIndex = 0
        Me.btnLoadImage.Location = New System.Drawing.Point(0, 0)
        Me.btnLoadImage.Margin = New System.Windows.Forms.Padding(0)
        Me.btnLoadImage.Name = "btnLoadImage"
        Me.btnLoadImage.Size = New System.Drawing.Size(204, 40)
        Me.btnLoadImage.TabIndex = 34
        Me.btnLoadImage.Text = "載入影像 Load Image "
        '
        'dgvProduct
        '
        Me.dgvProduct.AllowUserToAddRows = False
        Me.dgvProduct.AllowUserToDeleteRows = False
        Me.dgvProduct.AutoGenerateColumns = False
        Me.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvProduct.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProduct.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LotIDDataGridViewTextBoxColumn, Me.SubstrateIDDataGridViewTextBoxColumn})
        Me.dgvProduct.DataSource = Me.BindingSourceProduct
        Me.dgvProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProduct.Location = New System.Drawing.Point(3, 743)
        Me.dgvProduct.Name = "dgvProduct"
        Me.dgvProduct.ReadOnly = True
        Me.dgvProduct.RowHeadersVisible = False
        Me.dgvProduct.RowTemplate.Height = 24
        Me.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProduct.Size = New System.Drawing.Size(204, 1)
        Me.dgvProduct.TabIndex = 39
        '
        'LotIDDataGridViewTextBoxColumn
        '
        Me.LotIDDataGridViewTextBoxColumn.DataPropertyName = "LotID"
        Me.LotIDDataGridViewTextBoxColumn.HeaderText = "Lot ID"
        Me.LotIDDataGridViewTextBoxColumn.Name = "LotIDDataGridViewTextBoxColumn"
        Me.LotIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.LotIDDataGridViewTextBoxColumn.Width = 70
        '
        'SubstrateIDDataGridViewTextBoxColumn
        '
        Me.SubstrateIDDataGridViewTextBoxColumn.DataPropertyName = "SubstrateID"
        Me.SubstrateIDDataGridViewTextBoxColumn.HeaderText = "Substrate ID"
        Me.SubstrateIDDataGridViewTextBoxColumn.Name = "SubstrateIDDataGridViewTextBoxColumn"
        Me.SubstrateIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.SubstrateIDDataGridViewTextBoxColumn.Width = 108
        '
        'BindingSourceProduct
        '
        Me.BindingSourceProduct.DataSource = GetType(DefectLib.CMyProduct)
        '
        'layoutMain
        '
        Me.layoutMain.ColumnCount = 2
        Me.layoutMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.layoutMain.Controls.Add(Me.tabViewControl, 0, 0)
        Me.layoutMain.Controls.Add(Me.layoutMainControl, 1, 0)
        Me.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutMain.Location = New System.Drawing.Point(0, 28)
        Me.layoutMain.Name = "layoutMain"
        Me.layoutMain.RowCount = 1
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMain.Size = New System.Drawing.Size(935, 721)
        Me.layoutMain.TabIndex = 30
        '
        'layoutMainControl
        '
        Me.layoutMainControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutMainControl.BackColor = System.Drawing.Color.LavenderBlush
        Me.layoutMainControl.ColumnCount = 2
        Me.layoutMainControl.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210.0!))
        Me.layoutMainControl.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMainControl.Controls.Add(Me.labYield, 0, 10)
        Me.layoutMainControl.Controls.Add(Me.labProductCount, 0, 11)
        Me.layoutMainControl.Controls.Add(Me.cbxIsAutoChangeModel, 0, 8)
        Me.layoutMainControl.Controls.Add(Me.cbxGatherStandardDeviation, 0, 7)
        Me.layoutMainControl.Controls.Add(Me.btnChangeModel, 0, 6)
        Me.layoutMainControl.Controls.Add(Me.btnRecipeManager, 0, 5)
        Me.layoutMainControl.Controls.Add(Me.btnStop, 0, 4)
        Me.layoutMainControl.Controls.Add(Me.btnTestRun, 0, 3)
        Me.layoutMainControl.Controls.Add(Me.btnSingleRun, 0, 1)
        Me.layoutMainControl.Controls.Add(Me.btnContinusRun, 0, 2)
        Me.layoutMainControl.Controls.Add(Me.layoutLoadImage, 0, 9)
        Me.layoutMainControl.Controls.Add(Me.dgvCodeReadResult, 1, 12)
        Me.layoutMainControl.Controls.Add(Me.dgvProduct, 0, 12)
        Me.layoutMainControl.Controls.Add(Me.gbxInformation, 0, 0)
        Me.layoutMainControl.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.layoutMainControl.Location = New System.Drawing.Point(588, 3)
        Me.layoutMainControl.Name = "layoutMainControl"
        Me.layoutMainControl.RowCount = 13
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.layoutMainControl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMainControl.Size = New System.Drawing.Size(344, 715)
        Me.layoutMainControl.TabIndex = 31
        '
        'labYield
        '
        Me.labYield.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labYield.BackColor = System.Drawing.Color.Transparent
        Me.labYield.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labYield.ForeColor = System.Drawing.Color.Maroon
        Me.labYield.Location = New System.Drawing.Point(3, 690)
        Me.labYield.Name = "labYield"
        Me.labYield.Size = New System.Drawing.Size(204, 25)
        Me.labYield.TabIndex = 99
        Me.labYield.Text = "良率：0/0 (0%)"
        Me.labYield.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labProductCount
        '
        Me.labProductCount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labProductCount.BackColor = System.Drawing.Color.Transparent
        Me.labProductCount.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labProductCount.ForeColor = System.Drawing.Color.Maroon
        Me.labProductCount.Location = New System.Drawing.Point(3, 715)
        Me.labProductCount.Name = "labProductCount"
        Me.labProductCount.Size = New System.Drawing.Size(204, 25)
        Me.labProductCount.TabIndex = 98
        Me.labProductCount.Text = "產品剩餘數量：0"
        Me.labProductCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgvCodeReadResult
        '
        Me.dgvCodeReadResult.AllowUserToAddRows = False
        Me.dgvCodeReadResult.AllowUserToDeleteRows = False
        Me.dgvCodeReadResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCodeReadResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCodeReadResult.Location = New System.Drawing.Point(213, 743)
        Me.dgvCodeReadResult.Name = "dgvCodeReadResult"
        Me.dgvCodeReadResult.ReadOnly = True
        Me.dgvCodeReadResult.RowHeadersVisible = False
        Me.dgvCodeReadResult.RowTemplate.Height = 24
        Me.dgvCodeReadResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCodeReadResult.Size = New System.Drawing.Size(128, 1)
        Me.dgvCodeReadResult.TabIndex = 100
        '
        'mnuDrawImage
        '
        Me.mnuDrawImage.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuDrawImage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDrawRecipe, Me.mnuDrawDefect, Me.mnuDrawModel, Me.mnuDrawModelCenter, Me.mnuDrawImageLine1St, Me.mnuSaveImage, Me.mnuSaveStandardDeviationModelImage, Me.mnuLoadStandardDeviationModelImage, Me.mnuClearStandardDeviationModelImage})
        Me.mnuDrawImage.Name = "MenuDisplay"
        Me.mnuDrawImage.Size = New System.Drawing.Size(281, 202)
        '
        'mnuDrawRecipe
        '
        Me.mnuDrawRecipe.Name = "mnuDrawRecipe"
        Me.mnuDrawRecipe.Size = New System.Drawing.Size(280, 24)
        Me.mnuDrawRecipe.Text = "顯示 / 不顯示 Recipe"
        '
        'mnuDrawDefect
        '
        Me.mnuDrawDefect.Name = "mnuDrawDefect"
        Me.mnuDrawDefect.Size = New System.Drawing.Size(280, 24)
        Me.mnuDrawDefect.Text = "顯示 / 不顯示 Defect"
        '
        'mnuDrawModel
        '
        Me.mnuDrawModel.Name = "mnuDrawModel"
        Me.mnuDrawModel.Size = New System.Drawing.Size(280, 24)
        Me.mnuDrawModel.Text = "顯示 / 不顯示 Model"
        '
        'mnuDrawModelCenter
        '
        Me.mnuDrawModelCenter.Name = "mnuDrawModelCenter"
        Me.mnuDrawModelCenter.Size = New System.Drawing.Size(280, 24)
        Me.mnuDrawModelCenter.Text = "顯示 / 不顯示 Model Center"
        '
        'mnuDrawImageLine1St
        '
        Me.mnuDrawImageLine1St.Name = "mnuDrawImageLine1St"
        Me.mnuDrawImageLine1St.Size = New System.Drawing.Size(277, 6)
        '
        'mnuSaveImage
        '
        Me.mnuSaveImage.Name = "mnuSaveImage"
        Me.mnuSaveImage.Size = New System.Drawing.Size(280, 24)
        Me.mnuSaveImage.Text = "儲存原圖"
        '
        'mnuSaveStandardDeviationModelImage
        '
        Me.mnuSaveStandardDeviationModelImage.Name = "mnuSaveStandardDeviationModelImage"
        Me.mnuSaveStandardDeviationModelImage.Size = New System.Drawing.Size(280, 24)
        Me.mnuSaveStandardDeviationModelImage.Text = "儲存標準差樣本"
        '
        'mnuLoadStandardDeviationModelImage
        '
        Me.mnuLoadStandardDeviationModelImage.Name = "mnuLoadStandardDeviationModelImage"
        Me.mnuLoadStandardDeviationModelImage.Size = New System.Drawing.Size(280, 24)
        Me.mnuLoadStandardDeviationModelImage.Text = "載入標準差樣本"
        '
        'mnuClearStandardDeviationModelImage
        '
        Me.mnuClearStandardDeviationModelImage.Name = "mnuClearStandardDeviationModelImage"
        Me.mnuClearStandardDeviationModelImage.Size = New System.Drawing.Size(280, 24)
        Me.mnuClearStandardDeviationModelImage.Text = "清除標準差樣本"
        '
        'mnuProduct
        '
        Me.mnuProduct.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuProduct.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuClearProduct, Me.mnuProductLine1St, Me.mnuClearAllProduct})
        Me.mnuProduct.Name = "MenuDisplay"
        Me.mnuProduct.Size = New System.Drawing.Size(223, 58)
        '
        'mnuClearProduct
        '
        Me.mnuClearProduct.Name = "mnuClearProduct"
        Me.mnuClearProduct.Size = New System.Drawing.Size(222, 24)
        Me.mnuClearProduct.Text = "刪除選取之產品資訊"
        '
        'mnuProductLine1St
        '
        Me.mnuProductLine1St.Name = "mnuProductLine1St"
        Me.mnuProductLine1St.Size = New System.Drawing.Size(219, 6)
        '
        'mnuClearAllProduct
        '
        Me.mnuClearAllProduct.Name = "mnuClearAllProduct"
        Me.mnuClearAllProduct.Size = New System.Drawing.Size(222, 24)
        Me.mnuClearAllProduct.Text = "刪除所有之產品資訊"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(935, 749)
        Me.Controls.Add(Me.layoutMain)
        Me.Controls.Add(Me.menuMain)
        Me.Font = New System.Drawing.Font("微軟正黑體", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "銓發科技 - Laser Marking AOI"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.menuMain.ResumeLayout(false)
        Me.menuMain.PerformLayout
        Me.tabViewControl.ResumeLayout(false)
        Me.TabMap.ResumeLayout(false)
        Me.layoutMap.ResumeLayout(false)
        Me.tabMain.ResumeLayout(false)
        Me.layoutView.ResumeLayout(false)
        Me.tabView.ResumeLayout(false)
        Me.tabResult.ResumeLayout(false)
        Me.tabLocate.ResumeLayout(false)
        Me.layoutLocate.ResumeLayout(false)
        Me.tabLog.ResumeLayout(false)
        Me.tabLogView.ResumeLayout(false)
        Me.tabSystemLog.ResumeLayout(false)
        Me.tabProcessLog.ResumeLayout(false)
        Me.tabControlLog.ResumeLayout(false)
        Me.tabAlarmLog.ResumeLayout(false)
        Me.tabHandshakeLog.ResumeLayout(false)
        Me.tabManualLog.ResumeLayout(false)
        Me.tabHistory.ResumeLayout(false)
        Me.layoutHistory.ResumeLayout(false)
        Me.gbxInformation.ResumeLayout(false)
        Me.layoutLoadImage.ResumeLayout(false)
        Me.layoutLoadImage.PerformLayout
        CType(Me.dgvProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSourceProduct,System.ComponentModel.ISupportInitialize).EndInit
        Me.layoutMain.ResumeLayout(false)
        Me.layoutMainControl.ResumeLayout(false)
        CType(Me.dgvCodeReadResult,System.ComponentModel.ISupportInitialize).EndInit
        Me.mnuDrawImage.ResumeLayout(false)
        Me.mnuProduct.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents bkUpdate As System.ComponentModel.BackgroundWorker
    Friend WithEvents bkTime As System.ComponentModel.BackgroundWorker
    Friend WithEvents menuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuHardwareConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVersion As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents tabViewControl As iTVisionService.iTVControl.CSharpTabControl
    Friend WithEvents tabMain As System.Windows.Forms.TabPage
    Friend WithEvents layoutView As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbxInformation As iTVisionService.iTVControl.CBorderGroupBox
    Friend WithEvents usrRunning As iTVisionService.iTVControl.usrConnectStatus
    Friend WithEvents usrExecute As iTVisionService.iTVControl.usrConnectStatus
    Friend WithEvents usrAlarm As iTVisionService.iTVControl.usrConnectStatus
    Friend WithEvents btnChangeModel As iTVisionService.ButtonLib.CButton
    Friend WithEvents labRecipeID As System.Windows.Forms.Label
    Friend WithEvents tabLogView As iTVisionService.iTVControl.CSharpTabControl
    Friend WithEvents tabSystemLog As System.Windows.Forms.TabPage
    Friend WithEvents rtxtProcess As System.Windows.Forms.RichTextBox
    Friend WithEvents rtxtControl As System.Windows.Forms.RichTextBox
    Friend WithEvents tabAlarmLog As System.Windows.Forms.TabPage
    Friend WithEvents rtxtAlarm As System.Windows.Forms.RichTextBox
    Friend WithEvents tabHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabManualLog As System.Windows.Forms.TabPage
    Friend WithEvents rtxtManual As System.Windows.Forms.RichTextBox
    Friend WithEvents btnSingleRun As iTVisionService.ButtonLib.CButton
    Friend WithEvents picView As iTVisionService.usrDisplay
    Friend WithEvents btnContinusRun As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnClearAlarm As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnStop As iTVisionService.ButtonLib.CButton
    Friend WithEvents btnRecipeManager As iTVisionService.ButtonLib.CButton
    Friend WithEvents usrDefectView As DefectLib.usrDefectView
    Friend WithEvents mnuRecipe As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUnitTest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents layoutMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents layoutLoadImage As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnLoadImage As iTVisionService.ButtonLib.CButton
    Friend WithEvents tbxLoadImage As System.Windows.Forms.TextBox
    Friend WithEvents mnuDrawImage As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDrawRecipe As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDrawDefect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaveImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents layoutHistory As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOpenHistory As iTVisionService.ButtonLib.CButton
    Friend WithEvents lstInspectHistory As DefectLib.CDefectViewCamera
    Friend WithEvents mnuSaveStandardDeviationModelImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLoadStandardDeviationModelImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbxGatherStandardDeviation As System.Windows.Forms.CheckBox
    Friend WithEvents rtxtSystem As System.Windows.Forms.RichTextBox
    Friend WithEvents tabProcessLog As System.Windows.Forms.TabPage
    Friend WithEvents tabControlLog As System.Windows.Forms.TabPage
    Friend WithEvents mnuDrawModel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents labGray As System.Windows.Forms.Label
    Friend WithEvents tabHandshakeLog As System.Windows.Forms.TabPage
    Friend WithEvents rtxtHandshake As System.Windows.Forms.RichTextBox
    Friend WithEvents mnuDrawModelCenter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClearStandardDeviationModelImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDrawImageLine1St As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingSourceProduct As System.Windows.Forms.BindingSource
    Friend WithEvents labStatusTime As System.Windows.Forms.Label
    Friend WithEvents labStatusDate As System.Windows.Forms.Label
    Friend WithEvents dgvProduct As System.Windows.Forms.DataGridView
    Friend WithEvents mnuProduct As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuClearProduct As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProductLine1St As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuClearAllProduct As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabLog As System.Windows.Forms.TabPage
    Friend WithEvents mnuUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLogInOut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbxIsAutoChangeModel As System.Windows.Forms.CheckBox
    Friend WithEvents mnuRecipeCodeReader As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnTestRun As iTVisionService.ButtonLib.CButton
    Friend WithEvents layoutMainControl As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LotIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubstrateIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents labProductCount As System.Windows.Forms.Label
    Friend WithEvents tabView As iTVisionService.iTVControl.CSharpTabControl
    Friend WithEvents tabResult As System.Windows.Forms.TabPage
    Friend WithEvents tabLocate As System.Windows.Forms.TabPage
    Friend WithEvents labModelCount As System.Windows.Forms.Label
    Friend WithEvents layoutLocate As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents picLocate2 As iTVisionService.usrDisplay
    Friend WithEvents picLocate1 As iTVisionService.usrDisplay
    Friend WithEvents UsrDisplay2 As iTVisionService.usrDisplay
    Friend WithEvents labYield As System.Windows.Forms.Label
    Friend WithEvents CButton1 As iTVisionService.ButtonLib.CButton
    Friend WithEvents dgvCodeReadResult As System.Windows.Forms.DataGridView
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents TabMap As System.Windows.Forms.TabPage
    Friend WithEvents layoutMap As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents mvwMapView As iTVisionService.DisplayLib.usrMapView
    Friend WithEvents dlvMapDieList As iTVisionService.DisplayLib.usrDieListView
    Friend WithEvents dlvMapDefectList As iTVisionService.DisplayLib.usrDefectListView
    Friend WithEvents mnuRecipeInspect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRecipeWaferMap As System.Windows.Forms.ToolStripMenuItem
End Class
