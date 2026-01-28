Public Class CommDay

    Private Sub CommDay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDevices()
    End Sub

    Private Sub LoadDevices()
        cbDevices.Items.Clear()
        Dim devices = GetDevices()
        For Each d In devices
            cbDevices.Items.Add(d)
        Next
        If cbDevices.Items.Count > 0 Then
            cbDevices.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If cbDevices.SelectedItem Is Nothing Then
            MessageBox.Show("Kies een device.")
            Return
        End If

        Dim coordText As String = tbCoordinaten.Text.Trim()
        If String.IsNullOrWhiteSpace(coordText) Then
            MessageBox.Show("Vul coördinaat in (lat,lon).")
            tbCoordinaten.Focus()
            Return
        End If

        Dim parts = coordText.Split(","c)
        If parts.Length <> 2 Then
            MessageBox.Show("Formaat: 52.3392,4.9150")
            tbCoordinaten.Focus()
            Return
        End If

        Dim lat As String = parts(0).Trim()
        Dim lon As String = parts(1).Trim()

        Dim deviceId As String = cbDevices.SelectedItem.ToString()
        SendTeleportToDevice(deviceId, lat, lon)

        'MessageBox.Show($"Teleport naar {lat},{lon} gestuurd naar {deviceId}.")
    End Sub

    Private Sub btnAllDevices_Click(sender As Object, e As EventArgs) Handles btnAllDevices.Click
        Dim coordText As String = tbCoordinaten.Text.Trim()
        If String.IsNullOrWhiteSpace(coordText) Then
            MessageBox.Show("Vul coördinaat in (lat,lon).")
            tbCoordinaten.Focus()
            Return
        End If

        Dim parts = coordText.Split(","c)
        If parts.Length <> 2 Then
            MessageBox.Show("Formaat: 52.3392,4.9150")
            tbCoordinaten.Focus()
            Return
        End If

        Dim lat As String = parts(0).Trim()
        Dim lon As String = parts(1).Trim()

        Dim devices = GetDevices()
        If devices.Count = 0 Then
            MessageBox.Show("Geen devices gevonden.")
            Return
        End If

        Dim succesvol = 0
        For Each deviceId In devices
            Try
                SendTeleportToDevice(deviceId, lat, lon)
                succesvol += 1
            Catch ex As Exception
                ' log fouten als nodig
            End Try
        Next

        MessageBox.Show($"Teleport naar {lat},{lon} gestuurd naar {succesvol}/{devices.Count} devices.")
    End Sub
    Private Sub CopyVanClipboard_Click(sender As Object, e As EventArgs) Handles CopyVanClipboard.Click
        Try
            tbCoordinaten.Text = Clipboard.GetText()
            'MessageBox.Show("Coördinaat gekopieerd uit clipboard.")
        Catch ex As Exception
            MessageBox.Show("Fout bij kopiëren uit clipboard: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDevices()

    End Sub
End Class
