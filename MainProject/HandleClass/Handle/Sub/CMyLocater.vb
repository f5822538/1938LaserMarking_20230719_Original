Imports iTVisionInspectLib
Imports System.Math

Public Structure LocaterResult
    Public Succeed As Boolean
    Public Score As Double
    Public Angle As Double
    Public X As Double
    Public Y As Double

    Public Sub Clear()
        Succeed = False
        Score = 0.0
        X = 0.0
        Y = 0.0
    End Sub
End Structure

Public Class CMyLocater
    Private Enum SearchRegionValue As Integer
        X = 13312
        Y = 17408
        Width = 128
        Height = 130
        Smoth = 108
        FirstLevel = 31
        LastLevel = 32
    End Enum

    Private moMyEquipment As CMyEquipment
    Private moImagePreprocess As II_ImagePreprocess
    Private moBlob As II_Blob
    Private moFinder As CMultipleFinder
    Private moCircleFinder As ITVCircleDetector  '' Augustin 221228
    Private moResult As LocaterResult 'FindModel-的結果
    Private nModelWidth As Integer = 0
    Private nModelHeight As Integer = 0
    Public CircleLocationX As Double  '' Augustin 221228
    Public CircleLocationY As Double  '' Augustin 221228
    Public CircleRadius As Double     '' Augustin 221228

    Public Locater1CircleX As Double  '' Augustin 230202
    Public Locater1CircleY As Double  '' Augustin 230202
    Public Locater1CircleRadius As Double     '' Augustin 230202
    Public Locater2CircleX As Double  '' Augustin 230202
    Public Locater2CircleY As Double  '' Augustin 230202
    Public Locater2CircleRadius As Double     '' Augustin 230202

    ''' <summary>
    '''  'FindModel-的結果
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Result As LocaterResult
        Get
            Return moResult
        End Get
    End Property

    Public Sub New(oMyEquipment As CMyEquipment)
        moMyEquipment = oMyEquipment
        moFinder = New CMultipleFinder(True, AppMgr.MILApp)
        moCircleFinder = New ITVCircleDetector  '' Augustin 221228
        moImagePreprocess = CImagePreprocessCreator.CreateImagePreprocess()
        moBlob = CBlobCreator.CreateBlob()
    End Sub

    Public Function UpdateModelCenter(oSourceImageHeader As ImageHeader, ByRef oSearchRange As Rectangle, ByRef oModel As Rectangle, nThreshold As Integer, nArea As Integer) As Boolean
        Try
            If oSourceImageHeader.Ptr = IntPtr.Zero OrElse oSourceImageHeader.Width < oSearchRange.Right OrElse oSourceImageHeader.Height < oSearchRange.Bottom Then Return False
            If moImagePreprocess Is Nothing OrElse moBlob Is Nothing Then Return False

            Dim oSourceROIImageHeader As ImageHeader = GetImageHeader(oSourceImageHeader, oModel)
            Dim oBinarizeBitmap As Bitmap = New Bitmap(oModel.Width, oModel.Height, Imaging.PixelFormat.Format8bppIndexed)
            Dim oBinarizeImageHeader As ImageHeader = GetImageHeader(oBinarizeBitmap)
            Call moImagePreprocess.BinarizeForSingle(oSourceROIImageHeader, oBinarizeImageHeader, ImagePreprocessConditionAndThreshMode.GREATER_OR_EQUAL, nThreshold)
            moBlob.ForegroundValue = BlobForegroundValue.Black
            moBlob.IdentificationModeValue = BlobIdentificationModeValue.INDIVIDUAL
            Call moBlob.Calculate(oBinarizeImageHeader, oSourceROIImageHeader)
            Call oBinarizeBitmap.Dispose()
            oBinarizeBitmap = Nothing

            Dim oBlobFilterOneConditionParameterList As New List(Of BlobFilterOneConditionParameter)
            Dim oBlobFilterOneConditionParameter As BlobFilterOneConditionParameter
            Dim oBlobResultList As New List(Of BlobResult)
            oBlobFilterOneConditionParameter.Operation = BlobOperationType.DELETE
            oBlobFilterOneConditionParameter.SelectionCriterion = BlobSelectionCriterionType.AREA
            oBlobFilterOneConditionParameter.Condition = BlobOneConditionType.LESS
            oBlobFilterOneConditionParameter.CondLow = nArea
            Call oBlobFilterOneConditionParameterList.Add(oBlobFilterOneConditionParameter)
            Call moBlob.FilterOneCondition(oBlobFilterOneConditionParameterList)
            Call moBlob.GetResults(oBlobResultList)

            Dim nMaxArea As Double = 0.0
            For nIndex As Integer = 0 To oBlobResultList.Count - 1
                If oBlobResultList(nIndex).Area > nMaxArea Then
                    oSearchRange.X = CInt(oBlobResultList(nIndex).DefectCenterX - (oSearchRange.Width / 2) + oModel.X)
                    oSearchRange.Y = CInt(oBlobResultList(nIndex).DefectCenterY - (oSearchRange.Height / 2) + oModel.Y)
                    oModel.X = CInt(oBlobResultList(nIndex).DefectCenterX - (oModel.Width / 2) + oModel.X)
                    oModel.Y = CInt(oBlobResultList(nIndex).DefectCenterY - (oModel.Height / 2) + oModel.Y)
                    nMaxArea = oBlobResultList(nIndex).Area
                End If
            Next

            If oSourceImageHeader.Width < oSearchRange.Right Then
                oSearchRange.X = oSearchRange.X - (oSearchRange.Right - oSourceImageHeader.Width + 1)
            End If

            If oSourceImageHeader.Height < oSearchRange.Bottom Then
                oSearchRange.Y = oSearchRange.Y - (oSearchRange.Bottom - oSourceImageHeader.Height + 1)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function UpdateModel(SourceImage As Bitmap, oSearchRange As Rectangle, oModel As Rectangle, nScore As Integer, nSmoth As Integer) As Boolean
        Try
            '' Augustin 230109 
            Dim oBinarieImage As New ITVImage
            Dim oBinarizeBitmap As Bitmap
            Dim oBinarizeImageHeader As New ImageHeader
            'Dim sSavePath As String = Application.StartupPath & "\Recipe\" & moMyEquipment.MainRecipe.RecipeID & "_BIN.Bmp"

            If SourceImage Is Nothing OrElse SourceImage.Width < oSearchRange.Right OrElse SourceImage.Height < oSearchRange.Bottom Then Return False

            '' Binarize
            oBinarizeBitmap = CType(SourceImage.Clone(), Bitmap)
            oBinarizeImageHeader = GetImageHeader(oBinarizeBitmap)
            moImagePreprocess.BinarizeForSingle(oBinarizeImageHeader, oBinarizeImageHeader, ImagePreprocessConditionAndThreshMode.GREATER_OR_EQUAL, moMyEquipment.MainRecipe.Threshold)
            oBinarieImage.AssignToImageHeader(oBinarizeImageHeader)
            oBinarizeBitmap = oBinarieImage.GetBitmap
            'oBinarizeBitmap.Save(sSavePath, Imaging.ImageFormat.Bmp)

            If moFinder.ModelCount > 0 Then moFinder.RemoveModel(0)
            nModelWidth = oModel.Width
            nModelHeight = oModel.Height
            moFinder.AddModel(oBinarizeBitmap, oModel)
            'moFinder.AddModel(SourceImage, oModel)
            moFinder.SetModelParameter(0, MFControl.Accpet, nScore)
            moFinder.SetModelParameter(0, SearchRegionValue.X, oSearchRange.X)
            moFinder.SetModelParameter(0, SearchRegionValue.Y, oSearchRange.Y)
            moFinder.SetModelParameter(0, SearchRegionValue.Width, oSearchRange.Width)
            moFinder.SetModelParameter(0, SearchRegionValue.Height, oSearchRange.Height)
            moFinder.SetModelParameter(0, SearchRegionValue.Smoth, nSmoth)

            oBinarieImage.Dispose()
            oBinarizeBitmap.Dispose()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function Find(oBitmap As Bitmap) As Boolean
        '' Augustin 230109 
        Dim oBinarieImage As New ITVImage
        Dim oBinarizeBitmap As Bitmap '二值化Bitmap
        Dim oBinarizeImageHeader As New ImageHeader
        'Dim sSavePath As String = "D:\BIN.Bmp"

        '' Binarize
        oBinarizeBitmap = CType(oBitmap.Clone(), Bitmap) '二值化Bitmap
        oBinarizeImageHeader = GetImageHeader(oBinarizeBitmap)
        moImagePreprocess.BinarizeForSingle(oBinarizeImageHeader, oBinarizeImageHeader, ImagePreprocessConditionAndThreshMode.GREATER_OR_EQUAL, moMyEquipment.MainRecipe.Threshold)
        oBinarieImage.AssignToImageHeader(oBinarizeImageHeader)
        oBinarizeBitmap = oBinarieImage.GetBitmap '二值化Bitmap
        'oBinarizeBitmap.Save(sSavePath, Imaging.ImageFormat.Bmp)

        moResult.Clear()
        If moFinder.ModelCount = 0 Then
            '-------------------------定位孔異常圖片-開始--------------------------
            Dim holeBinBmpDirPath As String = "D:\img\HoleLocateBinBmp\" '定位孔異常圖片-資料夾路徑
            If Directory.Exists(holeBinBmpDirPath) = False Then Directory.CreateDirectory(holeBinBmpDirPath)
            Dim holeBinBmpFilePath As String = String.Format("{0}{1:yyyy-MM-dd_HH_mm_ss}-{2}.bmp", holeBinBmpDirPath, DateTime.Now, "Default")
            oBinarizeBitmap.Save(holeBinBmpFilePath, Imaging.ImageFormat.Bmp) '儲存-定位孔異常圖片(二值化)
            oBitmap.Save(holeBinBmpFilePath, Imaging.ImageFormat.Bmp) '儲存-定位孔異常圖片(原圖)
            '-------------------------定位孔異常圖片-結束--------------------------
            Return False
        End If

        moResult.Succeed = moFinder.FindModel(oBinarizeBitmap) '設定-FindModel-的結果
        'moResult.Succeed = moFinder.FindModel(oBitmap)

        If moResult.Succeed = True Then
            moResult.Angle = If(moFinder.ModelResult(0).Angle > 180, moFinder.ModelResult(0).Angle - 360, moFinder.ModelResult(0).Angle)
            moResult.X = moFinder.ModelResult(0).X
            moResult.Y = moFinder.ModelResult(0).Y
            moResult.Score = moFinder.ModelResult(0).Score
        Else
            '-------------------------定位孔異常圖片-開始--------------------------
            Dim holeBinBmpDirPath As String = "D:\img\HoleLocateBinBmp\" '定位孔異常圖片-資料夾路徑
            If Directory.Exists(holeBinBmpDirPath) = False Then Directory.CreateDirectory(holeBinBmpDirPath)
            Dim holeBinBmpFilePath As String = String.Format("{0}{1:yyyy-MM-dd_HH_mm_ss}-{2}.bmp", holeBinBmpDirPath, DateTime.Now, "Default")
            oBinarizeBitmap.Save(holeBinBmpFilePath, Imaging.ImageFormat.Bmp) '儲存-定位孔異常圖片(二值化)
            oBitmap.Save(holeBinBmpFilePath, Imaging.ImageFormat.Bmp) '儲存-定位孔異常圖片(原圖)
            '-------------------------定位孔異常圖片-結束--------------------------
        End If

        oBinarieImage.Dispose()
        oBinarizeBitmap.Dispose()
        Return True
    End Function

    '' Augustin 230202 New Find Test
    Public Function FindChangeModel(oBitmap As Bitmap, oROI As Rectangle, nLocaterNo As Integer) As Boolean
        '' Augustin 230109 
        Dim oBinarieImage As New ITVImage
        Dim oBinarizeBitmap As Bitmap '二值化Bitmap
        Dim oBinarizeImageHeader As New ImageHeader
        'Dim sSavePath As String = "D:\BIN.Bmp"

        '' Binarize
        oBinarizeBitmap = CType(oBitmap.Clone(), Bitmap) '二值化Bitmap
        oBinarizeImageHeader = GetImageHeader(oBinarizeBitmap)
        moImagePreprocess.BinarizeForSingle(oBinarizeImageHeader, oBinarizeImageHeader, ImagePreprocessConditionAndThreshMode.GREATER_OR_EQUAL, moMyEquipment.MainRecipe.Threshold)
        oBinarieImage.AssignToImageHeader(oBinarizeImageHeader)
        oBinarizeBitmap = oBinarieImage.GetBitmap '二值化Bitmap
        'oBinarizeBitmap.Save(sSavePath, Imaging.ImageFormat.Bmp)

        moResult.Clear()
        'If moFinder.ModelCount = 0 Then Return False
        'moResult.Succeed = moFinder.FindModel(oBinarizeBitmap)
        'moResult.Succeed = moFinder.FindModel(oBitmap)
        moResult.Succeed = FindCircleChangeModel(oBinarizeBitmap, oROI, nLocaterNo) '設定-FindModel-的結果
        If moResult.Succeed = True AndAlso nLocaterNo = 0 Then
            'moResult.Angle = If(moFinder.ModelResult(0).Angle > 180, moFinder.ModelResult(0).Angle - 360, moFinder.ModelResult(0).Angle)
            'moResult.X = moFinder.ModelResult(0).X
            'moResult.Y = moFinder.ModelResult(0).Y
            'moResult.Score = moFinder.ModelResult(0).Score
            moResult.X = Locater1CircleX
            moResult.Y = Locater1CircleY
        ElseIf moResult.Succeed = True AndAlso nLocaterNo = 1 Then
            moResult.X = Locater2CircleX
            moResult.Y = Locater2CircleY
        Else
            '-------------------------定位孔異常圖片-開始--------------------------
            Dim holeBinBmpDirPath As String = "D:\img\HoleLocateBinBmp\" '定位孔異常圖片-資料夾路徑
            If Directory.Exists(holeBinBmpDirPath) = False Then Directory.CreateDirectory(holeBinBmpDirPath)
            Dim holeBinBmpFilePath As String = String.Format("{0}{1:yyyy-MM-dd_HH_mm_ss}-{2}.bmp", holeBinBmpDirPath, DateTime.Now, nLocaterNo)
            oBinarizeBitmap.Save(holeBinBmpFilePath, Imaging.ImageFormat.Bmp) '儲存-定位孔異常圖片(二值化)
            oBitmap.Save(holeBinBmpFilePath, Imaging.ImageFormat.Bmp) '儲存-定位孔異常圖片(原圖)
            '-------------------------定位孔異常圖片-結束--------------------------
            Return False
        End If
        oBinarieImage.Dispose()
        oBinarizeBitmap.Dispose()
        Return True
    End Function

    '' Augustin 221228
    Public Function FindCircle(oBitmap As Bitmap, ROI As Rectangle) As Boolean
        Dim oROI As New ITVImageROI
        Dim oBinarieImage As New ITVImage
        Dim oBinarizeBitmap As Bitmap
        Dim oBinarizeImageHeader As New ImageHeader
        Dim sSavePath As String = moMyEquipment.MainRecipe.BinarizePicturePath

        '' Binarize
        oBinarizeBitmap = CType(oBitmap.Clone(), Bitmap)
        oBinarizeImageHeader = GetImageHeader(oBinarizeBitmap)
        moImagePreprocess.BinarizeForSingle(oBinarizeImageHeader, oBinarizeImageHeader, ImagePreprocessConditionAndThreshMode.GREATER_OR_EQUAL, moMyEquipment.MainRecipe.Threshold)
        oBinarieImage.AssignToImageHeader(oBinarizeImageHeader)
        oBinarizeBitmap = oBinarieImage.GetBitmap
        '' Augustin 230512
        If File.Exists(sSavePath & "\RecipeBIN.Bmp") Then
            Kill(sSavePath & "\RecipeBIN.Bmp")
        End If
        oBinarizeBitmap.Save(sSavePath & "\RecipeBIN.Bmp", Imaging.ImageFormat.Bmp)

        oROI.ApplyRegion(ROI)

        Dim RadiusMax As Double = moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadius + moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadiusTolerance
        Dim RadiusMin As Double = moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadius - moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadiusTolerance

        moCircleFinder.FindCircles(oBinarieImage, RadiusMin, RadiusMax, LineThresholdMode.ThresholdVeryLow, 0, 100, oROI)

        If moCircleFinder.NumberOfResult = 0 Then Return False

        For i As Integer = 0 To moCircleFinder.NumberOfResult - 1
            CircleLocationX = Round(moCircleFinder.Result(i).LocationX, MidpointRounding.AwayFromZero)
            CircleLocationY = Round(moCircleFinder.Result(i).LocationY, MidpointRounding.AwayFromZero)
            CircleRadius = Round(moCircleFinder.Result(i).Radius, MidpointRounding.AwayFromZero)
        Next
        oBinarieImage.Dispose()
        oBinarizeBitmap.Dispose()
        oROI.Dispose()

        Return True
    End Function

    ''' <summary>
    ''' FindCircleChangeModel
    ''' </summary>
    ''' <param name="oBitmap"></param>
    ''' <param name="ROI"></param>
    ''' <param name="nLocaterNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindCircleChangeModel(oBitmap As Bitmap, ROI As Rectangle, nLocaterNo As Integer) As Boolean
        Dim oROI As New ITVImageROI
        Dim oBinarieImage As New ITVImage
        Dim oBinarizeBitmap As Bitmap
        Dim oBinarizeImageHeader As New ImageHeader
        'Dim sSavePath As String = moMyEquipment.MainRecipe.BinarizePicturePath

        '' Binarize
        oBinarizeBitmap = CType(oBitmap.Clone(), Bitmap)
        oBinarizeImageHeader = GetImageHeader(oBinarizeBitmap)
        moImagePreprocess.BinarizeForSingle(oBinarizeImageHeader, oBinarizeImageHeader, ImagePreprocessConditionAndThreshMode.GREATER_OR_EQUAL, moMyEquipment.MainRecipe.Threshold)
        oBinarieImage.AssignToImageHeader(oBinarizeImageHeader)
        oBinarizeBitmap = oBinarieImage.GetBitmap
        'oBinarizeBitmap.Save(sSavePath, Imaging.ImageFormat.Bmp)

        oROI.ApplyRegion(ROI)

        Dim RadiusMax As Double = moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadius + moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadiusTolerance
        Dim RadiusMin As Double = moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadius - moMyEquipment.MainRecipe.RecipeCamera.Locate1.CircleRadiusTolerance

        moCircleFinder.FindCircles(oBinarieImage, RadiusMin, RadiusMax, LineThresholdMode.ThresholdVeryLow, 0, 100, oROI)

        If moCircleFinder.NumberOfResult = 0 Then Return False

        For i As Integer = 0 To moCircleFinder.NumberOfResult - 1
            If nLocaterNo = 0 Then
                Locater1CircleX = Round(moCircleFinder.Result(i).LocationX, MidpointRounding.AwayFromZero)
                Locater1CircleY = Round(moCircleFinder.Result(i).LocationY, MidpointRounding.AwayFromZero)
                Locater1CircleRadius = Round(moCircleFinder.Result(i).Radius, MidpointRounding.AwayFromZero)
            Else
                Locater2CircleX = Round(moCircleFinder.Result(i).LocationX, MidpointRounding.AwayFromZero)
                Locater2CircleY = Round(moCircleFinder.Result(i).LocationY, MidpointRounding.AwayFromZero)
                Locater2CircleRadius = Round(moCircleFinder.Result(i).Radius, MidpointRounding.AwayFromZero)
            End If
        Next
        oBinarieImage.Dispose()
        oBinarizeBitmap.Dispose()
        oROI.Dispose()

        Return True
    End Function

    Public Sub Close()
        moImagePreprocess.Close()
        moBlob.Close()
        moFinder = Nothing
        moCircleFinder = Nothing '' Augustin 221228
    End Sub
End Class