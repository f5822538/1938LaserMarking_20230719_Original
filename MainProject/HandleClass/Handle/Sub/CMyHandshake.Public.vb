<TypeConverter(GetType(CEnumConvecter))>
Public Enum HandshakeType
    <Description("NA")> NA = 0
    <Description("產品資訊")> LotInfo = 1
    '<Description("下載產品分布")> StripMapDownload = 2
    <Description("上傳產品分布")> StripMapUpload = 3
End Enum

Partial Class CMyHandshake

    Private Const LotInfo As String = "LotInfo"
    'Private Const StripMapDownload As String = "StripMapDownload"
    Private Const StripMapUpload As String = "StripMapUpload"
    Private Const SOH As Byte = &H1
    Private Const EOT As Byte = &H4

    Public Delegate Function CheckSuccess() As Boolean
    Private msSOH As Char = Chr(SOH)
    Private msEOT As Char = Chr(EOT)

    Public Function CreateHandshakeThread() As Boolean
        Dim bIsOpen = True
        Try
            HandshakeThread = New CThreadServerExtend(moMyEquipment.LogHandshake, SynchronizationContext.Current, moHandshakeConfig.HandshakeIP, moHandshakeConfig.HandshakePort, 500)
            HandshakeThread.StartThread()
            moMyEquipment.MyLog.LogSystem.Log(LOGHandle.HANDLE_CREATE, String.Format("Server 開啟成功，IP：{0}。Port：{1}", moHandshakeConfig.HandshakeIP, moHandshakeConfig.HandshakePort))
        Catch ex As Exception
            moMyEquipment.MyLog.LogSystem.LogError(String.Format("Server 開啟失敗，IP：{0}。Port：{1}，Error：{2}", moHandshakeConfig.HandshakeIP, moHandshakeConfig.HandshakePort, ex.ToString))
            moMyEquipment.MyLog.LogAlarm.LogError(String.Format("Server 開啟失敗，IP：{0}。Port：{1}", moHandshakeConfig.HandshakeIP, moHandshakeConfig.HandshakePort))
            bIsOpen = False
        End Try
        Return bIsOpen
    End Function

    Public Sub CloseHandshakeThread()
        If HandshakeThread IsNot Nothing Then
            Call HandshakeThread.StopThread()
        End If
        HandshakeThread = Nothing
    End Sub

    Public Sub ClearReceive()
        HandshakeThread.ClearQueue()
    End Sub

    Public Function Read(ByRef oHandshakeType As HandshakeType, ByRef oProductList As List(Of CMyProduct), ByRef sLotRecipeID As String) As Boolean
        If moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
        Dim sTextList As String() = Nothing
        Dim sText As String = ""
        Try
            oHandshakeType = HandshakeType.NA
            sText = HandshakeThread.Receive()
            oHandshakeType = ReadType(sText, sTextList)
            Select Case oHandshakeType
                Case HandshakeType.LotInfo

                    If sTextList.Length >= 2 Then
                        Return BuildLotInfo(sTextList(1), oProductList, sLotRecipeID)
                    Else
                        ''2021201 調整當沒有逗號時會發生的問題
                        Return BuildLotInfo(sTextList(0), oProductList, sLotRecipeID)
                    End If

                    'Case HandshakeType.StripMapDownload : Return BuildStripMapDownload(sTextList(1), oProductProcess, oProductLotID)
                Case HandshakeType.StripMapUpload
                    If sTextList.Length >= 2 Then
                        Return StripMapUploadACK(sTextList(1))
                    Else
                        Return StripMapUploadACK(sTextList(0))
                    End If
            End Select
        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("讀取交握錯誤，Command：{0}。Error：{1}", sText, ex.ToString))
        End Try
        Return False
    End Function

    Public Function ReadType(sText As String, Optional ByRef sTextList As String() = Nothing) As HandshakeType
        If sText = "" Then Return HandshakeType.NA
        Try
            moMyEquipment.LogProcess.LogInformation(String.Format("接收到IT產品參數資訊字串訊息：{0}", sText))

            sTextList = Split(sText, ",")
            If sTextList.Length >= 2 Then
                Return DecodeText(sTextList(1))
            Else
                ''20211201  調整新方法沒有逗號的情況時
                Return DecodeText(sTextList(0))
            End If
        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("解析交握型態錯誤，Command：{0}。Error：{1}", sText, ex.ToString))
            Return HandshakeType.NA
        End Try
        Return HandshakeType.NA
    End Function

    Public Function BuildLotInfo(sText As String, ByRef oProductList As List(Of CMyProduct), ByRef sRecipeID As String) As Boolean
        If sText = "" AndAlso moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
        Dim oXmlDoc As XmlDocument = New XmlDocument
        Dim oXmlNode As XmlNode
        Dim oXmlList As XmlNodeList

        Dim sLotID As String = ""
        Dim sStepName As String = ""
        Dim sProdline As String = ""
        Dim sFloor As String = ""
        Dim sEQPID As String = ""
        Dim sEQPtype As String = ""
        Dim nProductCount As Integer = 0

        ''Augustin 220523 Read and Build XML File
        Try
            oXmlDoc.LoadXml(sText)
            oXmlNode = oXmlDoc.SelectSingleNode("Transaction/LotID")
            If oXmlNode IsNot Nothing Then
                sLotID = oXmlNode.InnerText
            Else
                Return False
            End If

            oXmlNode = oXmlDoc.SelectSingleNode("Transaction/RecipeID")
            If oXmlNode IsNot Nothing Then
                sRecipeID = oXmlNode.InnerText
            Else
                Return False
            End If

            'oXmlNode = oXmlDoc.SelectSingleNode("Transaction/StepName")
            'If oXmlNode IsNot Nothing Then
            '    sStepName = oXmlNode.InnerText
            'Else
            '    Return False
            'End If

            'oXmlNode = oXmlDoc.SelectSingleNode("Transaction/Prodline")
            'If oXmlNode IsNot Nothing Then
            '    sProdline = oXmlNode.InnerText
            'Else
            '    Return False
            'End If

            'oXmlNode = oXmlDoc.SelectSingleNode("Transaction/Floor")
            'If oXmlNode IsNot Nothing Then
            '    sFloor = oXmlNode.InnerText
            'Else
            '    Return False
            'End If

            'oXmlNode = oXmlDoc.SelectSingleNode("Transaction/EQPID")
            'If oXmlNode IsNot Nothing Then
            '    sEQPID = oXmlNode.InnerText
            'Else
            '    Return False
            'End If

            'oXmlNode = oXmlDoc.SelectSingleNode("Transaction/EQPtype")
            'If oXmlNode IsNot Nothing Then
            '    sEQPtype = oXmlNode.InnerText
            'Else
            '    Return False
            'End If

            oXmlList = oXmlDoc.SelectNodes("Transaction/StripIDList/StripID")
            If oXmlNode IsNot Nothing Then
                nProductCount = oXmlList.Count
            Else
                Return False
            End If

            For Each sStripID As Xml.XmlNode In oXmlList
                Dim oProduct As New CMyProduct
                oProduct.LotID = sLotID
                oProduct.RecipeID = sRecipeID
                oProduct.StepName = sStepName
                oProduct.Prodline = sProdline
                oProduct.Floor = sFloor
                oProduct.EQPID = sEQPID
                oProduct.EQPtype = sEQPtype
                oProduct.SubstrateID = sStripID.InnerText
                oProductList.Add(oProduct)
            Next
        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("解析產品資訊錯誤，Command：{0}。Error：{1}", sText, ex.ToString))
            Return False
        End Try
        Return True
    End Function

    ''注意這裡的spath暫時用自訂設置，以便之後調整
    Public Function BuildStripOriginalMapInfo(sPath As String, ByRef oProduct As CMyProduct, olog As II_LogTraceExtend) As Boolean
        'If sPath = "" AndAlso moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True

        Try
            sPath = "D:\img\ImportXML"
            Dim xDoc As XmlDocument = New XmlDocument

            xDoc.Load(String.Format("{0}\{1}.xml", sPath, oProduct.SubstrateID))
            If xDoc Is Nothing Then
                olog.LogError("讀取產品XML失敗，請確認XML檔案名稱")
                Return False
            End If

            Dim namespaceManager As XmlNamespaceManager = New XmlNamespaceManager(xDoc.NameTable)
            namespaceManager.AddNamespace("pf", "urn:semi-org:xsd.E142-1.V1005.SubstrateMap")
            Dim xNodesList As Xml.XmlNodeList = xDoc.SelectNodes("//pf:BinCode", namespaceManager)
            oProduct.MapDownLoadInfo = xDoc.InnerXml

            If xNodesList IsNot Nothing AndAlso xNodesList.Count = oProduct.DimensionY Then

                For nIndexY = 0 To oProduct.DimensionY - 1
                    Dim sBinCodeList As String = ""
                    sBinCodeList = xNodesList.ItemOf(nIndexY).InnerText

                    For nIndexX = 0 To oProduct.DimensionX - 1

                        'oProduct.MarkList((oProduct.DimensionX - 1 - nIndexX) + nIndexY * oProduct.DimensionX).OriginalBinCode = sBinCodeList.Substring(nIndexX * 4, 4)
                        ''10 18 測試修改
                        oProduct.MarkList((nIndexX) + nIndexY * oProduct.DimensionX).OriginalBinCode = sBinCodeList.Substring(nIndexX * 4, 4)

                        Select Case sBinCodeList.Substring(nIndexX * 4, 4)
                            Case moHandshakeConfig.IsNoDieCode

                                'oProduct.MarkList((oProduct.DimensionX - 1 - nIndexX) + nIndexY * oProduct.DimensionX).OriginalType = ResultType.NoDie
                                ''10 18 測試修改
                                oProduct.MarkList((nIndexX) + nIndexY * oProduct.DimensionX).OriginalType = ResultType.NoDie
                            Case Else

                        End Select
                    Next
                Next
            Else
                olog.LogError("讀取產品XML失敗，請確認XML節點名稱正確和X、Y數量與RCP一致")
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Function SendStripMapUploadIncludeOriginalMapInfo(ByRef oProduct As CMyProduct) As Boolean

        If moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True

        Dim sInspectMap As String = <Overlay MapName="UploadBinMap">
                                        <BinCodeMap BinType="Integer2" NullBin="0000">
                                            <BinCode>BinCode</BinCode>
                                        </BinCodeMap>
                                    </Overlay>.ToString
        Try
            Dim sBinCode As String = ""
            Dim nOffsetIndex As Integer = 0
            For nIndexY As Integer = 0 To oProduct.DimensionY - 1
                Dim sBinCodeLine As String = "<BinCode>"
                For nIndexX As Integer = 0 To oProduct.DimensionX - 1

                    ''10 18 測試修改
                    Dim nIndex As Integer = nIndexY * oProduct.DimensionX + nIndexX - nOffsetIndex

                    'Dim nIndex As Integer = (oProduct.DimensionX - 1 - nIndexX) + nIndexY * oProduct.DimensionX + nOffsetIndex

                    Dim sBinCodeResult As String = ""
                    If oProduct.MarkList.Count <= nIndex Then
                        sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode

                        ''10 18 測試修改
                    ElseIf oProduct.MarkList.Item(nIndex).MarkX <> nIndexX OrElse oProduct.MarkList.Item(nIndex).MarkY <> nIndexY Then
                        'ElseIf oProduct.MarkList.Item(nIndex).MarkX <> (oProduct.DimensionX - 1 - nIndexX) OrElse oProduct.MarkList.Item(nIndex).MarkY <> nIndexY Then

                        sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                        nOffsetIndex += 1
                    Else
                        If oProduct.MarkList.Item(nIndex).Result = ResultType.NoDie Then
                            sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNoDieCode
                        Else
                            If oProduct.MarkList.Item(nIndex).IsGray = True Then
                                sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsGrayCode
                            Else
                                Select Case oProduct.MarkList.Item(nIndex).Result
                                    Case ResultType.OK : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsOKCode
                                    Case ResultType.NGDark : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                    Case ResultType.NGBright : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                    Case ResultType.Offset : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                    Case ResultType.Lose : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                    Case ResultType.Indistinct : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                    Case Else : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsGrayCode
                                End Select
                            End If
                        End If
                    End If
                    sBinCodeLine = String.Format("{0}{1}", sBinCodeLine, sBinCodeResult)
                Next
                sBinCode = String.Format("{0}{1}</BinCode>", sBinCode, sBinCodeLine)
            Next

            sInspectMap = sInspectMap.Replace("<BinCode>BinCode</BinCode>", sBinCode)

            Dim sMapAll As String = ""
            sMapAll = oProduct.MapDownLoadInfo.Replace("</SubstrateMap>", sInspectMap + "</SubstrateMap>")
            sMapAll = sMapAll.Replace(vbCr, "")
            sMapAll = sMapAll.Replace(vbLf, "")
            sMapAll = sMapAll.Replace(vbCrLf, "")
            sMapAll = sMapAll.Replace(">  <", "><")
            If moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.IsExportStripMapXML = True Then
                If Directory.Exists(moMyEquipment.HardwareConfig.MiscConfig.ExportProductXmlPath) = False Then
                    Call Directory.CreateDirectory(moMyEquipment.HardwareConfig.MiscConfig.ExportProductXmlPath)
                End If
                Dim oFile As New StreamWriter(String.Format("{0}\{1}.xml", moMyEquipment.HardwareConfig.MiscConfig.ExportProductXmlPath, oProduct.SubstrateID))
                oFile.Write(sMapAll)
                oFile.Close()
            End If

            Dim sTransaction As String = <Transaction Name="StripMapUpload" Type="Request">
                                             <Image>IsUpLoadInspectPicture</Image><Downtime>IsUpLoadMarkShiftPicture</Downtime>TransferContent</Transaction>.ToString()
            oProduct.StripMap = sTransaction.Replace("IsUpLoadInspectPicture", moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.IsUpLoadInspectPicture.ToDescription.ToString)
            oProduct.StripMap = oProduct.StripMap.Replace("IsUpLoadMarkShiftPicture", moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.UpLoadMarkShiftPictureToIT)

            oProduct.StripMap = oProduct.StripMap.Replace("TransferContent", sMapAll)
            oProduct.StripMap = oProduct.StripMap.Replace(vbCr, "")
            oProduct.StripMap = oProduct.StripMap.Replace(vbLf, "")
            oProduct.StripMap = oProduct.StripMap.Replace(vbCrLf, "")
            oProduct.StripMap = oProduct.StripMap.Replace(">  <", "><")
            oProduct.StripMap = String.Format("{0},{1},{2}", msSOH, oProduct.StripMap, msEOT)

            Call HandshakeThread.Send(oProduct.StripMap)

        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("發送產品 Map 錯誤，Command：{0}。Error：{1}", oProduct.StripMap, ex.ToString))
            Return False
        End Try
        Return True
    End Function

    'Public Function BuildStripMapDownload(sText As String, ByRef oProductProcess As CMyProduct, oProductLotID As CMyProduct) As Boolean
    '    If sText = "" AndAlso moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
    '    Dim oXmlDoc As XmlDocument = New XmlDocument
    '    Dim oXmlNamespaceManager As XmlNamespaceManager
    '    Dim oXmlNodeList As XmlNodeList
    '    Dim oXmlNode As XmlNode
    '    Try
    '        oProductProcess.LotID = oProductLotID.LotID
    '        oProductProcess.RecipeID = oProductLotID.RecipeID
    '        oProductProcess.StripMap = sText
    '        oXmlDoc.LoadXml(sText)
    '        oXmlNamespaceManager = New XmlNamespaceManager(oXmlDoc.NameTable)
    '        oXmlNamespaceManager.AddNamespace("ns", "urn:semi-org:xsd.E142-1.V1005.SubstrateMap")
    '        oXmlNodeList = oXmlDoc.SelectNodes("Transaction/ns:MapData/ns:Layouts/ns:Layout", oXmlNamespaceManager)
    '        If oXmlNodeList IsNot Nothing AndAlso oXmlNodeList.Count >= 3 Then
    '            oProductProcess.DimensionX = CInt(oXmlNodeList(2).ChildNodes(0).Attributes("X").Value)
    '            oProductProcess.DimensionY = CInt(oXmlNodeList(2).ChildNodes(0).Attributes("Y").Value)
    '        Else
    '            Return False
    '        End If
    '        oXmlNode = oXmlDoc.SelectSingleNode("Transaction/ns:MapData/ns:SubstrateMaps/ns:SubstrateMap", oXmlNamespaceManager)
    '        If oXmlNodeList IsNot Nothing Then
    '            oProductProcess.SubstrateID = oXmlNode.Attributes("SubstrateId").Value
    '        Else
    '            Return False
    '        End If

    '        oProductProcess.MarkList.Clear()
    '        oXmlNodeList = oXmlDoc.SelectNodes("Transaction/ns:MapData/ns:SubstrateMaps/ns:SubstrateMap/ns:Overlay/ns:BinCodeMap/ns:BinCode", oXmlNamespaceManager)
    '        If oXmlNodeList IsNot Nothing AndAlso oXmlNodeList.Count >= oProductProcess.DimensionY Then
    '            For nIndexY As Integer = 0 To oProductProcess.DimensionY - 1
    '                Dim sInnerText As String = oXmlNodeList(nIndexY).ChildNodes(0).InnerText
    '                For nIndexX As Integer = 0 To oProductProcess.DimensionX - 1
    '                    Dim oMarkInfo As New CMyMarkInfo
    '                    Dim oBinCode As String = Mid(sInnerText, 1, 4)
    '                    oMarkInfo.MarkX = nIndexX
    '                    oMarkInfo.MarkY = nIndexY
    '                    oMarkInfo.BinCode = oBinCode
    '                    oMarkInfo.IsProcess = If(oBinCode = moMyEquipment.HardwareConfig.HandshakeConfig.IsProcessCode, True, False)
    '                    oProductProcess.MarkList.Add(oMarkInfo)
    '                    sInnerText = Mid(sInnerText, 5)
    '                Next
    '            Next
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        moMyEquipment.LogHandshake.LogError(String.Format("解析產品資訊錯誤，Command：{0}。Error：{1}", sText, ex.ToString))
    '        Return False
    '    End Try

    '    Return True
    'End Function

    Public Function StripMapUploadACK(sText As String) As Boolean
        If sText = "" AndAlso moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
        Dim oXmlDoc As XmlDocument = New XmlDocument
        Dim oXmlNode As XmlNode
        Try
            oXmlDoc.LoadXml(sText)
            oXmlNode = oXmlDoc.SelectSingleNode("Transaction/ErrorCode")
            If oXmlNode IsNot Nothing Then
                If oXmlNode.InnerText <> "" Then
                    moMyEquipment.LogHandshake.LogError(String.Format("Map Upload Error Code：{0}", oXmlNode.InnerText))
                    Return False
                End If
            Else
                Return False
            End If
            oXmlNode = oXmlDoc.SelectSingleNode("Transaction/ErrorText")
            If oXmlNode IsNot Nothing Then
                If oXmlNode.InnerText <> "" Then
                    moMyEquipment.LogHandshake.LogError(String.Format("Map Upload Error Text：{0}", oXmlNode.InnerText))
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("解析產品資訊錯誤，Command：{0}。Error：{1}", sText, ex.ToString))
            Return False
        End Try
        Return True
    End Function

    Public Function SendLotInfoACK(oAlarmCode As AlarmCode, sAlarmText As String) As Boolean
        If moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
        Dim sContent As String =
    <Transaction Name=<%= LotInfo %> Type="Reply">
        <ErrorCode><%= oAlarmCode.GetHashCode().ToString() %></ErrorCode>
        <ErrorText><%= sAlarmText %></ErrorText>
    </Transaction>.ToString()

        Try
            sContent = sContent.Replace(vbCr, "")
            sContent = sContent.Replace(vbLf, "")
            sContent = sContent.Replace(">  <", "><")
            sContent = String.Format("{0},{1},{2}", msSOH, sContent, msEOT)
            Call HandshakeThread.Send(sContent)
        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("發送產品資訊回復錯誤，Command：{0}。Error：{1}", sContent, ex.ToString))
            Return False
        End Try
        Return True
    End Function

    'Public Function SendStripMapDownloadACK(oAlarmCode As AlarmCode, oProduct As CMyProduct) As Boolean
    '    If moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
    '    Dim sContent As String =
    '<Transaction Name=<%= StripMapDownload %> Type="Reply">
    '    <SubstrateId><%= oProduct.SubstrateID %></SubstrateId>
    '    <ErrorCode><%= oAlarmCode.GetHashCode().ToString() %></ErrorCode>
    '    <ErrorText><%= If(oAlarmCode = AlarmCode.IsOK, "ACK", EnumHelper.GetDescription(oAlarmCode)).ToString() %></ErrorText>
    '</Transaction>.ToString()

    '    Try
    '        sContent = sContent.Replace(vbCr, "")
    '        sContent = sContent.Replace(vbLf, "")
    '        sContent = sContent.Replace(">  <", "><")
    '        sContent = String.Format("{0},{1},{2}", msSOH, sContent, msEOT)
    '        Call HandshakeThread.Send(sContent)
    '    Catch ex As Exception
    '        moMyEquipment.LogHandshake.LogError(String.Format("取得產品資訊錯誤，Command：{0}。Error：{1}", sContent, ex.ToString))
    '        Return False
    '    End Try
    '    Return True
    'End Function

    Public Function SendStripMapUpload(oProduct As CMyProduct) As Boolean
        If moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
        oProduct.StripMap = <Transaction Name="StripMapUpload" Type="Request">
                                <xml></xml>
                                <MapData xmlns="urn:semi-org:xsd.E142-1.V1005.SubstrateMap">
                                    <Layouts>
                                        <Layout LayoutId="Strip" DefaultUnits="mm">
                                            <Dimension X="1" Y="1"/>
                                        </Layout>
                                        <Layout LayoutId="SubMatrix" DefaultUnits="mm">
                                            <Dimension X="1" Y="1"/>
                                        </Layout>
                                        <Layout LayoutId="Device" DefaultUnits="mm">
                                            <Dimension X="ProductDimensionX" Y="ProductDimensionY"/>
                                        </Layout>
                                    </Layouts>
                                    <SubstrateMaps>
                                        <SubstrateMap SubstrateType="Strip" SubstrateId="ProductSubstrateID" LayoutSpecifier="Strip/SubMatrix/Device">
                                            <Overlay MapName="BinCodeMap" MapVersion="1">
                                                <BinCodeMap BinType="Integer2" NullBin="0000">
                                                    <BinCode>BinCode</BinCode>
                                                </BinCodeMap>
                                            </Overlay>
                                        </SubstrateMap>
                                    </SubstrateMaps>
                                </MapData>
                            </Transaction>.ToString()

        Try
            Dim sBinCode As String = ""
            Dim nOffsetIndex As Integer = 0
            For nIndexY As Integer = 0 To oProduct.DimensionY - 1
                Dim sBinCodeLine As String = "<BinCode>"
                For nIndexX As Integer = 0 To oProduct.DimensionX - 1
                    Dim nIndex As Integer = nIndexY * oProduct.DimensionX + nIndexX - nOffsetIndex
                    Dim sBinCodeResult As String = ""
                    If oProduct.MarkList.Count <= nIndex Then
                        sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                    ElseIf oProduct.MarkList.Item(nIndex).MarkX <> nIndexX OrElse oProduct.MarkList.Item(nIndex).MarkY <> nIndexY Then
                        sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                        nOffsetIndex += 1
                    Else
                        If oProduct.MarkList.Item(nIndex).IsGray = True Then
                            sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsGrayCode
                        Else
                            Select Case oProduct.MarkList.Item(nIndex).Result
                                Case ResultType.OK : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsOKCode
                                Case ResultType.NGDark : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                Case ResultType.NGBright : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                Case ResultType.Offset : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                Case ResultType.Lose : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                Case ResultType.Indistinct : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGCode
                                    'Case ResultType.NGDark : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGDarkCode
                                    'Case ResultType.NGBright : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsNGBrightCode
                                    'Case ResultType.Offset : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsOffsetCode
                                    'Case ResultType.Lose : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsLoseCode
                                    'Case ResultType.Pass : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsPassCode
                                    'Case ResultType.Indistinct : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsIndistinctCode
                                Case Else : sBinCodeResult = moMyEquipment.HardwareConfig.HandshakeConfig.IsGrayCode
                            End Select
                        End If
                    End If
                    sBinCodeLine = String.Format("{0}{1}", sBinCodeLine, sBinCodeResult)
                Next
                sBinCode = String.Format("{0}{1}</BinCode>", sBinCode, sBinCodeLine)
            Next

            oProduct.StripMap = oProduct.StripMap.Replace("<xml></xml>", "<?xml version=""1.0"" encoding=""utf-8""?>")
            oProduct.StripMap = oProduct.StripMap.Replace("ProductDimensionX", oProduct.DimensionX.ToString())
            oProduct.StripMap = oProduct.StripMap.Replace("ProductDimensionY", oProduct.DimensionY.ToString())
            oProduct.StripMap = oProduct.StripMap.Replace("ProductSubstrateID", oProduct.SubstrateID)
            oProduct.StripMap = oProduct.StripMap.Replace("<BinCode>BinCode</BinCode>", sBinCode)
            oProduct.StripMap = oProduct.StripMap.Replace(vbCr, "")
            oProduct.StripMap = oProduct.StripMap.Replace(vbLf, "")
            oProduct.StripMap = oProduct.StripMap.Replace(">  <", "><")
            oProduct.StripMap = String.Format("{0},{1},{2}", msSOH, oProduct.StripMap, msEOT)
            Call HandshakeThread.Send(oProduct.StripMap)
        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("發送產品 Map 錯誤，Command：{0}。Error：{1}", oProduct.StripMap, ex.ToString))
            Return False
        End Try
        Return True
    End Function

    Public Function UpdateAIInfo(oProduct As CMyProduct, oInspectSum As CInspectSum) As Boolean
        If moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return True
        oProduct.StripMap = <?xml version="1.0" encoding="UTF-8"?>
                            <Transaction Name="AIImageInfo"><Lot>LotID</Lot><StripID>StripID</StripID><StepName>StepName</StepName><ProdLine>Prodline</ProdLine><Floor>Floor</Floor><EqpId>EQPID</EqpId><EqpType>EQPtype</EqpType><RecipeName>RecipeID</RecipeName>
                                <IMAGE Count="NumberOfCount"><item>ImageName</item>
                                </IMAGE></Transaction>.ToString()
        Try
            Dim AllDefectImageName As String = ""
            For nnIndexDefect As Integer = 0 To oInspectSum.DefectList.DefectList.Count - 1
                Dim DefectImageNameLine As String = "<item>"
                Dim DefectSingleImageName As String = ""

                DefectSingleImageName = String.Format("{0}_{1}_R{2:d3}_C{3:d3}_{4:yyyyMMddHHHmmss}.bmp", oInspectSum.ProductConfig.RecipeID, oInspectSum.ProductConfig.SubstrateID, oInspectSum.DefectList.DefectList.Item(nnIndexDefect).DefectIndex.X, oInspectSum.DefectList.DefectList.Item(nnIndexDefect).DefectIndex.Y, oInspectSum.ReceiveTime)
                DefectImageNameLine = String.Format("{0}{1}", DefectImageNameLine, DefectSingleImageName)
                AllDefectImageName = String.Format("{0}{1}</item>", AllDefectImageName, DefectImageNameLine)
            Next

            'Dim AllImageName As String = AllDefectImageName
            'For nnIndexOK As Integer = 0 To oInspectSum.DefectList.OKList.Count - 1
            '    Dim OKImageNameLine As String = "<item>"
            '    Dim OKSingleImageName As String = ""

            '    OKSingleImageName = String.Format("{0}_{1}_R{2:d3}_C{3:d3}_{4:yyyyMMddHHHmmss}.bmp", oInspectSum.ProductConfig.RecipeID, oInspectSum.ProductConfig.SubstrateID, oInspectSum.DefectList.OKList.Item(nnIndexOK).DefectIndex.X, oInspectSum.DefectList.OKList.Item(nnIndexOK).DefectIndex.Y, oInspectSum.ReceiveTime)
            '    OKImageNameLine = String.Format("{0}{1}", OKImageNameLine, OKSingleImageName)
            '    AllImageName = String.Format("{0}{1}</item>", AllImageName, OKImageNameLine)
            'Next

            oProduct.StripMap = oProduct.StripMap.Replace("LotID", oInspectSum.ProductConfig.LotID)
            oProduct.StripMap = oProduct.StripMap.Replace("StepName", oInspectSum.ProductConfig.StepName)
            oProduct.StripMap = oProduct.StripMap.Replace("Prodline", oInspectSum.ProductConfig.Prodline)
            oProduct.StripMap = oProduct.StripMap.Replace("Floor", oInspectSum.ProductConfig.Floor)
            oProduct.StripMap = oProduct.StripMap.Replace("EQPID", oInspectSum.ProductConfig.EQPID)
            oProduct.StripMap = oProduct.StripMap.Replace("EQPtype", oInspectSum.ProductConfig.EQPtype)
            oProduct.StripMap = oProduct.StripMap.Replace("RecipeID", oInspectSum.ProductConfig.RecipeID)
            oProduct.StripMap = oProduct.StripMap.Replace("NumberOfCount", (oInspectSum.DefectList.DefectList.Count).ToString)

            oProduct.StripMap = oProduct.StripMap.Replace("<item>ImageName</item>", AllDefectImageName)
            oProduct.StripMap = oProduct.StripMap.Replace(vbCr, "")
            oProduct.StripMap = oProduct.StripMap.Replace(vbLf, "")
            oProduct.StripMap = oProduct.StripMap.Replace(">  <", "><")

            Try
                Dim oFile As New StreamWriter(String.Format("{0}.xml", oInspectSum.InspectResult.AIXMLFileName))
                oFile.Write(oProduct.StripMap)
                oFile.Close()
            Catch ex As Exception
                moMyEquipment.LogProcess.LogError(String.Format("寫入 AI XML 檔案錯誤，Command：{0}。Error：{1}", oProduct.StripMap, ex.ToString))
            End Try

            Call HandshakeThread.Send(oProduct.StripMap)
        Catch ex As Exception
            moMyEquipment.LogHandshake.LogError(String.Format("發送 AI 影像資訊錯誤，Command：{0}。Error：{1}", oProduct.StripMap, ex.ToString))
            Return False
        End Try
        Return True
    End Function

    Public Function WaitReceive(ByRef sReceive As String, oCheckExit As CheckSuccess, nTime As Integer) As AlarmCode
        If moMyEquipment.HardwareConfig.HandshakeBypass = True Then Return AlarmCode.IsOK
        If HandshakeThread Is Nothing AndAlso HandshakeThread.IsConnected = False Then Return AlarmCode.IsHandshakeNotConnected
        Dim oTimeStart As Date = DateAndTime.Now
        sReceive = ""
        Try
            While True
                If oCheckExit() = True Then
                    Return AlarmCode.IsStop
                End If

                If (DateAndTime.Now - oTimeStart).TotalSeconds > nTime Then
                    Return AlarmCode.IsWaitHandshakeTimeout
                End If

                sReceive = HandshakeThread.Receive()
                If sReceive.Length > 0 Then Return AlarmCode.IsOK

                Thread.Sleep(10)
            End While

            Return AlarmCode.IsOK
        Catch ex As Exception
            moMyEquipment.MyLog.LogHandshake.LogError(String.Format("等待交握通訊回復錯誤，Error：{0}", ex.ToString))
            Return AlarmCode.IsHandshakeFailed
        End Try
    End Function

    Public Function DecodeText(sText As String) As HandshakeType
        Dim oXmlDoc As XmlDocument = New XmlDocument
        Dim oXmlNode As XmlNode
        Try
            sText = sText.Replace("<?xml version=""1.0""?>", "")
            oXmlDoc.LoadXml(sText)
            oXmlNode = oXmlDoc.SelectSingleNode("Transaction")
            If oXmlNode IsNot Nothing Then
                Select Case oXmlNode.Attributes("Name").Value
                    Case LotInfo : Return HandshakeType.LotInfo
                        'Case StripMapDownload : Return HandshakeType.StripMapDownload
                    Case StripMapUpload : Return HandshakeType.StripMapUpload
                End Select
            End If
        Catch ex As Exception
            moMyEquipment.MyLog.LogHandshake.LogError(String.Format("解讀交握格式錯誤，Error：{0}", ex.ToString))
        End Try
        Return HandshakeType.NA
    End Function
End Class