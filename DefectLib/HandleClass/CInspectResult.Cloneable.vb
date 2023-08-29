Partial Class CInspectResult

    Public Function TOCSVLine() As String
        Dim oSB As New StringBuilder
        oSB.Append(Sequnce & ",")
        oSB.Append(Now.ToString() & ",")
        oSB.Append(RecipeID & ",")
        oSB.Append(CodeID & ",")
        oSB.Append((DefectCount - DefectNoDieCount).ToString() & ",")  '' Augustin 230310 

        If AlignStatus = True Then '¹ï¦ì²§±`
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        If FindStatus = True Then '·å²«-Y
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        'If CycleInspectStatus = True Then
        '    oSB.Append("Y" & ",")
        'Else
        '    oSB.Append("N" & ",")
        'End If

        If ModleInspectStatus = True Then '¼ËªO²§±`/ÀË´ú²§±` (¼ËªO)
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        If ModleOffsetStatus = True Then 'ÀË´ú²§±` (°¾²¾)
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        If ModleLoseStatus = True AndAlso (DefectCount - DefectNoDieCount) > 0 Then 'º|¹p(CInspectResult)
            oSB.Append("Y" & ",") 'ÀË´ú²§±` (º|¹p)
        Else
            oSB.Append("N" & ",")
        End If

        '' Augustin 230310
        If DefectNoDieCount > 0 Then 'ÀË´ú²§±` (No Die)
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