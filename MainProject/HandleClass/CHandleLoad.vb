Public Class CHandleLoad

    Public Sub New()
        Dim oDomain As AppDomain = AppDomain.CurrentDomain
        AddHandler oDomain.AssemblyResolve, AddressOf DomainResolveEventHandler
    End Sub

    Public Function LoadNativeDLL(sPath As String, sName As String) As Assembly
        Dim sResourceName As String = Assembly.GetExecutingAssembly.GetName.Name & "." & sName

        Dim aAssemblyData As Byte()

        Using stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sResourceName)
            aAssemblyData = New Byte(CInt(stream.Length - 1)) {}
            stream.Read(aAssemblyData, 0, aAssemblyData.Length)
        End Using

        Dim bExistFileOk As Boolean = False
        Dim sTempFile As String = ""

        Using aSHA As New SHA1CryptoServiceProvider()
            Dim sFileHash As String = BitConverter.ToString(aSHA.ComputeHash(aAssemblyData)).Replace("-", String.Empty)

            sTempFile = sPath + sName

            If File.Exists(sTempFile) Then
                Dim aExistFileData As Byte() = File.ReadAllBytes(sTempFile)
                Dim sExistFileHash As String = BitConverter.ToString(aSHA.ComputeHash(aExistFileData)).Replace("-", String.Empty)

                If sFileHash = sExistFileHash Then
                    bExistFileOk = True
                Else
                    bExistFileOk = False
                End If
            Else
                bExistFileOk = False
            End If
        End Using

        File.Delete(Application.StartupPath & "\" & sName)

        If bExistFileOk = False Then
            System.IO.File.WriteAllBytes(sTempFile, aAssemblyData)
        End If

        Debug.Print("Created : " & sTempFile)

        Return Nothing
    End Function

    Private Shared Function DomainResolveEventHandler(sender As Object, args As ResolveEventArgs) As Assembly

        Dim sResourceName1Lower As String = Assembly.GetExecutingAssembly.GetName.Name & ".xx" + New AssemblyName(args.Name).Name + ".dll"
        Dim sResourceName2Lower As String = Assembly.GetExecutingAssembly.GetName.Name & "." + New AssemblyName(args.Name).Name + ".dll"
        Dim sResourceName2Upper As String = Assembly.GetExecutingAssembly.GetName.Name & "." + New AssemblyName(args.Name).Name + ".DLL"

        File.Delete(Application.StartupPath & "\" & args.Name & ".dll")

        Dim oStream As Stream

        oStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sResourceName1Lower)

        If oStream IsNot Nothing Then
            Debug.Print(sResourceName1Lower)
            Dim aAssemblyData As Byte() = New Byte(CInt(oStream.Length - 1)) {}
            oStream.Read(aAssemblyData, 0, aAssemblyData.Length)

            For nCount As Integer = 0 To aAssemblyData.Length - 1
                aAssemblyData(nCount) = CByte(aAssemblyData(nCount) Xor 10)
            Next

            Return Assembly.Load(aAssemblyData)
        End If

        oStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sResourceName2Lower)

        If oStream IsNot Nothing Then
            Debug.Print("===>" & sResourceName2Lower)
            Dim aAssemblyData As Byte() = New Byte(CInt(oStream.Length - 1)) {}
            oStream.Read(aAssemblyData, 0, aAssemblyData.Length)
            Return Assembly.Load(aAssemblyData)
        End If

        oStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sResourceName2Upper)

        If oStream IsNot Nothing Then
            Debug.Print("===>" & sResourceName2Upper)
            Dim aAssemblyData As Byte() = New Byte(CInt(oStream.Length - 1)) {}
            oStream.Read(aAssemblyData, 0, aAssemblyData.Length)
            Return Assembly.Load(aAssemblyData)
        End If

        Return Nothing
    End Function

End Class