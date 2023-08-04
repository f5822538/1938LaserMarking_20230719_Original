<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWaferMap
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
        Me.layoutWaferMap = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlCommand = New System.Windows.Forms.Panel()
        Me.btnCancel = New iTVisionService.ButtonLib.CButton()
        Me.btnSave = New iTVisionService.ButtonLib.CButton()
        Me.btnQuit = New iTVisionService.ButtonLib.CButton()
        Me.pgdWaferMap = New System.Windows.Forms.PropertyGrid()
        Me.dlvMapDieList = New iTVisionService.DisplayLib.usrDieListView()
        Me.mvwMapView = New iTVisionService.DisplayLib.usrMapView()
        Me.mnuMapView = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuEffectiveToInvalid = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInvalidToEffective = New System.Windows.Forms.ToolStripMenuItem()
        Me.layoutWaferMap.SuspendLayout()
        Me.pnlCommand.SuspendLayout()
        Me.mnuMapView.SuspendLayout()
        Me.SuspendLayout()
        '
        'layoutWaferMap
        '
        Me.layoutWaferMap.ColumnCount = 2
        Me.layoutWaferMap.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutWaferMap.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.layoutWaferMap.Controls.Add(Me.pnlCommand, 0, 0)
        Me.layoutWaferMap.Controls.Add(Me.pgdWaferMap, 1, 0)
        Me.layoutWaferMap.Controls.Add(Me.dlvMapDieList, 0, 1)
        Me.layoutWaferMap.Controls.Add(Me.mvwMapView, 0, 2)
        Me.layoutWaferMap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutWaferMap.Location = New System.Drawing.Point(0, 0)
        Me.layoutWaferMap.Name = "layoutWaferMap"
        Me.layoutWaferMap.RowCount = 3
        Me.layoutWaferMap.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.layoutWaferMap.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.layoutWaferMap.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.layoutWaferMap.Size = New System.Drawing.Size(1284, 1005)
        Me.layoutWaferMap.TabIndex = 0
        '
        'pnlCommand
        '
        Me.pnlCommand.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCommand.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlCommand.Controls.Add(Me.btnCancel)
        Me.pnlCommand.Controls.Add(Me.btnSave)
        Me.pnlCommand.Controls.Add(Me.btnQuit)
        Me.pnlCommand.Location = New System.Drawing.Point(3, 3)
        Me.pnlCommand.Name = "pnlCommand"
        Me.pnlCommand.Size = New System.Drawing.Size(636, 54)
        Me.pnlCommand.TabIndex = 6
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
        Me.btnCancel.Location = New System.Drawing.Point(383, 9)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(181, 36)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "取消 Cancel"
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
        Me.btnSave.Size = New System.Drawing.Size(181, 36)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "儲存參數 Save Recpe"
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
        Me.btnQuit.Location = New System.Drawing.Point(196, 9)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(181, 36)
        Me.btnQuit.TabIndex = 7
        Me.btnQuit.Text = "載入並離開 Quit"
        '
        'pgdWaferMap
        '
        Me.pgdWaferMap.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdWaferMap.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdWaferMap.CommandsForeColor = System.Drawing.Color.White
        Me.pgdWaferMap.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdWaferMap.HelpVisible = False
        Me.pgdWaferMap.LineColor = System.Drawing.Color.Blue
        Me.pgdWaferMap.Location = New System.Drawing.Point(642, 0)
        Me.pgdWaferMap.Margin = New System.Windows.Forms.Padding(0)
        Me.pgdWaferMap.Name = "pgdWaferMap"
        Me.pgdWaferMap.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.layoutWaferMap.SetRowSpan(Me.pgdWaferMap, 2)
        Me.pgdWaferMap.Size = New System.Drawing.Size(642, 438)
        Me.pgdWaferMap.TabIndex = 31
        Me.pgdWaferMap.ToolbarVisible = False
        Me.pgdWaferMap.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdWaferMap.ViewForeColor = System.Drawing.Color.White
        '
        'dlvMapDieList
        '
        Me.dlvMapDieList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dlvMapDieList.BackColor = System.Drawing.Color.Transparent
        Me.dlvMapDieList.DieListContextMenuStrip = Nothing
        Me.dlvMapDieList.DieListViewType = CType(iTVisionService.DisplayLib.DieListViewType.Type2Vertical, iTVisionService.DisplayLib.DieListViewType)
        Me.dlvMapDieList.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dlvMapDieList.IsViewDefectImage = False
        Me.dlvMapDieList.IsViewProperty = True
        Me.dlvMapDieList.Location = New System.Drawing.Point(0, 60)
        Me.dlvMapDieList.Margin = New System.Windows.Forms.Padding(0)
        Me.dlvMapDieList.Name = "dlvMapDieList"
        Me.dlvMapDieList.Size = New System.Drawing.Size(642, 378)
        Me.dlvMapDieList.TabIndex = 2
        '
        'mvwMapView
        '
        Me.mvwMapView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mvwMapView.BackColor = System.Drawing.Color.Transparent
        Me.mvwMapView.CircleIndentation = 0.5R
        Me.layoutWaferMap.SetColumnSpan(Me.mvwMapView, 2)
        Me.mvwMapView.IsDrawMapCircle = False
        Me.mvwMapView.IsDrawMapIndex = True
        Me.mvwMapView.IsDrawNGDie = True
        Me.mvwMapView.IsDrawNGFeature = False
        Me.mvwMapView.IsReverseMapIndexColumn = False
        Me.mvwMapView.IsReverseMapIndexRow = False
        Me.mvwMapView.IsViewDefectImage = False
        Me.mvwMapView.IsViewMapInformation = True
        Me.mvwMapView.Location = New System.Drawing.Point(0, 438)
        Me.mvwMapView.MapCircleColor = System.Drawing.Color.Thistle
        Me.mvwMapView.MapDefectCircleColor = System.Drawing.Color.MediumPurple
        Me.mvwMapView.MapDieRectangleColor = System.Drawing.Color.Blue
        Me.mvwMapView.MapImageContextMenuStrip = Me.mnuMapView
        Me.mvwMapView.MapInformationFont = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mvwMapView.MapInformationText = ""
        Me.mvwMapView.MapSelectedDefectCircleColor = System.Drawing.Color.Purple
        Me.mvwMapView.MapSelectedDefectColor = System.Drawing.Color.Maroon
        Me.mvwMapView.MapSelectedDieColor = System.Drawing.Color.Yellow
        Me.mvwMapView.Margin = New System.Windows.Forms.Padding(0)
        Me.mvwMapView.Name = "mvwMapView"
        Me.mvwMapView.Size = New System.Drawing.Size(1284, 567)
        Me.mvwMapView.SourceImageContextMenuStrip = Nothing
        Me.mvwMapView.TabIndex = 1
        '
        'mnuMapView
        '
        Me.mnuMapView.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuMapView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEffectiveToInvalid, Me.mnuInvalidToEffective})
        Me.mnuMapView.Name = "MenuDisplay"
        Me.mnuMapView.Size = New System.Drawing.Size(277, 52)
        '
        'mnuEffectiveToInvalid
        '
        Me.mnuEffectiveToInvalid.Name = "mnuEffectiveToInvalid"
        Me.mnuEffectiveToInvalid.Size = New System.Drawing.Size(276, 24)
        Me.mnuEffectiveToInvalid.Text = "EffectiveDie To InvalidDie"
        '
        'mnuInvalidToEffective
        '
        Me.mnuInvalidToEffective.Name = "mnuInvalidToEffective"
        Me.mnuInvalidToEffective.Size = New System.Drawing.Size(276, 24)
        Me.mnuInvalidToEffective.Text = "Invalid Die To Effective Die"
        '
        'frmWaferMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1284, 1005)
        Me.Controls.Add(Me.layoutWaferMap)
        Me.Name = "frmWaferMap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmWaferMap"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.layoutWaferMap.ResumeLayout(False)
        Me.pnlCommand.ResumeLayout(False)
        Me.mnuMapView.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents layoutWaferMap As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents mvwMapView As iTVisionService.DisplayLib.usrMapView
    Friend WithEvents dlvMapDieList As iTVisionService.DisplayLib.usrDieListView
    Friend WithEvents pgdWaferMap As System.Windows.Forms.PropertyGrid
    Friend WithEvents mnuMapView As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuEffectiveToInvalid As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInvalidToEffective As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlCommand As System.Windows.Forms.Panel
    Private WithEvents btnCancel As iTVisionService.ButtonLib.CButton
    Private WithEvents btnSave As iTVisionService.ButtonLib.CButton
    Private WithEvents btnQuit As iTVisionService.ButtonLib.CButton
End Class
