''' <summary>
''' 光學瑕疵檢測結果(用於出報表)
''' </summary>
''' <remarks></remarks>
Public Class CInspectResult
    Public Sequnce As Integer = 0
    Public RecipeID As String = ""
    Public CodeID As String = ""
    Public AlignStatus As Boolean = False '對位異常
    Public FindStatus As Boolean = False '瑕疵-N(預設值/初始值)
    Public CycleInspectStatus As Boolean = False
    Public ModleInspectStatus As Boolean = False '樣板異常/檢測異常 (樣板)-異常:True,正常:False
    Public ModleOffsetStatus As Boolean = False '檢測異常 (偏移)
    Public ModleLoseStatus As Boolean = False '漏雷(CInspectResult)
    Public InspectPath As String = ""
    Public AIOKPath As String = ""
    Public AINGPath As String = ""
    Public AINODIEPath As String = ""
    Public AIOffsetPath As String = ""
    Public AILoseAndRotatePath As String = ""
    'Public AIOffsetToFtpPath As String = ""

    Public AIXMLFileName As String = ""

    Public Name As String = "" '檔案日期{0:yyyy-MM-dd_HH_mm_ss_fff}
    Public TactTime As Double = 0.0 '一個單元開始生產到下一個單元開始生產之間的平均時間間隔
    Public DefectCount As Integer = 0 '瑕疵數量(包含-漏雷數量)
    Public DefectNoDieCount As Integer = 0 'No Die數量(Defect)
    Public NotDefectNoDieCount As Integer = 0 'No Die數量(NotDefect)
End Class

Partial Class CInspectResult : Implements II_InspectReport

    Public Function GetDMFileName() As String Implements iTVisionService.II_InspectReport.GetDMFileName
        Return String.Format("{0}\{1}.JPG", InspectPath, Name)
    End Function

    Public Function GetHtmlFileName() As String Implements iTVisionService.II_InspectReport.GetHtmlFileName
        Return String.Format("{0}\{1}.html", InspectPath, Name)
    End Function

    Public Function GetPath() As String Implements iTVisionService.II_InspectReport.GetPath
        Return InspectPath
    End Function

    Public Function GetShortDMFileName() As String Implements iTVisionService.II_InspectReport.GetShortDMFileName
        Return String.Format("{0}.JPG", Name)
    End Function

    Public Function GetXmlFileName() As String Implements iTVisionService.II_InspectReport.GetXmlFileName
        Return String.Format("{0}\{1}.xml", InspectPath, Name)
    End Function

    Public Function GetCode1FileName() As String
        Return String.Format("{0}\{1}_Code1.JPG", InspectPath, Name)
    End Function

    Public Function GetCode2FileName() As String
        Return String.Format("{0}\{1}_Code2.JPG", InspectPath, Name)
    End Function
End Class