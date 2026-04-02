Imports System.IO
Imports System.Linq
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmHistorie
    Private historiePath As String
    Private parentForm As HenkAdb
    Private ini As IniFile

    Public Sub New(parent As HenkAdb)
        InitializeComponent()
        Me.parentForm = parent
        historiePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Historie.txt")
    End Sub

    Private Sub frmHistorie_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ← Positie laden EERST (voor SetupDgv)
        Dim iniPath = Path.Combine(Application.StartupPath, "settings.ini")
        ini = New IniFile(iniPath)
        LoadFormPosition(Me, ini)

        SetupDgv()
        LoadHistorie()
    End Sub

    Private Sub SetupDgv()
        dgvHistorie.Columns.Clear()
        dgvHistorie.Columns.Add("DatumTijd", "Datum/Tijd")
        dgvHistorie.Columns.Add("Coordinaat", "Coordinaat")
        dgvHistorie.Columns.Add("Omschrijving", "Omschrijving")

        dgvHistorie.Columns("DatumTijd").ReadOnly = True
        dgvHistorie.Columns("Coordinaat").ReadOnly = True
        dgvHistorie.Columns("Omschrijving").ReadOnly = False

        dgvHistorie.RowHeadersVisible = False
        dgvHistorie.AllowUserToAddRows = False
        dgvHistorie.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistorie.Columns("Omschrijving").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub LoadHistorie()
        dgvHistorie.Rows.Clear()
        If Not File.Exists(historiePath) Then Return

        Dim allLines = File.ReadAllLines(historiePath)
        Dim reversedLines = allLines.Reverse().ToArray()

        For Each line In reversedLines
            If String.IsNullOrWhiteSpace(line) Then Continue For
            Dim parts = line.Split("|"c)
            If parts.Length >= 3 Then
                dgvHistorie.Rows.Add(parts(0), parts(1), parts(2))
            End If
        Next
    End Sub

    Private Sub dgvHistorie_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHistorie.CellEndEdit
        If e.ColumnIndex = dgvHistorie.Columns("Omschrijving").Index Then
            SaveHistorie()
        End If
    End Sub

    Private Sub SaveHistorie()
        Dim lines As New List(Of String)
        For Each row As DataGridViewRow In dgvHistorie.Rows
            Dim datum = If(row.Cells("DatumTijd").Value IsNot Nothing, row.Cells("DatumTijd").Value.ToString(), "")
            Dim coord = If(row.Cells("Coordinaat").Value IsNot Nothing, row.Cells("Coordinaat").Value.ToString(), "")
            Dim omsValue = row.Cells("Omschrijving").Value
            Dim oms As String = If(omsValue IsNot Nothing, omsValue.ToString(), "")

            If Not String.IsNullOrEmpty(datum) AndAlso Not String.IsNullOrEmpty(coord) Then
                lines.Add($"{datum}|{coord}|{oms}")
            End If
        Next
        lines.Reverse()
        File.WriteAllLines(historiePath, lines.ToArray())
        Logger.Log("Historie opgeslagen na edit")
    End Sub

    Private Sub dgvHistorie_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHistorie.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim coord = dgvHistorie.Rows(e.RowIndex).Cells("Coordinaat").Value?.ToString()
            If Not String.IsNullOrEmpty(coord) Then
                parentForm.tbCoordinaten.Text = coord
            End If
            Me.Close()
        End If
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Me.Close()
    End Sub

    Private Sub frmHistorie_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' ← Positie opslaan bij sluiten
        SaveFormPosition(Me, ini)
    End Sub

End Class
