<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGyms
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
        Me.tbGymFilter = New System.Windows.Forms.TextBox()
        Me.dgvGyms = New System.Windows.Forms.DataGridView()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.tbPlaatsFilter = New System.Windows.Forms.TextBox()
        Me.lblFIlterGym = New System.Windows.Forms.Label()
        Me.lblPlaatsFilter = New System.Windows.Forms.Label()
        Me.lblRouteFilter = New System.Windows.Forms.Label()
        Me.tbRouteFilter = New System.Windows.Forms.TextBox()
        CType(Me.dgvGyms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbGymFilter
        '
        Me.tbGymFilter.Location = New System.Drawing.Point(12, 40)
        Me.tbGymFilter.Name = "tbGymFilter"
        Me.tbGymFilter.Size = New System.Drawing.Size(136, 20)
        Me.tbGymFilter.TabIndex = 0
        '
        'dgvGyms
        '
        Me.dgvGyms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGyms.Location = New System.Drawing.Point(12, 66)
        Me.dgvGyms.Name = "dgvGyms"
        Me.dgvGyms.Size = New System.Drawing.Size(383, 473)
        Me.dgvGyms.TabIndex = 2
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Location = New System.Drawing.Point(9, 11)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(29, 13)
        Me.lblFilter.TabIndex = 3
        Me.lblFilter.Text = "Filter"
        '
        'tbPlaatsFilter
        '
        Me.tbPlaatsFilter.Location = New System.Drawing.Point(154, 40)
        Me.tbPlaatsFilter.Name = "tbPlaatsFilter"
        Me.tbPlaatsFilter.Size = New System.Drawing.Size(100, 20)
        Me.tbPlaatsFilter.TabIndex = 4
        '
        'lblFIlterGym
        '
        Me.lblFIlterGym.AutoSize = True
        Me.lblFIlterGym.Location = New System.Drawing.Point(9, 24)
        Me.lblFIlterGym.Name = "lblFIlterGym"
        Me.lblFIlterGym.Size = New System.Drawing.Size(31, 13)
        Me.lblFIlterGym.TabIndex = 5
        Me.lblFIlterGym.Text = "Gym:"
        '
        'lblPlaatsFilter
        '
        Me.lblPlaatsFilter.AutoSize = True
        Me.lblPlaatsFilter.Location = New System.Drawing.Point(151, 24)
        Me.lblPlaatsFilter.Name = "lblPlaatsFilter"
        Me.lblPlaatsFilter.Size = New System.Drawing.Size(36, 13)
        Me.lblPlaatsFilter.TabIndex = 6
        Me.lblPlaatsFilter.Text = "Plaats"
        '
        'lblRouteFilter
        '
        Me.lblRouteFilter.AutoSize = True
        Me.lblRouteFilter.Location = New System.Drawing.Point(257, 24)
        Me.lblRouteFilter.Name = "lblRouteFilter"
        Me.lblRouteFilter.Size = New System.Drawing.Size(36, 13)
        Me.lblRouteFilter.TabIndex = 7
        Me.lblRouteFilter.Text = "Route"
        '
        'tbRouteFilter
        '
        Me.tbRouteFilter.Location = New System.Drawing.Point(260, 40)
        Me.tbRouteFilter.Name = "tbRouteFilter"
        Me.tbRouteFilter.Size = New System.Drawing.Size(100, 20)
        Me.tbRouteFilter.TabIndex = 8
        '
        'frmGyms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 551)
        Me.Controls.Add(Me.tbRouteFilter)
        Me.Controls.Add(Me.lblRouteFilter)
        Me.Controls.Add(Me.lblPlaatsFilter)
        Me.Controls.Add(Me.lblFIlterGym)
        Me.Controls.Add(Me.tbPlaatsFilter)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.dgvGyms)
        Me.Controls.Add(Me.tbGymFilter)
        Me.Name = "frmGyms"
        Me.Text = "Overzicht Gyms"
        CType(Me.dgvGyms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbGymFilter As TextBox
    Friend WithEvents dgvGyms As DataGridView
    Friend WithEvents lblFilter As Label
    Friend WithEvents tbPlaatsFilter As TextBox
    Friend WithEvents lblFIlterGym As Label
    Friend WithEvents lblPlaatsFilter As Label
    Friend WithEvents lblRouteFilter As Label
    Friend WithEvents tbRouteFilter As TextBox
End Class
