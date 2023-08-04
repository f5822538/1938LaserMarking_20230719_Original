Public Class CMyModelImage
    Public ModelImage As MIL_ID = 0
    Public IsProcess As Boolean = True
    'Public IsPass As Boolean = False
    Public IsOffset As Boolean = False
    Public IsLose As Boolean = False
    Public IsOffsetGray As Boolean = False
    Public CenterX As Integer = 0
    Public CenterY As Integer = 0
    Public PositionX As Integer = 0
    Public PositionY As Integer = 0
    Public PositionAngle As Double = 0.0
    Public MarkX As Integer = 0
    Public MarkY As Integer = 0

    Public Sub CameraImageClear()
        If ModelImage <> 0 Then
            MIL.MbufFree(ModelImage)
            ModelImage = MIL.M_NULL
        End If
    End Sub
End Class