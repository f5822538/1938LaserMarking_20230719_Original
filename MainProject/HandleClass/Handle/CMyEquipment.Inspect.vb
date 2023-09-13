Imports iTVisionInspectLib

Public Structure CalculationResult
    Public Succeed As Boolean
    Public Angle As Double
    Public X As Double
    Public Y As Double
    Public OffsetAngle As Double
    Public OffsetX As Integer
    Public OffsetY As Integer

    Public Sub Clear()
        Succeed = False
        Angle = 0.0
        X = 0.0
        Y = 0.0
        OffsetAngle = 0.0
        OffsetX = 0
        OffsetY = 0
    End Sub
End Structure

Partial Class CMyEquipment

    Private moImageProcess As II_ImageProcess
    Private moImagePreprocess As II_ImagePreprocess  '' Augustin 230109
    Public Locater1 As CMyLocater '定位孔-1
    Public Locater2 As CMyLocater '定位孔-2
    Public IsNotUpdateMap As Boolean = False '預設值-資料上報
    Public mdCalAngle As Double = 0.0

    Private Function InitialLocater() As Boolean
        Try
            moImageProcess = CImageProcessCreator.CreateImageProcess()
            moImagePreprocess = CImagePreprocessCreator.CreateImagePreprocess()  '' Augustin 230109
            Locater1 = New CMyLocater(Me)
            Locater2 = New CMyLocater(Me)
            Return True
        Catch ex As Exception
            Call LogSystem.LogError(String.Format("創建 Locater 失敗，Error：{0}", ex.ToString))
            Call LogAlarm.LogError("創建 Locater 失敗")
            Return False
        End Try
    End Function

    Public Function AllocateInspect(oImageHeader As ImageHeader) As AlarmCode
        Try
            If Locater1.UpdateModel(UpdateImage(oImageHeader), MainRecipe.RecipeCamera.Locate1.FindModelZone, MainRecipe.RecipeCamera.Locate1.PatternZone, MainRecipe.RecipeCamera.Locate1.Score, MainRecipe.RecipeCamera.Locate1.Smoth) = False Then
                LogSystem.LogError("樣本 1 更新失敗！")
                Return AlarmCode.IsUpdateModelFailed
            End If

            If Locater2.UpdateModel(UpdateImage(oImageHeader), MainRecipe.RecipeCamera.Locate2.FindModelZone, MainRecipe.RecipeCamera.Locate2.PatternZone, MainRecipe.RecipeCamera.Locate2.Score, MainRecipe.RecipeCamera.Locate2.Smoth) = False Then
                LogSystem.LogError("樣本 2 更新失敗！")
                Return AlarmCode.IsUpdateModelFailed
            End If

            Return AlarmCode.IsOK
        Catch ex As Exception
            Return AlarmCode.IsAllocateInspectFailed
        End Try
    End Function

    ''' <summary>
    ''' 重要區塊 (利用FindModel算法尋找定位孔)
    ''' </summary>
    ''' <param name="oBitmap"></param>
    ''' <param name="nSequence"></param>
    ''' <param name="oLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindModel(oBitmap As Bitmap, nSequence As Integer, oLog As II_LogTraceExtend) As Boolean
        Dim bFindModelSuccess As Boolean = True

        If moMainRecipe.RecipeCamera.Locate1.FindModelZone = Rectangle.Empty Then
            FindMark1X = 0.0
            FindMark1Y = 0.0
        ElseIf Locater1.Find(oBitmap) = True AndAlso Locater1.Result.Succeed = True Then '定位孔-1
            '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            FindMark1X = Locater1.Result.X
            FindMark1Y = Locater1.Result.Y
            '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            If nSequence = -1 Then
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("FindModel Find [Model 1]：X = {0}, Y = {1}, Score = {2}", FindMark1X, FindMark1Y, Locater1.Result.Score)) '定位重要訊息
            Else
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("[{0:d4}] FindModel Find [Model 1]：X = {1}, Y = {2}, Score = {3}", nSequence, FindMark1X, FindMark1Y, Locater1.Result.Score)) '定位重要訊息
            End If
        Else
            bFindModelSuccess = False
            If nSequence = -1 Then
                Call oLog.LogError(String.Format("FindModel Find [Model 1] Failed"))
            Else
                Call oLog.LogError(String.Format("[{0:d4}] FindModel Find [Model 1] Failed", nSequence))
            End If
        End If

        If moMainRecipe.RecipeCamera.Locate2.FindModelZone = Rectangle.Empty Then
            FindMark2X = 0.0
            FindMark2Y = 0.0
        ElseIf Locater2.Find(oBitmap) = True AndAlso Locater2.Result.Succeed = True Then '定位孔-2
            '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            FindMark2X = Locater2.Result.X
            FindMark2Y = Locater2.Result.Y
            '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            If nSequence = -1 Then
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("FindModel Find [Model 2]：X = {0}, Y = {1}, Score = {2}", FindMark2X, FindMark2Y, Locater2.Result.Score)) '定位重要訊息
            Else
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("[{0:d4}] FindModel Find [Model 2]：X = {1}, Y = {2}, Score = {3}", nSequence, FindMark2X, FindMark2Y, Locater2.Result.Score)) '定位重要訊息
            End If
        Else
            bFindModelSuccess = False
            If nSequence = -1 Then
                Call oLog.LogError(String.Format("FindModel Find [Model 2] Failed"))
            Else
                Call oLog.LogError(String.Format("[{0:d4}] FindModel Find [Model 2] Failed", nSequence))
            End If
        End If

        Return bFindModelSuccess
    End Function

    ''' <summary>
    ''' Augustin 230202 New Find Model Test (使用FindCircle算法尋找定位孔)
    ''' </summary>
    ''' <param name="oBitmap"></param>
    ''' <param name="nSequence"></param>
    ''' <param name="oLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindModelChangeModel(oBitmap As Bitmap, nSequence As Integer, oLog As II_LogTraceExtend) As Boolean
        Dim bFindModelSuccess As Boolean = True

        If moMainRecipe.RecipeCamera.Locate1.FindModelZone = Rectangle.Empty Then
            FindMark1X = 0.0
            FindMark1Y = 0.0
        ElseIf Locater1.FindChangeModel(oBitmap, moMainRecipe.RecipeCamera.Locate1.FindModelZone, 0) = True AndAlso Locater1.Result.Succeed = True Then
            '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            FindMark1X = Locater1.Result.X
            FindMark1Y = Locater1.Result.Y
            '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            If nSequence = -1 Then
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("FindModelChangeModel Find [Model 1]：X = {0}, Y = {1}, Score = {2}", FindMark1X, FindMark1Y, Locater1.Result.Score)) '定位重要訊息
            Else
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("[{0:d4}] FindModelChangeModel Find [Model 1]：X = {1}, Y = {2}, Score = {3}", nSequence, FindMark1X, FindMark1Y, Locater1.Result.Score)) '定位重要訊息
            End If
        Else
            bFindModelSuccess = False
            If nSequence = -1 Then
                Call oLog.LogError(String.Format("FindModelChangeModel Find [Model 1] Failed"))
            Else
                Call oLog.LogError(String.Format("[{0:d4}] FindModelChangeModel Find [Model 1] Failed", nSequence))
            End If
        End If

        If moMainRecipe.RecipeCamera.Locate2.FindModelZone = Rectangle.Empty Then
            FindMark2X = 0.0
            FindMark2Y = 0.0
        ElseIf Locater2.FindChangeModel(oBitmap, moMainRecipe.RecipeCamera.Locate2.FindModelZone, 1) = True AndAlso Locater2.Result.Succeed = True Then
            '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            FindMark2X = Locater2.Result.X
            FindMark2Y = Locater2.Result.Y
            '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            If nSequence = -1 Then
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("FindModelChangeModel Find [Model 2]：X = {0}, Y = {1}, Score = {2}", FindMark2X, FindMark2Y, Locater2.Result.Score)) '定位重要訊息
            Else
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("[{0:d4}] FindModelChangeModel Find [Model 2]：X = {1}, Y = {2}, Score = {3}", nSequence, FindMark2X, FindMark2Y, Locater2.Result.Score)) '定位重要訊息
            End If
        Else
            bFindModelSuccess = False
            If nSequence = -1 Then
                Call oLog.LogError(String.Format("FindModelChangeModel Find [Model 2] Failed"))
            Else
                Call oLog.LogError(String.Format("[{0:d4}] FindModelChangeModel Find [Model 2] Failed", nSequence))
            End If
        End If

        Return bFindModelSuccess
    End Function

    ''' <summary>
    ''' 重要區塊 (使用FindCircle算法尋找定位孔)
    ''' </summary>
    ''' <param name="oLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeModel(oLog As II_LogTraceExtend) As Boolean
        Try
            Dim oBitmap As Bitmap = UpdateImage(ImageHeader)
            Dim nRectangle As New Rectangle
            Dim nPatternZone As Rectangle = moMainRecipe.RecipeCamera.Locate1.PatternZone '取得-定位孔1-樣本區域 (X,Y,W,H)
            Dim nOffset As Point = Point.Empty
            Dim nOffsetLimit As Integer = CInt(moHardwareConfig.OffsetLimitValue / moHardwareConfig.CameraConfig.PixelSize)

            '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            FindModelChangeModel(oBitmap, -1, oLog) '使用FindCircle算法尋找定位孔
            '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            nRectangle.X = CInt(FindMark1X - (moMainRecipe.RecipeCamera.Locate1.PatternZone.Width / 2))
            nRectangle.Y = CInt(FindMark1Y - (moMainRecipe.RecipeCamera.Locate1.PatternZone.Height / 2))
            nRectangle.Width = moMainRecipe.RecipeCamera.Locate1.PatternZone.Width
            nRectangle.Height = moMainRecipe.RecipeCamera.Locate1.PatternZone.Height
            moMainRecipe.RecipeCamera.Locate1.PatternZone = nRectangle '設定-定位孔1-樣本區域 (X,Y,W,H)

            nRectangle.X = CInt(FindMark2X - (moMainRecipe.RecipeCamera.Locate2.PatternZone.Width / 2))
            nRectangle.Y = CInt(FindMark2Y - (moMainRecipe.RecipeCamera.Locate2.PatternZone.Height / 2))
            nRectangle.Width = moMainRecipe.RecipeCamera.Locate2.PatternZone.Width
            nRectangle.Height = moMainRecipe.RecipeCamera.Locate2.PatternZone.Height
            moMainRecipe.RecipeCamera.Locate2.PatternZone = nRectangle '設定-定位孔2-樣本區域 (X,Y,W,H)
            moMainRecipe.RecipeCamera.RecipeModelDiff.SummationSquareCount = 0

            Call ClearStandardDeviationModel(moMainRecipe.RecipeCamera.RecipeModelDiff)
            Call SaveStandardDeviationModel(moMainRecipe.RecipeCamera.RecipeModelDiff, Application.StartupPath & "\Recipe", moMainRecipe.RecipeID)

            '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            '參數1: ImageHeader
            '參數2: 設定-尋找區域 (X,Y,W,H)
            '參數3: 設定-樣本區域 (X,Y,W,H)
            '參數4: 設定-定位孔灰階門檻值 (10)
            '參數5: 設定-定位孔最小面積 (100)
            Locater1.UpdateModelCenter(ImageHeader, moMainRecipe.RecipeCamera.Locate1.FindModelZone, moMainRecipe.RecipeCamera.Locate1.PatternZone, MainRecipe.Threshold, MainRecipe.RecipeCamera.Locate1.Area)
            Locater2.UpdateModelCenter(ImageHeader, moMainRecipe.RecipeCamera.Locate2.FindModelZone, moMainRecipe.RecipeCamera.Locate2.PatternZone, MainRecipe.Threshold, MainRecipe.RecipeCamera.Locate2.Area)
            '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            nOffset.X = nPatternZone.X - moMainRecipe.RecipeCamera.Locate1.PatternZone.X '定位孔1-X軸-位移量
            nOffset.Y = nPatternZone.Y - moMainRecipe.RecipeCamera.Locate1.PatternZone.Y '定位孔1-Y軸-位移量

            Call Rotate(ImageHeader, ImageHeader, 0, 0, 0, nOffset.X, nOffset.Y)

            Dim nROI As Rectangle = moMainRecipe.RecipeCamera.Locate1.FindModelZone '設定nROI-定位孔1-尋找區域 (X,Y,W,H)

            '' Augustin 230215 Test
            If moHardwareConfig.OffsetLimitUse = True AndAlso Math.Abs(nOffset.Y) <= nOffsetLimit Then
                Call nROI.Offset(nOffset) '位移-矩形的位置
                moMainRecipe.RecipeCamera.Locate1.FindModelZone = nROI '設定-定位孔1-尋找區域 (X,Y,W,H)為nROI

                nROI = moMainRecipe.RecipeCamera.Locate2.FindModelZone '設定nROI-定位孔2-尋找區域 (X,Y,W,H)

                Call nROI.Offset(nOffset) '位移-矩形的位置
                moMainRecipe.RecipeCamera.Locate2.FindModelZone = nROI '設定-定位孔2-尋找區域 (X,Y,W,H)為nROI
            Else
                If nOffset.Y > 0 Then
                    nOffset.Y = nOffsetLimit
                Else
                    nOffset.Y = -nOffsetLimit
                End If

                '------------------------Debug-定位孔尋找區域位移-開始--------------------------
                If Debugger.IsAttached = True Then
                    Call nROI.Offset(nOffset) '位移-矩形的位置(現行執行處)
                    moMainRecipe.RecipeCamera.Locate1.FindModelZone = nROI '設定-定位孔1-尋找區域 (X,Y,W,H)為nROI

                    nROI = moMainRecipe.RecipeCamera.Locate2.FindModelZone '設定nROI-定位孔2-尋找區域 (X,Y,W,H)

                    Call nROI.Offset(nOffset) '位移-矩形的位置(現行執行處)
                    moMainRecipe.RecipeCamera.Locate2.FindModelZone = nROI '設定-定位孔2-尋找區域 (X,Y,W,H)為nROI
                End If
                '------------------------Debug-定位孔尋找區域位移-結束--------------------------
            End If

            nOffset.Y = nPatternZone.Y - moMainRecipe.RecipeCamera.Locate1.PatternZone.Y '定位孔1-Y軸-位移量

            '------------------------Debug-定位孔樣本區域位移-開始--------------------------
            If Debugger.IsAttached = True Then
                nROI = moMainRecipe.RecipeCamera.Locate1.PatternZone '設定nROI-定位孔1-樣本區域 (X,Y,W,H)
                Call nROI.Offset(nOffset) '樣本區域-定位孔1-位移-矩形的位置(現行執行處)
                moMainRecipe.RecipeCamera.Locate1.PatternZone = nROI '設定-定位孔1-樣本區域 (X,Y,W,H)為nROI

                nROI = moMainRecipe.RecipeCamera.Locate2.PatternZone '設定nROI-定位孔2-樣本區域 (X,Y,W,H)
                Call nROI.Offset(nOffset) '樣本區域-定位孔2-位移-矩形的位置(現行執行處)
                moMainRecipe.RecipeCamera.Locate2.PatternZone = nROI '設定-定位孔2-樣本區域 (X,Y,W,H)為nROI
            End If
            '------------------------Debug-定位孔樣本區域位移-結束--------------------------

            Call Locater1.UpdateModel(oBitmap, moMainRecipe.RecipeCamera.Locate1.FindModelZone, moMainRecipe.RecipeCamera.Locate1.PatternZone, moMainRecipe.RecipeCamera.Locate1.Score, MainRecipe.RecipeCamera.Locate1.Smoth)
            Call Locater2.UpdateModel(oBitmap, moMainRecipe.RecipeCamera.Locate2.FindModelZone, moMainRecipe.RecipeCamera.Locate2.PatternZone, moMainRecipe.RecipeCamera.Locate2.Score, MainRecipe.RecipeCamera.Locate2.Smoth)

            If BuildImageForCopy(oBitmap, RecipeID, RecipeHeader, -1, oLog) = False Then Return False
            moMainRecipe.RecipeCamera.RecipeModelDiff.IsGatherStandardDeviation = False '是否收集標準差影像-False
            Call moMainRecipe.SaveConfig(moMainRecipe.RecipeID)
            Call oBitmap.Save(moMainRecipe.RecipeCamera.TempleteImagePath, Imaging.ImageFormat.Bmp)
            Call UpdateModelList(moMainRecipe.RecipeCamera.RecipeModelDiff, RecipeID)
            Call UpdateDefectROI()  '' Augustin 220726 Add for Wafer Map
            moMainRecipe.RecipeCamera.RecipeModelDiff.IsGatherStandardDeviation = True '是否收集標準差影像-True

            Return True
        Catch ex As Exception
            Call oLog.LogError("Change Model Failed")
            Return False
        End Try
    End Function

    Public Function Rotate(oSourceImageHeader As ImageHeader, oDestinationImageHeader As ImageHeader, nOffsetAngle As Double, nCenterX As Integer, nCenterY As Integer, nOffsetX As Integer, nOffsetY As Integer) As Boolean
        If moImageProcess Is Nothing OrElse oSourceImageHeader.Ptr = IntPtr.Zero OrElse oDestinationImageHeader.Ptr = IntPtr.Zero OrElse oSourceImageHeader.Width <> oDestinationImageHeader.Width OrElse oSourceImageHeader.Height <> oDestinationImageHeader.Height Then Return False
        Dim oCopyBitmap As Bitmap = New Bitmap(oSourceImageHeader.Width, oSourceImageHeader.Height, Imaging.PixelFormat.Format8bppIndexed)
        Dim oCopyImageHeader As ImageHeader = GetImageHeader(oCopyBitmap)

        If moImageProcess.Rotate(oSourceImageHeader, oCopyImageHeader, nOffsetAngle, nCenterX, nCenterY, nOffsetX, nOffsetY) = False Then
            oCopyBitmap.Dispose()
            oCopyBitmap = Nothing
            Return False
        End If

        Call moImageProcess.BufferCopy(oCopyImageHeader, oDestinationImageHeader)

        oCopyBitmap.Dispose()
        oCopyBitmap = Nothing
        Return True
    End Function

    '' Augustin 230202
    Private Sub NewCalculationShift(dFindModelX1 As Double, dFindModelY1 As Double, dFindModelX2 As Double, dFindModelY2 As Double)
        Dim dLineX1 As Double = 0.0
        Dim dLineY1 As Double = 0.0
        Dim dLineX2 As Double = 0.0
        Dim dLineY2 As Double = 0.0
        Dim dAngle1 As Double = 0.0
        Dim dAngle2 As Double = 0.0
        Dim LineDist1 As Double = 0.0
        Dim LineDist2 As Double = 0.0
        Dim ShiftX As Double = 0.0
        Dim ShiftY As Double = 0.0


        With moMainRecipe.RecipeCamera
            'oModelFinderShift.refX = dFindModelX1
            'oModelFinderShift.refY = dFindModelY1
            'oModelFinderShift.shiftX = .Locate1.PatternZone.X + (.Locate1.PatternZone.Width / 2) - dFindModelX1
            'oModelFinderShift.shiftY = .Locate1.PatternZone.Y + (.Locate1.PatternZone.Height / 2) - dFindModelY1
            ShiftX = .Locate1.PatternZone.X + (.Locate1.PatternZone.Width / 2) - dFindModelX1
            ShiftY = .Locate1.PatternZone.Y + (.Locate1.PatternZone.Height / 2) - dFindModelY1

            dLineX1 = (.Locate2.PatternZone.X + .Locate2.PatternZone.Width / 2) - (.Locate1.PatternZone.X + .Locate1.PatternZone.Width / 2)
            dLineY1 = (.Locate2.PatternZone.Y + .Locate2.PatternZone.Height / 2) - (.Locate1.PatternZone.Y + .Locate1.PatternZone.Height / 2)
        End With

        LineDist1 = Math.Sqrt(dLineX1 ^ 2 + dLineY1 ^ 2)

        '' Augustin 230107 Test
        Dim NewX = dFindModelX2 - ShiftX
        Dim NewY = dFindModelY2 - ShiftY
        LineDist2 = Math.Sqrt((NewX - (moMainRecipe.RecipeCamera.Locate1.PatternZone.X + moMainRecipe.RecipeCamera.Locate1.PatternZone.Width / 2)) ^ 2 + _
                              (NewY - (moMainRecipe.RecipeCamera.Locate1.PatternZone.Y + moMainRecipe.RecipeCamera.Locate1.PatternZone.Height / 2)) ^ 2)

        Dim TanTheta As Double = LineDist2 / LineDist1
        dAngle1 = Math.Atan(TanTheta)
        mdCalAngle = dAngle1
        'dLineX2 = dFindModelX2 - dFindModelX1
        'dLineY2 = dFindModelY2 - dFindModelY1
        'Dim m1 As Double = dLineY1 / dLineX1
        'Dim m2 As Double = dLineY2 / dLineX2
        'dAngle1 = Math.Atan((m1 - m2) / 1 + m2 * m1)
        'oModelFinderShift.Angle = dAngle1
        'dAngle1 = Math.Atan(m1) - Math.Atan(m2)
        'oModelFinderShift.Angle = dAngle1 * 180 / Math.PI

    End Sub
End Class