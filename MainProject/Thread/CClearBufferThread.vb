Public Class CClearBufferThread : Inherits CThreadBaseExtend

    Private moMyEquipment As CMyEquipment

    Public Sub New(oMyEquipment As CMyEquipment)
        Call MyBase.New(oMyEquipment.LogSystem, "Buffer Clear Thread")
        moMyEquipment = oMyEquipment
    End Sub

    Public Overrides Sub Process()
        While True
            If mbStopSlim.IsSet = True Then
                Exit While
            End If

            Dim nCount As Integer = 0

            While (nCount < 600)
                Call Thread.Sleep(1000)
                nCount += 1

                If mbStopSlim.IsSet = True Then
                    Exit While
                End If
            End While

            Call ProcessSingleRun()

            Call Thread.Sleep(1000)
        End While
    End Sub

    Public Sub ProcessSingleRun()
        Call moMyEquipment.SetSystemCache()
    End Sub
End Class