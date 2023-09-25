Partial Class SRAlarmCode

    ''' <summary>
    ''' TriggerWarning
    ''' </summary>
    ''' <param name="aAlarmCode"></param>
    ''' <param name="LogAlarm"></param>
    ''' <remarks></remarks>
    Public Shared Sub TriggerWarning(aAlarmCode As AlarmCode, LogAlarm As II_LogTraceExtend)
        Dim sDescription As String = EnumHelper.GetDescription(aAlarmCode)

        Select Case True
            Case sDescription <> ""
                LogAlarm.LogInformation(String.Format("警報代碼 [{0:D4}] {1}", CInt(aAlarmCode), sDescription))
            Case Else
                LogAlarm.LogInformation(String.Format("警報代碼 [{0:D4}] {1}", CInt(aAlarmCode), "未定義錯誤"))
        End Select

    End Sub

End Class