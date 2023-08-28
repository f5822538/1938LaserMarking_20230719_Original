Partial Class CMyEquipment

    Public IsUpdateCodeReadResult As Boolean = False
    Public CodeReadResultList As New List(Of String)

    ''' <summary>
    ''' MainProject\HandleClass\Handle\CMyEquipment.UpdateCodeReadResult.vb
    ''' </summary>
    ''' <param name="oDataGridView"></param>
    ''' <param name="nColumn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateSizeDgvCodeReadResult(oDataGridView As DataGridView, nColumn As Integer) As Boolean
        Try
            oDataGridView.Columns.Clear()
            For nCount As Integer = 1 To nColumn
                Dim oDataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
                oDataGridView.Columns.Add(oDataGridViewTextBoxColumn)
            Next

            oDataGridView.Columns(0).Width = 80
            oDataGridView.Columns(0).HeaderText = "CodeReadResult"
            oDataGridView.Columns(0).ReadOnly = True

            oDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            oDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            oDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
            oDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            oDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            oDataGridView.ColumnHeadersHeight = 50
            oDataGridView.ColumnHeadersDefaultCellStyle.Font = New Font("微軟正黑體", 9.75, FontStyle.Bold)
            oDataGridView.RowHeadersDefaultCellStyle.Font = New Font("微軟正黑體", 9.75, FontStyle.Regular)
            oDataGridView.DefaultCellStyle.Font = New Font("微軟正黑體", 9.75, FontStyle.Regular)
            oDataGridView.AllowUserToAddRows = False
            oDataGridView.AllowUserToDeleteRows = False
            oDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

        Catch ex As Exception
            LogSystem.LogDebug(ex.ToString)
        End Try
        Return True

    End Function

    ''' <summary>
    ''' MainProject\HandleClass\Handle\CMyEquipment.UpdateCodeReadResult.vb
    ''' </summary>
    ''' <param name="oDataGridView"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateValueDgvCodeReadResult(oDataGridView As DataGridView) As Boolean
        Try
            If CodeReadResultList.Count = 0 Then Return False
            'oDataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(34, 34, 34)
            oDataGridView.RowCount = CodeReadResultList.Count
            For iy = 0 To CodeReadResultList.Count - 1
                oDataGridView.Rows.Item(iy).HeaderCell.Value = (iy + 1).ToString
                For ix = 0 To oDataGridView.Columns.Count - 1
                    oDataGridView.Rows(iy).Cells(ix).Value = CodeReadResultList(iy).ToString
                    If CodeReadResultList(iy).ToString = "Fail" Then
                        oDataGridView.Rows(iy).Cells(ix).Style.ForeColor = Color.Red
                    Else
                        oDataGridView.Rows(iy).Cells(ix).Style.ForeColor = Color.DarkGreen
                    End If
                Next
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
End Class