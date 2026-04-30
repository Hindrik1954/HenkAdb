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
        Me.tbPlaatsFilter = New System.Windows.Forms.TextBox()
        Me.tbRouteFilter = New System.Windows.Forms.TextBox()
        Me.btnClearFilter = New System.Windows.Forms.Button()
        CType(Me.dgvGyms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbGymFilter
        '
        Me.tbGymFilter.Location = New System.Drawing.Point(12, 545)
        Me.tbGymFilter.Name = "tbGymFilter"
        Me.tbGymFilter.Size = New System.Drawing.Size(136, 20)
        Me.tbGymFilter.TabIndex = 0
        '
        'dgvGyms
        '
        Me.dgvGyms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGyms.Location = New System.Drawing.Point(12, 12)
        Me.dgvGyms.Name = "dgvGyms"
        Me.dgvGyms.Size = New System.Drawing.Size(543, 527)
        Me.dgvGyms.TabIndex = 2
        '
        'tbPlaatsFilter
        '
        Me.tbPlaatsFilter.Location = New System.Drawing.Point(230, 544)
        Me.tbPlaatsFilter.Name = "tbPlaatsFilter"
        Me.tbPlaatsFilter.Size = New System.Drawing.Size(100, 20)
        Me.tbPlaatsFilter.TabIndex = 4
        '
        'tbRouteFilter
        '
        Me.tbRouteFilter.Location = New System.Drawing.Point(377, 543)
        Me.tbRouteFilter.Name = "tbRouteFilter"
        Me.tbRouteFilter.Size = New System.Drawing.Size(86, 20)
        Me.tbRouteFilter.TabIndex = 8
        '
        'btnClearFilter
        '
        Me.btnClearFilter.Location = New System.Drawing.Point(480, 541)
        Me.btnClearFilter.Name = "btnClearFilter"
        Me.btnClearFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnClearFilter.TabIndex = 9
        Me.btnClearFilter.Text = "Clear filter"
        Me.btnClearFilter.UseVisualStyleBackColor = True
        '
        'frmGyms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 576)
        Me.Controls.Add(Me.btnClearFilter)
        Me.Controls.Add(Me.tbRouteFilter)
        Me.Controls.Add(Me.tbPlaatsFilter)
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
    Friend WithEvents tbPlaatsFilter As TextBox
    Friend WithEvents tbRouteFilter As TextBox
    Friend WithEvents btnClearFilter As Button
End Class
