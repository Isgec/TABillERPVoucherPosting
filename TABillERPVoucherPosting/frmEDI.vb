Imports System.Xml
Public Class frmEDI
  Private Sub frmEDI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  End Sub
  Private Sub cmdFB2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFB2.Click
    With fbd1
      .ShowNewFolderButton = False
      .Description = "Select ERPLN Folder"
      .RootFolder = System.Environment.SpecialFolder.MyComputer
      .SelectedPath = ERPLNFolder.Text
      .ShowDialog()
      If .SelectedPath <> String.Empty Then
        ERPLNFolder.Text = .SelectedPath
      End If
    End With
  End Sub
  Private Sub cmdFB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFB1.Click
    With fbd1
      .ShowNewFolderButton = False
      .Description = "Select Vault Folder"
      .RootFolder = System.Environment.SpecialFolder.MyComputer
      .SelectedPath = vaultFolder.Text
      .ShowDialog()
      If .SelectedPath <> String.Empty Then
        vaultFolder.Text = .SelectedPath
      End If
    End With
  End Sub
  Dim oRet As ReturnedValues
  Dim _MessageText As String
  Private Delegate Sub MethodInvoker()
  Private Sub SetMsg(ByVal strMsg As String)
    Me._MessageText = strMsg
    Dim dTask As New MethodInvoker(AddressOf SetMessage)
    Me.lstBox1.Invoke(dTask)
  End Sub
  Private Sub SetMessage()
    Me.lstBox1.Items.Add(Me._MessageText)
  End Sub
  Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
    MyCleanUp()
  End Sub
  Private Sub FrmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    frmEDI_Resize(Nothing, Nothing)
    e.Cancel = True
  End Sub
  Private Sub MyCleanUp()
    Windows.Forms.Application.Exit()
  End Sub
  Private Sub InitializeTimerSettings()
    With Tmr1
      .Interval = 5000
      .Start()
    End With
  End Sub
  Private Sub cmdTmrStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTmrStart.Click
    If vaultFolder.Text = String.Empty Or ERPLNFolder.Text = String.Empty Then
      MsgBox("Source Folder or Target Folder is invalid.", MsgBoxStyle.OkOnly, "Vault-ERPLN Error")
      Exit Sub
    End If
    If vaultFolder.Text = ERPLNFolder.Text Then
      MsgBox("Source Folder and Target Folder can not be same.", MsgBoxStyle.OkOnly, "Vault-ERPLN Error")
      Exit Sub
    End If
    cmdTmrStart.Enabled = False
    cmdTmrStop.Enabled = True
    vaultFolder.Enabled = False
    ERPLNFolder.Enabled = False
    cmdFB1.Enabled = False
    cmdFB2.Enabled = False
    lstBox1.Items.Clear()
    InitializeTimerSettings()
  End Sub

  Private Sub cmdTmrStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTmrStop.Click
    Tmr1.Stop()
    vaultFolder.Enabled = True
    ERPLNFolder.Enabled = True
    cmdFB1.Enabled = True
    cmdFB2.Enabled = True
    cmdTmrStart.Enabled = True
    cmdTmrStop.Enabled = False
  End Sub
  Private cnt As Integer = 0
  Private Sub Tmr1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr1.Tick
    Tmr1.Stop()
    If lstBox1.Items.Count > 5 Then
      For I = lstBox1.Items.Count - 4 To 0 Step -1
        lstBox1.Items.RemoveAt(I)
      Next
    End If
    Dim pEdi As New vchPosting
    Dim aFiles() As String = IO.Directory.GetFiles(vaultFolder.Text, "*.xml", IO.SearchOption.TopDirectoryOnly)
    If aFiles.Length > 0 Then
      Dim mRet As New ReturnedValues
      For Each tmpFile As String In aFiles
        Dim tmpFileName As String = IO.Path.GetFileName(tmpFile)
        cnt += 1
        lstBox1.Items.Add(cnt & " Starting:" & tmpFileName)
        '======================
        If IsFileAvailable(tmpFile) Then
          mRet = pEdi.ExecuteProcess(tmpFileName, vaultFolder.Text, ERPLNFolder.Text)
          If Not mRet.IsError Then
            lstBox1.Items.Add("Processed:" & mRet.Status)
          Else
            lstBox1.Items.Add("Error:" & mRet.ErrMessage)
          End If
        End If
        '======================
      Next
    End If
    Tmr1.Start()

  End Sub
  Public Shared Function IsFileAvailable(ByVal FilePath As String) As Boolean
    If Not IO.File.Exists(FilePath) Then Return False
    Dim fInfo As IO.FileInfo = Nothing
    Dim st As IO.FileStream = Nothing
    Try
      fInfo = New IO.FileInfo(FilePath)
    Catch ex As Exception
      Return False
    End Try
    Dim ret As Boolean = False
    If fInfo.IsReadOnly Then
      If DateDiff(DateInterval.Minute, fInfo.CreationTime, Now) >= 1 Then
        fInfo.IsReadOnly = False
      End If
    End If
    Try
      st = fInfo.Open(IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
      ret = True
    Catch ex As Exception
      ret = False
    Finally
      If st IsNot Nothing Then
        st.Close()
      End If
    End Try
    Return ret
  End Function

  Private Sub OpenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
    ShowInTaskbar = True
    Ni1.Visible = False
    WindowState = System.Windows.Forms.FormWindowState.Normal
  End Sub
  Private Sub QuitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem.Click
    cmdExit_Click(Nothing, Nothing)
  End Sub
  Private Sub frmEDI_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    If WindowState = System.Windows.Forms.FormWindowState.Minimized Then
      ShowInTaskbar = False
      Ni1.Visible = True
      Ni1.ShowBalloonTip(1000)
    End If
  End Sub
  Private Sub Ni1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Ni1.MouseDoubleClick
    ShowInTaskbar = True
    Ni1.Visible = False
    WindowState = System.Windows.Forms.FormWindowState.Normal
  End Sub
  Private Sub lstBox1_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles lstBox1.ControlAdded
    If lstBox1.Items.Count > 5 Then
      For I = lstBox1.Items.Count - 4 To 0 Step -1
        lstBox1.Items.RemoveAt(I)
      Next
    End If
  End Sub

  'Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
  '  Dim oTR As XmlDocument = New XmlDocument
  '  Try
  '    oTR.Load("c:\temp\abc.xml")
  '  Catch ex As Exception
  '  End Try
  '  If oTR.ChildNodes(1).Name.ToLower = "ERPFunctions".ToLower Then
  '    For Each fun As XmlNode In oTR.ChildNodes(1)
  '      Dim dllName As String = fun.Attributes("dll").Value
  '      Dim funName As String = fun.Attributes("fun").Value
  '      Dim ReadStr As String = fun.InnerText
  '    Next
  '  End If

  'End Sub
End Class
