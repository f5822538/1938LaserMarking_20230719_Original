Public Class CAutoRunThread : Inherits CThreadBaseExtend

    Private moMyEquipment As CMyEquipment
    Private moMainRecipe As CMainRecipe
    Private moRecipeCamera As CRecipeCamera
    Private moCamera As CMyCamera '檢測相機
    Private moCodeReaderCamera As CMyCamera '條碼相機
    Private moImageID As MIL_ID
    Private moCodeReaderImageID1 As MIL_ID
    Private moCodeReaderImageID2 As MIL_ID
    Private moImageHeader As ImageHeader
    Private moCodeReaderImageHeader As ImageHeader

    Private moRunInspect As New ManualResetEventSlim(False)
    Private moStopRun As New ManualResetEventSlim(False)
    Private moStatu As InspectStatu = InspectStatu.StopRun '程式已暫停
    Private mnSequence As Integer = 0
    Private mnSequence_OK As Integer = 0
    Private LastDate As Date = Now.Date
    Private mbAlignStatus As Boolean = False '設定-定位正常(初始值)
    Public Event AutoRunUpdateWaferMap()  '' Augustin 220726 Add for Wafer Map

    ''' <summary>
    ''' Number-3-執行順序
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Statu() As InspectStatu
        Get
            Return moStatu
        End Get
        Set(oValue As InspectStatu)
            If moRunInspect Is Nothing Then Exit Property
            If oValue = InspectStatu.ContinueRun OrElse oValue = InspectStatu.SingleRun OrElse oValue = InspectStatu.TestRun Then '連續執行 Continus Run
                If (oValue = InspectStatu.SingleRun AndAlso moStatu <> InspectStatu.StopRun) OrElse _
                   (oValue = InspectStatu.TestRun AndAlso moStatu <> InspectStatu.StopRun) OrElse _
                   (oValue = InspectStatu.ContinueRun AndAlso moStatu = InspectStatu.ContinueRun) Then
                    Exit Property
                End If
                moMyEquipment.MaxDefectCountForUpdateMap = moMainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount * moMyEquipment.HardwareConfig.MiscConfig.MaxDefectCountForUpdateMap \ 100
                moMyEquipment.MaxOffsetCountForUpdateToFttp = moMainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount * moMainRecipe.RecipeCamera.RecipeModelDiff.MaxOffsetPercentForUpdateToFtp \ 100
                Call moLog.LogInformation(String.Format("上報 Map 最大瑕疵數量：{0}", moMyEquipment.MaxDefectCountForUpdateMap))
                Call moLog.LogInformation(String.Format("上傳至Ftp  蓋印偏移 最大瑕疵數量：{0}", moMyEquipment.MaxOffsetCountForUpdateToFttp))
                Call moLog.LogInformation(String.Format("IO Bypass：{0}", moMyEquipment.HardwareConfig.IOBypass))
                Call moLog.LogInformation(String.Format("觸發 Bypass：{0}", moMyEquipment.HardwareConfig.TriggerBypass))
                Call moLog.LogInformation(String.Format("交握 Bypass：{0}", moMyEquipment.HardwareConfig.HandshakeBypass))
                Call moLog.LogInformation(String.Format("IT Bypass：{0}", moMyEquipment.HardwareConfig.ITBypass))
                Call moLog.LogInformation(String.Format("檢測 Bypass：{0}", moMyEquipment.HardwareConfig.InspectBypass))
                Call moLog.LogInformation(String.Format("條碼讀取 Bypass：{0}", moMyEquipment.HardwareConfig.CodeReaderBypass))
                Call moLog.LogInformation(String.Format("輸出警報 Bypass：{0}", moMyEquipment.HardwareConfig.OutputErrorBypass))
                Call moLog.LogInformation(String.Format("蜂鳴器 Bypass：{0}", moMyEquipment.HardwareConfig.BuzzerBypass))
                Call moRunInspect.Set()
            Else
                moStatu = InspectStatu.StopRun '程式已暫停
                Call moStopRun.Set()
                Call moRunInspect.Reset()
            End If

            moStatu = oValue
        End Set
    End Property

    Public ReadOnly Property IsRunning() As Boolean
        Get
            Return moRunInspect.IsSet = True AndAlso moStopRun.IsSet = False
        End Get
    End Property

    ''' <summary>
    ''' Number-1-執行順序
    ''' </summary>
    ''' <param name="oMyEquipment"></param>
    ''' <param name="oLog"></param>
    ''' <remarks></remarks>
    Public Sub New(oMyEquipment As CMyEquipment, oLog As II_LogTraceExtend)
        MyBase.New(oLog, " Auto Run", 256000)

        Try
            moMyEquipment = oMyEquipment
            moMainRecipe = oMyEquipment.MainRecipe
            moRecipeCamera = oMyEquipment.MainRecipe.RecipeCamera
            moCamera = oMyEquipment.Camera
            moCodeReaderCamera = oMyEquipment.CodeReaderCamera
            moImageID = oMyEquipment.ImageID
            moImageHeader = oMyEquipment.ImageHeader
            moCodeReaderImageHeader = oMyEquipment.CodeReaderImageHeader
            mnSequence = oMyEquipment.Sequence

            Try
                moHtmlReport = New CHtmlReport(moMyEquipment.HardwareConfig.MiscConfig.XsltFile, New SizeF(CSng(oMyEquipment.HardwareConfig.CameraConfig.PixelSize), CSng(oMyEquipment.HardwareConfig.CameraConfig.PixelSize)), GetType(CMyDefectList))
            Catch ex As Exception

            End Try

            Call moMyEquipment.MyLog.LogSystem.LogInformation("自動化流程啟動")
        Catch ex As Exception
            Call moMyEquipment.LogSystem.LogError(String.Format("Initial Auto Run Thread Fail，Error：{0}", ex.ToString()))
            Call moMyEquipment.LogAlarm.LogError("Initial Auto Run Thread Fail")
            Call moMyEquipment.TriggerAlarm(AlarmCode.IsThreadCreateFailed)
        End Try
    End Sub

    ''' <summary>
    ''' 執行緒工作處理流程:(AutoRunThread = New CAutoRunThread -> AutoRunThread.Process)
    ''' Number-2-執行順序
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Process()
        While True
            WaitHandle.WaitAny({mbStopSlim.WaitHandle, moRunInspect.WaitHandle})
            Select Case True
                Case mbStopSlim.IsSet() = True
                    Call CloseHandle()
                    Return

                Case moMyEquipment.IsAlarm.IsSet() = True OrElse moMyEquipment.IsErrorOn.IsSet() = True
                    If Statu <> InspectStatu.StopRun Then Statu = InspectStatu.StopRun
                    Exit Select

                Case Statu = InspectStatu.StopRun
                    Exit Select

                Case Statu = InspectStatu.SingleRun
                    If mbStopSlim.IsSet() = True Then
                        Call CloseHandle()
                        Return
                    End If

                    If moStopRun.IsSet() = True Then
                        Call moStopRun.Reset()
                        Exit Select
                    End If

                    If moMyEquipment.IsAlarm.IsSet() = True Then
                        Call moStopRun.Reset()
                        Exit Select
                    End If

                    Try
                        SingleRun(bIsTestRun:=False) '單次執行
                        Call GC.Collect()
                    Catch ex As Exception

                    End Try

                    Call moStopRun.Set()
                    Call moRunInspect.Reset()
                    moStatu = InspectStatu.StopRun '程式已暫停

                Case Statu = InspectStatu.ContinueRun
                    If mbStopSlim.IsSet() = True Then
                        Call CloseHandle()
                        Return
                    End If

                    If moStopRun.IsSet() = True Then
                        Call moStopRun.Reset()
                        Exit Select
                    End If

                    If moMyEquipment.IsAlarm.IsSet() = True Then
                        Call moStopRun.Reset()
                        Exit Select
                    End If

                    Try
                        SingleRun(bIsTestRun:=False) '連續執行
                        Call GC.Collect()
                    Catch ex As Exception

                    End Try

                    'Thread.Sleep(3000)
                    Thread.Sleep(100)

                Case Statu = InspectStatu.TestRun
                    If mbStopSlim.IsSet() = True Then
                        Call CloseHandle()
                        Return
                    End If

                    If moStopRun.IsSet() = True Then
                        Call moStopRun.Reset()
                        Exit Select
                    End If

                    If moMyEquipment.IsAlarm.IsSet() = True Then
                        Call moStopRun.Reset()
                        Exit Select
                    End If

                    Try
                        SingleRun(bIsTestRun:=True) '測試執行
                        Call GC.Collect()
                    Catch ex As Exception

                    End Try

                    Call moStopRun.Set()
                    Call moRunInspect.Reset()
                    moStatu = InspectStatu.StopRun '程式已暫停
            End Select

            Thread.Sleep(50)
        End While
    End Sub

    Private Sub CloseHandle()
        Try
            Call mbStopSlim.Set()
            'Thread.Sleep(1000)
            Thread.Sleep(100)
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Number-5-執行順序
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function WaitProcess() As AlarmCode
        Dim oTimeStart As Date = DateAndTime.Now
        Dim bIsTimeOut As Boolean = True

        While True
            If moMyEquipment.HardwareConfig.TriggerBypass = False Then
                If moMyEquipment.IsCanInspect.IsSet() = True AndAlso moMyEquipment.IO.ProductPresentSensor.IsOn() = True AndAlso moMyEquipment.IO.SafeSensor1.IsOn() = False AndAlso _
                   moMyEquipment.IO.SafeSensor2.IsOn() = False AndAlso moMyEquipment.InnerThread.HandshakeProcess.IsSet() = False Then
                    moMyEquipment.InnerThread.Inspect.Set() '將事件的狀態設定為已收到訊號，讓正在等候該事件的一個或多個執行緒繼續執行
                    While True
                        If moMyEquipment.InnerThread.HandshakeProcess.IsSet() = False Then
                            Return AlarmCode.IsOK '取得值，表示事件是否已設定
                        End If

                        If mbStopSlim.IsSet() = True OrElse moStopRun.IsSet() = True OrElse moRunInspect.IsSet() = False OrElse moStatu = InspectStatu.StopRun Then
                            Return AlarmCode.IsStop '程式已暫停
                        End If
                        Thread.Sleep(10)
                    End While
                End If
            Else
                Exit While
            End If

            If mbStopSlim.IsSet() = True OrElse moStopRun.IsSet() = True OrElse moRunInspect.IsSet() = False OrElse moStatu = InspectStatu.StopRun Then
                Return AlarmCode.IsStop '程式已暫停
            End If
            Thread.Sleep(10)
        End While

        moMyEquipment.IsCanInspect.Reset() '將事件的狀態設定為未收到信號，會造成執行緒封鎖
        Return AlarmCode.IsOK
    End Function

    ''' <summary>
    ''' CAutoRunThread.SingleRun
    ''' Number-4-執行順序
    ''' </summary>
    ''' <param name="bIsTestRun"></param>
    ''' <remarks></remarks>
    Private Sub SingleRun(bIsTestRun As Boolean)
        If mbStopSlim.IsSet() = True OrElse moStopRun.IsSet() = True OrElse moRunInspect.IsSet() = False OrElse moStatu = InspectStatu.StopRun Then
            Call moStopRun.Set()
            Call moRunInspect.Reset()
            Exit Sub
        End If

        '' Augustin 220726 Add for Wafer Map
        Call moMyEquipment.WaferMapReset()
        RaiseEvent AutoRunUpdateWaferMap()

        Dim oAlarmCode As AlarmCode = AlarmCode.IsOK
        Dim oCameraSnap As Task(Of Boolean) = Nothing '檢測相機-取像任務
        Dim oCodeReaderCameraSnap As Task(Of Boolean) = Nothing

        '-----------------------moMyEquipment-With-20230822-開始--------------------------
        With moMyEquipment
            mbAlignStatus = False '設定-定位正常(初始值)

            '-------------------------20230828-開始--------------------------
            If bIsTestRun = False Then '如果不是測試執行
                If moMyEquipment.CodeReaderCamera.Camera.IsNullCamera() = False AndAlso _
                    moMyEquipment.CodeReaderCamera.ChangeExposure(moMainRecipe.RecipeCamera.CodeReader.CodeReaderExposureTime1, "條碼相機", moLog) = False Then '更換條碼相機曝光時間-1
                    Call moLog.LogError(String.Format("[{0:d4}] 更換條碼相機曝光時間失敗 (1)", mnSequence))
                End If

                oCameraSnap = New Task(Of Boolean)(Function() moCamera.Snap(mnSequence, "檢測相機", moLog)) '檢測相機-取像任務
                oCodeReaderCameraSnap = New Task(Of Boolean)(Function() moCodeReaderCamera.Snap(mnSequence, "條碼相機", moLog)) '條碼相機-取像任務

                oAlarmCode = WaitProcess()
                If oAlarmCode <> AlarmCode.IsOK Then
                    Call oCameraSnap.Dispose() '釋放-檢測相機-取像任務
                    Call oCodeReaderCameraSnap.Dispose() '釋放-條碼相機-取像任務
                    Exit Sub
                End If

                mnSequence += 1
                moMyEquipment.YieldConfig.TotalCount += 1 '總數
                moMyEquipment.YieldConfig.TotalCount_Die += moMainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount 'Die 總數
            End If
            '-------------------------20230828-結束--------------------------


            Dim oRunOnceTact As New CTactTimeSpan '單次執行-TactTimeSpan
            Dim oTact As New CTactTimeSpan '條碼相機/檢測相機取像-TactTimeSpan
            'Dim oTactForInspectSnap As New CTactTimeSpan
            Try
                Dim oLightVacuumDown As Task(Of AlarmCode) = New Task(Of AlarmCode)(Function() .LightVacuumDown(moLog)) '任務-燈源下降

                '-------------------------20230828-開始--------------------------
                If bIsTestRun = False Then '如果不是測試執行
                    moMyEquipment.Camera.SnapStart(-1, "檢測相機", moLog) '檢測相機-取像開始 (不使用)
                    moMyEquipment.CodeReaderCamera.SnapStart(-1, "條碼相機", moLog) '條碼相機-取像開始 (不使用)
                    Call moMyEquipment.SetLightOn(moLog) '打開-燈源
                    Call moLog.LogInformation(String.Format("[{0:d4}] Light On", mnSequence))

                    oAlarmCode = moMyEquipment.LightVacuumUp(moLog) '燈源上升
                    If oAlarmCode <> AlarmCode.IsOK Then '檢測AlarmCode不是OK
                        Call moMyEquipment.LightVacuumDown(moLog) '燈源汽缸-下降
                        Call moMyEquipment.SetLightOff(moLog) '關閉-燈源
                        Call moMyEquipment.TriggerAlarm(oAlarmCode) '觸發Alarm
                        Call moMyEquipment.SetEroorOn(moLog) '設定-錯誤訊息
                        moMyEquipment.Camera.SnapStop(mnSequence, "檢測相機", moLog) '檢測相機-取像結束 (不使用)
                        moMyEquipment.CodeReaderCamera.SnapStop(mnSequence, "條碼相機", moLog) '條碼相機-取像結束 (不使用)
                        Call oCameraSnap.Dispose() '釋放-檢測相機-取像任務
                        Call oCodeReaderCameraSnap.Dispose() '釋放-條碼相機-取像任務
                        oLightVacuumDown = Nothing
                        Call moMyEquipment.InnerThread.Inspect.Reset() '封鎖執行緒
                        Exit Sub
                    End If
                    Call moLog.LogInformation(String.Format("[{0:d4}] Vacuum Up", mnSequence))

                    Call Thread.Sleep(moMyEquipment.HardwareConfig.MiscConfig.CaptureDelayTime)
                    Call oTact.ReSetTime()
                    Call moLog.LogInformation(String.Format("[{0:d4}] Snap Start (1)", mnSequence))
                    Call oCameraSnap.Start() '開始-檢測相機-取像任務
                    Call oCodeReaderCameraSnap.Start() '開始-條碼相機-取像任務

                    Call Task.WaitAll({oCodeReaderCameraSnap}) '等候-條碼相機-任務完成執行

                    Call oTact.CalSpan()
                    Call moLog.LogInformation(String.Format("[{0:d4}] 取像完畢 (1)。[{1:f4}]ms", mnSequence, oTact.CurrentSpan))
                    If oCodeReaderCameraSnap.Result = True Then '條碼相機-取像完成
                        If .BuildImageForCopy(moCodeReaderCamera.Camera.BitmapImage(True), moCodeReaderImageID1, moCodeReaderImageHeader, mnSequence, moLog) = False Then
                            Call moLog.LogError(String.Format("[{0:d4}] 條碼取像失敗 (1)", mnSequence))
                            Call moMyEquipment.LogAlarm.LogError("條碼取像失敗 (1)")
                            Call moMyEquipment.LightVacuumDown(moLog)
                            Call moMyEquipment.SetLightOff(moLog)
                            Call moMyEquipment.TriggerAlarm(AlarmCode.IsCodeReaderUpdateImageFailed)
                            Call moMyEquipment.SetEroorOn(moLog)
                            Call Task.WaitAll({oCameraSnap}) '等候-檢測相機-任務完成執行
                            moMyEquipment.Camera.SnapStop(mnSequence, "檢測相機", moLog) '檢測相機-取像結束 (不使用)
                            moMyEquipment.CodeReaderCamera.SnapStop(mnSequence, "條碼相機", moLog) '條碼相機-取像結束 (不使用)
                            Call oCameraSnap.Dispose() '釋放-檢測相機-取像任務
                            Call oCodeReaderCameraSnap.Dispose() '釋放-條碼相機-取像任務
                            oLightVacuumDown = Nothing
                            Call moMyEquipment.InnerThread.Inspect.Reset() '封鎖執行緒
                            Exit Sub
                        End If

                        If moMyEquipment.CodeReaderCamera.Camera.IsNullCamera() = False AndAlso _
                            moMyEquipment.CodeReaderCamera.ChangeExposure(moMainRecipe.RecipeCamera.CodeReader.CodeReaderExposureTime2, "條碼相機", moLog) = False Then '更換條碼相機曝光時間-2
                            Call moLog.LogError(String.Format("[{0:d4}] 更換條碼相機曝光時間失敗 (2)", mnSequence))
                        End If

                        Call oTact.ReSetTime()
                        Call moLog.LogInformation(String.Format("[{0:d4}] Snap Start (2)", mnSequence))

                        If moCodeReaderCamera.Snap(mnSequence, "條碼相機", moLog) = False Then  '條碼相機-取像失敗
                            Call moMyEquipment.LightVacuumDown(moLog)
                            Call moMyEquipment.SetLightOff(moLog)
                            Call moMyEquipment.TriggerAlarm(AlarmCode.IsSnapFailed)
                            Call moMyEquipment.SetEroorOn(moLog)
                            moMyEquipment.Camera.SnapStop(mnSequence, "檢測相機", moLog) '檢測相機-取像結束 (不使用)
                            moMyEquipment.CodeReaderCamera.SnapStop(mnSequence, "條碼相機", moLog) '條碼相機-取像結束 (不使用)
                            Call oCameraSnap.Dispose()
                            Call oCodeReaderCameraSnap.Dispose()
                            oLightVacuumDown = Nothing
                            Call moMyEquipment.InnerThread.Inspect.Reset()
                            Exit Sub
                        End If

                        Call oTact.CalSpan()
                        Call moLog.LogInformation(String.Format("[{0:d4}] 取像完畢 (2)。[{1:f4}]ms", mnSequence, oTact.CurrentSpan))
                        Call oTact.ReSetTime()
                    End If

                    Call Task.WaitAll({oCameraSnap}) '等候-檢測相機-任務完成執行

                    If oCameraSnap.Result = False OrElse oCodeReaderCameraSnap.Result = False Then
                        Call moMyEquipment.LightVacuumDown(moLog)
                        Call moMyEquipment.SetLightOff(moLog)
                        Call moMyEquipment.TriggerAlarm(AlarmCode.IsSnapFailed)
                        Call moMyEquipment.SetEroorOn(moLog)
                        moMyEquipment.Camera.SnapStop(mnSequence, "檢測相機", moLog) '檢測相機-取像結束 (不使用)
                        moMyEquipment.CodeReaderCamera.SnapStop(mnSequence, "條碼相機", moLog) '條碼相機-取像結束 (不使用)
                        Call oCameraSnap.Dispose()
                        Call oCodeReaderCameraSnap.Dispose()
                        oLightVacuumDown = Nothing
                        Call moMyEquipment.InnerThread.Inspect.Reset()
                        Exit Sub
                    End If

                    Call oTact.CalSpan()
                    Call moLog.LogInformation(String.Format("[{0:d4}] 等待檢測相機取像完畢。[{1:f4}]ms", mnSequence, oTact.CurrentSpan))
                    Call oTact.ReSetTime()
                    moMyEquipment.Camera.SnapStop(mnSequence, "檢測相機", moLog) '檢測相機-取像結束 (不使用)
                    moMyEquipment.CodeReaderCamera.SnapStop(mnSequence, "條碼相機", moLog) '條碼相機-取像結束 (不使用)
                    Call oCameraSnap.Dispose()
                    Call oCodeReaderCameraSnap.Dispose()
                    Call oTact.CalSpan()
                    Call moLog.LogInformation(String.Format("[{0:d4}] 相機停止取像。[{1:f4}]ms", mnSequence, oTact.CurrentSpan))
                    Call oTact.ReSetTime()

                    Call oLightVacuumDown.Start()
                    Call moMyEquipment.SetLightOff(moLog)
                End If
                '-------------------------20230828-結束--------------------------

                If .BuildImageForCopy(moCamera.Camera.BitmapImage(True), moImageID, moImageHeader, mnSequence, moLog) = False Then
                    Call moLog.LogError(String.Format("[{0:d4}] 取像失敗", mnSequence))
                    Call moMyEquipment.LogAlarm.LogError("取像失敗")
                    Call moMyEquipment.TriggerAlarm(AlarmCode.IsUpdateImageFailed)
                    Call moMyEquipment.SetEroorOn(moLog)

                    If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                        Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                        oAlarmCode = oLightVacuumDown.Result
                        Call oLightVacuumDown.Dispose()
                        If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                    End If

                    oLightVacuumDown = Nothing
                    Call moMyEquipment.InnerThread.Inspect.Reset()
                    Exit Sub
                End If

                If .BuildImageForCopy(moCodeReaderCamera.Camera.BitmapImage(True), moCodeReaderImageID2, moCodeReaderImageHeader, mnSequence, moLog) = False Then
                    Call moLog.LogError(String.Format("[{0:d4}] 條碼取像失敗 (2)", mnSequence))
                    Call moMyEquipment.LogAlarm.LogError("條碼取像失敗 (2)")
                    Call moMyEquipment.TriggerAlarm(AlarmCode.IsCodeReaderUpdateImageFailed)
                    Call moMyEquipment.SetEroorOn(moLog)

                    If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                        Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                        oAlarmCode = oLightVacuumDown.Result
                        Call oLightVacuumDown.Dispose()
                        If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                    End If

                    oLightVacuumDown = Nothing
                    Call moMyEquipment.InnerThread.Inspect.Reset()
                    Exit Sub
                End If

                Call oTact.CalSpan()
                Call moLog.LogInformation(String.Format("[{0:d4}] 影像更新完畢。[{1:f4}]ms", mnSequence, oTact.CurrentSpan))
                Call oTact.ReSetTime()

                '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                Dim bIsOK As Boolean = Locate() '定位及旋轉補正
                '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

                If bIsOK = False Then '定位錯誤/定位失敗
                    Call moLog.LogError(String.Format("[{0:d4}] 定位失敗", mnSequence))
                    Call moMyEquipment.LogAlarm.LogError("定位失敗")
                    Call moMyEquipment.TriggerAlarm(AlarmCode.IsLocateFailed)
                    Call moMyEquipment.SetEroorOn(moLog)

                    If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                        Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                        oAlarmCode = oLightVacuumDown.Result
                        Call oLightVacuumDown.Dispose()
                        If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                    End If

                    oLightVacuumDown = Nothing
                    Call moMyEquipment.InnerThread.Inspect.Reset()
                    Exit Sub
                End If

                If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then '條碼讀取不Bypass
                    'RecipeCamera-搜尋範圍 (X,Y,W,H) >= CodeReaderCamera
                    If moMainRecipe.RecipeCamera.CodeReader.SearchRange.Right >= moMyEquipment.CodeReaderCamera.Camera.CameraWidth OrElse _
                       moMainRecipe.RecipeCamera.CodeReader.SearchRange.Bottom >= moMyEquipment.CodeReaderCamera.Camera.CameraHeight Then
                        Call moLog.LogError(String.Format("[{0:d4}] 條碼參數錯誤 (Barcode)。", mnSequence))
                        Call moMyEquipment.TriggerAlarm(AlarmCode.IsCodeReaderParameterFailed)
                        Call moMyEquipment.SetEroorOn(moLog)

                        If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                            Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                            oAlarmCode = oLightVacuumDown.Result
                            Call oLightVacuumDown.Dispose()
                            If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                        End If

                        oLightVacuumDown = Nothing
                        Call moMyEquipment.InnerThread.Inspect.Reset()
                        Exit Sub
                    End If

                    moMyEquipment.CleanCodeReadValue()
                    oAlarmCode = moMyEquipment.Find(moCodeReaderImageID1, moMainRecipe.RecipeCamera.CodeReader, moLog)
                    If oAlarmCode = AlarmCode.IsOK Then
                        Call moLog.LogInformation(String.Format("[{0:d4}] 第一次讀取條碼成功！條碼：{1}。", mnSequence, moMyEquipment.CodeText))
                        oAlarmCode = LoadProduct()
                        If oAlarmCode <> AlarmCode.IsOK Then
                            '' Augustin 221102 Engineer required if 2D Code is not been recognize , no need to trigger alarm
                            'Call .TriggerAlarm(oAlarmCode)
                            'Call .SetEroorOn(moLog)

                            If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                                Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                                oAlarmCode = oLightVacuumDown.Result
                                Call oLightVacuumDown.Dispose()
                                If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                            End If

                            oLightVacuumDown = Nothing
                            Call moMyEquipment.InnerThread.Inspect.Reset()
                            Exit Sub
                        End If
                    Else
                        Call moLog.LogInformation(String.Format("[{0:d4}] 第一次讀取條碼失敗！進行第二次讀取。", mnSequence))
                        oAlarmCode = moMyEquipment.Find(moCodeReaderImageID2, moMainRecipe.RecipeCamera.CodeReader, moLog)
                        If oAlarmCode = AlarmCode.IsOK Then
                            Call moLog.LogInformation(String.Format("[{0:d4}] 第二次讀取條碼成功！條碼：{1}。", mnSequence, moMyEquipment.CodeText))
                            oAlarmCode = LoadProduct()
                            If oAlarmCode <> AlarmCode.IsOK Then
                                '' Augustin 221102 Engineer required if 2D Code is not been recognize , no need to trigger alarm
                                'Call .TriggerAlarm(oAlarmCode)
                                'Call .SetEroorOn(moLog)

                                If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                                    Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                                    oAlarmCode = oLightVacuumDown.Result
                                    Call oLightVacuumDown.Dispose()
                                    If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                                End If

                                oLightVacuumDown = Nothing
                                Call moMyEquipment.InnerThread.Inspect.Reset()
                                Exit Sub
                            End If
                        Else
                            Call moLog.LogInformation(String.Format("[{0:d4}] 第二次讀取條碼失敗！進行第三次讀取。", mnSequence))
                            If moMainRecipe.RecipeCamera.CodeReaderForInspect.SearchRange.Right >= moMyEquipment.Camera.Camera.CameraWidth OrElse moMainRecipe.RecipeCamera.CodeReaderForInspect.SearchRange.Bottom >= moMyEquipment.Camera.Camera.CameraHeight Then
                                Call moLog.LogError(String.Format("[{0:d4}] 條碼參數錯誤 (Inspect)。", mnSequence))
                                '' Augustin 221102 Engineer required if 2D Code is not been recognize , no need to trigger alarm
                                'Call .TriggerAlarm(AlarmCode.IsCodeReaderParameterFailed)
                                'Call .SetEroorOn(moLog)

                                If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                                    Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                                    oAlarmCode = oLightVacuumDown.Result
                                    Call oLightVacuumDown.Dispose()
                                    If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                                End If

                                oLightVacuumDown = Nothing
                                Call moMyEquipment.InnerThread.Inspect.Reset()
                                Exit Sub
                            End If

                            oAlarmCode = moMyEquipment.FindForInspect(moImageID, moMainRecipe.RecipeCamera.CodeReaderForInspect, moLog)

                            If oAlarmCode <> AlarmCode.IsOK Then
                                oAlarmCode = moMyEquipment.FindForInspect2(moImageID, moMainRecipe.RecipeCamera.CodeReaderForInspect2, moLog)
                                If oAlarmCode = AlarmCode.IsOK Then
                                    moMainRecipe.RecipeCamera.CodeReader.CodeZone = moMainRecipe.RecipeCamera.CodeReaderForInspect2.CodeZone
                                End If
                            Else
                                moMainRecipe.RecipeCamera.CodeReader.CodeZone = moMainRecipe.RecipeCamera.CodeReaderForInspect.CodeZone
                            End If

                            If oAlarmCode = AlarmCode.IsOK Then
                                Call moLog.LogInformation(String.Format("[{0:d4}] 第三次讀取條碼成功！條碼：{1}。", mnSequence, moMyEquipment.CodeText))
                                'moMainRecipe.RecipeCamera.CodeReader.CodeZone = moMainRecipe.RecipeCamera.CodeReaderForInspect.CodeZone
                                oAlarmCode = LoadProduct()
                                If oAlarmCode <> AlarmCode.IsOK Then
                                    '' Augustin 221102 Engineer required if 2D Code is not been recognize , no need to trigger alarm
                                    'Call .TriggerAlarm(oAlarmCode)
                                    'Call .SetEroorOn(moLog)

                                    If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                                        Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                                        oAlarmCode = oLightVacuumDown.Result
                                        Call oLightVacuumDown.Dispose()
                                        If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                                    End If

                                    oLightVacuumDown = Nothing
                                    Call moMyEquipment.InnerThread.Inspect.Reset()
                                    Exit Sub
                                End If
                            Else
                                moProductProcess = moMyEquipment.CopyProduct(moMyEquipment.ProductList.Item(0))
                                moProductProcess.SubstrateID = ""
                                '' Augustin 230512 Bypass Read Code Alarm
                                oAlarmCode = AlarmCode.IsOK
                            End If
                        End If
                    End If

                    If moMyEquipment.CodeReadResultList.Count >= 10 Then
                        moMyEquipment.CodeReadResultList.RemoveAt(0)
                    End If
                    If moMyEquipment.CodeText <> "" Then
                        moMyEquipment.CodeReadResultList.Add(moMyEquipment.CodeText)
                        moMyEquipment.IsUpdateCodeReadResult = True
                    Else
                        moMyEquipment.CodeReadResultList.Add("Fail")
                        moMyEquipment.IsUpdateCodeReadResult = True
                    End If

                End If

                Call moLog.LogInformation(String.Format("[{0:d4}] 條碼：{1}。產品條碼：{2}", mnSequence, moMyEquipment.CodeText, moProductProcess.SubstrateID))

                If moMyEquipment.IsChangeModel = True Then '如果要更換樣本
                    '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                    bIsOK = moMyEquipment.ChangeModel(moLog) '更換樣本 (使用FindCircle算法尋找定位孔)
                    '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))
                    If bIsOK = False Then '更換樣本失敗
                        Call moLog.LogError(String.Format("[{0:d4}] 更換樣本失敗", mnSequence))
                        Call moMyEquipment.LogAlarm.LogError("更換樣本失敗")
                        Call moMyEquipment.TriggerAlarm(AlarmCode.IsChangeModelFailed)
                        Call moMyEquipment.SetEroorOn(moLog)
                    End If

                    moMyEquipment.IsChangeModel = False '設定-不更換樣本

                    If bIsOK = False Then '更換樣本失敗
                        If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                            Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                            oAlarmCode = oLightVacuumDown.Result
                            Call oLightVacuumDown.Dispose()
                            If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode)
                        End If

                        oLightVacuumDown = Nothing
                        Call moMyEquipment.InnerThread.Inspect.Reset() '封鎖執行緒
                        Exit Try
                    End If
                End If

                oAlarmCode = RunInspect() '執行-取像檢測 '(((((((((((((((((((((((((((((((重要區塊))))))))))))))))))))))))))))))
                '--------------------------Run Inspect 失敗-開始--------------------------
                Dim runInspectErrMsg As String = "Run Inspect 失敗"
                If oAlarmCode <> AlarmCode.IsOK Then '檢測失敗
                    For Each value As AlarmCode In [Enum].GetValues(GetType(AlarmCode))
                        If oAlarmCode = value Then
                            Dim descriptionText As String = frmMain.GetDescriptionText(oAlarmCode)
                            runInspectErrMsg += ":在" & descriptionText & "時,發生異常情況"
                            Exit For
                        End If
                    Next
                    moLog.LogError(String.Format("[{0:d4}] " & runInspectErrMsg, mnSequence)) 'Log 日誌(處理 Process)
                    moMyEquipment.LogAlarm.LogError(String.Format("[{0:d4}] " & runInspectErrMsg, mnSequence)) 'Log 日誌(警報 Alarm)
                End If
                '--------------------------Run Inspect 失敗-結束--------------------------

                If oLightVacuumDown IsNot Nothing AndAlso oLightVacuumDown.Status <> TaskStatus.Created Then
                    Call Task.WaitAll({oLightVacuumDown}, moMyEquipment.HardwareConfig.HandshakeConfig.WaitLightTimeout * 1000) '等候-燈源汽缸下降-任務完成執行
                    oAlarmCode = oLightVacuumDown.Result '燈源汽缸下降結果(AlarmCode)
                    Call oLightVacuumDown.Dispose()
                    If oAlarmCode <> AlarmCode.IsOK Then moMyEquipment.TriggerAlarm(oAlarmCode) '觸發Alarm
                End If

                oLightVacuumDown = Nothing
            Catch ex As Exception
                Call moMyEquipment.LightVacuumDown(moLog)
                Call moMyEquipment.SetLightOff(moLog)
                Call moLog.LogError(String.Format("[{0:d4}] 檢測錯誤，Error：{1}", mnSequence, ex.ToString()))
                Call moMyEquipment.LogAlarm.LogError("檢測錯誤")
                Call moMyEquipment.TriggerAlarm(AlarmCode.IsSnapFailed)
                Call moMyEquipment.SetEroorOn(moLog)
            End Try

            Call moMyEquipment.InnerThread.Inspect.Reset() '封鎖執行緒
            Call oRunOnceTact.CalSpan()
            Call moLog.LogInformation(String.Format("======================= [{0:d4}] 單次執行，完成。[{1:f4}]ms =======================", mnSequence, oRunOnceTact.CurrentSpan))
            Call Thread.Sleep(200)
        End With
        '-----------------------moMyEquipment-End With-20230828-結束--------------------------

    End Sub
End Class