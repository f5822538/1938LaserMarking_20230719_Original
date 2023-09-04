''' <summary>
''' ���Ƿ岫�˴����G(�Ω�X����)
''' </summary>
''' <remarks></remarks>
Public Class CInspectResult
    Public Sequnce As Integer = 0
    Public RecipeID As String = ""
    Public CodeID As String = ""
    Public AlignStatus As Boolean = False '��첧�`
    Public FindStatus As Boolean = False '�岫-N(�w�]��/��l��)
    Public CycleInspectStatus As Boolean = False
    Public ModleInspectStatus As Boolean = False '�˪O���`/�˴����` (�˪O)-���`:True,���`:False
    Public ModleOffsetStatus As Boolean = False '�˴����` (����)
    Public ModleLoseStatus As Boolean = False '�|�p(CInspectResult)
    Public InspectPath As String = ""
    Public AIOKPath As String = ""
    Public AINGPath As String = ""
    Public AINODIEPath As String = ""
    Public AIOffsetPath As String = ""
    Public AILoseAndRotatePath As String = ""
    'Public AIOffsetToFtpPath As String = ""

    Public AIXMLFileName As String = ""

    Public Name As String = "" '�ɮפ��{0:yyyy-MM-dd_HH_mm_ss_fff}
    Public TactTime As Double = 0.0 '�@�ӳ椸�}�l�Ͳ���U�@�ӳ椸�}�l�Ͳ������������ɶ����j
    Public DefectCount As Integer = 0 '�岫�ƶq(�]�t-�|�p�ƶq)
    Public DefectNoDieCount As Integer = 0 'No Die�ƶq(Defect)
    Public NotDefectNoDieCount As Integer = 0 'No Die�ƶq(NotDefect)
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