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

            '0923 修改使用FindMore 作為 Find 延伸 可以讀出多個結果，如果有讀到其中一個則直接回報不用再往下讀
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




    ''' <summary>
    ''' NoDie-Debug-測試用
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <param name="olog"></param>
    ''' <param name="stripId"></param>
    ''' <param name="mnSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NewBuildStripOriginalMapInfo(sPath As String, olog As II_LogTraceExtend, stripId As String, mnSequence As Integer) As Boolean
        'If sPath = "" AndAlso moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True

        Try
            Dim xDoc As XmlDocument = New XmlDocument

            xDoc.Load(String.Format("{0}\{1}.xml", sPath, stripId))
            If xDoc Is Nothing Then
                olog.LogError("讀取產品XML失敗，請確認XML檔案名稱")
                Return False
            End If

            Dim namespaceManager As XmlNamespaceManager = New XmlNamespaceManager(xDoc.NameTable)
            namespaceManager.AddNamespace("pf", "urn:semi-org:xsd.E142-1.V1005.SubstrateMap")

            Dim elemList As XmlNodeList = xDoc.GetElementsByTagName("Layout")
            Dim dimensionX As Integer = 0
            Dim dimensionY As Integer = 0

            For i As Integer = 0 To elemList.Count - 1 Step 1
                Dim xmlAttr1 As XmlAttribute = elemList(i).Attributes("LayoutId")
                Dim xmlAttr2 As XmlAttribute = elemList(i).Attributes("DefaultUnits")
                Dim value1 As String = xmlAttr1.Value
                Dim value2 As String = xmlAttr2.Value
                If value1 = "Device" AndAlso xmlAttr2.Value = "mm" Then
                    Dim elem As XmlNode = elemList(i)
                    If elem.ChildNodes.Count = 1 Then
                        For j As Integer = 0 To elem.ChildNodes.Count - 1 Step 1
                            Dim xAtrr As XmlAttribute = elem.ChildNodes(j).Attributes("X")
                            Dim yAtrr As XmlAttribute = elem.ChildNodes(j).Attributes("Y")
                            If xAtrr IsNot Nothing AndAlso yAtrr IsNot Nothing Then
                                Int32.TryParse(xAtrr.Value, dimensionX)
                                Int32.TryParse(yAtrr.Value, dimensionY)
                            End If
                        Next
                    End If
                End If
            Next

            Dim xNodesList As XmlNodeList = xDoc.SelectNodes("//pf:BinCode", namespaceManager)
            If xNodesList IsNot Nothing AndAlso xNodesList.Count = dimensionY Then

                '把NoDie座標存入csv檔中
                'NoDieIndexFile-------------------------20231002-開始--------------------------
                Dim dateTimeNow As Date = DateTime.Now
                Dim stwNoDieWriter As StreamWriter = Nothing
                Dim sPath1 As String = String.Format("{0}\NoDieIndexFile\{1:yyyy-MM}\{1:yyyy-MM-dd}\{1:HH_mm_ss_fff}", Application.StartupPath, dateTimeNow) '報告-重要路徑
                If Directory.Exists(sPath1) = False Then Directory.CreateDirectory(sPath1)
                Dim strNoDieFileName = String.Format(stripId & "-" & "[{0:d4}] NoDieIndexFile.csv", mnSequence)
                AppMgr.StrNoDieFilePath = Path.Combine(sPath1, strNoDieFileName)
                'NoDieIndexFile-------------------------20231002-結束--------------------------

                For nIndexY = 0 To dimensionY - 1 Step 1
                    Dim sBinCodeList As String = xNodesList.ItemOf(nIndexY).InnerText
                    For nIndexX = 0 To dimensionX - 1 Step 1

                        Select Case sBinCodeList.Substring(nIndexX * 4, 4)
                            Case "0000", "010F" 'No Die 之代碼

                                '把NoDie座標存入csv檔中
                                'NoDieIndexFile-------------------------20231002-開始--------------------------
                                If File.Exists(AppMgr.StrNoDieFilePath) = False Then
                                    stwNoDieWriter = New StreamWriter(Path:=AppMgr.StrNoDieFilePath, append:=True, Encoding:=Encoding.UTF8)
                                    If File.Exists(AppMgr.StrNoDieFilePath) = True Then
                                        stwNoDieWriter.WriteLine("recipeId" & "," & "lotId" & "," & stripId & "," & mnSequence & "," & (dimensionX - nIndexX) & "," & (nIndexY + 1))
                                    End If
                                Else
                                    stwNoDieWriter.WriteLine("recipeId" & "," & "lotId" & "," & stripId & "," & mnSequence & "," & (dimensionX - nIndexX) & "," & (nIndexY + 1))
                                End If
                                'NoDieIndexFile-------------------------20231002-結束--------------------------

                            Case Else

                        End Select
                    Next
                Next

                If stwNoDieWriter IsNot Nothing Then
                    stwNoDieWriter.Flush()
                    stwNoDieWriter.Close()
                End If
            Else
                olog.LogError("讀取產品XML失敗，請確認XML節點名稱正確和X、Y數量與RCP一致")
                Return False
            End If
        Catch ex As Exception
            olog.LogError(ex.Message & Environment.NewLine & ex.StackTrace)
            Return False
        End Try
        Return True
    End Function





End Class