Partial Class CMyEquipment

    Public Delegate Function CheckSuccess() As Boolean

    Public Function IsTimeOutOrStop(nTimeSecs As Integer, aCheckSuccess As CheckSuccess) As AlarmCode
        Dim oTimeStart As Date = DateAndTime.Now
        Dim bIsReady As Boolean = True

        While True
            If IsStop.IsSet = True Then
                Return AlarmCode.IsStop
                Exit While
            End If

            If (DateAndTime.Now - oTimeStart).TotalSeconds > nTimeSecs Then
                Return AlarmCode.IsTimeOut
                Exit While
            End If

            If aCheckSuccess() = True Then
                Exit While
            End If

            Thread.Sleep(20)
        End While

        Return AlarmCode.IsOK
    End Function

    Public Function IsTimeOutOrStopSleepMotorInitial(nTimeSecs As Integer, aCheckSuccess1 As CheckSuccess, aCheckSuccess2 As CheckSuccess, nDelay As Integer) As AlarmCode
        If aCheckSuccess1() = True Or aCheckSuccess2() = True Then Return AlarmCode.IsOK
        Dim oTimeStart As Date = DateTime.Now
        Dim bIsReady As Boolean = True

        While True

            If aCheckSuccess1() = True Then
                Exit While
            End If

            If aCheckSuccess2() = True Then
                Exit While
            End If

            If IsStop.IsSet = True Then
                Return AlarmCode.IsStop
                Exit While
            End If

            If (DateTime.Now - oTimeStart).TotalSeconds > nTimeSecs Then
                Return AlarmCode.IsTimeOut
                Exit While
            End If

            Thread.Sleep(1000)
        End While

        Thread.Sleep(nDelay)
        Return AlarmCode.IsOK
    End Function
End Class