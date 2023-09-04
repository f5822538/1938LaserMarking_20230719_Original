Public Class usrDefectView

    Private moImgList As New List(Of PictureBox)
    Public moBitmap As New Bitmap(10, 10)

    ''' <summary>
    ''' frmMain.moAutoRunThread_AutoRunFinished -> usrDefectView.AddData
    ''' </summary>
    ''' <param name="oInspectSumCamera"></param>
    ''' <remarks></remarks>
    Public Sub AddData(oInspectSumCamera As CInspectSum)
        oInspectSumCamera.AddItem(DefectViewCamera.Items) '¦Cªí§Ç

        If DefectViewCamera.Items.Count > 1000 Then
            DefectViewCamera.Items.RemoveAt(DefectViewCamera.Items.Count - 1)
        End If
    End Sub

    Public Sub AddToPictureName(ByVal sFileName As String)
        If IO.File.Exists(sFileName) = True Then
            Dim oImage = moImgList.Item(moImgList.Count - 2).Image

            For nCount As Integer = moImgList.Count To 2 Step -1
                moImgList.Item(nCount - 1).Image = moImgList.Item(nCount - 2).Image
            Next

            moImgList.Item(0).Image = oImage
            moImgList.Item(0).Image = Image.FromFile(sFileName)
        End If
    End Sub

    Public Sub ClearPicture()
        For Each o In moImgList
            o.Image = moBitmap
        Next
    End Sub

    Public Sub New()
        InitializeComponent()

        moImgList.Add(PIC1)
        moImgList.Add(PIC2)
        moImgList.Add(PIC3)
        moImgList.Add(PIC4)

        ClearPicture()
    End Sub

    Private Sub DefectView_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        ClearPicture()
    End Sub

    Private Sub DefectViewCamera_ItemSelectionChanged(sender As System.Object, e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles DefectViewCamera.ItemSelectionChanged
        If DefectViewCamera.SelectedItems.Count > 0 Then
            Try
                Dim sValue As String = DefectViewCamera.SelectedItems.Item(0).SubItems(CameraVIEWLIST.CameraVIEWLIST_DM).Text

                sValue = Replace(sValue, "File:\\", "")

                If IO.File.Exists(sValue) = True Then
                    picDM.Image = Image.FromFile(sValue)
                Else
                    picDM.Image = Nothing
                End If
            Catch ex As System.Exception

            End Try
        End If
    End Sub

End Class