Imports MyHandleResult = System.Tuple(Of Boolean, String)
Imports iTVisionService.DisplayLib
Imports RecipeLib

Public Class frmMain

    Private Const LOGIN As String = "41 - 使用者登入 User Log In"
    Private Const LOGOUT As String = "41 - 使用者登出 User Log Out"
    Public Const MapZoom As Integer = -1 '' Augustin 220726 Add for Wafer Map

    Private moHardwareConfig As CHardwareConfig
    Private WithEvents moMyEquipment As CMyEquipment
    Private moMainRecipe As CMainRecipe
    Private WithEvents moAutoRunThread As CAutoRunThread
    Private WithEvents moHandshakeThread As CHandshakeThread
    Private moLog As II_LogTraceExtend = CLogCreateorExtend.CreateDebugLog

    Private moCanvas As II_iTVCanvas
    Private moCanvasLocate1 As II_iTVCanvas
    Private moCanvasLocate2 As II_iTVCanvas
    Private moSync As SynchronizationContext

    Private mnCounter As Integer = 0

    '' Augustin 220726 Add for Wafer Map
    Private moMap As CMyMap
    Private moSelectedDefect As iTVisionService.DisplayLib.CMyDefect

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim aTact As New CTactTimeSpan

        Try
            '-------------------------測試-開始--------------------------
            If Debugger.IsAttached = True Then
                Dim locater1 As New CMyLocater(moMyEquipment) '定位孔-1
                Dim testBitMap1 = New Bitmap("D:\ASE_ProgramReleaseReport_202307\SourceImage\20220822_103727.735.Bmp")
                Dim aRectangle As Rectangle = Rectangle.FromLTRB(0, 0, testBitMap1.Width, testBitMap1.Height)
                Dim findChangeModelResult1 = locater1.FindChangeModel(testBitMap1, aRectangle, 0)
                Dim findResult1 = locater1.Find(testBitMap1)
            End If
            '-------------------------測試-結束--------------------------

            moSync = SynchronizationContext.Current
            moHardwareConfig = New CHardwareConfig(Application.StartupPath & "\Setup", "HardwareConfig", "INI")
            Call moHardwareConfig.LoadConfig()

            moMainRecipe = New CMainRecipe(String.Format("{0}\Recipe", Application.StartupPath), moHardwareConfig.MiscConfig.DefectSizeType = DefectSizeType.DefectAnd, moHardwareConfig.CameraConfig.PixelSize)
            moMyEquipment = New CMyEquipment(moHardwareConfig, moMainRecipe, moSync)

            With moMyEquipment.MyLog
                .LogSystem = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "System", rtxtSystem, Nothing, True)
                .LogProcess = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "Process", rtxtProcess, Nothing, True)
                .LogControl = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "Control", rtxtControl, Nothing, True)
                .LogAlarm = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "Alarm", rtxtAlarm, Nothing, True)
                .LogHandshake = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "Handshake", rtxtHandshake, Nothing, True)
                .LogManual = CLogCreateorExtend.CreateSimpleDisplayLog(Application.StartupPath, "Manual", rtxtManual, Nothing, True)
                .LogInspectCSV = CLogCreateor.CreateCSVFileLog(Application.StartupPath & "\CSV", "Inspect", Nothing)
            End With
            moLog = moMyEquipment.LogSystem

            Call aTact.CalSpan()
            Call moLog.LogInformation(String.Format("載入設定資料，時間：[{0:F4}]ms", aTact.CurrentSpan))
            Call aTact.ReSetTime()

            Call moMyEquipment.Initial()
            Call moMyEquipment.SetSystemCache()
            Call aTact.CalSpan()
            Call moLog.Log(LOGHandle.HANDLE_CREATE, String.Format("初始化硬體，時間：[{0:F4}]ms", aTact.CurrentSpan))
            Call aTact.ReSetTime()

            moAutoRunThread = moMyEquipment.InnerThread.AutoRunThread
            moHandshakeThread = moMyEquipment.InnerThread.HandshakeThread
            moMyEquipment.InnerThread.StartThread()
            Call aTact.CalSpan()
            Call moLog.Log(LOGHandle.HANDLE_CREATE, String.Format("開啟緒程，時間：[{0:F4}]ms", aTact.CurrentSpan))
            Call aTact.ReSetTime()

            Call aTact.CalSpan()
            With InitialLoadImage()
                If .Item1 = False Then
                    If moHardwareConfig.Debug = False Then
                        MsgBox(.Item2)
                    End If
                    moLog.LogError(.Item2)
                Else
                    moLog.LogInformation(.Item2)
                End If
            End With

            '' Augustin 220726 Add for Wafer Map
            Call moMyEquipment.WaferMap.SetMapView(mvwMapView)
            Call moMyEquipment.WaferMap.SetListView(dlvMapDieList)
            Call moMyEquipment.WaferMap.SetListView(dlvMapDefectList)

            picView.ContextMenuStrip = mnuDrawImage
            mnuLogInOut.Text = LOGIN
            Call moMyEquipment.UpdateSizeDgvCodeReadResult(dgvCodeReadResult, 1)
            Call UpdateTitle()
            Call moMyEquipment.UpdatePoolSetting() '' Augustin 220726 Add for Wafer Map
            Call bkUpdate.RunWorkerAsync()
            Call bkTime.RunWorkerAsync()
            Call moLog.LogInformation(Me.Text)
        Catch ex As System.Exception
            Call moLog.LogError(ex.ToString)
        End Try
        Call moLog.LogInformation(String.Format("系統啟動完成，時間：[{0:F4}]ms", aTact.CurrentSpan))
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("是否確定關閉？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.No Then
            e.Cancel = True
        Else
            On Error Resume Next
            Call DestoryThread()
            Call bkUpdate.CancelAsync()
            Call bkTime.CancelAsync()
            Call moMyEquipment.Close()
            Call moMyEquipment.UserProfileFile.SaveConfig("")
            Call moMyEquipment.DisableDisplay()
            Call Thread.Sleep(500)
        End If
    End Sub

    Private Sub btnSingleRun_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnSingleRun.ClickButtonArea
        Try
            If moHardwareConfig.HandshakeBypass = True Then
                BindingSourceProduct.DataSource = Nothing
                moMyEquipment.ProductList.Clear()
                'moMyEquipment.Handshake.BuildLotInfo("<Transaction Name=""LotInfo"" Type=""Request""><LotID>Lot0000001</LotID><RecipeID>A123456789</RecipeID></Transaction>", moMyEquipment.ProductLotID)
                'moMyEquipment.Handshake.BuildLotInfo("<Transaction Name=""LotInfo"" Type=""Request""><LotID>Lot0000001</LotID><RecipeID>A123456789</RecipeID><StepName>xxxxStepNamexx</StepName><Prodline>xxxProdlinexxx</Prodline><Floor>xxxFloorxxx</Floor><EQPID>xxxEQPIDxxx</EQPID><EQPtype>xxxEQPtypexxx</EQPtype><StripIDList><StripID>AAAAAAA123456789</StripID><StripID>BBBBBBB123456789</StripID><StripID>CCCCCCC123456789</StripID></StripIDList></Transaction>", moMyEquipment.ProductList, moMyEquipment.LotRecipeID)
                moMyEquipment.Handshake.BuildLotInfo("<Transaction Name=""LotInfo"" Type=""Request""><LotID>Lot0000001</LotID><RecipeID>A123456789</RecipeID><StepName>xxxxStepNamexx</StepName><Prodline>xxxProdlinexxx</Prodline><Floor>xxxFloorxxx</Floor><EQPID>xxxEQPIDxxx</EQPID><EQPtype>xxxEQPtypexxx</EQPtype><StripIDList><StripID>V22222222F6KM</StripID><StripID>V22222222F6KA</StripID><StripID>V22222222F6KB</StripID></StripIDList></Transaction>", moMyEquipment.ProductList, moMyEquipment.LotRecipeID)

                'Dim sText As String

                'Dim sText As String = "<Transaction Name=""StripMapDownload"" Type=""Request""><MapData xmlns=""urn:semi-org:xsd.E142-1.V1005.SubstrateMap""><Layouts><Layout LayoutId=""Strip"" DefaultUnits=""mm""><Dimension X=""1"" Y=""1""/></Layout><Layout LayoutId=""SubMatrix"" DefaultUnits=""mm""><Dimension X=""1"" Y=""1""/></Layout><Layout LayoutId=""Device"" DefaultUnits=""mm""><Dimension X=""36"" Y=""11""/></Layout></Layouts><SubstrateMaps><SubstrateMap SubstrateType=""Strip"" SubstrateId=""K612345678901003"" LayoutSpecifier=""Strip/SubMatrix/Device"" SubstrateSide=""TopSide"" OriginLocation=""LowerLeft"" Orientation=""0"" AxisDirection=""UpRight""><Overlay MapName=""BinCodeMap"" MapVersion=""1""><ReferenceDevices><ReferenceDevice Name=""FirstDevice""><Coordinates X=""0"" Y=""0""/></ReferenceDevice></ReferenceDevices><BinCodeMap BinType=""Integer2"" NullBin=""0000""><BinDefinitions><BinDefinition BinCode=""0100"" BinCount=""385"" BinQuality=""GOOD"" Pick=""true""/><BinDefinition BinCode=""010F"" BinCount=""11"" BinQuality=""FAIL""/></BinDefinitions><BinCode>010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode></BinCodeMap></Overlay></SubstrateMap></SubstrateMaps></MapData></Transaction>"
                'moMyEquipment.Handshake.BuildStripMapNoDieDownload(sText, moMyEquipment.ProductList(0))
                'Dim sText As String = "<Transaction Name=""StripMapDownload"" Type=""Request""><MapData xmlns=""urn:semi-org:xsd.E142-1.V1005.SubstrateMap""><Layouts><Layout LayoutId=""Strip"" DefaultUnits=""mm""><Dimension X=""1"" Y=""1""/></Layout><Layout LayoutId=""SubMatrix"" DefaultUnits=""mm""><Dimension X=""1"" Y=""1""/></Layout><Layout LayoutId=""Device"" DefaultUnits=""mm""><Dimension X=""36"" Y=""11""/></Layout></Layouts><SubstrateMaps><SubstrateMap SubstrateType=""Strip"" SubstrateId=""K612345678901003"" LayoutSpecifier=""Strip/SubMatrix/Device"" SubstrateSide=""TopSide"" OriginLocation=""LowerLeft"" Orientation=""0"" AxisDirection=""UpRight""><Overlay MapName=""BinCodeMap"" MapVersion=""1""><ReferenceDevices><ReferenceDevice Name=""FirstDevice""><Coordinates X=""0"" Y=""0""/></ReferenceDevice></ReferenceDevices><BinCodeMap BinType=""Integer2"" NullBin=""0000""><BinDefinitions><BinDefinition BinCode=""0100"" BinCount=""385"" BinQuality=""GOOD"" Pick=""true""/><BinDefinition BinCode=""010F"" BinCount=""11"" BinQuality=""FAIL""/></BinDefinitions><BinCode>010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode><BinCode>010F01000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100010001000100</BinCode></BinCodeMap></Overlay></SubstrateMap></SubstrateMaps></MapData></Transaction>"
                'moMyEquipment.Handshake.BuildStripMapDownload(sText, moMyEquipment.ProductProcess, moMyEquipment.ProductLotID)
                BindingSourceProduct.DataSource = moMyEquipment.ProductList
                Call dgvProduct.Refresh()
            End If
            moMyEquipment.InnerThread.StartSingleRun()
        Catch ex As System.Exception
            moLog.LogError(ex.ToString)
        End Try
    End Sub

    Private Sub btnContinusRun_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnContinusRun.ClickButtonArea
        Try
            moMyEquipment.InnerThread.StartContinusRun()
        Catch ex As Exception
            moLog.LogError(ex.ToString)
        End Try
    End Sub

    Private Sub btnTestRun_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnTestRun.ClickButtonArea
        Try
            moMyEquipment.InnerThread.StartTestRun()
        Catch ex As Exception
            moLog.LogError(ex.ToString)
        End Try
    End Sub

    Private Sub btnStop_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnStop.ClickButtonArea
        moMyEquipment.InnerThread.StopProcess()
    End Sub

    Private Sub btnClearAlarm_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnClearAlarm.ClickButtonArea
        moMyEquipment.ClearAlarm()
    End Sub

    Private Sub btnLoadImage_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnLoadImage.ClickButtonArea
        Dim oLoadImage As Bitmap = Nothing
        Dim oOpenFileDialog As OpenFileDialog = New OpenFileDialog()
        oOpenFileDialog.CheckFileExists = True
        oOpenFileDialog.InitialDirectory = Application.StartupPath
        oOpenFileDialog.Filter = "Image Files (*.bmp)|*.bmp|Image files (*.jpg)|*.jpg|All files (*.*)|*.*"
        oOpenFileDialog.Multiselect = False

        If oOpenFileDialog.ShowDialog() = DialogResult.OK Then
            If File.Exists(oOpenFileDialog.FileName) = True Then
                Call moMyEquipment.Camera.Camera.LoadFromFile(oOpenFileDialog.FileName, True)
                Call moMyEquipment.Camera.Camera.LoadFromFile(oOpenFileDialog.FileName, False)
                tbxLoadImage.Text = oOpenFileDialog.FileName
            Else
                Call moMyEquipment.MyLog.LogSystem.LogError("相機開啟失敗，檔案不存在")
                Call MsgBox("相機開啟失敗，檔案不存在", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            End If
        End If
    End Sub

    Private moFrmRecipeList As FrmRecipeList

    Private Sub btnRecipeManager_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnRecipeManager.ClickButtonArea
        Try
            If moFrmRecipeList Is Nothing Then moFrmRecipeList = New FrmRecipeList(moMyEquipment, Application.StartupPath)

            Try
                moFrmRecipeList.ShowDialog()
            Catch ex As Exception
                moFrmRecipeList = New FrmRecipeList(moMyEquipment, Application.StartupPath)
            End Try

            moFrmRecipeList.ShowDialog()
        Catch ex As Exception

        End Try
        '' Augustin 220401 Add Try to Catch the Error
        Try
            If moMyEquipment.ImageHeader.Ptr <> IntPtr.Zero Then
                Call moCanvas.SetDisplayImage()
                Call moCanvas.SetDisplayImage(moMyEquipment.UpdateImage(moMyEquipment.ImageHeader))
                Call moCanvasLocate1.SetDisplayImage()
                Call moCanvasLocate2.SetDisplayImage()
                Call moMyEquipment.CreateMarkImage1(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate1.PatternZone, moLog)
                Call moMyEquipment.CreateMarkImage2(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate2.PatternZone, moLog)
                Call moCanvasLocate1.SetDisplayImage(moMyEquipment.RecipeMarkBitmap1)
                Call moCanvasLocate2.SetDisplayImage(moMyEquipment.RecipeMarkBitmap2)
                Call moCanvas.UpdateCanvas()
                Call moCanvasLocate1.UpdateCanvas()
                Call moCanvasLocate2.UpdateCanvas()
                tabView.SelectedTab = tabLocate
            End If
        Catch ex As Exception
            MsgBox("請確認Recipe內容是否正確", MsgBoxStyle.OkOnly)
            Call moLog.LogError(ex.ToString)
        End Try

        If moMainRecipe.RecipeID.ToUpper <> "DEFAULT" Then
            Call moAutoRunThread.UpdateImage()
            '' Augustin 220726 Add for Wafer Map
            mvwMapView.IsDrawMapIndex = True
            mvwMapView.IsDrawMapCircle = False
            mvwMapView.MapDieRectangleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDieRectangle
            mvwMapView.MapCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorMapCircle
            mvwMapView.MapSelectedDieColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDie
            mvwMapView.MapSelectedDefectColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefect
            mvwMapView.MapDefectCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDefectCircle
            mvwMapView.MapSelectedDefectCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefectCircle
            mvwMapView.IsReverseMapIndexColumn = moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexColumn
            mvwMapView.IsReverseMapIndexRow = moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexRow
            mvwMapView.IsDrawNGDie = moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGDie
            mvwMapView.IsDrawNGFeature = moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGFeature
            mvwMapView.CircleIndentation = 0
            mvwMapView.IsDrawMapIndex = True
            mvwMapView.IsDrawMapCircle = False
            Call moMyEquipment.WaferMapSetColor()
            Call moMyEquipment.WaferMapReset()
            Call mvwMapView.SetMapZoom(MapZoom)
            Call mvwMapView.Refresh()
            Call mvwMapView.UpdateMapImage()
            Call dlvMapDieList.Refresh()
        End If
    End Sub

    Private Sub btnChangeModel_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnChangeModel.ClickButtonArea
        Call moLog.LogInformation("按下 [更換樣本]")
        Call moMyEquipment.ChangeModel(moLog)
        cbxGatherStandardDeviation.Checked = True
    End Sub

    Private Sub btnOpenHistory_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnOpenHistory.ClickButtonArea
        Try
            Using aOpenFileDialog As New OpenFileDialog
                Dim oDialogResult As DialogResult
                aOpenFileDialog.Filter = "CCD CSV Log files (*.CSV)|*.CSV"
                aOpenFileDialog.InitialDirectory = Application.StartupPath & "\CSV\"
                oDialogResult = aOpenFileDialog.ShowDialog

                lstInspectHistory.Items.Clear()
                lstInspectHistory.BeginUpdate()

                If oDialogResult = Windows.Forms.DialogResult.OK Then
                    Dim aTact As New CTactTimeSpan

                    Dim sReadFile As String
                    Using oStreamReader As New System.IO.StreamReader(aOpenFileDialog.FileName, System.Text.Encoding.Default)
                        sReadFile = oStreamReader.ReadToEnd
                    End Using
                    Dim sLine() As String = Split(sReadFile, vbCrLf)

                    lstInspectHistory.AddLineList(sLine, CameraVIEWLIST.CameraVIEWLIST_RAW, CameraVIEWLIST.CameraVIEWLIST_COUNT)
                    aTact.CalSpan()
                    Call moMyEquipment.LogSystem.LogInformation(String.Format("btnOpenHistory，時間：[{0:F4}]ms", aTact.CurrentSpan))
                End If
                lstInspectHistory.EndUpdate()
            End Using
        Catch ex As System.Exception
            Call moMyEquipment.LogSystem.LogError(ex.ToString)
        End Try
    End Sub

    Private Sub mnuHardwareConfig_Click(sender As System.Object, e As System.EventArgs) Handles mnuHardwareConfig.Click
        moLog.LogInformation("按下 [00 - 系統設定 System]")
        Dim ofrmHardwareConfig As New frmHardwareConfig(moMyEquipment, moMyEquipment.LogSystem)
        ofrmHardwareConfig.ShowDialog()
        Me.Text = String.Format("{0} 版本{1} 使用者 = [{2}] Recipe = [{3}]", moHardwareConfig.Title, Application.ProductVersion.ToString, moMyEquipment.CurrentUser.UserName, moMainRecipe.RecipeID)
        Call moLog.LogInformation(Me.Text)
    End Sub

    Private mofrmRecipe As frmRecipe

    Private Sub mnuRecipe_Click(sender As System.Object, e As System.EventArgs) Handles mnuRecipe.Click

        '' Augustin 220726 Bypass for Wafer Map
        'moLog.LogInformation("按下 [10 - 參數設定 Recipe]")
        'If moMainRecipe.RecipeID.ToUpper = "DEFAULT" Then
        '    Call MsgBox("請先載入製程參數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '    Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
        '    Return
        'End If

        'If MsgBox("是否載入原設定圖檔？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
        '    If File.Exists(moMainRecipe.RecipeCamera.TempleteImagePath) = True Then
        '        moMainRecipe.RecipeCamera.ImageBeenLoad = True
        '    Else
        '        Dim sInformation As String = "參數圖檔不存在，請檢查圖檔！"
        '        Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '        Call moLog.LogError(sInformation)
        '        Return
        '    End If
        'Else
        '    If moMyEquipment.Camera IsNot Nothing AndAlso moMyEquipment.Camera.Camera IsNot Nothing Then
        '        moMainRecipe.RecipeCamera.ImageBeenLoad = False
        '    Else
        '        Dim sInformation As String = "無相機影像！"
        '        Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '        Call moLog.LogError(sInformation)
        '        Return
        '    End If
        'End If

        'Try
        '    If moMainRecipe.RecipeCamera.ImageBeenLoad = True Then
        '        If moMyEquipment.BuildImageForLoad(moMainRecipe.RecipeCamera.TempleteImagePath, moMyEquipment.RecipeID, moMyEquipment.RecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔載入失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '    Else
        '        If MsgBox("是否使用相機原始影像？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
        '            If moMyEquipment.BuildImageForCopy(moMyEquipment.Camera.Camera.BitmapImage(True), moMyEquipment.RecipeID, moMyEquipment.RecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔取得失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '        Else
        '            If moMyEquipment.BuildImageForCopy(New Bitmap(moMyEquipment.ImageHeader.Width, moMyEquipment.ImageHeader.Height, moMyEquipment.ImageHeader.Stride, PixelFormat.Format8bppIndexed, moMyEquipment.ImageHeader.Ptr), moMyEquipment.RecipeID, moMyEquipment.RecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔取得失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '        End If
        '    End If
        'Catch ex As Exception
        '    Call moLog.LogError(ex.ToString)
        'End Try

        'Call moCanvasLocate1.SetDisplayImage()
        'Call moCanvasLocate2.SetDisplayImage()

        'Try
        '    If mofrmRecipe Is Nothing Then mofrmRecipe = New frmRecipe(moMyEquipment, moLog, moMyEquipment.UpdateImage(moMyEquipment.RecipeHeader))

        '    Try
        '        mofrmRecipe.ShowDialog()
        '    Catch ex As Exception
        '        mofrmRecipe = New frmRecipe(moMyEquipment, moLog, moMyEquipment.UpdateImage(moMyEquipment.RecipeHeader))
        '        mofrmRecipe.ShowDialog()
        '    End Try
        'Catch ex As Exception
        '    Call moLog.LogError(ex.ToString)
        'End Try
        'If mofrmRecipe IsNot Nothing Then mofrmRecipe.Dispose()

        'Call moAutoRunThread.UpdateImage()

        'If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then
        '    Call moMyEquipment.CodeReaderForInspect.SetParameter(moMainRecipe.RecipeCamera.CodeReaderForInspect)
        '    Call moMyEquipment.CodeReaderForInspect2.SetParameter(moMainRecipe.RecipeCamera.CodeReaderForInspect2)
        'End If

        'Call moMyEquipment.CreateMarkImage1(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate1.PatternZone, moLog)
        'Call moMyEquipment.CreateMarkImage2(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate2.PatternZone, moLog)
        'Call moCanvasLocate1.SetDisplayImage(moMyEquipment.RecipeMarkBitmap1)
        'Call moCanvasLocate2.SetDisplayImage(moMyEquipment.RecipeMarkBitmap2)
        'tabView.SelectedTab = tabLocate

        'cbxGatherStandardDeviation.Checked = moMainRecipe.RecipeCamera.RecipeModelDiff.IsGatherStandardDeviation
        'If moMyEquipment.Camera.Camera.IsNullCamera() = False AndAlso moMyEquipment.Camera.ChangeExposure(moMainRecipe.ExposureTime, "檢測相機", moLog) = False Then MsgBox("設定 [檢測相機] 曝光時間，失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        'Call moCanvas.UpdateCanvas()
        'Call moCanvasLocate1.UpdateCanvas()
        'Call moCanvasLocate2.UpdateCanvas()
    End Sub

    Private mofrmRecipeCodeReader As frmRecipeCodeReader

    Private Sub mnuRecipeCodeReader_Click(sender As System.Object, e As System.EventArgs) Handles mnuRecipeCodeReader.Click
        moLog.LogInformation("按下 [20 - 條碼參數設定 Code]")
        If moMainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("請先載入製程參數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return
        End If

        If MsgBox("是否載入原設定圖檔？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
            If File.Exists(moMainRecipe.RecipeCamera.CodeReaderImagePath) = True Then
                moMainRecipe.RecipeCamera.ImageBeenLoad = True
            Else
                Dim sInformation As String = "參數圖檔不存在，請檢查圖檔！"
                Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                Call moLog.LogError(sInformation)
                Return
            End If
        Else
            If moMyEquipment.CodeReaderCamera IsNot Nothing AndAlso moMyEquipment.CodeReaderCamera.Camera IsNot Nothing Then
                moMainRecipe.RecipeCamera.ImageBeenLoad = False
            Else
                Dim sInformation As String = "無相機影像！"
                Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                Call moLog.LogError(sInformation)
                Return
            End If
        End If

        Try
            If moMainRecipe.RecipeCamera.ImageBeenLoad = True Then
                If moMyEquipment.BuildImageForLoad(moMainRecipe.RecipeCamera.CodeReaderImagePath, moMyEquipment.CodeReaderRecipeID, moMyEquipment.CodeReaderRecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔載入失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Else
                If moMyEquipment.BuildImageForCopy(moMyEquipment.CodeReaderCamera.Camera.BitmapImage(True), moMyEquipment.CodeReaderRecipeID, moMyEquipment.CodeReaderRecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔取得失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            End If
        Catch ex As Exception
            Call moLog.LogError(ex.ToString)
        End Try

        Try
            If mofrmRecipeCodeReader Is Nothing Then mofrmRecipeCodeReader = New frmRecipeCodeReader(moMyEquipment, moLog, moMyEquipment.UpdateImage(moMyEquipment.CodeReaderRecipeHeader))

            Try
                mofrmRecipeCodeReader.ShowDialog()
            Catch ex As Exception
                mofrmRecipeCodeReader = New frmRecipeCodeReader(moMyEquipment, moLog, moMyEquipment.UpdateImage(moMyEquipment.CodeReaderRecipeHeader))
                mofrmRecipeCodeReader.ShowDialog()
            End Try
        Catch ex As Exception
            Call moLog.LogError(ex.ToString)
        End Try
        If mofrmRecipeCodeReader IsNot Nothing Then mofrmRecipeCodeReader.Dispose()

        If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then
            Call moMyEquipment.CodeReader.SetParameter(moMainRecipe.RecipeCamera.CodeReader)
        End If

        If moMyEquipment.CodeReaderCamera.Camera.IsNullCamera() = False AndAlso moMyEquipment.CodeReaderCamera.ChangeExposure(moMainRecipe.RecipeCamera.CodeReader.CodeReaderExposureTime1, "條碼相機", moLog) = False Then MsgBox("設定 [條碼相機] 曝光時間，失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")

        moCanvas.UpdateCanvas()
    End Sub

    Private mofrmIntegrateTest As frmIntegrateTest

    Private Sub mnuUnitTest_Click(sender As System.Object, e As System.EventArgs) Handles mnuUnitTest.Click
        moLog.LogInformation("按下 [30 - 單元測試 Test]")
        Try
            BindingSourceProduct.DataSource = Nothing
            moMyEquipment.IsHandshakeCanProcess = False
            Try
                If mofrmIntegrateTest Is Nothing Then mofrmIntegrateTest = New frmIntegrateTest(moMyEquipment)
                Try
                    Call mofrmIntegrateTest.ShowDialog()
                Catch ex As Exception
                    mofrmIntegrateTest.Dispose()
                    mofrmIntegrateTest = Nothing
                    mofrmIntegrateTest = New frmIntegrateTest(moMyEquipment)
                    Call mofrmIntegrateTest.ShowDialog()
                End Try

                mofrmIntegrateTest.Dispose()
                mofrmIntegrateTest = Nothing
            Catch ex As Exception

            End Try
            moMyEquipment.IsHandshakeCanProcess = True
            If moMyEquipment.ProductList IsNot Nothing Then BindingSourceProduct.DataSource = moMyEquipment.ProductList
            Call dgvProduct.Refresh()
        Catch ex As System.Exception
            moLog.LogError(ex.ToString)
        End Try
    End Sub

    Private Sub mnuLogInOut_Click(sender As System.Object, e As System.EventArgs) Handles mnuLogInOut.Click
        If mnuLogInOut.Text = LOGIN Then
            moLog.LogInformation(String.Format("按下 [{0}]", LOGIN))
            Dim ofrmLogIn As New frmLogIn(moMyEquipment.CurrentUser, moMyEquipment.UserProfileFile)
            Call ofrmLogIn.ShowDialog()

            Select Case True
                Case moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Administrator
                    mnuLogInOut.Text = LOGOUT
                    mnuRight.Visible = True
                Case moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Engineer
                    mnuLogInOut.Text = LOGOUT
                Case moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Operator
            End Select
        Else
            moLog.LogInformation(String.Format("按下 [{0}]", LOGOUT))
            moMyEquipment.CurrentUser = New CUserProfile With {.UserName = "Default", .PassWord = "Default", .Level = USERLEVEL.USER_Operator}
            mnuLogInOut.Text = LOGIN
        End If

        Call UpdateTitle()
    End Sub

    Private Sub mnuRight_Click(sender As System.Object, e As System.EventArgs) Handles mnuRight.Click
        moLog.LogInformation("按下 [42 - 使用者管理 User Manager]")
        Dim ofrmRight As New frmRight(moMyEquipment.UserProfileFile)
        Call ofrmRight.ShowDialog()
    End Sub

    Private Sub mnuVersion_Click(sender As System.Object, e As System.EventArgs) Handles mnuVersion.Click
        moLog.LogInformation("按下 [90 - 版本資訊 Version]")
        Call LoadHistory(moMyEquipment.LogSystem)
    End Sub

    Private Sub mnuDrawRecipe_Click(sender As System.Object, e As System.EventArgs) Handles mnuDrawRecipe.Click
        moCanvas.IsDrawDefineRecipeCase0 = Not moCanvas.IsDrawDefineRecipeCase0
        moCanvas.IsDrawDefineRecipeCase1 = Not moCanvas.IsDrawDefineRecipeCase1
        moCanvas.IsDrawDefineRecipeCase3 = Not moCanvas.IsDrawDefineRecipeCase3
        moCanvas.IsDrawDefineRecipeCase4 = Not moCanvas.IsDrawDefineRecipeCase4
        moCanvas.UpdateCanvas()
    End Sub

    Private Sub mnuDrawDefect_Click(sender As System.Object, e As System.EventArgs) Handles mnuDrawDefect.Click
        moCanvas.IsDrawDefectCase0 = Not moCanvas.IsDrawDefectCase0
        moCanvas.UpdateCanvas()
    End Sub

    Private Sub mnuDrawModel_Click(sender As System.Object, e As System.EventArgs) Handles mnuDrawModel.Click
        moCanvas.IsDrawDefectCase1 = Not moCanvas.IsDrawDefectCase1
        moCanvas.UpdateCanvas()
    End Sub

    Private Sub mnuDrawModelCenter_Click(sender As System.Object, e As System.EventArgs) Handles mnuDrawModelCenter.Click
        moCanvas.IsDrawDefectCase2 = Not moCanvas.IsDrawDefectCase2
        moCanvas.UpdateCanvas()
    End Sub

    Private Sub mnuSaveImage_Click(sender As System.Object, e As System.EventArgs) Handles mnuSaveImage.Click
        Dim sPath As String = Application.StartupPath & "\SourceImage"
        Dim oDate As DateTime = DateTime.Now
        Dim sFileName As String = String.Format("{0}\{1:d4}{2:d2}{3:d2}_{4:d2}{5:d2}{6:d2}.{7:d3}.Bmp", sPath, oDate.Year, oDate.Month, oDate.Day, oDate.Hour, oDate.Minute, oDate.Second, oDate.Millisecond)
        If Directory.Exists(sPath) = False Then Directory.CreateDirectory(sPath)
        moMyEquipment.UpdateImage(moMyEquipment.ImageHeader).Save(sFileName, Imaging.ImageFormat.Bmp)
    End Sub

    Private Sub mnuSaveStandardDeviationModelImage_Click(sender As System.Object, e As System.EventArgs) Handles mnuSaveStandardDeviationModelImage.Click
        moLog.LogInformation("按下 [儲存標準差樣本]")
        If moMainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("請先載入製程參數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return
        End If

        SaveStandardDeviationModel(moMainRecipe.RecipeCamera.RecipeModelDiff, Application.StartupPath & "\Recipe", moMainRecipe.RecipeID)
    End Sub

    Private Sub mnuLoadStandardDeviationModelImage_Click(sender As System.Object, e As System.EventArgs) Handles mnuLoadStandardDeviationModelImage.Click
        moLog.LogInformation("按下 [載入標準差樣本]")
        If moMainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("請先載入製程參數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return
        End If

        LoadStandardDeviationModel(moMainRecipe.RecipeCamera.RecipeModelDiff, Application.StartupPath & "\Recipe", moMainRecipe.RecipeID)
    End Sub

    Private Sub mnuClearStandardDeviationModelImage_Click(sender As System.Object, e As System.EventArgs) Handles mnuClearStandardDeviationModelImage.Click
        moLog.LogInformation("按下 [載入標準差樣本]")
        If moMainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("請先載入製程參數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return
        End If

        ClearStandardDeviationModel(moMainRecipe.RecipeCamera.RecipeModelDiff)
    End Sub

    Private Sub mnuClearProduct_Click(sender As System.Object, e As System.EventArgs) Handles mnuClearProduct.Click
        Try
            If BindingSourceProduct.Position >= 0 Then
                Call BindingSourceProduct.RemoveAt(BindingSourceProduct.Position)
                Call dgvProduct.Refresh()
            Else
                Call MsgBox("請先點選產品資訊！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            End If
        Catch ex As System.Exception
            moLog.LogError(ex.ToString)
        End Try
    End Sub

    Private Sub mnuClearAllProduct_Click(sender As System.Object, e As System.EventArgs) Handles mnuClearAllProduct.Click
        Try
            If BindingSourceProduct.Position >= 0 Then
                Call BindingSourceProduct.Clear()
                Call dgvProduct.Refresh()
            End If
        Catch ex As System.Exception
            moLog.LogError(ex.ToString)
        End Try
    End Sub

    Private Sub menuMain_DoubleClick(sender As System.Object, e As System.EventArgs) Handles menuMain.DoubleClick
        'If mnuLogInOut.Text = LOGIN Then
        '    moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Administrator
        '    moMyEquipment.CurrentUser.PassWord = "iTVision"
        '    mnuLogInOut.Text = LOGOUT
        '    mnuRight.Visible = True
        '    mnuLogInOut.Text = LOGOUT
        'Else
        '    moMyEquipment.CurrentUser = New CUserProfile With {.UserName = "Default", .PassWord = "Default", .Level = USERLEVEL.USER_Operator}
        '    moMyEquipment.CurrentUser.PassWord = ""
        '    mnuLogInOut.Text = LOGIN
        '    mnuRight.Visible = False
        'End If

        'Call UpdateTitle()
    End Sub

    Private Sub cbxIsAutoChangeModel_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxIsAutoChangeModel.CheckedChanged
        moMyEquipment.IsChangeModel = cbxIsAutoChangeModel.Checked
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
                Case moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Administrator
                    CheckRoleAdministratorAttribute(Me.GetType, Me)
                    mnuHardwareConfig.Enabled = moAutoRunThread.IsRunning() = False
                    mnuRecipe.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
                    mnuRecipeCodeReader.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
                    mnuUnitTest.Enabled = moAutoRunThread.IsRunning() = False
                    'layoutLoadImage.Visible = IsNullCamera(moMyEquipment.Camera.Camera)
                    layoutMainControl.RowStyles.Item(6).Height = If(moMainRecipe.RecipeID.ToUpper <> "DEFAULT", 50, 0)
                    layoutMainControl.RowStyles.Item(7).Height = If(moMyEquipment.CurrentUser.PassWord = "iTVision" AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT", 50, 0)
                    layoutMainControl.RowStyles.Item(9).Height = 0
                    btnChangeModel.Visible = moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
                    cbxGatherStandardDeviation.Visible = moMyEquipment.CurrentUser.PassWord = "iTVision" AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
                    '' Augustin 221102 Bypass For Scroll Bar
                    'dgvProduct.Enabled = moMyEquipment.CurrentUser.PassWord = "iTVision" AndAlso moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT" AndAlso moMyEquipment.CurrentUser.PassWord = "iTVision"
                    If moMyEquipment.CurrentUser.PassWord = "iTVision" AndAlso dgvProduct.ContextMenuStrip IsNot mnuProduct Then dgvProduct.ContextMenuStrip = mnuProduct
                    mnuRight.Visible = True
                Case moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Engineer
                    CheckRoleEngineerAttribute(Me.GetType, Me)
                    mnuHardwareConfig.Enabled = False
                    mnuRecipe.Enabled = False
                    mnuRecipeCodeReader.Enabled = False
                    mnuUnitTest.Enabled = moAutoRunThread.IsRunning() = False
                    'layoutLoadImage.Visible = False
                    layoutMainControl.RowStyles.Item(6).Height = 0
                    layoutMainControl.RowStyles.Item(7).Height = 0
                    layoutMainControl.RowStyles.Item(9).Height = 0
                    btnChangeModel.Visible = False
                    cbxGatherStandardDeviation.Visible = False
                    dgvProduct.ContextMenuStrip = Nothing
                    '' Augustin 221102 Bypass For Scroll Bar
                    'dgvProduct.Enabled = False
                    mnuRight.Visible = False
                Case moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Operator
                    CheckRoleOperatorAttribute(Me.GetType, Me)
                    mnuHardwareConfig.Enabled = False
                    mnuRecipe.Enabled = False
                    mnuRecipeCodeReader.Enabled = False
                    mnuUnitTest.Enabled = False
                    'layoutLoadImage.Visible = False
                    layoutMainControl.RowStyles.Item(6).Height = 0
                    layoutMainControl.RowStyles.Item(7).Height = 0
                    layoutMainControl.RowStyles.Item(9).Height = 0
                    btnChangeModel.Visible = False
                    cbxGatherStandardDeviation.Visible = False
                    dgvProduct.ContextMenuStrip = Nothing
                    '' Augustin 221102 Bypass For Scroll Bar
                    'dgvProduct.Enabled = False
                    mnuRight.Visible = False
            End Select

            usrAlarm.RefreshStatus(moMyEquipment.IsAlarm.IsSet() = True OrElse moMyEquipment.IsErrorOn.IsSet() = True)
            usrRunning.RefreshStatus(moMyEquipment.InnerThread.Inspect.IsSet() = True)
            usrExecute.RefreshStatus(moMyEquipment.HardwareConfig.TriggerBypass = True OrElse (moMyEquipment.IO.ProductPresentSensor.IsOn() = True AndAlso moMyEquipment.IO.SafeSensor1.IsOn() = False AndAlso moMyEquipment.IO.SafeSensor2.IsOn() = False))

            btnClearAlarm.Enabled = moMyEquipment.IsAlarm.IsSet() = True OrElse moMyEquipment.IsErrorOn.IsSet() = True
            btnSingleRun.Enabled = moMyEquipment.IsAlarm.IsSet() = False AndAlso moMyEquipment.IsErrorOn.IsSet() = False AndAlso moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT" AndAlso (moHardwareConfig.TriggerBypass = True OrElse moMyEquipment.IO.HomeSensor.IsOn() = True)
            btnContinusRun.Enabled = moMyEquipment.IsAlarm.IsSet() = False AndAlso moMyEquipment.IsErrorOn.IsSet() = False AndAlso moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT" AndAlso (moHardwareConfig.TriggerBypass = True OrElse moMyEquipment.IO.HomeSensor.IsOn() = True)
            btnTestRun.Enabled = moMyEquipment.IsAlarm.IsSet() = False AndAlso moMyEquipment.IsErrorOn.IsSet() = False AndAlso moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
            btnStop.Enabled = moAutoRunThread.IsRunning() = True
            btnRecipeManager.Enabled = moAutoRunThread.IsRunning() = False
            btnChangeModel.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"

            mnuSaveStandardDeviationModelImage.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
            mnuLoadStandardDeviationModelImage.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
            mnuClearStandardDeviationModelImage.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"

            mnuClearProduct.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
            mnuClearAllProduct.Enabled = moAutoRunThread.IsRunning() = False AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
            mnuSaveStandardDeviationModelImage.Visible = moMyEquipment.CurrentUser.PassWord = "iTVision" AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
            mnuLoadStandardDeviationModelImage.Visible = moMyEquipment.CurrentUser.PassWord = "iTVision" AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"
            mnuClearStandardDeviationModelImage.Visible = moMyEquipment.CurrentUser.PassWord = "iTVision" AndAlso moMainRecipe.RecipeID.ToUpper <> "DEFAULT"

            If moMainRecipe.RecipeID.ToUpper <> "DEFAULT" Then cbxGatherStandardDeviation.Checked = moMainRecipe.RecipeCamera.RecipeModelDiff.IsGatherStandardDeviation

            Dim nYield As Integer = 0
            If moMyEquipment.YieldConfig.TotalCount > 0 Then nYield = (moMyEquipment.YieldConfig.OKCount * 100 \ moMyEquipment.YieldConfig.TotalCount)
            labYield.Text = String.Format("良率：{0}/{1} ({2}%)", moMyEquipment.YieldConfig.OKCount, moMyEquipment.YieldConfig.TotalCount, nYield)
            'Dim Die_nYield As Integer = 0
            'If moMyEquipment.YieldConfig.TotalCount_Die > 0 Then Die_nYield = (moMyEquipment.YieldConfig.OKCount_Die * 100 \ moMyEquipment.YieldConfig.TotalCount_Die)
            'labDieYield.Text = String.Format("良率：{0}/{1} ({2}%)", moMyEquipment.YieldConfig.OKCount_Die, moMyEquipment.YieldConfig.TotalCount_Die, Die_nYield)
            labModelCount.Text = String.Format("樣本數：{0}", moMainRecipe.RecipeCamera.RecipeModelDiff.SummationSquareCount)
            labProductCount.Text = String.Format("產品剩餘數量：{0}", BindingSourceProduct.Count)
            labYield.ForeColor = If(nYield >= moHardwareConfig.MiscConfig.YieldThreshold, Color.DarkSlateGray, Color.Maroon)

            If moMyEquipment.IsUpdateCodeReadResult = True Then
                moMyEquipment.IsUpdateCodeReadResult = False
                moMyEquipment.UpdateValueDgvCodeReadResult(dgvCodeReadResult)
            End If
            moCanvas.UpdateCanvas() '' Augustin 220726 Add for Wafer Map
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bkTime_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bkTime.DoWork
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

    Private Sub bkTime_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bkTime.ProgressChanged
        With Now
            labStatusDate.Text = .ToString("yyyy-MM-dd")
            labStatusTime.Text = .ToString("HH:mm:ss")
        End With
    End Sub

    Private Sub moAutoRunThread_AutoRunFinished(sender As Object, e As CAutoRunFinished) Handles moAutoRunThread.AutoRunFinished
        moSync.Send(
         Sub()
             Call usrDefectView.ClearPicture()
             Call moMainRecipe.RecipeCamera.DefectList.Clear()
             Call moMainRecipe.RecipeCamera.ModelList.Clear()
             Call moMainRecipe.RecipeCamera.ModelCenterListStart.Clear()
             Call moMainRecipe.RecipeCamera.ModelCenterListEnd.Clear()

             Call moMainRecipe.RecipeCamera.ModelList.AddRange(e.InspectSum.DefectList.ModelList)
             Call moMainRecipe.RecipeCamera.DefectList.AddRange(e.InspectSum.DefectListDraw)
             Call moMainRecipe.RecipeCamera.ModelCenterListStart.AddRange(e.InspectSum.ModelCenterListStart1St)
             Call moMainRecipe.RecipeCamera.ModelCenterListEnd.AddRange(e.InspectSum.ModelCenterListEnd1St)

             For nCount As Integer = 1 To Math.Min(4, e.InspectSum.DefectList.DefectList.Count)
                 Call usrDefectView.AddToPictureName(e.InspectSum.DefectList.DefectList(nCount - 1).DefectImage.FileName)
             Next

             Call usrDefectView.AddData(e.InspectSum)
             Call moMyEquipment.LogInspectCSV.Log(e.InspectSum.InspectResult.TOCSVLine())

             If cbxIsAutoChangeModel.Checked = True Then
                 cbxIsAutoChangeModel.Checked = False
                 tabView.SelectedTab = tabLocate
             Else
                 tabView.SelectedTab = tabResult
             End If

             If moMyEquipment.IsNotUpdateMap = False AndAlso moMyEquipment.HardwareConfig.MiscConfig.IsAutoRemoveProduct = True Then
                 Try
                     BindingSourceProduct.DataSource = Nothing
                     moMyEquipment.RemoveProduct(e.InspectSum.InspectResult.CodeID)
                     BindingSourceProduct.DataSource = moMyEquipment.ProductList
                     Call dgvProduct.Refresh()
                 Catch ex As System.Exception
                     moLog.LogError(ex.ToString)
                 End Try
             End If

             Call moCanvas.UpdateCanvas()
             Call cbxIsAutoChangeModel.Refresh()
         End Sub, "")
    End Sub

    Private Sub moHandshakeThread_ChangeRecipe(oHandshakeProductList As List(Of CMyProduct)) Handles moHandshakeThread.ChangeRecipe
        moSync.Send(
         Sub()
             If moMyEquipment.ImageHeader.Ptr <> IntPtr.Zero Then
                 Call moCanvas.SetDisplayImage()
                 Call moCanvas.SetDisplayImage(moMyEquipment.UpdateImage(moMyEquipment.ImageHeader))
                 Call moCanvasLocate1.SetDisplayImage()
                 Call moCanvasLocate2.SetDisplayImage()
                 Call moMyEquipment.CreateMarkImage1(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate1.PatternZone, moLog)
                 Call moMyEquipment.CreateMarkImage2(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate2.PatternZone, moLog)
                 Call moCanvasLocate1.SetDisplayImage(moMyEquipment.RecipeMarkBitmap1)
                 Call moCanvasLocate2.SetDisplayImage(moMyEquipment.RecipeMarkBitmap2)
                 Call moCanvas.UpdateCanvas()
                 Call moCanvasLocate1.UpdateCanvas()
                 Call moCanvasLocate2.UpdateCanvas()
                 tabView.SelectedTab = tabLocate
             End If

             cbxIsAutoChangeModel.Checked = moMyEquipment.IsChangeModel

             Call moAutoRunThread.UpdateImage()
             Call UpdateTitle()
             Call cbxIsAutoChangeModel.Refresh()
             Try
                 BindingSourceProduct.DataSource = Nothing

                 moMyEquipment.ProductList.Clear()
                 moMyEquipment.ProductList.AddRange(oHandshakeProductList)

                 BindingSourceProduct.DataSource = moMyEquipment.ProductList
                 Call dgvProduct.Refresh()
             Catch ex As System.Exception
                 moLog.LogError(ex.ToString)
             End Try
         End Sub, "")
    End Sub

    Private Sub picView_SendMouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs, cGrey As System.Drawing.Color) Handles picView.SendMouseMove
        labGray.Text = String.Format("X = {0:d6}, Y = {1:d6} C = {2:d3}", e.X, e.Y, cGrey.G)
    End Sub

    Private Sub UpdateTitle() Handles moMyEquipment.UpdateTitle
        Me.Text = String.Format("{0} 版本{1} 使用者 = [{2}] Recipe = [{3}]", moHardwareConfig.Title, Application.ProductVersion.ToString, moMyEquipment.CurrentUser.UserName, moMainRecipe.RecipeID)
        labRecipeID.Text = moMainRecipe.RecipeID
    End Sub

    Private Sub cbxGatherStandardDeviation_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxGatherStandardDeviation.CheckedChanged
        moMainRecipe.RecipeCamera.RecipeModelDiff.IsGatherStandardDeviation = cbxGatherStandardDeviation.Checked
        moMainRecipe.RecipeCamera.RecipeModelDiff.SaveIsGatherStandardDeviation()
    End Sub

    Private Sub DestoryThread()
        moMyEquipment.InnerThread.StopThread()
    End Sub

    Private Function InitialLoadImage() As MyHandleResult
        Try
            moCanvas = picView
            moCanvasLocate1 = picLocate1
            moCanvasLocate2 = picLocate2

            Call moCanvas.SelectCurrentDrawCase(RecipeDrawType.DrawNothing)
            Call moCanvas.SetCanvasDrawing(moMainRecipe.RecipeCamera)
            moCanvas.IsDrawDefineRecipeCase0 = False
            moCanvas.IsDrawDefineRecipeCase1 = False
            moCanvas.IsDrawDefineRecipeCase3 = False
            moCanvas.IsDrawDefineRecipeCase4 = False
            moCanvas.IsDrawDefectCase0 = True
            moCanvas.IsDrawDefectCase1 = False
            moCanvas.IsDrawDefectCase2 = False
        Catch ex As Exception
            moMyEquipment.LogAlarm.LogError("Initial Load Image 錯誤")
            Return Tuple.Create(False, String.Format("Initial Load Image 錯誤，Error：{0}", ex.ToString()))
        End Try

        Return Tuple.Create(True, "Initial Load Image")
    End Function

    Private Sub CButton1_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles CButton1.ClickButtonArea
        Dim sText As String = ""

        sText = "<Transaction=""LotInfo""Type=""Request""><LotID>Lot0000001</LotID><RecipeID>A123456789</RecipeID><StepName>xxxxxx</StepName><Prodline>xxxxxx</Prodline><Floor>xxxxxx</Floor><EQPID>xxxxxx</EQPID><EQPtype>xxxxxx</EQPtype><StripIDList><StripID>AAAAAAA123456789</StripID><StripID>BBBBBBB123456789</StripID><StripID>CCCCCCC123456789</StripID></StripIDList></Transaction>"

        sText = "<TransactionName=""LotInfo""Type=""Request""><LotID>Lot0000001</LotID><RecipeID>A123456789</RecipeID><StepName>xxxxxx</StepName><Prodline>xxxxxx</Prodline><Floor>xxxxxx</Floor><EQPID>xxxxxx</EQPID><EQPtype>xxxxxx</EQPtype><StripIDList><StripID>AAAAAAA123456789</StripID><StripID>BBBBBBB123456789</StripID><StripID>CCCCCCC123456789</StripID></StripIDList></Transaction>"

        sText = "<Transaction Name=""LotInfo"" Type=""Request""><LotID>Lot0000001</LotID><RecipeID>A123456789</RecipeID><StepName>xxxxxx</StepName><Prodline>xxxxxx</Prodline><Floor>xxxxxx</Floor><EQPID>xxxxxx</EQPID><EQPtype>xxxxxx</EQPtype><StripIDList><StripID>AAAAAAA123456789</StripID><StripID>BBBBBBB123456789</StripID><StripID>CCCCCCC123456789</StripID></StripIDList></Transaction>"
        Dim oXmlDoc As XmlDocument = New XmlDocument
        Dim oXmlNode As XmlNode
        Try
            sText = sText.Replace("<?xml version=""1.0""?>", "")
            oXmlDoc.LoadXml(sText)
            oXmlNode = oXmlDoc.SelectSingleNode("Transaction")

        Catch ex As Exception
            moMyEquipment.LogAlarm.LogError(String.Format("解讀交握格式錯誤，Error：{0}", ex.ToString))
        End Try

        'moMyEquipment.ProductList.Clear()
        'moMyEquipment.Handshake.BuildLotInfo("<Transaction Name=""LotInfo"" Type=""Request""><LotID>Lot0000001</LotID><RecipeID>A123456789</RecipeID><StepName>xxxxStepNamexx</StepName><Prodline>xxxProdlinexxx</Prodline><Floor>xxxFloorxxx</Floor><EQPID>xxxEQPIDxxx</EQPID><EQPtype>xxxEQPtypexxx</EQPtype><StripIDList><StripID>V22222222F6KM</StripID><StripID>V22222222F6KA</StripID><StripID>V22222222F6KB</StripID></StripIDList></Transaction>", moMyEquipment.ProductList, moMyEquipment.LotRecipeID)

        'Dim oProductList As New List(Of CMyProduct)

        'Dim oProduct As New CMyProduct

        'moMyEquipment.ProductList.Item(0).SubstrateID = "V22222222F6KM"
        'moMyEquipment.ProductList.Item(1).SubstrateID = "V22222222F6KA"
        'moMyEquipment.ProductList.Item(2).SubstrateID = "V22222222F6KB"

        'oProduct.SubstrateID = "V22222222F6KM"
        'oProduct.DimensionX = 46
        'oProduct.DimensionY = 12
        'oProductList.Add(oProduct)

        'oProduct.SubstrateID = "V22222222F6KA"
        'oProductList.Add(oProduct)

        'oProduct.SubstrateID = "V22222222F6KB"
        'oProductList.Add(oProduct)

        'moMyEquipment.Handshake.BuildStripOriginalMapInfo(moMyEquipment.HardwareConfig.MiscConfig.ReadProductXmlPath, moMyEquipment.ProductList, moMainRecipe.RecipeCamera.RecipeModelDiff)

        'Dim oReceiveTime As Date = DateTime.Now
        'Dim sPath As String = "D:\"
        'Dim oInspectResult As New CInspectResult
        'Dim oDefectList As New CMyDefectList
        'Dim oProductConfig As CMyProductConfig
        'Dim oProductInspectResultSum As CInspectSum

        'oProductConfig = New CMyProductConfig(sPath, oInspectResult.Name, "INI")
        'oProductInspectResultSum = New CInspectSum(oInspectResult, oReceiveTime, oDefectList, oProductConfig)

        'oProductInspectResultSum.ProductConfig.LotID = "9527"
        'oProductInspectResultSum.ProductConfig.StepName = "StepName9527"
        'oProductInspectResultSum.ProductConfig.Prodline = "Prodline9527"
        'oProductInspectResultSum.ProductConfig.Floor = "Floor9527"
        'oProductInspectResultSum.ProductConfig.EQPID = "EQPID9527"
        'oProductInspectResultSum.ProductConfig.EQPtype = "EQPtype9527"
        'oProductInspectResultSum.ProductConfig.SubstrateID = "asdfg"
        'oProductInspectResultSum.ProductConfig.RecipeID = "123456"

        ''oProductInspectResultSum.ProductConfig.MarkCount = 6

        'Dim DefectX As New List(Of Integer)
        'Dim DefectY As New List(Of Integer)
        'Dim OKX As New List(Of Integer)
        'Dim OKY As New List(Of Integer)
        'DefectX.AddRange({1, 2, 3})
        'DefectY.AddRange({1, 2, 2})
        'OKX.AddRange({1, 2, 3})
        'OKY.AddRange({2, 1, 1})

        ''DefectX.Add(1)
        ''DefectX.Add(2)
        ''DefectX.Add(3)
        ''DefectY.Add(1)
        ''DefectY.Add(2)
        ''DefectY.Add(2)
        ''OKX.Add(1)
        ''OKX.Add(2)
        ''OKX.Add(3)
        ''OKY.Add(2)
        ''OKY.Add(1)
        ''OKY.Add(1)

        'For Index As Integer = 0 To 2
        '    Dim oDefect As New CMyDefect
        '    Dim oOK As New CMyDefect
        '    oDefect.DefectIndex.X = DefectX(Index)
        '    oDefect.DefectIndex.Y = DefectY(Index)
        '    oProductInspectResultSum.DefectList.DefectList.Add(oDefect)
        '    oOK.DefectIndex.X = OKX(Index)
        '    oOK.DefectIndex.Y = OKY(Index)
        '    oProductInspectResultSum.DefectList.OKList.Add(oOK)
        'Next
        'oProductInspectResultSum.ReceiveTime = DateTime.Now

        'Call moMyEquipment.Handshake.UpdateAIInfo(oProduct, oProductInspectResultSum)
    End Sub

    Private Sub mnuRecipeInspect_Click(sender As System.Object, e As System.EventArgs) Handles mnuRecipeInspect.Click
        '' Augustin 220726 Add for Wafer Map
        moLog.LogInformation("按下 [10 - 參數設定 Recipe]")
        If moMainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("請先載入製程參數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return
        End If

        If MsgBox("是否載入原設定圖檔？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
            If File.Exists(moMainRecipe.RecipeCamera.TempleteImagePath) = True Then
                moMainRecipe.RecipeCamera.ImageBeenLoad = True
            Else
                Dim sInformation As String = "參數圖檔不存在，請檢查圖檔！"
                Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                Call moLog.LogError(sInformation)
                Return
            End If
        Else
            If moMyEquipment.Camera IsNot Nothing AndAlso moMyEquipment.Camera.Camera IsNot Nothing Then
                moMainRecipe.RecipeCamera.ImageBeenLoad = False
            Else
                Dim sInformation As String = "無相機影像！"
                Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                Call moLog.LogError(sInformation)
                Return
            End If
        End If

        Try
            If moMainRecipe.RecipeCamera.ImageBeenLoad = True Then
                If moMyEquipment.BuildImageForLoad(moMainRecipe.RecipeCamera.TempleteImagePath, moMyEquipment.RecipeID, moMyEquipment.RecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔載入失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Else
                If MsgBox("是否使用相機原始影像？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.Yes Then
                    If moMyEquipment.BuildImageForCopy(moMyEquipment.Camera.Camera.BitmapImage(True), moMyEquipment.RecipeID, moMyEquipment.RecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔取得失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                Else
                    If moMyEquipment.BuildImageForCopy(New Bitmap(moMyEquipment.ImageHeader.Width, moMyEquipment.ImageHeader.Height, moMyEquipment.ImageHeader.Stride, PixelFormat.Format8bppIndexed, moMyEquipment.ImageHeader.Ptr), moMyEquipment.RecipeID, moMyEquipment.RecipeHeader, -1, moMyEquipment.LogSystem) = False Then MsgBox("圖檔取得失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                End If
            End If
        Catch ex As Exception
            Call moLog.LogError(ex.ToString)
        End Try

        Call moCanvasLocate1.SetDisplayImage()
        Call moCanvasLocate2.SetDisplayImage()

        Try
            If mofrmRecipe Is Nothing Then mofrmRecipe = New frmRecipe(moMyEquipment, moLog, moMyEquipment.UpdateImage(moMyEquipment.RecipeHeader))

            Try
                mofrmRecipe.ShowDialog()
            Catch ex As Exception
                mofrmRecipe = New frmRecipe(moMyEquipment, moLog, moMyEquipment.UpdateImage(moMyEquipment.RecipeHeader))
                mofrmRecipe.ShowDialog()
            End Try
        Catch ex As Exception
            Call moLog.LogError(ex.ToString)
        End Try
        If mofrmRecipe IsNot Nothing Then mofrmRecipe.Dispose()

        Call moAutoRunThread.UpdateImage()

        If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then
            Call moMyEquipment.CodeReaderForInspect.SetParameter(moMainRecipe.RecipeCamera.CodeReaderForInspect)
            Call moMyEquipment.CodeReaderForInspect2.SetParameter(moMainRecipe.RecipeCamera.CodeReaderForInspect2)
        End If

        Call moMyEquipment.CreateMarkImage1(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate1.PatternZone, moLog)
        Call moMyEquipment.CreateMarkImage2(moMyEquipment.ImageHeader, moMainRecipe.RecipeCamera.Locate2.PatternZone, moLog)
        Call moCanvasLocate1.SetDisplayImage(moMyEquipment.RecipeMarkBitmap1)
        Call moCanvasLocate2.SetDisplayImage(moMyEquipment.RecipeMarkBitmap2)
        tabView.SelectedTab = tabLocate

        cbxGatherStandardDeviation.Checked = moMainRecipe.RecipeCamera.RecipeModelDiff.IsGatherStandardDeviation
        If moMyEquipment.Camera.Camera.IsNullCamera() = False AndAlso moMyEquipment.Camera.ChangeExposure(moMainRecipe.ExposureTime, "檢測相機", moLog) = False Then MsgBox("設定 [檢測相機] 曝光時間，失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        Call moCanvas.UpdateCanvas()
        Call moCanvasLocate1.UpdateCanvas()
        Call moCanvasLocate2.UpdateCanvas()

        mvwMapView.IsDrawMapIndex = True
        mvwMapView.IsDrawMapCircle = False
        mvwMapView.MapDieRectangleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDieRectangle
        mvwMapView.MapCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorMapCircle
        mvwMapView.MapSelectedDieColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDie
        mvwMapView.MapSelectedDefectColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefect
        mvwMapView.MapDefectCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorDefectCircle
        mvwMapView.MapSelectedDefectCircleColor = moMainRecipe.RecipeCamera.RecipeWaferMap.ColorSelectedDefectCircle
        mvwMapView.IsReverseMapIndexColumn = moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexColumn
        mvwMapView.IsReverseMapIndexRow = moMainRecipe.RecipeCamera.RecipeWaferMap.IsReverseMapIndexRow
        mvwMapView.IsDrawNGDie = moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGDie
        mvwMapView.IsDrawNGFeature = moMainRecipe.RecipeCamera.RecipeWaferMap.IsDrawNGFeature
        mvwMapView.CircleIndentation = 0
        mvwMapView.IsDrawMapIndex = True
        mvwMapView.IsDrawMapCircle = False
        Call moMyEquipment.WaferMapSetColor()
        Call moMyEquipment.WaferMapReset()
        Call mvwMapView.SetMapZoom(MapZoom)
        Call mvwMapView.Refresh()
        Call mvwMapView.UpdateMapImage()
        Call dlvMapDieList.Refresh()
    End Sub

    '' Augustin 220726 Add for Wafer Map
    Private Sub mnuRecipeWaferMap_Click(sender As System.Object, e As System.EventArgs) Handles mnuRecipeWaferMap.Click
        Dim mofrmWaferMap As New frmWaferMap(moMyEquipment)
        Call moMyEquipment.WaferMapReset()
        Call mofrmWaferMap.ShowDialog()
        Call mofrmWaferMap.Dispose()

        mvwMapView.CircleIndentation = 0
        mvwMapView.IsDrawMapIndex = True
        mvwMapView.IsDrawMapCircle = False
        Call moMyEquipment.WaferMapReset()
        Call mvwMapView.SetMapZoom(MapZoom)
        Call mvwMapView.Refresh()
        Call mvwMapView.UpdateMapImage()
        Call dlvMapDieList.Refresh()
    End Sub

    ''Augustin 220726 Add for Wafer Map
    Private Sub moAutoRunThread_AutoRunUpdateWaferMap() Handles moAutoRunThread.AutoRunUpdateWaferMap
        moSync.Send(
         Sub()
             Call mvwMapView.Refresh()
             Call dlvMapDieList.Refresh()
             Call mvwMapView.UpdateMapImage()
             Call moMyEquipment.WaferMap.TotalDefectList.Clear()
         End Sub, "")
    End Sub

    ''Augustin 220726 Add for Wafer Map
    Private Sub dlvMapDefectList_SelectedDefect(ByVal oSelectedDefect As iTVisionService.DisplayLib.CMyDefect) Handles dlvMapDefectList.SelectedDefect
        moSelectedDefect = oSelectedDefect
    End Sub


    ''' <summary>
    ''' 表單關閉後的事件
    ''' </summary>
    ''' 20230719 Chia Add for frmMain Open/Close Bug Fix
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            GC.Collect()
            Application.Exit()
            Application.ExitThread()
            Environment.Exit(Environment.ExitCode)
        Catch ex As Exception
            Dim msg As String = ex.Message
        End Try

    End Sub
End Class