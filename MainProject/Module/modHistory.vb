Module ModHistory

    Public Sub LoadHistory(ByRef oLog As II_LogTraceExtend)

        Dim sContent As String =
<command>
2020-02-25 Initial Release 2.1.0.1
    2020-02-25 0.01 依瑕疵種類建立開關，位移、字瑕疵、Compound 瑕疵。
2021-01-04 Initial Release 2.1.0.2
    2021-01-04 0.01 參數設定，修改建置 Mark 位置之問題。
    2021-01-04 0.02 取像時間延後至 200 ms。
    2021-01-04 0.03 檢測結束後延遲 2 s，預防蟲付檢驗。
2021-01-20 Initial Release 2.1.0.3
    2021-01-20 0.01 AI 交握新增成員，以及儲存 XML 檔。
2021-10-08 Initial Release 2.1.0.4
    2021-10-08 0.01 AOI相機可多讀取一長型條碼區域
2021-11-15 Initial Release 2.1.0.5
    2021-11-15 0.01 新增AI交握成員
    2021-11-15 0.02 下載每條Strip NoDie資訊
    2021-11-15 0.03 AI存圖圖檔名路徑
    2021-11-15 0.04 NoDie、漏雷比對
    2021-11-15 0.05 StripUpLoad上傳時多補上NoDie資訊
    2021-11-15 0.06 蓋印偏移串修開關
    2021-11-15 0.07 檢測小圖抓取開關
2021-11-23 Initial Release 2.1.0.6
    2021-11-23 0.01 AI存小圖 新增漏雷(角度)分類資料夾

</command>.Value.Replace(vbLf, vbCrLf)

        Call oLog.LogInformation(sContent)
    End Sub

    Public Function ByteToHexString(ByRef abytIn() As Byte) As String
        Dim strTemp As New StringBuilder(abytIn.Length * 3 - 1)
        For nCount As Integer = 1 To abytIn.Length
            Dim nByte As Byte = abytIn(nCount - 1)
            If nCount = 1 Then
                strTemp.Append(IIf(nByte > 15, Conversion.Hex(nByte), "0" & Conversion.Hex(nByte)))
            Else
                strTemp.Append(IIf(nByte > 15, " " & Conversion.Hex(nByte), " 0" & Conversion.Hex(nByte)))
            End If
        Next
        Return strTemp.ToString()
    End Function

End Module