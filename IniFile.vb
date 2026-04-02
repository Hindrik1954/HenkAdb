Imports System.Runtime.InteropServices
Imports System.Text

Public Class IniFile
    Private ReadOnly _path As String

    Public Sub New(path As String)
        _path = path
    End Sub

    <DllImport("kernel32", CharSet:=CharSet.Unicode)>
    Private Shared Function GetPrivateProfileString(
        section As String,
        key As String,
        defaultValue As String,
        retVal As StringBuilder,
        size As Integer,
        filePath As String
    ) As Integer
    End Function

    <DllImport("kernel32", CharSet:=CharSet.Unicode)>
    Private Shared Function WritePrivateProfileString(
        section As String,
        key As String,
        value As String,
        filePath As String
    ) As Integer
    End Function

    Public Function ReadValue(section As String, key As String, defaultValue As String) As String
        Dim sb As New StringBuilder(255)
        GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, _path)
        Return sb.ToString()
    End Function

    Public Sub WriteValue(section As String, key As String, value As String)
        WritePrivateProfileString(section, key, value, _path)
    End Sub
End Class
