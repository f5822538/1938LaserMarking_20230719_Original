Partial Class CInnerThread

    Public Sub StartSingleRun()
        If IsCanRunInspect() = False Then Exit Sub
        AutoRunThread.Statu = InspectStatu.SingleRun
    End Sub

    Public Sub StartContinusRun()
        If IsCanRunInspect() = False Then Exit Sub
        AutoRunThread.Statu = InspectStatu.ContinueRun
    End Sub

    Public Sub StartTestRun()
        If IsCanRunInspect() = False Then Exit Sub
        AutoRunThread.Statu = InspectStatu.TestRun
    End Sub

    Public Sub StopProcess()
        AutoRunThread.Statu = InspectStatu.StopRun
    End Sub

    Public Function IsCanRunInspect() As Boolean
        If moMyEquipment.HardwareConfig.InspectBypass = True Then Return True
        If moMyEquipment.MainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("請先載入製程參數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return False
        End If

        'Try
        '    Dim nRecipeID As Integer = CInt(moMyEquipment.MainRecipe.RecipeID)
        'Catch ex As Exception
        '    Call MsgBox("製程名稱含有非數字字元！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '    Call moMyEquipment.TriggerWarning(AlarmCode.IsRecipeIDNotNumber)
        '    Return False
        'End Try

        If moMyEquipment.IsAlarm.IsSet = True Then
            Call MsgBox("請先排除機台異常！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Return False
        End If

        'If moMyEquipment.IsInitial.IsSet = False Then
        '    Call MsgBox("請先初始化機台！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '    Return
        'End If

        Return True
    End Function

    Public Function IsRunning() As Boolean
        Try
            Return AutoRunThread.IsRunning = True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class