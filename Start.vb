Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Threading
Public Class HenkAdb
    Private oldCoordText As String = ""
    Private ini As IniFile
    Private Sub CommDay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim iniPath = Path.Combine(Application.StartupPath, "settings.ini")
        ini = New IniFile(iniPath)
        LoadFormPosition(Me, ini)

        Logger.Log("=== APP STARTED ===")

        Logger.Log("App gestart - Cleanup logs uitgevoerd")

        ' HISTORIE OPSCHONEN: records ouder dan 2 dagen zonder omschrijving verwijderen
        CleanupHistorie()

        ' EERST ADB INITIALISEREN
        AdbHelper.InitializeAdbPath()
        LoadDevices()

        ' DEBUG/RELEASE titel + versie
        Dim baseTitle As String
#If DEBUG Then
        baseTitle = "HenkAdb acceptatie"
#Else
        baseTitle = "HenkAdb"
#End If
        Me.Text = baseTitle & " v" & System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3)

        ' GROEN MAKEN BIJ START
        btnStart.BackColor = Color.Green
        btnStart.ForeColor = Color.White
        btnCopyVanClipboard.BackColor = Color.Blue
        btnCopyVanClipboard.ForeColor = Color.White
        btnToestellen.BackColor = Color.Orange
        btnToestellen.ForeColor = Color.Black
        ' NIEUW: bekende coördinaten laden
        LoadKnownCoords()

    End Sub
    Private Sub VangTabel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If ini Is Nothing Then
            Dim iniPath = Path.Combine(Application.StartupPath, "settings.ini")
            ini = New IniFile(iniPath)
        End If

        SaveFormPosition(Me, ini)
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

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Logger.Log("=== btnStart CLICKED ===")
        Dim inputText As String = tbCoordinaten.Text.Trim()
        If String.IsNullOrWhiteSpace(inputText) Then
            Logger.Log("Empty input - abort")
            MessageBox.Show("Vul coordinaat of routenaam in.")
            tbCoordinaten.Focus()
            Return
        End If

        Dim devices = AdbHelper.GetDevices() ' ← Logt automatisch
        If devices.Count = 0 Then
            Logger.Log("NO DEVICES CONNECTED - ABORT", "ERROR")
            MessageBox.Show("Geen devices gekoppeld! Sluit USB debug aan.")
            Return
        End If

        Dim parts = inputText.Split(","c)
        Dim succesvol = 0
        Dim totalChecked = 0

        ' Eerst tellen welke devices geselecteerd zijn
        Dim selectedDevices As New List(Of String)
        For Each row As DataGridViewRow In dgvDevices.Rows
            If row.Cells("Select").Value = True Then
                Dim deviceId As String = row.Cells("DeviceID").Value.ToString()
                selectedDevices.Add(deviceId)
            End If
        Next
        totalChecked = selectedDevices.Count

        If totalChecked = 0 Then
            MessageBox.Show("Geen devices geselecteerd.")
            Return
        End If

        ' Als het route is (geen coordinaat) gewoon 1x sturen zoals eerst
        If parts.Length <> 2 Then
            For Each deviceId In selectedDevices
                Logger.Log($"Sending ROUTE '{inputText}' to {deviceId}")
                Try
                    AdbHelper.SendRouteToDevice(deviceId, inputText)
                    succesvol += 1
                    Logger.Log($"✓ SUCCESS {deviceId}")
                Catch ex As Exception
                    Logger.LogError(ex, $"Send to {deviceId}")
                End Try
            Next
        Else
            ' Coördinaat: A -> offset -> A, steeds voor ALLE devices tegelijk
            Dim lat As String = parts(0).Trim()
            Dim lon As Double = Double.Parse(parts(1).Trim(), Globalization.CultureInfo.InvariantCulture)
            Dim lonOrig As String = lon.ToString(Globalization.CultureInfo.InvariantCulture)
            Dim lonOffset As String = (lon + 0.00001).ToString(Globalization.CultureInfo.InvariantCulture)

            ' Stap 1: alle devices naar A
            For Each deviceId In selectedDevices
                Logger.Log($"Step 1 (A) '{lat},{lonOrig}' to {deviceId}")
                Try
                    AdbHelper.SendTeleportToDevice(deviceId, lat, lonOrig)
                Catch ex As Exception
                    Logger.LogError(ex, $"Step1 to {deviceId}")
                End Try
            Next
            Threading.Thread.Sleep(200)

            ' Stap 2: alle devices naar A+offset
            For Each deviceId In selectedDevices
                Logger.Log($"Step 2 (A+offset) '{lat},{lonOffset}' to {deviceId}")
                Try
                    AdbHelper.SendTeleportToDevice(deviceId, lat, lonOffset)
                Catch ex As Exception
                    Logger.LogError(ex, $"Step2 to {deviceId}")
                End Try
            Next
            Threading.Thread.Sleep(200)

            ' Stap 3: alle devices terug naar A
            For Each deviceId In selectedDevices
                Logger.Log($"Step 3 (A) '{lat},{lonOrig}' to {deviceId}")
                Try
                    AdbHelper.SendTeleportToDevice(deviceId, lat, lonOrig)
                    succesvol += 1 ' eindstatus gelukt
                    Logger.Log($"✓ SUCCESS {deviceId}")
                Catch ex As Exception
                    Logger.LogError(ex, $"Step3 to {deviceId}")
                End Try
            Next
        End If

        Logger.Log($"btnStart FINISHED: {succesvol}/{totalChecked} success, {devices.Count} total devices")
        Dim historiePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Historie.txt")
        Dim logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{inputText}|"
        File.AppendAllText(historiePath, logLine & Environment.NewLine)
        Logger.Log($"Historie toegevoegd: {logLine.TrimEnd()}")
    End Sub


    Private Sub btnToestellen_Click(sender As Object, e As EventArgs) Handles btnToestellen.Click
        Dim f As New Toestellen()

        Dim btnBounds = btnToestellen.Bounds
        Dim btnScreenLoc = Me.PointToScreen(btnToestellen.Location)

        f.StartPosition = FormStartPosition.Manual

        ' BOVEN de knop: Y = knopY - formHoogte - marge
        Dim marge As Integer = 10
        Dim x As Integer = btnScreenLoc.X
        Dim y As Integer = btnScreenLoc.Y - f.Height - marge

        ' Optioneel: voorkomen dat hij boven het scherm uit komt
        If y < 0 Then y = 0

        f.Location = New Point(x, y)

        f.ShowDialog(Me)
        LoadDevices()
    End Sub

    Private Sub CopyVanClipboard_Click(sender As Object, e As EventArgs) Handles btnCopyVanClipboard.Click
        Try
            Dim clip As String = Clipboard.GetText().Trim()
            If String.IsNullOrWhiteSpace(clip) Then
                MessageBox.Show("Klembord is leeg.")
                Return
            End If

            ' URL → coördinaten halen
            If BegintMetHttps(clip) Then
                clip = HaalCoördinatenUitURL(clip)
            End If

            ' Trim coördinaten OF laat route-tekst intact
            Dim coord As String
            Dim parts = clip.Split(","c)
            If parts.Length = 2 AndAlso IsNumeric(parts(0).Trim()) AndAlso IsNumeric(parts(1).Trim()) Then
                ' Geldig lat,lon → trimmen
                coord = TrimCoord(clip)
            Else
                ' Route-tekst (geen komma of ongeldig) → ongewijzigd
                coord = clip
            End If

            tbCoordinaten.Text = coord
            tbCoordinaten.SelectAll()
            ' TextChanged triggert UpdateDistance automatisch
        Catch ex As Exception
            MessageBox.Show("Fout bij klembord: " & ex.Message)
        End Try
    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDevices()
        chbAllDevices.Checked = False
        ' GEEN UpdateDistance hier nodig
    End Sub
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If MessageBox.Show("App afsluiten?", "Bevestig", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveCoord_Click(sender As Object, e As EventArgs) Handles btnSaveCoord.Click
        Dim huidigeCoord As String = tbCoordinaten.Text.Trim()
        If String.IsNullOrWhiteSpace(huidigeCoord) Then
            MessageBox.Show("Geen coordinaat om op te slaan.")
            Return
        End If

        Using f As New AddCoord
            f.tbCoord.Text = huidigeCoord

            ' Positioneer BOVEN btnSaveCoord
            Dim btnBounds = btnSaveCoord.Bounds
            Dim btnScreenLoc = Me.PointToScreen(btnSaveCoord.Location)

            f.StartPosition = FormStartPosition.Manual

            Dim marge As Integer = 10
            Dim x As Integer = btnScreenLoc.X
            Dim y As Integer = btnScreenLoc.Y - f.Height - marge

            If y < 0 Then y = 0

            f.Location = New Point(x, y)

            If f.ShowDialog(Me) = DialogResult.OK Then
                LoadKnownCoords()
            End If
        End Using
    End Sub

    Private Sub tbCoordinaten_TextChanged(sender As Object, e As EventArgs) Handles tbCoordinaten.TextChanged
        ' MessageBox.Show("OLD: " & oldCoordText & vbCrLf & "NEW: " & tbCoordinaten.Text)
        UpdateDistance()
        oldCoordText = tbCoordinaten.Text
    End Sub

    Private Sub UpdateDistance()
        Dim current = GPSHelper.ParseCoord(tbCoordinaten.Text.Trim())
        Dim previous = GPSHelper.ParseCoord(oldCoordText.Trim())

        If current.isValid AndAlso previous.isValid Then
            Dim distance = GPSHelper.CalculateDistance(current.lat, current.lon, previous.lat, previous.lon)
            lblKm.Text = distance.ToString("F2") & " km"
            lblKm.ForeColor = Color.Blue

            lblCdTijd.Text = GPSHelper.GetCooldownTime(distance)
            lblCdTijd.ForeColor = Color.Green
        Else
            lblKm.Text = "0.00 km"
            lblCdTijd.Text = "0 min"
            lblKm.ForeColor = Color.Gray
            lblCdTijd.ForeColor = Color.Gray
        End If
    End Sub

    Private Function HaalCoördinatenUitURL(strUrl As String) As String  'Aanpassing 15 okt 2025
        ' Controleer of de URL "place/" bevat
        If strUrl.Contains("place/") Then
            Dim parts() As String = strUrl.Split(New String() {"place/"}, StringSplitOptions.None)
            If parts.Length > 1 Then
                Return parts(1)
            End If
        End If

        ' Controleer of de URL "?q=" bevat
        If strUrl.Contains("?q=") Then
            Dim parts() As String = strUrl.Split(New String() {"?q="}, StringSplitOptions.None)
            If parts.Length > 1 Then
                Return parts(1)
            End If
        End If

        Console.WriteLine("Coördinaten niet gevonden in het juiste formaat.")
        Return strUrl ' Optioneel: retourneer originele string als fallback
    End Function

    Private Function BegintMetHttps(str As String) As Boolean
        Return str.StartsWith("https://")
    End Function

    Private Function TrimCoord(cCoord As String) As String
        If Trim(cCoord) <> "" Then
            ' Splits de string in de afzonderlijke coördinaten met de , als scheidingsteken
            Dim delen() As String = cCoord.Split(","c)

            If delen.Length < 2 Then
                Return cCoord ' onvolledig → niks trimmen
            End If

            Dim strLatitude As String = delen(0).Substring(0, Math.Min(13, delen(0).Length))
            Dim strLongitude As String = delen(1).Substring(0, Math.Min(13, delen(1).Length))

            Dim strDeelLat() As String = strLatitude.Split("."c)
            Dim strDeelLon() As String = strLongitude.Split("."c)

            If strDeelLat.Length < 2 OrElse strDeelLon.Length < 2 Then
                Return cCoord ' geen decimale notatie → niks trimmen
            End If

            Dim strLat As String = strDeelLat(0) & "." & strDeelLat(1).Substring(0, Math.Min(4, strDeelLat(1).Length))
            Dim strLon As String = strDeelLon(0) & "." & strDeelLon(1).Substring(0, Math.Min(4, strDeelLon(1).Length))

            ' Combineer de geformatteerde coördinaten weer in een enkele string
            Return strLat & "," & strLon
        Else
            Return ""
        End If
    End Function
    Private Sub LoadKnownCoords()
        dgvKnownCoord.Rows.Clear()
        dgvKnownCoord.Columns.Clear()

        ' 3 kolommen met VASTE breedtes
        dgvKnownCoord.Columns.Add("Code", "Code")
        dgvKnownCoord.Columns("Code").Width = 80
        dgvKnownCoord.Columns("Code").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

        dgvKnownCoord.Columns.Add("Coord", "Coördinaat")
        dgvKnownCoord.Columns("Coord").Width = 150
        dgvKnownCoord.Columns("Coord").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

        dgvKnownCoord.Columns.Add("Omschrijving", "Omschrijving")
        dgvKnownCoord.Columns("Omschrijving").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgvKnownCoord.RowHeadersVisible = False
        dgvKnownCoord.AllowUserToAddRows = False

        ' Bestand laden
        Dim appDir = Path.GetDirectoryName(Application.ExecutablePath)
        Dim filePath = Path.Combine(appDir, "CoordInfo.txt")

        If Not File.Exists(filePath) Then Return

        Dim lines = File.ReadAllLines(filePath)
        For Each line In lines
            If String.IsNullOrWhiteSpace(line) Then Continue For
            Dim parts = line.Split("|"c)
            If parts.Length >= 3 Then
                dgvKnownCoord.Rows.Add(parts(0).Trim(), parts(1).Trim(), parts(2).Trim())
            End If
        Next
    End Sub

    Private Sub dgvKnownCoord_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvKnownCoord.CellDoubleClick
        If e.RowIndex < 0 Then Return
        tbCoordinaten.Text = dgvKnownCoord.Rows(e.RowIndex).Cells("Coord").Value.ToString()
    End Sub

    Private Sub tbFilter_TextChanged(sender As Object, e As EventArgs) Handles tbFilter.TextChanged
        FilterCoordinaatLijst()
    End Sub

    Private Sub btnClearFilter_Click(sender As Object, e As EventArgs) Handles btnClearFilter.Click
        tbFilter.Clear()    ' ← Leegmaken
        FilterCoordinaatLijst()
    End Sub
    Private Sub FilterCoordinaatLijst()
        Dim filterText = tbFilter.Text.Trim().ToLower()

        For Each row As DataGridViewRow In dgvKnownCoord.Rows
            ' ← NIEUW: Alleen echte rijen filteren (geen "new row")
            If row.IsNewRow Then Continue For

            Dim codeValue = row.Cells("Code").Value
            Dim code = If(codeValue IsNot Nothing, codeValue.ToString().ToLower(), "")

            Dim visible = String.IsNullOrEmpty(filterText) OrElse code.Contains(filterText)
            row.Visible = visible
        Next
    End Sub
    Private Sub CleanupHistorie()
        Dim historiePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Historie.txt")
        If Not File.Exists(historiePath) Then Return

        Dim grens As DateTime = DateTime.Now.AddDays(-2)
        Dim allLines = File.ReadAllLines(historiePath)
        Dim behouden As New List(Of String)
        Dim verwijderd As Integer = 0

        For Each line In allLines
            If String.IsNullOrWhiteSpace(line) Then Continue For

            Dim parts = line.Split("|"c)
            If parts.Length < 3 Then
                ' Ongeldig formaat → bewaren
                behouden.Add(line)
                Continue For
            End If

            Dim datumStr = parts(0).Trim()
            Dim omschrijving = parts(2).Trim()
            Dim datumWaarde As DateTime

            ' Alleen verwijderen als: datum lukt te parsen ÉN ouder dan 2 dagen ÉN omschrijving leeg
            If DateTime.TryParse(datumStr, datumWaarde) Then
                If datumWaarde < grens AndAlso String.IsNullOrEmpty(omschrijving) Then
                    verwijderd += 1
                    Continue For
                End If
            End If

            behouden.Add(line)
        Next

        If verwijderd > 0 Then
            File.WriteAllLines(historiePath, behouden.ToArray())
            Logger.Log($"Historie cleanup: {verwijderd} oude records zonder omschrijving verwijderd")
        Else
            Logger.Log("Historie cleanup: niets te verwijderen")
        End If
    End Sub

    Private Sub btnHistorie_Click(sender As Object, e As EventArgs) Handles btnHistorie.Click
        Dim f As New frmHistorie(Me)

        ' ← RECHTS VAN HENKADB (gelijke hoogte)
        f.StartPosition = FormStartPosition.Manual

        ' Screen coördinaten HenkAdb
        Dim myScreenLoc = Me.PointToScreen(New Point(0, 0))
        Dim myWidth = Me.Width
        Dim myHeight = Me.Height

        ' Rechts + kleine marge
        f.Location = New Point(
            myScreenLoc.X + myWidth + 10,  ' ← RECHTS van HenkAdb
            myScreenLoc.Y                   ' ← ZELFDE HOOGTE
        )

        ' Grootte aanpassen (bijv. 600x400)
        'f.Size = New Size(600, 400)
        'f.WindowState = FormWindowState.Normal

        f.Show()  ' ← Niet ShowDialog (kan naast blijven)
    End Sub

End Class