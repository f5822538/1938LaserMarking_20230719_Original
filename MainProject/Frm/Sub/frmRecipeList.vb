Public Class FrmRecipeList

    Private moFileInfoList As New List(Of FileInfo)
    Private msInitialPath As String
    Private moCurrentRecipe As CMainRecipe
    Private moMyEquipment As CMyEquipment
    Private moMyInnerThread As CInnerThread
    Private moLog As II_LogTraceExtend

    Public Sub New(oMyEquipment As CMyEquipment, sInitialPath As String)
        InitializeComponent()

        moMyEquipment = oMyEquipment
        moMyInnerThread = oMyEquipment.InnerThread
        moCurrentRecipe = oMyEquipment.MainRecipe
        moLog = oMyEquipment.LogSystem
        msInitialPath = sInitialPath

        Call moLog.LogInformation(String.Format("製程參數檔案路徑[{0}\Recipe]", msInitialPath))
    End Sub

    Private Sub FrmRecipeList_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        moFileInfoList = (From o In New DirectoryInfo(msInitialPath & "\Recipe").GetFiles("*.rcp") Order By o.Name).ToList

        BindingSourceFile.DataSource = moFileInfoList

        If moMyEquipment.CurrentUser.Level = USERLEVEL.USER_Operator Then
            btnRecipeAdd.Enabled = False
            btnRecipeDelete.Enabled = False
            btnSaveAs.Enabled = False
        Else
            btnRecipeAdd.Enabled = True
            btnRecipeDelete.Enabled = True
            btnSaveAs.Enabled = True
        End If
    End Sub

    Private Sub btnRecipeAdd_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnRecipeAdd.ClickButtonArea
        Me.SendToBack()

        Dim sName As String = InputBox("輸入新Recipe名稱", "Recipe設定")
        If sName = "" Then Exit Sub

        'Try
        '    Dim nRecipeID As Integer = CInt(sName)
        'Catch ex As Exception
        '    Call MsgBox("製程名稱含有非數字字元！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '    Call moMyEquipment.TriggerWarning(AlarmCode.IsRecipeIDNotNumber)
        '    Exit Sub
        'End Try

        If My.Computer.FileSystem.FileExists(String.Format("{0}\Recipe\{1}.RCP", msInitialPath, sName)) = True Then
            Call MsgBox("檔案已有，請輸入新名稱！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        Else
            Call moLog.LogInformation(String.Format("新增輸入的 {0}\Recipe\{1}", msInitialPath, sName))
            Dim oRecipe As New CMainRecipe(String.Format("{0}\Recipe", msInitialPath), moMyEquipment.HardwareConfig.MiscConfig.DefectSizeType = DefectSizeType.DefectAnd, moMyEquipment.HardwareConfig.CameraConfig.PixelSize)
            oRecipe.SaveConfig(sName)
            moFileInfoList = (From o In New DirectoryInfo(msInitialPath & "\Recipe").GetFiles("*.rcp") Order By o.Name).ToList
            BindingSourceFile.DataSource = moFileInfoList
            Dim sTest As String = String.Format("{0}.rcp", sName).ToUpper
            moFileInfoList.ForEach(
                Sub(x)
                    If x.Name.ToUpper = sTest Then
                        BindingSourceFile.Position = moFileInfoList.IndexOf(x)
                    End If
                End Sub)
        End If
    End Sub

    Private Sub btnRecipeDelete_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnRecipeDelete.ClickButtonArea
        If BindingSourceFile.Position >= 0 Then
            Dim sName As String = moFileInfoList(BindingSourceFile.Position).Name.ToUpper
            If MsgBox("確認刪除？", MsgBoxStyle.YesNo, "銓發科技股份有限公司") = MsgBoxResult.No Then Return
            If File.Exists(String.Format("{0}\Recipe\{1}.bmp", msInitialPath, Replace(sName, ".RCP", ""))) Then
                File.Delete(String.Format("{0}\Recipe\{1}.bmp", msInitialPath, Replace(sName, ".RCP", "")))
            End If

            If File.Exists(String.Format("{0}\Recipe\{1}CodeReader.bmp", msInitialPath, Replace(sName, ".RCP", ""))) Then
                File.Delete(String.Format("{0}\Recipe\{1}CodeReader.bmp", msInitialPath, Replace(sName, ".RCP", "")))
            End If

            For Each sFileName As String In Directory.GetFiles(String.Format("{0}\Recipe", msInitialPath), String.Format("{0}Summation*.BMP", Replace(sName, ".RCP", "")))
                File.Delete(sFileName)
            Next

            Call My.Computer.FileSystem.DeleteFile(String.Format("{0}\Recipe\{1}", msInitialPath, sName))
            Call moLog.LogInformation(String.Format("刪除選定的 {0}Recipe\{1}", msInitialPath, sName))

            moFileInfoList = (From o In New DirectoryInfo(msInitialPath & "\Recipe").GetFiles("*.rcp") Order By o.Name).ToList
            BindingSourceFile.DataSource = moFileInfoList
        End If
    End Sub

    Private Sub btnLoadRecipe_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnLoadRecipe.ClickButtonArea
        If BindingSourceFile.Position >= 0 Then
            Dim sName As String = moFileInfoList(BindingSourceFile.Position).Name.ToUpper
            sName = sName.Replace(".RCP", "")

            'Try
            '    Dim nRecipeID As Integer = CInt(sName)
            'Catch ex As Exception
            '    Call MsgBox("製程名稱含有非數字字元！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            '    Call moMyEquipment.TriggerWarning(AlarmCode.IsRecipeIDNotNumber)
            '    Exit Sub
            'End Try

            Call moCurrentRecipe.LoadConfig(sName)
            Call UpdateRecipeModel()

            If moMyEquipment.HardwareConfig.CodeReaderBypass = False Then
                Call moMyEquipment.CodeReader.SetParameter(moCurrentRecipe.RecipeCamera.CodeReader)
                Call moMyEquipment.CodeReaderForInspect.SetParameter(moCurrentRecipe.RecipeCamera.CodeReaderForInspect)
                Call moMyEquipment.CodeReaderForInspect2.SetParameter(moCurrentRecipe.RecipeCamera.CodeReaderForInspect2)
            End If

            Call moMyEquipment.RaiseUpdateTitle()
            If moMyEquipment.Camera.Camera.IsNullCamera() = False AndAlso moMyEquipment.Camera.ChangeExposure(moCurrentRecipe.ExposureTime, "檢測相機", moLog) = False Then MsgBox("設定 [檢測相機] 曝光時間，失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            If moMyEquipment.CodeReaderCamera.Camera.IsNullCamera() = False AndAlso moMyEquipment.CodeReaderCamera.ChangeExposure(moCurrentRecipe.RecipeCamera.CodeReader.CodeReaderExposureTime1, "條碼相機", moLog) = False Then MsgBox("設定 [條碼相機] 曝光時間，失敗！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        End If
        Call Me.Dispose()
    End Sub

    Private Sub btnSaveAs_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnSaveAs.ClickButtonArea
        If moCurrentRecipe.RecipeID = "Default" Then
            Call MsgBox("請先選擇 Recipe！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Exit Sub
        End If

        Me.SendToBack()

        Dim sName As String = InputBox("輸入新 Recipe 名稱", "Recipe設定")

        'Try
        '    Dim nRecipeID As Integer = CInt(sName)
        'Catch ex As Exception
        '    Call MsgBox("製程名稱含有非數字字元！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        '    Call moMyEquipment.TriggerWarning(AlarmCode.IsRecipeIDNotNumber)
        '    Exit Sub
        'End Try

        If My.Computer.FileSystem.FileExists(String.Format("{0}\Recipe\{1}.RCP", msInitialPath, sName)) = True Then
            Call MsgBox("檔案重複，請重新命名！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
        Else
            Call moLog.LogInformation(String.Format("新增輸入的 {0}\Recipe\{1}", msInitialPath, sName))

            Dim sOldName As String = moCurrentRecipe.RecipeID
            Dim sOldImage As String = moCurrentRecipe.RecipeCamera.TempleteImagePath
            Dim sOldCodeReaderImage As String = moCurrentRecipe.RecipeCamera.CodeReaderImagePath

            moCurrentRecipe.RecipeCamera.TempleteImagePath = moCurrentRecipe.RecipeCamera.TempleteImagePath.Replace(sOldName & ".bmp", sName & ".bmp")
            moCurrentRecipe.RecipeCamera.CodeReaderImagePath = moCurrentRecipe.RecipeCamera.CodeReaderImagePath.Replace(sOldName & "CodeReader.bmp", sName & "CodeReader.bmp")
            moCurrentRecipe.SaveConfig(sName)
            If System.IO.File.Exists(sOldImage) Then
                Try
                    System.IO.File.Copy(sOldImage, moCurrentRecipe.RecipeCamera.TempleteImagePath, True)
                Catch ex As Exception

                End Try
            End If
            If System.IO.File.Exists(sOldCodeReaderImage) Then
                Try
                    System.IO.File.Copy(sOldCodeReaderImage, moCurrentRecipe.RecipeCamera.CodeReaderImagePath, True)
                Catch ex As Exception

                End Try
            End If

            moCurrentRecipe.LoadConfig(sOldName)
            UpdateRecipeModel()

            moFileInfoList = (From o In New DirectoryInfo(msInitialPath & "\Recipe").GetFiles("*.rcp") Order By o.Name).ToList
            BindingSourceFile.DataSource = moFileInfoList
            Dim sTest As String = String.Format("{0}.rcp", sName).ToUpper
            moFileInfoList.ForEach(
                Sub(x)
                    If x.Name.ToUpper = sTest Then
                        BindingSourceFile.Position = moFileInfoList.IndexOf(x)
                    End If
                End Sub)
        End If
    End Sub

    Private Sub btnQuit_ClickButtonArea(Sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnQuit.ClickButtonArea
        Call Me.Close()
    End Sub

    Private Sub UpdateRecipeModel()
        If moMyEquipment.HardwareConfig.InspectBypass = True Then Exit Sub
        GC.Collect()
        If moMyEquipment.BuildImageForLoad(moCurrentRecipe.RecipeCamera.TempleteImagePath, moMyEquipment.ImageID, moMyEquipment.ImageHeader, -1, moMyEquipment.LogSystem) = True Then
            UpdateModelList(moCurrentRecipe.RecipeCamera.RecipeModelDiff, moMyEquipment.ImageID, moMyEquipment.MainRecipe.RecipeID)
        Else
            Dim sInformation As String = "圖檔載入失敗！"
            Call MsgBox(sInformation, MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
            Call moLog.LogError(sInformation)
            DeleteModel(moCurrentRecipe.RecipeCamera.RecipeModelDiff)
        End If

        If moCurrentRecipe.RecipeCamera.Locate1.PatternZone = Rectangle.Empty OrElse moCurrentRecipe.RecipeCamera.Locate2.PatternZone = Rectangle.Empty OrElse moMyEquipment.AddModel(moMyEquipment.ImageID, moMyEquipment.ImageHeader, moCurrentRecipe.RecipeCamera.Locate1.PatternZone, moCurrentRecipe.RecipeCamera.Locate2.PatternZone) = False Then
            MsgBox("Camera Add Model 失敗！", MsgBoxStyle.OkOnly, "警告")
        End If

        '' Augustin 220726 Add for Wafer Map
        Call moMyEquipment.WaferMapCreate()
        Call moMyEquipment.UpdateDefectROI()

        GC.Collect()
    End Sub
End Class