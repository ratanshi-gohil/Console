<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MonitorWindow
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
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.txtFieldValue = New System.Windows.Forms.TextBox()
        Me.lblFieldName = New System.Windows.Forms.Label()
        Me.lblFieldId = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.columnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwLoanData = New System.Windows.Forms.ListView()
        Me.columnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.lstLoans = New System.Windows.Forms.ListBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lstFolders = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(265, 274)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 27
        Me.btnRefresh.Text = "Refresh"
        '
        'btnCommit
        '
        Me.btnCommit.Location = New System.Drawing.Point(181, 274)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(75, 23)
        Me.btnCommit.TabIndex = 26
        Me.btnCommit.Text = "Commit"
        '
        'txtFieldValue
        '
        Me.txtFieldValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFieldValue.Location = New System.Drawing.Point(261, 242)
        Me.txtFieldValue.Name = "txtFieldValue"
        Me.txtFieldValue.Size = New System.Drawing.Size(348, 20)
        Me.txtFieldValue.TabIndex = 25
        '
        'lblFieldName
        '
        Me.lblFieldName.Location = New System.Drawing.Point(261, 221)
        Me.lblFieldName.Name = "lblFieldName"
        Me.lblFieldName.Size = New System.Drawing.Size(242, 16)
        Me.lblFieldName.TabIndex = 24
        '
        'lblFieldId
        '
        Me.lblFieldId.Location = New System.Drawing.Point(261, 198)
        Me.lblFieldId.Name = "lblFieldId"
        Me.lblFieldId.Size = New System.Drawing.Size(242, 16)
        Me.lblFieldId.TabIndex = 23
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(181, 245)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(37, 13)
        Me.label6.TabIndex = 22
        Me.label6.Text = "Value:"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(181, 221)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(63, 13)
        Me.label5.TabIndex = 21
        Me.label5.Text = "Field Name:"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(181, 198)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(46, 13)
        Me.label4.TabIndex = 20
        Me.label4.Text = "Field ID:"
        '
        'columnHeader3
        '
        Me.columnHeader3.Text = "Value"
        Me.columnHeader3.Width = 147
        '
        'columnHeader1
        '
        Me.columnHeader1.Text = "Field ID"
        Me.columnHeader1.Width = 48
        '
        'lvwLoanData
        '
        Me.lvwLoanData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwLoanData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2, Me.columnHeader3})
        Me.lvwLoanData.FullRowSelect = True
        Me.lvwLoanData.GridLines = True
        Me.lvwLoanData.HideSelection = False
        Me.lvwLoanData.Location = New System.Drawing.Point(181, 23)
        Me.lvwLoanData.MultiSelect = False
        Me.lvwLoanData.Name = "lvwLoanData"
        Me.lvwLoanData.Size = New System.Drawing.Size(428, 168)
        Me.lvwLoanData.TabIndex = 19
        Me.lvwLoanData.UseCompatibleStateImageBehavior = False
        Me.lvwLoanData.View = System.Windows.Forms.View.Details
        '
        'columnHeader2
        '
        Me.columnHeader2.Text = "Field"
        Me.columnHeader2.Width = 108
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(181, 6)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(146, 13)
        Me.label3.TabIndex = 18
        Me.label3.Text = "Selected Loan Summary Info:"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(11, 178)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(39, 13)
        Me.label2.TabIndex = 17
        Me.label2.Text = "Loans:"
        '
        'lstLoans
        '
        Me.lstLoans.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstLoans.Location = New System.Drawing.Point(9, 194)
        Me.lstLoans.Name = "lstLoans"
        Me.lstLoans.Size = New System.Drawing.Size(160, 108)
        Me.lstLoans.TabIndex = 16
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(11, 6)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(71, 13)
        Me.label1.TabIndex = 15
        Me.label1.Text = "Loan Folders:"
        '
        'lstFolders
        '
        Me.lstFolders.Location = New System.Drawing.Point(9, 23)
        Me.lstFolders.Name = "lstFolders"
        Me.lstFolders.Size = New System.Drawing.Size(160, 147)
        Me.lstFolders.TabIndex = 14
        '
        'MonitorWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(621, 324)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnCommit)
        Me.Controls.Add(Me.txtFieldValue)
        Me.Controls.Add(Me.lblFieldName)
        Me.Controls.Add(Me.lblFieldId)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.lvwLoanData)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.lstLoans)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.lstFolders)
        Me.Name = "MonitorWindow"
        Me.Text = "Encompass Loan Viewer (VB.NET)"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstFolders As System.Windows.Forms.ListBox
    Friend WithEvents lvwLoanData As System.Windows.Forms.ListView
    Friend WithEvents lstLoans As System.Windows.Forms.ListBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnCommit As System.Windows.Forms.Button
    Friend WithEvents txtFieldValue As System.Windows.Forms.TextBox
    Friend WithEvents lblFieldName As System.Windows.Forms.Label
    Friend WithEvents lblFieldId As System.Windows.Forms.Label
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents columnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents columnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents columnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label

End Class
