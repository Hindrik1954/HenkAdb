<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HenkAdb
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tbCoordinaten = New System.Windows.Forms.TextBox()
        Me.lblCoordinaten = New System.Windows.Forms.Label()
        Me.btnAllDevices = New System.Windows.Forms.Button()
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
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvKnownCoord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbCoordinaten
        '
        Me.tbCoordinaten.Location = New System.Drawing.Point(22, 25)
        Me.tbCoordinaten.Name = "tbCoordinaten"
        Me.tbCoordinaten.Size = New System.Drawing.Size(222, 20)
        Me.tbCoordinaten.TabIndex = 2
        '
        'lblCoordinaten
        '
        Me.lblCoordinaten.AutoSize = True
        Me.lblCoordinaten.Location = New System.Drawing.Point(19, 9)
        Me.lblCoordinaten.Name = "lblCoordinaten"
        Me.lblCoordinaten.Size = New System.Drawing.Size(67, 13)
        Me.lblCoordinaten.TabIndex = 3
        Me.lblCoordinaten.Text = "Coordinaten:"
        '
        'btnAllDevices
        '
        Me.btnAllDevices.Location = New System.Drawing.Point(158, 51)
        Me.btnAllDevices.Name = "btnAllDevices"
        Me.btnAllDevices.Size = New System.Drawing.Size(86, 34)
        Me.btnAllDevices.TabIndex = 5
        Me.btnAllDevices.Text = "Start"
        Me.btnAllDevices.UseVisualStyleBackColor = True
        '
        'btnCopyVanClipboard
        '
        Me.btnCopyVanClipboard.Location = New System.Drawing.Point(22, 51)
        Me.btnCopyVanClipboard.Name = "btnCopyVanClipboard"
        Me.btnCopyVanClipboard.Size = New System.Drawing.Size(86, 34)
        Me.btnCopyVanClipboard.TabIndex = 6
        Me.btnCopyVanClipboard.Text = "Plakken"
        Me.btnCopyVanClipboard.UseVisualStyleBackColor = True
        '
        'dgvDevices
        '
        Me.dgvDevices.AllowUserToAddRows = False
        Me.dgvDevices.AllowUserToDeleteRows = False
        Me.dgvDevices.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.dgvDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDevices.Location = New System.Drawing.Point(22, 145)
        Me.dgvDevices.Name = "dgvDevices"
        Me.dgvDevices.Size = New System.Drawing.Size(222, 208)
        Me.dgvDevices.TabIndex = 7
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(250, 282)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(86, 34)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "Verversen"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'chbAllDevices
        '
        Me.chbAllDevices.AutoSize = True
        Me.chbAllDevices.Location = New System.Drawing.Point(22, 122)
        Me.chbAllDevices.Name = "chbAllDevices"
        Me.chbAllDevices.Size = New System.Drawing.Size(91, 17)
        Me.chbAllDevices.TabIndex = 9
        Me.chbAllDevices.Text = "Alle toestellen"
        Me.chbAllDevices.UseVisualStyleBackColor = True
        '
        'lblAfstand
        '
        Me.lblAfstand.AutoSize = True
        Me.lblAfstand.Location = New System.Drawing.Point(19, 94)
        Me.lblAfstand.Name = "lblAfstand"
        Me.lblAfstand.Size = New System.Drawing.Size(46, 13)
        Me.lblAfstand.TabIndex = 12
        Me.lblAfstand.Text = "Afstand:"
        '
        'lblKm
        '
        Me.lblKm.AutoSize = True
        Me.lblKm.Location = New System.Drawing.Point(92, 94)
        Me.lblKm.Name = "lblKm"
        Me.lblKm.Size = New System.Drawing.Size(28, 13)
        Me.lblKm.TabIndex = 13
        Me.lblKm.Text = "0.00"
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(250, 322)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(86, 34)
        Me.btnStop.TabIndex = 14
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblCooldown
        '
        Me.lblCooldown.AutoSize = True
        Me.lblCooldown.Location = New System.Drawing.Point(142, 94)
        Me.lblCooldown.Name = "lblCooldown"
        Me.lblCooldown.Size = New System.Drawing.Size(54, 13)
        Me.lblCooldown.TabIndex = 15
        Me.lblCooldown.Text = "Cooldown"
        '
        'lblCdTijd
        '
        Me.lblCdTijd.AutoSize = True
        Me.lblCdTijd.Location = New System.Drawing.Point(215, 94)
        Me.lblCdTijd.Name = "lblCdTijd"
        Me.lblCdTijd.Size = New System.Drawing.Size(28, 13)
        Me.lblCdTijd.TabIndex = 16
        Me.lblCdTijd.Text = "0.00"
        '
        'dgvKnownCoord
        '
        Me.dgvKnownCoord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvKnownCoord.Location = New System.Drawing.Point(366, 12)
        Me.dgvKnownCoord.Name = "dgvKnownCoord"
        Me.dgvKnownCoord.Size = New System.Drawing.Size(335, 341)
        Me.dgvKnownCoord.TabIndex = 17
        '
        'btnSaveCoord
        '
        Me.btnSaveCoord.Location = New System.Drawing.Point(250, 242)
        Me.btnSaveCoord.Name = "btnSaveCoord"
        Me.btnSaveCoord.Size = New System.Drawing.Size(86, 34)
        Me.btnSaveCoord.TabIndex = 18
        Me.btnSaveCoord.Text = "Save coord"
        Me.btnSaveCoord.UseVisualStyleBackColor = True
        '
        'btnToestellen
        '
        Me.btnToestellen.Location = New System.Drawing.Point(250, 176)
        Me.btnToestellen.Name = "btnToestellen"
        Me.btnToestellen.Size = New System.Drawing.Size(86, 60)
        Me.btnToestellen.TabIndex = 19
        Me.btnToestellen.Text = "Toestellen (ont)koppelen"
        Me.btnToestellen.UseVisualStyleBackColor = True
        '
        'HenkAdb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(716, 365)
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
        Me.Controls.Add(Me.btnAllDevices)
        Me.Controls.Add(Me.lblCoordinaten)
        Me.Controls.Add(Me.tbCoordinaten)
        Me.Name = "HenkAdb"
        Me.Text = "HenkAdb"
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvKnownCoord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbCoordinaten As TextBox
    Friend WithEvents lblCoordinaten As Label
    Friend WithEvents btnAllDevices As Button
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
End Class
