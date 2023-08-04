Imports System.Xml.Serialization

<XmlType("DefectFile")> Public Class CMyDefectList
    <XmlArray("DefectList")> Public DefectList As New List(Of CMyDefect)
    <XmlAttribute("CodeID")> Public Property CodeID As String = ""
    Public ModelList As New List(Of Rectangle)
    Public OKList As New List(Of CMyDefect)
End Class