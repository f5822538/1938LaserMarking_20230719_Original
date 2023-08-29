Module modLibrary

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

    <Extension()> Public Sub CopyMemory(oBitMapTarget As ITVImage, oBitMapSource As Bitmap, x As Integer, y As Integer)
        Dim oSource As New ITVImage
        Dim oROI As New ITVImageROI
        oSource.AssignToBitmap(oBitMapSource)
        oROI.ApplyRegion(New Point(x, y), oBitMapSource.Size)
        Dim oROITarget As ITVImage = oBitMapTarget.GetChildImage(oROI)
        ImageProcessingFunctions.iCopy(oSource, oROITarget)
    End Sub

    Public Function FindEdge(oSourceImageID As MIL_ID, ByRef oMeasureData As MeasureInfo) As Boolean
        Try
            Dim oMeasureContextID As MIL_ID
            Dim nResult As Double
            Dim nResult1 As Double
            MIL.MmeasAllocMarker(MIL.M_DEFAULT_HOST, MIL.M_EDGE, MIL.M_DEFAULT, oMeasureContextID)
            MIL.MmeasSetMarker(oMeasureContextID, MIL.M_NUMBER, MIL.M_ALL, MIL.M_NULL)
            MIL.MmeasSetMarker(oMeasureContextID, MIL.M_FILTER_TYPE, MIL.M_PREWITT, MIL.M_NULL)
            MIL.MmeasSetMarker(oMeasureContextID, MIL.M_BOX_ORIGIN, oMeasureData.ROI.X, oMeasureData.ROI.Y)
            MIL.MmeasSetMarker(oMeasureContextID, MIL.M_BOX_SIZE, oMeasureData.ROI.Width, oMeasureData.ROI.Height)

            Select Case (oMeasureData.MeasurePositive)
                Case RecipeLib.MeasurePositive.BlackToWhite : MIL.MmeasSetMarker(oMeasureContextID, MIL.M_POLARITY, MIL.M_POSITIVE, MIL.M_OPPOSITE)
                Case RecipeLib.MeasurePositive.WhiteToBlack : MIL.MmeasSetMarker(oMeasureContextID, MIL.M_POLARITY, MIL.M_NEGATIVE, MIL.M_OPPOSITE)
                Case RecipeLib.MeasurePositive.Any : MIL.MmeasSetMarker(oMeasureContextID, MIL.M_POLARITY, MIL.M_ANY, MIL.M_OPPOSITE)
            End Select

            Select Case (oMeasureData.Direction)
                Case iTVisionService.Direction.Recipe_UpToBottom
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_ORIENTATION, MIL.M_HORIZONTAL, MIL.M_NULL)
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_BOX_ANGLE, 0.0, MIL.M_NULL)
                Case iTVisionService.Direction.Recipe_BottomToUp
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_ORIENTATION, MIL.M_HORIZONTAL, MIL.M_NULL)
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_BOX_ANGLE, 180.0, MIL.M_NULL)
                Case iTVisionService.Direction.Recipe_LeftToRight
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_ORIENTATION, MIL.M_VERTICAL, MIL.M_NULL)
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_BOX_ANGLE, 0.0, MIL.M_NULL)
                Case iTVisionService.Direction.Recipe_RightToLeft
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_ORIENTATION, MIL.M_VERTICAL, MIL.M_NULL)
                    MIL.MmeasSetMarker(oMeasureContextID, MIL.M_BOX_ANGLE, 180.0, MIL.M_NULL)
            End Select

            MIL.MmeasFindMarker(MIL.M_DEFAULT, oSourceImageID, oMeasureContextID, MIL.M_POSITION)
            MIL.MmeasGetResultSingle(oMeasureContextID, MIL.M_NUMBER, nResult, nResult1, 0)

            If nResult > 0 Then
                oMeasureData.Succeed = True
                MIL.MmeasGetResultSingle(oMeasureContextID, MIL.M_POSITION + MIL.M_EDGE_FIRST, oMeasureData.ResultX, oMeasureData.ResultY, 0)
            Else
                oMeasureData.Succeed = False
                oMeasureData.ResultX = 0
                oMeasureData.ResultY = 0
            End If

            MIL.MmeasFree(oMeasureContextID)
            oMeasureContextID = MIL.M_NULL

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetCorner(ByVal oSource As ImageHeader, ByRef oROI1 As Rectangle, nDirection1 As ITV_Direction, ByVal nIndex1 As Integer, oROI2 As Rectangle, nDirection2 As ITV_Direction, ByVal nIndex2 As Integer, ByRef X As Double, ByRef Y As Double) As Integer
        X = 0
        Y = 0
        Dim oLine As New ITVLineDetector, oImage As New ITVImage, oROI As New ITVImageROI
        oImage.AssignToImageHeader(oSource)

        oROI.ApplyRegion(oROI1)
        oLine.FindLinesFull(oImage, nDirection1, 10, False, 0, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        'oLine.FindLinesWithoutAngle(oImage, nDirection1, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        If nIndex1 + 1 > oLine.NumberOfResult Then Return 0
        If nDirection1 = ITV_Direction.LeftToRight OrElse nDirection1 = ITV_Direction.RightToLeft Then X = oLine.Result(nIndex1).LocationX Else Y = oLine.Result(nIndex1).LocationY

        oROI.ApplyRegion(oROI2)
        oLine.FindLinesFull(oImage, nDirection2, 10, False, 0, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        'oLine.FindLinesWithoutAngle(oImage, nDirection2, LineThresholdMode.ThresholdLow, 0, 0, oROI)
        If nIndex2 + 1 > oLine.NumberOfResult Then Return 0
        If nDirection2 = ITV_Direction.LeftToRight OrElse nDirection2 = ITV_Direction.RightToLeft Then X = oLine.Result(nIndex2).LocationX Else Y = oLine.Result(nIndex2).LocationY
        Return 1
    End Function

    Public Function AnalysisCorner(ByVal oSource As ImageHeader, ByRef oROI1 As Rectangle, nDirection1 As ITV_Direction, ByVal nIndex1 As Integer, oROI2 As Rectangle, nDirection2 As ITV_Direction, ByVal nIndex2 As Integer, ByRef nNum1 As Integer, ByRef nNum2 As Integer) As Integer
        Dim oLine As New ITVLineDetector, oImage As New ITVImage, oROI As New ITVImageROI, otmpImage As New ITVImage
        oImage.AssignToImageHeader(oSource)

        oROI.ApplyRegion(oROI1)

        otmpImage.CreateImage(oROI1.Width, oROI1.Height)
        If nDirection1 = ITV_Direction.TopToBottom OrElse nDirection1 = ITV_Direction.BottomToTop Then
            ImageProcessingFunctions.iMaxFilter(oImage, otmpImage, oROI1.Width, 1, oROI)
        Else
            ImageProcessingFunctions.iMaxFilter(oImage, otmpImage, 1, oROI1.Height, oROI)
        End If
        oLine.FindLinesWithoutAngle(otmpImage, nDirection1, LineThresholdMode.ThresholdLow, 0, 0)
        nNum1 = oLine.NumberOfResult

        oROI.ApplyRegion(oROI2)
        otmpImage.CreateImage(oROI2.Width, oROI2.Height)
        If nDirection2 = ITV_Direction.TopToBottom OrElse nDirection2 = ITV_Direction.BottomToTop Then
            ImageProcessingFunctions.iMaxFilter(oImage, otmpImage, oROI2.Width, 1, oROI)
        Else
            ImageProcessingFunctions.iMaxFilter(oImage, otmpImage, 1, oROI2.Height, oROI)
        End If

        oLine.FindLinesWithoutAngle(otmpImage, nDirection2, LineThresholdMode.ThresholdLow, 0, 0)
        nNum2 = oLine.NumberOfResult
        Return 1
    End Function

    Public Function AddModel(oRecipe As CRecipeModelDiff, TemplateID As MIL_ID) As Boolean
        Try
            Dim bIsOK As Boolean = False
            bIsOK = oRecipe.PatternMatching1St.AddModel(TemplateID, New Rectangle(0, 0, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height))
            If bIsOK = False Then Return False
            bIsOK = oRecipe.PatternMatching2Nd.AddModel(TemplateID, New Rectangle(0, 0, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height))
            Return bIsOK
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 標準差的模型差異(CAutoRunThread.RunInspect -> modLibrary.ModelDiffForStandardDeviation)
    ''' </summary>
    ''' <param name="oCameraSourceImage"></param>
    ''' <param name="oRecipe"></param>
    ''' <param name="oInspectSum"></param>
    ''' <param name="oProduct"></param>
    ''' <param name="oMyEquipment"></param>
    ''' <param name="oLog"></param>
    ''' <param name="nSequence"></param>
    ''' <param name="bIsSaveImage"></param>
    ''' <param name="nDefectMaxCount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModelDiffForStandardDeviation(oCameraSourceImage As MIL_ID, oRecipe As CRecipeModelDiff, oInspectSum As CInspectSum, oProduct As CMyProduct, oMyEquipment As CMyEquipment, oLog As II_LogTraceExtend, nSequence As Integer, bIsSaveImage As Boolean, nDefectMaxCount As Integer) As Boolean
        Try
            Dim nSourceWidth As Integer = CInt(MIL.MbufInquire(oCameraSourceImage, MIL.M_SIZE_X))
            Dim nSourceHeight As Integer = CInt(MIL.MbufInquire(oCameraSourceImage, MIL.M_SIZE_Y))
            Dim oModelImageList1St As New List(Of CMyModelImage)
            Dim oModelImageList2Nd As New List(Of CMyModelImage)
            Dim oInspectImageFloat As MIL_ID
            Dim oStandardBrightDeviationImageFloat As MIL_ID
            Dim oStandardDarkDeviationImageFloat As MIL_ID
            Dim oStandardBrightDeviationImageUnsigned As MIL_ID
            Dim oStandardDarkDeviationImageUnsigned As MIL_ID
            Dim oInspectImageUnsigned As MIL_ID
            Dim oStandardUpperLimitImage As MIL_ID
            Dim oStandardLowerLimitImage As MIL_ID
            Dim oPM As New CMyPatternMatching
            Dim nLongResultCountPositive As Long = 0
            Dim nLongResultCountNegative As Long = 0

            Dim oFindModelAllTask1St As New Task(Of Boolean)(Function() FindModelAll(oCameraSourceImage, oModelImageList1St, oRecipe, oInspectSum, oProduct, oLog, PatternMatchingType.PatternMatching1St, nSequence, bIsSaveImage))
            Dim oFindModelAllTask2Nd As New Task(Of Boolean)(Function() FindModelAll(oCameraSourceImage, oModelImageList2Nd, oRecipe, oInspectSum, oProduct, oLog, PatternMatchingType.PatternMatching2Nd, nSequence, bIsSaveImage))

            Dim oTact As New CTactTimeSpan
            Call oFindModelAllTask1St.Start()
            Call oFindModelAllTask2Nd.Start()
            Call Task.WaitAll(oFindModelAllTask1St, oFindModelAllTask2Nd)

            If oFindModelAllTask1St.Status <> TaskStatus.RanToCompletion OrElse oFindModelAllTask2Nd.Status <> TaskStatus.RanToCompletion Then
                oFindModelAllTask1St = Nothing
                oFindModelAllTask2Nd = Nothing
                Return False
            End If

            If oFindModelAllTask1St.Result = False OrElse oFindModelAllTask2Nd.Result = False Then
                Call oFindModelAllTask1St.Dispose()
                Call oFindModelAllTask2Nd.Dispose()
                oFindModelAllTask1St = Nothing
                oFindModelAllTask2Nd = Nothing
                Return False
            End If

            Call oFindModelAllTask1St.Dispose()
            Call oFindModelAllTask2Nd.Dispose()
            oFindModelAllTask1St = Nothing
            oFindModelAllTask2Nd = Nothing

            Call oTact.CalSpan()
            Call oLog.LogInformation(String.Format("[{0:d4}] 樣板數量 1St：[{1}]。樣板數量 2Nd：[{2}]。[{3:f4}]ms", nSequence, oModelImageList1St.Count, oModelImageList2Nd.Count, oTact.CurrentSpan))
            oTact.ReSetTime()

            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 32 + MIL.M_FLOAT, MIL.M_IMAGE + MIL.M_PROC, oInspectImageFloat)
            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oInspectImageUnsigned)
            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 32 + MIL.M_FLOAT, MIL.M_IMAGE + MIL.M_PROC, oStandardBrightDeviationImageFloat)
            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 32 + MIL.M_FLOAT, MIL.M_IMAGE + MIL.M_PROC, oStandardDarkDeviationImageFloat)
            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oStandardBrightDeviationImageUnsigned)
            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oStandardDarkDeviationImageUnsigned)
            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oStandardUpperLimitImage)
            MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oStandardLowerLimitImage)

            MIL.MimArith(oRecipe.SummationID, MIL.M_NULL, oInspectImageFloat, MIL.M_SQUARE)
            MIL.MimArith(oInspectImageFloat, oRecipe.SummationSquareCount, oInspectImageFloat, MIL.M_DIV_CONST)
            MIL.MimArith(oRecipe.SummationSquareID, oInspectImageFloat, oInspectImageFloat, MIL.M_SUB)
            MIL.MimArith(oInspectImageFloat, oRecipe.SummationSquareCount, oInspectImageFloat, MIL.M_DIV_CONST)
            MIL.MimArith(oInspectImageFloat, MIL.M_NULL, oInspectImageFloat, MIL.M_SQUARE_ROOT)
            If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I10SquareRoot.BMP", oInspectSum.InspectResult.InspectPath), oInspectImageFloat)
            MIL.MimArith(oInspectImageFloat, oRecipe.BrightStandardDeviation, oStandardBrightDeviationImageFloat, MIL.M_MULT_CONST)
            MIL.MimArith(oInspectImageFloat, oRecipe.DarkStandardDeviation, oStandardDarkDeviationImageFloat, MIL.M_MULT_CONST)
            MIL.MbufClear(oStandardBrightDeviationImageUnsigned, 0)
            MIL.MimArith(oStandardBrightDeviationImageUnsigned, oStandardBrightDeviationImageFloat, oStandardBrightDeviationImageUnsigned, MIL.M_ADD + MIL.M_SATURATION)
            MIL.MbufClear(oStandardDarkDeviationImageUnsigned, 0)
            MIL.MimArith(oStandardDarkDeviationImageUnsigned, oStandardDarkDeviationImageFloat, oStandardDarkDeviationImageUnsigned, MIL.M_ADD + MIL.M_SATURATION)
            MIL.MimArith(oStandardBrightDeviationImageUnsigned, oRecipe.BaseBrightThreshold, oStandardBrightDeviationImageUnsigned, MIL.M_SUB_CONST + MIL.M_SATURATION)
            MIL.MimArith(oStandardBrightDeviationImageUnsigned, oRecipe.BaseBrightThreshold, oStandardBrightDeviationImageUnsigned, MIL.M_ADD_CONST + MIL.M_SATURATION)
            If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I23BaseBrightStandardDeviation.BMP", oInspectSum.InspectResult.InspectPath), oStandardBrightDeviationImageUnsigned)
            MIL.MimArith(oStandardDarkDeviationImageUnsigned, oRecipe.BaseDarkThreshold, oStandardDarkDeviationImageUnsigned, MIL.M_SUB_CONST + MIL.M_SATURATION)
            MIL.MimArith(oStandardDarkDeviationImageUnsigned, oRecipe.BaseDarkThreshold, oStandardDarkDeviationImageUnsigned, MIL.M_ADD_CONST + MIL.M_SATURATION)
            If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I24BaseDarkStandardDeviation.BMP", oInspectSum.InspectResult.InspectPath), oStandardDarkDeviationImageUnsigned)

            If oRecipe.SummationSquareCount = 0 Then
                MIL.MbufCopy(oRecipe.TemplateID1St, oInspectImageUnsigned)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I25AverageInteger.BMP", oInspectSum.InspectResult.InspectPath), oInspectImageUnsigned)
            Else
                MIL.MimArith(oRecipe.SummationID, oRecipe.SummationSquareCount, oInspectImageFloat, MIL.M_DIV_CONST)
                MIL.MbufClear(oInspectImageUnsigned, 0)
                MIL.MimArith(oInspectImageUnsigned, oInspectImageFloat, oInspectImageUnsigned, MIL.M_ADD + MIL.M_SATURATION)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I25AverageInteger.BMP", oInspectSum.InspectResult.InspectPath), oInspectImageUnsigned)
            End If

            MIL.MimArith(oInspectImageUnsigned, oStandardBrightDeviationImageUnsigned, oStandardUpperLimitImage, MIL.M_ADD + MIL.M_SATURATION)
            If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I26StandardUpperLimit.BMP", oInspectSum.InspectResult.InspectPath), oStandardUpperLimitImage)
            MIL.MimArith(oInspectImageUnsigned, oStandardDarkDeviationImageUnsigned, oStandardLowerLimitImage, MIL.M_SUB + MIL.M_SATURATION)
            If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I27StandardLowerLimit.BMP", oInspectSum.InspectResult.InspectPath), oStandardLowerLimitImage)

            MIL.MbufFree(oInspectImageFloat)
            MIL.MbufFree(oStandardBrightDeviationImageFloat)
            MIL.MbufFree(oStandardDarkDeviationImageFloat)
            MIL.MbufFree(oStandardBrightDeviationImageUnsigned)
            MIL.MbufFree(oStandardDarkDeviationImageUnsigned)
            MIL.MbufFree(oInspectImageUnsigned)
            oInspectImageFloat = MIL.M_NULL
            oStandardBrightDeviationImageFloat = MIL.M_NULL
            oStandardDarkDeviationImageFloat = MIL.M_NULL
            oStandardBrightDeviationImageUnsigned = MIL.M_NULL
            oStandardDarkDeviationImageUnsigned = MIL.M_NULL
            oInspectImageUnsigned = MIL.M_NULL

            Call oTact.CalSpan()
            Call oLog.LogInformation(String.Format("[{0:d4}] 標準差計算完畢！[{1:f4}]ms", nSequence, oTact.CurrentSpan))
            oTact.ReSetTime()

            Dim bIsIndistinct As Boolean = False
            If oModelImageList1St.Count >= 0 Then bIsIndistinct = oModelImageList2Nd.Count < (oModelImageList1St.Count \ 2)

            '標準差(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            oInspectSum.InspectResult.ModleInspectStatus = False
            Parallel.ForEach(oModelImageList1St, Sub(o)
                                                     If StandardDeviation(o, oModelImageList2Nd, oRecipe, oInspectSum, oProduct, oStandardUpperLimitImage, oStandardLowerLimitImage, oMyEquipment, bIsSaveImage, bIsIndistinct, nDefectMaxCount, oLog, nSequence) = False _
                                                         AndAlso oInspectSum.InspectResult.ModleInspectStatus = False Then
                                                         oInspectSum.InspectResult.ModleInspectStatus = True '樣板異常/檢測異常 (樣板)-異常:True
                                                     End If
                                                 End Sub)
            '標準差(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            Call oTact.CalSpan()
            Call oLog.LogInformation(String.Format("[{0:d4}] 標準差檢測完畢！[{1:f4}]ms", nSequence, oTact.CurrentSpan))
            oTact.ReSetTime()

            '漏雷(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
            Parallel.ForEach(oProduct.MarkList, Sub(o)
                                                    BuildLoseModel(oCameraSourceImage, oRecipe, oInspectSum, o, oMyEquipment, oLog, nSequence, oMyEquipment.HardwareConfig.MiscConfig.IsSaveAIOKImage)
                                                End Sub)
            '漏雷(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))

            Call oTact.CalSpan()
            Call oLog.LogInformation(String.Format("[{0:d4}] 儲存漏雷瑕疵完畢！[{1:f4}]ms", nSequence, oTact.CurrentSpan))
            Call oLog.LogInformation(String.Format("[{0:d4}] 瑕疵數量 (單元)：{1} 個", nSequence, oInspectSum.InspectResult.DefectCount))
            oTact.ReSetTime()

            Parallel.ForEach(oModelImageList1St, Sub(o)
                                                     CameraImageClear(o)
                                                 End Sub)

            Parallel.ForEach(oModelImageList2Nd, Sub(o)
                                                     CameraImageClear(o)
                                                 End Sub)

            oModelImageList1St.Clear()
            oModelImageList2Nd.Clear()

            Call oTact.CalSpan()
            Call oLog.LogInformation(String.Format("[{0:d4}] 清除樣板完畢！[{1:f4}]ms", nSequence, oTact.CurrentSpan))

            MIL.MbufFree(oStandardUpperLimitImage)
            MIL.MbufFree(oStandardLowerLimitImage)
            oStandardUpperLimitImage = MIL.M_NULL
            oStandardLowerLimitImage = MIL.M_NULL
            Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] ModelDiffForStandardDeviation Failed！Error：{1}", nSequence, ex.ToString()))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' modLibrary.ModelDiffForStandardDeviation -> modLibrary.FindModelAll
    ''' </summary>
    ''' <param name="oCameraSourceImage"></param>
    ''' <param name="oModelImageList"></param>
    ''' <param name="oRecipe"></param>
    ''' <param name="oInspectSum"></param>
    ''' <param name="oProduct"></param>
    ''' <param name="oLog"></param>
    ''' <param name="oPatternMatchingType"></param>
    ''' <param name="nSequence"></param>
    ''' <param name="bIsSaveImage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindModelAll(oCameraSourceImage As MIL_ID, ByRef oModelImageList As List(Of CMyModelImage), oRecipe As CRecipeModelDiff, oInspectSum As CInspectSum, oProduct As CMyProduct, oLog As II_LogTraceExtend, oPatternMatchingType As PatternMatchingType, nSequence As Integer, bIsSaveImage As Boolean) As Boolean
        Try
            Dim oTemporaryImage As MIL_ID
            Dim nRefX As New List(Of Double)
            Dim nRefY As New List(Of Double)
            Dim nRefAngle As New List(Of Double)
            Dim bIsOK As Boolean = True
            Dim bIsCountDisable As Boolean = False

            Select Case oPatternMatchingType
                Case PatternMatchingType.PatternMatching1St
                    bIsOK = oRecipe.PatternMatching1St.FindModelAll(oCameraSourceImage, oRecipe, nRefX, nRefY, nRefAngle)

                    If oInspectSum IsNot Nothing Then
                        oInspectSum.ModelCenterListStart1St.Clear()
                        oInspectSum.ModelCenterListEnd1St.Clear()
                    End If

                Case PatternMatchingType.PatternMatching2Nd
                    bIsOK = oRecipe.PatternMatching2Nd.FindModelAll(oCameraSourceImage, oRecipe, nRefX, nRefY, nRefAngle)

                    If oInspectSum IsNot Nothing Then
                        oInspectSum.ModelCenterListStart2Nd.Clear()
                        oInspectSum.ModelCenterListEnd2Nd.Clear()
                    End If
            End Select

            If oPatternMatchingType = PatternMatchingType.PatternMatching1St AndAlso oRecipe.SummationSquareCount < 100 Then
                oRecipe.IsGatherStandardDeviation = True
                bIsCountDisable = True
            End If

            '-------------------------20230828-開始--------------------------
            For nIndex As Integer = 0 To nRefX.Count - 1
                Dim oModelImage As New CMyModelImage
                oModelImage.CenterX = CInt(Math.Round(nRefX.Item(nIndex) + oRecipe.SearchRange.X))
                oModelImage.CenterY = CInt(Math.Round(nRefY.Item(nIndex) + oRecipe.SearchRange.Y))
                oModelImage.PositionX = CInt(Math.Round(oModelImage.CenterX - oRecipe.ModelSize.Width / 2))
                oModelImage.PositionY = CInt(Math.Round(oModelImage.CenterY - oRecipe.ModelSize.Height / 2))
                oModelImage.PositionAngle = If(nRefAngle.Item(nIndex) < 180, nRefAngle.Item(nIndex), 360 - nRefAngle.Item(nIndex))

                If oPatternMatchingType = PatternMatchingType.PatternMatching1St Then
                    MIL.MbufChild2d(oCameraSourceImage, oModelImage.PositionX, oModelImage.PositionY, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, oModelImage.ModelImage)
                End If
                oModelImageList.Add(oModelImage) 'List(Of CMyModelImage) [oModelImageList] 增加 CMyModelImage [oModelImage]

                If oPatternMatchingType = PatternMatchingType.PatternMatching1St Then
                    If oRecipe.IsGatherStandardDeviation = True AndAlso bIsOK = True Then
                        MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 32 + MIL.M_FLOAT, MIL.M_IMAGE + MIL.M_PROC, oTemporaryImage)

                        oRecipe.SummationSquareCount += 1
                        MIL.MimArith(oRecipe.SummationID, oModelImage.ModelImage, oRecipe.SummationID, MIL.M_ADD)
                        MIL.MimArith(oModelImage.ModelImage, MIL.M_NULL, oTemporaryImage, MIL.M_SQUARE)
                        MIL.MimArith(oRecipe.SummationSquareID, oTemporaryImage, oRecipe.SummationSquareID, MIL.M_ADD)
                        MIL.MbufFree(oTemporaryImage)
                        oTemporaryImage = MIL.M_NULL

                        If oRecipe.SummationSquareCount >= 20000 Then
                            oRecipe.IsGatherStandardDeviation = False
                        End If
                    End If

                    'moLog.LogInformation(String.Format("[{0:d4}] [Model {1}]：RefX = {2}, RefY = {3}", nSequence, (nRecipeIndex + 1), nRefX.Item(nIndex), nRefY.Item(nIndex)))
                End If
            Next
            '-------------------------20230828-結束--------------------------

            If oPatternMatchingType = PatternMatchingType.PatternMatching1St Then
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I01Summation.BMP", oInspectSum.InspectResult.InspectPath), oRecipe.SummationID)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I03SummationSquare.BMP", oInspectSum.InspectResult.InspectPath), oRecipe.SummationSquareID)
            End If

            If oPatternMatchingType = PatternMatchingType.PatternMatching1St AndAlso bIsCountDisable = True Then oRecipe.IsGatherStandardDeviation = False

            If oProduct IsNot Nothing Then Parallel.ForEach(oModelImageList, Sub(o)
                                                                                 BuildProductPosition(o, oRecipe, oInspectSum, oProduct, oPatternMatchingType, oLog, nSequence)
                                                                             End Sub)

            Return bIsOK
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] FindModelAll Failed！Error：{1}", nSequence, ex.ToString()))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' modLibrary.FindModelAll -> modLibrary.BuildProductPosition
    ''' </summary>
    ''' <param name="oModelImage"></param>
    ''' <param name="oRecipeModelDiff"></param>
    ''' <param name="oInspectSum"></param>
    ''' <param name="oProduct"></param>
    ''' <param name="oPatternMatchingType"></param>
    ''' <param name="oLog"></param>
    ''' <param name="nSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BuildProductPosition(ByRef oModelImage As CMyModelImage, oRecipeModelDiff As CRecipeModelDiff, oInspectSum As CInspectSum, oProduct As CMyProduct, oPatternMatchingType As PatternMatchingType, oLog As II_LogTraceExtend, nSequence As Integer) As Boolean
        Try
            Dim nPositionX As Integer = oModelImage.PositionX
            Dim nPositionY As Integer = oModelImage.PositionY
            Dim nPositionAngle As Double = oModelImage.PositionAngle

            Dim ModelCenterLine1StStart As Point = New Point(oModelImage.CenterX - 10, oModelImage.CenterY - 10)
            Dim ModelCenterLine1StEnd As Point = New Point(oModelImage.CenterX + 10, oModelImage.CenterY + 10)
            Dim ModelCenterLine2NdStart As Point = New Point(oModelImage.CenterX + 10, oModelImage.CenterY - 10)
            Dim ModelCenterLine2NdEnd As Point = New Point(oModelImage.CenterX - 10, oModelImage.CenterY + 10)

            Select Case oPatternMatchingType
                Case PatternMatchingType.PatternMatching1St
                    SyncLock CAutoRunThread.ProcessModelCenter1StListLock
                        oInspectSum.ModelCenterListStart1St.Add(ModelCenterLine1StStart)
                        oInspectSum.ModelCenterListEnd1St.Add(ModelCenterLine1StEnd)
                        oInspectSum.ModelCenterListStart1St.Add(ModelCenterLine2NdStart)
                        oInspectSum.ModelCenterListEnd1St.Add(ModelCenterLine2NdEnd)
                    End SyncLock

                Case PatternMatchingType.PatternMatching2Nd
                    SyncLock CAutoRunThread.ProcessModelCenter2NdListLock
                        oInspectSum.ModelCenterListStart2Nd.Add(ModelCenterLine1StStart)
                        oInspectSum.ModelCenterListEnd2Nd.Add(ModelCenterLine1StEnd)
                        oInspectSum.ModelCenterListStart2Nd.Add(ModelCenterLine2NdStart)
                        oInspectSum.ModelCenterListEnd2Nd.Add(ModelCenterLine2NdEnd)
                    End SyncLock
            End Select

            Dim nOffsetMin As Integer = oRecipeModelDiff.OffsetMin
            Dim nOffsetGrayMin As Integer = oRecipeModelDiff.OffsetMin + oRecipeModelDiff.OffsetGrayMin
            Dim nLoseMin As Integer = oRecipeModelDiff.LoseMin
            Dim oRecipeMarkList As List(Of CRecipeMark) = (From o In oRecipeModelDiff.RecipeMarkList.RecipeMarkList
                                                            Where
                                                            nPositionX > o.PositionX - nLoseMin AndAlso
                                                            nPositionX < o.PositionX + nLoseMin AndAlso
                                                            nPositionY > o.PositionY - nLoseMin AndAlso
                                                            nPositionY < o.PositionY + nLoseMin AndAlso
                                                            nPositionAngle < oRecipeModelDiff.AngleMin).ToList

            If oRecipeMarkList.Count < 1 Then
                oModelImage.IsLose = True '漏雷
                oModelImage.IsProcess = False
                oModelImage.MarkX = -1
                oModelImage.MarkY = -1

                Return False
            Else
                With oRecipeMarkList.Item(0)
                    If nPositionX < .PositionX - nOffsetGrayMin OrElse nPositionX > .PositionX + nOffsetGrayMin OrElse nPositionY < .PositionY - nOffsetGrayMin OrElse nPositionY > .PositionY + nOffsetGrayMin Then
                        oModelImage.IsOffset = True
                        oModelImage.IsProcess = False
                        Dim nIndex As Integer = oProduct.MarkIndex(oRecipeMarkList.Item(0).MarkX, oRecipeMarkList.Item(0).MarkY)
                        If nIndex < 0 Then
                            oModelImage.MarkX = -1
                            oModelImage.MarkY = -1
                        Else
                            oModelImage.MarkX = oProduct.MarkList.Item(nIndex).MarkX
                            oModelImage.MarkY = oProduct.MarkList.Item(nIndex).MarkY
                        End If

                        Return False
                    End If
                    If nPositionX < .PositionX - nOffsetMin OrElse nPositionX > .PositionX + nOffsetMin OrElse nPositionY < .PositionY - nOffsetMin OrElse nPositionY > .PositionY + nOffsetMin Then
                        oModelImage.IsOffsetGray = True
                        oModelImage.IsProcess = False
                        oModelImage.MarkX = -1
                        oModelImage.MarkY = -1

                        Return False
                    End If
                End With
            End If

            If oRecipeMarkList.Item(0).MarkX < oProduct.DimensionX AndAlso oRecipeMarkList.Item(0).MarkY < oProduct.DimensionY Then
                Dim nIndex As Integer = oProduct.MarkIndex(oRecipeMarkList.Item(0).MarkX, oRecipeMarkList.Item(0).MarkY)
                If nIndex < 0 Then
                    oModelImage.IsProcess = False
                    oModelImage.MarkX = -1
                    oModelImage.MarkY = -1
                Else
                    'oModelImage.IsProcess = If(bIsBypass = True, True, oProduct.MarkList.Item(nIndex).IsProcess)
                    oModelImage.IsProcess = True
                    'oModelImage.IsPass = Not oModelImage.IsProcess
                    oModelImage.MarkX = oProduct.MarkList.Item(nIndex).MarkX
                    oModelImage.MarkY = oProduct.MarkList.Item(nIndex).MarkY
                End If
            Else
                oModelImage.IsProcess = False
                oModelImage.MarkX = -1
                oModelImage.MarkY = -1
                Return True
            End If
            Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] BuildProductPosition Failed！Error：{1}", nSequence, ex.ToString()))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oModelImage"></param>
    ''' <param name="oModelImageList2Nd"></param>
    ''' <param name="oRecipe"></param>
    ''' <param name="oInspectSum"></param>
    ''' <param name="oProduct"></param>
    ''' <param name="oStandardUpperLimitImage"></param>
    ''' <param name="oStandardLowerLimitImage"></param>
    ''' <param name="oMyEquipment"></param>
    ''' <param name="bIsSaveImage"></param>
    ''' <param name="bIsIndistinct"></param>
    ''' <param name="nDefectMaxCount"></param>
    ''' <param name="oLog"></param>
    ''' <param name="nSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function StandardDeviation(oModelImage As CMyModelImage, oModelImageList2Nd As List(Of CMyModelImage), oRecipe As CRecipeModelDiff, ByRef oInspectSum As CInspectSum, ByRef oProduct As CMyProduct, oStandardUpperLimitImage As MIL_ID, oStandardLowerLimitImage As MIL_ID, oMyEquipment As CMyEquipment, bIsSaveImage As Boolean, bIsIndistinct As Boolean, nDefectMaxCount As Integer, oLog As II_LogTraceExtend, nSequence As Integer) As Boolean
        Try
            With oInspectSum
                Dim oModelImagePosition As Rectangle = New Rectangle(oModelImage.PositionX, oModelImage.PositionY, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height)
                SyncLock CAutoRunThread.ProcessDefectListLock
                    .DefectList.ModelList.Add(oModelImagePosition)
                End SyncLock
                Dim nIndex As Integer = 0

                Dim sResult As String = ""
                If oModelImage.IsProcess = False Then
                    nIndex = oRecipe.MarkIndex(oModelImage.MarkX, oModelImage.MarkY)
                    If nIndex < 0 Then
                        Call oLog.LogError(String.Format("[{0:d4}] Mark Index Failed！(Recipe)", nSequence))
                        Return False
                    End If

                    If oModelImage.IsOffset = True Then
                        If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.OK
                            Return True
                        End If

                        oProduct.MarkList.Item(nIndex).Result = ResultType.Offset
                        sResult = "NG"
                        Dim oDefect As New CMyDefect
                        oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        oDefect.InspectType = InspectType.ModelDiff
                        oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                        oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result
                        oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result)
                        oDefect.MeanGray = 0
                        oDefect.BodyArea = oRecipe.ModelSize.Width * oRecipe.ModelSize.Height
                        oDefect.DefectArea = CInt(oDefect.BodyArea * oMyEquipment.HardwareConfig.CameraConfig.PixelSize)
                        oDefect.DefectBoundary.Top = oModelImage.PositionY
                        oDefect.DefectBoundary.Left = oModelImage.PositionX
                        oDefect.DefectBoundary.Bottom = oRecipe.ModelSize.Height + oModelImage.PositionY
                        oDefect.DefectBoundary.Right = oRecipe.ModelSize.Width + oModelImage.PositionX
                        oDefect.DefectBoundary.Width = oRecipe.ModelSize.Width
                        oDefect.DefectBoundary.Height = oRecipe.ModelSize.Height
                        oDefect.DefectSize = New CITVPointWapper(oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height)
                        oDefect.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterY))
                        oDefect.DefectCenter = New CITVPointWapper(CInt((oRecipe.ModelSize.Width \ 2) + oModelImage.PositionX), CInt((oRecipe.ModelSize.Height \ 2) + oModelImage.PositionY))
                        oDefect.DefectCoordinate = New CITVPointWapper(oModelImage.MarkX, oModelImage.MarkY)  '' Augustin 220726 Add for Wafer Map
                        oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1)

                        oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                     oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID, .ProductConfig.EQPID,
                                                                     oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                     oInspectSum.ReceiveTime, sResult)

                        oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                        If .InspectResult.ModleOffsetStatus = False Then .InspectResult.ModleOffsetStatus = True
                        If oProduct.MarkList.Item(nIndex).OriginalType <> ResultType.NoDie Then
                            SyncLock CAutoRunThread.ProcessDefectListLock
                                .DefectList.DefectList.Add(oDefect)
                                .DefectListDraw.Add(oDefect)
                            End SyncLock
                        End If
                        MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                        Return False
                    ElseIf oModelImage.IsOffsetGray = True Then
                        If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.OK
                            Return True
                        End If
                        oProduct.MarkList.Item(nIndex).IsGray = True
                        oProduct.MarkList.Item(nIndex).Result = ResultType.Offset
                        Dim oDefect As New CMyDefect
                        oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        oDefect.InspectType = InspectType.ModelDiff
                        oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                        oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result
                        oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result)
                        oDefect.MeanGray = 0
                        oDefect.BodyArea = oRecipe.ModelSize.Width * oRecipe.ModelSize.Height
                        oDefect.DefectArea = CInt(oDefect.BodyArea * oMyEquipment.HardwareConfig.CameraConfig.PixelSize)
                        oDefect.DefectBoundary.Top = oModelImage.PositionY
                        oDefect.DefectBoundary.Left = oModelImage.PositionX
                        oDefect.DefectBoundary.Bottom = oRecipe.ModelSize.Height + oModelImage.PositionY
                        oDefect.DefectBoundary.Right = oRecipe.ModelSize.Width + oModelImage.PositionX
                        oDefect.DefectBoundary.Width = oRecipe.ModelSize.Width
                        oDefect.DefectBoundary.Height = oRecipe.ModelSize.Height
                        oDefect.DefectSize = New CITVPointWapper(oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height)
                        oDefect.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterY))
                        oDefect.DefectCenter = New CITVPointWapper(CInt((oRecipe.ModelSize.Width \ 2) + oModelImage.PositionX), CInt((oRecipe.ModelSize.Height \ 2) + oModelImage.PositionY))
                        oDefect.DefectCoordinate = New CITVPointWapper(oModelImage.MarkX, oModelImage.MarkY)  '' Augustin 220726 Add for Wafer Map
                        oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1)

                        sResult = "NG"
                        oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                     oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID, .ProductConfig.EQPID,
                                                                     oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                     oInspectSum.ReceiveTime, sResult)

                        oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                        If .InspectResult.ModleOffsetStatus = False Then .InspectResult.ModleOffsetStatus = True
                        SyncLock CAutoRunThread.ProcessDefectListLock
                            .DefectList.DefectList.Add(oDefect)
                            .DefectListDraw.Add(oDefect)
                        End SyncLock

                        MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                        Return False
                    ElseIf oModelImage.IsLose = True Then '漏雷
                        If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.OK
                            Return True
                        End If
                        oProduct.MarkList.Item(nIndex).Result = ResultType.Lose '漏雷(CMyMarkInfo)
                        Dim oDefect As New CMyDefect
                        oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        oDefect.InspectType = InspectType.ModelDiff
                        oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                        oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result
                        oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result)
                        oDefect.MeanGray = 0
                        oDefect.BodyArea = oRecipe.ModelSize.Width * oRecipe.ModelSize.Height
                        oDefect.DefectArea = CInt(oDefect.BodyArea * oMyEquipment.HardwareConfig.CameraConfig.PixelSize)
                        oDefect.DefectBoundary.Top = oModelImage.PositionY
                        oDefect.DefectBoundary.Left = oModelImage.PositionX
                        oDefect.DefectBoundary.Bottom = oRecipe.ModelSize.Height + oModelImage.PositionY
                        oDefect.DefectBoundary.Right = oRecipe.ModelSize.Width + oModelImage.PositionX
                        oDefect.DefectBoundary.Width = oRecipe.ModelSize.Width
                        oDefect.DefectBoundary.Height = oRecipe.ModelSize.Height
                        oDefect.DefectSize = New CITVPointWapper(oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height)
                        oDefect.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterY))
                        oDefect.DefectCenter = New CITVPointWapper(CInt((oRecipe.ModelSize.Width \ 2) + oModelImage.PositionX), CInt((oRecipe.ModelSize.Height \ 2) + oModelImage.PositionY))
                        oDefect.DefectCoordinate = New CITVPointWapper(oModelImage.MarkX, oModelImage.MarkY)  '' Augustin 220726 Add for Wafer Map
                        oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1)

                        sResult = "NG"
                        oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                     oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID, .ProductConfig.EQPID,
                                                                     oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                     oInspectSum.ReceiveTime, sResult)

                        oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                        If oInspectSum.InspectResult.ModleLoseStatus = False Then
                            oInspectSum.InspectResult.ModleLoseStatus = True '漏雷(CInspectResult)
                        End If

                        SyncLock CAutoRunThread.ProcessDefectListLock
                            .DefectList.DefectList.Add(oDefect)
                            .DefectListDraw.Add(oDefect)
                        End SyncLock

                        MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                        Return False
                        'ElseIf oModelImage.IsPass = True Then
                        '    oProduct.MarkList.Item(nIndex).Result = ResultType.Pass
                        '    Return True
                    Else
                        oProduct.MarkList.Item(nIndex).Result = ResultType.NA
                        Return True
                    End If
                ElseIf oModelImage.IsProcess = True Then
                    nIndex = oProduct.MarkIndex(oModelImage.MarkX, oModelImage.MarkY)
                    If nIndex < 0 Then
                        Call oLog.LogError(String.Format("[{0:d4}] Mark Index Failed！(Product)", nSequence))
                        Return False
                    End If

                    If bIsIndistinct = True Then
                        Dim oSelectModelImageList2Nd As List(Of CMyModelImage) = (From o In oModelImageList2Nd Where o.MarkX = oModelImage.MarkX AndAlso o.MarkY = oModelImage.MarkY).ToList

                        If oSelectModelImageList2Nd.Count < 1 Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.Indistinct
                            Dim oDefect As New CMyDefect
                            oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                            oDefect.InspectType = InspectType.ModelDiff
                            oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                            oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result
                            oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result)
                            oDefect.MeanGray = 0
                            oDefect.BodyArea = oRecipe.ModelSize.Width * oRecipe.ModelSize.Height
                            oDefect.DefectArea = CInt(oDefect.BodyArea * oMyEquipment.HardwareConfig.CameraConfig.PixelSize)
                            oDefect.DefectBoundary.Top = oModelImage.PositionY
                            oDefect.DefectBoundary.Left = oModelImage.PositionX
                            oDefect.DefectBoundary.Bottom = oRecipe.ModelSize.Height + oModelImage.PositionY
                            oDefect.DefectBoundary.Right = oRecipe.ModelSize.Width + oModelImage.PositionX
                            oDefect.DefectBoundary.Width = oRecipe.ModelSize.Width
                            oDefect.DefectBoundary.Height = oRecipe.ModelSize.Height
                            oDefect.DefectSize = New CITVPointWapper(oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height)
                            oDefect.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterY))
                            oDefect.DefectCenter = New CITVPointWapper(CInt((oRecipe.ModelSize.Width \ 2) + oModelImage.PositionX), CInt((oRecipe.ModelSize.Height \ 2) + oModelImage.PositionY))
                            oDefect.DefectCoordinate = New CITVPointWapper(oModelImage.MarkX, oModelImage.MarkY)  '' Augustin 220726 Add for Wafer Map
                            oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1)

                            sResult = "NG"
                            oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                         oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID, .ProductConfig.EQPID,
                                                                         oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                         oInspectSum.ReceiveTime, sResult)

                            oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                            SyncLock CAutoRunThread.ProcessDefectListLock
                                If .InspectResult.ModleOffsetStatus = False Then .InspectResult.ModleOffsetStatus = True
                                .DefectList.DefectList.Add(oDefect)
                                .DefectListDraw.Add(oDefect)
                            End SyncLock

                            MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                            Return False
                        End If
                    End If
                End If

                Dim oPositiveDeviationImage As MIL_ID
                Dim oNegativeDeviationImage As MIL_ID
                Dim oBlobPositiveFeatureList As MIL_ID
                Dim oBlobNegativeFeatureList As MIL_ID
                Dim oBlobResultPositiveResult As MIL_ID
                Dim oBlobResultNegativeResult As MIL_ID
                Dim nLongResultCountPositive As Long = 0
                Dim nLongResultCountNegative As Long = 0

                MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oPositiveDeviationImage)
                MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oNegativeDeviationImage)

                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\{1:00}{2:00}I30Camera.BMP", .InspectResult.InspectPath, oModelImage.MarkX, oModelImage.MarkY), oModelImage.ModelImage)
                MIL.MimArith(oModelImage.ModelImage, oStandardUpperLimitImage, oPositiveDeviationImage, MIL.M_SUB + MIL.M_SATURATION)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\{1:00}{2:00}I31PositiveDeviation.BMP", .InspectResult.InspectPath, oModelImage.MarkX, oModelImage.MarkY), oPositiveDeviationImage)
                MIL.MimArith(oStandardLowerLimitImage, oModelImage.ModelImage, oNegativeDeviationImage, MIL.M_SUB + MIL.M_SATURATION)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\{1:00}{2:00}I32NegativeDeviation.BMP", .InspectResult.InspectPath, oModelImage.MarkX, oModelImage.MarkY), oNegativeDeviationImage)

                MIL.MimBinarize(oPositiveDeviationImage, oPositiveDeviationImage, MIL.M_FIXED + MIL.M_GREATER_OR_EQUAL, oRecipe.BrightThreshold, MIL.M_NULL)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\{1:00}{2:00}I33PositiveThreshold.BMP", .InspectResult.InspectPath, oModelImage.MarkX, oModelImage.MarkY), oPositiveDeviationImage)
                MIL.MimBinarize(oNegativeDeviationImage, oNegativeDeviationImage, MIL.M_FIXED + MIL.M_GREATER_OR_EQUAL, oRecipe.DarkThreshold, MIL.M_NULL)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\{1:00}{2:00}I34NegativeThreshold.BMP", .InspectResult.InspectPath, oModelImage.MarkX, oModelImage.MarkY), oNegativeDeviationImage)

                MIL.MimClose(oPositiveDeviationImage, oPositiveDeviationImage, oRecipe.MergeTolerance, MIL.M_BINARY)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\{1:00}{2:00}I33PositiveClose.BMP", .InspectResult.InspectPath, oModelImage.MarkX, oModelImage.MarkY), oPositiveDeviationImage)
                MIL.MimClose(oNegativeDeviationImage, oNegativeDeviationImage, oRecipe.MergeTolerance, MIL.M_BINARY)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\{1:00}{2:00}I34NegativeClose.BMP", .InspectResult.InspectPath, oModelImage.MarkX, oModelImage.MarkY), oNegativeDeviationImage)

                MIL.MblobAllocFeatureList(MIL.M_DEFAULT_HOST, oBlobPositiveFeatureList)
                MIL.MblobAllocResult(MIL.M_DEFAULT_HOST, oBlobResultPositiveResult)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_AREA)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_CENTER_OF_GRAVITY_X)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_CENTER_OF_GRAVITY_Y)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_BOX_X_MIN)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_BOX_X_MAX)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_BOX_Y_MIN)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_BOX_Y_MAX)
                MIL.MblobSelectFeature(oBlobPositiveFeatureList, MIL.M_MEAN_PIXEL)

                MIL.MblobAllocFeatureList(MIL.M_DEFAULT_HOST, oBlobNegativeFeatureList)
                MIL.MblobAllocResult(MIL.M_DEFAULT_HOST, oBlobResultNegativeResult)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_AREA)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_CENTER_OF_GRAVITY_X)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_CENTER_OF_GRAVITY_Y)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_BOX_X_MIN)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_BOX_X_MAX)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_BOX_Y_MIN)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_BOX_Y_MAX)
                MIL.MblobSelectFeature(oBlobNegativeFeatureList, MIL.M_MEAN_PIXEL)

                MIL.MblobCalculate(oPositiveDeviationImage, oModelImage.ModelImage, oBlobPositiveFeatureList, oBlobResultPositiveResult)
                MIL.MblobCalculate(oNegativeDeviationImage, oModelImage.ModelImage, oBlobNegativeFeatureList, oBlobResultNegativeResult)

                MIL.MblobSelect(oBlobResultPositiveResult, MIL.M_DELETE, MIL.M_AREA, MIL.M_LESS, oRecipe.BrightDefectSizeMin, MIL.M_NULL)
                MIL.MblobSelect(oBlobResultNegativeResult, MIL.M_DELETE, MIL.M_AREA, MIL.M_LESS, oRecipe.DarkDefectSizeMin, MIL.M_NULL)
                MIL.MblobGetNumber(oBlobResultPositiveResult, nLongResultCountPositive)
                MIL.MblobGetNumber(oBlobResultNegativeResult, nLongResultCountNegative)

                If nLongResultCountPositive = 0 AndAlso nLongResultCountNegative = 0 Then
                    oProduct.MarkList.Item(nIndex).Result = ResultType.OK
                    MIL.MbufFree(oPositiveDeviationImage)
                    MIL.MbufFree(oNegativeDeviationImage)
                    MIL.MblobFree(oBlobPositiveFeatureList)
                    MIL.MblobFree(oBlobNegativeFeatureList)
                    MIL.MblobFree(oBlobResultPositiveResult)
                    MIL.MblobFree(oBlobResultNegativeResult)
                    oPositiveDeviationImage = MIL.M_NULL
                    oNegativeDeviationImage = MIL.M_NULL
                    oBlobPositiveFeatureList = MIL.M_NULL
                    oBlobNegativeFeatureList = MIL.M_NULL
                    oBlobResultPositiveResult = MIL.M_NULL
                    oBlobResultNegativeResult = MIL.M_NULL
                    Return True
                End If

                Dim nResultCountPositive As Integer = CInt(nLongResultCountPositive - 1)
                Dim AreaPositive(nResultCountPositive) As Integer
                Dim DefectLPositive(nResultCountPositive) As Integer
                Dim DefectTPositive(nResultCountPositive) As Integer
                Dim DefectRPositive(nResultCountPositive) As Integer
                Dim DefectBPositive(nResultCountPositive) As Integer
                Dim DefectCenterXPositive(nResultCountPositive) As Double
                Dim DefectCenterYPositive(nResultCountPositive) As Double
                Dim DefectMeanGrayPositive(nResultCountPositive) As Double

                Dim nResultCountNegative As Integer = CInt(nLongResultCountNegative - 1)
                Dim AreaNegative(nResultCountNegative) As Integer
                Dim DefectLNegative(nResultCountNegative) As Integer
                Dim DefectTNegative(nResultCountNegative) As Integer
                Dim DefectRNegative(nResultCountNegative) As Integer
                Dim DefectBNegative(nResultCountNegative) As Integer
                Dim DefectCenterXNegative(nResultCountNegative) As Double
                Dim DefectCenterYNegative(nResultCountNegative) As Double
                Dim DefectMeanGrayNegative(nResultCountNegative) As Double

                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_AREA + MIL.M_BINARY + MIL.M_TYPE_LONG, AreaPositive)
                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_BOX_X_MIN + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectLPositive)
                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_BOX_Y_MIN + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectTPositive)
                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_BOX_X_MAX + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectRPositive)
                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_BOX_Y_MAX + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectBPositive)
                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_CENTER_OF_GRAVITY_X + MIL.M_BINARY + MIL.M_TYPE_DOUBLE, DefectCenterXPositive)
                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_CENTER_OF_GRAVITY_Y + MIL.M_BINARY + MIL.M_TYPE_DOUBLE, DefectCenterYPositive)
                MIL.MblobGetResult(oBlobResultPositiveResult, MIL.M_MEAN_PIXEL + MIL.M_TYPE_DOUBLE, DefectMeanGrayPositive)

                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_AREA + MIL.M_BINARY + MIL.M_TYPE_LONG, AreaNegative)
                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_BOX_X_MIN + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectLNegative)
                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_BOX_Y_MIN + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectTNegative)
                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_BOX_X_MAX + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectRNegative)
                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_BOX_Y_MAX + MIL.M_BINARY + MIL.M_TYPE_LONG, DefectBNegative)
                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_CENTER_OF_GRAVITY_X + MIL.M_BINARY + MIL.M_TYPE_DOUBLE, DefectCenterXNegative)
                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_CENTER_OF_GRAVITY_Y + MIL.M_BINARY + MIL.M_TYPE_DOUBLE, DefectCenterYNegative)
                MIL.MblobGetResult(oBlobResultNegativeResult, MIL.M_MEAN_PIXEL + MIL.M_TYPE_DOUBLE, DefectMeanGrayNegative)

                Dim sImageName As String = ""
                Dim bIsAddDefect As Boolean = False
                Dim oDefectIndex As Integer = 0
                Dim bIsGray As Boolean = oProduct.MarkList.Item(nIndex).IsGray
                Dim oResult As ResultType = oProduct.MarkList.Item(nIndex).Result

                Parallel.For(0, nResultCountPositive + 1, Sub(nIndexPositive As Integer)
                                                              'For nIndexPositive As Integer = 0 To nResultCountPositive
                                                              Dim oDefectImage As MIL_ID
                                                              Dim oDefectResult As MIL_ID
                                                              Dim oDefectType As ResultType = ResultType.NA
                                                              Dim nDefectMean As Integer = 100
                                                              Dim nTop As Integer = DefectTPositive(nIndexPositive) + oModelImage.PositionY
                                                              Dim nLeft As Integer = DefectLPositive(nIndexPositive) + oModelImage.PositionX
                                                              Dim nBottom As Integer = DefectBPositive(nIndexPositive) + oModelImage.PositionY
                                                              Dim nRight As Integer = DefectRPositive(nIndexPositive) + oModelImage.PositionX
                                                              Dim nWidth As Integer = DefectRPositive(nIndexPositive) - DefectLPositive(nIndexPositive) + 1
                                                              Dim nHeight As Integer = DefectBPositive(nIndexPositive) - DefectTPositive(nIndexPositive) + 1

                                                              MIL.MimAllocResult(MIL.M_DEFAULT_HOST, 1, MIL.M_STAT_LIST, oDefectResult)
                                                              MIL.MbufChild2d(oRecipe.TemplateID1St, DefectLPositive(nIndexPositive), DefectTPositive(nIndexPositive), nWidth, nHeight, oDefectImage)
                                                              MIL.MimStat(oDefectImage, oDefectResult, MIL.M_MEAN, MIL.M_NULL, MIL.M_NULL, MIL.M_NULL)
                                                              MIL.MimGetResult(oDefectResult, MIL.M_MEAN + MIL.M_TYPE_LONG, nDefectMean)
                                                              MIL.MimFree(oDefectResult)
                                                              MIL.MbufFree(oDefectImage)
                                                              oDefectResult = MIL.M_NULL
                                                              oDefectImage = MIL.M_NULL

                                                              oDefectType = If(nDefectMean < oRecipe.MeanGray, ResultType.NGDark, ResultType.NGBright)

                                                              If oDefectType = ResultType.NGDark Then
                                                                  If oMyEquipment.MainRecipe.CompoundDeafetBypass = True Then Exit Sub
                                                              Else
                                                                  If oMyEquipment.MainRecipe.WordDeafetBypass = True Then Exit Sub
                                                              End If

                                                              Dim nDefectLength As Double = 1
                                                              If oMyEquipment.HardwareConfig.MiscConfig.DefectSizeType = DefectSizeType.DefectAnd Then
                                                                  nDefectLength = nWidth * nHeight
                                                              Else
                                                                  nDefectLength = Math.Pow(((nWidth * nWidth) + (nHeight * nHeight)), 0.5)
                                                              End If

                                                              If oDefectType = ResultType.NGDark Then
                                                                  '暗檢亮
                                                                  If nDefectLength < oRecipe.DarkDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.DarkDefectSizeMin + oRecipe.DarkDefectSizeGrayMin) Then bIsGray = True
                                                              Else
                                                                  '亮檢亮
                                                                  If DefectMeanGrayPositive(nIndexPositive) < (oRecipe.MeanGray * 3) Then Exit Sub
                                                                  If nDefectLength < oRecipe.BrightDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.BrightDefectSizeMin + oRecipe.BrightDefectSizeGrayMin) Then bIsGray = True
                                                              End If

                                                              If oResult = ResultType.NA Then
                                                                  If oDefectType = ResultType.NGDark Then
                                                                      oResult = ResultType.NGDark
                                                                  Else
                                                                      oResult = ResultType.NGBright
                                                                  End If
                                                              End If

                                                              If .InspectResult.ModleInspectStatus = False Then
                                                                  .InspectResult.ModleInspectStatus = True '樣板異常/檢測異常 (樣板)-異常:True
                                                              End If
                                                              Dim oDefect As New CMyDefect
                                                              oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                                                              oDefect.InspectType = InspectType.ModelDiff
                                                              oDefect.DefectType = If(oDefectType = ResultType.NGDark, Comp_InsperrorType.Comp_Dark, Comp_InsperrorType.Comp_Bright)
                                                              oDefect.ResultType = If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright)
                                                              oDefect.DefectName = EnumHelper.GetDescription(If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright))
                                                              oDefect.MeanGray = nDefectMean
                                                              oDefect.BodyArea = AreaPositive(nIndexPositive)
                                                              oDefect.DefectArea = CInt(oDefect.BodyArea * oMyEquipment.HardwareConfig.CameraConfig.PixelSize)
                                                              oDefect.DefectBoundary.Top = nTop
                                                              oDefect.DefectBoundary.Left = nLeft
                                                              oDefect.DefectBoundary.Bottom = nBottom
                                                              oDefect.DefectBoundary.Right = nRight
                                                              oDefect.DefectBoundary.Width = nWidth
                                                              oDefect.DefectBoundary.Height = nHeight
                                                              oDefect.DefectSize = New CITVPointWapper(oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height)
                                                              oDefect.DefectPosition = New CITVPointWapper(CInt(DefectCenterXPositive(nIndexPositive) + oModelImage.PositionX), CInt(DefectCenterYPositive(nIndexPositive) + oModelImage.PositionY))
                                                              oDefect.DefectCenter = New CITVPointWapper(CInt(DefectCenterXPositive(nIndexPositive) + oModelImage.PositionX), CInt(DefectCenterYPositive(nIndexPositive) + oModelImage.PositionY))
                                                              oDefect.DefectCoordinate = New CITVPointWapper(oModelImage.MarkX, oModelImage.MarkY)  '' Augustin 220726 Add for Wafer Map
                                                              oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1)

                                                              sResult = "NG"
                                                              oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                                                           oMyEquipment.MainRecipe.RecipeID, .InspectResult.CodeID, .ProductConfig.EQPID,
                                                                                                           oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                                                           .ReceiveTime, sResult)

                                                              oDefect.DefectFileName = String.Format("{0}\{1}", .InspectResult.InspectPath, oDefect.DefectImage.FileName)

                                                              sImageName = oDefect.DefectFileName
                                                              SyncLock CAutoRunThread.ProcessDefectListLock
                                                                  .DefectListDraw.Add(oDefect)
                                                                  oDefectIndex = .DefectListDraw.Count - 1
                                                              End SyncLock
                                                              bIsAddDefect = True
                                                              'Next
                                                          End Sub)

                'If bIsAddDefect = False Then
                Parallel.For(0, nResultCountNegative + 1, Sub(nIndexNegative As Integer)
                                                              'For nIndexNegative As Integer = 0 To nResultCountNegative
                                                              Dim oDefectImage As MIL_ID
                                                              Dim oDefectResult As MIL_ID
                                                              Dim oDefectType As ResultType = ResultType.NA
                                                              Dim nDefectMean As Integer = 100
                                                              Dim nTop As Integer = DefectTNegative(nIndexNegative) + oModelImage.PositionY
                                                              Dim nLeft As Integer = DefectLNegative(nIndexNegative) + oModelImage.PositionX
                                                              Dim nBottom As Integer = DefectBNegative(nIndexNegative) + oModelImage.PositionY
                                                              Dim nRight As Integer = DefectRNegative(nIndexNegative) + oModelImage.PositionX
                                                              Dim nWidth As Integer = DefectRNegative(nIndexNegative) - DefectLNegative(nIndexNegative) + 1
                                                              Dim nHeight As Integer = DefectBNegative(nIndexNegative) - DefectTNegative(nIndexNegative) + 1

                                                              MIL.MimAllocResult(MIL.M_DEFAULT_HOST, 1, MIL.M_STAT_LIST, oDefectResult)
                                                              MIL.MbufChild2d(oRecipe.TemplateID1St, DefectLNegative(nIndexNegative), DefectTNegative(nIndexNegative), nWidth, nHeight, oDefectImage)
                                                              MIL.MimStat(oDefectImage, oDefectResult, MIL.M_MEAN, MIL.M_NULL, MIL.M_NULL, MIL.M_NULL)
                                                              MIL.MimGetResult(oDefectResult, MIL.M_MEAN + MIL.M_TYPE_LONG, nDefectMean)
                                                              MIL.MimFree(oDefectResult)
                                                              MIL.MbufFree(oDefectImage)
                                                              oDefectResult = MIL.M_NULL
                                                              oDefectImage = MIL.M_NULL

                                                              oDefectType = If(nDefectMean < oRecipe.MeanGray, ResultType.NGDark, ResultType.NGBright)

                                                              If oDefectType = ResultType.NGDark Then
                                                                  If oMyEquipment.MainRecipe.CompoundDeafetBypass = True Then Exit Sub
                                                              Else
                                                                  If oMyEquipment.MainRecipe.WordDeafetBypass = True Then Exit Sub
                                                              End If

                                                              Dim nDefectLength As Double = 1
                                                              If oMyEquipment.HardwareConfig.MiscConfig.DefectSizeType = DefectSizeType.DefectAnd Then
                                                                  nDefectLength = nWidth * nHeight
                                                              Else
                                                                  nDefectLength = Math.Pow(((nWidth * nWidth) + (nHeight * nHeight)), 0.5)
                                                              End If

                                                              If oDefectType = ResultType.NGDark Then
                                                                  '暗檢暗
                                                                  If nDefectLength < oRecipe.DarkDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.DarkDefectSizeMin + oRecipe.DarkDefectSizeGrayMin) Then bIsGray = True
                                                              Else
                                                                  '亮檢暗
                                                                  If DefectMeanGrayNegative(nIndexNegative) > (oRecipe.MeanGray - (oRecipe.MeanGray * 0.4)) Then Exit Sub
                                                                  If nDefectLength < oRecipe.BrightDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.BrightDefectSizeMin + oRecipe.BrightDefectSizeGrayMin) Then bIsGray = True
                                                              End If

                                                              If oResult = ResultType.NA Then
                                                                  If oDefectType = ResultType.NGDark Then
                                                                      oResult = ResultType.NGDark
                                                                  Else
                                                                      oResult = ResultType.NGBright
                                                                  End If
                                                              End If

                                                              If .InspectResult.ModleInspectStatus = False Then
                                                                  .InspectResult.ModleInspectStatus = True '樣板異常/檢測異常 (樣板)-異常:True
                                                              End If
                                                              Dim oDefect As New CMyDefect
                                                              oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                                                              oDefect.InspectType = InspectType.ModelDiff
                                                              oDefect.DefectType = If(oDefectType = ResultType.NGDark, Comp_InsperrorType.Comp_Dark, Comp_InsperrorType.Comp_Bright)
                                                              oDefect.ResultType = If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright)
                                                              oDefect.DefectName = EnumHelper.GetDescription(If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright))
                                                              oDefect.MeanGray = nDefectMean
                                                              oDefect.BodyArea = AreaNegative(nIndexNegative)
                                                              oDefect.DefectArea = CInt(oDefect.BodyArea * oMyEquipment.HardwareConfig.CameraConfig.PixelSize)
                                                              oDefect.DefectBoundary.Top = nTop
                                                              oDefect.DefectBoundary.Left = nLeft
                                                              oDefect.DefectBoundary.Bottom = nBottom
                                                              oDefect.DefectBoundary.Right = nRight
                                                              oDefect.DefectBoundary.Width = nWidth
                                                              oDefect.DefectBoundary.Height = nHeight
                                                              oDefect.DefectSize = New CITVPointWapper(oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height)
                                                              oDefect.DefectPosition = New CITVPointWapper(CInt(DefectCenterXNegative(nIndexNegative) + oModelImage.PositionX), CInt(DefectCenterYNegative(nIndexNegative) + oModelImage.PositionY))
                                                              oDefect.DefectCenter = New CITVPointWapper(CInt(DefectCenterXNegative(nIndexNegative) + oModelImage.PositionX), CInt(DefectCenterYNegative(nIndexNegative) + oModelImage.PositionY))
                                                              oDefect.DefectCoordinate = New CITVPointWapper(oModelImage.MarkX, oModelImage.MarkY)  '' Augustin 220726 Add for Wafer Map
                                                              oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1)

                                                              sResult = "NG"
                                                              oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                                                           oMyEquipment.MainRecipe.RecipeID, .InspectResult.CodeID, .ProductConfig.EQPID,
                                                                                                           oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                                                           .ReceiveTime, sResult)

                                                              oDefect.DefectFileName = String.Format("{0}\{1}", .InspectResult.InspectPath, oDefect.DefectImage.FileName)

                                                              sImageName = oDefect.DefectFileName
                                                              SyncLock CAutoRunThread.ProcessDefectListLock
                                                                  .DefectListDraw.Add(oDefect)
                                                                  oDefectIndex = .DefectListDraw.Count - 1
                                                              End SyncLock
                                                              bIsAddDefect = True
                                                              'Next
                                                          End Sub)
                'End If

                SyncLock CAutoRunThread.ProcessDefectListLock
                    If bIsAddDefect = True Then .DefectList.DefectList.Add(CType(.DefectListDraw.Item(oDefectIndex).Clone(), CMyDefect))
                End SyncLock

                oProduct.MarkList.Item(nIndex).IsGray = bIsGray
                oProduct.MarkList.Item(nIndex).Result = oResult

                If oProduct.MarkList.Item(nIndex).Result = ResultType.NA Then oProduct.MarkList.Item(nIndex).Result = ResultType.OK
                If sImageName <> "" AndAlso .DefectList.DefectList.Count < nDefectMaxCount Then MIL.MbufExport(sImageName, MIL.M_BMP, oModelImage.ModelImage)

                MIL.MbufFree(oPositiveDeviationImage)
                MIL.MbufFree(oNegativeDeviationImage)
                MIL.MblobFree(oBlobPositiveFeatureList)
                MIL.MblobFree(oBlobNegativeFeatureList)
                MIL.MblobFree(oBlobResultPositiveResult)
                MIL.MblobFree(oBlobResultNegativeResult)
                oPositiveDeviationImage = MIL.M_NULL
                oNegativeDeviationImage = MIL.M_NULL
                oBlobPositiveFeatureList = MIL.M_NULL
                oBlobNegativeFeatureList = MIL.M_NULL
                oBlobResultPositiveResult = MIL.M_NULL
                oBlobResultNegativeResult = MIL.M_NULL
                Return bIsAddDefect = False
            End With
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] StandardDeviation Failed！Error：{1}", nSequence, ex.ToString()))
            Return False
        End Try
    End Function

    Public Sub BuildLoseModel(oCameraSourceImage As MIL_ID, oRecipe As CRecipeModelDiff, ByRef oInspectSum As CInspectSum, ByRef oMarkInfo As CMyMarkInfo, oMyEquipment As CMyEquipment, oLog As II_LogTraceExtend, nSequence As Integer, bIsSaveAIOKImage As Boolean)
        Try
            With oMarkInfo
                Dim sResult As String = ""

                If .Result = ResultType.NGDark OrElse .Result = ResultType.NGBright OrElse .Result = ResultType.Offset OrElse .Result = ResultType.Indistinct OrElse .Result = ResultType.Lose Then
                    SyncLock CAutoRunThread.ProcessDefectListLock
                        oInspectSum.InspectResult.DefectCount += 1

                        '-------------------------瑕疵結果訊息-開始--------------------------
                        Dim defectResultMsg As String = String.Empty
                        For Each value As ResultType In [Enum].GetValues(GetType(ResultType))
                            If oMarkInfo.Result = value Then
                                defectResultMsg = frmMain.GetDescriptionText(oMarkInfo.Result)
                                Exit For
                            End If
                        Next
                        oLog.LogError(String.Format("[{0:d4}] A瑕疵:" & defectResultMsg, nSequence)) 'Log 日誌(處理 Process)
                        oLog.LogError(String.Format("[{0:d4}] DefectCount:" & oInspectSum.InspectResult.DefectCount, nSequence)) 'Log 日誌(處理 Process)
                        '-------------------------瑕疵結果訊息-結束--------------------------
                    End SyncLock
                End If

                '-------------------------If oMarkInfo.Result = ResultType.OK-開始--------------------------
                If oMarkInfo.Result = ResultType.OK Then
                    If bIsSaveAIOKImage = True Then
                        Dim oAI As New CMyDefect
                        Dim oAIModelImage As MIL_ID = 0
                        Dim nAIIndex As Integer = oRecipe.MarkIndex(.MarkX, .MarkY)

                        oAI.DefectBoundary.Width = oRecipe.ModelSize.Width
                        oAI.DefectBoundary.Height = oRecipe.ModelSize.Height
                        oAI.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionY))
                        oAI.DefectCoordinate = New CITVPointWapper(.MarkX, .MarkY)  '' Augustin 220726 Add for Wafer Map
                        oAI.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - .MarkX, .MarkY + 1)
                        oInspectSum.ReceiveTime = DateTime.Now

                        If oMarkInfo.OriginalType = ResultType.NoDie Then
                            sResult = "NoDie"
                            oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                               oInspectSum.InspectResult.AINODIEPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                               oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - .MarkX, .MarkY + 1, oInspectSum.ReceiveTime, sResult)
                        Else
                            sResult = "OK"
                            oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                               oInspectSum.InspectResult.AIOKPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                               oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - .MarkX, .MarkY + 1, oInspectSum.ReceiveTime, sResult)
                        End If

                        MIL.MbufChild2d(oCameraSourceImage, oAI.DefectPosition.X, oAI.DefectPosition.Y, oAI.DefectBoundary.Width, oAI.DefectBoundary.Height, oAIModelImage)
                        MIL.MbufExport(oAI.DefectFileName, MIL.M_BMP, oAIModelImage)
                        MIL.MbufFree(oAIModelImage)
                        oAIModelImage = MIL.M_NULL

                        SyncLock CAutoRunThread.ProcessDefectListLock
                            oInspectSum.DefectList.OKList.Add(oAI)
                        End SyncLock
                    End If

                    'Return True
                End If
                '-------------------------If oMarkInfo.Result = ResultType.OK-結束--------------------------

                Dim nIndex As Integer = oRecipe.MarkIndex(oMarkInfo.MarkX, oMarkInfo.MarkY)

                '-------------------------If oMarkInfo.Result = ResultType.NA AndAlso nIndex >= 0-開始--------------------------
                If oMarkInfo.Result = ResultType.NA AndAlso nIndex >= 0 Then
                    If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                        oMarkInfo.Result = ResultType.OK

                        If bIsSaveAIOKImage = True Then
                            Dim oAI As New CMyDefect
                            Dim oAIModelImage As MIL_ID = 0
                            Dim nAIIndex As Integer = oRecipe.MarkIndex(.MarkX, .MarkY)

                            oAI.DefectBoundary.Width = oRecipe.ModelSize.Width
                            oAI.DefectBoundary.Height = oRecipe.ModelSize.Height
                            oAI.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionY))
                            oAI.DefectCoordinate = New CITVPointWapper(.MarkX, .MarkY)  '' Augustin 220726 Add for Wafer Map
                            oAI.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - .MarkX, .MarkY + 1)
                            oInspectSum.ReceiveTime = DateTime.Now

                            If oMarkInfo.OriginalType = ResultType.NoDie Then
                                sResult = "NoDie"
                                oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                                   oInspectSum.InspectResult.AINODIEPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                                   oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - .MarkX, .MarkY + 1, oInspectSum.ReceiveTime, sResult)
                            Else
                                sResult = "OK"
                                oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                                   oInspectSum.InspectResult.AIOKPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                                   oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - .MarkX, .MarkY + 1, oInspectSum.ReceiveTime, sResult)
                            End If

                            MIL.MbufChild2d(oCameraSourceImage, oAI.DefectPosition.X, oAI.DefectPosition.Y, oAI.DefectBoundary.Width, oAI.DefectBoundary.Height, oAIModelImage)
                            MIL.MbufExport(oAI.DefectFileName, MIL.M_BMP, oAIModelImage)
                            MIL.MbufFree(oAIModelImage)
                            oAIModelImage = MIL.M_NULL

                            SyncLock CAutoRunThread.ProcessDefectListLock
                                oInspectSum.DefectList.OKList.Add(oAI)
                            End SyncLock
                        End If

                        'Return True
                    End If

                    oMarkInfo.Result = ResultType.Lose '漏雷(CMyMarkInfo)
                    SyncLock CAutoRunThread.ProcessDefectListLock
                        oInspectSum.InspectResult.DefectCount += 1

                        '-------------------------瑕疵結果訊息-開始--------------------------
                        Dim defectResultMsg As String = String.Empty
                        For Each value As ResultType In [Enum].GetValues(GetType(ResultType))
                            If oMarkInfo.Result = value Then
                                defectResultMsg = frmMain.GetDescriptionText(oMarkInfo.Result)
                                Exit For
                            End If
                        Next
                        oLog.LogError(String.Format("[{0:d4}] B瑕疵:" & defectResultMsg, nSequence)) 'Log 日誌(處理 Process)
                        oLog.LogError(String.Format("[{0:d4}] DefectCount:" & oInspectSum.InspectResult.DefectCount, nSequence)) 'Log 日誌(處理 Process)
                        '-------------------------瑕疵結果訊息-結束--------------------------
                    End SyncLock

                    Dim oDefect As New CMyDefect
                    oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                    oDefect.InspectType = InspectType.ModelDiff
                    oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                    oDefect.ResultType = oMarkInfo.Result
                    oDefect.DefectName = EnumHelper.GetDescription(.Result)
                    oDefect.MeanGray = 0
                    oDefect.BodyArea = oRecipe.ModelSize.Width * oRecipe.ModelSize.Height
                    oDefect.DefectArea = CInt(oDefect.BodyArea * oMyEquipment.HardwareConfig.CameraConfig.PixelSize)
                    oDefect.DefectBoundary.Top = CInt(oRecipe.RecipeMarkList.RecipeMarkList.Item(nIndex).PositionY)
                    oDefect.DefectBoundary.Left = CInt(oRecipe.RecipeMarkList.RecipeMarkList.Item(nIndex).PositionX)
                    oDefect.DefectBoundary.Bottom = CInt(oRecipe.ModelSize.Height + oRecipe.RecipeMarkList.RecipeMarkList.Item(nIndex).PositionY)
                    oDefect.DefectBoundary.Right = CInt(oRecipe.ModelSize.Width + oRecipe.RecipeMarkList.RecipeMarkList.Item(nIndex).PositionX)
                    oDefect.DefectBoundary.Width = oRecipe.ModelSize.Width
                    oDefect.DefectBoundary.Height = oRecipe.ModelSize.Height
                    oDefect.DefectSize = New CITVPointWapper(oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height)
                    oDefect.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionY))
                    oDefect.DefectCenter = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nIndex).PositionCenterY))
                    oDefect.DefectCoordinate = New CITVPointWapper(.MarkX, .MarkY)  '' Augustin 220726 for Wafer Map
                    oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - .MarkX, .MarkY + 1)

                    sResult = "NG"
                    oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                 oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                                 oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - .MarkX, .MarkY + 1, oInspectSum.ReceiveTime, sResult)

                    oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                    oInspectSum.InspectResult.ModleLoseStatus = True '漏雷(CInspectResult)

                    SyncLock CAutoRunThread.ProcessDefectListLock
                        oInspectSum.DefectList.DefectList.Add(oDefect)
                        oInspectSum.DefectListDraw.Add(oDefect)

                        '-------------------------瑕疵結果訊息-開始--------------------------
                        oLog.LogError(String.Format("[{0:d4}] C瑕疵:", nSequence)) 'Log 日誌(處理 Process)
                        oLog.LogError(String.Format("[{0:d4}] oInspectSum.DefectListDraw.Count:" & oInspectSum.DefectListDraw.Count, nSequence)) 'Log 日誌(處理 Process)
                        '-------------------------瑕疵結果訊息-結束--------------------------
                    End SyncLock

                    Dim oModelImage As MIL_ID = 0
                    MIL.MbufChild2d(oCameraSourceImage, oDefect.DefectPosition.X, oDefect.DefectPosition.Y, oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height, oModelImage)
                    MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage)
                    MIL.MbufFree(oModelImage)
                    oModelImage = MIL.M_NULL
                    'Return False
                Else
                    'Return True
                End If
                '-------------------------If oMarkInfo.Result = ResultType.NA AndAlso nIndex >= 0-結束--------------------------

            End With
            'Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] BuildLoseModel Failed！Error：{1}", nSequence, ex.ToString()))
            'Return False
        End Try

    End Sub

    Public Function CameraImageClear(ByRef oModelImage As CMyModelImage) As Boolean
        Try
            oModelImage.CameraImageClear()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 比較原始版本並檢查NoDie部分(CAutoRunThread.RunInspect -> modLibrary.CompareOriginalAndInspectNoDieSection)
    ''' </summary>
    ''' <param name="oInspectSum"></param>
    ''' <param name="oProduct"></param>
    ''' <param name="oLog"></param>
    ''' <param name="nDefectMaxCount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CompareOriginalAndInspectNoDieSection(ByRef oInspectSum As CInspectSum, ByRef oProduct As CMyProduct, oLog As II_LogTraceExtend, nDefectMaxCount As Integer) As Boolean
        Try
            For i_InspectSum = 0 To oInspectSum.DefectList.DefectList.Count - 1
                For i_oProduct = 0 To oProduct.MarkList.Count - 1

                    '' 10 18測試修改
                    'If (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.Y) = oProduct.MarkList(i_oProduct).MarkY AndAlso (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.X) = oProduct.MarkList(i_oProduct).MarkX Then

                    '(((((((((((((((((((((((((((((((重要區塊-開始-Begin))))))))))))))))))))))))))))))
                    If (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.Y) = oProduct.MarkList(i_oProduct).MarkY + 1 AndAlso _
                       (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.X) = (oProduct.DimensionX - oProduct.MarkList(i_oProduct).MarkX) Then

                        If oProduct.MarkList(i_oProduct).OriginalType = ResultType.NoDie Then 'No Die-標記
                            oInspectSum.DefectList.DefectList(i_InspectSum).ResultType = ResultType.NoDie '(((((((((((((((((((((((((((((((重要區塊))))))))))))))))))))))))))))))
                            oInspectSum.InspectResult.DefectNoDieCount += 1 'No Die數量(Defect)
                        End If
                    Else
                        If oProduct.MarkList(i_oProduct).OriginalType = ResultType.NoDie Then 'No Die-標記
                            If oProduct.MarkList(i_oProduct).Result <> ResultType.NoDie Then
                                oProduct.MarkList(i_oProduct).Result = ResultType.NoDie '(((((((((((((((((((((((((((((((重要區塊))))))))))))))))))))))))))))))
                                oInspectSum.InspectResult.NotDefectNoDieCount += 1 'No Die數量(NotDefect)
                            End If
                        End If
                    End If
                    '(((((((((((((((((((((((((((((((重要區塊-結束-End  ))))))))))))))))))))))))))))))
                Next
            Next
        Catch ex As Exception
            oLog.LogError(ex.ToString)
            Return False
        End Try
        Return True
    End Function

End Module