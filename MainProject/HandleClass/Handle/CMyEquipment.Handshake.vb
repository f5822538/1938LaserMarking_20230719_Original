Partial Class CMyEquipment

    Public Handshake As CMyHandshake
    Public IsChangeModel As Boolean = False
    Public IsHandshakeCanProcess As Boolean = True

    Public Function InitialHandshake() As Boolean
        Try
            Handshake = New CMyHandshake(Me)
            If Handshake.CreateHandshakeThread() = False Then Return False
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Sub CloseHandshake()
        If Handshake IsNot Nothing Then
            Call Handshake.CloseHandshakeThread()
        End If
    End Sub

    Public Sub ClearHandshake()
        If Handshake IsNot Nothing Then
            Call Handshake.ClearReceive()
        End If
    End Sub

    Public Function Read(ByRef oHandshakeType As HandshakeType, ByRef oProductList As List(Of CMyProduct), ByRef sLotRecipeID As String, moLog As II_LogTraceExtend) As AlarmCode
        If Handshake.Read(oHandshakeType, oProductList, sLotRecipeID) = False Then Return AlarmCode.IsHandshakeReadFailed
        Select Case oHandshakeType
            Case HandshakeType.NA
            Case HandshakeType.LotInfo : moLog.LogInformation(String.Format("讀取{0}，成功！", EnumHelper.GetDescription(HandshakeType.LotInfo)))
                'Case HandshakeType.StripMapDownload : moLog.LogInformation(String.Format("讀取{0}，成功！", EnumHelper.GetDescription(HandshakeType.StripMapDownload)))
            Case HandshakeType.StripMapUpload : moLog.LogInformation(String.Format("讀取{0}，成功！", EnumHelper.GetDescription(HandshakeType.StripMapUpload)))
        End Select
        Return AlarmCode.IsOK
    End Function

    Public Function SendLotInfoACK(oAlarmCode As AlarmCode, sAlarmText As String, moLog As II_LogTraceExtend) As AlarmCode
        If Handshake.SendLotInfoACK(oAlarmCode, sAlarmText) = False Then Return AlarmCode.IsSendLotInfoACKFailed
        moLog.LogInformation(String.Format("發送{0}，成功！", EnumHelper.GetDescription(HandshakeType.LotInfo)))
        Return AlarmCode.IsOK
    End Function

    'Public Function SendStripMapDownloadACK(oAlarmCode As AlarmCode, ByRef oProduct As CMyProduct, moLog As II_LogTraceExtend) As AlarmCode
    '    If Handshake.SendStripMapDownloadACK(oAlarmCode, oProduct) = False Then Return AlarmCode.IsSendStripMapDownloadACKFailed
    '    moLog.LogInformation(String.Format("發送{0}，成功！", EnumHelper.GetDescription(HandshakeType.StripMapDownload)))
    '    Return AlarmCode.IsOK
    'End Function

    ''' <summary>
    ''' 發送-Lead Frame上的每個Die映射圖上傳
    ''' </summary>
    ''' <param name="oProduct"></param>
    ''' <param name="moLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendStripMapUpload(ByRef oProduct As CMyProduct, moLog As II_LogTraceExtend) As AlarmCode
        If Handshake.SendStripMapUploadIncludeOriginalMapInfo(oProduct) = False Then
            Return AlarmCode.IsSendStripMapUploadFailed  'TCP 發送上傳產品分布失敗
            'If Handshake.SendStripMapUpload(oProduct) = False Then Return AlarmCode.IsSendStripMapUploadFailed
        Else
            moLog.LogInformation(String.Format("發送{0}，成功！", EnumHelper.GetDescription(HandshakeType.LotInfo)))
            Return AlarmCode.IsOK
        End If
    End Function

    ''' <summary>
    ''' 呼叫-Handshake.UpdateAIInfo(寫入 AI XML 檔案)
    ''' </summary>
    ''' <param name="oProduct"></param>
    ''' <param name="oInspectSum"></param>
    ''' <param name="moLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateAIInfo(oProduct As CMyProduct, oInspectSum As CInspectSum, moLog As II_LogTraceExtend) As AlarmCode
        'If moMainRecipe.RecipeCamera.RecipeModelDiff.IsUpLoadInspectPicture = True Then
        'End If
        If Handshake.UpdateAIInfo(oProduct, oInspectSum) = False Then Return AlarmCode.IsSendUpdateAIInfoFailed
        moLog.LogInformation(String.Format("發送{0}，成功！", EnumHelper.GetDescription(HandshakeType.LotInfo)))
        Return AlarmCode.IsOK
    End Function

    Public Function WaitMapUploadACK(ByRef oProductProcess As CMyProduct, oCheckExit As CMyHandshake.CheckSuccess) As AlarmCode
        Dim oAlarmCode As AlarmCode = AlarmCode.IsOK
        Dim oHandshakeType As HandshakeType
        Dim sReceive As String = ""
        oAlarmCode = Handshake.WaitReceive(sReceive, oCheckExit, moHardwareConfig.HandshakeConfig.HandshakeTimeout)
        If oAlarmCode <> AlarmCode.IsOK Then Return oAlarmCode
        oHandshakeType = Handshake.ReadType(sReceive)
        If oHandshakeType <> HandshakeType.StripMapUpload Then Return AlarmCode.IsHandshakeReadFailed
        Return AlarmCode.IsOK
    End Function
End Class