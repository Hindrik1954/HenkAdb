Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Security.Cryptography

Public Class DbConfig
    Public Property Host As String
    Public Property DatabaseName As String
    Public Property Username As String
    Public Property Password As String
End Class

Public Module DbConfigHelper

    Private ReadOnly Entropy As Byte() = Encoding.UTF8.GetBytes("AdbHenk-DbConfig-v1")

    Public Function GetConfigPath() As String
        Return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "dbconfig.enc")
    End Function

    Public Function ConfigFileExists() As Boolean
        Return File.Exists(GetConfigPath())
    End Function

    Public Sub SaveEncryptedConfig(host As String, databaseName As String, username As String, password As String)
        Dim plainText As String =
            $"Host={host}{Environment.NewLine}" &
            $"Database={databaseName}{Environment.NewLine}" &
            $"Username={username}{Environment.NewLine}" &
            $"Password={password}"

        Dim plainBytes As Byte() = Encoding.UTF8.GetBytes(plainText)
        Dim encryptedBytes As Byte() =
            ProtectedData.Protect(plainBytes, Entropy, DataProtectionScope.LocalMachine)

        File.WriteAllBytes(GetConfigPath(), encryptedBytes)
    End Sub

    Public Function LoadEncryptedConfig() As DbConfig
        Dim filePath As String = GetConfigPath()

        If Not File.Exists(filePath) Then
            Throw New FileNotFoundException("Bestand dbconfig.enc niet gevonden naast AdbHenk.exe")
        End If

        Dim encryptedBytes As Byte() = File.ReadAllBytes(filePath)
        Dim plainBytes As Byte() =
            ProtectedData.Unprotect(encryptedBytes, Entropy, DataProtectionScope.LocalMachine)

        Dim plainText As String = Encoding.UTF8.GetString(plainBytes)
        Dim cfg As New DbConfig()

        For Each line As String In plainText.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
            Dim parts = line.Split(New Char() {"="c}, 2)
            If parts.Length <> 2 Then Continue For

            Select Case parts(0).Trim().ToLower()
                Case "host"
                    cfg.Host = parts(1).Trim()
                Case "database"
                    cfg.DatabaseName = parts(1).Trim()
                Case "username"
                    cfg.Username = parts(1).Trim()
                Case "password"
                    cfg.Password = parts(1).Trim()
            End Select
        Next

        Return cfg
    End Function

    Public Function GetConnectionString() As String
        Dim cfg As DbConfig = LoadEncryptedConfig()
        Return $"Server={cfg.Host};Database={cfg.DatabaseName};Uid={cfg.Username};Pwd={cfg.Password};SslMode=Disabled;"
    End Function

End Module