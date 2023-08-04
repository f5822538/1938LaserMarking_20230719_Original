Public Class CMiscConfig : Inherits CConfigBase

    Public Class SR
        Public Const IsSaveSourceImage As String = "是否儲存原始影像"
        Public Const IsSaveInspectImage As String = "是否儲存檢測影像"
        'Public Const IsUseModelFinder As String = "是否使用 Model Finder"
        Public Const IsAutoRemoveProduct As String = "是否將完成之產品資訊自動刪除"
        Public Const CaptureDelayTime As String = "相機取像延遲之時間 (ms)"
        Public Const MaxDefectCountForUpdateMap As String = "上報 Map 最大瑕疵數量 (%)"
        Public Const SaveReportDay As String = "檢測結果保留天數"
        Public Const DefectSizeType As String = "瑕疵面積計算方式"
        Public Const DefectMaxCount As String = "瑕疵最大數量"
        Public Const YieldThreshold As String = "良率門檻值 (%)"
        Public Const HomeSensorDelayTime As String = "原點檢知防撞延遲時間 (ms)"

        Public Const XsltFile As String = "報告轉換檔 (.XSLT)"
        Public Const AIPath As String = "AI 存檔路徑"
        Public Const IsSaveAIOKImage As String = "是否儲存 AI OK 影像"
        Public Const IsSaveAINGImage As String = "是否儲存 AI NG 影像"

        Public Const ReadProductXmlPath As String = "讀取產品原始Map Xml路徑"
        Public Const ExportProductXmlPath As String = "輸出產品結果Map Xml路徑"

        'Public Const UpLoadOffsetToFtpPath As String = "上傳偏移瑕疵照片Ftp路徑"

    End Class

    <Index2Display(10, SR.IsSaveSourceImage)> Public Property IsSaveSourceImage As Boolean = False
    <Index2Display(11, SR.IsSaveInspectImage)> Public Property IsSaveInspectImage As Boolean = False
    Public IsUseModelFinder As Boolean = True
    <Index2Display(12, SR.IsAutoRemoveProduct)> Public Property IsAutoRemoveProduct As Boolean = True
    <Index2Display(13, SR.CaptureDelayTime), Range(0, Integer.MaxValue)> Public Property CaptureDelayTime As Integer = 300
    <Index2Display(14, SR.MaxDefectCountForUpdateMap), Range(0, Integer.MaxValue)> Public Property MaxDefectCountForUpdateMap As Integer = 30
    <Index2Display(15, SR.SaveReportDay), Range(0, Integer.MaxValue)> Public Property SaveReportDay As UInteger = 30
    <Index2Display(16, SR.DefectSizeType)> Public Property DefectSizeType As DefectSizeType = DefectSizeType.DefectAnd
    <Index2Display(17, SR.DefectMaxCount), Range(1, Integer.MaxValue)> Public Property DefectMaxCount As Integer = 1000
    <Index2Display(18, SR.YieldThreshold), Range(1, Integer.MaxValue)> Public Property YieldThreshold As Integer = 95
    <Index2Display(19, SR.HomeSensorDelayTime), Range(0, Integer.MaxValue)> Public Property HomeSensorDelayTime As Integer = 1000

    <Index2Display(20, SR.XsltFile), Editor(GetType(CUINameTypeEditor), GetType(UITypeEditor)), OpenFile(), FileDialogFilter("Transfer File (*.xslt)|*.xslt")> Public Property XsltFile As String = ""
    <Index2Display(21, SR.AIPath), Editor(GetType(CUIPathNameTypeEditor), GetType(UITypeEditor))> Public Property AIPath As String = "D:\img"

    <Index2Display(22, SR.IsSaveAIOKImage)> Public Property IsSaveAIOKImage As Boolean = False
    <Index2Display(23, SR.IsSaveAINGImage)> Public Property IsSaveAINGImage As Boolean = False

    ''Augustin 220407 IT Handshake Test
    <Index2Display(30, SR.ReadProductXmlPath), Editor(GetType(CUIPathNameTypeEditor), GetType(UITypeEditor))> Public Property ReadProductXmlPath As String = "D:\img\ImportXML"
    <Index2Display(31, SR.ExportProductXmlPath), Editor(GetType(CUIPathNameTypeEditor), GetType(UITypeEditor))> Public Property ExportProductXmlPath As String = "D:\img\ExportXML"
    '<Index2Display(30, SR.ReadProductXmlPath), Editor(GetType(CUIPathNameTypeEditor), GetType(UITypeEditor))> Public Property ReadProductXmlPath As String = "D:\Product\Import\XML"
    '<Index2Display(31, SR.ExportProductXmlPath), Editor(GetType(CUIPathNameTypeEditor), GetType(UITypeEditor))> Public Property ExportProductXmlPath As String = "D:\Product\Export\XML"

    '<Index2Display(32, SR.UpLoadOffsetToFtpPath), Editor(GetType(CUIPathNameTypeEditor), GetType(UITypeEditor))> Public Property UpLoadOffsetToFtpPath As String = "D:\ftp_file"

    Public Sub New(oSetting As II_Setting)
        MyBase.New(oSetting)
    End Sub
End Class