Imports System.Data.Odbc
Imports System.Data
Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.OleDb
Imports MySql.Data.MySqlClient

Public Class ConnectorBD

    Private sError As String
    Private sTipoConexion As String
    Private sConexion As String
    Private sConexionExcel As String
    Private bActivacion As Boolean = False
    Private sPathLog As String
    Private oWrite As New cLog3
    Public DT As New DataTable
    Private ExcelConexion As New OleDbConnection
    Const SQL = "SQLSERVER"
    Const MYSQL = "MYSQL"




    Public WriteOnly Property TipoConexion()
        Set(ByVal value)
            sTipoConexion = value
        End Set
    End Property

    Public WriteOnly Property Conexion()
        Set(ByVal value)
            sConexion = value
        End Set
    End Property
    Public WriteOnly Property ConexionExcel()
        Set(ByVal value)
            sConexionExcel = value
        End Set
    End Property
    Public ReadOnly Property CapturaError()
        Get
            Return sError
        End Get
    End Property
    Public Property CargaLog()
        Get
            Return oWrite.PathLog
        End Get
        Set(ByVal value)
            oWrite.PathLog = value
        End Set
    End Property
    Public Sub New()
        'oWrite.PathLog = System.Configuration.ConfigurationManager.AppSettings("Log3")
        'TipoConexion = System.Configuration.ConfigurationManager.AppSettings("TipoConexion")
        'Conexion = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnection").ToString
    End Sub


    Public Function LlenaDataSet(ByVal sSelect As String, ByVal sNombreTabla As String, ByVal ds As DataSet) As DataSet
        Try
            If oWrite.PathLog <> "" Then oWrite.EscribeQuery(sSelect)
            If sTipoConexion = SQL Then
                Using daSQL = New SqlDataAdapter(sSelect, sConexion)
                    SyncLock daSQL
                        If Not ds Is Nothing Then
                            daSQL.Fill(ds, sNombreTabla)
                        End If
                    End SyncLock
                End Using
            ElseIf sTipoConexion = MYSQL Then
                Using daSql = New MySqlDataAdapter(sSelect, sConexion)
                    SyncLock daSql
                        If Not ds Is Nothing Then
                            daSql.Fill(ds, sNombreTabla)
                        End If
                    End SyncLock

                End Using
            Else
                Using daODBC = New OdbcDataAdapter(sSelect, sConexion)
                    SyncLock daODBC
                        If Not ds Is Nothing Then
                            daODBC.Fill(ds, sNombreTabla)
                        End If
                    End SyncLock
                End Using
            End If

        Catch ex As Exception
            sError = ex.Message
            oWrite.EscribeLog("LLenaDataSET", "Falla Carga DataTable  " + sNombreTabla + " " + sError)
        End Try
        Return ds
    End Function

    Public Function LlenaDataSetExcel(ByVal sSelect As String, ByVal sNombreTabla As String, ByVal ds As DataSet) As DataSet
        Try
            Using oleda As OleDbDataAdapter = New OleDbDataAdapter(sSelect, sConexionExcel)
                oleda.TableMappings.Add("Tabla", "TestTabla")
                oleda.Fill(ds, sNombreTabla)
            End Using
            Return ds
        Catch ex As Exception
            sError = ex.Message
            oWrite.EscribeLog("LLenaDataSET", "Falla Carga DataTable  " + sNombreTabla + " " + sError)
            Return Nothing
        End Try
    End Function

    Public Function CLower(ByVal sText As String) As String
        CLower = "upper(" & sText & ")"
    End Function
    Public Function GetSQLQuoted(ByVal sText As Object) As String
        Dim i As Integer
        Dim sRes As String = ""
        Dim sChar As String = ""

        For i = 1 To Len(sText)
            sChar = Mid(sText, i, 1)
            If sChar = "'" Then
                sRes = sRes + "''"
            Else
                sRes = sRes + sChar
            End If
        Next
        GetSQLQuoted = sRes
    End Function
    Public Sub AperturaConexionExcel()
        Try
            ExcelConexion.ConnectionString = sConexionExcel
            ExcelConexion.Open()
        Catch ex As Exception
            sError = ex.Message
            oWrite.EscribeLog("ExcelConexion", "Falla Instruccion  " + " Instruccion: ExcelConexion.Open() " + sError)
        End Try
    End Sub

    Public Sub CierreConexionExcel()
        ExcelConexion.Close()
    End Sub

    Public Function ExecBoolExcel(ByVal sEjecucion As String) As Boolean

        Dim oDbCommand As New OleDbCommand()
        Try
            If oWrite.PathLog <> "" Then oWrite.EscribeQuery(sEjecucion)
            oDbCommand.Connection = ExcelConexion
            oDbCommand.CommandText = sEjecucion
            oDbCommand.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            sError = ex.Message
            oWrite.EscribeLog("ExecBool", "Falla Instruccion  " + " Instruccion: " + sEjecucion + " " + sError)
            Return False
        End Try


    End Function
    Public Function ValidaTransaccion(ByVal sEjecucion As String) As Boolean
        Dim success As Boolean
        Dim sqlTransaccion As SqlTransaction
        If oWrite.PathLog <> "" Then oWrite.EscribeQuery(sEjecucion)
        If sTipoConexion = "SQL" Then
            Using SQLConexion As New SqlConnection(sConexion)
                SQLConexion.Open()
                sqlTransaccion = SQLConexion.BeginTransaction
                Try
                    Using SQLCommand As New SqlCommand()
                        SQLCommand.Transaction = sqlTransaccion
                        SQLCommand.Connection = SQLConexion
                        SQLCommand.CommandText = sEjecucion
                        SQLCommand.ExecuteNonQuery()
                        success = True
                    End Using
                Catch ex As Exception
                    sError = ex.Message
                    oWrite.EscribeLog("ExecBool", "Falla Instruccion  " + " Instruccion: " + sEjecucion + " " + sError)
                    success = False
                Finally

                End Try
            End Using
            Return success
        End If




    End Function


    Public Function ExecBool(ByVal sEjecucion As String) As Boolean
        Dim success As Boolean
        Try

            If oWrite.PathLog <> "" Then oWrite.EscribeQuery(sEjecucion)
            If sTipoConexion = "SQL" Then
                Using SQLConexion As New SqlConnection(sConexion)
                    Using SQLCommand As New SqlCommand(sEjecucion, SQLConexion)
                        SQLCommand.Connection.Open()
                        SQLCommand.ExecuteNonQuery()
                        success = True
                    End Using
                End Using
            ElseIf sTipoConexion = MYSQL Then
                Using SQLConexion As New MySqlConnection(sConexion)
                    Using SQLCommand As New MySqlCommand(sEjecucion, SQLConexion)
                        SQLCommand.Connection.Open()
                        SQLCommand.ExecuteNonQuery()
                        success = True
                    End Using
                End Using
            Else
                Using oCon As New OdbcConnection(sConexion)
                    Using myCommand As New OdbcCommand(sEjecucion, oCon)
                        myCommand.Connection.Open()
                        myCommand.ExecuteNonQuery()
                        success = True
                    End Using
                    oCon.Close()
                End Using

            End If

        Catch ex As Exception
            sError = ex.Message
            oWrite.EscribeLog("ExecBool", "Falla Instruccion  " + " Instruccion: " + sEjecucion + " " + sError)
            success = False
        Finally

        End Try
        Return success
    End Function

    Public Function OpenRecordSet(ByVal sSelect As String, Optional ByVal sNombreTabla As String = "", Optional ByVal bExcel As Boolean = False) As Boolean
        Dim DS As New DataSet
        If sNombreTabla = "" Then sNombreTabla = "AUXILIAR"
        Try
            If bExcel = True Then
                DS = LlenaDataSetExcel(sSelect, sNombreTabla, DS)
            Else
                DS = LlenaDataSet(sSelect, sNombreTabla, DS)
            End If
            If LLenaDataTable(DS, sNombreTabla) Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overloads Function LLenaDataTable(ByVal DataSet As DataSet, ByVal sNombreTabla As String) As Boolean
        DT = Nothing
        Try
            DT = DataSet.Tables(sNombreTabla)
            If IsNothing(DT) Then
                Return False
            End If
            If DT.Rows.Count = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            sError = ex.Message
            Return False
        End Try
    End Function



End Class
