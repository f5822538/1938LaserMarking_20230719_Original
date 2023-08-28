Partial Class CInnerThread

    Public Sub StartSingleRun()
        If IsCanRunInspect() = False Then Exit Sub '�S�����J�s�{�Ѽ�
        AutoRunThread.Statu = InspectStatu.SingleRun
    End Sub

    Public Sub StartContinusRun()
        If IsCanRunInspect() = False Then Exit Sub '�S�����J�s�{�Ѽ�
        AutoRunThread.Statu = InspectStatu.ContinueRun
    End Sub

    Public Sub StartTestRun()
        If IsCanRunInspect() = False Then Exit Sub '�S�����J�s�{�Ѽ�
        AutoRunThread.Statu = InspectStatu.TestRun
    End Sub

    Public Sub StopProcess()
        AutoRunThread.Statu = InspectStatu.StopRun
    End Sub

    ''' <summary>
    ''' �P�_�O�_�i�H��������˴�(�P�_�O�_�����J�s�{�Ѽ�)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCanRunInspect() As Boolean
        If moMyEquipment.HardwareConfig.InspectBypass = True Then Return True

        If moMyEquipment.MainRecipe.RecipeID.ToUpper = "DEFAULT" Then
            Call MsgBox("�Х����J�s�{�ѼơI", MsgBoxStyle.OkOnly, "�͵o��ުѥ��������q")
            Call moMyEquipment.TriggerWarning(AlarmCode.IsNotLoadRecipe)
            Return False
        End If

        'Try
        '    Dim nRecipeID As Integer = CInt(moMyEquipment.MainRecipe.RecipeID)
        'Catch ex As Exception
        '    Call MsgBox("�s�{�W�٧t���D�Ʀr�r���I", MsgBoxStyle.OkOnly, "�͵o��ުѥ��������q")
        '    Call moMyEquipment.TriggerWarning(AlarmCode.IsRecipeIDNotNumber)
        '    Return False
        'End Try

        If moMyEquipment.IsAlarm.IsSet = True Then
            Call MsgBox("�Х��ư����x���`�I", MsgBoxStyle.OkOnly, "�͵o��ުѥ��������q")
            Return False
        End If

        'If moMyEquipment.IsInitial.IsSet = False Then
        '    Call MsgBox("�Х���l�ƾ��x�I", MsgBoxStyle.OkOnly, "�͵o��ުѥ��������q")
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