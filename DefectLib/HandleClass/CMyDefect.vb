<XmlType("Defect")> Public Class CMyDefect
    Public Property DefectBoundary As New CMyDefectRectangle
    Public Property DefectCenter As New CITVPointWapper()
    Public Property DefectPosition As New CITVPointWapper()
    Public Property DefectCoordinate As New CITVPointWapper() '' Augustin 220726 Add for Wafer Map
    Public Property DefectIndex As New CITVPointWapper()
    Public Property DefectImage As New CMyDefectImage() '瑕疵點位小圖(可用於出報表)

    <XmlAttribute()> Public Property DefectType As Comp_InsperrorType
    <XmlAttribute()> Public Property BodyArea As Integer = 0
    <XmlAttribute()> Public Property DefectArea As Integer = 0

    Public Property DefectSize As New CITVPointWapper()

    <XmlAttribute()> Public Property MeanGray As Double = 0.0
    <XmlAttribute()> Public Property MaxGray As Integer = 0
    <XmlAttribute()> Public Property MinGray As Integer = 0
    <XmlAttribute()> Public Property BaseGray As Double = 0.0

    <XmlAttribute()> Public Property DefectSizeJudge As COMP_DEFECTSIZE
    <XmlAttribute()> Public Property ResultType As ResultType
    <XmlAttribute()> Public Property DefectName As String = "" '瑕疵名稱(可用於出報表)
    <XmlAttribute()> Public Property DefectGraySizeJudge As COMP_DEFECTGRAYLEVEOVERSIZE
    <XmlAttribute()> Public Property DefectShapeJudge As COMP_DEFECTSHAPE
    <XmlAttribute()> Public Property InspectType As InspectType
    <XmlAttribute()> Public Property ZoneName As String = ""
    <XmlAttribute()> Public Property DefectZoneSection As Integer = 0

    <XmlAttribute()> Public Property DefectFileName As String = ""
    <XmlAttribute()> Public Property InpsectMethod As Comp_Inspect_Method = Comp_Inspect_Method.Comp_Horizontal
    <XmlAttribute()> Public Property Coef As Double = 0.0

    <XmlAttribute()> Public Property SelfDefine As String = ""

    <XmlAttribute()> Public IsNeedRemove As Boolean = False
End Class