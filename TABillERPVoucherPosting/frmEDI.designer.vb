<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEDI
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEDI))
    Me.fsw1 = New System.IO.FileSystemWatcher()
    Me.Ni1 = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.cms1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.vaultFolder = New System.Windows.Forms.TextBox()
    Me.fbd1 = New System.Windows.Forms.FolderBrowserDialog()
    Me.ERPLNFolder = New System.Windows.Forms.TextBox()
    Me.cmdFB1 = New System.Windows.Forms.Button()
    Me.cmdFB2 = New System.Windows.Forms.Button()
    Me.lstBox1 = New System.Windows.Forms.ListBox()
    Me.cmdExit = New System.Windows.Forms.Button()
    Me.Tmr1 = New System.Windows.Forms.Timer(Me.components)
    Me.cmdTmrStop = New System.Windows.Forms.Button()
    Me.cmdTmrStart = New System.Windows.Forms.Button()
    CType(Me.fsw1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.cms1.SuspendLayout()
    Me.SuspendLayout()
    '
    'fsw1
    '
    Me.fsw1.EnableRaisingEvents = True
    Me.fsw1.SynchronizingObject = Me
    '
    'Ni1
    '
    Me.Ni1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
    Me.Ni1.BalloonTipText = "Vault-ERPLN EDI"
    Me.Ni1.BalloonTipTitle = "EDI Service"
    Me.Ni1.ContextMenuStrip = Me.cms1
    Me.Ni1.Icon = CType(resources.GetObject("Ni1.Icon"), System.Drawing.Icon)
    Me.Ni1.Text = "Vault-ERP EDI"
    '
    'cms1
    '
    Me.cms1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.QuitToolStripMenuItem})
    Me.cms1.Name = "cms1"
    Me.cms1.Size = New System.Drawing.Size(104, 48)
    '
    'OpenToolStripMenuItem
    '
    Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
    Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
    Me.OpenToolStripMenuItem.Text = "Open"
    '
    'QuitToolStripMenuItem
    '
    Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
    Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
    Me.QuitToolStripMenuItem.Text = "Quit"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(73, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Source Folder"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 36)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(70, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Target Folder"
    '
    'vaultFolder
    '
    Me.vaultFolder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.vaultFolder.Location = New System.Drawing.Point(102, 9)
    Me.vaultFolder.Name = "vaultFolder"
    Me.vaultFolder.Size = New System.Drawing.Size(252, 20)
    Me.vaultFolder.TabIndex = 3
    Me.vaultFolder.Text = "C:\inetpub\wwwroot\App_Temp\TABill\s"
    '
    'ERPLNFolder
    '
    Me.ERPLNFolder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ERPLNFolder.Location = New System.Drawing.Point(102, 35)
    Me.ERPLNFolder.Name = "ERPLNFolder"
    Me.ERPLNFolder.Size = New System.Drawing.Size(252, 20)
    Me.ERPLNFolder.TabIndex = 4
    Me.ERPLNFolder.Text = "C:\inetpub\wwwroot\App_Temp\TABill\t"
    '
    'cmdFB1
    '
    Me.cmdFB1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdFB1.Location = New System.Drawing.Point(361, 6)
    Me.cmdFB1.Name = "cmdFB1"
    Me.cmdFB1.Size = New System.Drawing.Size(41, 23)
    Me.cmdFB1.TabIndex = 5
    Me.cmdFB1.Text = ". . ."
    Me.cmdFB1.UseVisualStyleBackColor = True
    '
    'cmdFB2
    '
    Me.cmdFB2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdFB2.Location = New System.Drawing.Point(360, 35)
    Me.cmdFB2.Name = "cmdFB2"
    Me.cmdFB2.Size = New System.Drawing.Size(41, 23)
    Me.cmdFB2.TabIndex = 6
    Me.cmdFB2.Text = ". . ."
    Me.cmdFB2.UseVisualStyleBackColor = True
    '
    'lstBox1
    '
    Me.lstBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lstBox1.FormattingEnabled = True
    Me.lstBox1.Location = New System.Drawing.Point(2, 109)
    Me.lstBox1.Name = "lstBox1"
    Me.lstBox1.Size = New System.Drawing.Size(411, 121)
    Me.lstBox1.TabIndex = 9
    '
    'cmdExit
    '
    Me.cmdExit.Location = New System.Drawing.Point(5, 80)
    Me.cmdExit.Name = "cmdExit"
    Me.cmdExit.Size = New System.Drawing.Size(69, 23)
    Me.cmdExit.TabIndex = 10
    Me.cmdExit.Text = "Exit"
    Me.cmdExit.UseVisualStyleBackColor = True
    '
    'Tmr1
    '
    '
    'cmdTmrStop
    '
    Me.cmdTmrStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdTmrStop.Enabled = False
    Me.cmdTmrStop.Location = New System.Drawing.Point(236, 80)
    Me.cmdTmrStop.Name = "cmdTmrStop"
    Me.cmdTmrStop.Size = New System.Drawing.Size(80, 23)
    Me.cmdTmrStop.TabIndex = 12
    Me.cmdTmrStop.Text = "Stop Timer"
    Me.cmdTmrStop.UseVisualStyleBackColor = True
    '
    'cmdTmrStart
    '
    Me.cmdTmrStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdTmrStart.Location = New System.Drawing.Point(322, 80)
    Me.cmdTmrStart.Name = "cmdTmrStart"
    Me.cmdTmrStart.Size = New System.Drawing.Size(80, 23)
    Me.cmdTmrStart.TabIndex = 11
    Me.cmdTmrStart.Text = "Start Timer"
    Me.cmdTmrStart.UseVisualStyleBackColor = True
    '
    'frmEDI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(414, 236)
    Me.Controls.Add(Me.cmdExit)
    Me.Controls.Add(Me.cmdTmrStop)
    Me.Controls.Add(Me.cmdTmrStart)
    Me.Controls.Add(Me.lstBox1)
    Me.Controls.Add(Me.cmdFB2)
    Me.Controls.Add(Me.cmdFB1)
    Me.Controls.Add(Me.ERPLNFolder)
    Me.Controls.Add(Me.vaultFolder)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmEDI"
    Me.ShowInTaskbar = False
    Me.Text = "Perks-ERPLn EDI Service"
    CType(Me.fsw1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.cms1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents fsw1 As System.IO.FileSystemWatcher
  Friend WithEvents cmdFB2 As System.Windows.Forms.Button
  Friend WithEvents cmdFB1 As System.Windows.Forms.Button
  Friend WithEvents ERPLNFolder As System.Windows.Forms.TextBox
  Friend WithEvents vaultFolder As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Ni1 As System.Windows.Forms.NotifyIcon
  Friend WithEvents cms1 As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents QuitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents fbd1 As System.Windows.Forms.FolderBrowserDialog
  Friend WithEvents lstBox1 As System.Windows.Forms.ListBox
	Friend WithEvents cmdExit As System.Windows.Forms.Button
	Friend WithEvents cmdTmrStop As System.Windows.Forms.Button
	Friend WithEvents cmdTmrStart As System.Windows.Forms.Button
	Friend WithEvents Tmr1 As System.Windows.Forms.Timer
End Class
