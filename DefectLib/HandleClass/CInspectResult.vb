''' <summary>CInspectResult</summary>
''' <remarks></remarks>
Public Class CInspectResult
    Public Sequnce As Integer = 0
    Public RecipeID As String = ""
    Public CodeID As String = ""
    Public AlignStatus As Boolean = False
    Public FindStatus As Boolean = False
    Public CycleInspectStatus As Boolean = False
    Public ModleInspectStatus As Boolean = False
    Public ModleOffsetStatus As Boolean = False
    Public ModleLoseStatus As Boolean = False
    Public InspectPath As String = ""
    Public AIOKPath As String = ""
    Public AINGPath As String = ""
    Public AINODIEPath As String = ""
    Public AIOffsetPath As String = ""
    Public AILoseAndRotatePath As String = ""
    'Public AIOffsetToFtpPath As String = ""

    Public AIXMLFileName As String = ""

    Public Name As String = ""
    Public TactTime As Double = 0.0
    Public DefectCount As Integer = 0
    Public DefectNoDieCount As Integer = 0
    Public NotDefectNoDieCount As Integer = 0
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