Imports System.Windows.Forms.ListView

Partial Class CInspectSum

    ''' <summary>
    ''' usrDefectView.AddData -> CInspectSum.AddItem
    ''' </summary>
    ''' <param name="oItemList"></param>
    ''' <remarks></remarks>
    Public Sub AddItem(oItemList As ListViewItemCollection)
        With oItemList.Insert(0, InspectResult.Sequnce.ToString()) '�C���
            .SubItems.Add(Now.ToString())
            .SubItems.Add(InspectResult.RecipeID)
            .SubItems.Add(InspectResult.CodeID)
            .SubItems.Add((InspectResult.DefectCount - InspectResult.DefectNoDieCount).ToString()) '�岫-�ƶq

            If InspectResult.AlignStatus = True Then '��첧�`
                .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                .SubItems.Add("N")
            End If

            If InspectResult.FindStatus = True Then '�岫-Y(�˪O���`)
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

            If InspectResult.ModleInspectStatus = True Then '�˪O���`/�˴����` (�˪O)-���`:True
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Green
                .SubItems.Add("N")
            End If

            If InspectResult.ModleOffsetStatus = True Then '�˴����` (����)
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red
                .SubItems.Add("Y")
            Else
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Green
                .SubItems.Add("N")
            End If

            If InspectResult.ModleLoseStatus = True AndAlso (InspectResult.DefectCount - InspectResult.DefectNoDieCount) > 0 Then  '' Augustin 230310 '�|�p(CInspectResult)
                If .ForeColor <> Drawing.Color.Red Then .ForeColor = Drawing.Color.Red '�˴����` (�|�p)
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