Partial Class SRAlarmCode

    Public Shared Sub TriggerWarning(aAlarmCode As AlarmCode, LogAlarm As II_LogTraceExtend)
        Dim sDescription As String = EnumHelper.GetDescription(aAlarmCode)

        Select Case True
            Case sDescription <> ""
                LogAlarm.LogInformation(String.Format("Code [{0:D4}] {1}", CInt(aAlarmCode), sDescription))
            Case Else
                LogAlarm.LogInformation(String.Format("Code [{0:D4}] {1}", CInt(aAlarmCode), "未定義錯誤"))
        End Select

    End Sub

End Class