Public Class CCameraConfig : Inherits CConfigBase

    Private Class SR
        Public Const CameraType As String = "相機類型"
        Public Const CameraIP As String = "相機 IP"
        Public Const PixelSize As String = "影像解析度 (um)"

        Public Const ImageFileName As String = "測試使用影像檔案名稱"
        Public Const ImageSize As String = "測試影像檔案尺寸"
    End Class

    Private msImageFileName As String = ""

    <Index2Display(10, SR.CameraType)> Public Property CameraType As CameraType = CameraType.CaptureCamera
    <Index2Display(11, SR.CameraIP)> Public Property CameraIP As String = "192.168.11.1"
    <Index2Display(12, SR.PixelSize)> Public Property PixelSize As Double = 20

    <Index2Display(21, SR.ImageSize)> Public Property ImageSize As Size = New Size(1, 1)
    <Index2Display(20, SR.ImageFileName), Editor(GetType(CUINameTypeEditor), GetType(UITypeEditor)), OpenFile(), FileDialogFilter("Camera File (*.BMP)|*.BMP")>
    Public Property ImageFileName As String
        Get
            Return msImageFileName
        End Get
        Set(value As String)
            msImageFileName = value

            If IO.File.Exists(msImageFileName) = True Then
                Dim oBitmap As Bitmap = CType(Image.FromFile(msImageFileName), Bitmap)

                If (oBitmap.Width Mod 32) = 0 Then
                    ImageSize = New Size With {.Width = oBitmap.Width, .Height = oBitmap.Height}
                Else
                    Call MsgBox("需要處理的資料影像，影像寬度必須為 32 的倍數！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                    msImageFileName = ""
                End If
                oBitmap.Dispose()
            End If
        End Set
    End Property

    Public Sub New(oSetting As II_Setting)
        MyBase.New(oSetting)
    End Sub
End Class