Public Class CHandshakeThread : Inherits CThreadBaseExtend

    Public Event ChangeRecipe(oHandshakeProductList As List(Of CMyProduct)) '更換樣本-事件

    Private moMyEquipment As CMyEquipment
    Private moProductProcess As New CMyProduct
    Private moHandshakeType As HandshakeType = HandshakeType.NA

    Public Sub New(oMyEquipment As CMyEquipment)
        MyBase.New(oMyEquipment.LogHandshake, " Handshake")
        moMyEquipment = oMyEquipment
        Call moLog.Log(LOGHandle.HANDLE_CREATE, String.Format("{0} 流程啟動", moThread.Name))
    End Sub

    Public Overrides Sub Process()
        While True
            If mbStopSlim.IsSet = True Then
                Exit While
            End If

            Try
                Call ProcessSingleRun()
            Catch ex As System.Exception
                Call moLog.LogError(ex.ToString)
            End Try

            Thread.Sleep(50)
        End While
    End Sub

    Public Sub ProcessSingleRun()
        If moMyEquipment.IsHandshakeCanProcess = False Then Exit Sub
        Try
            Dim oAlarmCode As AlarmCode = AlarmCode.IsOK
            If moMyEquipment.IO.SafeSensor1 IsNot Nothing AndAlso moMyEquipment.IO.SafeSensor2 IsNot Nothing AndAlso (moMyEquipment.IO.SafeSensor1.IsOn = True OrElse moMyEquipment.IO.SafeSensor2.IsOn = True) AndAlso (moMyEquipment.IO.LightVacuumUp1.IsOn() = True OrElse moMyEquipment.IO.LightVacuumUp2.IsOn() = True) Then
                Call moMyEquipment.IO.LightVacuumUp1.SetOff()
                Call moMyEquipment.IO.LightVacuumUp2.SetOff()
                moMyEquipment.LogSystem.LogError("安全 Sensor 已啟動")
                moMyEquipment.LogAlarm.LogError("安全 Sensor 已啟動")
            End If
            If moMyEquipment.IO.HomeSensor IsNot Nothing AndAlso moMyEquipment.IO.HomeSensor.IsOn = True AndAlso (moMyEquipment.IO.LightVacuumUp1.IsOn() = True OrElse moMyEquipment.IO.LightVacuumUp2.IsOn() = True) Then
                Call Thread.Sleep(moMyEquipment.HardwareConfig.MiscConfig.HomeSensorDelayTime)
                If moMyEquipment.IO.LightVacuumUp1.IsOn() = True OrElse moMyEquipment.IO.LightVacuumUp2.IsOn() = True Then
                    Call moMyEquipment.IO.LightVacuumUp1.SetOff()
                    Call moMyEquipment.IO.LightVacuumUp2.SetOff()
                    moMyEquipment.LogSystem.LogError("原點 Sensor 已啟動")
                    moMyEquipment.LogAlarm.LogError("原點 Sensor 已啟動")
                    If moMyEquipment.IO.LightVacuumUp1.IsOn() = True OrElse moMyEquipment.IO.LightVacuumUp2.IsOn() = True Then
                        moMyEquipment.SetEroorOn()
                        moMyEquipment.TriggerAlarm(AlarmCode.IsNotSafty)
                    End If
                End If
            End If

            If moMyEquipment.IsCanInspect.IsSet() = False AndAlso moMyEquipment.IO.ProductPresentSensor IsNot Nothing AndAlso moMyEquipment.IO.ProductPresentSensor.IsOn() = False Then
                Call Thread.Sleep(100)
                moMyEquipment.LogSystem.LogInformation("檢測已啟動")
                Call moMyEquipment.IsCanInspect.Set()
            End If

            If moMyEquipment.InnerThread.Inspect.IsSet() = False AndAlso moMyEquipment.IsAlarm.IsSet() = False Then
                If moProductProcess.MarkList.Count > 0 Then moProductProcess.ClearMark()
                Dim oProductList As New List(Of CMyProduct)
                moMyEquipment.Read(moHandshakeType, oProductList, moMyEquipment.LotRecipeID, moLog)
                Select Case moHandshakeType
                    Case HandshakeType.LotInfo
                        Call moMyEquipment.InnerThread.HandshakeProcess.Set()

                        While True
                            If moMyEquipment.InnerThread.Inspect.IsSet() = False Then Exit While
                            If mbStopSlim.IsSet() = True Then Exit Select
                            Thread.Sleep(10)
                        End While

                        If moMyEquipment.HardwareConfig.ITBypass = True Then
                            oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsOK, "ACK", moLog)
                            moMyEquipment.LogProcess.LogInformation("Lot Info - IT Bypass，回傳 Ack")

                            If oAlarmCode <> AlarmCode.IsOK Then
                                moMyEquipment.TriggerAlarm(oAlarmCode)
                                Call moMyEquipment.SetEroorOn()
                            End If
                            Exit Select
                        End If

                        If File.Exists(String.Format("{0}\Recipe\{1}.RCP", Application.StartupPath, moMyEquipment.LotRecipeID)) = False Then
                            moMyEquipment.LogProcess.LogInformation("Lot Info - Recipe 不存在")
                            Call moMyEquipment.TriggerAlarm(AlarmCode.IsChangeRecipeFailed)
                            oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsChangeRecipeFailed, "Change Recipe Failed", moLog)
                            Call moMyEquipment.SetEroorOn()
                            Exit Select
                        Else
                            If moMyEquipment.LotRecipeID <> moMyEquipment.MainRecipe.RecipeID Then
                                Try
                                    Call moMyEquipment.MainRecipe.LoadConfig(moMyEquipment.LotRecipeID)

                                    'Call moMyEquipment.Handshake.BuildStripOriginalMapInfo(moMyEquipment.HardwareConfig.MiscConfig.ReadProductXmlPath, oProductList, moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff)

                                    moMyEquipment.MaxDefectCountForUpdateMap = moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount * moMyEquipment.HardwareConfig.MiscConfig.MaxDefectCountForUpdateMap \ 100
                                    moMyEquipment.MaxOffsetCountForUpdateToFttp = moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.MarkXCount * moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.MarkYCount * moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff.MaxOffsetPercentForUpdateToFtp \ 100
                                    If File.Exists(moMyEquipment.MainRecipe.RecipeCamera.TempleteImagePath) = False Then
                                        moMyEquipment.LogProcess.LogInformation("Lot Info - 檢測影像不存在")
                                        Call moMyEquipment.TriggerAlarm(AlarmCode.IsRecipeImageIsNothing)
                                        oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsRecipeImageIsNothing, "Recipe Image Is Nothing", moLog)
                                        Call moMyEquipment.SetEroorOn()
                                        Exit Select
                                    End If

                                    If File.Exists(moMyEquipment.MainRecipe.RecipeCamera.CodeReaderImagePath) = False Then
                                        moMyEquipment.LogProcess.LogInformation("Lot Info - 條碼影像不存在")
                                        Call moMyEquipment.TriggerAlarm(AlarmCode.IsRecipeImageIsNothing)
                                        oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsRecipeImageIsNothing, "Recipe Image Is Nothing", moLog)
                                        Call moMyEquipment.SetEroorOn()
                                        Exit Select
                                    End If

                                    If UpdateRecipeModel() = False Then
                                        moMyEquipment.LogProcess.LogInformation("Lot Info - 更新樣本失敗")
                                        oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsUpdateRecipeModelFailed, "Update Recipe Model Failed", moLog)
                                        Call moMyEquipment.SetEroorOn()
                                        Exit Select
                                    End If

                                    If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then
                                        Call moMyEquipment.CodeReader.SetParameter(moMyEquipment.MainRecipe.RecipeCamera.CodeReader)
                                        Call moMyEquipment.CodeReaderForInspect.SetParameter(moMyEquipment.MainRecipe.RecipeCamera.CodeReaderForInspect)
                                        Call moMyEquipment.CodeReaderForInspect2.SetParameter(moMyEquipment.MainRecipe.RecipeCamera.CodeReaderForInspect2)
                                    End If

                                    Call moMyEquipment.InnerThread.AutoRunThread.UpdateImage()

                                    If moMyEquipment.Camera.Camera.IsNullCamera() = False AndAlso moMyEquipment.Camera.ChangeExposure(moMyEquipment.MainRecipe.ExposureTime, "檢測相機", moLog) = False Then
                                        moMyEquipment.LogProcess.LogInformation("Lot Info - 修改檢測相機曝光時間失敗")
                                        oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsChangeCameraExposureTimeFailed, "Change Inspect Camera Exposure Time Failed", moLog)
                                        Call moMyEquipment.SetEroorOn()
                                        Exit Select
                                    End If

                                    If moMyEquipment.CodeReaderCamera.Camera.IsNullCamera() = False AndAlso moMyEquipment.CodeReaderCamera.ChangeExposure(moMyEquipment.MainRecipe.RecipeCamera.CodeReader.CodeReaderExposureTime1, "條碼相機", moLog) = False Then
                                        moMyEquipment.LogProcess.LogInformation("Lot Info - 修改條碼相機曝光時間失敗")
                                        oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsChangeCodeReaderCameraExposureTimeFailed, "Change Code Reader Camera Exposure Time Failed", moLog)
                                        Call moMyEquipment.SetEroorOn()
                                        Exit Select
                                    End If
                                Catch ex As Exception
                                    moMyEquipment.TriggerAlarm(AlarmCode.IsChangeRecipeFailed)
                                    oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsChangeRecipeFailed, "Change Recipe Failed", moLog)
                                    Call moMyEquipment.SetEroorOn()
                                    Exit Select
                                End Try
                            End If
                            moMyEquipment.IsChangeModel = True '設定-更換樣本
                            RaiseEvent ChangeRecipe(oProductList) '觸發-更換樣本-事件
                            oAlarmCode = moMyEquipment.SendLotInfoACK(AlarmCode.IsOK, "ACK", moLog)

                            If oAlarmCode <> AlarmCode.IsOK Then
                                moMyEquipment.TriggerAlarm(oAlarmCode)
                                Call moMyEquipment.SetEroorOn()
                                Exit Select
                            End If
                        End If

                        'Case HandshakeType.StripMapDownload
                        '    oAlarmCode = moMyEquipment.SendStripMapDownloadACK(AlarmCode.IsOK, moProductProcess, moLog)
                        '    If oAlarmCode <> AlarmCode.IsOK Then
                        '        moMyEquipment.TriggerAlarm(oAlarmCode)
                        '        Exit Sub
                        '    End If
                        '    SyncLock CMyEquipment.ProductQueueLock
                        '        moMyEquipment.ProductList.Add(moProductProcess)
                        '    End SyncLock
                        '    moProductProcess = New CMyProduct
                End Select
            End If

            Call moMyEquipment.InnerThread.HandshakeProcess.Reset()
        Catch ex As Exception
            Call moLog.LogError(String.Format("交握流程錯誤！Error：{0}", ex.ToString()))
            Call moMyEquipment.SetEroorOn()
        End Try

        If moMyEquipment.InnerThread.IsRunning = True AndAlso moMyEquipment.IO.StartLight IsNot Nothing AndAlso moMyEquipment.IO.StartLight.IsOn() = False Then moMyEquipment.IO.StartLight.SetOn()
        If moMyEquipment.InnerThread.IsRunning = False AndAlso moMyEquipment.IO.StartLight IsNot Nothing AndAlso moMyEquipment.IO.StartLight.IsOn() = True Then moMyEquipment.IO.StartLight.SetOff()
        If (moMyEquipment.IsAlarm.IsSet() = True OrElse moMyEquipment.IsErrorOn.IsSet() = True) AndAlso moMyEquipment.IO.AlarmLight IsNot Nothing AndAlso moMyEquipment.IO.AlarmLight.IsOn() = False Then moMyEquipment.IO.AlarmLight.SetOn()
        If (moMyEquipment.IsAlarm.IsSet() = False AndAlso moMyEquipment.IsErrorOn.IsSet() = False) AndAlso moMyEquipment.IO.AlarmLight IsNot Nothing AndAlso moMyEquipment.IO.AlarmLight.IsOn() = True Then moMyEquipment.IO.AlarmLight.SetOff()
    End Sub

    Private Function UpdateRecipeModel() As Boolean
        If moMyEquipment.HardwareConfig.InspectBypass = True Then Return False
        GC.Collect()
        If moMyEquipment.BuildImageForLoad(moMyEquipment.MainRecipe.RecipeCamera.TempleteImagePath, moMyEquipment.ImageID, moMyEquipment.ImageHeader, -1, moMyEquipment.LogSystem) = True Then
            UpdateModelList(moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff, moMyEquipment.ImageID, moMyEquipment.MainRecipe.RecipeID)
        Else
            Call moLog.LogError("圖檔載入失敗！")
            DeleteModel(moMyEquipment.MainRecipe.RecipeCamera.RecipeModelDiff)
            Return False
        End If

        If moMyEquipment.MainRecipe.RecipeCamera.Locate1.PatternZone = Rectangle.Empty OrElse moMyEquipment.MainRecipe.RecipeCamera.Locate2.PatternZone = Rectangle.Empty OrElse moMyEquipment.AddModel(moMyEquipment.ImageID, moMyEquipment.ImageHeader, moMyEquipment.MainRecipe.RecipeCamera.Locate1.PatternZone, moMyEquipment.MainRecipe.RecipeCamera.Locate2.PatternZone) = False Then
            Call moLog.LogError("Camera Add Model 失敗！")
            Return False
        End If

        GC.Collect()
        Return True
    End Function
End Class