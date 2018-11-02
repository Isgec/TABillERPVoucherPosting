Imports System.Xml
Public Class vchPosting
  Public Function ExecuteProcess(ByVal xmlFile As String, ByVal vaultPath As String, ByVal ERPLNPath As String) As ReturnedValues
    Dim mRet As New ReturnedValues
    Dim BatchID As String = ""
    Dim oTR As XmlDocument = New XmlDocument
    Try
      oTR.Load(vaultPath & "\" & xmlFile)
    Catch ex As Exception
      mRet.IsError = True
      mRet.ErrMessage = ex.Message
    End Try
    Try
      Dim oBN As ERP.lgInforLN = New ERP.lgInforLN
      oBN.Connect()
      'Processing
      If oTR.ChildNodes(1).Name.ToLower = "ERPFunctions".ToLower Then
        For Each fun As XmlNode In oTR.ChildNodes(1)
          Dim dllName As String = fun.Attributes("dll").Value
          Dim funName As String = fun.Attributes("fun").Value
          Dim ReadStr As String = fun.InnerText
          Dim oLRet As ERP.ReturnedValues = Nothing
          oLRet = oBN.Execute(funName, ReadStr, dllName)
        Next
      Else 'ERPFunction
        For Each cmp As XmlNode In oTR.ChildNodes(1)
          'Process for each company
          Dim IsProject As Boolean = False
          Dim First As Boolean = True
          Dim mProcessID As String = ""
          Dim ReadStr As String = ""
          Dim oLRet As ERP.ReturnedValues = Nothing
          Try
            IsProject = Convert.ToBoolean(cmp.Attributes("IsProject").Value)
          Catch ex As Exception
          End Try
          'Lines
          Dim oLines As XmlNode = cmp.ChildNodes(0)
          Dim oBatch As XmlNode = cmp.ChildNodes(1)
          Dim oErr As XmlNode = cmp.ChildNodes(2)
          For Each cmpChild As XmlNode In cmp.ChildNodes
            If cmpChild.Name.ToLower = "lines" Then
              For Each line As XmlNode In cmpChild.ChildNodes
                'If line.Name.ToLower = "creditline" Then Continue For
                ReadStr = line.InnerText
                If First Then
                  ReadStr = ReadStr.Replace("[ProcessID]", "")
                Else
                  ReadStr = ReadStr.Replace("[ProcessID]", mProcessID)
                End If
                If IsProject Then
                  'oLRet = oBN.Execute("test", "255")
                  oLRet = oBN.Execute("tfisgdll0102.add.perk.lines.new", ReadStr)
                Else
                  oLRet = oBN.Execute("tfisgdll0102.add.perk.lines", ReadStr)
                End If
                If oLRet.RetVal > 0 Then
                  Dim tmp As XmlNode = oErr.ChildNodes(0).CloneNode(True)
                  tmp.InnerText = ("Line: " & oLRet.RetStr)
                  oErr.AppendChild(tmp)
                Else
                  If line.Name.ToLower <> "creditline" Then
                    Dim oLval() As String = oLRet.DllRetStr.Split("|".ToCharArray)
                    mProcessID = oLval(0)
                    line.Attributes("ProcessID").Value = oLval(0)
                    line.Attributes("SerialNo").Value = oLval(1)
                  End If
                End If
                First = False
              Next
              Exit For
            End If
          Next
          'Batch
          For Each cmpChild As XmlNode In cmp.ChildNodes
            If cmpChild.Name.ToLower = "batch" Then
              ReadStr = cmpChild.InnerText
              ReadStr = ReadStr.Replace("[ProcessID]", mProcessID)
              If IsProject Then
                oLRet = oBN.Execute("tfisgdll0102.create.batch.integration", ReadStr)
              Else
                oLRet = oBN.Execute("tfisgdll0102.create.batch", ReadStr)
              End If
              Exit For
            End If
          Next
          'Get Batch No
          If oLRet.RetVal > 0 Then
            Dim tmp As XmlNode = oErr.ChildNodes(0).CloneNode(True)
            tmp.InnerText = ("Batch:" & oLRet.RetStr)
            oErr.AppendChild(tmp)
          Else
            For Each cmpChild As XmlNode In cmp.ChildNodes
              If cmpChild.Name.ToLower = "lines" Then
                For Each line As XmlNode In cmpChild.ChildNodes
                  If line.Name.ToLower <> "creditline" Then
                    ReadStr = line.Attributes("GetBatchDocument").Value
                    ReadStr = ReadStr.Replace("[ProcessID]", line.Attributes("ProcessID").Value)
                    ReadStr = ReadStr.Replace("[SerialNo]", line.Attributes("SerialNo").Value)
                    oLRet = oBN.Execute("tfisgdll0102.get.batch.document", ReadStr)
                    If oLRet.RetVal > 0 Then
                      Dim tmp As XmlNode = oErr.ChildNodes(0).CloneNode(True)
                      tmp.InnerText = ("Document: " & oLRet.RetStr)
                      oErr.AppendChild(tmp)
                    Else
                      Dim oLval() As String = oLRet.DllRetStr.Split("|".ToCharArray)
                      line.Attributes("BatchNo").Value = oLval(0)
                      line.Attributes("DocumentNo").Value = oLval(1)
                      line.Attributes("LineNo").Value = oLval(2)
                      If BatchID = String.Empty Then
                        BatchID = oLval(0)
                      End If
                    End If
                  End If
                Next
                Exit For
              End If
            Next
          End If
        Next
      End If 'ERPFunction 

      'End Processing
      oBN.Disconnect()
      Try
        oTR.Save(ERPLNPath & "\P_" & IO.Path.GetFileNameWithoutExtension(xmlFile) & ".xml")
        IO.File.Delete(vaultPath & "\" & xmlFile)
      Catch ex As Exception
        mRet.IsError = True
        mRet.ErrMessage = ex.Message
      End Try
    Catch ex As Exception
      mRet.IsError = True
      mRet.ErrMessage = ex.Message
    End Try
    Return mRet
  End Function
End Class
Public Class ReturnedValues
	Private _IsError As Boolean = False
	Private _ErrMessage As String = ""
	Private _Status As String = ""
	Private _xmlFile As String = ""
	Private _vaultPath As String = ""
	Private _ERPLNPath As String = ""
	Public Property Status As String
		Get
			Return _Status
		End Get
		Set(ByVal value As String)
			_Status = value
		End Set
	End Property
	Public Property ErrMessage As String
		Get
			Return _ErrMessage
		End Get
		Set(ByVal value As String)
			_ErrMessage = value
		End Set
	End Property
	Public Property IsError As Boolean
		Get
			Return _IsError
		End Get
		Set(ByVal value As Boolean)
			_IsError = value
		End Set
	End Property
	Public Property xmlFile() As String
		Get
			Return Me._xmlFile
		End Get
		Set(ByVal Value As String)
			Me._xmlFile = Value
		End Set
	End Property
	Public Property VaultPath() As String
		Get
			Return Me._vaultPath
		End Get
		Set(ByVal Value As String)
			Me._vaultPath = Value
		End Set
	End Property
	Public Property ERPLNPath() As String
		Get
			Return Me._ERPLNPath
		End Get
		Set(ByVal Value As String)
			Me._ERPLNPath = Value
		End Set
	End Property

End Class