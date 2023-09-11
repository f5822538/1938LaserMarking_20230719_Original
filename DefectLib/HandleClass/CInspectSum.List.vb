Imports System.Windows.Forms.ListView

Partial Class CInspectSum

    ''' <summary>
    ''' usrDefectView.AddData -> CInspectSum.AddItem
    ''' </summary>
    ''' <param name="oItemList"></param>
    ''' <remarks></remarks>
    Public Sub AddItem(oItemList As ListViewItemCollection)
        With oItemList.Insert(0, InspectResult.Sequnce.ToString()) '列表序
            .SubItems.Add(Now.ToString())
            .SubItems.Add(InspectResult.RecipeID)
            .SubItems.Add(InspectResult.CodeID)
            .SubItems.Add((InspectResult.DefectCount - InspectResult.DefectNoDieCount).ToString()) '瑕疵-數量

            If InspectResult.AlignStatus = True Then '對位異常
                .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                .SubItems.Add("N")
            End If

            If InspectResult.FindStatus = True Then '瑕疵-Y(樣板異常)
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                .SubItems.Add("N")
            End If

            'If InspectResult.CycleInspectStatus = True Then
            '    If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
            '    .SubItems.Add("Y")
            'Else
            '    .SubItems.Add("N")
            'End If

            If InspectResult.ModleInspectStatus = True Then '樣板異常/檢測異常 (樣板)-異常:True
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Green
                .SubItems.Add("N")
            End If

            If InspectResult.ModleOffsetStatus = True Then '檢測異常 (偏移)
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Green
                .SubItems.Add("N")
            End If

            If InspectResult.ModleLoseStatus = True AndAlso (InspectResult.DefectCount - InspectResult.DefectNoDieCount) > 0 Then  '' Augustin 230310 '漏雷(CInspectResult)
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red '檢測異常 (漏雷)
                .SubItems.Add("Y")
            Else
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Green
                .SubItems.Add("N")
            End If

            '' Augustin 230310
            'If InspectResult.DefectNoDieCount > 0 Then
            '    .SubItems.Add("Y")
            'Else
            '    .SubItems.Add("N")
            'End If

            .SubItems.Add(InspectResult.GetHtmlFileName)
            .SubItems.Add(InspectResult.GetDMFileName)
            .SubItems.Add(InspectResult.GetXmlFileName)

        End With
    End Sub
End Class