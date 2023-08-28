Imports System.Drawing
Public Class CRecipeModelDiff : Inherits CConfigBase

    Private Class SR
        Public Const ROI As String = "ROI"
        Public Const Find As String = "尋找"
        Public Const Offset As String = "偏移"
        Public Const Threshold As String = "容差"
        Public Const DefectSize As String = "瑕疵大小"
        Public Const Model As String = "樣本"
        Public Const ExportSetting As String = "輸出設定"

        Public Const ModelTopLeft As String = "樣板左上角 (X,Y)"
        Public Const ModelSize As String = "樣板大小 (W,H)"
        Public Const ModelSizeWum As String = "樣板大小 (W) (um)"  '' Augustin 221206
        Public Const ModelSizeHum As String = "樣板大小 (H) (um)"  '' Augustin 221206
        Public Const SearchRange As String = "比對範圍 (X,Y,W,H)"

        Public Const MarkXCount As String = "標記數量 (X)"
        Public Const MarkYCount As String = "標記數量 (Y)"
        Public Const ModelScore1St As String = "第一次尋找分數"
        Public Const ModelScore2Nd As String = "第二次尋找分數"

        Public Const OffsetMinForUM As String = "偏移最小範圍 (um)"
        Public Const OffsetMin As String = "偏移最小範圍 (Pixel)"
        Public Const OffsetGrayMinForUM As String = "偏移最小範圍 Gray (um)"
        Public Const OffsetGrayMin As String = "偏移最小範圍 Gray (Pixel)"
        Public Const LoseMinForUM As String = "漏雷最小範圍 (um)"
        Public Const LoseMin As String = "漏雷最小範圍 (Pixel)"
        Public Const AngleMin As String = "最小旋轉量 (度)"

        Public Const DarkStandardDeviation As String = "[暗瑕疵] 標準差倍數 (1 ~ 10)"
        Public Const BrightStandardDeviation As String = "[亮瑕疵] 標準差倍數 (1 ~ 10)"
        Public Const DarkThreshold As String = "[暗瑕疵] 容差上限 (0 ~ 255)"
        Public Const BrightThreshold As String = "[亮瑕疵] 容差上限 (0 ~ 255)"
        Public Const DarkDefectSizeMinForUM As String = "[背] 最小瑕疵大小 (um)"
        Public Const DarkDefectSizeMin As String = "[背] 最小瑕疵大小 (Pixel)"
        Public Const DarkDefectSizeGrayMinForUM As String = "[背] 最小瑕疵大小 Gray (um)"
        Public Const DarkDefectSizeGrayMin As String = "[背] 最小瑕疵大小 Gray (Pixel)"
        Public Const BrightDefectSizeMinForUM As String = "[字] 最小瑕疵大小 (um)"
        Public Const BrightDefectSizeMin As String = "[字] 最小瑕疵大小 (Pixel)"
        Public Const BrightDefectSizeGrayMinForUM As String = "[字] 最小瑕疵大小 Gray (um)"
        Public Const BrightDefectSizeGrayMin As String = "[字] 最小瑕疵大小 Gray (Pixel)"
        Public Const MergeTolerance As String = "瑕疵合併之區域"

        Public Const IsGatherStandardDeviation As String = "是否收集標準差影像"
        Public Const InspectSummationSquareCount As String = "標準差收集個數"

        Public Const IsExportStripMapXML As String = "是否輸出Mark Out XML檔案"
        Public Const IsUpLoadInspectPicture As String = "是否要求IT抓取檢測小圖"
        Public Const IsUpLoadMarkShiftPicture As String = "是開啟蓋印偏移串修功能"

        Public Const MaxOffsetPercentForUpdateToFtp As String = "蓋印偏移占總數最大百分比"

        'Public Const ModelAngle As String = "尋找角度"
        'Public Const BaseDarkThreshold As String = "基本暗容差上限 (0 ~ 255)"
        'Public Const BaseBrightThreshold As String = "基本亮容差上限 (0 ~ 255)"
    End Class

    <Index3Category(10, SR.ROI), Index2Display(10, SR.ModelTopLeft)> Public Property ModelTopLeft As Point = Point.Empty
    <Index3Category(10, SR.ROI), Index2Display(11, SR.ModelSize)> Public Property ModelSize As Size = Size.Empty
    <Index3Category(10, SR.ROI), Index2Display(12, SR.ModelSizeWum)> Public Property ModelSizeWum As Integer  '' Augustin 221206
    '    Get
    '        Return mnModelSizeWum
    '    End Get
    '    Set(value As Integer)
    '        ModelSize = New Size(CInt(value / mdPixelSize), CInt(ModelSize.Height))
    '        If ModelSize.Width < 1 Then
    '            ModelSize = New Size(1, ModelSize.Height)
    '        End If
    '        mnModelSizeWum = CInt(ModelSize.Width * mdPixelSize)
    '        If mnModelSizeWum > value Then
    '            ModelSize = New Size(ModelSize.Width - 1, ModelSize.Height)
    '            If ModelSize.Width < 1 OrElse ModelSize.Height < 1 Then
    '                ModelSize = New Size(1, ModelSize.Height)
    '            End If
    '            mnModelSizeWum = CInt(ModelSize.Width * mdPixelSize)
    '        End If
    '    End Set
    'End Property

    <Index3Category(10, SR.ROI), Index2Display(13, SR.ModelSizeHum)> Public Property ModelSizeHum As Integer  '' Augustin 221206
    '    Get
    '        Return mnModelSizeHum
    '    End Get
    '    Set(value As Integer)
    '        ModelSize = New Size(CInt(ModelSize.Width), CInt(value / mdPixelSize))
    '        If ModelSize.Height < 1 Then
    '            ModelSize = New Size(ModelSize.Width, 1)
    '        End If
    '        mnModelSizeHum = CInt(ModelSize.Height * mdPixelSize)
    '        If mnModelSizeHum > value Then
    '            ModelSize = New Size(ModelSize.Width, ModelSize.Height - 1)
    '            If ModelSize.Height < 1 Then
    '                ModelSize = New Size(ModelSize.Width, 1)
    '            End If
    '            mnModelSizeHum = CInt(ModelSize.Height * mdPixelSize)
    '        End If
    '    End Set
    'End Property
    <Index3Category(10, SR.ROI), Index2Display(12, SR.SearchRange)> Public Property SearchRange As Rectangle = Rectangle.Empty

    <Index3Category(20, SR.Find), Index2Display(20, SR.MarkXCount), Range(1, 1000)> Public Property MarkXCount As Integer = 54
    <Index3Category(20, SR.Find), Index2Display(21, SR.MarkYCount), Range(1, 1000)> Public Property MarkYCount As Integer = 15
    <Index3Category(20, SR.Find), Index2Display(22, SR.ModelScore1St), Range(1, 100)> Public Property ModelScore1St As Double = 45
    <Index3Category(20, SR.Find), Index2Display(23, SR.ModelScore2Nd), Range(1, 100)> Public Property ModelScore2Nd As Double = 65

    <Index3Category(30, SR.Offset), Index2Display(30, SR.OffsetMinForUM), Range(1, Integer.MaxValue)> Public Property OffsetMinForUM As Integer
        Get
            Return mnOffsetMinForUM
        End Get
        Set(value As Integer)
            OffsetMin = CInt(value / mdPixelSize)
            If OffsetMin < 1 Then OffsetMin = 1
            mnOffsetMinForUM = CInt(OffsetMin * mdPixelSize)
            If mnOffsetMinForUM > value Then
                OffsetMin = OffsetMin - 1
                If OffsetMin < 1 Then OffsetMin = 1
                mnOffsetMinForUM = CInt(OffsetMin * mdPixelSize)
            End If
        End Set
    End Property

    <Index3Category(30, SR.Offset), Index2Display(31, SR.OffsetMin), Range(1, 100), [ReadOnly](True)> Public Property OffsetMin As Integer = 9

    <Index3Category(30, SR.Offset), Index2Display(32, SR.OffsetGrayMinForUM), Range(1, Integer.MaxValue)> Public Property OffsetGrayMinForUM As Integer
        Get
            Return mnOffsetGrayMinForUM
        End Get
        Set(value As Integer)
            OffsetGrayMin = CInt(value / mdPixelSize)
            If OffsetGrayMin < 1 Then OffsetGrayMin = 1
            mnOffsetGrayMinForUM = CInt(OffsetGrayMin * mdPixelSize)
            If mnOffsetGrayMinForUM > value Then
                OffsetGrayMin = OffsetGrayMin - 1
                If OffsetGrayMin < 1 Then OffsetGrayMin = 1
                mnOffsetGrayMinForUM = CInt(OffsetGrayMin * mdPixelSize)
            End If
        End Set
    End Property

    <Index3Category(30, SR.Offset), Index2Display(33, SR.OffsetGrayMin), Range(1, 100), [ReadOnly](True)> Public Property OffsetGrayMin As Integer = 1

    <Index3Category(30, SR.Offset), Index2Display(34, SR.LoseMinForUM), Range(1, Integer.MaxValue)> Public Property LoseMinForUM As Integer
        Get
            Return mnLoseMinForUM
        End Get
        Set(value As Integer)
            LoseMin = CInt(value / mdPixelSize)
            If LoseMin < 1 Then LoseMin = 1
            mnLoseMinForUM = CInt(LoseMin * mdPixelSize)
            If mnLoseMinForUM > value Then
                LoseMin = LoseMin - 1
                If LoseMin < 1 Then LoseMin = 1
                mnLoseMinForUM = CInt(LoseMin * mdPixelSize)
            End If
        End Set
    End Property

    <Index3Category(30, SR.Offset), Index2Display(35, SR.LoseMin), Range(1, 100), [ReadOnly](True)> Public Property LoseMin As Integer = 20
    <Index3Category(30, SR.Offset), Index2Display(36, SR.AngleMin), Range(1, 10)> Public Property AngleMin As Integer = 2

    <Index3Category(40, SR.Threshold), Index2Display(40, SR.DarkStandardDeviation), Range(1, 10)> Public Property DarkStandardDeviation As Double = 2
    <Index3Category(40, SR.Threshold), Index2Display(41, SR.BrightStandardDeviation), Range(1, 10)> Public Property BrightStandardDeviation As Double = 3
    <Index3Category(40, SR.Threshold), Index2Display(42, SR.DarkThreshold), Range(1, 255)> Public Property DarkThreshold As Integer = 15
    <Index3Category(40, SR.Threshold), Index2Display(43, SR.BrightThreshold), Range(1, 255)> Public Property BrightThreshold As Integer = 25

    <Index3Category(50, SR.DefectSize), Index2Display(50, SR.DarkDefectSizeMinForUM), Range(1, Integer.MaxValue)> Public Property DarkDefectSizeMinForUM As Integer
        Get
            Return mnDarkDefectSizeMinForUM
        End Get
        Set(value As Integer)
            DarkDefectSizeMin = CInt(value / mdPixelSize)
            If mbIsDefectSizeAnd = True Then DarkDefectSizeMin = DarkDefectSizeMin \ CInt(mdPixelSize)
            If DarkDefectSizeMin < 1 Then DarkDefectSizeMin = 1
            mnDarkDefectSizeMinForUM = CInt(DarkDefectSizeMin * mdPixelSize)
            If mbIsDefectSizeAnd = True Then mnDarkDefectSizeMinForUM = CInt(mnDarkDefectSizeMinForUM * mdPixelSize)
            If mnDarkDefectSizeMinForUM > value Then
                DarkDefectSizeMin = DarkDefectSizeMin - 1
                If DarkDefectSizeMin < 1 Then DarkDefectSizeMin = 1
                mnDarkDefectSizeMinForUM = CInt(DarkDefectSizeMin * mdPixelSize)
                If mbIsDefectSizeAnd = True Then mnDarkDefectSizeMinForUM = CInt(mnDarkDefectSizeMinForUM * mdPixelSize)
            End If
        End Set
    End Property

    <Index3Category(50, SR.DefectSize), Index2Display(51, SR.DarkDefectSizeMin), Range(1, Integer.MaxValue), [ReadOnly](True)> Public Property DarkDefectSizeMin As Integer = 2

    <Index3Category(50, SR.DefectSize), Index2Display(52, SR.DarkDefectSizeGrayMinForUM), Range(1, Integer.MaxValue)> Public Property DarkDefectSizeGrayMinForUM As Integer
        Get
            Return mnDarkDefectSizeGrayMinForUM
        End Get
        Set(value As Integer)
            DarkDefectSizeGrayMin = CInt(value / mdPixelSize)
            If mbIsDefectSizeAnd = True Then DarkDefectSizeGrayMin = DarkDefectSizeGrayMin \ CInt(mdPixelSize)
            If DarkDefectSizeGrayMin < 1 Then DarkDefectSizeGrayMin = 1
            mnDarkDefectSizeGrayMinForUM = CInt(DarkDefectSizeGrayMin * mdPixelSize)
            If mbIsDefectSizeAnd = True Then mnDarkDefectSizeGrayMinForUM = CInt(mnDarkDefectSizeGrayMinForUM * mdPixelSize)
            If mnDarkDefectSizeGrayMinForUM > value Then
                DarkDefectSizeGrayMin = DarkDefectSizeGrayMin - 1
                If DarkDefectSizeGrayMin < 1 Then DarkDefectSizeGrayMin = 1
                mnDarkDefectSizeGrayMinForUM = CInt(DarkDefectSizeGrayMin * mdPixelSize)
                If mbIsDefectSizeAnd = True Then mnDarkDefectSizeGrayMinForUM = CInt(mnDarkDefectSizeGrayMinForUM * mdPixelSize)
            End If
        End Set
    End Property

    <Index3Category(50, SR.DefectSize), Index2Display(53, SR.DarkDefectSizeGrayMin), Range(1, Integer.MaxValue), [ReadOnly](True)> Public Property DarkDefectSizeGrayMin As Integer = 1

    <Index3Category(50, SR.DefectSize), Index2Display(54, SR.BrightDefectSizeMinForUM), Range(1, Integer.MaxValue)> Public Property BrightDefectSizeMinForUM As Integer
        Get
            Return mnBrightDefectSizeMinForUM
        End Get
        Set(value As Integer)
            BrightDefectSizeMin = CInt(value / mdPixelSize)
            If mbIsDefectSizeAnd = True Then BrightDefectSizeMin = BrightDefectSizeMin \ CInt(mdPixelSize)
            If BrightDefectSizeMin < 1 Then BrightDefectSizeMin = 1
            mnBrightDefectSizeMinForUM = CInt(BrightDefectSizeMin * mdPixelSize)
            If mbIsDefectSizeAnd = True Then mnBrightDefectSizeMinForUM = CInt(mnBrightDefectSizeMinForUM * mdPixelSize)
            If mnBrightDefectSizeMinForUM > value Then
                BrightDefectSizeMin = BrightDefectSizeMin - 1
                If BrightDefectSizeMin < 1 Then BrightDefectSizeMin = 1
                mnBrightDefectSizeMinForUM = CInt(BrightDefectSizeMin * mdPixelSize)
                If mbIsDefectSizeAnd = True Then mnBrightDefectSizeMinForUM = CInt(mnBrightDefectSizeMinForUM * mdPixelSize)
            End If
        End Set
    End Property

    <Index3Category(50, SR.DefectSize), Index2Display(55, SR.BrightDefectSizeMin), Range(1, Integer.MaxValue), [ReadOnly](True)> Public Property BrightDefectSizeMin As Integer = 6

    <Index3Category(50, SR.DefectSize), Index2Display(56, SR.BrightDefectSizeGrayMinForUM), Range(1, Integer.MaxValue)> Public Property BrightDefectSizeGrayMinForUM As Integer
        Get
            Return mnBrightDefectSizeGrayMinForUM
        End Get
        Set(value As Integer)
            BrightDefectSizeGrayMin = CInt(value / mdPixelSize)
            If mbIsDefectSizeAnd = True Then BrightDefectSizeGrayMin = BrightDefectSizeGrayMin \ CInt(mdPixelSize)
            If BrightDefectSizeGrayMin < 1 Then BrightDefectSizeGrayMin = 1
            mnBrightDefectSizeGrayMinForUM = CInt(BrightDefectSizeGrayMin * mdPixelSize)
            If mbIsDefectSizeAnd = True Then mnBrightDefectSizeGrayMinForUM = CInt(mnBrightDefectSizeGrayMinForUM * mdPixelSize)
            If mnBrightDefectSizeGrayMinForUM > value Then
                BrightDefectSizeGrayMin = BrightDefectSizeGrayMin - 1
                If BrightDefectSizeGrayMin < 1 Then BrightDefectSizeGrayMin = 1
                mnBrightDefectSizeGrayMinForUM = CInt(BrightDefectSizeGrayMin * mdPixelSize)
                If mbIsDefectSizeAnd = True Then mnBrightDefectSizeGrayMinForUM = CInt(mnBrightDefectSizeGrayMinForUM * mdPixelSize)
            End If
        End Set
    End Property

    <Index3Category(50, SR.DefectSize), Index2Display(57, SR.BrightDefectSizeGrayMin), Range(1, Integer.MaxValue), [ReadOnly](True)> Public Property BrightDefectSizeGrayMin As Integer = 1
    <Index3Category(50, SR.DefectSize), Index2Display(58, SR.MergeTolerance), Range(1, 10)> Public Property MergeTolerance As Integer = 0

    <Index3Category(60, SR.Model), Index2Display(60, SR.IsGatherStandardDeviation), [ReadOnly](True)> Public Property IsGatherStandardDeviation As Boolean = True
    <Index3Category(60, SR.Model), Index2Display(61, SR.InspectSummationSquareCount), [ReadOnly](True)> Public Property SummationSquareCount As Integer = 0

    <Browsable(False)> Public Property RecipeMarkList As CRecipeMarkList

    Private mbIsDefectSizeAnd As Boolean = True
    Private mdPixelSize As Double = 33.42
    Private mnOffsetMinForUM As Integer = 300
    Private mnModelSizeWum As Integer = 300   '' Augustin 221206
    Private mnModelSizeHum As Integer = 300  '' Augustin 221206 
    Private mnOffsetGrayMinForUM As Integer = 33
    Private mnLoseMinForUM As Integer = 700

    Private mnDarkDefectSizeMinForUM As Integer = 70
    Private mnDarkDefectSizeGrayMinForUM As Integer = 33
    Private mnBrightDefectSizeMinForUM As Integer = 200
    Private mnBrightDefectSizeGrayMinForUM As Integer = 33

    Public ModelAngle As Double = 3
    Public BaseDarkThreshold As Integer = 3
    Public BaseBrightThreshold As Integer = 3

    Private msParentName As String
    Public TemplateID1St As MIL_ID
    Public SummationID As MIL_ID
    Public SummationSquareID As MIL_ID

    Public ModelRectangle As Rectangle = Rectangle.Empty

    Public PatternMatching1St As CMyPatternMatching = New CMyPatternMatching(PatternMatchingType.PatternMatching1St)
    Public PatternMatching2Nd As CMyPatternMatching = New CMyPatternMatching(PatternMatchingType.PatternMatching2Nd)

    Public ModelCenterDrawListStart As New List(Of Point)
    Public ModelCenterDrawListEnd As New List(Of Point)

    <Browsable(False)> Public Property MeanGray As Integer = 0

    Public ReadOnly Property MarkIndex(nMarkX As Integer, nMarkY As Integer) As Integer
        Get
            If nMarkX < 0 OrElse nMarkY < 0 OrElse nMarkX >= MarkXCount OrElse nMarkY >= MarkYCount Then Return -1
            Dim nIndex As Integer = nMarkY * MarkXCount + nMarkX
            If RecipeMarkList.RecipeMarkList.Count <= nIndex OrElse RecipeMarkList.RecipeMarkList.Item(nIndex).MarkX <> nMarkX OrElse RecipeMarkList.RecipeMarkList.Item(nIndex).MarkY <> nMarkY Then
                For nIndex = 0 To RecipeMarkList.RecipeMarkList.Count - 1
                    If RecipeMarkList.RecipeMarkList.Item(nIndex).MarkX = nMarkX AndAlso RecipeMarkList.RecipeMarkList.Item(nIndex).MarkY = nMarkY Then Return nIndex
                Next
            Else
                Return nIndex
            End If
            Return -1
        End Get
    End Property

    <Index3Category(70, SR.ExportSetting), Index2Display(71, SR.IsExportStripMapXML)> Public Property IsExportStripMapXML As Boolean = False
    <Index3Category(70, SR.ExportSetting), Index2Display(72, SR.IsUpLoadInspectPicture)> Public Property IsUpLoadInspectPicture As UpLoadInspectImage = UpLoadInspectImage.OPEN
    <Index3Category(70, SR.ExportSetting), Index2Display(73, SR.IsUpLoadMarkShiftPicture)> Public Property IsUpLoadMarkShiftPicture As UpLoadMarkShiftImage = UpLoadMarkShiftImage.OPEN
    'Public Property UpLoadMarkShiftPictureToIT As String = "OFF"
    Public UpLoadMarkShiftPictureToIT As String = "OFF"

    <Index3Category(70, SR.ExportSetting), Index2Display(74, SR.MaxOffsetPercentForUpdateToFtp), Range(0, 100)> Public Property MaxOffsetPercentForUpdateToFtp As Integer = 20

    Public Sub New(sParentName As String, oSetting As II_Setting, bIsDefectSizeAnd As Boolean, dPixelSize As Double)
        MyBase.New(oSetting)
        msParentName = sParentName
        mbIsDefectSizeAnd = bIsDefectSizeAnd
        mdPixelSize = dPixelSize

        RecipeMarkList = New CRecipeMarkList(moSetting)
    End Sub

    Public Function CreateRecipeMarkConfig() As CRecipeMark
        Return New CRecipeMark(msParentName & "_Mark", moSetting)
    End Function
End Class