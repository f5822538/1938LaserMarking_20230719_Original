﻿Imports System.Net.WebRequestMethods.Ftp
Public Class CMyEquipment

    Private moHardwareConfig As CHardwareConfig
    Private moMainRecipe As CMainRecipe
    Private moInnerThread As CInnerThread
    Private moUpdateStatus As CMyUpdateStatus

    Public YieldConfig As CYieldConfig
    Public Sequence As Integer = 0

    Public ReadOnly Property HardwareConfig As CHardwareConfig
        Get
            Return moHardwareConfig
        End Get
    End Property

    Public ReadOnly Property MainRecipe As CMainRecipe
        Get
            Return moMainRecipe
        End Get
    End Property

    Public ReadOnly Property InnerThread As CInnerThread
        Get
            Return moInnerThread
        End Get
    End Property

    Public ReadOnly Property UpdateStatus As CMyUpdateStatus
        Get
            Return moUpdateStatus
        End Get
    End Property

    Public Sub New(oHardwareConfig As CHardwareConfig, oMainRecipe As CMainRecipe, oSync As SynchronizationContext)
        '-------------------------20231019-開始--------------------------
        SynchronizationContext.SetSynchronizationContext(oSync)
        '-------------------------20231019-結束--------------------------


        moHardwareConfig = oHardwareConfig
        moMainRecipe = oMainRecipe

        moUserProfileFile = New CUserProfileFile(Application.StartupPath & "\Setup", "UserList", "INI")
        moUserProfileFile.LoadConfig("")

        moUpdateStatus = New CMyUpdateStatus(oSync)
        moIO = New CMyIO
    End Sub

    ''' <summary>
    ''' frmMain.frmMain_Load -> CMyEquipment.Initial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial() As Boolean
        Dim IsOk As Boolean = True

        If CreateDIO3208() = False Then IsOk = False
        If InitialCamera() = False Then IsOk = False '初始化-相機
        If InitialHandshake() = False Then IsOk = False 'Server 開啟

        If moHardwareConfig.MiscConfig.IsUseModelFinder = True Then
            If InitialLocater() = False Then
                IsOk = False
            End If
        End If

        If moHardwareConfig.CodeReaderBypass = False Then '條碼讀取不Bypass
            If InitialCodeReader() = False Then
                IsOk = False
            End If
        End If

        moInnerThread = New CInnerThread(Me) '初始化-CInnerThread

        Dim oAlarmCode As AlarmCode = LightVacuumDown(LogSystem) '燈源下降
        If oAlarmCode <> AlarmCode.IsOK Then
            TriggerAlarm(oAlarmCode) : Return False
        End If

        Call SetLightOff(LogSystem) '關閉-燈源

        Dim sFileName As String = String.Format("{0:yyyy-MM-dd}", Date.Now)
        YieldConfig = New CYieldConfig(Application.StartupPath & "\Yield", sFileName, "INI")
        YieldConfig.LoadConfig(sFileName)

        Return IsOk
    End Function

    Public Sub Close()
        Call LightVacuumDown(LogSystem)
        Call SetLightOff(LogSystem) '關閉-燈源

        Call CameraClose()
        Call CloseHandshake()
        Call moImageProcess.Close()
        Call moMainRecipe.RecipeCamera.RecipeModelDiff.PatternMatching1St.Close()
        Call moMainRecipe.RecipeCamera.RecipeModelDiff.PatternMatching2Nd.Close()
        Call ClodeCodeReader()
    End Sub
End Class

Partial Class CMyEquipment

    Public Property CurrentUser As New CUserProfile With {.UserName = "Default", .PassWord = "Default", .Level = USERLEVEL.USER_Operator}
    Private moUserProfileFile As CUserProfileFile

    Public Event UpdateTitle()

    Public ReadOnly Property UserProfileFile As CUserProfileFile
        Get
            Return moUserProfileFile
        End Get
    End Property

    Public Sub RaiseUpdateTitle()
        RaiseEvent UpdateTitle()
    End Sub
End Class