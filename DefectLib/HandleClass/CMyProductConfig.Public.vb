Partial Class CMyProductConfig

    Public Sub CopyTo(ByRef oProduct As CMyProduct)
        Dim oMarkInfoList As New List(Of CMyMarkInfo)
        MarkList.ForEach(Sub(o As CMyMarkInfo)
                             Dim oMarkInfo As New CMyMarkInfo
                             'oMarkInfo.IsProcess = o.IsProcess
                             oMarkInfo.AfterInspectBinCode = o.AfterInspectBinCode
                             oMarkInfo.MarkX = o.MarkX
                             oMarkInfo.MarkY = o.MarkY
                             oMarkInfo.Result = o.Result
                             oMarkInfo.IsGray = o.IsGray
                             oMarkInfoList.Add(oMarkInfo)
                         End Sub)

        With oProduct
            .StripMap = StripMap
            .ErrorCode = ErrorCode
            .ErrorText = ErrorText
            .LotID = LotID
            .StepName = StepName
            .Prodline = Prodline
            .Floor = Floor
            .EQPID = EQPID
            .EQPtype = EQPtype
            .RecipeID = RecipeID
            .SubstrateID = SubstrateID
            .DimensionX = DimensionX
            .DimensionY = DimensionY
            .MarkList.Clear()
            .MarkList = oMarkInfoList
        End With
    End Sub

    Public Sub ClearMark()
        StripMap = ""
        ErrorCode = ""
        ErrorText = ""
        StepName = ""
        Prodline = ""
        Floor = ""
        EQPID = ""
        EQPtype = ""
        SubstrateID = ""
        DimensionX = 1
        DimensionY = 1
        MarkList.Clear()
    End Sub
End Class