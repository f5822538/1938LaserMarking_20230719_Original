Public Enum SendCommand As Integer
    Send_No = 0

    HandshakeRead = 11
    HandshakeSendLotInfoACK = 12
    'HandshakeSendStripMapDownloadACK = 13
    HandshakeSendStripMapUpload = 14

    CameraExposure = 21
    CameraGain = 22
    CameraSaveImage = 23
    CameraCodeReader = 24
    CodeReaderCameraExposure = 25
    CodeReaderCameraGain = 26
    CodeReaderCameraSaveImage = 27
    CodeReader = 28
End Enum

Public Class frmIntegrateTest

    Private moMyEquipment As CMyEquipment
    Private moCodeReaderImageID As MIL_ID
    Private moLog As II_LogTraceExtend
    Private moLogCamera As II_LogTraceExtend
    Private moLogCodeReaderCamera As II_LogTraceExtend
    Private moCanvasCamera As II_iTVCanvas
    Private moCanvasCodeReaderCamera As II_iTVCanvas
    Private mnSendCommand As SendCommand = SendCommand.Send_No
    Private moTabName As String = ""

    Private moHandshakeType As HandshakeType = HandshakeType.NA

    Private mnMBlockAddress As Integer = 1
    Private mnDBlockAddress As Integer = 1
    Private mnDBlockValue As Short = 1
    Private mdGetExposCamera As Double = 1
    Private mdGetGainCamera As Double = 1
    Private mdSetExposCamera As Double = 1
    Private mdSetGainCamera As Double = 1
    Private mdGetExposCodeReaderCamera As Double = 1
    Private mdGetGainCodeReaderCamera As Double = 1
    Private mdSetExposCodeReaderCamera As Double = 1
    Private mdSetGainCodeReaderCamera As Double = 1

    Public Sub New(oMyEquipment As CMyEquipment)
        InitializeComponent()

        moMyEquipment = oMyEquipment
        moLog = moMyEquipment.LogManual
    End Sub

    Private Sub frmIntegrateTest_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            moLogCamera = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "Camera Test", rtxtCamera, Nothing, True)
            moLogCodeReaderCamera = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "Code Reader Camera Test", rtxtCodeReaderCamera, Nothing, True)
            moCanvasCamera = picCameraView
            moCanvasCodeReaderCamera = picCodeReaderCameraView

            If moMyEquipment.Camera.Camera IsNot Nothing Then
                moCanvasCamera.SetDisplayImage(moMyEquipment.Camera.Camera.BitmapImage(True))

                If moMyEquipment.Camera.CameraLightControl IsNot Nothing Then
                    txtCameraMaxExpos.Text = moMyEquipment.Camera.CameraLightControl.ExposureMax.ToString
                    txtCameraMinExpos.Text = moMyEquipment.Camera.CameraLightControl.ExposureMin.ToString
                End If

                moMyEquipment.Camera.Camera.DigitalGainGet(mdGetGainCamera)
                txtCameraCurrentGain.Text = mdGetGainCamera.ToString()

                If AreaCameraLib.IsNullCamera(moMyEquipment.Camera.Camera) = True Then
                    btnCameraExposure.Enabled = False
                    btnCameraGain.Enabled = False
                Else
                    mdGetExposCamera = moMyEquipment.Camera.CameraLightControl.GetExposure
                    txtCameraCurrentExpos.Text = mdGetExposCamera.ToString()
                End If
            End If

            If moMyEquipment.CodeReaderCamera.Camera IsNot Nothing Then
                moCanvasCodeReaderCamera.SetDisplayImage(moMyEquipment.CodeReaderCamera.Camera.BitmapImage(True))
                moCanvasCodeReaderCamera.SetCanvasDrawing(moMyEquipment.MainRecipe.RecipeCamera)
                moCanvasCodeReaderCamera.IsDrawDefineRecipeCase2 = True

                If moMyEquipment.CodeReaderCamera.CameraLightControl IsNot Nothing Then
                    txtCodeReaderCameraMaxExpos.Text = moMyEquipment.CodeReaderCamera.CameraLightControl.ExposureMax.ToString
                    txtCodeReaderCameraMinExpos.Text = moMyEquipment.CodeReaderCamera.CameraLightControl.ExposureMin.ToString
                End If

                moMyEquipment.CodeReaderCamera.Camera.DigitalGainGet(mdGetGainCodeReaderCamera)
                txtCodeReaderCameraCurrentGain.Text = mdGetGainCodeReaderCamera.ToString()

                If AreaCameraLib.IsNullCamera(moMyEquipment.CodeReaderCamera.Camera) = True Then
                    btnCodeReaderCameraExposure.Enabled = False
                    btnCodeReaderCameraGain.Enabled = False
                Else
                    mdGetExposCodeReaderCamera = moMyEquipment.CodeReaderCamera.CameraLightControl.GetExposure
                    txtCodeReaderCameraCurrentExpos.Text = mdGetExposCodeReaderCamera.ToString()
                End If
            End If

            ToolStripButtonLinkProductList.Enabled = True
            ToolStripButtonUnlinkProductList.Enabled = False
            ToolStripButtonAddProduct.Enabled = False
            ToolStripButtonClearProduct.Enabled = False

            Call usr8In8Out.UpdateInputListAndOutPutList(CLogCreateor.CreateLogToLogExtend(moLog), moMyEquipment.DIO3208.InPutList, moMyEquipment.DIO3208.OutPutList)
            Call usr8In8Out.UpdateTitleList(moMyEquipment.DIO3208.InPutNameList, moMyEquipment.DIO3208.OutputNameList)

            Call bkUpdate.RunWorkerAsync()
            Call bkCommand.RunWorkerAsync()
        Catch ex As Exception
            Call moLog.LogError(String.Format("整合測試初始錯誤，Error：{0}", ex.ToString))
        End Try
    End Sub

    Private Sub frmIntegrateTest_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        Call moLogCamera.DisableDisplay()
        Call bkUpdate.CancelAsync()
        Call bkCommand.CancelAsync()
    End Sub

    Private Sub btnQuit_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnQuit.ClickButtonArea
        Call Me.Close()
    End Sub

    Private Sub ToolStripButtonRead_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonRead.Click
        mnSendCommand = SendCommand.HandshakeRead
    End Sub

    Private Sub Send_ToolStripButtonRead()
        If moMyEquipment.Handshake IsNot Nothing AndAlso moSelectProduct IsNot Nothing Then
            Try
                BindingSourceProduct.DataSource = Nothing
                Call moMyEquipment.Read(moHandshakeType, moMyEquipment.ProductList, moMyEquipment.LotRecipeID, moLog)
                BindingSourceProduct.DataSource = moMyEquipment.ProductList
            Catch ex As Exception
                moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub ToolStripButtonSendLotInfoACK_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonSendLotInfoACK.Click
        mnSendCommand = SendCommand.HandshakeSendLotInfoACK
    End Sub

    Private Sub Send_ToolStripButtonSendLotInfoACK()
        If moMyEquipment.Handshake IsNot Nothing AndAlso moSelectProduct IsNot Nothing Then
            Try
                Call moMyEquipment.SendLotInfoACK(AlarmCode.IsOK, "ACK", moLog)
            Catch ex As Exception
                moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    'Private Sub ToolStripButtonSendStripMapDownloadACK_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonSendStripMapDownloadACK.Click
    '    mnSendCommand = SendCommand.HandshakeSendStripMapDownloadACK
    'End Sub

    'Private Sub Send_ToolStripButtonSendStripMapDownloadACK()
    '    If moMyEquipment.Handshake IsNot Nothing AndAlso moSelectProduct IsNot Nothing Then
    '        Try
    '            moMyEquipment.SendStripMapDownloadACK(AlarmCode.IsOK, moSelectProduct, moLog)
    '        Catch ex As Exception
    '            moLog.LogError(ex.ToString)
    '        End Try
    '    End If
    'End Sub

    Private Sub ToolStripButtonSendStripMapUpload_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonSendStripMapUpload.Click
        mnSendCommand = SendCommand.HandshakeSendStripMapUpload
    End Sub

    ''' <summary>
    ''' 發送-工具條按鈕發送上傳產品分布
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Send_ToolStripButtonSendStripMapUpload()
        If moMyEquipment.Handshake IsNot Nothing AndAlso moSelectProduct IsNot Nothing Then
            Try
                moMyEquipment.SendStripMapUpload(moSelectProduct, moLog) 'TCP 發送上傳產品分布
            Catch ex As Exception
                moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub ToolStripButtonLinkProductList_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonLinkProductList.Click
        BindingSourceProduct.DataSource = moMyEquipment.ProductList
        ToolStripButtonLinkProductList.Enabled = False
        ToolStripButtonUnlinkProductList.Enabled = True
        ToolStripButtonAddProduct.Enabled = True
        ToolStripButtonClearProduct.Enabled = moMyEquipment.ProductList.Count > 0
    End Sub

    Private Sub ToolStripButtonUnlinkProductList_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonUnlinkProductList.Click
        BindingSourceProduct.DataSource = Nothing
        ToolStripButtonLinkProductList.Enabled = True
        ToolStripButtonUnlinkProductList.Enabled = False
        ToolStripButtonAddProduct.Enabled = False
        ToolStripButtonClearProduct.Enabled = False
    End Sub

    Private Sub ToolStripButtonAddProduct_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonAddProduct.Click
        Dim nIndex As Integer = 0
        Dim oProduct As New CMyProduct
        If dgvProduct.SelectedCells.Count > 0 Then nIndex = dgvProduct.SelectedCells(0).RowIndex + 1
        BindingSourceProduct.DataSource = Nothing
        moMyEquipment.ProductList.Insert(nIndex, oProduct)
        BindingSourceProduct.DataSource = moMyEquipment.ProductList
        BindingSourceProduct.Position = nIndex
        ToolStripButtonClearProduct.Enabled = moMyEquipment.ProductList.Count > 0
    End Sub

    Private Sub ToolStripButtonClearProduct_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonClearProduct.Click
        If BindingSourceProduct.Position >= 0 Then
            Call BindingSourceProduct.RemoveAt(BindingSourceProduct.Position)
            Call dgvProduct.Refresh()
        Else
            Call MsgBox("請先點選產品資訊！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        End If
        ToolStripButtonClearProduct.Enabled = moMyEquipment.ProductList.Count > 0
    End Sub

    Private Sub ToolStripButtonLoadProductConfig_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonLoadProductConfig.Click
        Dim oLoadImage As Bitmap = Nothing
        Dim oOpenFileDialog As OpenFileDialog = New OpenFileDialog()
        oOpenFileDialog.CheckFileExists = True
        oOpenFileDialog.InitialDirectory = Application.StartupPath
        oOpenFileDialog.Filter = "Config files (*.INI)|*.INI|All files (*.*)|*.*"
        oOpenFileDialog.Multiselect = False

        If oOpenFileDialog.ShowDialog() = DialogResult.OK Then
            If File.Exists(oOpenFileDialog.FileName) = True Then
                pgdHandshake.SelectedObject = Nothing
                BindingSourceMarkInfo.DataSource = Nothing
                Dim oProductConfig As New CMyProductConfig(oOpenFileDialog.FileName.Replace("\" & oOpenFileDialog.SafeFileName, ""), oOpenFileDialog.SafeFileName.Replace(".INI", ""), "INI")
                oProductConfig.LoadConfig()
                oProductConfig.CopyTo(moSelectProduct)
                pgdHandshake.SelectedObject = moSelectProduct
                BindingSourceMarkInfo.DataSource = moSelectProduct.MarkList
                dgvProduct.Refresh()
                pgdHandshake.Refresh()
                dgvMarkInfo.Refresh()
                Call MsgBox("檔案載入完成！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Else
                Call moMyEquipment.MyLog.LogSystem.LogError("檔案載入失敗，檔案不存在")
                Call MsgBox("檔案載入失敗，檔案不存在", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            End If
        End If
    End Sub

    Private Sub ToolStripButtonSaveProductConfig_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonSaveProductConfig.Click
        Dim oSaveFileDialog As New SaveFileDialog()

        oSaveFileDialog.Filter = "Config files (*.INI)|*.INI|All files (*.*)|*.*"

        If oSaveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim sFileName As String = Mid(oSaveFileDialog.FileName, InStrRev(oSaveFileDialog.FileName, "\") + 1)
            Dim oProductConfig As New CMyProductConfig(oSaveFileDialog.FileName.Replace("\" & sFileName, ""), sFileName.Replace(".INI", ""), "INI")
            moSelectProduct.CopyTo(oProductConfig)
            oProductConfig.SaveConfig()
            Call MsgBox("檔案儲存完成！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        End If
    End Sub

    Public moSelectProduct As New CMyProduct

    Private Sub BindingSourceProduct_ListChanged(sender As System.Object, e As System.ComponentModel.ListChangedEventArgs) Handles BindingSourceProduct.ListChanged
        pgdHandshake.SelectedObject = Nothing
        BindingSourceMarkInfo.DataSource = Nothing
        If BindingSourceProduct.Position >= 0 Then
            moSelectProduct = moMyEquipment.ProductList.Item(BindingSourceProduct.Position)
            BindingSourceMarkInfo.DataSource = moSelectProduct.MarkList
            pgdHandshake.SelectedObject = moSelectProduct
        End If
        pgdHandshake.Refresh()
    End Sub

    Private Sub BindingSourceProduct_PositionChanged(sender As System.Object, e As System.EventArgs) Handles BindingSourceProduct.PositionChanged
        pgdHandshake.SelectedObject = Nothing
        BindingSourceMarkInfo.DataSource = Nothing
        If BindingSourceProduct.Position >= 0 Then
            moSelectProduct = moMyEquipment.ProductList.Item(BindingSourceProduct.Position)
            BindingSourceMarkInfo.DataSource = moSelectProduct.MarkList
            pgdHandshake.SelectedObject = moSelectProduct
        End If
        pgdHandshake.Refresh()
    End Sub

    Private Sub pgdHandshake_PropertyValueChanged(s As System.Object, e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgdHandshake.PropertyValueChanged
        dgvProduct.Refresh()
    End Sub

    Private moSelectMarkInfo As CMyMarkInfo

    Private Sub BindingSourceMarkInfo_ListChanged(sender As System.Object, e As System.ComponentModel.ListChangedEventArgs) Handles BindingSourceMarkInfo.ListChanged
        pgdMarkInfo.SelectedObject = Nothing
        If BindingSourceMarkInfo.Position >= 0 Then
            moSelectMarkInfo = moSelectProduct.MarkList.Item(BindingSourceMarkInfo.Position)
            pgdMarkInfo.SelectedObject = moSelectMarkInfo
        End If
        pgdMarkInfo.Refresh()
    End Sub

    Private Sub BindingSourceMarkInfo_PositionChanged(sender As System.Object, e As System.EventArgs) Handles BindingSourceMarkInfo.PositionChanged
        pgdMarkInfo.SelectedObject = Nothing
        If BindingSourceMarkInfo.Position >= 0 Then
            moSelectMarkInfo = moSelectProduct.MarkList.Item(BindingSourceMarkInfo.Position)
            pgdMarkInfo.SelectedObject = moSelectMarkInfo
        End If
        pgdMarkInfo.Refresh()
    End Sub

    Private Sub pgdMarkInfo_PropertyValueChanged(s As System.Object, e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgdMarkInfo.PropertyValueChanged
        dgvMarkInfo.Refresh()
    End Sub

    Private Sub btnCameraExposure_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCameraExposure.ClickButtonArea
        mnSendCommand = SendCommand.CameraExposure
    End Sub

    Private Sub Send_btnCameraExposure()
        If moMyEquipment.Camera.Camera IsNot Nothing AndAlso moMyEquipment.Camera.CameraLightControl IsNot Nothing AndAlso txtCameraExpos.Text IsNot "" Then
            Try
                moMyEquipment.Camera.CameraLightControl.ChangeExposure(mdSetExposCamera)
                mdGetExposCamera = moMyEquipment.Camera.CameraLightControl.GetExposure()
            Catch ex As Exception
                moLogCamera.LogError(ex.ToString)
                moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnCameraGain_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCameraGain.ClickButtonArea
        mnSendCommand = SendCommand.CameraGain
    End Sub

    Private Sub Send_btnCameraGain()
        If moMyEquipment.Camera.Camera IsNot Nothing AndAlso moMyEquipment.Camera.CameraLightControl IsNot Nothing AndAlso txtCameraGain.Text IsNot "" Then
            Try
                moMyEquipment.Camera.Camera.DigitalGainSet(mdSetGainCamera)
                moMyEquipment.Camera.Camera.DigitalGainGet(mdGetGainCamera)
            Catch ex As Exception
                moLogCamera.LogError(ex.ToString)
                moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnCameraSaveImage_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCameraSaveImage.ClickButtonArea
        mnSendCommand = SendCommand.CameraSaveImage
    End Sub

    Private Sub Send_btnCameraSaveImage()
        If moMyEquipment.Camera.Camera IsNot Nothing Then
            Try
                moMyEquipment.Camera.Snap(-1, "檢測相機", moLogCamera)

                If IO.Directory.Exists(Application.StartupPath & "\TestImage") = False Then
                    IO.Directory.CreateDirectory(Application.StartupPath & "\TestImage")
                End If
                Dim oDataTime As DateTime = DateTime.Now

                Dim sFileName As String = String.Format("{0}\TestImage\{1:yyyy-MM-dd_HH_mm_ss}.bmp", Application.StartupPath, oDataTime)
                Dim oSaveBitmap As New Bitmap(moMyEquipment.Camera.Camera.CameraWidth, moMyEquipment.Camera.Camera.CameraHeight)

                moLogCamera.LogInformation(String.Format("Save {0} Success", sFileName))
                moLog.LogInformation(String.Format("Save {0} Success", sFileName))

                Using oGC As Graphics = Graphics.FromImage(oSaveBitmap)
                    Call oGC.DrawImage(moMyEquipment.Camera.Camera.BitmapImage(True), New Rectangle(0, 0, moMyEquipment.Camera.Camera.CameraWidth, moMyEquipment.Camera.Camera.CameraHeight))
                    Try
                        Call oSaveBitmap.Save(sFileName, Imaging.ImageFormat.Bmp)
                    Catch ex As Exception
                        Call moLogCamera.LogError(ex.ToString)
                        Call moLog.LogError(ex.ToString)
                    End Try
                End Using

                Call oSaveBitmap.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub btnCameraCodeReader_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCameraCodeReader.ClickButtonArea
        mnSendCommand = SendCommand.CameraCodeReader
    End Sub

    Private Sub Send_btnCameraCodeReader()
        If moMyEquipment.Camera.Camera IsNot Nothing Then
            Try
                moMyEquipment.Camera.Snap(-1, "檢測相機", moLogCamera)

                If moMyEquipment.BuildImageForCopy(moMyEquipment.Camera.Camera.BitmapImage(True), moMyEquipment.ImageID, moMyEquipment.ImageHeader, moMyEquipment.Sequence, moLogCamera) = False Then
                    Call moLog.LogError("取像失敗")
                    Exit Sub
                End If

                If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then
                    Dim oAlarmCode As AlarmCode = moMyEquipment.FindForInspect(moMyEquipment.ImageID, moMyEquipment.MainRecipe.RecipeCamera.CodeReaderForInspect, moLogCamera)
                    If oAlarmCode <> AlarmCode.IsOK Then
                        oAlarmCode = moMyEquipment.FindForInspect2(moMyEquipment.ImageID, moMyEquipment.MainRecipe.RecipeCamera.CodeReaderForInspect2, moLogCamera)
                    End If
                    If oAlarmCode <> AlarmCode.IsOK Then
                        Call moLog.LogError("讀取條碼失敗")
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                Call moLogCamera.LogError(ex.ToString)
                Call moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnCodeReaderCameraExposure_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCodeReaderCameraExposure.ClickButtonArea
        mnSendCommand = SendCommand.CodeReaderCameraExposure
    End Sub

    Private Sub Send_btnCodeReaderCameraExposure()
        If moMyEquipment.CodeReaderCamera.Camera IsNot Nothing AndAlso moMyEquipment.CodeReaderCamera.CameraLightControl IsNot Nothing AndAlso txtCodeReaderCameraExpos.Text IsNot "" Then
            Try
                moMyEquipment.CodeReaderCamera.CameraLightControl.ChangeExposure(mdSetExposCodeReaderCamera)
                mdGetExposCodeReaderCamera = moMyEquipment.CodeReaderCamera.CameraLightControl.GetExposure()
            Catch ex As Exception
                moLogCodeReaderCamera.LogError(ex.ToString)
                moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnCodeReaderCameraGain_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCodeReaderCameraGain.ClickButtonArea
        mnSendCommand = SendCommand.CodeReaderCameraGain
    End Sub

    Private Sub Send_btnCodeReaderCameraGain()
        If moMyEquipment.CodeReaderCamera.Camera IsNot Nothing AndAlso moMyEquipment.CodeReaderCamera.CameraLightControl IsNot Nothing AndAlso txtCodeReaderCameraGain.Text IsNot "" Then
            Try
                moMyEquipment.CodeReaderCamera.Camera.DigitalGainSet(mdSetGainCodeReaderCamera)
                moMyEquipment.CodeReaderCamera.Camera.DigitalGainGet(mdGetGainCodeReaderCamera)
            Catch ex As Exception
                moLogCodeReaderCamera.LogError(ex.ToString)
                moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnCodeReaderCameraSaveImage_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCodeReaderCameraSaveImage.ClickButtonArea
        mnSendCommand = SendCommand.CodeReaderCameraSaveImage
    End Sub

    Private Sub Send_btnCodeReaderCameraSaveImage()
        If moMyEquipment.CodeReaderCamera.Camera IsNot Nothing Then
            Try
                moMyEquipment.CodeReaderCamera.Snap(-1, "條碼相機", moLogCodeReaderCamera)

                If IO.Directory.Exists(Application.StartupPath & "\TestImage") = False Then
                    IO.Directory.CreateDirectory(Application.StartupPath & "\TestImage")
                End If
                Dim oDataTime As DateTime = DateTime.Now

                Dim sFileName As String = String.Format("{0}\TestImage\{1:yyyy-MM-dd_HH_mm_ss}.bmp", Application.StartupPath, oDataTime)
                Dim oSaveBitmap As New Bitmap(moMyEquipment.CodeReaderCamera.Camera.CameraWidth, moMyEquipment.CodeReaderCamera.Camera.CameraHeight)

                moLogCodeReaderCamera.LogInformation(String.Format("Save {0} Success", sFileName))
                moLog.LogInformation(String.Format("Save {0} Success", sFileName))

                Using oGC As Graphics = Graphics.FromImage(oSaveBitmap)
                    Call oGC.DrawImage(moMyEquipment.CodeReaderCamera.Camera.BitmapImage(True), New Rectangle(0, 0, moMyEquipment.CodeReaderCamera.Camera.CameraWidth, moMyEquipment.CodeReaderCamera.Camera.CameraHeight))
                    Try
                        Call oSaveBitmap.Save(sFileName, Imaging.ImageFormat.Bmp)
                    Catch ex As Exception
                        Call moLogCodeReaderCamera.LogError(ex.ToString)
                        Call moLog.LogError(ex.ToString)
                    End Try
                End Using

                Call oSaveBitmap.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub btnCodeReader_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnCodeReader.ClickButtonArea
        mnSendCommand = SendCommand.CodeReader
    End Sub

    Private Sub Send_btnCodeReader()
        If moMyEquipment.CodeReaderCamera.Camera IsNot Nothing Then
            Try
                moMyEquipment.CodeReaderCamera.Snap(-1, "條碼相機", moLogCodeReaderCamera)

                If moMyEquipment.BuildImageForCopy(moMyEquipment.CodeReaderCamera.Camera.BitmapImage(True), moCodeReaderImageID, moMyEquipment.CodeReaderImageHeader, moMyEquipment.Sequence, moLogCodeReaderCamera) = False Then
                    Call moLog.LogError("取像失敗")
                    Exit Sub
                End If

                If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then
                    Dim oAlarmCode As AlarmCode = moMyEquipment.Find(moCodeReaderImageID, moMyEquipment.MainRecipe.RecipeCamera.CodeReader, moLogCodeReaderCamera)
                    If oAlarmCode <> AlarmCode.IsOK Then
                        Call moLog.LogError("讀取條碼失敗")
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                Call moLogCodeReaderCamera.LogError(ex.ToString)
                Call moLog.LogError(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub tabIntegrateTest_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tabIntegrateTest.SelectedIndexChanged
        moTabName = tabIntegrateTest.SelectedTab.Name

        Call moMyEquipment.Camera.SnapStop(-1, "檢測相機", moLogCamera)
        Call moMyEquipment.CodeReaderCamera.SnapStop(-1, "條碼相機", moLogCodeReaderCamera)
        Select Case True
            Case tabIntegrateTest.SelectedTab Is tabCamera : moMyEquipment.Camera.SnapStart(-1, "檢測相機", moLogCamera)
            Case tabIntegrateTest.SelectedTab Is tabCodeReaderCamera : moMyEquipment.CodeReaderCamera.SnapStart(-1, "條碼相機", moLogCodeReaderCamera)
        End Select
    End Sub

    Private Sub txtCameraExpos_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCameraExpos.TextChanged
        Try
            mdSetExposCamera = CInt(txtCameraExpos.Text)
        Catch ex As Exception
            txtCameraExpos.Text = mdSetExposCamera.ToString()
        End Try
    End Sub

    Private Sub txtCameraGain_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCameraGain.TextChanged
        Try
            mdSetGainCamera = CInt(txtCameraGain.Text)
        Catch ex As Exception
            txtCameraGain.Text = mdSetGainCamera.ToString()
        End Try
    End Sub

    Private Sub txtCodeReaderCameraExpos_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCodeReaderCameraExpos.TextChanged
        Try
            mdSetExposCodeReaderCamera = CInt(txtCodeReaderCameraExpos.Text)
        Catch ex As Exception
            txtCodeReaderCameraExpos.Text = mdSetExposCodeReaderCamera.ToString()
        End Try
    End Sub

    Private Sub txtCodeReaderCameraGain_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCodeReaderCameraGain.TextChanged
        Try
            mdSetGainCodeReaderCamera = CInt(txtCodeReaderCameraGain.Text)
        Catch ex As Exception
            txtCodeReaderCameraGain.Text = mdSetGainCodeReaderCamera.ToString()
        End Try
    End Sub

    Private Sub bkUpdate_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bkUpdate.DoWork
        Dim oWorker As BackgroundWorker = CType(sender, BackgroundWorker)

        While True
            If oWorker.CancellationPending = True Then
                e.Cancel = True
                Exit While
            End If

            Call Thread.Sleep(500)

            Try
                oWorker.ReportProgress(0)
            Catch ex As System.Exception
            End Try
        End While
    End Sub

    Private Sub bkUpdate_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bkUpdate.ProgressChanged
        Try
            Select Case True
                Case tabIntegrateTest.SelectedTab Is tabIO
                    If moMyEquipment.HardwareConfig.IOBypass = False Then usr8In8Out.UpdateStatus()

                Case tabIntegrateTest.SelectedTab Is tabHandshake
                    Select Case moHandshakeType
                        Case HandshakeType.NA : ToolStripTextBoxHandshakeType.Text = ""
                        Case HandshakeType.LotInfo : ToolStripTextBoxHandshakeType.Text = EnumHelper.GetDescription(HandshakeType.LotInfo)
                            'Case HandshakeType.StripMapDownload : ToolStripTextBoxHandshakeType.Text = EnumHelper.GetDescription(HandshakeType.StripMapDownload)
                        Case HandshakeType.StripMapUpload : ToolStripTextBoxHandshakeType.Text = EnumHelper.GetDescription(HandshakeType.StripMapUpload)
                    End Select

                Case tabIntegrateTest.SelectedTab Is tabCamera
                    txtCameraCurrentExpos.Text = mdGetExposCamera.ToString()
                    txtCameraCurrentGain.Text = mdGetGainCamera.ToString()

                Case tabIntegrateTest.SelectedTab Is tabCodeReaderCamera
                    txtCodeReaderCameraCurrentExpos.Text = mdGetExposCodeReaderCamera.ToString()
                    txtCodeReaderCameraCurrentGain.Text = mdGetGainCodeReaderCamera.ToString()
            End Select
        Catch ex As Exception
            Call moLog.LogError(String.Format("元件顯示錯誤，Error：{0}", ex.ToString))
            Call Me.Close()
        End Try
    End Sub

    Private Sub bkCommand_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bkCommand.DoWork
        Dim oWorker As BackgroundWorker = CType(sender, BackgroundWorker)

        While True
            If oWorker.CancellationPending = True Then
                e.Cancel = True
                Exit While
            End If

            Try
                If ProcessSend() = True Then oWorker.ReportProgress(0)
            Catch ex As System.Exception
            End Try

            Call Thread.Sleep(1000)
        End While
    End Sub

    Private Sub bkCommand_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bkCommand.ProgressChanged
        Try
            Select Case True
                Case tabIntegrateTest.SelectedTab Is tabHandshake
                    Call pgdHandshake.Refresh()

                Case tabIntegrateTest.SelectedTab Is tabCamera
                    Try
                        Call moCanvasCamera.UpdateCanvas()
                        Call picCameraView.Refresh()
                        txtCameraFocus.Text = CImageBaseCore.ForcsCalculate(20, CShort(moMyEquipment.Camera.Camera.CameraWidth), CShort(moMyEquipment.Camera.Camera.CameraHeight), moMyEquipment.Camera.Camera.ImagePtr(True)).ToString()
                    Catch ex As Exception
                        Call moLogCamera.LogError(String.Format("Camera 取像失敗，Error：{0}", ex.ToString))
                        Call moLog.LogError(String.Format("Camera 取像失敗，Error：{0}", ex.ToString))
                    End Try

                Case tabIntegrateTest.SelectedTab Is tabCodeReaderCamera
                    Try
                        Call moCanvasCodeReaderCamera.UpdateCanvas()
                        Call picCodeReaderCameraView.Refresh()
                        txtCodeReaderCameraFocus.Text = CImageBaseCore.ForcsCalculate(20, CShort(moMyEquipment.CodeReaderCamera.Camera.CameraWidth), CShort(moMyEquipment.CodeReaderCamera.Camera.CameraHeight), moMyEquipment.CodeReaderCamera.Camera.ImagePtr(True)).ToString()
                    Catch ex As Exception
                        Call moLogCodeReaderCamera.LogError(String.Format("CodeReaderCamera 取像失敗，Error：{0}", ex.ToString))
                        Call moLog.LogError(String.Format("CodeReaderCamera 取像失敗，Error：{0}", ex.ToString))
                    End Try
            End Select
        Catch ex As Exception
            Call moLog.LogError(String.Format("元件顯示錯誤，Error：{0}", ex.ToString))
            MsgBox(String.Format("元件顯示錯誤，Error：{0}", ex.ToString), MsgBoxStyle.OkOnly, "警告")
            Call Me.Close()
        End Try
    End Sub

    Private Function ProcessSend() As Boolean
        Dim sCommand As SendCommand = mnSendCommand
        If mnSendCommand <> SendCommand.Send_No Then mnSendCommand = SendCommand.Send_No

        Try
            Select Case sCommand
                Case SendCommand.HandshakeRead : Call Send_ToolStripButtonRead()
                Case SendCommand.HandshakeSendLotInfoACK : Call Send_ToolStripButtonSendLotInfoACK()
                    'Case SendCommand.HandshakeSendStripMapDownloadACK : Call Send_ToolStripButtonSendStripMapDownloadACK()
                Case SendCommand.HandshakeSendStripMapUpload : Call Send_ToolStripButtonSendStripMapUpload()

                Case SendCommand.CameraExposure : Call Send_btnCameraExposure()
                Case SendCommand.CameraGain : Call Send_btnCameraGain()
                Case SendCommand.CameraSaveImage : Call Send_btnCameraSaveImage()
                Case SendCommand.CameraCodeReader : Call Send_btnCameraCodeReader()

                Case SendCommand.CodeReaderCameraExposure : Call Send_btnCodeReaderCameraExposure()
                Case SendCommand.CodeReaderCameraGain : Call Send_btnCodeReaderCameraGain()
                Case SendCommand.CodeReaderCameraSaveImage : Call Send_btnCodeReaderCameraSaveImage()
                Case SendCommand.CodeReader : Call Send_btnCodeReader()

                Case Else
                    Select Case True
                        Case moTabName = tabCamera.Name
                            Try
                                Call moMyEquipment.Camera.Snap(-1, "檢測相機", moLogCamera)
                            Catch ex As Exception
                                Call moLogCamera.LogError(String.Format("Camera 取像失敗，Error：{0}", ex.ToString))
                                Call moLog.LogError(String.Format("Camera 取像失敗，Error：{0}", ex.ToString))
                                Return False
                            End Try
                            Return True
                        Case moTabName = tabCodeReaderCamera.Name
                            Try
                                Call moMyEquipment.CodeReaderCamera.Snap(-1, "條碼相機", moLogCodeReaderCamera)
                            Catch ex As Exception
                                Call moLogCodeReaderCamera.LogError(String.Format("Code Reader Camera 取像失敗，Error：{0}", ex.ToString))
                                Call moLog.LogError(String.Format("Code Reader Camera 取像失敗，Error：{0}", ex.ToString))
                                Return False
                            End Try
                            Return True

                        Case Else : Return False
                    End Select
            End Select
        Catch ex As Exception
            Call moLog.LogError(String.Format("呼叫函式失敗錯誤，Error：{0}", ex.ToString))
            MsgBox(String.Format("呼叫函式失敗錯誤，Error：{0}", ex.ToString), MsgBoxStyle.OkOnly, "警告")
        End Try

        Return True
    End Function

    Private Sub picCameraView_SendMouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs, cGrey As System.Drawing.Color) Handles picCameraView.SendMouseMove
        usrStatusCamera.MyLable.Text = String.Format("X = {0:d6}, Y = {1:d6} C = {2:d3}", e.X, e.Y, cGrey.G)
    End Sub

    Private Sub UpdateLEDStatus(oLED As LEDLable, oInput As Boolean)
        Call oLED.SetColor(If(oInput, Color.Lime, Color.Black))
    End Sub
End Class