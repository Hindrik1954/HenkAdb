Module HenkAdb1
    Public Class IniFile
        Private Declare Ansi Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" _
            (ByVal section As String, ByVal key As String, ByVal def As String,
             ByVal retVal As System.Text.StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer

        Private Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" _
            (ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Integer

        Private iniPath As String

        Public Sub New(ByVal iniPath As String)
            Me.iniPath = iniPath
        End Sub

        Public Function ReadString(section As String, key As String, defaultValue As String) As String
            Dim buffer As New System.Text.StringBuilder(255)
            GetPrivateProfileString(section, key, defaultValue, buffer, buffer.Capacity, iniPath)
            Return buffer.ToString()
        End Function

        Public Sub WriteString(section As String, key As String, value As String)
            WritePrivateProfileString(section, key, value, iniPath)
        End Sub
    End Class
End Module
