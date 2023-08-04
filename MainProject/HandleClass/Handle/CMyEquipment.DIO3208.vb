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

    Public Function CreateDIO3208() As Boolean
        Try
            moDIO3208 = CJSCardCreater.CreateDIO3208B(CLogCreateor.CreateLogToLogExtend(LogControl), 0)

            If moDIO3208.IsOpen = False Then
                Call LogSystem.LogError("創建 DIO3208 失敗")
                Call LogAlarm.LogError("創建 DIO3208 失敗")
                Return False
            End If

            Call moDIO3208.OpenBoard()
            Update3208Interface()
            Return True
        Catch ex As Exception
            Call LogSystem.LogError(String.Format("創建 DIO3208 錯誤，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("創建 DIO3208 錯誤")
            Return False
        End Try
    End Function

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

        IO.LightVacuum1UpSensor = DIO3208.GetInput(0)
        IO.LightVacuum1DownSensor = DIO3208.GetInput(1)
        IO.LightVacuum2UpSensor = DIO3208.GetInput(2)
        IO.LightVacuum2DownSensor = DIO3208.GetInput(3)
        IO.ProductPresentSensor = DIO3208.GetInput(4)
        IO.SafeSensor1 = New CReverseInput(DIO3208.GetInput(5))
        IO.SafeSensor2 = New CReverseInput(DIO3208.GetInput(6))
        IO.HomeSensor = DIO3208.GetInput(7)

        IO.LightVacuumUp1 = DIO3208.GetOutPut(0)
        IO.LightOn = DIO3208.GetOutPut(1)
        IO.StartLight = DIO3208.GetOutPut(2)
        IO.AlarmLight = DIO3208.GetOutPut(3)
        IO.LightVacuumUp2 = DIO3208.GetOutPut(4)
        IO.Error = DIO3208.GetOutPut(5)
        IO.Buzzer = DIO3208.GetOutPut(7)
    End Sub

    Public Sub SetEroorOn(Optional oLog As II_LogTraceExtend = Nothing)
        If moHardwareConfig.IOBypass = True Then Exit Sub
        If moHardwareConfig.OutputErrorBypass = True Then Exit Sub

        IsErrorOn.Set()

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

        IsErrorOn.Reset()
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
        'Return AlarmCode.IsOK

        Select Case oAlarmCode
            Case AlarmCode.IsStop : Return AlarmCode.IsStop
            Case AlarmCode.IsTimeOut : Return AlarmCode.IsLightMoveDownTimeout
            Case Else : Return AlarmCode.IsLightMoveDownFailed
        End Select
    End Function
End Class