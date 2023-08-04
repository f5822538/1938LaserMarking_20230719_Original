Public Class CMyProductConfig : Inherits CConfigBase

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
        Public Const MarkCount As String = "蓋印數量"
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
    <Index2Display(23, SR.MarkCount), [ReadOnly](True)> Public ReadOnly Property MarkCount As Integer
        Get
            Return MarkList.Count
        End Get
    End Property

    <Browsable(False)> Public Property MarkListString As String = ""

    Public MarkList As New List(Of CMyMarkInfo)

    Public ReadOnly Property MarkIndex(nMarkX As Integer, nMarkY As Integer) As Integer
        Get
            If nMarkX < 0 OrElse nMarkY < 0 OrElse nMarkX >= DimensionX OrElse nMarkY >= DimensionY Then Return -1
            Dim nIndex As Integer = nMarkY * DimensionX + nMarkX
            If MarkList.Item(nIndex).MarkX <> nMarkX OrElse MarkList.Item(nIndex).MarkY <> nMarkY Then
                For nIndex = 0 To MarkList.Count - 1
                    If MarkList.Item(nIndex).MarkX = nMarkX AndAlso MarkList.Item(nIndex).MarkY = nMarkY Then Return nIndex
                Next
            End If
            Return nIndex
        End Get
    End Property

    Public Sub New(sPathName As String, sFileName As String, sExtendName As String)
        Call MyBase.New(CIniCreator.CreateSimpleIni(sPathName, sFileName, sExtendName))
    End Sub
End Class