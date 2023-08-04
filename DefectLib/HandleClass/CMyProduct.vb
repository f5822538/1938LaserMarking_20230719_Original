Public Class CMyProduct

    Private Class SR
        Public Const StripMap As String = "交握內容"
        Public Const ErrorCode As String = "錯誤代碼"
        Public Const ErrorText As String = "錯誤內容"
        Public Const LotID As String = "Lot ID"
        Public Const StepName As String = "Step Name"
        Public Const Prodline As String = "Prodline"
        Public Const Floor As String = "Floor"
        Public Const EQPID As String = "EQP ID"
        Public Const EQPtype As String = "EQP Type"
        Public Const RecipeID As String = "Recipe ID"
        Public Const SubstrateID As String = "Substrate ID"
        Public Const DimensionX As String = "Dimension X"
        Public Const DimensionY As String = "Dimension Y"
    End Class

    <Index2Display(10, SR.StripMap)> Public Property StripMap As String = ""
    <Index2Display(11, SR.ErrorCode)> Public Property ErrorCode As String = ""
    <Index2Display(12, SR.ErrorText)> Public Property ErrorText As String = ""
    <Index2Display(13, SR.LotID)> Public Property LotID As String = ""
    <Index2Display(14, SR.StepName)> Public Property StepName As String = ""
    <Index2Display(15, SR.Prodline)> Public Property Prodline As String = ""
    <Index2Display(16, SR.Floor)> Public Property Floor As String = ""
    <Index2Display(17, SR.EQPID)> Public Property EQPID As String = ""
    <Index2Display(18, SR.EQPtype)> Public Property EQPtype As String = ""
    <Index2Display(19, SR.RecipeID)> Public Property RecipeID As String = ""
    <Index2Display(20, SR.SubstrateID)> Public Property SubstrateID As String = ""
    <Index2Display(21, SR.DimensionX)> Public Property DimensionX As Integer = -1
    <Index2Display(22, SR.DimensionY)> Public Property DimensionY As Integer = -1

    Public MarkList As New List(Of CMyMarkInfo)
    Public MapDownLoadInfo As String = ""

    Public ReadOnly Property MarkIndex(nMarkX As Integer, nMarkY As Integer) As Integer
        Get
            If nMarkX < 0 OrElse nMarkY < 0 OrElse nMarkX >= DimensionX OrElse nMarkY >= DimensionY Then Return -1
            Dim nIndex As Integer = nMarkY * DimensionX + nMarkX
            If MarkList.Count <= nIndex OrElse MarkList.Item(nIndex).MarkX <> nMarkX OrElse MarkList.Item(nIndex).MarkY <> nMarkY Then
                For nIndex = 0 To MarkList.Count - 1
                    If MarkList.Item(nIndex).MarkX = nMarkX AndAlso MarkList.Item(nIndex).MarkY = nMarkY Then Return nIndex
                Next
            Else
                Return nIndex
            End If
            Return -1
        End Get
    End Property

    Public Sub GetLotID(ByRef sLotID As String)
        sLotID = LotID
    End Sub

    Public Sub GetEQPID(ByRef oProductConfig As CMyProductConfig)
        oProductConfig.EQPID = EQPID
    End Sub

    Public Sub CopyTo(ByRef oProductConfig As CMyProductConfig)
        Dim oMarkInfoList As New List(Of CMyMarkInfo)
        MarkList.ForEach(Sub(o As CMyMarkInfo)
                             Dim oMarkInfo As New CMyMarkInfo
                             'oMarkInfo.IsProcess = o.IsProcess
                             oMarkInfo.AfterInspectBinCode = o.AfterInspectBinCode
                             oMarkInfo.MarkX = o.MarkX
                             oMarkInfo.MarkY = o.MarkY
                             oMarkInfo.Result = o.Result
                             oMarkInfo.IsGray = o.IsGray
                             oMarkInfoList.Add(oMarkInfo)
                         End Sub)

        With oProductConfig
            .StripMap = StripMap
            .ErrorCode = ErrorCode
            .ErrorText = ErrorText
            .LotID = LotID
            .StepName = StepName
            .Prodline = Prodline
            .Floor = Floor
            .EQPID = EQPID
            .EQPtype = EQPtype
            .RecipeID = RecipeID
            .SubstrateID = SubstrateID
            .DimensionX = DimensionX
            .DimensionY = DimensionY
            .MarkList.Clear()
            .MarkList = oMarkInfoList
        End With
    End Sub

    Public Sub ClearMark()
        StripMap = ""
        ErrorCode = ""
        ErrorText = ""
        StepName = ""
        Prodline = ""
        Floor = ""
        EQPID = ""
        EQPtype = ""
        SubstrateID = ""
        DimensionX = 1
        DimensionY = 1
        MarkList.Clear()
    End Sub
End Class