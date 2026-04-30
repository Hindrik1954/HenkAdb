Imports System.Data
Imports System.IO
Imports System.Linq

Public Class frmGyms


    Private _henkAdbForm As HenkAdb
    Private ini As IniFile
    Private gymsTable As DataTable
    Private gymsView As DataView

    Public Sub New(parent As HenkAdb)
        InitializeComponent()
        Me._henkAdbForm = parent
    End Sub

    Private Sub frmGyms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim iniPath As String = Path.Combine(Application.StartupPath, "settings.ini")
        ini = New IniFile(iniPath)

        LoadFormPosition(Me, ini)
        SetupDgv()
        LoadGyms()
    End Sub

    Private Sub frmGyms_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If ini IsNot Nothing Then
            SaveFormPosition(Me, ini)
        End If
    End Sub

    Private Sub SetupDgv()
        dgvGyms.AutoGenerateColumns = False
        dgvGyms.Columns.Clear()

        Dim colGymNaam As New DataGridViewTextBoxColumn()
        colGymNaam.Name = "GymNaam"
        colGymNaam.HeaderText = "GymNaam"
        colGymNaam.DataPropertyName = "GymNaam"
        colGymNaam.Width = 220
        dgvGyms.Columns.Add(colGymNaam)

        Dim colPlaats As New DataGridViewTextBoxColumn()
        colPlaats.Name = "Plaats"
        colPlaats.HeaderText = "Plaats"
        colPlaats.DataPropertyName = "Plaats"
        colPlaats.Width = 150
        dgvGyms.Columns.Add(colPlaats)

        Dim colRaidroute As New DataGridViewTextBoxColumn()
        colRaidroute.Name = "Raidroute"
        colRaidroute.HeaderText = "Raidroute"
        colRaidroute.DataPropertyName = "Raidroute"
        colRaidroute.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvGyms.Columns.Add(colRaidroute)

        Dim colCoord As New DataGridViewTextBoxColumn()
        colCoord.Name = "Coordinaten"
        colCoord.HeaderText = "Coordinaten"
        colCoord.DataPropertyName = "Coordinaten"
        colCoord.Visible = False
        dgvGyms.Columns.Add(colCoord)

        dgvGyms.RowHeadersVisible = False
        dgvGyms.AllowUserToAddRows = False
        dgvGyms.AllowUserToDeleteRows = False
        dgvGyms.ReadOnly = True
        dgvGyms.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvGyms.MultiSelect = False
    End Sub

    Private Sub LoadGyms()
        Try
            gymsTable = GymRepository.GetGyms()
            gymsView = New DataView(gymsTable)
            dgvGyms.DataSource = gymsView
        Catch ex As Exception
            MessageBox.Show("Fout bij laden van gyms: " & ex.Message, "Gyms", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyFilters()
        If gymsView Is Nothing Then Return

        Dim gymFilter As String = EscapeLikeValue(tbGymFilter.Text.Trim())
        Dim plaatsFilter As String = EscapeLikeValue(tbPlaatsFilter.Text.Trim())
        Dim routeFilter As String = EscapeLikeValue(tbRouteFilter.Text.Trim())

        Dim filters As New List(Of String)

        If Not String.IsNullOrWhiteSpace(gymFilter) Then
            filters.Add($"Convert(GymNaam, 'System.String') LIKE '%{gymFilter}%'")
        End If

        If Not String.IsNullOrWhiteSpace(plaatsFilter) Then
            filters.Add($"Convert(Plaats, 'System.String') LIKE '%{plaatsFilter}%'")
        End If

        If Not String.IsNullOrWhiteSpace(routeFilter) Then
            filters.Add($"Convert(Raidroute, 'System.String') LIKE '%{routeFilter}%'")
        End If

        gymsView.RowFilter = String.Join(" AND ", filters)
    End Sub

    Private Function EscapeLikeValue(value As String) As String
        Return value.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]")
    End Function

    Private Sub tbGymFilter_TextChanged(sender As Object, e As EventArgs) Handles tbGymFilter.TextChanged
        ApplyFilters()
    End Sub

    Private Sub tbPlaatsFilter_TextChanged(sender As Object, e As EventArgs) Handles tbPlaatsFilter.TextChanged
        ApplyFilters()
    End Sub

    Private Sub tbRouteFilter_TextChanged(sender As Object, e As EventArgs) Handles tbRouteFilter.TextChanged
        ApplyFilters()
    End Sub

    Private Sub dgvGyms_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGyms.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim coordObj = dgvGyms.Rows(e.RowIndex).Cells("Coordinaten").Value
        If coordObj Is Nothing Then Return

        Dim coord As String = coordObj.ToString().Trim()
        If String.IsNullOrWhiteSpace(coord) Then Return

        _henkAdbForm.tbCoordinaten.Text = coord
        Me.Close()
    End Sub

    Private Sub btnClearFilter_Click(sender As Object, e As EventArgs) Handles btnClearFilter.Click
        tbGymFilter.Clear()
        tbPlaatsFilter.Clear()
        tbRouteFilter.Clear()
    End Sub
End Class