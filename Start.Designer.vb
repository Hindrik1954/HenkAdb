<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HenkAdb
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HenkAdb))
        Me.tbCoordinaten = New System.Windows.Forms.TextBox()
        Me.lblCoordinaten = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnCopyVanClipboard = New System.Windows.Forms.Button()
        Me.dgvDevices = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.chbAllDevices = New System.Windows.Forms.CheckBox()
        Me.lblAfstand = New System.Windows.Forms.Label()
        Me.lblKm = New System.Windows.Forms.Label()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.lblCooldown = New System.Windows.Forms.Label()
        Me.lblCdTijd = New System.Windows.Forms.Label()
        Me.dgvKnownCoord = New System.Windows.Forms.DataGridView()
        Me.btnSaveCoord = New System.Windows.Forms.Button()
        Me.btnToestellen = New System.Windows.Forms.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.tbFilter = New System.Windows.Forms.TextBox()
        Me.btnClearFilter = New System.Windows.Forms.Button()
        Me.btnHistorie = New System.Windows.Forms.Button()
        Me.btnGyms = New System.Windows.Forms.Button()
        Me.btnDB = New System.Windows.Forms.Button()
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvKnownCoord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbCoordinaten
        '
        Me.tbCoordinaten.Location = New System.Drawing.Point(15, 15)
        Me.tbCoordinaten.Name = "tbCoordinaten"
        Me.tbCoordinaten.Size = New System.Drawing.Size(175, 20)
        Me.tbCoordinaten.TabIndex = 2
        '
        'lblCoordinaten
        '
        Me.lblCoordinaten.AutoSize = True
        Me.lblCoordinaten.Location = New System.Drawing.Point(12, -1)
        Me.lblCoordinaten.Name = "lblCoordinaten"
        Me.lblCoordinaten.Size = New System.Drawing.Size(67, 13)
        Me.lblCoordinaten.TabIndex = 3
        Me.lblCoordinaten.Text = "Coordinaten:"
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(104, 41)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(86, 48)
        Me.btnStart.TabIndex = 5
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnCopyVanClipboard
        '
        Me.btnCopyVanClipboard.Location = New System.Drawing.Point(15, 41)
        Me.btnCopyVanClipboard.Name = "btnCopyVanClipboard"
        Me.btnCopyVanClipboard.Size = New System.Drawing.Size(86, 48)
        Me.btnCopyVanClipboard.TabIndex = 6
        Me.btnCopyVanClipboard.Text = "Plakken"
        Me.btnCopyVanClipboard.UseVisualStyleBackColor = True
        '
        'dgvDevices
        '
        Me.dgvDevices.AllowUserToAddRows = False
        Me.dgvDevices.AllowUserToDeleteRows = False
        Me.dgvDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDevices.Location = New System.Drawing.Point(12, 141)
        Me.dgvDevices.Name = "dgvDevices"
        Me.dgvDevices.Size = New System.Drawing.Size(222, 242)
        Me.dgvDevices.TabIndex = 7
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(334, 389)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(64, 34)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "Verversen"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'chbAllDevices
        '
        Me.chbAllDevices.AutoSize = True
        Me.chbAllDevices.Location = New System.Drawing.Point(15, 118)
        Me.chbAllDevices.Name = "chbAllDevices"
        Me.chbAllDevices.Size = New System.Drawing.Size(91, 17)
        Me.chbAllDevices.TabIndex = 9
        Me.chbAllDevices.Text = "Alle toestellen"
        Me.chbAllDevices.UseVisualStyleBackColor = True
        '
        'lblAfstand
        '
        Me.lblAfstand.AutoSize = True
        Me.lblAfstand.Location = New System.Drawing.Point(15, 92)
        Me.lblAfstand.Name = "lblAfstand"
        Me.lblAfstand.Size = New System.Drawing.Size(46, 13)
        Me.lblAfstand.TabIndex = 12
        Me.lblAfstand.Text = "Afstand:"
        '
        'lblKm
        '
        Me.lblKm.AutoSize = True
        Me.lblKm.Location = New System.Drawing.Point(67, 92)
        Me.lblKm.Name = "lblKm"
        Me.lblKm.Size = New System.Drawing.Size(28, 13)
        Me.lblKm.TabIndex = 13
        Me.lblKm.Text = "0.00"
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(583, 389)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(64, 34)
        Me.btnStop.TabIndex = 14
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblCooldown
        '
        Me.lblCooldown.AutoSize = True
        Me.lblCooldown.Location = New System.Drawing.Point(111, 92)
        Me.lblCooldown.Name = "lblCooldown"
        Me.lblCooldown.Size = New System.Drawing.Size(54, 13)
        Me.lblCooldown.TabIndex = 15
        Me.lblCooldown.Text = "Cooldown"
        '
        'lblCdTijd
        '
        Me.lblCdTijd.AutoSize = True
        Me.lblCdTijd.Location = New System.Drawing.Point(171, 92)
        Me.lblCdTijd.Name = "lblCdTijd"
        Me.lblCdTijd.Size = New System.Drawing.Size(28, 13)
        Me.lblCdTijd.TabIndex = 16
        Me.lblCdTijd.Text = "0.00"
        '
        'dgvKnownCoord
        '
        Me.dgvKnownCoord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvKnownCoord.Location = New System.Drawing.Point(256, 41)
        Me.dgvKnownCoord.Name = "dgvKnownCoord"
        Me.dgvKnownCoord.Size = New System.Drawing.Size(387, 342)
        Me.dgvKnownCoord.TabIndex = 17
        '
        'btnSaveCoord
        '
        Me.btnSaveCoord.Location = New System.Drawing.Point(251, 389)
        Me.btnSaveCoord.Name = "btnSaveCoord"
        Me.btnSaveCoord.Size = New System.Drawing.Size(64, 34)
        Me.btnSaveCoord.TabIndex = 18
        Me.btnSaveCoord.Text = "Save coord"
        Me.btnSaveCoord.UseVisualStyleBackColor = True
        '
        'btnToestellen
        '
        Me.btnToestellen.Location = New System.Drawing.Point(15, 389)
        Me.btnToestellen.Name = "btnToestellen"
        Me.btnToestellen.Size = New System.Drawing.Size(86, 34)
        Me.btnToestellen.TabIndex = 19
        Me.btnToestellen.Text = "Toestellen (ont)koppelen"
        Me.btnToestellen.UseVisualStyleBackColor = True
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Location = New System.Drawing.Point(253, -1)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(29, 13)
        Me.lblFilter.TabIndex = 20
        Me.lblFilter.Text = "Filter"
        '
        'tbFilter
        '
        Me.tbFilter.Location = New System.Drawing.Point(256, 15)
        Me.tbFilter.Name = "tbFilter"
        Me.tbFilter.Size = New System.Drawing.Size(100, 20)
        Me.tbFilter.TabIndex = 21
        '
        'btnClearFilter
        '
        Me.btnClearFilter.Location = New System.Drawing.Point(362, 15)
        Me.btnClearFilter.Name = "btnClearFilter"
        Me.btnClearFilter.Size = New System.Drawing.Size(75, 22)
        Me.btnClearFilter.TabIndex = 22
        Me.btnClearFilter.Text = "Schoon filter"
        Me.btnClearFilter.UseVisualStyleBackColor = True
        '
        'btnHistorie
        '
        Me.btnHistorie.Location = New System.Drawing.Point(417, 389)
        Me.btnHistorie.Name = "btnHistorie"
        Me.btnHistorie.Size = New System.Drawing.Size(64, 34)
        Me.btnHistorie.TabIndex = 23
        Me.btnHistorie.Text = "Historie"
        Me.btnHistorie.UseVisualStyleBackColor = True
        '
        'btnGyms
        '
        Me.btnGyms.Location = New System.Drawing.Point(500, 389)
        Me.btnGyms.Name = "btnGyms"
        Me.btnGyms.Size = New System.Drawing.Size(64, 34)
        Me.btnGyms.TabIndex = 24
        Me.btnGyms.Text = "Gyms"
        Me.btnGyms.UseVisualStyleBackColor = True
        '
        'btnDB
        '
        Me.btnDB.Location = New System.Drawing.Point(136, 397)
        Me.btnDB.Name = "btnDB"
        Me.btnDB.Size = New System.Drawing.Size(75, 23)
        Me.btnDB.TabIndex = 25
        Me.btnDB.Text = "DB enc"
        Me.btnDB.UseVisualStyleBackColor = True
        '
        'HenkAdb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(660, 432)
        Me.Controls.Add(Me.btnDB)
        Me.Controls.Add(Me.btnGyms)
        Me.Controls.Add(Me.btnHistorie)
        Me.Controls.Add(Me.btnClearFilter)
        Me.Controls.Add(Me.tbFilter)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.btnToestellen)
        Me.Controls.Add(Me.btnSaveCoord)
        Me.Controls.Add(Me.dgvKnownCoord)
        Me.Controls.Add(Me.lblCdTijd)
        Me.Controls.Add(Me.lblCooldown)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.lblKm)
        Me.Controls.Add(Me.lblAfstand)
        Me.Controls.Add(Me.chbAllDevices)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dgvDevices)
        Me.Controls.Add(Me.btnCopyVanClipboard)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.lblCoordinaten)
        Me.Controls.Add(Me.tbCoordinaten)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HenkAdb"
        Me.Text = "HenkAdb"
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvKnownCoord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbCoordinaten As TextBox
    Friend WithEvents lblCoordinaten As Label
    Friend WithEvents btnStart As Button
    Friend WithEvents btnCopyVanClipboard As Button
    Friend WithEvents dgvDevices As DataGridView
    Friend WithEvents btnRefresh As Button
    Friend WithEvents chbAllDevices As CheckBox
    Friend WithEvents lblAfstand As Label
    Friend WithEvents lblKm As Label
    Friend WithEvents btnStop As Button
    Friend WithEvents lblCooldown As Label
    Friend WithEvents lblCdTijd As Label
    Friend WithEvents dgvKnownCoord As DataGridView
    Friend WithEvents btnSaveCoord As Button
    Friend WithEvents btnToestellen As Button
    Friend WithEvents lblFilter As Label
    Friend WithEvents tbFilter As TextBox
    Friend WithEvents btnClearFilter As Button
    Friend WithEvents btnHistorie As Button
    Friend WithEvents btnGyms As Button
    Friend WithEvents btnDB As Button
End Class
