Imports System.IO

Public Class Toestellen

    Private Sub Toestellen_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LaadToestellen()
    End Sub

    Private Sub LaadToestellen()
        ' KLEUREN zoals Start-form!
        btnKoppelen.BackColor = Color.Green
        btnKoppelen.ForeColor = Color.White

        btnOntkoppel.BackColor = Color.Red
        btnOntkoppel.ForeColor = Color.White

        btnAnnuleer.BackColor = Color.Gray
        btnAnnuleer.ForeColor = Color.White

        btnStopServer.BackColor = Color.Purple
        btnStopServer.ForeColor = Color.White

        dgvToestellen.Rows.Clear()
        dgvToestellen.Columns.Clear()
        ' Checkbox + kolommen (bestaand)
        Dim colChk As New DataGridViewCheckBoxColumn()
        colChk.Name = "Select"
        colChk.HeaderText = ""
        colChk.Width = 50
        dgvToestellen.Columns.Add(colChk)

        dgvToestellen.Columns.Add("Naam", "Naam")
        dgvToestellen.Columns.Add("Conn", "Conn")
        dgvToestellen.Columns("Conn").Visible = False

        dgvToestellen.RowHeadersVisible = False
        dgvToestellen.AllowUserToAddRows = False
        dgvToestellen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Dim filePath = Path.Combine(Application.StartupPath, "Telefoon.txt")
        If Not IO.File.Exists(filePath) Then
            MessageBox.Show("Telefoon.txt mist!")
            Return
        End If

        ' Haal HUIDIGE gekoppelde devices op
        Dim connectedDevices = AdbHelper.GetDevices()

        For Each line In IO.File.ReadAllLines(filePath)
            If line.Contains("|") Then
                Dim parts() = line.Split("|"c)
                Dim naam = parts(0).Trim()
                Dim conn = parts(1).Trim()

                ' Check of reeds gekoppeld (IP:poort matcht device ID)
                Dim isConnected = connectedDevices.Any(Function(d) d.EndsWith(conn))

                dgvToestellen.Rows.Add(isConnected, naam, conn)
            End If
        Next
    End Sub
    Private Sub cbKiesAlle_CheckedChanged(sender As Object, e As EventArgs) Handles cbKiesAlle.CheckedChanged
        For Each r As DataGridViewRow In dgvToestellen.Rows
            r.Cells("Select").Value = cbKiesAlle.Checked
        Next
    End Sub
    Private Sub btnKoppelen_Click(sender As Object, e As EventArgs) Handles btnKoppelen.Click
        VoerConnect("connect")
    End Sub
    Private Sub btnOntkoppel_Click(sender As Object, e As EventArgs) Handles btnOntkoppel.Click
        VoerConnect("disconnect")
    End Sub
    Private Sub VoerConnect(cmd As String)
        Dim count = 0
        For Each r As DataGridViewRow In dgvToestellen.Rows
            Dim chk As Boolean = False
            Boolean.TryParse(r.Cells("Select").Value?.ToString(), chk)
            If chk Then
                Dim conn = r.Cells("Conn").Value.ToString()
                AdbHelper.RunAdbCommand(cmd & " " & conn)
                count += 1
            End If
        Next
        MessageBox.Show(count & " toestel(len) " & cmd & "ed!")
        Me.Close()
    End Sub
    Private Sub btnStopServer_Click(sender As Object, e As EventArgs) Handles btnStopServer.Click
        Try
            AdbHelper.RunAdbCommand("kill-server")
            MessageBox.Show("ADB server gestopt!")
        Catch ex As Exception
            MessageBox.Show("Fout: " & ex.Message)
        End Try

        Me.Close()  ' ← Terug naar Start
    End Sub
    Private Sub btnAnnuleer_Click(sender As Object, e As EventArgs) Handles btnAnnuleer.Click
        Me.Close()
    End Sub
End Class
