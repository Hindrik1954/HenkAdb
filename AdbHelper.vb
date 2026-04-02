Imports System.Diagnostics
Imports System.IO

Public Module AdbHelper
    Private ini As IniFile
    Private m_AdbPath As String = ""  ' ← DIT ONDERAAN MODULE

    Public Property AdbPath As String
        Get
            If String.IsNullOrEmpty(m_AdbPath) Then
                InitializeAdbPath()
            End If
            Return m_AdbPath
        End Get
        Set(value As String)
            m_AdbPath = value
            If Not value.EndsWith("adb.exe") Then m_AdbPath &= "\adb.exe"
            If ini IsNot Nothing Then
                'ini.WriteString("Settings", "PadAdb", m_AdbPath)
                ini.WriteValue("Settings", "PadAdb", m_AdbPath)
            End If
        End Set
    End Property

    Public Sub InitializeAdbPath()
        If ini Is Nothing Then
            ini = New IniFile(GetIniPath())
            m_AdbPath = ini.ReadValue("Settings", "PadAdb", "")  ' ← direct m_AdbPath invullen!
        End If

        ' Valideer direct op backing field
        If String.IsNullOrEmpty(m_AdbPath) OrElse Not File.Exists(m_AdbPath) Then
            SelectAdbPath()
        End If

        ' Test ADB direct
        Try
            Dim testResult = RunAdbCommand("version")
            If Not testResult.Contains("Android Debug Bridge") Then
                Throw New Exception("ADB reageert niet correct")
            End If
        Catch
            SelectAdbPath()
        End Try
    End Sub



    Private Function GetIniPath() As String
        Dim appDir = Path.GetDirectoryName(Application.ExecutablePath)
        Return Path.Combine(appDir, "HenkAdb.ini")
    End Function

    Private Sub SelectAdbPath()
        Using fbd As New FolderBrowserDialog()
            fbd.Description = "Selecteer map met adb.exe"
            fbd.ShowNewFolderButton = False
            If fbd.ShowDialog() = DialogResult.OK Then
                Dim testPath = Path.Combine(fbd.SelectedPath, "adb.exe")
                If File.Exists(testPath) Then
                    AdbPath = testPath
                Else
                    MessageBox.Show("adb.exe niet gevonden in geselecteerde map!")
                    Application.Exit()
                End If
            Else
                MessageBox.Show("Geen ADB map geselecteerd. App wordt afgesloten.")
                Application.Exit()
            End If
        End Using
    End Sub

    ' Bestaande functies blijven identiek:
    Public Function RunAdbCommand(arguments As String) As String
        Logger.Log($"ADB CALL: {AdbPath} {arguments}")
        Dim psi As New ProcessStartInfo()
        psi.FileName = AdbPath
        psi.Arguments = arguments
        psi.UseShellExecute = False
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.CreateNoWindow = True

        Using p As New Process()
            p.StartInfo = psi
            p.Start()
            Dim output As String = p.StandardOutput.ReadToEnd()
            Dim err As String = p.StandardError.ReadToEnd()
            p.WaitForExit()
            Dim result = output & If(String.IsNullOrEmpty(err), "", Environment.NewLine & err)
            Dim logLevel = If(err.Length > 0, "WARN", "INFO")
            Logger.Log($"ADB RESULT ({logLevel}): {result.Trim()}", logLevel)
            Return result
        End Using
    End Function
    Public Function GetDevices() As List(Of String)
        Logger.Log("Fetching ADB devices...")
        Dim result As String = RunAdbCommand("devices")
        Dim lines = result.Split({ControlChars.Lf, ControlChars.Cr}, StringSplitOptions.RemoveEmptyEntries)
        Dim list As New List(Of String)

        For Each line In lines
            If line.Contains("device") AndAlso Not line.StartsWith("List of devices") Then
                Dim parts = line.Split(ControlChars.Tab)
                If parts.Length > 0 Then list.Add(parts(0))
            End If
        Next
        Logger.Log($"Found {list.Count} online devices: [{String.Join(", ", list)}]")
        Return list
    End Function

    Public Sub SendTeleportToDevice(deviceId As String, latitude As String, longitude As String)
        'Dim args As String = $"-s {deviceId} shell am startservice -a theappninjas.gpsjoystick.TELEPORT --ef lat {latitude} --ef lng {longitude} --ef alt 0"
        'RunAdbCommand(args)
        Dim args As String = $"-s {deviceId} shell am start-foreground-service -a theappninjas.gpsjoystick.TELEPORT --ef lat {latitude} --ef lng {longitude}"
        RunAdbCommand(args)
    End Sub

    Public Sub SendRouteToDevice(deviceId As String, routeName As String)
        Dim args As String = $"-s {deviceId} shell am startservice -a theappninjas.gpsjoystick.ROUTE --es name ""{routeName}"""
        RunAdbCommand(args)
    End Sub

    Public Function IsDeviceConnected(deviceId As String) As Boolean
        Dim devices = GetDevices()
        Return devices.Contains(deviceId)
    End Function

    Public Function GetDeviceStatus(deviceId As String) As String
        Dim result = RunAdbCommand($"-s {deviceId} get-state")
        If result.Contains("device") Then Return "online"
        If result.Contains("offline") Or result.Contains("unauthorized") Then Return result.Trim()
        Return "unknown/not connected"
    End Function

End Module
