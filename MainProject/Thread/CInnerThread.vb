Public Class CInnerThread

    Private moMyEquipment As CMyEquipment
    Public AutoRunThread As CAutoRunThread
    Public HandshakeThread As CHandshakeThread
    Public TowerThread As CTowerThread

    Private moEraseThreadLog As CEraseThreadExtend
    Private moEraseThreadReport As CEraseThreadExtend
    Private moClearBufferThread As CClearBufferThread

    Private moLog As II_LogTraceExtend

    Public Inspect As New ManualResetEventSlim(False)
    Public HandshakeProcess As New ManualResetEventSlim(False)

    ''' <summary>
    ''' moInnerThread = New CInnerThread(Me)
    ''' CInnerThread -> CAutoRunThread, CHandshakeThread, CTowerThread, CClearBufferThread, CEraseThreadExtend, CEraseThreadExtend
    ''' </summary>
    ''' <param name="oMyEquipment"></param>
    ''' <remarks></remarks>
    Public Sub New(oMyEquipment As CMyEquipment)
        Try
            '-------------------------20231019-開始--------------------------
            SynchronizationContext.SetSynchronizationContext(frmMain.moSync)
            '-------------------------20231019-結束--------------------------

            moMyEquipment = oMyEquipment
            moLog = moMyEquipment.LogSystem

            AutoRunThread = New CAutoRunThread(oMyEquipment, oMyEquipment.LogProcess)
            HandshakeThread = New CHandshakeThread(oMyEquipment)
            TowerThread = New CTowerThread(oMyEquipment)
            moClearBufferThread = New CClearBufferThread(oMyEquipment)
            moEraseThreadReport = New CEraseThreadExtend(Application.StartupPath & "\Report", CLogCreateorExtend.CreateNullLog, CInt(oMyEquipment.HardwareConfig.MiscConfig.SaveReportDay), 10, 1000)
            moEraseThreadLog = New CEraseThreadExtend(Application.StartupPath & "\Log", CLogCreateorExtend.CreateNullLog, CInt(oMyEquipment.HardwareConfig.MiscConfig.SaveReportDay), 10, 1000)
        Catch ex As System.Exception
            oMyEquipment.LogSystem.LogError(ex.ToString)
        End Try
    End Sub

    Public Sub StartThread()
        Call AutoRunThread.StartThread()
        Call HandshakeThread.StartThread()
        Call TowerThread.StartThread()
        Call moClearBufferThread.StartThread()
        Call moEraseThreadReport.StartThread()
        Call moEraseThreadLog.StartThread()
    End Sub

    Public Sub StopThread()
        Call AutoRunThread.StopThread()
        Call HandshakeThread.StopThread()
        Call TowerThread.StopThread()
        Call moClearBufferThread.StopThread()
        Call moEraseThreadReport.StopThread()
        Call moEraseThreadLog.StopThread()
        Thread.Sleep(200)
    End Sub
End Class