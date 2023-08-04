<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRecipeList
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BindingSourceFile = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridFile = New System.Windows.Forms.DataGridView()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreationTimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastWriteTimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnQuit = New iTVisionService.ButtonLib.CButton()
        Me.btnRecipeDelete = New iTVisionService.ButtonLib.CButton()
        Me.btnRecipeAdd = New iTVisionService.ButtonLib.CButton()
        Me.btnLoadRecipe = New iTVisionService.ButtonLib.CButton()
        Me.btnSaveAs = New iTVisionService.ButtonLib.CButton()
        CType(Me.BindingSourceFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BindingSourceFile
        '
        Me.BindingSourceFile.DataSource = GetType(System.IO.FileInfo)
        '
        'DataGridFile
        '
        Me.DataGridFile.AllowUserToAddRows = False
        Me.DataGridFile.AllowUserToDeleteRows = False
        Me.DataGridFile.AutoGenerateColumns = False
        Me.DataGridFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridFile.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameDataGridViewTextBoxColumn, Me.CreationTimeDataGridViewTextBoxColumn, Me.LastWriteTimeDataGridViewTextBoxColumn})
        Me.DataGridFile.DataSource = Me.BindingSourceFile
        Me.DataGridFile.Location = New System.Drawing.Point(2, 3)
        Me.DataGridFile.MultiSelect = False
        Me.DataGridFile.Name = "DataGridFile"
        Me.DataGridFile.ReadOnly = True
        Me.DataGridFile.RowTemplate.Height = 24
        Me.DataGridFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridFile.Size = New System.Drawing.Size(504, 440)
        Me.DataGridFile.TabIndex = 1
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NameDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.NameDataGridViewTextBoxColumn.HeaderText = "設定檔案名稱"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.NameDataGridViewTextBoxColumn.Width = 140
        '
        'CreationTimeDataGridViewTextBoxColumn
        '
        Me.CreationTimeDataGridViewTextBoxColumn.DataPropertyName = "CreationTime"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.CreationTimeDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.CreationTimeDataGridViewTextBoxColumn.HeaderText = "建立日期"
        Me.CreationTimeDataGridViewTextBoxColumn.Name = "CreationTimeDataGridViewTextBoxColumn"
        Me.CreationTimeDataGridViewTextBoxColumn.ReadOnly = True
        Me.CreationTimeDataGridViewTextBoxColumn.Width = 200
        '
        'LastWriteTimeDataGridViewTextBoxColumn
        '
        Me.LastWriteTimeDataGridViewTextBoxColumn.DataPropertyName = "LastWriteTime"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Format = "yyyy-MM-dd HH:mm:ss"
        Me.LastWriteTimeDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.LastWriteTimeDataGridViewTextBoxColumn.HeaderText = "修改日期"
        Me.LastWriteTimeDataGridViewTextBoxColumn.Name = "LastWriteTimeDataGridViewTextBoxColumn"
        Me.LastWriteTimeDataGridViewTextBoxColumn.ReadOnly = True
        Me.LastWriteTimeDataGridViewTextBoxColumn.Width = 200
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
        Me.btnQuit.Location = New System.Drawing.Point(512, 171)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(290, 36)
        Me.btnQuit.TabIndex = 6
        Me.btnQuit.Text = "離開 Quit"
        '
        'btnRecipeDelete
        '
        Me.btnRecipeDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnRecipeDelete.Corners.All = 10
        Me.btnRecipeDelete.Corners.LowerLeft = 10
        Me.btnRecipeDelete.Corners.LowerRight = 10
        Me.btnRecipeDelete.Corners.UpperLeft = 10
        Me.btnRecipeDelete.Corners.UpperRight = 10
        Me.btnRecipeDelete.DesignerSelected = False
        Me.btnRecipeDelete.ImageIndex = 0
        Me.btnRecipeDelete.Location = New System.Drawing.Point(512, 45)
        Me.btnRecipeDelete.Name = "btnRecipeDelete"
        Me.btnRecipeDelete.Size = New System.Drawing.Size(290, 36)
        Me.btnRecipeDelete.TabIndex = 7
        Me.btnRecipeDelete.Text = "刪除製程參數 Delete Recipe"
        '
        'btnRecipeAdd
        '
        Me.btnRecipeAdd.BackColor = System.Drawing.Color.Transparent
        Me.btnRecipeAdd.Corners.All = 10
        Me.btnRecipeAdd.Corners.LowerLeft = 10
        Me.btnRecipeAdd.Corners.LowerRight = 10
        Me.btnRecipeAdd.Corners.UpperLeft = 10
        Me.btnRecipeAdd.Corners.UpperRight = 10
        Me.btnRecipeAdd.DesignerSelected = False
        Me.btnRecipeAdd.ImageIndex = 0
        Me.btnRecipeAdd.Location = New System.Drawing.Point(512, 3)
        Me.btnRecipeAdd.Name = "btnRecipeAdd"
        Me.btnRecipeAdd.Size = New System.Drawing.Size(290, 36)
        Me.btnRecipeAdd.TabIndex = 8
        Me.btnRecipeAdd.Text = "新增製程參數 Add New Recipe"
        '
        'btnLoadRecipe
        '
        Me.btnLoadRecipe.BackColor = System.Drawing.Color.Transparent
        Me.btnLoadRecipe.Corners.All = 10
        Me.btnLoadRecipe.Corners.LowerLeft = 10
        Me.btnLoadRecipe.Corners.LowerRight = 10
        Me.btnLoadRecipe.Corners.UpperLeft = 10
        Me.btnLoadRecipe.Corners.UpperRight = 10
        Me.btnLoadRecipe.DesignerSelected = False
        Me.btnLoadRecipe.ImageIndex = 0
        Me.btnLoadRecipe.Location = New System.Drawing.Point(512, 87)
        Me.btnLoadRecipe.Name = "btnLoadRecipe"
        Me.btnLoadRecipe.Size = New System.Drawing.Size(290, 36)
        Me.btnLoadRecipe.TabIndex = 9
        Me.btnLoadRecipe.Text = "載入製程參數 Load Recipe"
        '
        'btnSaveAs
        '
        Me.btnSaveAs.BackColor = System.Drawing.Color.Transparent
        Me.btnSaveAs.Corners.All = 10
        Me.btnSaveAs.Corners.LowerLeft = 10
        Me.btnSaveAs.Corners.LowerRight = 10
        Me.btnSaveAs.Corners.UpperLeft = 10
        Me.btnSaveAs.Corners.UpperRight = 10
        Me.btnSaveAs.DesignerSelected = True
        Me.btnSaveAs.ImageIndex = 0
        Me.btnSaveAs.Location = New System.Drawing.Point(512, 129)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(290, 36)
        Me.btnSaveAs.TabIndex = 10
        Me.btnSaveAs.Text = "另存新檔SaveAs Recipe"
        '
        'FrmRecipeList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(807, 455)
        Me.Controls.Add(Me.btnSaveAs)
        Me.Controls.Add(Me.btnLoadRecipe)
        Me.Controls.Add(Me.btnRecipeDelete)
        Me.Controls.Add(Me.btnRecipeAdd)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.DataGridFile)
        Me.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRecipeList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "製程管理 Recipe Manager"
        Me.TopMost = True
        CType(Me.BindingSourceFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents BindingSourceFile As System.Windows.Forms.BindingSource
    Private WithEvents DataGridFile As System.Windows.Forms.DataGridView
    Private WithEvents btnQuit As iTVisionService.ButtonLib.CButton
    Private WithEvents btnRecipeDelete As iTVisionService.ButtonLib.CButton
    Private WithEvents btnRecipeAdd As iTVisionService.ButtonLib.CButton
    Private WithEvents btnLoadRecipe As iTVisionService.ButtonLib.CButton
    Private WithEvents btnSaveAs As iTVisionService.ButtonLib.CButton
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreationTimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastWriteTimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
