<TypeConverter(GetType(CEnumConvecter))>
Public Enum LOGHandle
    <Description("Create")> HANDLE_CREATE = 1
    <Description("Control")> HANDLE_CONTROL = 2
    <Description("Initial")> HANDLE_INITIAL = 3
    <Description("Inspect")> HANDLE_INSPECT = 4
End Enum

Public Class CMyLog

    Public Property LogSystem As II_LogTraceExtend
    Public Property LogProcess As II_LogTraceExtend
    Public Property LogControl As II_LogTraceExtend
    Public Property LogAlarm As II_LogTraceExtend
    Public Property LogHandshake As II_LogTraceExtend
    Public Property LogManual As II_LogTraceExtend
    Public Property LogInspectCSV As II_LogTrace

    Public moList As New List(Of II_LogTraceExtend)

    Public Sub DisableDisplay()
        Call LogSystem.DisableDisplay()
        Call LogProcess.DisableDisplay()
        Call LogControl.DisableDisplay()
        Call LogAlarm.DisableDisplay()
        Call LogHandshake.DisableDisplay()
        Call LogManual.DisableDisplay()
        Call LogInspectCSV.DisableDisplay()
    End Sub
End Class