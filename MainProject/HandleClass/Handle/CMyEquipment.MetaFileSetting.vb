Partial Class CMyEquipment

    Private moMetaFileSetting As New CMetaFileSetting
    Private mbIsFirstCache As Boolean = False

    Public Sub SetSystemCache()
        Dim nMin As Long
        Dim nMax As Long
        Dim nFlag As FileCacheFlags

        Call moMetaFileSetting.EmptyWorkingSetProcess()

        If mbIsFirstCache = False Then
            Call moMetaFileSetting.ClearSystemFileCacheSize()

            If moMetaFileSetting.GetSystemFileCacheSize(nMin, nMax, nFlag) = True Then
                Call LogSystem.LogInformation(String.Format("File Meta Cache min={0}, max={1}, Flag={2}", nMin, nMax, nFlag))
            End If

            If moMetaFileSetting.SetSystemFileCacheSize(10240, 124001) = True Then
                If moMetaFileSetting.GetSystemFileCacheSize(nMin, nMax, nFlag) = True Then
                    Call LogSystem.LogInformation(String.Format("After File Meta Cache min={0}, max={1}, Flag={2}", nMin, nMax, nFlag))
                End If
            End If
            mbIsFirstCache = True
        End If
    End Sub
End Class