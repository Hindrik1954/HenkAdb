Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.IO
Public Class HenkAdb
    Private oldCoordText As String = ""
    Private Sub CommDay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' EERST ADB INITIALISEREN
        AdbHelper.InitializeAdbPath()
        LoadDevices()

        ' GROEN MAKEN BIJ START
        btnAllDevices.BackColor = Color.Green
        btnAllDevices.ForeColor = Color.White
        btnCopyVanClipboard.BackColor = Color.Blue
        btnCopyVanClipboard.ForeColor = Color.White
        btnToestellen.BackColor = Color.Orange
        btnToestellen.ForeColor = Color.Black
        ' NIEUW: bekende coördinaten laden
        LoadKnownCoords()
    End Sub
    Private Sub LoadDevices()
        dgvDevices.Rows.Clear()
        If dgvDevices.Columns("Select") Is Nothing Then
            Dim colSelect As New DataGridViewCheckBoxColumn()
            colSelect.Name = "Select"
            colSelect.HeaderText = "Select"
            colSelect.Width = 60
            dgvDevices.Columns.Insert(0, colSelect)
        End If
        If dgvDevices.Columns("DeviceID") Is Nothing Then
            dgvDevices.Columns.Add("DeviceID", "Device ID")
        End If
        Dim devices = GetDevices()
        For Each d In devices
            dgvDevices.Rows.Add(False, d)
        Next
        dgvDevices.RowHeadersVisible = False
        dgvDevices.Columns("DeviceID").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvDevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub
    Private Sub chbAllDevices_CheckedChanged(sender As Object, e As EventArgs) Handles chbAllDevices.CheckedChanged
        For Each row As DataGridViewRow In dgvDevices.Rows
            row.Cells("Select").Value = chbAllDevices.Checked
        Next
    End Sub

    Private Sub dgvDevices_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDevices.CellClick
        If e.RowIndex < 0 OrElse e.ColumnIndex <> 1 Then Return
        Dim deviceId As String = dgvDevices.Rows(e.RowIndex).Cells("DeviceID").Value.ToString()
        Dim inputText As String = tbCoordinaten.Text.Trim()
        If String.IsNullOrWhiteSpace(inputText) Then
            MessageBox.Show("Vul coördinaat of routenaam in.")
            tbCoordinaten.Focus()
            Return
        End If

        Dim parts = inputText.Split(","c)
        If parts.Length = 2 Then
            ' Coördinaten: TELEPORT
            SendTeleportToDevice(deviceId, parts(0).Trim(), parts(1).Trim())
            MessageBox.Show($"Teleport {inputText} → {deviceId}")
        Else
            ' Routenaam: ROUTE
            SendRouteToDevice(deviceId, inputText)
            MessageBox.Show($"Route '{inputText}' → {deviceId}")
        End If
    End Sub

    Private Sub dgvDevices_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvDevices.CurrentCellDirtyStateChanged
        If dgvDevices.IsCurrentCellInEditMode Then dgvDevices.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub btnAllDevices_Click(sender As Object, e As EventArgs) Handles btnAllDevices.Click
        Dim inputText As String = tbCoordinaten.Text.Trim()
        If String.IsNullOrWhiteSpace(inputText) Then
            MessageBox.Show("Vul coördinaat of routenaam in.")
            tbCoordinaten.Focus()
            Return
        End If

        Dim parts = inputText.Split(","c)
        Dim succesvol = 0

        For Each row As DataGridViewRow In dgvDevices.Rows
            If row.Cells("Select").Value = True Then
                Dim deviceId As String = row.Cells("DeviceID").Value.ToString()
                Try
                    If parts.Length = 2 Then
                        SendTeleportToDevice(deviceId, parts(0).Trim(), parts(1).Trim())
                    Else
                        SendRouteToDevice(deviceId, inputText)
                    End If
                    succesvol += 1
                Catch
                End Try
            End If
        Next

    End Sub
    Private Sub btnToestellen_Click(sender As Object, e As EventArgs) Handles btnToestellen.Click
        Dim f As New Toestellen()

        ' Positioneer ONDER btnToestellen (zoals AddCoord!)
        Dim btnBounds = btnToestellen.Bounds
        f.StartPosition = FormStartPosition.Manual
        f.Location = New Point(
        Me.PointToScreen(btnToestellen.Location).X,
        Me.PointToScreen(btnToestellen.Location).Y + btnBounds.Height + 10
    )

        f.ShowDialog(Me)    ' ← Modal (blokkeert tot sluiten)
        LoadDevices()       ' ← Herlaad devices na connect/disconnect
    End Sub



    Private Sub CopyVanClipboard_Click(sender As Object, e As EventArgs) Handles btnCopyVanClipboard.Click
        Try
            tbCoordinaten.Text = Clipboard.GetText().Trim()
            tbCoordinaten.SelectAll()
            UpdateDistance()  ' ← NIEUW
        Catch ex As Exception
            MessageBox.Show("Fout: " & ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDevices()
        chbAllDevices.Checked = False
        UpdateDistance()  ' ← Update lblKm na refresh
    End Sub
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If MessageBox.Show("App afsluiten?", "Bevestig", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveCoord_Click(sender As Object, e As EventArgs) Handles btnSaveCoord.Click
        Dim huidigeCoord As String = tbCoordinaten.Text.Trim()
        If String.IsNullOrWhiteSpace(huidigeCoord) Then
            MessageBox.Show("Geen coördinaat om op te slaan.")
            Return
        End If

        Using f As New AddCoord()
            ' Directe tekst-toewijzing ipv properties
            f.tbCoord.Text = huidigeCoord

            ' Positioneer 50px onder btnSaveCoord
            Dim btnBounds = btnSaveCoord.Bounds
            f.StartPosition = FormStartPosition.Manual
            f.Location = New Point(Me.PointToScreen(btnSaveCoord.Location).X,
                               Me.PointToScreen(btnSaveCoord.Location).Y + btnBounds.Height + 50)

            If f.ShowDialog(Me) = DialogResult.OK Then
                LoadKnownCoords()
            End If
        End Using
    End Sub
    Private Function CalculateDistance(lat1 As Double, lon1 As Double, lat2 As Double, lon2 As Double) As Double
        Dim R = 6371.0
        Dim dLat = DegToRad(lat2 - lat1)
        Dim dLon = DegToRad(lon2 - lon1)
        Dim a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
        Dim c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))
        Return Math.Round(R * c, 2)
    End Function

    Private Function DegToRad(degrees As Double) As Double
        Return degrees * Math.PI / 180.0
    End Function

    ' Bovenin de class:
    Private Function ParseCoord(coordText As String) As (lat As Double, lon As Double, isValid As Boolean)
        Try
            coordText = coordText.Trim()
            Dim commaIndex = coordText.IndexOf(","c)
            If commaIndex > 0 AndAlso commaIndex < coordText.Length - 1 Then
                Dim latStr = coordText.Substring(0, commaIndex).Trim()
                Dim lonStr = coordText.Substring(commaIndex + 1).Trim()
                ' Replace comma met punt voor Double (NL fix)
                latStr = latStr.Replace(",", ".")
                lonStr = lonStr.Replace(",", ".")
                Dim lat = Double.Parse(latStr, System.Globalization.CultureInfo.InvariantCulture)
                Dim lon = Double.Parse(lonStr, System.Globalization.CultureInfo.InvariantCulture)
                Return (lat, lon, True)
            End If
        Catch
            ' Foutieve coördinaat
        End Try
        Return (0, 0, False)
    End Function

    Private Sub tbCoordinaten_TextChanged(sender As Object, e As EventArgs) Handles tbCoordinaten.TextChanged
        ' Update oldCoordText NA berekening (voor eerste keer wordt het 0km)
        UpdateDistance()
        oldCoordText = tbCoordinaten.Text  ' ← NU bijwerken
    End Sub

    Private Sub UpdateDistance()
        Dim current = ParseCoord(tbCoordinaten.Text.Trim())
        Dim previous = ParseCoord(oldCoordText.Trim())  ' ← NU direct uit geheugen

        If current.isValid AndAlso previous.isValid Then
            Dim distance = CalculateDistance(current.lat, current.lon, previous.lat, previous.lon)
            lblKm.Text = distance.ToString("F2") + " km"
            lblKm.ForeColor = Color.Blue

            Dim cooldown = GetCooldownTime(distance)
            lblCdTijd.Text = cooldown
            lblCdTijd.ForeColor = Color.Green
        Else
            lblKm.Text = "0.00 km"
            lblCdTijd.Text = "0 min"
            lblKm.ForeColor = Color.Gray
            lblCdTijd.ForeColor = Color.Gray
        End If
    End Sub

    Private Function GetCooldownTime(distanceKm As Double) As String
        ' Jouw EXACTE tabel Afstand;Cooldown
        Dim cooldownTable = {
            (1, 1), (2, 1), (3, 2), (4, 2), (5, 3), (8, 4), (10, 6),
            (15, 8), (20, 11), (25, 14), (30, 16), (35, 17), (40, 18),
            (45, 19), (50, 20), (60, 21), (70, 22), (80, 23), (90, 24),
            (100, 26), (125, 28), (150, 31), (175, 33), (201, 36),
            (250, 41), (300, 46), (328, 48), (350, 49), (400, 54),
            (450, 58), (500, 61), (550, 65), (600, 69), (650, 73),
            (700, 76), (751, 81), (802, 83), (839, 88), (897, 90),
            (948, 94), (1007, 97), (1100, 101), (1180, 109), (1221, 112),
            (1300, 117), (1355, 119), (1403, 120)
        }

        ' Zoek EERSTVOLGENDE >= afstand (upper bound)
        For i = 0 To cooldownTable.Length - 1
            If distanceKm <= cooldownTable(i).Item1 Then
                Return cooldownTable(i).Item2.ToString() + " min"
            End If
        Next

        ' Groter dan laatste: gebruik laatste
        Return cooldownTable(cooldownTable.Length - 1).Item2.ToString() + " min"
    End Function
    Private Sub LoadKnownCoords()
        dgvKnownCoord.Rows.Clear()

        Dim appDir = Path.GetDirectoryName(Application.ExecutablePath)
        Dim filePath = Path.Combine(appDir, "CoordInfo.txt")

        If Not File.Exists(filePath) Then
            Return
        End If

        ' Zorg dat DGV kolommen heeft: Coord, Omschrijving
        If dgvKnownCoord.Columns.Count = 0 Then
            dgvKnownCoord.Columns.Add("Coord", "Coördinaat")
            dgvKnownCoord.Columns.Add("Omschrijving", "Omschrijving")
        Else
            If dgvKnownCoord.Columns("Coord") Is Nothing Then
                dgvKnownCoord.Columns.Add("Coord", "Coördinaat")
            End If
            If dgvKnownCoord.Columns("Omschrijving") Is Nothing Then
                dgvKnownCoord.Columns.Add("Omschrijving", "Omschrijving")
            End If
        End If

        Dim lines = File.ReadAllLines(filePath)
        For Each line In lines
            If String.IsNullOrWhiteSpace(line) Then Continue For
            Dim parts = line.Split("|"c)
            Dim coord As String = parts(0)
            Dim oms As String = If(parts.Length > 1, parts(1), "")
            dgvKnownCoord.Rows.Add(coord, oms)
        Next

        dgvKnownCoord.RowHeadersVisible = False
        dgvKnownCoord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Private Sub dgvKnownCoord_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvKnownCoord.CellDoubleClick
        If e.RowIndex < 0 Then Return
        Dim coord As String = dgvKnownCoord.Rows(e.RowIndex).Cells("Coord").Value.ToString()
        tbCoordinaten.Text = coord
    End Sub


End Class
