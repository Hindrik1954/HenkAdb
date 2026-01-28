<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CommDay
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
        Me.cbDevices = New System.Windows.Forms.ComboBox()
        Me.tbCoordinaten = New System.Windows.Forms.TextBox()
        Me.lblCoordinaten = New System.Windows.Forms.Label()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.btnAllDevices = New System.Windows.Forms.Button()
        Me.CopyVanClipboard = New System.Windows.Forms.Button()
        Me.dgvDevices = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Button()
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbDevices
        '
        Me.cbDevices.FormattingEnabled = True
        Me.cbDevices.Location = New System.Drawing.Point(22, 51)
        Me.cbDevices.Name = "cbDevices"
        Me.cbDevices.Size = New System.Drawing.Size(286, 21)
        Me.cbDevices.TabIndex = 1
        '
        'tbCoordinaten
        '
        Me.tbCoordinaten.Location = New System.Drawing.Point(22, 25)
        Me.tbCoordinaten.Name = "tbCoordinaten"
        Me.tbCoordinaten.Size = New System.Drawing.Size(286, 20)
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
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(323, 49)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(83, 23)
        Me.btnSend.TabIndex = 4
        Me.btnSend.Text = "1 toestel"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnAllDevices
        '
        Me.btnAllDevices.Location = New System.Drawing.Point(410, 49)
        Me.btnAllDevices.Name = "btnAllDevices"
        Me.btnAllDevices.Size = New System.Drawing.Size(83, 23)
        Me.btnAllDevices.TabIndex = 5
        Me.btnAllDevices.Text = "Alle toestellen"
        Me.btnAllDevices.UseVisualStyleBackColor = True
        '
        'CopyVanClipboard
        '
        Me.CopyVanClipboard.Location = New System.Drawing.Point(323, 22)
        Me.CopyVanClipboard.Name = "CopyVanClipboard"
        Me.CopyVanClipboard.Size = New System.Drawing.Size(170, 23)
        Me.CopyVanClipboard.TabIndex = 6
        Me.CopyVanClipboard.Text = "Copy van clipboard"
        Me.CopyVanClipboard.UseVisualStyleBackColor = True
        '
        'dgvDevices
        '
        Me.dgvDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDevices.Location = New System.Drawing.Point(22, 96)
        Me.dgvDevices.Name = "dgvDevices"
        Me.dgvDevices.Size = New System.Drawing.Size(286, 133)
        Me.dgvDevices.TabIndex = 7
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(323, 78)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(83, 23)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "Verversen"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'CommDay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 241)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dgvDevices)
        Me.Controls.Add(Me.CopyVanClipboard)
        Me.Controls.Add(Me.btnAllDevices)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.lblCoordinaten)
        Me.Controls.Add(Me.tbCoordinaten)
        Me.Controls.Add(Me.cbDevices)
        Me.Name = "CommDay"
        Me.Text = "CommDay"
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbDevices As ComboBox
    Friend WithEvents tbCoordinaten As TextBox
    Friend WithEvents lblCoordinaten As Label
    Friend WithEvents btnSend As Button
    Friend WithEvents btnAllDevices As Button
    Friend WithEvents CopyVanClipboard As Button
    Friend WithEvents dgvDevices As DataGridView
    Friend WithEvents btnRefresh As Button
End Class
