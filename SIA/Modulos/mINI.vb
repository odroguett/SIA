Imports cConexion

Module mINI
    Public sTipoConexion As String
    Public oConexion As New ConnectorBD
    Public oConstantes As New cConstantes
    Public oErrores As New cErrores
    Public oConversion As New cConversiones
    Public oParametros As New cParametros
    Public oConfig As New cConfig
    Public Outiles As New cUtiles
    Public Sub InicializarVariables()
        oConexion.TipoConexion = "MYSQL"
        oConexion.Conexion = "Data Source=localhost;Database=SIV;Uid=root;Password=srv12345"

    End Sub

End Module

