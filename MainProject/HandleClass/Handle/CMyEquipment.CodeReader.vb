Partial Class CMyEquipment

    Public CodeReader As CMyCodeReader
    Public CodeReaderForInspect As CMyCodeReader
    Private msCodeText As String = ""
    Public CodeReaderForInspect2 As CMyCodeReader

    Public ReadOnly Property CodeText As String
        Get
            Return msCodeText
        End Get
    End Property

    Private Function InitialCodeReader() As Boolean
        Try
            CodeReader = New CMyCodeReader(Me)
            CodeReaderForInspect = New CMyCodeReader(Me)
            CodeReaderForInspect2 = New CMyCodeReader(Me)
            Return True
        Catch ex As Exception
            Call LogSystem.LogError(String.Format("創建 Code Reader 錯誤，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("創建 Code Reader 錯誤")
            Return False
        End Try
    End Function

    Private Sub ClodeCodeReader()
        CodeReader.Close()
        CodeReaderForInspect.Close()
        CodeReaderForInspect2.Close()
    End Sub

    Public Function CleanCodeReadValue() As Boolean
        msCodeText = ""
        Return True
    End Function

    Public Function Find(oImageID As MIL_ID, oRecipeCodeReader As CRecipeCodeReader, oLog As II_LogTraceExtend) As AlarmCode
        If CodeReader Is Nothing Then Return AlarmCode.IsReadCodeFailed

        Try
            msCodeText = ""
            oRecipeCodeReader.CodeZone = Rectangle.Empty
            If CodeReader.Find(oImageID, oRecipeCodeReader) = False Then Return AlarmCode.IsReadCodeFailed

            If CodeReader.Result.Score > 0 Then
                oRecipeCodeReader.CodeZone = CodeReader.Result.CodeZone
                msCodeText = CodeReader.Result.Code
                Call oLog.LogInformation(String.Format("讀取 Code 完成 (Barcode)！Code：{0}", msCodeText))
            Else
                Call oLog.LogError("讀取 Code 失敗 (Barcode)！")
                Return AlarmCode.IsReadCodeFailed
            End If
        Catch ex As Exception
            Call oLog.LogError(String.Format("讀取 Code 錯誤 (Barcode)，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("讀取 Code 錯誤 (Barcode)")
            Return AlarmCode.IsReadCodeFailed
        End Try
        Return AlarmCode.IsOK
    End Function

    Public Function FindForInspect(oImageID As MIL_ID, oRecipeCodeReader As CRecipeCodeReader, oLog As II_LogTraceExtend) As AlarmCode
        If CodeReaderForInspect Is Nothing Then Return AlarmCode.IsReadCodeFailed

        Try
            msCodeText = ""
            oRecipeCodeReader.CodeZone = Rectangle.Empty

            '0923 修改使用FindMore 作為 Find 延伸 可以讀出多個結果，如果有毒到其中一個則直接回報不用再往下讀
            'If CodeReaderForInspect.Find(oImageID, oRecipeCodeReader) = False Then Return AlarmCode.IsReadCodeFailed
            If CodeReaderForInspect.FindMore(oImageID, oRecipeCodeReader) = False Then Return AlarmCode.IsReadCodeFailed

            If CodeReaderForInspect.Result.Score > 0 Then
                oRecipeCodeReader.CodeZone = CodeReaderForInspect.Result.CodeZone
                msCodeText = CodeReaderForInspect.Result.Code
                Call oLog.LogInformation(String.Format("讀取 Code 完成 (Inspect)！Code：{0}", msCodeText))
            Else
                Call oLog.LogError("讀取 Code 失敗 (Inspect)！")
                Return AlarmCode.IsReadCodeFailed
            End If
        Catch ex As Exception
            Call oLog.LogError(String.Format("讀取 Code 錯誤 (Inspect)，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("讀取 Code 錯誤 (Inspect)")
            Return AlarmCode.IsReadCodeFailed
        End Try
        Return AlarmCode.IsOK
    End Function

    Public Function FindForInspect2(oImageID As MIL_ID, oRecipeCodeReader As CRecipeCodeReader, oLog As II_LogTraceExtend) As AlarmCode
        If CodeReaderForInspect2 Is Nothing Then Return AlarmCode.IsReadCodeFailed

        Try
            msCodeText = ""
            oRecipeCodeReader.CodeZone = Rectangle.Empty

            '0924 修改使用FindMore 作為 Find 延伸 可以讀出多個結果，如果有毒到其中一個則直接回報不用再往下讀
            'If CodeReaderForInspect.Find(oImageID, oRecipeCodeReader) = False Then Return AlarmCode.IsReadCodeFailed
            If CodeReaderForInspect2.FindMore(oImageID, oRecipeCodeReader) = False Then Return AlarmCode.IsReadCodeFailed

            If CodeReaderForInspect2.Result.Score > 0 Then
                oRecipeCodeReader.CodeZone = CodeReaderForInspect2.Result.CodeZone
                msCodeText = CodeReaderForInspect2.Result.Code
                Call oLog.LogInformation(String.Format("讀取 Code 完成 (Inspect)！Code：{0}", msCodeText))
            Else
                Call oLog.LogError("讀取 Code 失敗 (Inspect)！")
                Return AlarmCode.IsReadCodeFailed
            End If
        Catch ex As Exception
            Call oLog.LogError(String.Format("讀取 Code 錯誤 (Inspect)，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("讀取 Code 錯誤 (Inspect)")
            Return AlarmCode.IsReadCodeFailed
        End Try
        Return AlarmCode.IsOK
    End Function

End Class