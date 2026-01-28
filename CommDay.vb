Imports System.Security.Principal
Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports System.Windows
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles
Imports System.Timers


Public Class CommDay
    Private WithEvents myTimer As New System.Timers.Timer()
    Private strIngevoerdeCoordinaten As String
    Private intAantalRondes As Integer  'Variabele voor het opslaan van het maximaal aantal rondes dat op een commday gedaan  moet worden
    Public strJournaal As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler Me.Paint, AddressOf Form1_Paint
        dgvAccBeschikbaar.BorderStyle = BorderStyle.None
        dgvBezettingTf.BorderStyle = BorderStyle.None

        strJournaal = HaalDbInstelling("JournaalPath") & Format(Now, "yyyy-MM-dd") & " CommDay.txt"

        ' Stel het interval in op 5 minuten (300000 milliseconden)
        ' Roept de sub OnTimedEvent aan

        myTimer.Interval = CInt(HaalDbInstelling("Systemtimer")) ' 5 minuten in milliseconden
        myTimer.AutoReset = True  ' Herhaal het interval
        myTimer.Enabled = True    ' Timer starten

        'Buttons activeren in ontwikkelfase
        Knoppen()

        'Lees  ingestelde waarde uit de instellingentabel
        intAantalRondes = LeesInstelling("Rondes")

        VerversDGV()
        VulvangstPerPhone()

    End Sub
    Private Sub dgvAccBeschikbaar_DoubleClick(sender As Object, e As EventArgs) Handles dgvAccBeschikbaar.DoubleClick
        Dim intAccRIj As Integer = dgvAccBeschikbaar.CurrentCell.RowIndex
        Dim accRow As DataGridViewRow = dgvAccBeschikbaar.Rows(intAccRIj)
        Dim strStatus As String = accRow.Cells("Status").Value.ToString()


        Select Case strStatus
            Case 0
                'Keuze lijst  accounts is niet te gebruiken
                'Keuzelijst telefoons is te gebruiken.
                DgvAccBeschikbaarAan(False)

            Case 1

                If Clipboard.ContainsText() Then
                    ToonTxtBox(True)
                    tbCoordinaten.Text = Clipboard.GetText()
                Else
                    MessageBox.Show("Er is geen tekst beschikbaar op het klembord.")
                End If
            Case 2
                'Account zit nog in de cooldown periode, het account is echter al weer aan de beurt om alvast mee te draaien
                'dit wordt dan de volgende ronde
                'Status verhogen naar 2, Ronde blijft 1, Cooldowntijdstip invullen telefoon vrijgeven in Telefoon en tfBezetting. Coordinaten vragen
                DgvAccBeschikbaarAan(False)

            Case 3
                Dim strAccount As String = accRow.Cells("AccCode").Value.ToString()
                Dim strCoord As String
                Dim strTijd As String
                Dim intMinuten As Integer

                strCoord = ""
                strTijd = ""

                If Clipboard.ContainsText() Then
                    ToonTxtBox(True)
                    ToonExtra(True)
                    tbCoordinaten.Text = Clipboard.GetText()
                    VorigeCoord(strAccount, strCoord, strTijd)
                    tbCoordVV.Text = strCoord
                    tbAfstand.Text = Afstand(tbCoordinaten.Text, tbCoordVV.Text)
                    intMinuten = CoolDown(tbAfstand.Text)
                    tbCoolDown.Text = Str(intMinuten)
                    tbNewCdEinde.Text = TelTijd(strTijd, intMinuten + 1)
                Else
                    MessageBox.Show("Er is geen tekst beschikbaar op het klembord.")
                End If

            Case 4
                'Hier nog controleren of de cooldown voorbij is
                'Coordinaten controleren
                StatusVierVerwerken(accRow)
                VerversDGV()
        End Select

    End Sub
    Private Sub dgvBezettingTf_DoubleClick(sender As Object, e As EventArgs) Handles dgvBezettingTf.DoubleClick
        Dim intTfRIj As Integer = dgvBezettingTf.CurrentCell.RowIndex  'Bepaal het volgnummer van de aangeklikte rij
        Dim TfRow As DataGridViewRow = dgvBezettingTf.Rows(intTfRIj)  ' Haal de hele rij met data op
        Dim strAccount = TfRow.Cells("AccCode").Value.ToString()      'Bepaal welk account in de rij is verwerkt

        If IsRowDisabled(TfRow) Then                                                     'De telefoon is al in gebruik
            MessageBox.Show("Deze telefoon is bezet door " & strAccount)
        Else
            ' Controleer of er een geselecteerde cel is
            If dgvBezettingTf.CurrentCell IsNot Nothing Then
                Dim strTijdStip As String = Format(Now, "HH:mm")                        'Bepaal het tijdstip
                Dim strTelefoon As String = TfRow.Cells("tfCode").Value.ToString()      'Bepaal  welek telefoom is gekozen
                Dim intAccRIj As Integer = dgvAccBeschikbaar.CurrentCell.RowIndex       'Bepaal de regel van het account in de DGV
                Dim accRow As DataGridViewRow = dgvAccBeschikbaar.Rows(intAccRIj)       'Bepaal de data uit de specifieke regel
                Dim intRonde As Integer = accRow.Cells("ronde").Value                   'Bepaal de laatst verwerkte ronde
                Dim intStatus As Integer = accRow.Cells("status").Value                 'Bepaal de status van de laatst verwerkte ronde

                strAccount = accRow.Cells("AccCode").Value.ToString()                   'Bepaal welk account het betreft
                'VulTfBezetting(strAccount, strTelefoon, strTijdStip)

                intRonde = intRonde + 1                                                 'Verhoog het rondenummer met 1  
                If intRonde > intAantalRondes Then                                      'Controleer of alle rondes al verwerkt zijn
                    intStatus = 6                                                       'Zet de staus op 6, regel krijg verderop een zwarte kleur
                Else
                    intStatus = intStatus + 1                                           'Verhoog de staus 
                End If

                Select Case intStatus                                                   'Verwerk de status
                    Case 0, 1, 3, 4, 5
                        VulTfBezetting(strAccount, strTelefoon, strTijdStip)
                    Case 2
                        VulTfBezetting(strAccount, strTelefoon, strTijdStip)
                        strTelefoon = ""
                    Case 6                                                              'Dit account is klaar als het status 6 heeft
                        strTelefoon = ""
                        intRonde = intAantalRondes
                End Select

                VulAccBeschikbaar(strAccount, strTelefoon, Str(intStatus), strTijdStip, Str(intRonde))

                VerversDGV()
            Else
                MessageBox.Show("Er is geen cel geselecteerd.")
            End If

        End If
        DgvAccBeschikbaarAan(True)

    End Sub
    Private Sub ToonTxtBox(lToon)
        tbCoordinaten.Visible = lToon
        BtnAnnuleren.Visible = lToon
        btnSlaOp.Visible = lToon
        lblCoordinaten.Visible = lToon
        lblLevel.Visible = lToon
        tbLevel.Visible = lToon
    End Sub
    Public Sub VerversDGV()
        BeschikbareAcc()
        BezettingTf()
        ToonTxtBox(False)

    End Sub
    Private Sub BezettingTf()
        Dim strQuery As String = "Select tfCode, AccCode,StartTijd from TfBezetting order by tfCode"
        Dim dt As DataTable = ExecuteQuery(strQuery)

        dgvBezettingTf.DataSource = dt
        dgvBezettingTf.RowHeadersVisible = False
        dgvBezettingTf.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dgvBezettingTf.Columns("TfCode").Width = 80
        dgvBezettingTf.Columns("AccCode").Width = 30
        dgvBezettingTf.Columns("StartTijd").Width = 60
        dgvBezettingTf.Visible = True
        dgvBezettingTf.Enabled = False

        DisableRowsBasedOnCellValue()
    End Sub

    Private Sub DisableRowsBasedOnCellValue()
        For Each row As DataGridViewRow In dgvBezettingTf.Rows
            ' Controleer de waarde van een specifieke cel, bijvoorbeeld in kolom 0
            If row.Cells(1).Value.ToString() IsNot "" Then
                ' Maak de rij grijs
                row.DefaultCellStyle.BackColor = Color.LightGray
                row.DefaultCellStyle.ForeColor = Color.Red

                ' Zet de rij in een "disabled" staat door hem niet wijzigbaar te maken
                row.ReadOnly = True
            End If
        Next
    End Sub
    Private Sub GeefStatusEenKleur()
        Dim strStatus As String = ""

        For Each row As DataGridViewRow In dgvAccBeschikbaar.Rows
            ' Controleer de waarde van een specifieke cel, bijvoorbeeld in kolom 0
            strStatus = row.Cells(1).Value.ToString()

            Select Case strStatus
                Case 0
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                    row.DefaultCellStyle.ForeColor = Color.Black
                Case 1
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                    row.DefaultCellStyle.ForeColor = Color.Black

                Case 2
                    row.DefaultCellStyle.BackColor = Color.Red
                    row.DefaultCellStyle.ForeColor = Color.White

                Case 3
                    row.DefaultCellStyle.BackColor = Color.Purple
                    row.DefaultCellStyle.ForeColor = Color.White
                Case 4
                    row.DefaultCellStyle.BackColor = Color.DarkOrange
                    row.DefaultCellStyle.ForeColor = Color.White
                Case 5
                    row.DefaultCellStyle.BackColor = Color.DarkOrange
                    row.DefaultCellStyle.ForeColor = Color.White
                Case 6
                    row.DefaultCellStyle.BackColor = Color.Black
                    row.DefaultCellStyle.ForeColor = Color.White


            End Select

        Next
    End Sub
    Private Sub BeschikbareAcc()
        ' Databasequery kan op de achtergrondthread draaien
        Dim strQuery As String = "Select ab.AccCode, ab.status, ab.ronde, ab.EndCoolDown, ab.vangtijd, ab.Telefoon " &
                             "from AccBeschikbaar ab ORDER BY ab.Status, ab.Telefoon, ab.Ronde, ab.EndCoolDown, ab.AccCode"
        Dim dt As DataTable = ExecuteQuery(strQuery)

        ' UI-updates moeten op de UI-thread draaien
        If dgvAccBeschikbaar.InvokeRequired Then
            dgvAccBeschikbaar.Invoke(Sub()
                                         dgvAccBeschikbaar.DataSource = dt
                                         dgvAccBeschikbaar.RowHeadersVisible = False
                                         dgvAccBeschikbaar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                                         dgvAccBeschikbaar.Columns("AccCode").Width = 70
                                         dgvAccBeschikbaar.Columns("status").Width = 30
                                         dgvAccBeschikbaar.Columns("ronde").Width = 30
                                         dgvAccBeschikbaar.Columns("EndCoolDown").Width = 80
                                         dgvAccBeschikbaar.Columns("Telefoon").Width = 100
                                         dgvAccBeschikbaar.Columns("vangtijd").Width = 80

                                         dgvAccBeschikbaar.Columns("AccCode").Visible = True
                                         dgvAccBeschikbaar.Columns("status").Visible = True
                                         dgvAccBeschikbaar.Columns("ronde").Visible = True
                                         dgvAccBeschikbaar.Columns("Telefoon").Visible = True
                                         dgvAccBeschikbaar.Enabled = True
                                         GeefStatusEenKleur()
                                     End Sub)
        Else
            ' Direct uitvoeren als we al op de UI-thread zijn
            dgvAccBeschikbaar.DataSource = dt
            dgvAccBeschikbaar.RowHeadersVisible = False
            dgvAccBeschikbaar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            dgvAccBeschikbaar.Columns("AccCode").Width = 70
            dgvAccBeschikbaar.Columns("status").Width = 30
            dgvAccBeschikbaar.Columns("ronde").Width = 30
            dgvAccBeschikbaar.Columns("EndCoolDown").Width = 80
            dgvAccBeschikbaar.Columns("Telefoon").Width = 100
            dgvAccBeschikbaar.Columns("vangtijd").Width = 80

            dgvAccBeschikbaar.Columns("AccCode").Visible = True
            dgvAccBeschikbaar.Columns("status").Visible = True
            dgvAccBeschikbaar.Columns("ronde").Visible = True
            dgvAccBeschikbaar.Columns("Telefoon").Visible = True
            dgvAccBeschikbaar.Enabled = True
            GeefStatusEenKleur()
        End If
    End Sub

    Private Sub StatusVierVerwerken(GekozenRij)
        Dim strAccCode As String = GekozenRij.cells("AccCode").value.ToString()
        Dim strTelefoon As String = GekozenRij.cells("Telefoon").value.ToString()
        Dim strTime As String = "00:00"
        Dim strStatus As String = "2"
        Dim strEndCd = CoolDownTijd()
        Dim strVangtijd As String = Format(Now, "HH:mm")
        Dim strCoordinaten As String = ""
        Dim strLevel As String = ""
        Dim intRonde = GekozenRij.cells("Ronde").value
        Dim strTelOud As String = strTelefoon
        VulTfBezetting("", strTelefoon, strTime)
        strTelefoon = ""
        VulAccStatus_2(strAccCode, strTelefoon, strStatus, strVangtijd, strCoordinaten, strEndCd, strLevel, Str(intRonde), strTelOud)

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSlaOp.Click
        VerwerkStatus_1()
        ToonExtra(False)
        tbLevel.Text = ""
        VulvangstPerPhone()

    End Sub
    Private Sub VerwerkStatus_1()
        Dim intAccRIj As Integer = dgvAccBeschikbaar.CurrentCell.RowIndex
        Dim accRow As DataGridViewRow = dgvAccBeschikbaar.Rows(intAccRIj)
        Dim intStatus As Integer = accRow.Cells("Status").Value + 1

        'Pokemon is gevangen en de cooldown periode gaat in
        Dim strTelefoon As String = accRow.Cells("Telefoon").Value.ToString()
        Dim strAccCode As String = accRow.Cells("AccCode").Value.ToString()
        Dim strTijdstip As String = Format(Now, "HH:mm")
        Dim intRonde As Integer = accRow.Cells("Ronde").Value
        Dim strEndCd As String = CoolDownTijd()
        Dim strCoordinaten As String = tbCoordinaten.Text
        Dim strTelOud As String = strTelefoon

        If Len(strCoordinaten) > 10 Then

            If intStatus = 4 Then
                If CoolDownOver(tbNewCdEinde.Text) Then
                    intStatus = 2
                    'intRonde = intRonde + 1
                    VulTfBezetting("", strTelefoon, "00:00")
                    strTelefoon = ""
                Else
                    MessageBox.Show(strTelefoon & " stoppen.")
                    strTijdstip = accRow.Cells("vangtijd").Value.ToString()
                    strEndCd = tbNewCdEinde.Text

                End If
            Else
                VulTfBezetting("", strTelefoon, "00:00")
                strTelefoon = ""
            End If


            VulAccStatus_2(strAccCode, strTelefoon, Str(intStatus), strTijdstip, strCoordinaten, strEndCd, tbLevel.Text, Str(intRonde), strTelOud)
            VerversDGV()
            strIngevoerdeCoordinaten = ""
        Else

            MessageBox.Show("Geen of geen corecte coordianten ingevoerd")
        End If

    End Sub
    Private Function IsRowDisabled(row As DataGridViewRow) As Boolean
        ' Controleer of de rij een specifieke achtergrondkleur heeft die we gebruiken voor "disabled"
        Return row.DefaultCellStyle.BackColor = Color.LightGray AndAlso row.ReadOnly
    End Function
    Private Sub BtnAnnuleren_Click(sender As Object, e As EventArgs) Handles BtnAnnuleren.Click
        ToonTxtBox(False)
        ToonExtra(False)
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        CooldownOpheffen()
        BeschikbareAcc()
        ToonTxtBox(False)
    End Sub
    Private Sub ControleerStatus_4()
        Dim strQuery As String = "Select * From AccBeschikbaar ab Where ab.STATUS = '4' AND ab.EndCoolDown <current_time"
        Dim dt As DataTable = ExecuteQuery(strQuery)
        Dim nAantalRecords As Integer = dt.Rows.Count
        Dim nI As IntegerProperty
        Dim strTelefoon As String = ""

        If nAantalRecords > 0 Then

            For nI = 0 To nAantalRecords - 1
                strTelefoon = strTelefoon & dt.Rows(nI)("Telefoon").ToString() & vbCrLf
            Next
            MessageBox.Show(" Gegeijzelde Pm op de volgende toestellen kunnen gevangen worden" & vbCrLf & strTelefoon)
        End If

    End Sub

    Private Sub btnStatusCtrl_Click(sender As Object, e As EventArgs)
        ControleerStatus_4()
    End Sub
    Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs) Handles myTimer.Elapsed
        ' Voer hier de actie uit die je elke 5 minuten wilt uitvoeren
        'MessageBox.Show("5 minuten zijn verstreken!")
        ControleerStatus_4()
        CooldownOpheffen()
        BeschikbareAcc()
        ToonTxtBox(False)
    End Sub
    Private Function CoolDownTijd()
        Dim currentTime As DateTime = DateTime.Now
        Dim newTime As DateTime = currentTime.AddMinutes(121)

        CoolDownTijd = newTime.ToString("HH:mm")

    End Function

    Private Function HaalDbInstelling(StrNaam)
        Dim strQuery As String = "Select Waarde1 from Instellingen where naam = '" & StrNaam & "'"
        Dim dtResult As DataTable = ExecuteQuery(strQuery)
        Dim nAantalRecords As Integer = dtResult.Rows.Count
        Dim strReturn As String = ""

        If nAantalRecords = 1 Then
            strReturn = dtResult.Rows(0)("Waarde1").ToString()
        End If
        HaalDbInstelling = strReturn

    End Function

    'Private Sub BtnBereken_Click(sender As Object, e As EventArgs)
    '    Bereken()
    'End Sub
    Private Sub DgvAccBeschikbaarAan(lAanUit)
        dgvAccBeschikbaar.Enabled = lAanUit
        dgvBezettingTf.Enabled = Not lAanUit

        ' Forceer het formulier om opnieuw te tekenen zodat de kleuren worden bijgewerkt
        Me.Invalidate() 'Kleur veranderen van de dgv

    End Sub
    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint

        DrawBorder(e.Graphics, dgvAccBeschikbaar, If(dgvAccBeschikbaar.Enabled, Color.Green, Color.Red))
        DrawBorder(e.Graphics, dgvBezettingTf, If(dgvBezettingTf.Enabled, Color.Green, Color.Red))

    End Sub
    Private Sub DrawBorder(graphics As Graphics, dgv As DataGridView, color As Color)
        Dim borderWidth As Integer = 4 ' Stel de gewenste dikte van de rand in
        Using pen As New Pen(color, borderWidth)
            Dim rect As Rectangle = dgv.Bounds
            rect.Inflate(borderWidth, borderWidth) ' Vergroot de rechthoek om de rand te tekenen
            graphics.DrawRectangle(pen, rect)
        End Using
    End Sub
    Private Sub BtnSchoonDb_Click(sender As Object, e As EventArgs) Handles BtnSchoonDb.Click

        If MessageBox.Show("Zeker weten, dat je tabellen wilt schonen?", "Schonen Tabellen", MessageBoxButtons.YesNo) = vbYes Then
            SchrijfNaarBestand("Tabellen worden geschoond")
            TabellenKlaarZetten()
            VerversDGV()
        End If
    End Sub
    Private Sub Knoppen()

        'Knoppen
        'Textboxen
        tbCoordVV.Visible = False
        tbCoolDown.Visible = False
        lblVangenOm.Visible = False
        tbNewCdEinde.Visible = False
        tbAfstand.Visible = False
        'Labels
        lblAfstand.Visible = False
        lblCoordinaten.Visible = False
        tbCoordVV.Visible = False
        lblLevel.Visible = False
        lblCdTijd.Visible = False
        lblCoordVV.Visible = False
        lblVangenOm.Visible = False

    End Sub

    Private Sub ToonExtra(lToon)

        lblAfstand.Visible = lToon
        tbCoordVV.Visible = lToon
        lblCdTijd.Visible = lToon
        lblCoordVV.Visible = lToon
        tbAfstand.Visible = lToon
        tbCoordVV.Visible = lToon
        tbCoolDown.Visible = lToon
        lblVangenOm.Visible = lToon
        tbNewCdEinde.Visible = lToon

    End Sub
    Sub VulvangstPerPhone()
        'Dim dtVangstPerPhone As New DataTable
        Dim strQuery As String = "SELECT Telefoon, COUNT(*) Aantal FROM CommDayJournal WHERE Date(Tijdstip) = DATE(NOW()) GROUP BY Telefoon"
        'dtVangstPerPhone = ExecuteQuery(strQuery)
        dgvVangstPerPhone.DataSource = ExecuteQuery(strQuery)
        dgvVangstPerPhone.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvVangstPerPhone.RowHeadersVisible = False

    End Sub
    Private Sub BtnVervers_Click(sender As Object, e As EventArgs) Handles BtnVervers.Click
        VerversDGV()
    End Sub

    Private Sub btnCorrAccount_Click(sender As Object, e As EventArgs) Handles btnCorrScherm.Click
        'Me.Hide()
        Correctiescherm.Show()
        VerversDGV()
    End Sub

    Private Sub btnStoppen_Click(sender As Object, e As EventArgs) Handles btnStoppen.Click
        Me.Hide()
        'Startscherm.Show()

    End Sub
    Private Sub CommDay_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
    End Sub
    Private Sub dgvBezettingTf_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvBezettingTf.KeyDown
        ' Controleer of de Delete-toets is ingedrukt
        If e.KeyCode = Keys.Delete Then
            ' Controleer of er een rij is geselecteerd
            If dgvBezettingTf.SelectedRows.Count > 0 Then
                ' Optioneel: Vraag bevestiging aan de gebruiker
                If MessageBox.Show("Wil je de geselecteerde rij verwijderen?", "Bevestiging", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    ' Verwijder de geselecteerde rij
                    For Each row As DataGridViewRow In dgvBezettingTf.SelectedRows
                        If Not row.IsNewRow Then ' Zorg ervoor dat de nieuwe rij niet wordt verwijderd
                            dgvBezettingTf.Rows.Remove(row)
                        End If
                    Next
                End If
            End If
        End If
    End Sub

End Class
