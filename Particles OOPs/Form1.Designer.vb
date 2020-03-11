<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.AddParticleButton = New System.Windows.Forms.Button()
        Me.PauseButton = New System.Windows.Forms.Button()
        Me.CollisionsCheckbox = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        'AddParticleButton
        '
        Me.AddParticleButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddParticleButton.Location = New System.Drawing.Point(34, 564)
        Me.AddParticleButton.Name = "AddParticleButton"
        Me.AddParticleButton.Size = New System.Drawing.Size(118, 69)
        Me.AddParticleButton.TabIndex = 0
        Me.AddParticleButton.Text = "Add Particle"
        Me.AddParticleButton.UseVisualStyleBackColor = True
        '
        'PauseButton
        '
        Me.PauseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PauseButton.Location = New System.Drawing.Point(158, 564)
        Me.PauseButton.Name = "PauseButton"
        Me.PauseButton.Size = New System.Drawing.Size(118, 69)
        Me.PauseButton.TabIndex = 1
        Me.PauseButton.Text = "Pause"
        Me.PauseButton.UseVisualStyleBackColor = True
        '
        'CollisionsCheckbox
        '
        Me.CollisionsCheckbox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CollisionsCheckbox.AutoSize = True
        Me.CollisionsCheckbox.Checked = True
        Me.CollisionsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CollisionsCheckbox.Location = New System.Drawing.Point(603, 616)
        Me.CollisionsCheckbox.Name = "CollisionsCheckbox"
        Me.CollisionsCheckbox.Size = New System.Drawing.Size(69, 17)
        Me.CollisionsCheckbox.TabIndex = 2
        Me.CollisionsCheckbox.Text = "Collisions"
        Me.CollisionsCheckbox.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 661)
        Me.Controls.Add(Me.CollisionsCheckbox)
        Me.Controls.Add(Me.PauseButton)
        Me.Controls.Add(Me.AddParticleButton)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.Text = "Particle Collision Simulator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents AddParticleButton As Button
    Friend WithEvents PauseButton As Button
    Friend WithEvents CollisionsCheckbox As CheckBox
End Class
