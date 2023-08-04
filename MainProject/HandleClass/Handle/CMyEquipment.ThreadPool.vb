Imports System.Threading
Imports iTVisionService.DisplayLib

Partial Class CMyEquipment

    Private Const MaxParallel As Integer = 8

    Public Shared ParallelOptions As New ParallelOptions()

    Public Sub UpdatePoolSetting()
        Dim nMinWorkThread As Integer
        Dim nMinCompletionPortThreads As Integer
        Dim nMaxWorkThread As Integer
        Dim nMaxCompletionPortThreads As Integer

        ParallelOptions.MaxDegreeOfParallelism = MaxParallel
        CMyMap.ParallelOptions = ParallelOptions

        ThreadPool.GetMinThreads(nMinWorkThread, nMinCompletionPortThreads)
        ThreadPool.GetMaxThreads(nMaxWorkThread, nMaxCompletionPortThreads)

        LogSystem.LogInformation(String.Format("MinWorkThread={0:d4} MinWorkThreadCompletePort={1:d4}", nMinWorkThread, nMinCompletionPortThreads))
        LogSystem.LogInformation(String.Format("MaxWorkThread={0:d4} MaxWorkThreadCompletePort={1:d4}", nMaxWorkThread, nMaxCompletionPortThreads))

        nMinWorkThread = Math.Max(MaxParallel * 2, nMinWorkThread)
        nMinCompletionPortThreads = Math.Max(MaxParallel, nMinCompletionPortThreads)

        Dim bIsOK As Boolean = ThreadPool.SetMinThreads(nMinWorkThread, nMinCompletionPortThreads)
        LogSystem.LogInformation(String.Format("MinWorkThread={0:d4} MinWorkThreadCompletePort={1:d4} {2}", nMinWorkThread, nMinCompletionPortThreads, bIsOK))
    End Sub

End Class