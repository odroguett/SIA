Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Threading
Public Class cLog3
    Private ThLog As Thread
    Private ThQuery As Thread
    Private sPathLog As String
    Private sProceso As String
    Private sComentario As String
    Private sConsulta As String
    Private sTrStreamWriter As StreamWriter

    Public Property PathLog()
        Get
            Return sPathLog
        End Get
        Set(ByVal value)
            sPathLog = value

        End Set
    End Property
    Public Sub New()
        ThLog = New Threading.Thread(AddressOf WriteLineLog)
        ThQuery = New Threading.Thread(AddressOf WriteLineQuery)
    End Sub
    Public Sub EscribeLog(ByVal sDato As String, ByVal sDato1 As String)
        Try
            sProceso = sDato
            sComentario = sDato1
            Do While ThLog.IsAlive = True
            Loop
            ThLog = New Threading.Thread(AddressOf WriteLineLog)
            ThLog.Start()

        Catch ex As Exception
        End Try
    End Sub


    Public Sub EscribeQuery(ByVal sSQl As String)
        Try
            sConsulta = sSQl
            Do While ThQuery.IsAlive = True
            Loop
            ThQuery = New Threading.Thread(AddressOf WriteLineQuery)
            ThQuery.Start()

        Catch ex As Exception
        End Try
    End Sub

    Public Sub WriteLineLog()
        Try
            Dim dFechaProceso As DateTime = DateTime.Now
            sTrStreamWriter = New StreamWriter(sPathLog, True, System.Text.Encoding.Default)
            sTrStreamWriter.WriteLine(dFechaProceso.ToString("MM/dd/yyyy") + vbTab + Format(TimeOfDay, "hh:mm:ss") + vbTab + sProceso + vbTab + sComentario)
            sTrStreamWriter.Close()
        Catch ex As Exception

        End Try
        
    End Sub
    Public Sub WriteLineQuery()
        Try
            Dim dFechaProceso As DateTime = DateTime.Now
            sTrStreamWriter = New StreamWriter(sPathLog, True, System.Text.Encoding.Default)
            sTrStreamWriter.WriteLine(dFechaProceso.ToString("MM/dd/yyyy") + vbTab + Format(TimeOfDay, "hh:mm:ss") + vbTab + sConsulta)
            sTrStreamWriter.Close()
        Catch ex As Exception
        End Try

    End Sub


End Class
