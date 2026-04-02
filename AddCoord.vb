Imports System.IO
Imports System.Linq

Public Class AddCoord
    Private ini As IniFile
    Private Sub AddCoord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Open het scherm op de coordinaten uit de setting.in

        Dim iniPath = Path.Combine(Application.StartupPath, "settings.ini")
        ini = New IniFile(iniPath)

        LoadFormPosition(Me, ini)

        tbCoord.ReadOnly = True
        tbCoord.BackColor = SystemColors.ControlLight

        tbCodeOms.Visible = False
        tbCodeOms.ReadOnly = False
        tbCodeOms.BackColor = SystemColors.Window

        ' CRUCIALE FIX: DropDown voor vrije tekst (niet DropDownList!)
        cbCode.DropDownStyle = ComboBoxStyle.DropDown
        cbCode.MaxLength = 50  ' ← Optioneel, voorkom te lange invoer

        LaadCodes()
    End Sub
    Private Sub VangTabel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If ini Is Nothing Then
            Dim iniPath = Path.Combine(Application.StartupPath, "settings.ini")
            ini = New IniFile(iniPath)
        End If

        SaveFormPosition(Me, ini)
    End Sub
    Private Sub cbCode_Leave(sender As Object, e As EventArgs) Handles cbCode.Leave
        Dim invoer = cbCode.Text.Trim()
        If String.IsNullOrEmpty(invoer) Then
            tbCodeOms.Visible = False
            tbCodeOms.Clear()
            Return
        End If

        Dim idx = invoer.IndexOf(" - ")
        Dim code = If(idx > 0, invoer.Substring(0, idx).Trim(), invoer)

        If IsCodeBestaand(code) Then
            tbCodeOms.Visible = False
            tbCodeOms.Clear()
        Else
            tbCodeOms.Visible = True
            tbCodeOms.Focus()
            tbCodeOms.SelectAll()
        End If
    End Sub
    Private Sub LaadCodes()
        cbCode.Items.Clear()
        Dim appDir = Path.GetDirectoryName(Application.ExecutablePath)
        Dim codePath = Path.Combine(appDir, "Code.txt")

        If Not File.Exists(codePath) Then
            Return  ' Stilletjes, geen melding meer nodig
        End If

        For Each line In File.ReadAllLines(codePath)
            If String.IsNullOrWhiteSpace(line) Then Continue For

            Dim parts = line.Split("|"c)
            If parts.Length >= 1 Then
                Dim code = parts(0).Trim()
                Dim oms = If(parts.Length > 1, parts(1).Trim(), "")

                Dim display = If(String.IsNullOrEmpty(oms), code, code & " - " & oms)
                cbCode.Items.Add(display)
            End If
        Next
    End Sub

    Private Function IsCodeBestaand(code As String) As Boolean
        Dim appDir = Path.GetDirectoryName(Application.ExecutablePath)
        Dim codePath = Path.Combine(appDir, "Code.txt")

        If Not File.Exists(codePath) Then Return False

        Return File.ReadAllLines(codePath).Any(Function(line) line.Split("|"c)(0).Trim() = code)
    End Function

    Private Sub btnAnnuleer_Click(sender As Object, e As EventArgs) Handles btnAnnuleer.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim invoer = cbCode.Text.Trim()
        If String.IsNullOrWhiteSpace(invoer) Then
            MessageBox.Show("Code mag niet leeg zijn!")
            cbCode.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(tbCoord.Text) OrElse String.IsNullOrWhiteSpace(tbOmsCoord.Text) Then
            MessageBox.Show("Coordinaat én omschrijving invullen!")
            Return
        End If

        ' Code-deel extraheren voor opslag
        Dim idx = invoer.IndexOf(" - ")
        Dim code = If(idx > 0, invoer.Substring(0, idx).Trim(), invoer)

        ' NIEUWE CODE: Append aan Code.txt als nieuw
        If Not IsCodeBestaand(code) Then
            Dim nieuweCodeLine = code & "|" & tbCodeOms.Text.Trim()
            Dim appDir = Path.GetDirectoryName(Application.ExecutablePath)
            Dim codePath = Path.Combine(appDir, "Code.txt")
            File.AppendAllLines(codePath, {nieuweCodeLine})
        End If

        ' CoordInfo.txt opslaan (code|coord|omschrijving)
        Dim appDir2 = Path.GetDirectoryName(Application.ExecutablePath)
        Dim filePath = Path.Combine(appDir2, "CoordInfo.txt")
        Dim line = code & "|" & tbCoord.Text.Trim() & "|" & tbOmsCoord.Text.Trim()
        File.AppendAllLines(filePath, {line})

        ' Reset tbCodeOms onzichtbaar
        tbCodeOms.Visible = False
        tbCodeOms.Clear()

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class
