<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
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
        Me.serverBox = New System.Windows.Forms.ComboBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.cancelBtn = New System.Windows.Forms.Button()
        Me.okBtn = New System.Windows.Forms.Button()
        Me.passwordBox = New System.Windows.Forms.TextBox()
        Me.loginNameBox = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'serverBox
        '
        Me.serverBox.Location = New System.Drawing.Point(112, 72)
        Me.serverBox.MaxLength = 150
        Me.serverBox.Name = "serverBox"
        Me.serverBox.Size = New System.Drawing.Size(124, 21)
        Me.serverBox.TabIndex = 24
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Location = New System.Drawing.Point(12, 76)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(60, 23)
        Me.label3.TabIndex = 29
        Me.label3.Text = "Server:"
        '
        'cancelBtn
        '
        Me.cancelBtn.BackColor = System.Drawing.SystemColors.Control
        Me.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelBtn.Location = New System.Drawing.Point(160, 108)
        Me.cancelBtn.Name = "cancelBtn"
        Me.cancelBtn.Size = New System.Drawing.Size(76, 23)
        Me.cancelBtn.TabIndex = 26
        Me.cancelBtn.Text = "&Cancel"
        Me.cancelBtn.UseVisualStyleBackColor = False
        '
        'okBtn
        '
        Me.okBtn.BackColor = System.Drawing.SystemColors.Control
        Me.okBtn.Location = New System.Drawing.Point(96, 108)
        Me.okBtn.Name = "okBtn"
        Me.okBtn.Size = New System.Drawing.Size(60, 23)
        Me.okBtn.TabIndex = 25
        Me.okBtn.Text = "&OK"
        Me.okBtn.UseVisualStyleBackColor = False
        '
        'passwordBox
        '
        Me.passwordBox.Location = New System.Drawing.Point(112, 44)
        Me.passwordBox.Name = "passwordBox"
        Me.passwordBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.passwordBox.Size = New System.Drawing.Size(124, 20)
        Me.passwordBox.TabIndex = 23
        '
        'loginNameBox
        '
        Me.loginNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.loginNameBox.Location = New System.Drawing.Point(112, 12)
        Me.loginNameBox.Name = "loginNameBox"
        Me.loginNameBox.Size = New System.Drawing.Size(124, 20)
        Me.loginNameBox.TabIndex = 22
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Location = New System.Drawing.Point(12, 44)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(100, 23)
        Me.label2.TabIndex = 28
        Me.label2.Text = "Password:"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Location = New System.Drawing.Point(12, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(100, 23)
        Me.label1.TabIndex = 27
        Me.label1.Text = "Login Name:"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(251, 142)
        Me.Controls.Add(Me.serverBox)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.cancelBtn)
        Me.Controls.Add(Me.okBtn)
        Me.Controls.Add(Me.passwordBox)
        Me.Controls.Add(Me.loginNameBox)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Name = "LoginForm"
        Me.Text = "Server Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents serverBox As System.Windows.Forms.ComboBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents cancelBtn As System.Windows.Forms.Button
    Private WithEvents okBtn As System.Windows.Forms.Button
    Private WithEvents passwordBox As System.Windows.Forms.TextBox
    Private WithEvents loginNameBox As System.Windows.Forms.TextBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
