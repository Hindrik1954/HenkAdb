Imports System.Diagnostics

Public Module AdbHelper

    Public Property AdbPath As String = "C:\NemoAdb\adb.exe"

    Public Function RunAdbCommand(arguments As String) As String
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
            Return output & If(String.IsNullOrEmpty(err), "", Environment.NewLine & err)
        End Using
    End Function

    Public Function GetDevices() As List(Of String)
        Dim result As String = RunAdbCommand("devices")
        Dim lines = result.Split({ControlChars.Lf, ControlChars.Cr}, StringSplitOptions.RemoveEmptyEntries)

        Dim list As New List(Of String)
        For Each line In lines
            If line.Contains("device") AndAlso Not line.StartsWith("List of devices") Then
                Dim parts = line.Split(ControlChars.Tab)
                If parts.Length > 0 Then list.Add(parts(0))
            End If
        Next
        Return list
    End Function

    Public Sub SendTeleportToDevice(deviceId As String, latitude As String, longitude As String)
        Dim args As String = $"-s {deviceId} shell am startservice -a theappninjas.gpsjoystick.TELEPORT --ef lat {latitude} --ef lng {longitude} --ef alt 0"
        RunAdbCommand(args)
    End Sub

End Module
