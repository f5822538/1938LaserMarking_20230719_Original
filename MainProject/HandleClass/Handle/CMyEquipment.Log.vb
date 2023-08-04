Partial Class CMyEquipment
    Private moMyLog As New CMyLog

    Public ReadOnly Property MyLog As CMyLog
        Get
            Return moMyLog
        End Get
    End Property

    Public ReadOnly Property LogSystem As II_LogTraceExtend
        Get
            Return moMyLog.LogSystem
        End Get
    End Property

    Public ReadOnly Property LogProcess As II_LogTraceExtend
        Get
            Return moMyLog.LogProcess
        End Get
    End Property

    Public ReadOnly Property LogControl As II_LogTraceExtend
        Get
            Return moMyLog.LogControl
        End Get
    End Property

    Public ReadOnly Property LogAlarm As II_LogTraceExtend
        Get
            Return moMyLog.LogAlarm
        End Get
    End Property

    Public ReadOnly Property LogHandshake As II_LogTraceExtend
        Get
            Return moMyLog.LogHandshake
        End Get
    End Property

    Public ReadOnly Property LogManual As II_LogTraceExtend
        Get
            Return moMyLog.LogManual
        End Get
    End Property

    Public ReadOnly Property LogInspectCSV As II_LogTrace
        Get
            Return moMyLog.LogInspectCSV
        End Get
    End Property

    Public Sub DisableDisplay()
        moMyLog.DisableDisplay()
    End Sub
End Class