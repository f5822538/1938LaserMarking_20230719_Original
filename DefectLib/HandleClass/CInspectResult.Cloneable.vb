Partial Class CInspectResult

    Public Function TOCSVLine() As String
        Dim oSB As New StringBuilder
        oSB.Append(Sequnce & ",")
        oSB.Append(Now.ToString() & ",")
        oSB.Append(RecipeID & ",")
        oSB.Append(CodeID & ",")
        oSB.Append((DefectCount - DefectNoDieCount).ToString() & ",")  '' Augustin 230310 

        If AlignStatus = True Then '��첧�`
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        If FindStatus = True Then '�岫-Y(�˪O���`)
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        'If CycleInspectStatus = True Then
        '    oSB.Append("Y" & ",")
        'Else
        '    oSB.Append("N" & ",")
        'End If

        '-------------------------20230912-�}�l--------------------------
        'If ModleInspectStatus = True Then '�˪O���`/�˴����` (�˪O)-���`:True
        If ModleInspectStatus = True AndAlso (DefectCount - DefectNoDieCount) > 0 Then '�˪O���`/�˴����` (�˪O)-���`:True
            '-------------------------20230912-����--------------------------
            oSB.Append("Y" & ",") '((((((((((((((((((((((((((((((( ���n�϶� ))))))))))))))))))))))))))))))
        Else
            oSB.Append("N" & ",")
        End If


        If ModleOffsetStatus = True Then '�˴����` (����)
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        If ModleLoseStatus = True AndAlso (DefectCount - DefectNoDieCount) > 0 Then '�|�p(CInspectResult)
            oSB.Append("Y" & ",") '�˴����` (�|�p)
        Else
            oSB.Append("N" & ",")
        End If

        '' Augustin 230310
        If DefectNoDieCount > 0 Then '�˴����` (No Die)
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        oSB.Append(GetHtmlFileName() & ",")
        oSB.Append(GetDMFileName() & ",")
        oSB.Append(GetXmlFileName() & ",")

        oSB.Append(",")
        oSB.Append(",")
        oSB.Append(",")
        oSB.Append(",")
        oSB.Append(",")
        Return oSB.ToString
    End Function

End Class