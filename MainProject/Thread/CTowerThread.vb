﻿Public Class CTowerThread : Inherits CThreadBaseExtend

    Private moMyEquipment As CMyEquipment

    Public Sub New(oMyEquipment As CMyEquipment)
        MyBase.New(oMyEquipment.LogControl, String.Format("Tower Thread"))

        '-------------------------20231019-開始--------------------------
        SynchronizationContext.SetSynchronizationContext(frmMain.moSync)
        '-------------------------20231019-結束--------------------------

        moMyEquipment = oMyEquipment
        Call moLog.Log(LOGHandle.HANDLE_CREATE, String.Format("{0} 流程啟動", moThread.Name))
    End Sub

    ''' <summary>
    ''' Process
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Process()
        While True
            If mbStopSlim.IsSet = True Then
                Exit While
            End If

            Try
                ProcessSingleRun()

                Thread.Sleep(10)
            Catch ex As System.Exception
                Call moLog.LogError(ex.ToString)
            End Try
        End While
    End Sub

    ''' <summary>
    ''' ProcessSingleRun
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ProcessSingleRun() As Boolean
        Try
            If moMyEquipment.HardwareConfig.BuzzerBypass = False AndAlso (moMyEquipment.IsAlarm.IsSet() = True OrElse moMyEquipment.IsErrorOn.IsSet() = True) Then
                If moMyEquipment.IO.Buzzer IsNot Nothing Then
                    moMyEquipment.IO.Buzzer.SetOn(moLog)
                    Thread.Sleep(1000)
                    moMyEquipment.IO.Buzzer.SetOff(moLog)
                    Thread.Sleep(1000)
                End If
            End If
            Return True
        Catch ex As Exception
            Call moLog.LogError(String.Format("Tower 執行錯誤！Error：{0}", ex.ToString()))
            Return False
        End Try
    End Function
End Class