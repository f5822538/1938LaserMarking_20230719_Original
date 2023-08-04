Public Class CInspectSum

    Public InspectResult As CInspectResult
    Public ReceiveTime As Date
    Public DefectList As CMyDefectList
    Public ProductConfig As CMyProductConfig
    Public DefectListDraw As New List(Of CMyDefect)
    Public ModelCenterListStart1St As New List(Of Point)
    Public ModelCenterListEnd1St As New List(Of Point)
    Public ModelCenterListStart2Nd As New List(Of Point)
    Public ModelCenterListEnd2Nd As New List(Of Point)

    Public Sub New(oInspectResult As CInspectResult, oReceiveTime As Date, oDefectList As CMyDefectList, oProductConfig As CMyProductConfig)
        InspectResult = oInspectResult
        ReceiveTime = oReceiveTime
        DefectList = oDefectList
        ProductConfig = oProductConfig
    End Sub
End Class