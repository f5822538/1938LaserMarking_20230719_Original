Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Threading

Public Class AppMgr
    Shared sInstanceLocation As String = "LaserMarkingX64"
    Shared bIsObsolete As Boolean = False
    Shared oObsoleteMutex As New Mutex(True, sInstanceLocation, bIsObsolete)

    Shared MyHandleLoad As New CHandleLoad
    Public Shared MILApp As MIL_ID = 0
    Public Shared MILSystem As MIL_ID = 0
    Public Shared MILDisplay As MIL_ID = 0
    Public Shared Property StrNoDieFilePath As String = ""

    Shared Sub Main()
        Dim ofrmMain = New frmMain()
        Try
            If Not bIsObsolete Then
                MsgBox("目前程式已經執行，請先確認！", MsgBoxStyle.OkOnly, "銓發科技股份有限公司")
                Exit Sub
            End If

            Dim oDomain As AppDomain = AppDomain.CurrentDomain
            Dim sDLLPath As String = Application.StartupPath & "\DLL\"

            If Directory.Exists(sDLLPath) = False Then Call Directory.CreateDirectory(sDLLPath)
            Dim sEnvPath As String = Environment.GetEnvironmentVariable("path")
            Dim sSetupPath As String = Application.StartupPath & "\Setup\"
            Environment.SetEnvironmentVariable("path", sEnvPath & ";" & sDLLPath)

            MyHandleLoad.LoadNativeDLL(sSetupPath, "DefectReport.xslt")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "ComperiodLib.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "iTVisionInspectCoreVC2010.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "iTVisionImageBaseCore.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "iTVisionImageLibMilCoreV10.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "iTVisionImageLibCore.dll")
            ' --------------------------------------------------------------
            ' 如果將KeyPro所使用的資料複製到System32與SysWOW64下就不需要
            ' --------------------------------------------------------------
            MyHandleLoad.LoadNativeDLL(sDLLPath, "hasp_windows_114385.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "hasp_windows_x64_114385.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "haspdnert.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "haspdnert_x64.dll")
            MyHandleLoad.LoadNativeDLL(sDLLPath, "haspvlib_114385.dll")

            MIL.MappAlloc(MIL.M_DEFAULT, MILApp)
            MIL.MsysAlloc(MIL.M_SYSTEM_HOST, MIL.M_DEFAULT, MIL.M_DEFAULT, MILSystem)
            MIL.MdispAlloc(MILSystem, MIL.M_DEFAULT, "", MIL.M_DEFAULT, MILDisplay)
            iTVisionInspectLib.SetMilObject(CInt(MILApp), CInt(MILSystem), CInt(MILDisplay))
            MIL.MappControl(MIL.M_ERROR, MIL.M_PRINT_DISABLE)
            MIL.MsysControl(MIL.M_DEFAULT_HOST, MIL.M_ALLOCATION_OVERSCAN_SIZE, 0)
            MIL.MsysControl(MIL.M_DEFAULT_HOST, MIL.M_ALLOCATION_OVERSCAN, MIL.M_DISABLE)

            Application.Run(ofrmMain)

            ofrmMain.Dispose()
            MIL.MdispFree(MILDisplay)
            MIL.MsysFree(MILSystem)
            MIL.MappFree(MILApp)
            iTVisionInspectLib.SetMilObject(0, 0, 0)
        Catch ex As Exception
            MsgBox(ex.Message & Environment.NewLine & ex.StackTrace, MsgBoxStyle.OkOnly, "錯誤")
        End Try
    End Sub
End Class