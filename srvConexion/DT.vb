Imports System.Data.OleDb
Public Class DT
    Inherits DataTable
    Public DT As New DataTable
    Private iPosicion As Double
    Public Sub New()
        iPosicion = 0
    End Sub
    Public Property RecordSet()
        Get
            Return DT
        End Get
        Set(ByVal value)
            DT = value
        End Set
    End Property


    Public Property BookMark()
        Get
            Return iPosicion
        End Get
        Set(ByVal value)
            iPosicion = value
        End Set
    End Property

    Public Function EOF() As Boolean
        Try
            If DT.Rows.Count = iPosicion Or DT.Rows.Count = 0 Then
                'iPosicion = 0
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try

    End Function
    Public Function GetValue(ByVal sCampo As String) As Object
        Try
            Dim ofila As DataRow = DT.Rows(iPosicion)
            'Return DT.Rows(iPosicion).Item(sCampo)
            If Not IsDBNull(DT.Rows(iPosicion).Item(sCampo)) Then
                Return DT.Rows(iPosicion).Item(sCampo)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetTipo(ByVal iPosicion As Integer) As Object
        Try
            If Not IsDBNull(DT.Rows(0).Item(iPosicion)) Then
                Return DT.Rows(0).Item(iPosicion)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetTipoColumna(ByVal sCampo As String) As Object
        Try
            Dim ofila As DataRow = DT.Rows(iPosicion)
            'Return DT.Rows(iPosicion).Item(sCampo)
            If Not IsDBNull(DT.Rows(iPosicion).Item(sCampo)) Then
                Select Case UCase(DT.Rows(iPosicion).Item(sCampo).GetType.Name)
                    Case "BOOLEAN", "BYTE", "DECIMAL", "DOUBLE", "INT16", "INT32", "INT64", "SBYTE", "SSINGLE", "UINT16", "UINT32", "UINT64"
                        Return "NUMBER"
                    Case "CHAR", "STRING"
                        Return "STRING"
                    Case "DATETIME", "TIMESPAN"
                        Return "DATE"

                    Case Else
                        Return UCase(DT.Rows(iPosicion).Item(sCampo).GetType.Name)
                End Select
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    
    Public Function GetTotalColumns() As Integer
        Try
            Return DT.Columns.Count - 1
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Public Function NombreColumna(ByVal iPosicion As Integer) As String
        Try
            Return DT.Columns(iPosicion).ColumnName
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub MoveNext()
        iPosicion = iPosicion + 1
    End Sub

    Public Sub MoveFirst()
        iPosicion = 0
    End Sub

    Public Sub MoveLast()
        iPosicion = DT.Rows.Count
    End Sub

    Public Shadows Function Rows() As DataRowCollection
        Return DT.Rows
    End Function

    Public Sub Disconnect()
        DT = Nothing
        iPosicion = 0
    End Sub

    Public Function RecordCount() As Double
        Return DT.Rows.Count - 1
    End Function
    Public Sub ReplaceValue(ByVal sCampo As String, ByVal sValue As Object)
        Try
            Dim ofila As DataRow = DT.Rows(iPosicion)
            If Not IsDBNull(DT.Rows(iPosicion).Item(sCampo)) Then
                DT.Rows(iPosicion).Item(sCampo) = sValue
            End If
        Catch ex As Exception

        End Try
    End Sub



End Class
