Public Class CRecipeMark : Inherits CConfigBase

    Private Class SR
        Public Const MarkX As String = "座標 X"
        Public Const MarkY As String = "座標 Y"

        Public Const PositionCenterX As String = "中心位置 X"
        Public Const PositionCenterY As String = "中心位置 Y"
        Public Const PositionX As String = "位置 X"
        Public Const PositionY As String = "位置 Y"
    End Class

    <Index2Display(10, SR.MarkX)> Public Property MarkX As Integer = 0
    <Index2Display(11, SR.MarkY)> Public Property MarkY As Integer = 0

    <Index2Display(20, SR.PositionCenterX)> Public Property PositionCenterX As Double = 0
    <Index2Display(21, SR.PositionCenterY)> Public Property PositionCenterY As Double = 0
    <Index2Display(22, SR.PositionX)> Public Property PositionX As Double = 0
    <Index2Display(23, SR.PositionY)> Public Property PositionY As Double = 0

    Public IndexX As Integer = 0
    Public IndexY As Integer = 0

    Private msParentName As String
    Public IsUse As Boolean = True

    Public ReadOnly Property IsEmpty As Boolean
        Get
            Return MarkX = 0 AndAlso MarkY = 0 AndAlso PositionCenterX = 0 AndAlso PositionCenterY = 0
        End Get
    End Property

    Public Sub New(oSetting As II_Setting)
        MyBase.New(oSetting)
    End Sub

    Public Sub New(sParentName As String, oSetting As II_Setting)
        MyBase.New(oSetting)
        msParentName = sParentName
    End Sub
End Class