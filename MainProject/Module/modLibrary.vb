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
    ''' �зǮt���ҫ��t��(CAutoRunThread.RunInspect -> modLibrary.ModelDiffForStandardDeviation)
    ''' StandardDeviation
    ''' BuildLoseModel
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

            Dim oFindModelAllTask1St As New Task(Of Boolean)(Function() FindModelAll(oCameraSourceImage, oModelImageList1St, oRecipe, oInspectSum, oProduct, oLog, PatternMatchingType.PatternMatching1St, nSequence, bIsSaveImage)) '�\�L�|�p/�\�L��m
            Dim oFindModelAllTask2Nd As New Task(Of Boolean)(Function() FindModelAll(oCameraSourceImage, oModelImageList2Nd, oRecipe, oInspectSum, oProduct, oLog, PatternMatchingType.PatternMatching2Nd, nSequence, bIsSaveImage)) '�\�L�|�p/�\�L��m

            Dim oTact As New CTactTimeSpan
            Call oFindModelAllTask1St.Start()
            Call oFindModelAllTask2Nd.Start()
            Task.WaitAll(oFindModelAllTask1St, oFindModelAllTask2Nd) '����-FindModelAll

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
            Call oLog.LogInformation(String.Format("[{0:d4}] �˪O�ƶq 1St�G[{1}]�C�˪O�ƶq 2Nd�G[{2}]�C[{3:f4}]ms", nSequence, oModelImageList1St.Count, oModelImageList2Nd.Count, oTact.CurrentSpan))
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
            Call oLog.LogInformation(String.Format("[{0:d4}] �зǮt�p�⧹���I[{1:f4}]ms", nSequence, oTact.CurrentSpan))
            oTact.ReSetTime()

            Dim bIsIndistinct As Boolean = False '�аO-�\�L���M
            If oModelImageList1St.Count >= 0 Then bIsIndistinct = oModelImageList2Nd.Count < (oModelImageList1St.Count \ 2)

            '�зǮt(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
            oInspectSum.InspectResult.ModleInspectStatus = False
            Parallel.ForEach(oModelImageList1St, Sub(o)
                                                     If StandardDeviation(o, oModelImageList2Nd, oRecipe, oInspectSum, oProduct, oStandardUpperLimitImage, oStandardLowerLimitImage, oMyEquipment, bIsSaveImage, bIsIndistinct, nDefectMaxCount, oLog, nSequence) = False _
                                                         AndAlso oInspectSum.InspectResult.ModleInspectStatus = False Then
                                                         oInspectSum.InspectResult.ModleInspectStatus = True '�˪O���`/�˴����` (�˪O)-���`:True
                                                     End If
                                                 End Sub)
            '�зǮt(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

            Call oTact.CalSpan()
            Call oLog.LogInformation(String.Format("[{0:d4}] �зǮt�˴������I[{1:f4}]ms", nSequence, oTact.CurrentSpan))
            oTact.ReSetTime()

            '�|�p(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
            Parallel.ForEach(oProduct.MarkList, Sub(o)
                                                    BuildLoseModel(oCameraSourceImage, oRecipe, oInspectSum, o, oMyEquipment, oLog, nSequence, oMyEquipment.HardwareConfig.MiscConfig.IsSaveAIOKImage)
                                                End Sub)
            '�|�p(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

            Call oTact.CalSpan()
            Call oLog.LogInformation(String.Format("[{0:d4}] �x�s�|�p�岫�����I[{1:f4}]ms", nSequence, oTact.CurrentSpan))
            Call oLog.LogInformation(String.Format("[{0:d4}] �岫�ƶq (�椸)�G{1} ��", nSequence, oInspectSum.InspectResult.DefectCount))
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
            Call oLog.LogInformation(String.Format("[{0:d4}] �M���˪O�����I[{1:f4}]ms", nSequence, oTact.CurrentSpan))

            MIL.MbufFree(oStandardUpperLimitImage)
            MIL.MbufFree(oStandardLowerLimitImage)
            oStandardUpperLimitImage = MIL.M_NULL
            oStandardLowerLimitImage = MIL.M_NULL
            Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] ModelDiffForStandardDeviation Failed�IError�G{1}", nSequence, ex.ToString()))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' modLibrary.ModelDiffForStandardDeviation -> modLibrary.FindModelAll
    ''' �\�L�|�p/�\�L��m,�첾/����,�\�L���M
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

            '-------------------------20230828-�}�l--------------------------
            For nIndex As Integer = 0 To nRefX.Count - 1 '�ڬOnIndex
                Dim oModelImage As New CMyModelImage
                oModelImage.CenterX = CInt(Math.Round(nRefX.Item(nIndex) + oRecipe.SearchRange.X))
                oModelImage.CenterY = CInt(Math.Round(nRefY.Item(nIndex) + oRecipe.SearchRange.Y))
                oModelImage.PositionX = CInt(Math.Round(oModelImage.CenterX - oRecipe.ModelSize.Width / 2))
                oModelImage.PositionY = CInt(Math.Round(oModelImage.CenterY - oRecipe.ModelSize.Height / 2))
                oModelImage.PositionAngle = If(nRefAngle.Item(nIndex) < 180, nRefAngle.Item(nIndex), 360 - nRefAngle.Item(nIndex))

                If oPatternMatchingType = PatternMatchingType.PatternMatching1St Then
                    MIL.MbufChild2d(oCameraSourceImage, oModelImage.PositionX, oModelImage.PositionY, oRecipe.ModelSize.Width, oRecipe.ModelSize.Height, oModelImage.ModelImage)
                End If
                oModelImageList.Add(oModelImage) 'List(Of CMyModelImage) [oModelImageList] �W�[ CMyModelImage [oModelImage]

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

                    'moLog.LogInformation(String.Format("[{0:d4}] [Model {1}]�GRefX = {2}, RefY = {3}", nSequence, (nRecipeIndex + 1), nRefX.Item(nIndex), nRefY.Item(nIndex)))
                End If
            Next
            '-------------------------20230828-����--------------------------

            If oPatternMatchingType = PatternMatchingType.PatternMatching1St Then
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I01Summation.BMP", oInspectSum.InspectResult.InspectPath), oRecipe.SummationID)
                If bIsSaveImage = True Then MIL.MbufSave(String.Format("{0}\I03SummationSquare.BMP", oInspectSum.InspectResult.InspectPath), oRecipe.SummationSquareID)
            End If

            If oPatternMatchingType = PatternMatchingType.PatternMatching1St AndAlso bIsCountDisable = True Then oRecipe.IsGatherStandardDeviation = False

            If oProduct IsNot Nothing Then Parallel.ForEach(oModelImageList, Sub(o)
                                                                                 BuildProductPosition(o, oRecipe, oInspectSum, oProduct, oPatternMatchingType, oLog, nSequence) '�\�L�|�p/�\�L��m,�첾/����,�\�L���M
                                                                             End Sub)

            Return bIsOK
        Catch ex As Exception
            Call oLog.LogError(String.Format("[{0:d4}] FindModelAll Failed�IError�G{1}", nSequence, ex.ToString()))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' modLibrary.FindModelAll -> modLibrary.BuildProductPosition
    ''' �\�L�|�p/�\�L��m
    ''' �첾/����
    ''' �첾/����(�Ƕ�)
    ''' �\�L���M
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
                oModelImage.IsLose = True '�\�L�|�p/�\�L��m
                oModelImage.IsProcess = False
                oModelImage.MarkX = -1
                oModelImage.MarkY = -1

                Return False
            Else
                With oRecipeMarkList.Item(0)
                    If nPositionX < .PositionX - nOffsetGrayMin OrElse nPositionX > .PositionX + nOffsetGrayMin OrElse nPositionY < .PositionY - nOffsetGrayMin OrElse nPositionY > .PositionY + nOffsetGrayMin Then
                        oModelImage.IsOffset = True '�첾/����
                        oModelImage.IsProcess = False
                        Dim nIndex As Integer = oProduct.MarkIndex(oRecipeMarkList.Item(0).MarkX, oRecipeMarkList.Item(0).MarkY) '�ڬOnIndex
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
                        oModelImage.IsOffsetGray = True '�첾/����(�Ƕ�)
                        oModelImage.IsProcess = False
                        oModelImage.MarkX = -1
                        oModelImage.MarkY = -1

                        Return False
                    End If
                End With
            End If

            If oRecipeMarkList.Item(0).MarkX < oProduct.DimensionX AndAlso oRecipeMarkList.Item(0).MarkY < oProduct.DimensionY Then
                Dim nIndex As Integer = oProduct.MarkIndex(oRecipeMarkList.Item(0).MarkX, oRecipeMarkList.Item(0).MarkY) '�ڬOnIndex
                If nIndex < 0 Then
                    oModelImage.IsProcess = False
                    oModelImage.MarkX = -1
                    oModelImage.MarkY = -1
                Else
                    'oModelImage.IsProcess = If(bIsBypass = True, True, oProduct.MarkList.Item(nIndex).IsProcess)
                    oModelImage.IsProcess = True '�\�L���M
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
            Call oLog.LogError(String.Format("[{0:d4}] BuildProductPosition Failed�IError�G{1}", nSequence, ex.ToString()))
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
                    oInspectSum.DefectList.ModelList.Add(oModelImagePosition)
                End SyncLock
                Dim nIndex As Integer = 0 '�ڬOnIndex

                Dim sResult As String = "" '���n-���XReport���Ϥ���
                If oModelImage.IsProcess = False Then '�j�����O���o�Ӭy�{(oModelImage.IsProcess = False)
                    nIndex = oRecipe.MarkIndex(oModelImage.MarkX, oModelImage.MarkY) '�ڬOnIndex
                    If nIndex < 0 Then
                        Call oLog.LogError(String.Format("[{0:d4}] Mark Index Failed�I(Recipe)", nSequence))
                        Return False '�b���GResult�C�����-�岫:0,�˴����`(�˪O):Y,��ܬ����r��]����[oModelImage.IsProcess = False, oRecipe.MarkIndex(oModelImage.MarkX, oModelImage.MarkY) < 0]
                    End If

                    If oModelImage.IsOffset = True Then '�첾/����
                        If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.OK '�аO-OK
                            Return True
                        End If

                        oProduct.MarkList.Item(nIndex).Result = ResultType.Offset '�аO-�첾/����
                        sResult = "NG" '���n-���XReport���Ϥ���(�첾/����)

                        '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                        Dim oDefect As New CMyDefect
                        oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        oDefect.InspectType = InspectType.ModelDiff
                        oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                        oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result '�аO-���G(�첾/����)
                        oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result) '�岫�W��(�i�Ω�X����)-�첾/����
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
                                                                     oInspectSum.ReceiveTime, sResult) '�岫�I��p��(�i�Ω�X����)

                        oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                        If .InspectResult.ModleOffsetStatus = False Then .InspectResult.ModleOffsetStatus = True
                        If oProduct.MarkList.Item(nIndex).OriginalType <> ResultType.NoDie Then 'No Die-�аO
                            SyncLock CAutoRunThread.ProcessDefectListLock
                                oInspectSum.DefectList.DefectList.Add(oDefect)
                                oInspectSum.DefectListDraw.Add(oDefect)
                            End SyncLock
                        End If
                        MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                        '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

                        Return False '�b���GResult�C�����-�岫:0,�˴����`(�˪O):Y,��ܬ����r��]����[oModelImage.IsOffset = True,�첾/����]
                    ElseIf oModelImage.IsOffsetGray = True Then '�аO-�첾/����(�Ƕ�)
                        If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.OK '�аO-OK
                            Return True
                        End If
                        oProduct.MarkList.Item(nIndex).IsGray = True
                        oProduct.MarkList.Item(nIndex).Result = ResultType.Offset '�аO-�첾/����

                        '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                        Dim oDefect As New CMyDefect
                        oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        oDefect.InspectType = InspectType.ModelDiff
                        oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                        oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result '�аO-�첾/����(�Ƕ�)
                        oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result) '�岫�W��(�i�Ω�X����)-�첾/����(�Ƕ�)
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

                        sResult = "NG" '���n-���XReport���Ϥ���[�첾/����(�Ƕ�)]
                        oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                     oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID, .ProductConfig.EQPID,
                                                                     oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                     oInspectSum.ReceiveTime, sResult) '�岫�I��p��(�i�Ω�X����)

                        oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                        If .InspectResult.ModleOffsetStatus = False Then .InspectResult.ModleOffsetStatus = True
                        SyncLock CAutoRunThread.ProcessDefectListLock
                            oInspectSum.DefectList.DefectList.Add(oDefect)
                            oInspectSum.DefectListDraw.Add(oDefect)
                        End SyncLock

                        MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                        '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

                        Return False '�b���GResult�C�����-�岫:0,�˴����`(�˪O):Y,��ܬ����r��]����[oModelImage.IsOffsetGray = True,�첾/����(�Ƕ�)]
                    ElseIf oModelImage.IsLose = True Then '�\�L�|�p/�\�L��m
                        If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.OK '�аO-OK
                            Return True
                        End If

                        '-------------------------20230905-�}�l--------------------------
                        If Debugger.IsAttached = True Then
                            '�쥻���g�k
                            '++++++ oModelImage.IsLose ------> oProduct.MarkList.Item(nIndex).Result = ResultType.Lose
                            'oProduct.MarkList.Item(nIndex).Result = ResultType.Lose '�|�p(CMyMarkInfo)
                            oProduct.MarkList.Item(nIndex).Result = ResultType.DieLoseLaser1 '�|�p(CMyMarkInfo)
                        ElseIf Debugger.IsAttached = False Then
                            '���ժ��g�k
                            '++++++ oModelImage.IsLose ------> oProduct.MarkList.Item(nIndex).Result = ResultType.DieLoseLaser
                            oProduct.MarkList.Item(nIndex).Result = ResultType.DieLoseLaser1 '�|�p(CMyMarkInfo)
                        End If
                        '-------------------------20230905-����--------------------------

                        '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                        Dim oDefect As New CMyDefect
                        oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        oDefect.InspectType = InspectType.ModelDiff
                        oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                        oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result '�\�L�|�p/�\�L��m
                        oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result) '�岫�W��(�i�Ω�X����) [oModelImage.IsLose = True]
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

                        sResult = "NG" 'StandardDeviation ���n-���XReport���Ϥ���(�\�L�|�p/�\�L��m)
                        oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                     oMyEquipment.MainRecipe.RecipeID,
                                                                     oInspectSum.InspectResult.CodeID,
                                                                     oInspectSum.ProductConfig.EQPID,
                                                                     oRecipe.MarkXCount - oModelImage.MarkX,
                                                                     oModelImage.MarkY + 1,
                                                                     oInspectSum.ReceiveTime,
                                                                     sResult) '�岫�I��p��(�i�Ω�X����)

                        oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                        SyncLock CAutoRunThread.ProcessDefectListLock
                            oInspectSum.DefectList.DefectList.Add(oDefect)
                            oInspectSum.DefectListDraw.Add(oDefect) '�Ω�e��(oModelImage.IsLose = True,�\�L�|�p/�\�L��m)

                            '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                            '-------------------------20230911-�}�l--------------------------
                            'oModelImage.IsLose = True ------> oInspectSum.InspectResult.ModleLoseStatus = True
                            If oInspectSum.InspectResult.ModleLoseStatus = False Then
                                oInspectSum.InspectResult.ModleLoseStatus = True 'StandardDeviation �|�p(CInspectResult)
                            End If
                            '-------------------------20230911-����--------------------------
                            '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))
                        End SyncLock

                        MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                        '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

                        Return False '�b���GResult�C�����-�岫:0,�˴����`(�˪O):Y,��ܬ����r��]����[oModelImage.IsLose = True,�\�L�|�p/�\�L��m]
                        'ElseIf oModelImage.IsPass = True Then
                        '    oProduct.MarkList.Item(nIndex).Result = ResultType.Pass
                        '    Return True
                    Else
                        oProduct.MarkList.Item(nIndex).Result = ResultType.NA '�аO-NA
                        Return True
                    End If
                ElseIf oModelImage.IsProcess = True Then
                    nIndex = oProduct.MarkIndex(oModelImage.MarkX, oModelImage.MarkY)
                    If nIndex < 0 Then
                        Call oLog.LogError(String.Format("[{0:d4}] Mark Index Failed�I(Product)", nSequence))
                        Return False '�b���GResult�C�����-�岫:0,�˴����`(�˪O):Y,��ܬ����r��]����[oModelImage.IsProcess = True, oProduct.MarkIndex(oModelImage.MarkX, oModelImage.MarkY) < 0]
                    End If

                    If bIsIndistinct = True Then '�аO-�\�L���M
                        Dim oSelectModelImageList2Nd As List(Of CMyModelImage) = (From o In oModelImageList2Nd Where o.MarkX = oModelImage.MarkX AndAlso o.MarkY = oModelImage.MarkY).ToList

                        If oSelectModelImageList2Nd.Count < 1 Then
                            oProduct.MarkList.Item(nIndex).Result = ResultType.Indistinct '�аO-�\�L���M

                            '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                            Dim oDefect As New CMyDefect
                            oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                            oDefect.InspectType = InspectType.ModelDiff
                            oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                            oDefect.ResultType = oProduct.MarkList.Item(nIndex).Result '�аO-�\�L���M
                            oDefect.DefectName = EnumHelper.GetDescription(oProduct.MarkList.Item(nIndex).Result) '�岫�W��(�i�Ω�X����)-�\�L���M
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

                            sResult = "NG" '���n-���XReport���Ϥ���(�\�L���M)
                            oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                         oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID, .ProductConfig.EQPID,
                                                                         oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                         oInspectSum.ReceiveTime, sResult) '�岫�I��p��(�i�Ω�X����)

                            oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName)

                            SyncLock CAutoRunThread.ProcessDefectListLock
                                If .InspectResult.ModleOffsetStatus = False Then .InspectResult.ModleOffsetStatus = True
                                oInspectSum.DefectList.DefectList.Add(oDefect)
                                oInspectSum.DefectListDraw.Add(oDefect)
                            End SyncLock

                            MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oModelImage.ModelImage)
                            '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

                            Return False '�b���GResult�C�����-�岫:0,�˴����`(�˪O):Y,��ܬ����r��]����[oModelImage.IsProcess = True, bIsIndistinct = True, oSelectModelImageList2Nd.Count < 1]
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
                    oProduct.MarkList.Item(nIndex).Result = ResultType.OK '�аO-OK
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
                Dim bIsAddDefect As Boolean = False '�ˬdoInspectSum.DefectList.DefectList�O�_�w�g�[�J�F����(CMyDefect)
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

                                                              If oDefectType = ResultType.NGDark Then '���岫 (�I)
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

                                                              If oDefectType = ResultType.NGDark Then '���岫 (�I)
                                                                  '�t�˫G
                                                                  If nDefectLength < oRecipe.DarkDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.DarkDefectSizeMin + oRecipe.DarkDefectSizeGrayMin) Then bIsGray = True
                                                              Else
                                                                  '�G�˫G
                                                                  If DefectMeanGrayPositive(nIndexPositive) < (oRecipe.MeanGray * 3) Then Exit Sub
                                                                  If nDefectLength < oRecipe.BrightDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.BrightDefectSizeMin + oRecipe.BrightDefectSizeGrayMin) Then bIsGray = True
                                                              End If

                                                              If oResult = ResultType.NA Then
                                                                  If oDefectType = ResultType.NGDark Then '���岫 (�I)
                                                                      oResult = ResultType.NGDark '���岫 (�I)
                                                                  Else
                                                                      oResult = ResultType.NGBright '���岫 (�r)
                                                                  End If
                                                              End If

                                                              If .InspectResult.ModleInspectStatus = False Then
                                                                  .InspectResult.ModleInspectStatus = True '�˪O���`/�˴����` (�˪O)-���`:True
                                                              End If

                                                              '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                                                              Dim oDefect As New CMyDefect
                                                              oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                                                              oDefect.InspectType = InspectType.ModelDiff
                                                              oDefect.DefectType = If(oDefectType = ResultType.NGDark, Comp_InsperrorType.Comp_Dark, Comp_InsperrorType.Comp_Bright)
                                                              oDefect.ResultType = If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright)
                                                              oDefect.DefectName = EnumHelper.GetDescription(If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright)) '�岫�W��(�i�Ω�X����)-���岫
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

                                                              sResult = "NG" '���n-���XReport���Ϥ���[���岫 (�I)/���岫 (�r)]:nResultCountPositive
                                                              oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                                                           oMyEquipment.MainRecipe.RecipeID, .InspectResult.CodeID, .ProductConfig.EQPID,
                                                                                                           oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                                                           .ReceiveTime, sResult) '�岫�I��p��(�i�Ω�X����)

                                                              oDefect.DefectFileName = String.Format("{0}\{1}", .InspectResult.InspectPath, oDefect.DefectImage.FileName)

                                                              sImageName = oDefect.DefectFileName
                                                              SyncLock CAutoRunThread.ProcessDefectListLock
                                                                  .DefectListDraw.Add(oDefect) '��oDefect(CMyDefect)�[�J��oInspectSum.DefectListDraw
                                                                  oDefectIndex = .DefectListDraw.Count - 1 'oInspectSum.DefectListDraw���X�����ޭȧ���̫�@��
                                                              End SyncLock
                                                              bIsAddDefect = True '�ˬdoInspectSum.DefectList.DefectList�O�_�w�g�[�J�F����(CMyDefect)
                                                              'Next
                                                              '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

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

                                                              If oDefectType = ResultType.NGDark Then '���岫 (�I)
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

                                                              If oDefectType = ResultType.NGDark Then '���岫 (�I)
                                                                  '�t�˷t
                                                                  If nDefectLength < oRecipe.DarkDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.DarkDefectSizeMin + oRecipe.DarkDefectSizeGrayMin) Then bIsGray = True
                                                              Else
                                                                  '�G�˷t
                                                                  If DefectMeanGrayNegative(nIndexNegative) > (oRecipe.MeanGray - (oRecipe.MeanGray * 0.4)) Then Exit Sub
                                                                  If nDefectLength < oRecipe.BrightDefectSizeMin Then Exit Sub
                                                                  If nDefectLength < (oRecipe.BrightDefectSizeMin + oRecipe.BrightDefectSizeGrayMin) Then bIsGray = True
                                                              End If

                                                              If oResult = ResultType.NA Then
                                                                  If oDefectType = ResultType.NGDark Then '���岫 (�I)
                                                                      oResult = ResultType.NGDark '���岫 (�I)
                                                                  Else
                                                                      oResult = ResultType.NGBright '���岫 (�r)
                                                                  End If
                                                              End If

                                                              If .InspectResult.ModleInspectStatus = False Then
                                                                  .InspectResult.ModleInspectStatus = True '�˪O���`/�˴����` (�˪O)-���`:True
                                                              End If

                                                              '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                                                              Dim oDefect As New CMyDefect
                                                              oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                                                              oDefect.InspectType = InspectType.ModelDiff
                                                              oDefect.DefectType = If(oDefectType = ResultType.NGDark, Comp_InsperrorType.Comp_Dark, Comp_InsperrorType.Comp_Bright)
                                                              oDefect.ResultType = If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright)
                                                              oDefect.DefectName = EnumHelper.GetDescription(If(oDefectType = ResultType.NGDark, ResultType.NGDark, ResultType.NGBright)) '�岫�W��(�i�Ω�X����)-���岫
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

                                                              sResult = "NG" '���n-���XReport���Ϥ���[���岫 (�I)/���岫 (�r)]:nResultCountNegative
                                                              oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                                                           oMyEquipment.MainRecipe.RecipeID, .InspectResult.CodeID, .ProductConfig.EQPID,
                                                                                                           oRecipe.MarkXCount - oModelImage.MarkX, oModelImage.MarkY + 1,
                                                                                                           .ReceiveTime, sResult) '�岫�I��p��(�i�Ω�X����)

                                                              oDefect.DefectFileName = String.Format("{0}\{1}", .InspectResult.InspectPath, oDefect.DefectImage.FileName)

                                                              sImageName = oDefect.DefectFileName
                                                              SyncLock CAutoRunThread.ProcessDefectListLock
                                                                  .DefectListDraw.Add(oDefect) '��oDefect(CMyDefect)�[�J��oInspectSum.DefectListDraw
                                                                  oDefectIndex = .DefectListDraw.Count - 1 'oInspectSum.DefectListDraw���X�����ޭȧ���̫�@��
                                                              End SyncLock
                                                              bIsAddDefect = True '�ˬdoInspectSum.DefectList.DefectList�O�_�w�g�[�J�F����(CMyDefect)
                                                              'Next
                                                              '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

                                                          End Sub)
                'End If

                SyncLock CAutoRunThread.ProcessDefectListLock
                    If bIsAddDefect = True Then
                        oInspectSum.DefectList.DefectList.Add(CType(oInspectSum.DefectListDraw.Item(oDefectIndex).Clone(), CMyDefect)) '��oDefect(CMyDefect)�[�J��oInspectSum.DefectList.DefectList
                    End If
                End SyncLock

                oProduct.MarkList.Item(nIndex).IsGray = bIsGray
                oProduct.MarkList.Item(nIndex).Result = oResult '�аO-���G

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
            Call oLog.LogError(String.Format("[{0:d4}] StandardDeviation Failed�IError�G{1}", nSequence, ex.ToString()))
            Return False '�b���GResult�C�����-�岫:0,�˴����`(�˪O):Y,��ܬ����r��]����
        End Try
    End Function

    Public Sub BuildLoseModel(oCameraSourceImage As MIL_ID, oRecipe As CRecipeModelDiff, ByRef oInspectSum As CInspectSum, ByRef oMarkInfo As CMyMarkInfo, oMyEquipment As CMyEquipment, oLog As II_LogTraceExtend, nSequence As Integer, bIsSaveAIOKImage As Boolean)
        Try
            With oMarkInfo
                Dim sResult As String = ""

                '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                '-------------------------20230911-�}�l--------------------------
                'If .Result = ResultType.NGDark OrElse .Result = ResultType.NGBright OrElse .Result = ResultType.Offset OrElse .Result = ResultType.Indistinct OrElse .Result = ResultType.Lose Then
                If .Result = ResultType.NGDark OrElse .Result = ResultType.NGBright OrElse .Result = ResultType.Offset OrElse _
                   .Result = ResultType.Indistinct OrElse .Result = ResultType.DieLoseLaser1 Then '�P�_����1
                    '-------------------------20230911-����--------------------------
                    '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))

                    SyncLock CAutoRunThread.ProcessDefectListLock
                        oInspectSum.InspectResult.DefectCount += 1 '((((((((((((((((((((((((((((((( ���n�϶� ))))))))))))))))))))))))))))))

                        '-------------------------�岫���G�T��-�}�l--------------------------
                        Dim defectResultMsg As String = String.Empty
                        For Each value As ResultType In [Enum].GetValues(GetType(ResultType))
                            If oMarkInfo.Result = value Then
                                defectResultMsg = frmMain.GetDescriptionText(oMarkInfo.Result)
                                Exit For
                            End If
                        Next
                        oLog.LogError(String.Format("[{0:d4}] A�岫:" & defectResultMsg, nSequence)) 'Log ��x(�B�z Process)
                        Dim stTrace1 As StackTrace = New StackTrace(fNeedFileInfo:=True)
                        Dim stFrame1 As StackFrame = stTrace1.GetFrames(0)
                        Dim fileName1 As String = stFrame1.GetFileName
                        Dim fileLineNum1 As Integer = stFrame1.GetFileLineNumber
                        Dim fileColNum1 As Integer = stFrame1.GetFileColumnNumber
                        Dim fileMethodName1 As String = stFrame1.GetMethod().Name
                        oLog.LogError("FileName:" & fileName1)
                        oLog.LogError("FileLineNumber:" & fileLineNum1)
                        oLog.LogError("FileColumnNumber:" & fileColNum1)
                        oLog.LogError("MethodName:" & fileMethodName1)
                        oLog.LogError(String.Format("[{0:d4}] DefectCount:" & oInspectSum.InspectResult.DefectCount, nSequence)) 'Log ��x(�B�z Process)
                        '-------------------------�岫���G�T��-����--------------------------
                    End SyncLock
                End If

                '-------------------------If oMarkInfo.Result = ResultType.OK-�}�l--------------------------
                If oMarkInfo.Result = ResultType.OK Then '�P�_����2
                    If bIsSaveAIOKImage = True Then
                        '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                        Dim oAI As New CMyDefect
                        Dim oAIModelImage As MIL_ID = 0
                        Dim nAIIndex As Integer = oRecipe.MarkIndex(oMarkInfo.MarkX, oMarkInfo.MarkY)

                        oAI.DefectBoundary.Width = oRecipe.ModelSize.Width
                        oAI.DefectBoundary.Height = oRecipe.ModelSize.Height
                        oAI.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionY))
                        oAI.DefectCoordinate = New CITVPointWapper(oMarkInfo.MarkX, oMarkInfo.MarkY)  '' Augustin 220726 Add for Wafer Map
                        oAI.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oMarkInfo.MarkX, oMarkInfo.MarkY + 1)
                        oInspectSum.ReceiveTime = DateTime.Now

                        If oMarkInfo.OriginalType = ResultType.NoDie Then 'No Die-�аO (���n�P�_����)
                            sResult = "NoDie"
                            oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                               oInspectSum.InspectResult.AINODIEPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                               oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - oMarkInfo.MarkX, oMarkInfo.MarkY + 1, oInspectSum.ReceiveTime, sResult)
                        Else
                            sResult = "OK"
                            oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                               oInspectSum.InspectResult.AIOKPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                               oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - oMarkInfo.MarkX, oMarkInfo.MarkY + 1, oInspectSum.ReceiveTime, sResult)
                        End If

                        MIL.MbufChild2d(oCameraSourceImage, oAI.DefectPosition.X, oAI.DefectPosition.Y, oAI.DefectBoundary.Width, oAI.DefectBoundary.Height, oAIModelImage)
                        MIL.MbufExport(oAI.DefectFileName, MIL.M_BMP, oAIModelImage)
                        MIL.MbufFree(oAIModelImage)
                        oAIModelImage = MIL.M_NULL

                        SyncLock CAutoRunThread.ProcessDefectListLock
                            oInspectSum.DefectList.OKList.Add(oAI)
                        End SyncLock
                        '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))
                    End If

                    'Return True
                End If
                '-------------------------If oMarkInfo.Result = ResultType.OK-����--------------------------


                '************************ 20230919-�����ϬM�{����ڴ��պ|�p�MNoDie���|�����H�U���{���϶�-�}�l ************************

                Dim nIndex As Integer = oRecipe.MarkIndex(oMarkInfo.MarkX, oMarkInfo.MarkY) '�ڬOnIndex

                '-------------------------If oMarkInfo.Result = ResultType.NA AndAlso nIndex >= 0-�}�l--------------------------
                If oMarkInfo.Result = ResultType.NA AndAlso nIndex >= 0 Then '�P�_����3
                    Dim oAIModelImage As MIL_ID = 0

                    If oMyEquipment.MainRecipe.PositionDeafetBypass = True Then
                        oMarkInfo.Result = ResultType.OK

                        If bIsSaveAIOKImage = True Then
                            '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                            Dim oAI As New CMyDefect
                            Dim nAIIndex As Integer = oRecipe.MarkIndex(oMarkInfo.MarkX, oMarkInfo.MarkY)

                            oAI.DefectBoundary.Width = oRecipe.ModelSize.Width
                            oAI.DefectBoundary.Height = oRecipe.ModelSize.Height
                            oAI.DefectPosition = New CITVPointWapper(CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionX), CInt(oRecipe.RecipeMarkList.RecipeMarkList(nAIIndex).PositionY))
                            oAI.DefectCoordinate = New CITVPointWapper(oMarkInfo.MarkX, oMarkInfo.MarkY)  '' Augustin 220726 Add for Wafer Map
                            oAI.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oMarkInfo.MarkX, oMarkInfo.MarkY + 1)
                            oInspectSum.ReceiveTime = DateTime.Now

                            If oMarkInfo.OriginalType = ResultType.NoDie Then 'No Die-�аO (���n�P�_����)
                                sResult = "NoDie"
                                oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                                   oInspectSum.InspectResult.AINODIEPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                                   oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - oMarkInfo.MarkX, oMarkInfo.MarkY + 1, oInspectSum.ReceiveTime, sResult)
                            Else
                                sResult = "OK"
                                oAI.DefectFileName = String.Format("{0}\{1}_{2}_{3}_R{4:d3}_C{5:d3}_{6:yyyyMMddHHHmmss}_{7}.bmp",
                                                                   oInspectSum.InspectResult.AIOKPath, oMyEquipment.MainRecipe.RecipeID, oInspectSum.InspectResult.CodeID,
                                                                   oInspectSum.ProductConfig.EQPID, oRecipe.MarkXCount - oMarkInfo.MarkX, oMarkInfo.MarkY + 1, oInspectSum.ReceiveTime, sResult)
                            End If

                            MIL.MbufChild2d(oCameraSourceImage, oAI.DefectPosition.X, oAI.DefectPosition.Y, oAI.DefectBoundary.Width, oAI.DefectBoundary.Height, oAIModelImage)
                            MIL.MbufExport(oAI.DefectFileName, MIL.M_BMP, oAIModelImage)
                            MIL.MbufFree(oAIModelImage)
                            oAIModelImage = MIL.M_NULL

                            SyncLock CAutoRunThread.ProcessDefectListLock
                                oInspectSum.DefectList.OKList.Add(oAI)
                            End SyncLock
                            '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))
                        End If

                        'Return True
                    End If



                    '=================================== 20230922-�}�l ===================================
                    If oMarkInfo.OriginalType = ResultType.NoDie AndAlso oInspectSum.InspectResult.DefectNoDieCount > 0 Then 'No Die-�аO (���n�P�_����)
                        oMarkInfo.Result = ResultType.NoDie
                        oInspectSum.InspectResult.ModleLoseStatus = False 'BuildLoseModel �|�p(CInspectResult) '2023-09-22 17:30 �]���n�����|�p�PNoDie�L�k���}�����D
                    Else
                        '-------------------------20230905-�}�l--------------------------
                        If Debugger.IsAttached = True Then
                            '�쥻���g�k
                            'oMarkInfo.Result = ResultType.Lose '�|�p(CMyMarkInfo)
                            oMarkInfo.Result = ResultType.DieLoseLaser2 '�|�p(CMyMarkInfo)
                        ElseIf Debugger.IsAttached = False Then
                            '���ժ��g�k
                            oMarkInfo.Result = ResultType.DieLoseLaser2 '�|�p(CMyMarkInfo)
                        End If
                        '-------------------------20230905-����--------------------------

                        SyncLock CAutoRunThread.ProcessDefectListLock
                            oInspectSum.InspectResult.DefectCount += 1 '((((((((((((((((((((((((((((((( ���n�϶� ))))))))))))))))))))))))))))))

                            '-------------------------�岫���G�T��-�}�l--------------------------
                            Dim defectResultMsg As String = String.Empty
                            For Each value As ResultType In [Enum].GetValues(GetType(ResultType))
                                If oMarkInfo.Result = value Then
                                    defectResultMsg = frmMain.GetDescriptionText(oMarkInfo.Result)
                                    Exit For
                                End If
                            Next
                            oLog.LogError(String.Format("[{0:d4}] B�岫:" & defectResultMsg, nSequence)) 'Log ��x(�B�z Process)
                            Dim stTrace1 As StackTrace = New StackTrace(fNeedFileInfo:=True)
                            Dim stFrame1 As StackFrame = stTrace1.GetFrames(0)
                            Dim fileName1 As String = stFrame1.GetFileName
                            Dim fileLineNum1 As Integer = stFrame1.GetFileLineNumber
                            Dim fileColNum1 As Integer = stFrame1.GetFileColumnNumber
                            Dim fileMethodName1 As String = stFrame1.GetMethod().Name
                            oLog.LogError("FileName:" & fileName1)
                            oLog.LogError("FileLineNumber:" & fileLineNum1)
                            oLog.LogError("FileColumnNumber:" & fileColNum1)
                            oLog.LogError("MethodName:" & fileMethodName1)
                            oLog.LogError(String.Format("[{0:d4}] DefectCount:" & oInspectSum.InspectResult.DefectCount, nSequence)) 'Log ��x(�B�z Process)
                            '-------------------------�岫���G�T��-����--------------------------
                        End SyncLock


                        '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                        Dim oDefect As New CMyDefect
                        oDefect.InpsectMethod = Comp_Inspect_Method.Comp_Define2
                        oDefect.InspectType = InspectType.ModelDiff
                        oDefect.DefectType = Comp_InsperrorType.Comp_Corner
                        oDefect.ResultType = oMarkInfo.Result '�\�L�|�p
                        oDefect.DefectName = EnumHelper.GetDescription(oMarkInfo.Result) '�岫�W��(�i�Ω�X����) [oMarkInfo.Result = ResultType.Lose]
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
                        oDefect.DefectCoordinate = New CITVPointWapper(oMarkInfo.MarkX, oMarkInfo.MarkY)  '' Augustin 220726 for Wafer Map
                        oDefect.DefectIndex = New CITVPointWapper(oRecipe.MarkXCount - oMarkInfo.MarkX, oMarkInfo.MarkY + 1)

                        sResult = "NG" 'BuildLoseModel ���n-���XReport���Ϥ���(�\�L�|�p/�\�L��m)
                        oDefect.DefectImage.FileName = String.Format("{0}_{1}_{2}_R{3:d3}_C{4:d3}_{5:yyyyMMddHHHmmss}_{6}.bmp",
                                                                     oMyEquipment.MainRecipe.RecipeID,
                                                                     oInspectSum.InspectResult.CodeID,
                                                                     oInspectSum.ProductConfig.EQPID,
                                                                     oRecipe.MarkXCount - oMarkInfo.MarkX,
                                                                     oMarkInfo.MarkY + 1,
                                                                     oInspectSum.ReceiveTime,
                                                                     sResult) '���n-���XReport���Ϥ���(�\�L�|�p)

                        oDefect.DefectFileName = String.Format("{0}\{1}", oInspectSum.InspectResult.InspectPath, oDefect.DefectImage.FileName) '�岫�I��p��(�i�Ω�X����)

                        SyncLock CAutoRunThread.ProcessDefectListLock
                            oInspectSum.DefectList.DefectList.Add(oDefect)
                            oInspectSum.DefectListDraw.Add(oDefect) '�Ω�e��(oMarkInfo.Result = ResultType.Lose,�\�L�|�p/�\�L��m)

                            '-------------------------�岫���G�T��-�}�l--------------------------
                            oLog.LogError(String.Format("[{0:d4}] C�岫:", nSequence)) 'Log ��x(�B�z Process)
                            Dim stTrace1 As StackTrace = New StackTrace(fNeedFileInfo:=True)
                            Dim stFrame1 As StackFrame = stTrace1.GetFrames(0)
                            Dim fileName1 As String = stFrame1.GetFileName
                            Dim fileLineNum1 As Integer = stFrame1.GetFileLineNumber
                            Dim fileColNum1 As Integer = stFrame1.GetFileColumnNumber
                            Dim fileMethodName1 As String = stFrame1.GetMethod().Name
                            oLog.LogError("FileName:" & fileName1)
                            oLog.LogError("FileLineNumber:" & fileLineNum1)
                            oLog.LogError("FileColumnNumber:" & fileColNum1)
                            oLog.LogError("MethodName:" & fileMethodName1)
                            oLog.LogError(String.Format("[{0:d4}] oInspectSum.DefectListDraw.Count:" & oInspectSum.DefectListDraw.Count, nSequence)) 'Log ��x(�B�z Process)
                            '-------------------------�岫���G�T��-����--------------------------

                            '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                            '-------------------------20230911-�}�l--------------------------
                            'oMarkInfo.Result = ResultType.Lose ------> oInspectSum.InspectResult.ModleLoseStatus = True
                            '************************ �u�����ӭn���|�p���϶��b�o�� '************************
                            'oInspectSum.InspectResult.ModleLoseStatus:�o��Flag(�����ܼ�)�|�v�T��|�pTriggerAlarm�ΰ���SetEroorOn,�n�ԷV�M�w�O�_�n�Q���ѱ�
                            'oInspectSum.InspectResult.ModleLoseStatus = True 'BuildLoseModel �|�p(CInspectResult) '2023-09-11 11:50 �]���Ͳ��u�W�c���X,�ҥH�����ѱ��H�Q����
                            oInspectSum.InspectResult.ModleLoseStatus = True 'BuildLoseModel �|�p(CInspectResult) '2023-09-22 17:30 �]���n�����|�p�PNoDie�L�k���}�����D 
                            '-------------------------20230911-����--------------------------
                            '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))
                        End SyncLock

                        MIL.MbufChild2d(oCameraSourceImage, oDefect.DefectPosition.X, oDefect.DefectPosition.Y, oDefect.DefectBoundary.Width, oDefect.DefectBoundary.Height, oAIModelImage)
                        MIL.MbufExport(oDefect.DefectFileName, MIL.M_BMP, oAIModelImage)
                        MIL.MbufFree(oAIModelImage)
                        oAIModelImage = MIL.M_NULL
                        '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))
                    End If
                    '=================================== 20230922-���� ===================================



                End If
                '-------------------------If oMarkInfo.Result = ResultType.NA AndAlso nIndex >= 0-����--------------------------

                '************************ 20230919-�����ϬM�{����ڴ��պ|�p�MNoDie���|�����H�U���{���϶�-���� ************************
            End With
        Catch ex As Exception
            oLog.LogError(String.Format("[{0:d4}] BuildLoseModel Failed�IError�G{1}", nSequence, ex.Message & Environment.NewLine & ex.StackTrace))
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
    ''' �����l�������ˬdNoDie����(CAutoRunThread.RunInspect -> modLibrary.CompareOriginalAndInspectNoDieSection)
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

                    '' 10 18���խק�
                    'If (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.Y) = oProduct.MarkList(i_oProduct).MarkY AndAlso (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.X) = oProduct.MarkList(i_oProduct).MarkX Then

                    '(((((((((((((((((((((((((((((((���n�϶�-�}�l-Begin))))))))))))))))))))))))))))))
                    If (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.Y) = oProduct.MarkList(i_oProduct).MarkY + 1 AndAlso _
                       (oInspectSum.DefectList.DefectList(i_InspectSum).DefectIndex.X) = (oProduct.DimensionX - oProduct.MarkList(i_oProduct).MarkX) Then 'No Die-���n�P�_����

                        '++++++ First judge OriginalType ------> Second judge ResultType ++++++
                        If oProduct.MarkList(i_oProduct).OriginalType = ResultType.NoDie Then 'No Die-�аO (���n�P�_����)
                            oInspectSum.DefectList.DefectList(i_InspectSum).ResultType = ResultType.NoDie '(((((((((((((((((((((((((((((((���n�϶�))))))))))))))))))))))))))))))
                            oInspectSum.InspectResult.DefectNoDieCount += 1 'No Die�ƶq(Defect)
                        End If
                    Else
                        If oProduct.MarkList(i_oProduct).OriginalType = ResultType.NoDie Then 'No Die-�аO (���n�P�_����)
                            If oProduct.MarkList(i_oProduct).Result <> ResultType.NoDie Then
                                oProduct.MarkList(i_oProduct).Result = ResultType.NoDie '(((((((((((((((((((((((((((((((���n�϶�))))))))))))))))))))))))))))))
                                oInspectSum.InspectResult.NotDefectNoDieCount += 1 'No Die�ƶq(NotDefect)
                            End If
                        End If
                    End If
                    '(((((((((((((((((((((((((((((((���n�϶�-����-End  ))))))))))))))))))))))))))))))
                Next
            Next
        Catch ex As Exception
            oLog.LogError(ex.ToString)
            Return False
        End Try
        Return True
    End Function

End Module