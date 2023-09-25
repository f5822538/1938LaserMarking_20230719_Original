Partial Class CMyEquipment

    Public CurrentAlarmCode As AlarmCode = AlarmCode.IsOK

    ''' <summary>
    ''' TriggerWarning
    ''' </summary>
    ''' <param name="aAlarmCode"></param>
    ''' <remarks></remarks>
    Public Sub TriggerWarning(aAlarmCode As AlarmCode)
        CurrentAlarmCode = aAlarmCode
        SRAlarmCode.TriggerWarning(aAlarmCode, LogAlarm)
    End Sub

    Public Sub TriggerWarning(aAlarmCode As AlarmCode, LogAlarm As II_LogTraceExtend)
        CurrentAlarmCode = aAlarmCode
        SRAlarmCode.TriggerWarning(aAlarmCode, LogAlarm)
    End Sub

    ''' <summary>
    ''' TriggerAlarm
    ''' </summary>
    ''' <param name="aAlarmCode"></param>
    ''' <remarks></remarks>
    Public Sub TriggerAlarm(aAlarmCode As AlarmCode)
        If moIsAlarm.IsSet = False Then
            TriggerWarning(aAlarmCode)

            If aAlarmCode <> AlarmCode.IsStop Then moIsAlarm.Set()
        End If
    End Sub

    Public Sub TriggerAlarm(aAlarmCode As AlarmCode, LogAlarm As II_LogTraceExtend)
        If moIsAlarm.IsSet = False Then
            TriggerWarning(aAlarmCode, LogAlarm)

            If aAlarmCode <> AlarmCode.IsStop Then moIsAlarm.Set()
        End If
    End Sub

    Public Sub ClearAlarm()
        SetEroorOff()
        moIsAlarm.Reset() 'moIsAlarm-將事件的狀態設定為未收到信號，會造成執行緒封鎖
        Thread.Sleep(500)
        moIsStop.Reset() 'moIsStop-將事件的狀態設定為未收到信號，會造成執行緒封鎖
        CurrentAlarmCode = AlarmCode.IsOK
    End Sub
End Class