<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecipeCodeReader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipeCodeReader))
        Me.picView = New iTVisionService.usrDisplay()
        Me.layoutCamera = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlCommand = New System.Windows.Forms.Panel()
        Me.btnCancel = New iTVisionService.ButtonLib.CButton()
        Me.btnSave = New iTVisionService.ButtonLib.CButton()
        Me.btnQuit = New iTVisionService.ButtonLib.CButton()
        Me.pgdCode = New System.Windows.Forms.PropertyGrid()
        Me.usrStatusCamera = New RecipeLib.CStatusCameraForMain()
        Me.mnuMark = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuCodeSearchROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCodeClearROI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModelDiff = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuApplyModelRegion1St = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearModelRegion1St = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModelDiffLine1St = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuApplySearchRegion = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearSearchRegion = New System.Windows.Forms.ToolStripMenuItem()
        Me.layoutCamera.SuspendLayout()
        Me.pnlCommand.SuspendLayout()
        Me.mnuMark.SuspendLayout()
        Me.mnuModelDiff.SuspendLayout()
        Me.SuspendLayout()
        '
        'picView
        '
        Me.picView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picView.BackColor = System.Drawing.Color.Transparent
        Me.layoutCamera.SetColumnSpan(Me.picView, 2)
        Me.picView.DetectValueOnMouseLocation = True
        Me.picView.Location = New System.Drawing.Point(3, 153)
        Me.picView.Name = "picView"
        Me.picView.Size = New System.Drawing.Size(851, 597)
        Me.picView.TabIndex = 3
        '
        'layoutCamera
        '
        Me.layoutCamera.ColumnCount = 2
        Me.layoutCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.layoutCamera.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCamera.Controls.Add(Me.pnlCommand, 0, 0)
        Me.layoutCamera.Controls.Add(Me.pgdCode, 1, 0)
        Me.layoutCamera.Controls.Add(Me.picView, 0, 1)
        Me.layoutCamera.Controls.Add(Me.usrStatusCamera, 0, 2)
        Me.layoutCamera.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutCamera.Location = New System.Drawing.Point(0, 0)
        Me.layoutCamera.Margin = New System.Windows.Forms.Padding(5)
        Me.layoutCamera.Name = "layoutCamera"
        Me.layoutCamera.RowCount = 4
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutCamera.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.layoutCamera.Size = New System.Drawing.Size(857, 813)
        Me.layoutCamera.TabIndex = 4
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
        Me.pnlCommand.Size = New System.Drawing.Size(394, 144)
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
        'pgdCode
        '
        Me.pgdCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgdCode.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pgdCode.CommandsForeColor = System.Drawing.Color.White
        Me.pgdCode.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.pgdCode.HelpVisible = False
        Me.pgdCode.LineColor = System.Drawing.Color.Blue
        Me.pgdCode.Location = New System.Drawing.Point(403, 3)
        Me.pgdCode.Name = "pgdCode"
        Me.pgdCode.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.pgdCode.Size = New System.Drawing.Size(451, 144)
        Me.pgdCode.TabIndex = 30
        Me.pgdCode.ToolbarVisible = False
        Me.pgdCode.ViewBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pgdCode.ViewForeColor = System.Drawing.Color.White
        '
        'usrStatusCamera
        '
        Me.usrStatusCamera.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.layoutCamera.SetColumnSpan(Me.usrStatusCamera, 2)
        Me.usrStatusCamera.Dock = System.Windows.Forms.DockStyle.None
        Me.usrStatusCamera.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.usrStatusCamera.Location = New System.Drawing.Point(0, 753)
        Me.usrStatusCamera.Name = "usrStatusCamera"
        Me.usrStatusCamera.Size = New System.Drawing.Size(857, 30)
        Me.usrStatusCamera.TabIndex = 1
        Me.usrStatusCamera.Text = "StatusStrip1"
        '
        'mnuMark
        '
        Me.mnuMark.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.mnuMark.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCodeSearchROI, Me.mnuCodeClearROI})
        Me.mnuMark.Name = "MenuDisplay"
        Me.mnuMark.Size = New System.Drawing.Size(175, 74)
        '
        'mnuCodeSearchROI
        '
        Me.mnuCodeSearchROI.Name = "mnuCodeSearchROI"
        Me.mnuCodeSearchROI.Size = New System.Drawing.Size(174, 24)
        Me.mnuCodeSearchROI.Text = "尋找範圍 ROI"
        '
        'mnuCodeClearROI
        '
        Me.mnuCodeClearROI.Name = "mnuCodeClearROI"
        Me.mnuCodeClearROI.Size = New System.Drawing.Size(174, 24)
        Me.mnuCodeClearROI.Text = "清除 ROI"
        '
        'mnuModelDiff
        '
        Me.mnuModelDiff.Font = New System.Drawing.Font("微軟正黑體", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136,Byte))
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
        'frmRecipeCodeReader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10!, 20!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(857, 813)
        Me.Controls.Add(Me.layoutCamera)
        Me.Font = New System.Drawing.Font("微軟正黑體", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmRecipeCodeReader"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "銓發科技股份有限公司 - 參數設定"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.layoutCamera.ResumeLayout(false)
        Me.layoutCamera.PerformLayout
        Me.pnlCommand.ResumeLayout(false)
        Me.mnuMark.ResumeLayout(false)
        Me.mnuModelDiff.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents picView As iTVisionService.usrDisplay
    Friend WithEvents layoutCamera As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlCommand As System.Windows.Forms.Panel
    Private WithEvents btnSave As iTVisionService.ButtonLib.CButton
    Private WithEvents btnQuit As iTVisionService.ButtonLib.CButton
    Friend WithEvents mnuMark As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents LineCountDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefectSizeRangeMaxDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AverageGrey1stDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AverageGrey2ndDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SearchIndentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NeedSearchDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents usrStatusCamera As RecipeLib.CStatusCameraForMain
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
    Friend WithEvents pgdCode As System.Windows.Forms.PropertyGrid
    Friend WithEvents mnuCodeSearchROI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCodeClearROI As System.Windows.Forms.ToolStripMenuItem
End Class
