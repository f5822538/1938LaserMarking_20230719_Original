Public Enum CameraVIEWLIST
    CameraVIEWLIST_SEQUENCE = 0
    CameraVIEWLIST_TIME = 1
    CameraVIEWLIST_RECIPE = 2
    CameraVIEWLIST_CODE = 3
    CameraVIEWLIST_DEFECTCOUNT = 4
    CameraVIEWLIST_ALIGNERROR = 5
    CameraVIEWLIST_FINDERROR = 6
    'CameraVIEWLIST_CYCLEERROR = 7
    CameraVIEWLIST_MODELERROR = 7
    CameraVIEWLIST_MODEL_OFFSET_ERROR = 8
    CameraVIEWLIST_MODEL_LOSE_ERROR = 9
    CameraVIEWLIST_REPORT = 10
    CameraVIEWLIST_DM = 11
    CameraVIEWLIST_RAW = 12

    CameraVIEWLIST_NOTHING1 = 13
    CameraVIEWLIST_NOTHING2 = 14
    CameraVIEWLIST_NOTHING3 = 15
    CameraVIEWLIST_NOTHING4 = 16
    CameraVIEWLIST_NOTHING5 = 17
    CameraVIEWLIST_NOTHING6 = 18
    CameraVIEWLIST_COUNT = 19
End Enum

''' <summary>
''' CDefectViewCamera
''' </summary>
''' <remarks></remarks>
Public Class CDefectViewCamera : Inherits ListView

    Private moLogCSV As II_LogTrace

    Public Sub New()
        MyBase.View = View.Details
        MyBase.GridLines = True
        MyBase.FullRowSelect = True

        MyBase.Columns.Add("序列", 60)
        MyBase.Columns.Add("時間", 180)
        MyBase.Columns.Add("Recipe", 100)
        MyBase.Columns.Add("條碼", 180)
        MyBase.Columns.Add("瑕疵", 60)
        MyBase.Columns.Add("對位異常", 80)
        MyBase.Columns.Add("樣板異常", 80)
        'MyBase.Columns.Add("檢測異常 (週期)", 120)
        MyBase.Columns.Add("檢測異常 (樣板)", 120)
        MyBase.Columns.Add("檢測異常 (偏移)", 120)
        MyBase.Columns.Add("檢測異常 (漏雷)", 120)
        MyBase.Columns.Add("檢測異常 (No Die)", 120)  '' Augustin 230310
        MyBase.Columns.Add("檢測報告", 0)
        MyBase.Columns.Add("檢測Map圖", 0)
        MyBase.Columns.Add("檢測原始資料", 0)
    End Sub

    Public Sub UpdateObject(oLog As II_LogTrace)
        moLogCSV = oLog
    End Sub

    'Public Sub AddData(oInspectSumCamera As CInspectSum)
    '    Dim oInspectSum As CInspectSum = oInspectSumCamera

    '    oInspectSum.AddItem(MyBase.Items)

    '    If MyBase.Items.Count > 1000 Then
    '        MyBase.Items.RemoveAt(MyBase.Items.Count - 1)
    '    End If
    'End Sub

    Public Sub AddLineList(sReaded() As String, nData As Integer, nMax As Integer)
        Dim oList As New List(Of ListViewItem)

        For nIndex As Integer = 1 To sReaded.Length '我是nIndex
            Dim aStr() As String = Split(sReaded(nIndex - 1), ",")
            If aStr.Length = nMax Then
                Dim o As New ListViewItem(aStr(0))
                With o
                    .ForeColor = If(aStr(CameraVIEWLIST.CameraVIEWLIST_DEFECTCOUNT) <> "0" OrElse aStr(CameraVIEWLIST.CameraVIEWLIST_ALIGNERROR) = "Y" OrElse aStr(CameraVIEWLIST.CameraVIEWLIST_FINDERROR) = "Y" OrElse aStr(CameraVIEWLIST.CameraVIEWLIST_MODELERROR) = "Y" OrElse aStr(CameraVIEWLIST.CameraVIEWLIST_MODEL_OFFSET_ERROR) = "Y", Drawing.Color.Red, Drawing.Color.Green)

                    For nCount = 1 To nData
                        .SubItems.Add(aStr(nCount))
                    Next
                End With
                oList.Add(o)
            End If
        Next

        MyBase.Items.AddRange(oList.ToArray)
    End Sub

    Private Sub CDefectViewCamera_DoubleClick(sender As Object, e As System.EventArgs) Handles Me.DoubleClick
        If MyBase.SelectedItems.Count > 0 Then
            Dim sValue As String = MyBase.SelectedItems.Item(0).SubItems(CameraVIEWLIST.CameraVIEWLIST_REPORT).Text

            sValue = Replace(sValue, "File:\\", "")

            Call Diagnostics.Process.Start("iexplore.exe", sValue)
        End If
    End Sub

End Class