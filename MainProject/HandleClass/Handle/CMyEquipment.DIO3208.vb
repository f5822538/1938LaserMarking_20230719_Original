Imports iTVisionService.iTVJS
Imports iTVisionService.iTVControl

Partial Class CMyEquipment

    Private moIO As CMyIO
    Private moDIO3208 As II_IOCard
    Public IsErrorOn As New ManualResetEventSlim(False)

    Public ReadOnly Property IO As CMyIO
        Get
            Return moIO
        End Get
    End Property

    Public ReadOnly Property DIO3208 As II_IOCard
        Get
            Return moDIO3208
        End Get
    End Property

    ''' <summary>
    ''' "創建-DIO3208
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateDIO3208() As Boolean
        Try
            moDIO3208 = CJSCardCreater.CreateDIO3208B(CLogCreateor.CreateLogToLogExtend(LogControl), 0)

            If moDIO3208.IsOpen = False Then
                Call LogSystem.LogError("創建 DIO3208 失敗")
                Call LogAlarm.LogError("創建 DIO3208 失敗")
                Return False
            End If

            moDIO3208.OpenBoard() 'DIO3208B-開卡
            Update3208Interface() '更新-DIO3208B-介面
            Return True
        Catch ex As Exception
            Call LogSystem.LogError(String.Format("創建 DIO3208 錯誤，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("創建 DIO3208 錯誤")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 更新-DIO3208B-介面
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Update3208Interface()
        moDIO3208.UpdateInPutName(0, CMyIO.SR.LightVacuum1UpSensor)
        moDIO3208.UpdateInPutName(1, CMyIO.SR.LightVacuum1DownSensor)
        moDIO3208.UpdateInPutName(2, CMyIO.SR.LightVacuum2UpSensor)
        moDIO3208.UpdateInPutName(3, CMyIO.SR.LightVacuum2DownSensor)
        moDIO3208.UpdateInPutName(4, CMyIO.SR.ProductPresentSensor)
        moDIO3208.UpdateInPutName(5, CMyIO.SR.SafeSensor1)
        moDIO3208.UpdateInPutName(6, CMyIO.SR.SafeSensor2)
        moDIO3208.UpdateInPutName(7, CMyIO.SR.HomeSensor)

        moDIO3208.UpdateOutPutName(0, CMyIO.SR.LightVacuumUp1)
        moDIO3208.UpdateOutPutName(1, CMyIO.SR.LightOn)
        moDIO3208.UpdateOutPutName(2, CMyIO.SR.StartLight)
        moDIO3208.UpdateOutPutName(3, CMyIO.SR.AlarmLight)
        moDIO3208.UpdateOutPutName(4, CMyIO.SR.LightVacuumUp2)
        moDIO3208.UpdateOutPutName(5, CMyIO.SR.Error)
        moDIO3208.UpdateOutPutName(7, CMyIO.SR.Buzzer)

        '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
        IO.LightVacuum1UpSensor = DIO3208.GetInput(0) '燈源汽缸1上位檢知
        IO.LightVacuum1DownSensor = DIO3208.GetInput(1) '燈源汽缸1下位檢知
        IO.LightVacuum2UpSensor = DIO3208.GetInput(2) '燈源汽缸2上位檢知
        IO.LightVacuum2DownSensor = DIO3208.GetInput(3) '燈源汽缸2下位檢知
        IO.ProductPresentSensor = DIO3208.GetInput(4) '產品-在席Sensor(產品在席檢知)
        IO.SafeSensor1 = New CReverseInput(DIO3208.GetInput(5)) '安全Sensor-1(安全檢知 1)
        IO.SafeSensor2 = New CReverseInput(DIO3208.GetInput(6)) '安全Sensor-2(安全檢知 2)
        IO.HomeSensor = DIO3208.GetInput(7) 'HomeSensor/原點 Sensor(原點檢知)

        IO.LightVacuumUp1 = DIO3208.GetOutPut(0) '燈源汽缸1上
        IO.LightOn = DIO3208.GetOutPut(1) '燈源開關
        IO.StartLight = DIO3208.GetOutPut(2) '啟動燈號
        IO.AlarmLight = DIO3208.GetOutPut(3) '警報燈號
        IO.LightVacuumUp2 = DIO3208.GetOutPut(4) '燈源汽缸2上
        IO.Error = DIO3208.GetOutPut(5) '緊急停止
        IO.Buzzer = DIO3208.GetOutPut(7) '蜂鳴器
        '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))
    End Sub

    ''' <summary>
    ''' SetEroorOn
    ''' </summary>
    ''' <param name="oLog"></param>
    ''' <remarks></remarks>
    Public Sub SetEroorOn(Optional oLog As II_LogTraceExtend = Nothing)
        If moHardwareConfig.IOBypass = True Then Exit Sub
        If moHardwareConfig.OutputErrorBypass = True Then Exit Sub

        IsErrorOn.Set() '將事件的狀態設定為已收到訊號，讓正在等候該事件的一個或多個執行緒繼續執行

        If oLog Is Nothing Then
            IO.Error.SetOn()
        Else
            IO.Error.SetOn(oLog)
        End If
        Thread.Sleep(200)
        If oLog Is Nothing Then
            IO.Error.SetOff()
        Else
            IO.Error.SetOff(oLog)
        End If
    End Sub

    Public Sub SetEroorOff(Optional oLog As II_LogTraceExtend = Nothing)
        If moHardwareConfig.IOBypass = True Then Exit Sub

        If oLog Is Nothing Then
            IO.Error.SetOff()
        Else
            IO.Error.SetOff(oLog)
        End If

        IsErrorOn.Reset() '將事件的狀態設定為未收到信號，會造成執行緒封鎖
    End Sub

    Public Sub SetLightOn(Optional oLog As II_LogTraceExtend = Nothing)
        If moHardwareConfig.IOBypass = True Then Exit Sub
        If oLog Is Nothing Then
            IO.LightOn.SetOn()
        Else
            IO.LightOn.SetOn(oLog)
        End If
    End Sub

    Public Sub SetLightOff(Optional oLog As II_LogTraceExtend = Nothing)
        If moHardwareConfig.IOBypass = True Then Exit Sub
        If oLog Is Nothing Then
            IO.LightOn.SetOff()
        Else
            IO.LightOn.SetOff(oLog)
        End If
    End Sub

    Public Function LightVacuumUp(oLog As II_LogTraceExtend) As AlarmCode
        If moHardwareConfig.IOBypass = True Then Return AlarmCode.IsOK
        If IO.SafeSensor1.IsOn = True OrElse IO.SafeSensor2.IsOn = True Then Return AlarmCode.IsNotSafe
        If IO.LightVacuum1UpSensor.IsOn() = True AndAlso IO.LightVacuum1DownSensor.IsOn() = False Then Return AlarmCode.IsOK

        Dim oAlarmCode As AlarmCode = AlarmCode.IsOK

        Call IO.LightVacuumUp1.SetOn()
        Call IO.LightVacuumUp2.SetOn()

        oAlarmCode = IsTimeOutOrStop(moHardwareConfig.HandshakeConfig.WaitLightTimeout, Function() IO.IsUp)

        If oAlarmCode = AlarmCode.IsOK Then
            oLog.LogInformation("燈源上升，完成！")
            Return AlarmCode.IsOK
        Else
            oLog.LogInformation("燈源上升，失敗！")
        End If

        Select Case oAlarmCode
            Case AlarmCode.IsStop : Return AlarmCode.IsStop
            Case AlarmCode.IsTimeOut : Return AlarmCode.IsLightMoveUpTimeout
            Case Else : Return AlarmCode.IsLightMoveUpFailed
        End Select
    End Function

    Public Function LightVacuumDown(oLog As II_LogTraceExtend) As AlarmCode
        If moHardwareConfig.IOBypass = True Then Return AlarmCode.IsOK
        If IO.LightVacuum1UpSensor.IsOn() = False AndAlso IO.LightVacuum1DownSensor.IsOn() = True Then Return AlarmCode.IsOK

        Dim oAlarmCode As AlarmCode = AlarmCode.IsOK

        Call IO.LightVacuumUp1.SetOff()
        Call IO.LightVacuumUp2.SetOff()

        oAlarmCode = IsTimeOutOrStop(moHardwareConfig.HandshakeConfig.WaitLightTimeout, Function() IO.IsDown)

        If oAlarmCode = AlarmCode.IsOK Then
            oLog.LogInformation("燈源下降，完成！")
            Return AlarmCode.IsOK
        Else
            oLog.LogInformation("燈源下降，失敗！")
        End If

        '' Augustin 220901 Modify
        Return oAlarmCode

        'Select Case oAlarmCode
        '    Case AlarmCode.IsStop : Return AlarmCode.IsStop
        '    Case AlarmCode.IsTimeOut : Return AlarmCode.IsLightMoveDownTimeout
        '    Case Else : Return AlarmCode.IsLightMoveDownFailed
        'End Select
    End Function
End Class