Partial Class CInnerThread

    Public Sub StartSingleRun()
        If IsCanRunInspect() = False Then Exit Sub '⊿Τ更籹祘把计
        AutoRunThread.Statu = InspectStatu.SingleRun
    End Sub

    Public Sub StartContinusRun()
        If IsCanRunInspect() = False Then Exit Sub '⊿Τ更籹祘把计
        AutoRunThread.Statu = InspectStatu.ContinueRun
    End Sub

    Public Sub StartTestRun()
        If IsCanRunInspect() = False Then Exit Sub '⊿Τ更籹祘把计
        AutoRunThread.Statu = InspectStatu.TestRun
    End Sub

    Public Sub StopProcess()
        AutoRunThread.Statu = InspectStatu.StopRun
    End Sub

    ''' <summary>
    ''' 耞琌磅︽厩浪代(耞琌Τ更籹祘把计)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCanRunInspect() As Boolean
        If moMyEquipment.HardwareConfig.InspectBypass = True Then Return True

        If moMyEquipment.MainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("叫更籹祘把计", MsgBoxStyle.OkOnly, "煌祇мΤそ")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return False
        End If

        'Try
        '    Dim nRecipeID As Integer = CInt(moMyEquipment.MainRecipe.RecipeID)
        'Catch ex As Exception
        '    Call MsgBox("籹祘嘿Τ獶计じ", MsgBoxStyle.OkOnly, "煌祇мΤそ")
        '    Call moMyEquipment.TriggerWarning(AlarmCode.IsRecipeIDNotNumber)
        '    Return False
        'End Try

        If moMyEquipment.IsAlarm.IsSet = True Then
            Call MsgBox("叫逼埃诀钵盽", MsgBoxStyle.OkOnly, "煌祇мΤそ")
            Return False
        End If

        'If moMyEquipment.IsInitial.IsSet = False Then
        '    Call MsgBox("叫﹍て诀", MsgBoxStyle.OkOnly, "煌祇мΤそ")
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