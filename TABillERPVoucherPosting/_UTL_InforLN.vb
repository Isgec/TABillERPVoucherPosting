Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports System

Namespace ERP
	Public Class lgInforLN
		Const PARSEDLL As String = "otfisgdll0102"
		Private oBaaN As Baan.BwCOleAutomationServer

    Public Function Execute(ByVal FunctionName As String, ByVal Arguments As String, Optional ByVal parseDLLName As String = "") As ReturnedValues
      SyncLock Me
        Dim oRet As New ReturnedValues
        Dim mSource As String = GetSource(FunctionName, Arguments)
        Dim mRet As Integer
        If parseDLLName = "" Then
          parseDLLName = PARSEDLL
        End If
        oRet.FunctionName = FunctionName
        oRet.Arguments = Arguments
        '==========================================
        'As it is causing UnHandled Error in BW API
        'Retry in this case only
        '2 sec delay in retry
        Dim RetryCount As Integer = 5
        Dim Retryed As Integer = 1
        '==========================================
Retry:
        Try
          mRet = oBaaN.ParseExecFunction(parseDLLName, mSource)
          If oBaaN.Error <> 0 Then
            Disconnect()
            oRet.RetVal = 1
            Select Case oBaaN.Error
              Case Is = -1
                oRet.RetStr = "DLL Unknown"
              Case Is = -2
                oRet.RetStr = "Function Unknown"
              Case Is = -3
                oRet.RetStr = "Syntax Error in Function Call"
            End Select
            oRet.RetStr = "BaaN Disconnected. Error : " & oRet.RetStr
          End If
        Catch ex As Exception
          If Retryed < RetryCount Then
            Retryed += 1
            Threading.Thread.Sleep(2000)
            GoTo Retry
          End If
          Disconnect()
          oRet.RetVal = 2
          oRet.RetStr = "Fatal error. : BaaN Disconnected."
        End Try
        If Not oBaaN Is Nothing Then
          Dim aStr() As String = oBaaN.ReturnValue.Split("||".ToCharArray, 2)
          If aStr(0) > "0" Then
            oRet.RetVal = 1
            If aStr.Length > 1 Then
              oRet.RetStr = aStr(1)
            End If
          Else
            oRet.RetVal = 0
            oRet.DllRetStr = aStr(1)
          End If
        End If
        Return oRet
      End SyncLock
    End Function
    Public Function Connect() As ReturnedValues
			Dim oBW() As Process
			Dim t As Process
			Dim oRet As New ReturnedValues
			Dim _may As Boolean
			oBW = Process.GetProcessesByName("bw")
			For Each t In oBW
        If t.ProcessName.ToLower = "bw" Then
          oRet.RetVal = 1
          oRet.RetStr = "BaaN is allready running !!! [Please Close BaaN]"
          _may = True
        End If
      Next
      If Not _may Then
        Dim ERPObjs As New List(Of String)
        Try
          Dim tr As IO.StreamReader = New IO.StreamReader(Application.StartupPath & "\BaaNObjects.txt")
          Dim str As String = tr.ReadLine
          Do While str IsNot Nothing
            ERPObjs.Add(str)
            str = tr.ReadLine
          Loop
          tr.Close()
        Catch ex As Exception
        End Try
        For Each str As String In ERPObjs
          Try
            oBaaN = CreateObject(str)
            oRet.RetVal = 0
            oRet.RetStr = "Connected"
            Exit For
          Catch ex As Exception
          End Try
        Next
        If oBaaN Is Nothing Then
          oRet.RetVal = 2
          oRet.RetStr = "Can Not Connect to BaaN."
          oBW = Process.GetProcessesByName("bw")
          For Each t In oBW
            If t.ProcessName.ToLower = "bw" Then
              t.Kill()
            End If
          Next
        End If
      End If
      oBW = Nothing
			Return oRet
		End Function
		Private Function GetSource(ByVal FunctionName As String, ByVal Arguments As String) As String
			Dim aArgs() As String
			Dim mSource As String
			Dim I As Integer
			mSource = ""
			If Arguments = "" Then
				mSource = FunctionName & "()"
			Else
				aArgs = Arguments.Split(",".ToCharArray)
				For I = 0 To aArgs.Length - 1
					If mSource = "" Then
						mSource = Chr(34) & aArgs(I) & Chr(34)
					Else
						mSource = mSource & "," & Chr(34) & aArgs(I) & Chr(34)
					End If
				Next
				mSource = FunctionName & "(" & mSource & ")"
			End If
			Return mSource
		End Function
		Public Sub Disconnect()
			Try
				oBaaN.Quit()
				oBaaN = Nothing
			Catch ex As Exception
				Try
					Dim oBW() As Process
					Dim t As Process
					oBW = Process.GetProcessesByName("bw")
					For Each t In oBW
						If t.ProcessName = "bw" Then
							t.Kill()
							Exit For
						End If
					Next
					oBaaN = Nothing
				Catch ex1 As Exception
				End Try
			End Try
		End Sub
		Public Sub Dispose()
			Try
				oBaaN.Quit()
				oBaaN = Nothing
			Catch ex As Exception
				Try
					Dim oBW() As Process
					Dim t As Process
					oBW = Process.GetProcessesByName("bw")
					For Each t In oBW
						If t.ProcessName = "bw" Then
							t.Kill()
							Exit For
						End If
					Next
					oBaaN = Nothing
				Catch ex1 As Exception
				End Try
			End Try
		End Sub
	End Class
	Public Class ReturnedValues
		Private _RetVal As Integer
		Private _DllRetVal As Integer
		Private _RetStr As String
		Private _DllRetStr As String
		Private _FunctionName As String
		Private _Arguments As String
		Public Property RetVal() As Integer
			Get
				Return Me._RetVal
			End Get
			Set(ByVal Value As Integer)
				Me._RetVal = Value
			End Set
		End Property
		Public Property DllRetVal() As Integer
			Get
				Return Me._DllRetVal
			End Get
			Set(ByVal Value As Integer)
				Me._DllRetVal = Value
			End Set
		End Property
		Public Property RetStr() As String
			Get
				Return Me._RetStr
			End Get
			Set(ByVal Value As String)
				Me._RetStr = Value
			End Set
		End Property
		Public Property DllRetStr() As String
			Get
				Return Me._DllRetStr
			End Get
			Set(ByVal Value As String)
				Me._DllRetStr = Value
			End Set
		End Property
		Public Property FunctionName() As String
			Get
				Return Me._FunctionName
			End Get
			Set(ByVal Value As String)
				Me._FunctionName = Value
			End Set
		End Property
		Public Property Arguments() As String
			Get
				Return Me._Arguments
			End Get
			Set(ByVal Value As String)
				Me._Arguments = Value
			End Set
		End Property
	End Class

End Namespace
