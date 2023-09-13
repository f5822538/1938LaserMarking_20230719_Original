Partial Class CMyEquipment

    Public ImageID As New MIL_ID
    Public RecipeID As New MIL_ID
    Public CodeReaderRecipeID As New MIL_ID
    Public ImageHeader As New ImageHeader
    Public CodeReaderImageHeader As New ImageHeader
    Public RecipeHeader As New ImageHeader
    Public CodeReaderRecipeHeader As New ImageHeader
    Public RecipeMarkBitmap1 As Bitmap
    Public RecipeMarkBitmap2 As Bitmap
    Public RecipeMarkImage1 As New ITVImage
    Public RecipeMarkImage2 As New ITVImage
    Private moPatternMatchingMark1 As CMyPatternMatching = New CMyPatternMatching()
    Private moPatternMatchingMark2 As CMyPatternMatching = New CMyPatternMatching()

    Public FindMark1X As Double = 0.0
    Public FindMark1Y As Double = 0.0
    Public FindMark2X As Double = 0.0
    Public FindMark2Y As Double = 0.0
    Public SucceedFind As Boolean = False 'FindModel的結果(True:成功, False:失敗)

    Public FindEdgeTopY As Integer = -1
    Public FindEdgeBottomY As Integer = -1
    Public FindEdgeLeftX As Integer = -1
    Public FindEdgeRightX As Integer = -1

    Public Sub CreateImage(ByRef oImageID As MIL_ID, ByRef oBitmap As Bitmap, oLog As II_LogTraceExtend)
        Try
            If oBitmap IsNot Nothing Then
                Call oBitmap.Dispose()
                oBitmap = Nothing
            End If

            If oImageID = 0 Then
                Call MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, moCamera.Camera.CameraWidth, moCamera.Camera.CameraHeight, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oImageID)
                GC.GetTotalMemory(True)
            End If

            Try
                oBitmap = New Bitmap(moCamera.Camera.CameraWidth, moCamera.Camera.CameraHeight, CInt(MIL.MbufInquire(oImageID, MIL.M_PITCH_BYTE)), Imaging.PixelFormat.Format8bppIndexed, MIL.MbufInquire(oImageID, MIL.M_HOST_ADDRESS))

            Catch ex As Exception
                Call oLog.LogError(String.Format("Create Bitmap，失敗！，Error：{0}", ex.ToString))
                Call LogAlarm.LogError("Create Bitmap，失敗！")
                Exit Sub
            End Try

            Dim oPalette As Imaging.ColorPalette = oBitmap.Palette
            For i As Integer = 0 To 255
                oPalette.Entries(i) = Color.FromArgb(i, i, i)
            Next
            oBitmap.Palette = oPalette
        Catch ex As Exception
            Call oLog.LogError(String.Format("Create Image，失敗！Error：{0}", ex.ToString))
            Call LogAlarm.LogError("Create Image，失敗！")
        End Try
    End Sub

    Public Function CreateMarkImage1(oImageHeader As ImageHeader, oRectangle As Rectangle, oLog As II_LogTraceExtend) As Boolean
        Try
            If oImageHeader.Ptr = IntPtr.Zero Then Return False

            If RecipeMarkImage1 IsNot Nothing Then
                Call RecipeMarkImage1.Dispose()
                RecipeMarkImage1 = Nothing
            End If

            If RecipeMarkBitmap1 IsNot Nothing Then
                Call RecipeMarkBitmap1.Dispose()
                RecipeMarkBitmap1 = Nothing
            End If

            RecipeMarkBitmap1 = New Bitmap(oRectangle.Width, oRectangle.Height, Imaging.PixelFormat.Format8bppIndexed)
            RecipeMarkImage1 = New ITVImage

            Dim oPalette As Imaging.ColorPalette = RecipeMarkBitmap1.Palette
            For i As Integer = 0 To 255
                oPalette.Entries(i) = Color.FromArgb(i, i, i)
            Next
            RecipeMarkBitmap1.Palette = oPalette

            Dim oImage As New ITVImage()
            Dim oROI As New ITVImageROI()

            oImage.AssignToImageHeader(oImageHeader)
            RecipeMarkImage1.AssignToBitmap(RecipeMarkBitmap1)
            oROI.ApplyRegion(oRectangle)

            ImageProcessingFunctions.iCopy(oImage, RecipeMarkImage1, oROI)

            oROI.Dispose()
            oImage.Dispose()
            oROI = Nothing
            oImage = Nothing
            Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("Create Mark Image 1，失敗！Error：{0}", ex.ToString))
            Call LogAlarm.LogError("Create Mark Image 1，失敗！")
            Return False
        End Try
    End Function

    Public Function CreateMarkImage2(oImageHeader As ImageHeader, oRectangle As Rectangle, oLog As II_LogTraceExtend) As Boolean
        Try
            If oImageHeader.Ptr = IntPtr.Zero Then Return False

            If RecipeMarkImage2 IsNot Nothing Then
                Call RecipeMarkImage2.Dispose()
                RecipeMarkImage2 = Nothing
            End If

            If RecipeMarkBitmap2 IsNot Nothing Then
                Call RecipeMarkBitmap2.Dispose()
                RecipeMarkBitmap2 = Nothing
            End If

            RecipeMarkBitmap2 = New Bitmap(oRectangle.Width, oRectangle.Height, Imaging.PixelFormat.Format8bppIndexed)
            RecipeMarkImage2 = New ITVImage

            Dim oPalette As Imaging.ColorPalette = RecipeMarkBitmap2.Palette
            For i As Integer = 0 To 255
                oPalette.Entries(i) = Color.FromArgb(i, i, i)
            Next
            RecipeMarkBitmap2.Palette = oPalette

            Dim oImage As New ITVImage()
            Dim oROI As New ITVImageROI()

            oImage.AssignToImageHeader(oImageHeader)
            RecipeMarkImage2.AssignToBitmap(RecipeMarkBitmap2)
            oROI.ApplyRegion(oRectangle)

            ImageProcessingFunctions.iCopy(oImage, RecipeMarkImage2, oROI)

            oROI.Dispose()
            oImage.Dispose()
            oROI = Nothing
            oImage = Nothing
            Return True
        Catch ex As Exception
            Call oLog.LogError(String.Format("Create Mark Image 2，失敗！Error：{0}", ex.ToString))
            Call LogAlarm.LogError("Create Mark Image 2，失敗！")
            Return False
        End Try
    End Function

    Public Function BuildImageForLoad(sImagePath As String, ByRef oImageID As MIL_ID, ByRef oImageHeader As ImageHeader, nSequence As Integer, oLog As II_LogTraceExtend) As Boolean
        If File.Exists(sImagePath) = False Then Return False

        Try
            Dim oLoadBitmap As Bitmap
            Try
                oLoadBitmap = New Bitmap(sImagePath)
            Catch ex As Exception
                If nSequence = -1 Then
                    Call oLog.LogError(String.Format("載入影像，失敗！路徑：{0}，Error：{1}", sImagePath, ex.ToString))
                Else
                    Call oLog.LogError(String.Format("[{0:d4}] 載入影像，失敗！路徑：{1}，Error：{2}", nSequence, sImagePath, ex.ToString))
                End If
                Call LogAlarm.LogError(String.Format("載入影像，失敗！路徑：{0}", sImagePath))
                Return False
            End Try

            Dim oSourceImage As New ITVImage
            Dim oDistImage As New ITVImage

            If oImageID <> 0 AndAlso (oImageHeader.Width <> CInt(MIL.MbufInquire(oImageID, MIL.M_SIZE_X)) OrElse oImageHeader.Height <> CInt(MIL.MbufInquire(oImageID, MIL.M_SIZE_Y)) OrElse oImageHeader.Stride <> CInt(MIL.MbufInquire(oImageID, MIL.M_PITCH_BYTE))) Then
                MIL.MbufFree(oImageID)
                oImageID = MIL.M_NULL
            End If

            If oImageID = 0 Then
                Call MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oLoadBitmap.Width, oLoadBitmap.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oImageID)
                GC.GetTotalMemory(True)
            End If

            oImageHeader = GetFullImageHeader(oImageID, nSequence, oLog)
            Call oSourceImage.AssignToBitmap(oLoadBitmap)
            Call oDistImage.AssignToImageHeader(oImageHeader)

            ImageProcessingFunctions.iCopy(oSourceImage, oDistImage)
            oSourceImage.Dispose()
            oDistImage.Dispose()
            oLoadBitmap.Dispose()
            oLoadBitmap = Nothing
            GC.GetTotalMemory(True)
        Catch ex As Exception
            Call oLog.LogError(String.Format("Build Image (Load)，失敗！Error：{0}", ex.ToString))
            Call LogAlarm.LogError("Build Image (Load)，失敗！")
            Return False
        End Try
        Return True
    End Function

    Public Function BuildImageForCopy(oSourceBitmap As Bitmap, ByRef oImageID As MIL_ID, ByRef oImageHeader As ImageHeader, nSequence As Integer, oLog As II_LogTraceExtend) As Boolean
        If oSourceBitmap Is Nothing Then Return False
        Dim oSourceImage As New ITVImage
        Dim oDistImage As New ITVImage

        Try
            If oImageID <> 0 AndAlso (oImageHeader.Width <> CInt(MIL.MbufInquire(oImageID, MIL.M_SIZE_X)) OrElse oImageHeader.Height <> CInt(MIL.MbufInquire(oImageID, MIL.M_SIZE_Y)) OrElse oImageHeader.Stride <> CInt(MIL.MbufInquire(oImageID, MIL.M_PITCH_BYTE))) Then
                MIL.MbufFree(oImageID)
                oImageID = MIL.M_NULL
            End If

            If oImageID = 0 Then
                Call MIL.MbufAlloc2d(MIL.M_DEFAULT_HOST, oSourceBitmap.Width, oSourceBitmap.Height, 8 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC, oImageID)
                GC.GetTotalMemory(True)
            End If

            oImageHeader = GetFullImageHeader(oImageID, nSequence, oLog)
            Call oSourceImage.AssignToBitmap(oSourceBitmap)
            Call oDistImage.AssignToImageHeader(oImageHeader)

            ImageProcessingFunctions.iCopy(oSourceImage, oDistImage)
        Catch ex As Exception
            Call oLog.LogError(String.Format("Build Image (Copy)，失敗！Error：{0}", ex.ToString))
            Call LogAlarm.LogError("Build Image (Copy)，失敗！")
            Return False
        End Try

        oSourceImage.Dispose()
        oDistImage.Dispose()
        GC.GetTotalMemory(True)
        Return True
    End Function

    Public Function GetFullImageHeader(oImageID As MIL_ID, nSequence As Integer, oLog As II_LogTraceExtend) As ImageHeader
        Dim tImageHeader As ImageHeader
        Try
            With tImageHeader
                .Ptr = MIL.MbufInquire(oImageID, MIL.M_HOST_ADDRESS)
                .Width = CInt(MIL.MbufInquire(oImageID, MIL.M_SIZE_X))
                .Height = CInt(MIL.MbufInquire(oImageID, MIL.M_SIZE_Y))
                .Stride = CInt(MIL.MbufInquire(oImageID, MIL.M_PITCH_BYTE))
            End With
        Catch ex As Exception
            If nSequence = -1 Then
                Call oLog.LogError(String.Format("Get Image Header，失敗！Error：{0}", ex.ToString))
            Else
                Call oLog.LogError(String.Format("[{0:d4}] Get Image Header，失敗！Error：{1}", nSequence, ex.ToString))
            End If
            Call LogAlarm.LogError("Get Image Header，失敗！")
        End Try
        Return tImageHeader
    End Function

    Public Function UpdateImage(oImageHeader As ImageHeader) As Bitmap
        With oImageHeader
            Dim oBitmap As Bitmap = New Bitmap(.Width, .Height, .Stride, Imaging.PixelFormat.Format8bppIndexed, .Ptr)
            Dim oPalette As Imaging.ColorPalette = oBitmap.Palette
            For i As Integer = 0 To 255
                oPalette.Entries(i) = Color.FromArgb(i, i, i)
            Next
            oBitmap.Palette = oPalette
            Return oBitmap
        End With
    End Function

    Public Function CopyImage(oSourceBitmap As Bitmap, ByRef oCopyBitmap As Bitmap, oLog As II_LogTraceExtend) As Boolean
        If oSourceBitmap Is Nothing Then Return False

        Try
            Dim oSourceImage As New ITVImage
            Dim oCopyImage As New ITVImage

            If oCopyBitmap IsNot Nothing Then
                Call oCopyBitmap.Dispose()
                oCopyBitmap = Nothing
            End If

            oCopyBitmap = New Bitmap(oSourceBitmap.Width, oSourceBitmap.Height, oSourceBitmap.PixelFormat)
            Call oSourceImage.AssignToBitmap(oSourceBitmap)
            Call oCopyImage.AssignToBitmap(oCopyBitmap)

            ImageProcessingFunctions.iCopy(oSourceImage, oCopyImage)
            oSourceImage.Dispose()
            oCopyImage.Dispose()
            GC.GetTotalMemory(True)
        Catch ex As Exception
            Call oLog.LogError(String.Format("Copy Image，失敗！Error：{0}", ex.ToString))
            Call LogAlarm.LogError("Copy Image，失敗！")
            Return False
        End Try
        Return True
    End Function

    Public Function AddModel(oImageID As MIL_ID, oImageHeader As ImageHeader, PatternZoneMark1 As Rectangle, PatternZoneMark2 As Rectangle) As Boolean
        Dim bIsOK As Boolean = True
        '' Augustin 230109 Test
        'If moPatternMatchingMark1.AddModel(oImageID, PatternZoneMark1, MainRecipe.RecipeCamera.Locate1.Score) = False Then bIsOK = False
        'If moPatternMatchingMark2.AddModel(oImageID, PatternZoneMark2, MainRecipe.RecipeCamera.Locate2.Score) = False Then bIsOK = False

        If moHardwareConfig.MiscConfig.IsUseModelFinder = True Then
            If AllocateInspect(oImageHeader) <> AlarmCode.IsOK Then bIsOK = False
        End If

        Return bIsOK
    End Function

    ''' <summary>
    ''' 利用特徵比對算法尋找定位孔
    ''' </summary>
    ''' <param name="nSequence"></param>
    ''' <param name="oLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlign(nSequence As Integer, oLog As II_LogTraceExtend) As Boolean
        Dim bFindModelSuccess As Boolean = True
        Dim nScore As Double = 0.0

        If moPatternMatchingMark1.FindModel(ImageID, moMainRecipe.RecipeCamera.Locate1.FindModelZone, FindMark1X, FindMark1Y, nScore, moMainRecipe.RecipeCamera.Locate1.PatternZone.Location) = True Then
            If nSequence = -1 Then
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("定位孔 GetAlign Find [Model 1]：X = {0}, Y = {1}, Score = {2}", FindMark1X, FindMark1Y, nScore)) '定位重要訊息
            Else
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("[{0:d4}] 定位孔 GetAlign Find [Model 1]：X = {1}, Y = {2}, Score = {3}", nSequence, FindMark1X, FindMark1Y, nScore)) '定位重要訊息
            End If
        ElseIf moMainRecipe.RecipeCamera.Locate1.FindModelZone = Rectangle.Empty Then
            FindMark1X = 0.0
            FindMark1Y = 0.0
        Else
            bFindModelSuccess = False
            If nSequence = -1 Then
                Call oLog.LogError(String.Format("定位孔 GetAlign Find [Model 1] Failed"))
            Else
                Call oLog.LogError(String.Format("[{0:d4}] 定位孔 GetAlign Find [Model 1] Failed", nSequence))
            End If
        End If

        If moPatternMatchingMark2.FindModel(ImageID, moMainRecipe.RecipeCamera.Locate2.FindModelZone, FindMark2X, FindMark2Y, nScore, moMainRecipe.RecipeCamera.Locate2.PatternZone.Location) = True Then
            If nSequence = -1 Then
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("定位孔 GetAlign Find [Model 2]：X = {0}, Y = {1}, Score = {2}", FindMark2X, FindMark2Y, nScore)) '定位重要訊息
            Else
                oLog.Log(LOGHandle.HANDLE_INSPECT, String.Format("[{0:d4}] 定位孔 GetAlign Find [Model 2]：X = {1}, Y = {2}, Score = {3}", nSequence, FindMark2X, FindMark2Y, nScore)) '定位重要訊息
            End If
        ElseIf moMainRecipe.RecipeCamera.Locate2.FindModelZone = Rectangle.Empty Then
            FindMark2X = 0.0
            FindMark2Y = 0.0
        Else
            bFindModelSuccess = False
            If nSequence = -1 Then
                Call oLog.LogError(String.Format("定位孔 GetAlign Find [Model 2] Failed"))
            Else
                Call oLog.LogError(String.Format("[{0:d4}] 定位孔 GetAlign Find [Model 2] Failed", nSequence))
            End If
        End If

        Return bFindModelSuccess
    End Function
End Class