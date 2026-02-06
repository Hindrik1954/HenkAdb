<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Toestellen
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
        Me.dgvToestellen = New System.Windows.Forms.DataGridView()
        Me.btnKoppelen = New System.Windows.Forms.Button()
        Me.btnOntkoppel = New System.Windows.Forms.Button()
        Me.btnAnnuleer = New System.Windows.Forms.Button()
        Me.cbKiesAlle = New System.Windows.Forms.CheckBox()
        Me.btnStopServer = New System.Windows.Forms.Button()
        CType(Me.dgvToestellen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvToestellen
        '
        Me.dgvToestellen.AllowUserToAddRows = False
        Me.dgvToestellen.AllowUserToDeleteRows = False
        Me.dgvToestellen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvToestellen.Location = New System.Drawing.Point(12, 35)
        Me.dgvToestellen.Name = "dgvToestellen"
        Me.dgvToestellen.Size = New System.Drawing.Size(184, 196)
        Me.dgvToestellen.TabIndex = 0
        '
        'btnKoppelen
        '
        Me.btnKoppelen.Location = New System.Drawing.Point(202, 35)
        Me.btnKoppelen.Name = "btnKoppelen"
        Me.btnKoppelen.Size = New System.Drawing.Size(78, 38)
        Me.btnKoppelen.TabIndex = 1
        Me.btnKoppelen.Text = "Koppel"
        Me.btnKoppelen.UseVisualStyleBackColor = True
        '
        'btnOntkoppel
        '
        Me.btnOntkoppel.Location = New System.Drawing.Point(202, 79)
        Me.btnOntkoppel.Name = "btnOntkoppel"
        Me.btnOntkoppel.Size = New System.Drawing.Size(78, 38)
        Me.btnOntkoppel.TabIndex = 2
        Me.btnOntkoppel.Text = "Ontkoppel"
        Me.btnOntkoppel.UseVisualStyleBackColor = True
        '
        'btnAnnuleer
        '
        Me.btnAnnuleer.Location = New System.Drawing.Point(202, 167)
        Me.btnAnnuleer.Name = "btnAnnuleer"
        Me.btnAnnuleer.Size = New System.Drawing.Size(78, 38)
        Me.btnAnnuleer.TabIndex = 3
        Me.btnAnnuleer.Text = "Annuleer"
        Me.btnAnnuleer.UseVisualStyleBackColor = True
        '
        'cbKiesAlle
        '
        Me.cbKiesAlle.AutoSize = True
        Me.cbKiesAlle.Location = New System.Drawing.Point(12, 12)
        Me.cbKiesAlle.Name = "cbKiesAlle"
        Me.cbKiesAlle.Size = New System.Drawing.Size(48, 17)
        Me.cbKiesAlle.TabIndex = 4
        Me.cbKiesAlle.Text = "Alles"
        Me.cbKiesAlle.UseVisualStyleBackColor = True
        '
        'btnStopServer
        '
        Me.btnStopServer.Location = New System.Drawing.Point(202, 123)
        Me.btnStopServer.Name = "btnStopServer"
        Me.btnStopServer.Size = New System.Drawing.Size(78, 38)
        Me.btnStopServer.TabIndex = 5
        Me.btnStopServer.Text = "Stop server"
        Me.btnStopServer.UseVisualStyleBackColor = True
        '
        'Toestellen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(288, 243)
        Me.Controls.Add(Me.btnStopServer)
        Me.Controls.Add(Me.cbKiesAlle)
        Me.Controls.Add(Me.btnAnnuleer)
        Me.Controls.Add(Me.btnOntkoppel)
        Me.Controls.Add(Me.btnKoppelen)
        Me.Controls.Add(Me.dgvToestellen)
        Me.Name = "Toestellen"
        Me.Text = "Toestellen"
        CType(Me.dgvToestellen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvToestellen As DataGridView
    Friend WithEvents btnKoppelen As Button
    Friend WithEvents btnOntkoppel As Button
    Friend WithEvents btnAnnuleer As Button
    Friend WithEvents cbKiesAlle As CheckBox
    Friend WithEvents btnStopServer As Button
End Class
