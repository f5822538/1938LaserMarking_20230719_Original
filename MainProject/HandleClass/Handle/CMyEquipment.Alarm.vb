Partial Class CMyEquipment

    Public CurrentAlarmCode As AlarmCode = AlarmCode.IsOK

    Public Sub TriggerWarning(aAlarmCode As AlarmCode)
        CurrentAlarmCode = aAlarmCode
        SRAlarmCode.TriggerWarning(aAlarmCode, LogAlarm)
    End Sub

    Public Sub TriggerAlarm(aAlarmCode As AlarmCode)
        If moIsAlarm.IsSet = False Then
            TriggerWarning(aAlarmCode)

            If aAlarmCode <> AlarmCode.IsStop Then moIsAlarm.Set()
        End If
    End Sub

    Public Sub ClearAlarm()
        SetEroorOff()
        moIsAlarm.Reset()
        Thread.Sleep(500)
        moIsStop.Reset()
        CurrentAlarmCode = AlarmCode.IsOK
    End Sub
End Class