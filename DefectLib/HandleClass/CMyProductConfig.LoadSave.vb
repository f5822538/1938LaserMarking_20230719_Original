Partial Class CMyProductConfig : Implements II_LoadSystem

    Public Sub LoadConfig() Implements II_LoadSystem.LoadConfig
        Dim oResult As Tuple(Of Boolean, String()) = moSetting.GetSectionValue(csSystemName)

        If oResult.Item1 = True Then
            StripMap = oResult.Item2(0).Replace("StripMap=", "")
            ErrorCode = moSetting.GetKeyValue("System", "ErrorCode", "")
            ErrorText = moSetting.GetKeyValue("System", "ErrorText", "")
            LotID = moSetting.GetKeyValue("System", "LotID", "")
            StepName = moSetting.GetKeyValue("System", "StepName", "")
            Prodline = moSetting.GetKeyValue("System", "Prodline", "")
            Floor = moSetting.GetKeyValue("System", "Floor", "")
            EQPID = moSetting.GetKeyValue("System", "EQPID", "")
            EQPtype = moSetting.GetKeyValue("System", "EQPtype", "")
            RecipeID = moSetting.GetKeyValue("System", "RecipeID", "")
            SubstrateID = moSetting.GetKeyValue("System", "SubstrateID", "")
            DimensionX = moSetting.GetKeyValue("System", "DimensionX", -1)
            DimensionY = moSetting.GetKeyValue("System", "DimensionY", -1)
            MarkListString = oResult.Item2(9).Replace("MarkListString=", "")
            Dim nMarkCount As Integer = moSetting.GetKeyValue("System", "MarkCount", -1)

            Dim sMarkList As String() = Split(MarkListString, ",")
            MarkList.Clear()
            For nMarkIndex = 0 To nMarkCount - 1
                Dim oMarkInfo As New CMyMarkInfo()
                Dim nIndex As Integer = nMarkIndex * 5 '我是nIndex
                'oMarkInfo.IsProcess = CBool(sMarkList(nIndex + 0))
                oMarkInfo.AfterInspectBinCode = sMarkList(nIndex + 0)
                oMarkInfo.MarkX = CInt(sMarkList(nIndex + 1))
                oMarkInfo.MarkY = CInt(sMarkList(nIndex + 2))
                oMarkInfo.Result = CType(sMarkList(nIndex + 3), ResultType)
                oMarkInfo.IsGray = CBool(sMarkList(nIndex + 4))
                MarkList.Add(oMarkInfo)
            Next
        End If
    End Sub

    Public Sub SaveConfig() Implements II_LoadSystem.SaveConfig
        MarkListString = ""

        If MarkList.Count > 0 Then
            MarkListString = String.Format("{0},{1},{2},{3},{4}", MarkList.Item(0).AfterInspectBinCode, MarkList.Item(0).MarkX, MarkList.Item(0).MarkY, CInt(MarkList.Item(0).Result), MarkList.Item(0).IsGray)
            For nIndex = 1 To MarkCount - 1 '我是nIndex
                MarkListString = String.Format("{0},{1},{2},{3},{4},{5}", MarkListString, MarkList.Item(nIndex).AfterInspectBinCode, MarkList.Item(nIndex).MarkX, MarkList.Item(nIndex).MarkY, CInt(MarkList.Item(nIndex).Result), MarkList.Item(0).IsGray)
            Next
        End If

        moSetting.SimpleSet(csSystemName, Me)
    End Sub
End Class