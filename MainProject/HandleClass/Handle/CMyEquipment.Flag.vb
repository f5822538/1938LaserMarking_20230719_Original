Partial Class CMyEquipment

    'Private moIsInitial As New ManualResetEventSlim(False)
    Private moIsStop As New ManualResetEventSlim(False)
    Private moIsAlarm As New ManualResetEventSlim(False)
    '-------------------------20230919-開始--------------------------
    Private moIsCanInspect As New ManualResetEventSlim(initialState:=False)
    '-------------------------20230919-結束--------------------------
    'Private moIsRunning As New ManualResetEventSlim(False)

    'Public ReadOnly Property IsInitial As ManualResetEventSlim
    '    Get
    '        Return moIsInitial
    '    End Get
    'End Property

    Public ReadOnly Property IsStop As ManualResetEventSlim
        Get

            Return moIsStop
        End Get
    End Property

    Public ReadOnly Property IsAlarm As ManualResetEventSlim
        Get
            Return moIsAlarm
        End Get
    End Property

    Public ReadOnly Property IsCanInspect As ManualResetEventSlim
        Get
            Return moIsCanInspect
        End Get
    End Property

    'Public ReadOnly Property IsRunning As ManualResetEventSlim
    '    Get
    '        Return moIsRunning
    '    End Get
    'End Property
End Class