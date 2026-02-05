<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddCoord
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
        Me.lblCoord = New System.Windows.Forms.Label()
        Me.lblOmschrijving = New System.Windows.Forms.Label()
        Me.tbCoord = New System.Windows.Forms.TextBox()
        Me.tbOmsCoord = New System.Windows.Forms.TextBox()
        Me.btnAnnuleer = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblCoord
        '
        Me.lblCoord.AutoSize = True
        Me.lblCoord.Location = New System.Drawing.Point(16, 15)
        Me.lblCoord.Name = "lblCoord"
        Me.lblCoord.Size = New System.Drawing.Size(103, 13)
        Me.lblCoord.TabIndex = 0
        Me.lblCoord.Text = "Coordinaten of route"
        '
        'lblOmschrijving
        '
        Me.lblOmschrijving.AutoSize = True
        Me.lblOmschrijving.Location = New System.Drawing.Point(16, 45)
        Me.lblOmschrijving.Name = "lblOmschrijving"
        Me.lblOmschrijving.Size = New System.Drawing.Size(67, 13)
        Me.lblOmschrijving.TabIndex = 1
        Me.lblOmschrijving.Text = "Omschrijving"
        '
        'tbCoord
        '
        Me.tbCoord.Location = New System.Drawing.Point(125, 12)
        Me.tbCoord.Name = "tbCoord"
        Me.tbCoord.Size = New System.Drawing.Size(242, 20)
        Me.tbCoord.TabIndex = 2
        '
        'tbOmsCoord
        '
        Me.tbOmsCoord.Location = New System.Drawing.Point(125, 38)
        Me.tbOmsCoord.Name = "tbOmsCoord"
        Me.tbOmsCoord.Size = New System.Drawing.Size(242, 20)
        Me.tbOmsCoord.TabIndex = 4
        '
        'btnAnnuleer
        '
        Me.btnAnnuleer.Location = New System.Drawing.Point(125, 75)
        Me.btnAnnuleer.Name = "btnAnnuleer"
        Me.btnAnnuleer.Size = New System.Drawing.Size(75, 23)
        Me.btnAnnuleer.TabIndex = 5
        Me.btnAnnuleer.Text = "Annuleer"
        Me.btnAnnuleer.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(211, 75)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Opslaan"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'AddCoord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 106)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnAnnuleer)
        Me.Controls.Add(Me.tbOmsCoord)
        Me.Controls.Add(Me.tbCoord)
        Me.Controls.Add(Me.lblOmschrijving)
        Me.Controls.Add(Me.lblCoord)
        Me.Name = "AddCoord"
        Me.Text = "AddCoord"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblCoord As Label
    Friend WithEvents lblOmschrijving As Label
    Friend WithEvents tbCoord As TextBox
    Friend WithEvents tbOmsCoord As TextBox
    Friend WithEvents btnAnnuleer As Button
    Friend WithEvents btnSave As Button
End Class
