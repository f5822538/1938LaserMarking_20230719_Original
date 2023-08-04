Partial Class CMyEquipment

    Public ProductList As New List(Of CMyProduct)
    Public LotRecipeID As String = ""
    Public MaxDefectCountForUpdateMap As Integer = 0
    Public MaxOffsetCountForUpdateToFttp As Integer = 0

    Public Function SelectProduct(ByRef oProduct As CMyProduct, oSubstrateID As String) As CMyProduct
        Dim oProductList As List(Of CMyProduct) = (From o In ProductList Where o.SubstrateID = oSubstrateID).ToList()

        If oProduct Is Nothing Then oProduct = New CMyProduct
        oProduct.SubstrateID = ""
        If oProductList.Count > 0 Then
            oProduct.ClearMark()
            oProduct = CopyProduct(oProductList.Item(0))
            Return oProduct
        Else
            Return oProduct
        End If
    End Function

    Public Function CopyProduct(oProduct As CMyProduct) As CMyProduct
        Dim oProductCopy As New CMyProduct

        With oProductCopy
            .StripMap = oProduct.StripMap
            .ErrorCode = oProduct.ErrorCode
            .ErrorText = oProduct.ErrorText
            .LotID = oProduct.LotID
            .StepName = oProduct.StepName
            .Prodline = oProduct.Prodline
            .Floor = oProduct.Floor
            .EQPID = oProduct.EQPID
            .EQPtype = oProduct.EQPtype
            .RecipeID = oProduct.RecipeID
            .SubstrateID = oProduct.SubstrateID
            .DimensionX = oProduct.DimensionX
            .DimensionY = oProduct.DimensionY
            .MarkList.Clear()

            oProduct.MarkList.ForEach(Sub(o As CMyMarkInfo)
                                          Dim oMarkInfo As New CMyMarkInfo
                                          oMarkInfo.AfterInspectBinCode = o.AfterInspectBinCode
                                          oMarkInfo.MarkX = o.MarkX
                                          oMarkInfo.MarkY = o.MarkY
                                          oMarkInfo.Result = o.Result
                                          oMarkInfo.IsGray = o.IsGray
                                          .MarkList.Add(oMarkInfo)
                                      End Sub)
        End With

        Return oProductCopy
    End Function

    Public Function RemoveProduct(CodeID As String) As AlarmCode
        If moHardwareConfig.HandshakeBypass = False Then
            For nIndex As Integer = 0 To ProductList.Count - 1
                If ProductList.Item(nIndex).SubstrateID = CodeID Then
                    Call ProductList.RemoveAt(nIndex)
                    Return AlarmCode.IsOK
                End If
            Next
        Else
            Return AlarmCode.IsOK
        End If
        Return AlarmCode.IsOK
    End Function
End Class