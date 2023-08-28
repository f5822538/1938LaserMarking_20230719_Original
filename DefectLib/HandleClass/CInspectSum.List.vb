Imports System.Windows.Forms.ListView

Partial Class CInspectSum

    Public Sub AddItem(oItemList As ListViewItemCollection)
        With oItemList.Insert(0, InspectResult.Sequnce.ToString()) '¦Cªí§Ç
            .SubItems.Add(Now.ToString())
            .SubItems.Add(InspectResult.RecipeID)
            .SubItems.Add(InspectResult.CodeID)
            .SubItems.Add((InspectResult.DefectCount - InspectResult.DefectNoDieCount).ToString())

            If InspectResult.AlignStatus = True Then
                .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                .SubItems.Add("N")
            End If

            If InspectResult.FindStatus = True Then '·å²«-Y
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

            If InspectResult.ModleInspectStatus = True Then
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Green
                .SubItems.Add("N")
            End If

            If InspectResult.ModleOffsetStatus = True Then
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Green
                .SubItems.Add("N")
            End If

            If InspectResult.ModleLoseStatus = True AndAlso (InspectResult.DefectCount - InspectResult.DefectNoDieCount) > 0 Then  '' Augustin 230310 'º|¹p(CInspectResult)
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
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