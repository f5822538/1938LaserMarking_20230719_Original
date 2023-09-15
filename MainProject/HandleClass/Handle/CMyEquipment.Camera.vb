Partial Class CMyEquipment

    Private moCamera As CMyCamera
    Private moCodeReaderCamera As CMyCamera

    Public ReadOnly Property Camera As CMyCamera
        Get
            Return moCamera
        End Get
    End Property

    Public ReadOnly Property CodeReaderCamera As CMyCamera
        Get
            Return moCodeReaderCamera
        End Get
    End Property

    ''' <summary>
    ''' 初始化-相機
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InitialCamera() As Boolean
        Dim bIsOpen = True
        Try
            moCamera = New CMyCamera(Me, moHardwareConfig.CameraConfig)
            If moCamera.Create() = False Then bIsOpen = False '開啟相機(檢測相機)
        Catch ex As Exception
            Call LogSystem.LogError(String.Format("創建 Camera 失敗，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("創建 Camera 失敗")
            Return False
        End Try

        Try
            moCodeReaderCamera = New CMyCamera(Me, moHardwareConfig.CodeReaderCameraConfig)
            If moCodeReaderCamera.Create() = False Then bIsOpen = False '開啟相機(條碼相機)
        Catch ex As Exception
            Call LogSystem.LogError(String.Format("創建條碼 Camera 失敗，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("創建條碼 Camera 失敗")
            Return False
        End Try
        Return bIsOpen
    End Function

    Private Sub CameraClose()
        moCamera.Close()
    End Sub
End Class