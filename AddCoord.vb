Imports System.IO

Public Class AddCoord

    Private Sub AddCoord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbCoord.ReadOnly = True
        tbCoord.BackColor = SystemColors.ControlLight
    End Sub

    Private Sub btnAnnuleer_Click(sender As Object, e As EventArgs) Handles btnAnnuleer.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(tbCoord.Text) OrElse String.IsNullOrWhiteSpace(tbOmsCoord.Text) Then
            MessageBox.Show("Vul beide velden in.")
            Return
        End If

        Dim appDir = Path.GetDirectoryName(Application.ExecutablePath)
        Dim filePath = Path.Combine(appDir, "CoordInfo.txt")
        Dim line As String = tbCoord.Text.Trim() & "|" & tbOmsCoord.Text.Trim()
        File.AppendAllLines(filePath, {line})

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class
