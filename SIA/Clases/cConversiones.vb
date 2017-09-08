Imports System.Collections
Imports cConexion
Public Class cConversiones
    Private hUnidad As New Hashtable()
    Private nGrupo As Integer
    Private sUnidad As String
    Private sUnidadConvercion As String
    Private nConversion As Double
    Private nCantidad As Double
    Private oCatalogoBaseDatos As New cCatalogoBaseDatos
    Private oGrupos As New cGrupos
    Public WriteOnly Property Unidad
        Set(value)
            sUnidad = value
        End Set
    End Property

    Public WriteOnly Property Cantidad
        Set(value)
            nCantidad = value
        End Set

    End Property

    Public ReadOnly Property UnidadConvercion
        Get
            Return sUnidadConvercion
        End Get

    End Property



    Public Property Conversion
        Get
            Return nConversion
        End Get
        Set(value)
            nConversion = value
        End Set
    End Property

    Public Property Grupo
        Get
            Return nGrupo
        End Get
        Set(value)
            nGrupo = value
        End Set
    End Property

    Public Sub CargaDatos()
        Dim tbUnidades As New DT
        tbUnidades.RecordSet = oCatalogoBaseDatos.TiposUnidad
        Do While tbUnidades.EOF = False
            sUnidad = tbUnidades.GetValue("unidad")
            oGrupos = New cGrupos(tbUnidades.GetValue("grupo"), tbUnidades.GetValue("conversion"))
            hUnidad.Add(tbUnidades.GetValue("unidad"), oGrupos)
            tbUnidades.MoveNext()
        Loop
    End Sub
    Public Function RealizaConversion() As Double
        Dim oGrupos As New cGrupos
        Dim nValor As Double
        oGrupos = hUnidad.Item(sUnidad)
        nValor = nCantidad * oGrupos.Conversion
        Select Case oGrupos.Grupo
            Case "GR"
                sUnidadConvercion = "GR"
            Case "ML"
                sUnidadConvercion = "ML"
            Case "UN"
                sUnidadConvercion = "UN"
        End Select
        Return nValor
    End Function

End Class
Public Class cGrupos

    Dim sGrupo As String
    Dim nConversion As Double
    Public Property Grupo
        Get
            Return sGrupo
        End Get
        Set(value)
            sGrupo = value
        End Set
    End Property
    Public Property Conversion
        Get
            Return nConversion
        End Get
        Set(value)
            nConversion = value
        End Set
    End Property
    Public Sub New()

    End Sub
    Public Sub New(ByVal nGrupoAux As String, ByVal nConversionAux As Double)
        Grupo = nGrupoAux
        Conversion = nConversionAux

    End Sub

End Class

