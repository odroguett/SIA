Imports System.Data
Imports System.Text.RegularExpressions
Imports cConexion

Public Class cUtiles


    Const DB_ORACLE = "ORACLE"
    Const DB_SQLSERVER = "SQLSERVER"
    Const DB_MYSQL = "MYSQL"

    Public Sub AddVarInsert(ByRef sColumns As String, ByRef sValues As String, ByVal sCampo As Object, ByVal sVar As Object, ByVal sTipo As String)
        sColumns = sColumns + sCampo + ", "
        If IsNull(sVar) Then
            sValues = sValues + " NULL, "
        Else
            sValues = sValues + GetSQLValue(sVar, sTipo) + ", "
        End If
    End Sub
    Public Function IsNull(ByVal sValor As Object) As Boolean
        If IsNothing(sValor) Or IsDBNull(sValor) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetSQLValue(ByVal sValue As Object, ByVal sType As String) As String
        'simplificada ***********revisar**************
        Dim sFormato As String = ""
        Dim sDecimal As String = ""
        Dim i As Integer
        Dim sChar As String = ""
        Dim sRes As String = ""


        Select Case sType
            Case "VARCHAR2", "CHAR", "VARCHAR", "STRING", "MAIL", "RUT"
                If IsNull(sValue) Then
                    GetSQLValue = "Null"
                ElseIf sValue = "" Then
                    GetSQLValue = "Null"
                Else
                    GetSQLValue = "'" + Trim(sValue) + "'"
                End If
            Case "NUMBER", "INTEGER", "FLOAT", "NUMERICO", "NUMERO", "DOUBLE"
                If IsDBNull(sValue) Or Not IsNumeric(sValue) Then
                    sValue = 0
                End If
                sFormato = Format(1, "0.0")
                sDecimal = Mid(sFormato, 2, 1)
                If sDecimal <> "." Then
                    For i = 1 To Len(CStr(sValue))
                        sChar = Mid(sValue, i, 1)
                        Select Case sChar
                            Case ","
                                sRes = sRes + "."
                            Case "."

                            Case Else
                                sRes = sRes + sChar
                        End Select
                    Next
                    GetSQLValue = sRes
                Else
                    GetSQLValue = sValue
                End If
            Case "DATE", "FECHA", "DATETIME"
                If IsNull(sValue) Then
                    GetSQLValue = "Null"
                ElseIf sValue.ToString = "" Then
                    GetSQLValue = "Null"
                ElseIf oConfig.BaseDeDatos = DB_ORACLE Then
                    GetSQLValue = "to_date('" + sValue + "','dd/mm/yyyy')"
                ElseIf oConfig.BaseDeDatos = DB_SQLSERVER Then
                    GetSQLValue = "'" + Format(CDate(sValue), "yyyy-MM-dd") + "'"
                ElseIf oConfig.BaseDeDatos = DB_MYSQL Then
                    GetSQLValue = "'" + Format(CDate(sValue), "yyyy/MM/dd") + "'"
                End If
            Case "DATEYEAR"
                If IsNull(sValue) Then
                    GetSQLValue = "Null"
                ElseIf sValue.ToString = "" Then
                    GetSQLValue = "Null"
                ElseIf oConfig.BaseDeDatos = DB_SQLSERVER Or oConfig.BaseDeDatos = DB_MYSQL Then
                    GetSQLValue = "to_date('" + sValue + "','dd/mm/yyyy')"
                ElseIf oConfig.BaseDeDatos = DB_ORACLE Then
                    GetSQLValue = "to_date('" + sValue + "','dd/mm/yyyy')"
                ElseIf oConfig.BaseDeDatos = DB_MYSQL Then
                    GetSQLValue = "'" + Format(CDate(sValue), "yyyy/MM/dd") + "'"
                End If
            Case "DATESEG"
                If IsNull(sValue) Then
                    GetSQLValue = "Null"
                ElseIf sValue.ToString = "" Then
                    GetSQLValue = "Null"
                Else
                    GetSQLValue = "'" + sValue + "'"
                End If
            Case "SEQUENCE"
                If IsNull(sValue) Then
                    GetSQLValue = "Null"
                ElseIf sValue.ToString = "" Then
                    GetSQLValue = "Null"
                Else
                    GetSQLValue = sValue
                End If

            Case Else
                If IsDBNull(sValue) Or sValue = Nothing Then
                    GetSQLValue = "Null"
                Else
                    GetSQLValue = "'" + sValue + "'"
                End If
        End Select
    End Function
    Public Function ValidaRut(ByVal sTexto As String) As Boolean
        Dim nLargo As Integer
        Dim txtRut As String
        txtRut = sTexto
        If txtRut <> "" Then
            ' Cambia "k" por "K"
            nLargo = Len(Trim(txtRut))
            If Right(Trim(txtRut), 1) = "k" Then
                txtRut = Left(Trim(txtRut), nLargo - 1) + "K"
            End If
            ' Elimina ceros al inicio
            Do Until Left(Trim(txtRut), 1) <> "0"
                txtRut = Mid(Trim(txtRut), 2)
            Loop
            If GetNiceRut(txtRut) Then
                If IsValidRut(txtRut) = False Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        End If
    End Function


    Private Function GetNiceRut(ByVal sRut As String) As Boolean
        Const Inicial As Integer = 1
        Const GUION As Integer = 2
        Const DIGITO As Integer = 3

        Dim i As Integer
        Dim iEstado As Integer
        Dim c As String '* 1
        Dim sNice As String = ""

        If sRut = "" Then
            Return False
            Exit Function
        End If

        iEstado = Inicial

        For i = 1 To Len(sRut)
            c = Mid(sRut, i, 1)
            Select Case iEstado
                Case Inicial
                    If IsDigit(c) Then
                        sNice = sNice + c
                    ElseIf c = "-" Then
                        iEstado = GUION
                        'sNice = Right("00000000" + sNice, 8) + "-"
                        sNice = Right("        " + sNice, 8) + "-"
                    ElseIf c <> "." Then
                        GetNiceRut = False
                        Exit Function
                    End If
                Case GUION
                    If IsDigit(c) Or c = "k" Or c = "K" Then
                        sNice = sNice + UCase(c)
                        iEstado = DIGITO
                    Else
                        GetNiceRut = False
                        Exit Function
                    End If
                Case DIGITO
                    'No se supone que lleguemos tal lejos
                    GetNiceRut = False
                    Exit Function
            End Select
        Next i

        If iEstado = DIGITO Then
            sRut = sNice
            GetNiceRut = True
        Else
            GetNiceRut = False
        End If

    End Function
    Function IsDigit(ByVal c As String) As Boolean
        If c >= "0" And c <= "9" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsValidRut(ByVal sRut10 As String) As Boolean

        Dim sDigito, sDigito10 As String
        Dim sRut8, sAux As String
        Dim nLength As Integer
        Dim nAux1, nAux2, nAux3, nAux4, nAux5, nAux6, nAux7, nAux8, nAux9, nAux10 As Integer

        'sRut10 = Trim(sRut10)
        sDigito10 = Right(Trim(sRut10), 1)
        nLength = Len(Trim(sRut10))
        sRut8 = Left(Trim(sRut10), nLength - 2)
        sAux = Right("00000000" + sRut8, 8)

        nAux1 = CInt(Mid(sAux, 1, 1)) * 3
        nAux2 = CInt(Mid(sAux, 2, 1)) * 2
        nAux3 = CInt(Mid(sAux, 3, 1)) * 7
        nAux4 = CInt(Mid(sAux, 4, 1)) * 6
        nAux5 = CInt(Mid(sAux, 5, 1)) * 5
        nAux6 = CInt(Mid(sAux, 6, 1)) * 4
        nAux7 = CInt(Mid(sAux, 7, 1)) * 3
        nAux8 = CInt(Mid(sAux, 8, 1)) * 2
        nAux9 = nAux1 + nAux2 + nAux3 + nAux4 + nAux5 + nAux6 + nAux7 + nAux8

        nAux10 = 11 - (nAux9 Mod 11)

        Select Case nAux10
            Case 10
                sDigito = "K"
            Case 11
                sDigito = 0
            Case Else
                sDigito = Trim(Str(nAux10))
        End Select

        If LCase(sDigito) = LCase(sDigito10) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function LastDay(ByVal dFecha As Date) As Date
        Dim dLastDay As Date
        Dim dInfinito As Date

        dInfinito = CDate("01/12/9999")
        If dFecha = dInfinito Then
            LastDay = CDate("31/12/9999")
            Exit Function
        End If
        dLastDay = dFecha
        Do Until Month(dLastDay) <> Month(dFecha)
            dLastDay = DateAdd(DateInterval.Day, 1, dLastDay)
        Loop

        dLastDay = DateAdd(DateInterval.Day, -1, dLastDay)
        LastDay = dLastDay
        Return LastDay
    End Function
    Public Function FirstDay(ByVal dFecha As Date) As Date
        Dim dFirstDay As Date
        dFirstDay = dFecha
        Do Until DateAndTime.Day(dFirstDay) = 1
            dFirstDay = DateAdd("d", -1, dFirstDay)
        Loop
        FirstDay = dFirstDay
    End Function


    Public Sub AddVarUpdate(ByRef sSql As String, ByVal sCampo As Object, ByVal sVar As Object, ByVal sTipo As String)
        sSql = sSql + sCampo + " = " + GetSQLValue(sVar, sTipo) + ","
    End Sub
    Public Sub AddFieldU(ByRef sSQL As String, ByVal sCampo As String, ByVal sVar As Object, ByVal sTipo As String)

        sSQL = sSQL + sCampo + " = " + GetSQLValue(sVar, GetDotItem(sTipo, 1)) + ","

    End Sub
    Public Sub AddFieldI(ByVal sColumns As String, ByVal sValues As String, ByVal txtUno As Object)
        'revisar adaptación
        sColumns = sColumns + txtUno.Text + ", "

        If txtUno.Text = "" Or txtUno.Text = "31/12/9999" Then
            sValues = sValues + " NULL, "
        Else
            sValues = sValues + GetSQLValue(txtUno.Text, GetDotItem(txtUno.tag, 1)) + ", "
        End If
    End Sub

    Public Shared Function GetItem(ByVal sUno As String, ByVal iPos As Integer, ByVal sChar As String) As String

        Dim i As Integer
        Dim iDot As Integer

        If iPos < 1 Then
            GetItem = ""
            Exit Function
        End If

        iDot = InStr(1, sUno, sChar)

        If iPos = 1 And iDot > 0 Then

            GetItem = Left(sUno, iDot - 1)
        ElseIf iPos = 1 And iDot = 0 Then
            'No hay puntos
            GetItem = sUno
        ElseIf iPos > 1 And iDot = 0 Then
            'No hay puntos e iDot se pasó
            GetItem = ""
        Else
            GetItem = GetItem(Mid(sUno, iDot + 1), iPos - 1, sChar)
        End If
    End Function

    Function GetDotItem(ByVal sUno As String, ByVal iPos As Integer) As String

        Dim iDot As Integer

        If iPos < 1 Then
            GetDotItem = ""
            Exit Function
        End If

        iDot = InStr(1, sUno, ".")

        If iPos = 1 And iDot > 0 Then

            GetDotItem = Left(sUno, iDot - 1)
        ElseIf iPos = 1 And iDot = 0 Then
            'No hay puntos
            GetDotItem = sUno
        ElseIf iPos > 1 And iDot = 0 Then
            'No hay puntos e iDot se pasó
            GetDotItem = ""
        Else
            GetDotItem = GetDotItem(Mid(sUno, iDot + 1), iPos - 1)
        End If
    End Function
    Function NoTail(ByVal sOld As Object, ByVal sTail As String) As Object

        Dim sNew As Object

        sNew = RTrim(sOld)

        If Right(LCase(sNew), Len(sTail)) = LCase(sTail) Then
            sNew = Left(sNew, Len(sNew) - Len(sTail))
        End If

        NoTail = sNew
    End Function

    Public Function ValidaTelefono(ByVal sTelefono As String) As Boolean
        If Not IsNumeric(sTelefono) Then
            Return False
        End If
        Return True
    End Function

    Public Function ValidarMail(ByVal sMail As String) As Boolean
        ' retorna true o false   
        Return Regex.IsMatch(sMail,
                "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
    End Function
    Public Function XNull(ByVal vVar As Object, Optional ByVal vXNull As Object = "", Optional ByVal sFormato As String = "") As Object
        If IsDBNull(vVar) Or IsNothing(vVar) Then
            XNull = vXNull
        Else
            If IsDBNull(sFormato) Or sFormato = "" Then
                XNull = vVar
            Else
                XNull = Format(vVar, sFormato)
            End If
        End If
    End Function
    Public Function SQLGet(ByVal sField As String,
                           ByVal sTable As String,
                           ByVal sWhere As String,
                           ByVal sOrder As String,
                           ByVal vNotFound As Object) As Object
        Dim sSQL As String
        Dim tB As New DataTable



        sSQL = "Select " & sField & " From " & sTable
        If sWhere <> vbNullString Then sSQL = sSQL & " Where " & sWhere
        If sOrder <> vbNullString Then sSQL = sSQL & " Order by " & sOrder

        If oConexion.OpenRecordSet(sSQL, "SQLGet") Then
            tB = oConexion.DT
        Else
            Return vNotFound
        End If

        If IsNull(tB.Rows(0).Item(0)) Then
            Return vNotFound
        Else
            Return tB.Rows(0).Item(0)
        End If
        Return vNotFound
    End Function
End Class
