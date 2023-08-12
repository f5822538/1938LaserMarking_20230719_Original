<TypeConverter(GetType(CEnumConvecter))>
Public Enum ResultType
    <Description("NA")> NA = 0
    <Description("OK")> OK = 1
    <Description("表面瑕疵 (背)")> NGDark = 2
    <Description("表面瑕疵 (字)")> NGBright = 3
    <Description("位移")> Offset = 4
    '' Augustin 220503 Modify
    <Description("蓋印漏雷/蓋印轉置")> Lose = 5
    '<Description("忽略")> Pass = 6
    <Description("蓋印不清")> Indistinct = 6
    ''0928
    <Description("NoDie")> NoDie = 7
End Enum

Public Class CMyMarkInfo
    Public Class SR
        'Public Const IsProcess As String = "是否處理"

        Public Const AfterInspectBinCode As String = "檢測後 Bin Code"
        Public Const MarkX As String = "X"
        Public Const MarkY As String = "Y"
        Public Const Result As String = "檢測結果"
        Public Const IsGray As String = "是否為人員判斷"

        Public Const OriginalBinCode As String = "原始 Bin Code"
        Public Const OriginalType As String = "原始狀態"
    End Class

    '<Index2Display(10, SR.IsProcess)> Public Property IsProcess As Boolean = False
    <Index2Display(11, SR.AfterInspectBinCode), [ReadOnly](True)> Public Property AfterInspectBinCode As String = ""
    <Index2Display(12, SR.MarkX), [ReadOnly](True)> Public Property MarkX As Integer = -1
    <Index2Display(13, SR.MarkY), [ReadOnly](True)> Public Property MarkY As Integer = -1
    <Index2Display(14, SR.Result)> Public Property Result As ResultType = ResultType.NA
    <Index2Display(15, SR.IsGray)> Public Property IsGray As Boolean = False
    <Index2Display(20, SR.OriginalBinCode), [ReadOnly](True)> Public Property OriginalBinCode As String = ""
    <Index2Display(21, SR.OriginalType)> Public Property OriginalType As ResultType = ResultType.NA

    Public Sub Clear()
        'IsProcess = False
        OriginalBinCode = ""
        AfterInspectBinCode = ""
        MarkX = -1
        MarkY = -1
        Result = ResultType.NA
        IsGray = False
    End Sub

    Public Function Success() As Boolean
        'Return (IsProcess = True AndAlso Result = ResultType.OK) OrElse IsProcess = False
        Return Result = ResultType.OK
    End Function
End Class