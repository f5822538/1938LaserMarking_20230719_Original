Partial Class CMyCamera

    Private WithEvents moSnapMultiple As II_SnapMultiple

    Public Function Create() As Boolean
        Dim bIsOpen = True
        Try
            If moCameraConfig.CameraType = CameraType.CaptureCamera Then
                Dim oTuple As Tuple(Of Boolean, II_Camera, II_CameraLightControl, II_SnapMultiple, II_CameraOutput)

                oTuple = CCameraCreateSingle.CreateDaHuaCameraByIP(moCameraConfig.CameraIP, CLogCreateor.CreateLogToLogExtend(moLog))
                With oTuple
                    Try
                        If .Item1 = True AndAlso .Item2.CameraOpen = True Then
                            moMyEquipment.MyLog.LogSystem.Log(LOGHandle.HANDLE_CREATE, String.Format("相機 {0} 開啟成功", moCameraConfig.CameraIP))
                            moCamera = .Item2
                            moCameraLightControl = .Item3
                            moSnapMultiple = .Item4
                            'moSnapContinus = .Item6
                            'moSnapContinus.SnapStop()
                            moCamera.SetTrigMode(True)
                        Else
                            If moMyEquipment.HardwareConfig.Debug = False Then
                                bIsOpen = False
                                moMyEquipment.MyLog.LogSystem.LogError(String.Format("相機 {0} 開啟失敗，使用 Null Camera", moCameraConfig.CameraIP))
                                If moMyEquipment.HardwareConfig.Debug = False Then MsgBox(String.Format("相機 {0} 開啟失敗，使用 Null Camera！", moCameraConfig.CameraIP), MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                            Else
                                moMyEquipment.MyLog.LogSystem.LogError(String.Format("相機 {0} 開啟失敗，使用 Null Camera", moCameraConfig.CameraIP))
                                moMyEquipment.MyLog.LogAlarm.LogError(String.Format("相機 {0} 開啟失敗，使用 Null Camera", moCameraConfig.CameraIP))
                            End If

                            With CCameraCreateSingle.CreateNullCameraSingle(moCameraConfig.ImageSize.Width, moCameraConfig.ImageSize.Height, 200)
                                moCamera = .Item1
                                moSnapMultiple = .Item3
                            End With

                            If File.Exists(moCameraConfig.ImageFileName) = True Then
                                moCamera.LoadFromFile(moCameraConfig.ImageFileName, True)
                                moCamera.LoadFromFile(moCameraConfig.ImageFileName, False)
                            Else
                                moMyEquipment.MyLog.LogSystem.LogError(String.Format("Null Camera {0} 開啟失敗，檔案不存在", moCameraConfig.CameraIP))
                                moMyEquipment.MyLog.LogAlarm.LogError(String.Format("Null Camera {0} 開啟失敗，檔案不存在", moCameraConfig.CameraIP))
                                bIsOpen = False
                            End If
                        End If
                    Catch ex As Exception
                        moMyEquipment.MyLog.LogSystem.LogError(String.Format("相機 {0} 開啟失敗，Error：{1}", moCameraConfig.CameraIP, ex.ToString))
                        moMyEquipment.MyLog.LogAlarm.LogError(String.Format("相機 {0} 開啟失敗", moCameraConfig.CameraIP))
                        bIsOpen = False
                    End Try
                End With
            Else
                Try
                    With CCameraCreateSingle.CreateNullCameraSingle(moCameraConfig.ImageSize.Width, moCameraConfig.ImageSize.Height, 200)
                        moCamera = .Item1
                        moSnapMultiple = .Item3
                    End With

                    If File.Exists(moCameraConfig.ImageFileName) = True Then
                        moCamera.LoadFromFile(moCameraConfig.ImageFileName, True)
                        moCamera.LoadFromFile(moCameraConfig.ImageFileName, False)
                    Else
                        moMyEquipment.MyLog.LogSystem.LogError(String.Format("Null Camera {0} 開啟失敗，檔案不存在", moCameraConfig.CameraIP))
                        moMyEquipment.MyLog.LogAlarm.LogError(String.Format("Null Camera {0} 開啟失敗，檔案不存在", moCameraConfig.CameraIP))
                        bIsOpen = False
                    End If
                Catch ex As Exception
                    moMyEquipment.MyLog.LogSystem.LogError(String.Format("{0} Null Camera 開啟失敗，Error：{1}", moCameraConfig.CameraIP, ex.ToString))
                    moMyEquipment.MyLog.LogAlarm.LogError(String.Format("{0} Null Camera 開啟失敗", moCameraConfig.CameraIP))
                    bIsOpen = False
                End Try
            End If
        Catch ex As Exception
            moMyEquipment.MyLog.LogSystem.LogError(String.Format("創建 {0} Camera 失敗，Error：{1}", moCameraConfig.CameraIP, ex.ToString))
            moMyEquipment.MyLog.LogAlarm.LogError(String.Format("創建 {0} Camera 失敗", moCameraConfig.CameraIP))
            bIsOpen = False
        End Try
        Return bIsOpen
    End Function

    Public Function ChangeExposure(nExposureTime As Double, sCameraName As String, oLog As II_LogTraceExtend) As Boolean
        Call moMyEquipment.LogControl.LogInformation(String.Format("{0} 修改曝光時間：{1} us", sCameraName, nExposureTime))

        Dim nCount As Integer = 0, nTimes As Integer = 0
        Dim aTact As New CTactTimeSpan
        Dim sException As String = ""

        While nCount < 3 AndAlso nTimes < 3
            Try
                If moCameraConfig.CameraType = CameraType.CaptureCamera Then
                    If moCamera.IsNullCamera() = False Then moCameraLightControl.ChangeExposure(nExposureTime)
                    Dim nResult As Double = moCameraLightControl.GetExposure
                    If nResult / nExposureTime > 1.1 OrElse nResult / nExposureTime < 0.9 Then
                        nTimes += 1
                    Else
                        Exit While
                    End If
                Else
                    Exit While
                End If
            Catch ex As Exception
                nCount += 1
                If nCount >= 3 Then sException = ex.ToString()
            End Try
        End While

        If nTimes >= 3 Then
            Call oLog.LogError(String.Format("{0} 修改曝光時間失敗", sCameraName))
            Call moMyEquipment.LogControl.LogError(String.Format("{0} 修改曝光時間失敗", sCameraName))
            Call moMyEquipment.LogAlarm.LogError(String.Format("{0} 修改曝光時間失敗", sCameraName))
        End If

        If nCount >= 3 Then
            Call oLog.LogError(String.Format("{0} 修改曝光時間錯誤，Error：{1}", sCameraName, sException))
            Call moMyEquipment.LogControl.LogError(String.Format("{0} 修改曝光時間錯誤，Error：{1}", sCameraName, sException))
            Call moMyEquipment.LogAlarm.LogError(String.Format("{0} 修改曝光時間錯誤", sCameraName))
        End If

        Call aTact.CalSpan()
        If aTact.CurrentSpan > 100 Then oLog.LogInformation(String.Format("{0} Change Exposure Time：{1}", sCameraName, aTact.CurrentSpan))
        Return True
    End Function

    Public Function SnapStart(nSequence As Integer, sCameraName As String, oLog As II_LogTraceExtend) As Boolean
        Return True
        If moCamera.IsNullCamera = True Then Return True
        Try
            Call oLog.LogInformation(String.Format("[{0:d4}] {1} 取像開始！", nSequence, sCameraName))
            Call moSnapContinus.SnapStart()
            Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] {1} 開始取像錯誤！Error：{2}", nSequence, sCameraName, ex.ToString()))
            Return False
        End Try
    End Function

    Public Function SnapStop(nSequence As Integer, sCameraName As String, oLog As II_LogTraceExtend) As Boolean
        Return True
        If moCamera.IsNullCamera = True Then Return True
        Try
            Call moSnapContinus.SnapStop()
            Call oLog.LogInformation(String.Format("[{0:d4}] {1} 停止取像！", nSequence, sCameraName))
            Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] {1} 停止取像錯誤！Error：{1}", nSequence, sCameraName, ex.ToString()))
            Return False
        End Try
    End Function

    Public Function Snap(nSequence As Integer, sCameraName As String, oLog As II_LogTraceExtend) As Boolean
        Dim bSnapSuccess As Boolean = False

        For nCount As Integer = 1 To 10
            Try
                bSnapSuccess = moCamera.Snap(True)
                'If moCamera.IsNullCamera() = True Then
                '    bSnapSuccess = moCamera.Snap(True)
                'Else
                '    bSnapSuccess = SnapForContinus(nSequence, sCameraName, oLog)
                'End If
            Catch ex As Exception
                Call moMyEquipment.MyLog.LogSystem.LogError(String.Format("[{0:d4}] {1} 取像錯誤，Error：{2}", nSequence, sCameraName, ex.ToString))
                Call moMyEquipment.MyLog.LogAlarm.LogError(String.Format("[{0:d4}] {1} 取像錯誤", nSequence, sCameraName))
            End Try
            If bSnapSuccess = True Then
                Call oLog.LogInformation(String.Format("[{0:d4}] {1} 取像完成！ [{2}]", nSequence, sCameraName, nCount))
                Exit For
            End If
            Call Thread.Sleep(100)
        Next

        Return bSnapSuccess
    End Function

    Private Function SnapForContinus(nSequence As Integer, sCameraName As String, oLog As II_LogTraceExtend) As Boolean
        Dim bSnapSuccess As Boolean = False
        Dim oTact As New CTactTimeSpan
        Try
            Call moCameraSnapEnd.Reset()
            Call moCameraSnapStart.Set()
            Dim oAlarmCode As AlarmCode = moMyEquipment.IsTimeOutOrStop(2, Function() moCameraSnapEnd.IsSet() = True)
            Call oTact.CalSpan()
            Call moCameraSnapStart.Reset()
            Call moCameraSnapEnd.Reset()

            If oAlarmCode = AlarmCode.IsOK Then
                bSnapSuccess = True
            Else
                bSnapSuccess = False
                Call oLog.LogError(String.Format("[{0:d4}] {1} 取像失敗！", nSequence, sCameraName))
            End If
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] {1} 取像錯誤！Error：{2}", nSequence, sCameraName, ex.ToString()))
        End Try

        Return bSnapSuccess
    End Function

    Private Sub moSnapMultiple_FrameAcquire(oFrameIndex As Integer) Handles moSnapMultiple.FrameAcquire
        If moCameraSnapStart.IsSet() = True Then moCameraSnapEnd.Set()
    End Sub

    Public Sub Close()
        If moCamera IsNot Nothing AndAlso moCamera.IsNullCamera() = False Then
            'Call moSnapContinus.SnapStop()
            Call moCamera.CameraClose()
        End If
    End Sub
End Class