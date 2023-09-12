Partial Class CInspectResult

    Public Function TOCSVLine() As String
        Dim oSB As New StringBuilder
        oSB.Append(Sequnce & ",")
        oSB.Append(Now.ToString() & ",")
        oSB.Append(RecipeID & ",")
        oSB.Append(CodeID & ",")
        oSB.Append((DefectCount - DefectNoDieCount).ToString() & ",")  '' Augustin 230310 

        If AlignStatus = True Then '對位異常
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        If FindStatus = True Then '瑕疵-Y(樣板異常)
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        'If CycleInspectStatus = True Then
        '    oSB.Append("Y" & ",")
        'Else
        '    oSB.Append("N" & ",")
        'End If

        '-------------------------20230912-開始--------------------------
        'If ModleInspectStatus = True Then '樣板異常/檢測異常 (樣板)-異常:True
        If ModleInspectStatus = True AndAlso (DefectCount - DefectNoDieCount) > 0 Then '樣板異常/檢測異常 (樣板)-異常:True
            '-------------------------20230912-結束--------------------------
            oSB.Append("Y" & ",") '((((((((((((((((((((((((((((((( 重要區塊 ))))))))))))))))))))))))))))))
        Else
            oSB.Append("N" & ",")
        End If


        If ModleOffsetStatus = True Then '檢測異常 (偏移)
            oSB.Append("Y" & ",")
        Else
            oSB.Append("N" & ",")
        End If

        If ModleLoseStatus = True AndAlso (DefectCount - DefectNoDieCount) > 0 Then '漏雷(CInspectResult)
            oSB.Append("Y" & ",") '檢測異常 (漏雷)
        Else
            oSB.Append("N" & ",")
        End If

        '' Augustin 230310
        If DefectNoDieCount > 0 Then '檢測異常 (No Die)
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