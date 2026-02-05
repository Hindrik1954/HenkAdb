<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CommDay
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
        dgvAccBeschikbaar = New DataGridView()
        dgvBezettingTf = New DataGridView()
        tbCoordinaten = New TextBox()
        lblCoordinaten = New Label()
        btnSlaOp = New Button()
        tbLevel = New TextBox()
        lblLevel = New Label()
        BtnAnnuleren = New Button()
        tbAfstand = New TextBox()
        tbCoolDown = New TextBox()
        BtnSchoonDb = New Button()
        tbCoordVV = New TextBox()
        lblCoordVV = New Label()
        lblAfstand = New Label()
        lblCdTijd = New Label()
        lblVangenOm = New Label()
        tbNewCdEinde = New TextBox()
        BtnVervers = New Button()
        btnCorrScherm = New Button()
        btnStoppen = New Button()
        dgvVangstPerPhone = New DataGridView()
        btnStapTerug = New Button()
        CType(dgvAccBeschikbaar, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvBezettingTf, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvVangstPerPhone, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvAccBeschikbaar
        ' 
        dgvAccBeschikbaar.AllowUserToAddRows = False
        dgvAccBeschikbaar.AllowUserToDeleteRows = False
        dgvAccBeschikbaar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccBeschikbaar.Location = New Point(21, 12)
        dgvAccBeschikbaar.Name = "dgvAccBeschikbaar"
        dgvAccBeschikbaar.ReadOnly = True
        dgvAccBeschikbaar.Size = New Size(516, 580)
        dgvAccBeschikbaar.TabIndex = 1
        ' 
        ' dgvBezettingTf
        ' 
        dgvBezettingTf.AllowUserToAddRows = False
        dgvBezettingTf.AllowUserToDeleteRows = False
        dgvBezettingTf.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvBezettingTf.Location = New Point(564, 12)
        dgvBezettingTf.Name = "dgvBezettingTf"
        dgvBezettingTf.ReadOnly = True
        dgvBezettingTf.Size = New Size(198, 227)
        dgvBezettingTf.TabIndex = 2
        ' 
        ' tbCoordinaten
        ' 
        tbCoordinaten.Location = New Point(122, 615)
        tbCoordinaten.Name = "tbCoordinaten"
        tbCoordinaten.Size = New Size(174, 23)
        tbCoordinaten.TabIndex = 3
        ' 
        ' lblCoordinaten
        ' 
        lblCoordinaten.AutoSize = True
        lblCoordinaten.Location = New Point(21, 618)
        lblCoordinaten.Name = "lblCoordinaten"
        lblCoordinaten.Size = New Size(73, 15)
        lblCoordinaten.TabIndex = 4
        lblCoordinaten.Text = "Coordinaten"
        ' 
        ' btnSlaOp
        ' 
        btnSlaOp.BackColor = Color.Turquoise
        btnSlaOp.Location = New Point(21, 694)
        btnSlaOp.Name = "btnSlaOp"
        btnSlaOp.Size = New Size(85, 31)
        btnSlaOp.TabIndex = 5
        btnSlaOp.Text = "Sla op"
        btnSlaOp.UseVisualStyleBackColor = False
        ' 
        ' tbLevel
        ' 
        tbLevel.Location = New Point(122, 647)
        tbLevel.Name = "tbLevel"
        tbLevel.Size = New Size(45, 23)
        tbLevel.TabIndex = 6
        ' 
        ' lblLevel
        ' 
        lblLevel.AutoSize = True
        lblLevel.Location = New Point(21, 655)
        lblLevel.Name = "lblLevel"
        lblLevel.Size = New Size(34, 15)
        lblLevel.TabIndex = 7
        lblLevel.Text = "Level"
        ' 
        ' BtnAnnuleren
        ' 
        BtnAnnuleren.BackColor = Color.FromArgb(CByte(255), CByte(128), CByte(255))
        BtnAnnuleren.Location = New Point(112, 694)
        BtnAnnuleren.Name = "BtnAnnuleren"
        BtnAnnuleren.Size = New Size(85, 31)
        BtnAnnuleren.TabIndex = 8
        BtnAnnuleren.Text = "Annuleren"
        BtnAnnuleren.UseVisualStyleBackColor = False
        ' 
        ' tbAfstand
        ' 
        tbAfstand.Location = New Point(564, 661)
        tbAfstand.Name = "tbAfstand"
        tbAfstand.PlaceholderText = "Afstand"
        tbAfstand.Size = New Size(70, 23)
        tbAfstand.TabIndex = 14
        tbAfstand.Tag = "C    Sub Main()"
        ' 
        ' tbCoolDown
        ' 
        tbCoolDown.Location = New Point(564, 721)
        tbCoolDown.Name = "tbCoolDown"
        tbCoolDown.PlaceholderText = "Cooldowntijd"
        tbCoolDown.Size = New Size(70, 23)
        tbCoolDown.TabIndex = 15
        ' 
        ' BtnSchoonDb
        ' 
        BtnSchoonDb.BackColor = Color.Red
        BtnSchoonDb.ForeColor = Color.Yellow
        BtnSchoonDb.Location = New Point(648, 747)
        BtnSchoonDb.Name = "BtnSchoonDb"
        BtnSchoonDb.Size = New Size(125, 32)
        BtnSchoonDb.TabIndex = 30
        BtnSchoonDb.Text = "Tabellen Klaarzetten"
        BtnSchoonDb.UseVisualStyleBackColor = False
        ' 
        ' tbCoordVV
        ' 
        tbCoordVV.Location = New Point(564, 541)
        tbCoordVV.Name = "tbCoordVV"
        tbCoordVV.Size = New Size(173, 23)
        tbCoordVV.TabIndex = 17
        ' 
        ' lblCoordVV
        ' 
        lblCoordVV.AutoSize = True
        lblCoordVV.Location = New Point(564, 515)
        lblCoordVV.Name = "lblCoordVV"
        lblCoordVV.Size = New Size(147, 15)
        lblCoordVV.TabIndex = 18
        lblCoordVV.Text = "Coordinaten vorige vangst"
        ' 
        ' lblAfstand
        ' 
        lblAfstand.AutoSize = True
        lblAfstand.Location = New Point(564, 635)
        lblAfstand.Name = "lblAfstand"
        lblAfstand.Size = New Size(48, 15)
        lblAfstand.TabIndex = 19
        lblAfstand.Text = "Afstand"
        ' 
        ' lblCdTijd
        ' 
        lblCdTijd.AutoSize = True
        lblCdTijd.Location = New Point(564, 695)
        lblCdTijd.Name = "lblCdTijd"
        lblCdTijd.Size = New Size(83, 15)
        lblCdTijd.TabIndex = 20
        lblCdTijd.Text = "Cd in minuten"
        ' 
        ' lblVangenOm
        ' 
        lblVangenOm.AutoSize = True
        lblVangenOm.Location = New Point(564, 575)
        lblVangenOm.Name = "lblVangenOm"
        lblVangenOm.Size = New Size(54, 15)
        lblVangenOm.TabIndex = 21
        lblVangenOm.Text = "Einde Cd"
        ' 
        ' tbNewCdEinde
        ' 
        tbNewCdEinde.Location = New Point(564, 601)
        tbNewCdEinde.Name = "tbNewCdEinde"
        tbNewCdEinde.Size = New Size(70, 23)
        tbNewCdEinde.TabIndex = 22
        ' 
        ' BtnVervers
        ' 
        BtnVervers.Location = New Point(255, 747)
        BtnVervers.Name = "BtnVervers"
        BtnVervers.Size = New Size(116, 32)
        BtnVervers.TabIndex = 23
        BtnVervers.Text = "Schermverversen"
        BtnVervers.UseVisualStyleBackColor = True
        ' 
        ' btnCorrScherm
        ' 
        btnCorrScherm.Location = New Point(21, 747)
        btnCorrScherm.Name = "btnCorrScherm"
        btnCorrScherm.Size = New Size(111, 32)
        btnCorrScherm.TabIndex = 25
        btnCorrScherm.Text = "Correctie scherm"
        btnCorrScherm.UseVisualStyleBackColor = True
        ' 
        ' btnStoppen
        ' 
        btnStoppen.Location = New Point(377, 747)
        btnStoppen.Name = "btnStoppen"
        btnStoppen.Size = New Size(113, 32)
        btnStoppen.TabIndex = 26
        btnStoppen.Text = "Stoppen"
        btnStoppen.UseVisualStyleBackColor = True
        ' 
        ' dgvVangstPerPhone
        ' 
        dgvVangstPerPhone.AllowUserToAddRows = False
        dgvVangstPerPhone.AllowUserToDeleteRows = False
        dgvVangstPerPhone.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvVangstPerPhone.Location = New Point(564, 256)
        dgvVangstPerPhone.Name = "dgvVangstPerPhone"
        dgvVangstPerPhone.ReadOnly = True
        dgvVangstPerPhone.Size = New Size(198, 247)
        dgvVangstPerPhone.TabIndex = 27
        ' 
        ' btnStapTerug
        ' 
        btnStapTerug.Location = New Point(138, 747)
        btnStapTerug.Name = "btnStapTerug"
        btnStapTerug.Size = New Size(111, 32)
        btnStapTerug.TabIndex = 28
        btnStapTerug.Text = "Stap terug"
        btnStapTerug.UseVisualStyleBackColor = True
        ' 
        ' CommDay
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ButtonFace
        ClientSize = New Size(785, 799)
        Controls.Add(btnStapTerug)
        Controls.Add(dgvVangstPerPhone)
        Controls.Add(btnStoppen)
        Controls.Add(btnCorrScherm)
        Controls.Add(BtnVervers)
        Controls.Add(tbNewCdEinde)
        Controls.Add(lblVangenOm)
        Controls.Add(lblCdTijd)
        Controls.Add(lblAfstand)
        Controls.Add(lblCoordVV)
        Controls.Add(tbCoordVV)
        Controls.Add(BtnSchoonDb)
        Controls.Add(tbCoolDown)
        Controls.Add(tbAfstand)
        Controls.Add(BtnAnnuleren)
        Controls.Add(lblLevel)
        Controls.Add(tbLevel)
        Controls.Add(btnSlaOp)
        Controls.Add(lblCoordinaten)
        Controls.Add(tbCoordinaten)
        Controls.Add(dgvBezettingTf)
        Controls.Add(dgvAccBeschikbaar)
        Name = "CommDay"
        Text = "Community Day"
        CType(dgvAccBeschikbaar, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvBezettingTf, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvVangstPerPhone, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents dgvAccBeschikbaar As DataGridView
    Friend WithEvents dgvBezettingTf As DataGridView
    Friend WithEvents tbCoordinaten As TextBox
    Friend WithEvents lblCoordinaten As Label
    Friend WithEvents btnSlaOp As Button
    Friend WithEvents tbLevel As TextBox
    Friend WithEvents lblLevel As Label
    Friend WithEvents BtnAnnuleren As Button
    Friend WithEvents tbAfstand As TextBox
    Friend WithEvents tbCoolDown As TextBox
    Friend WithEvents BtnSchoonDb As Button
    Friend WithEvents tbCoordVV As TextBox
    Friend WithEvents lblCoordVV As Label
    Friend WithEvents lblAfstand As Label
    Friend WithEvents lblCdTijd As Label
    Friend WithEvents lblVangenOm As Label
    Friend WithEvents tbNewCdEinde As TextBox
    Friend WithEvents BtnVervers As Button
    Friend WithEvents btnTelefoonCorr As Button
    Friend WithEvents btnCorrScherm As Button
    Friend WithEvents btnStoppen As Button
    Friend WithEvents dgvVangstPerPhone As DataGridView
    Friend WithEvents btnStapTerug As Button

End Class
