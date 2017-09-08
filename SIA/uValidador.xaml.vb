Imports System.Drawing
Imports System.Windows.Media.Animation

Public Class uValidador
    Private sTitulo As String
    Private sArrayError As New ArrayList
    Public WriteOnly Property Titulo()
        Set(ByVal value)
            sTitulo = value
        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public WriteOnly Property ArrayError()
        Set(ByVal value)
            sArrayError = value
        End Set
    End Property
    Public Sub Advertencia()
        Dim myFlowDoc As New FlowDocument()

        txtTitulo.Text = "Validación " + sTitulo
        For i = 0 To sArrayError.Count - 1
            myFlowDoc.Blocks.Add(New Paragraph(New Run(sArrayError.Item(i).ToString)))
            'txtError.AppendText(vbNewLine)
            'txtError.AppendText(vbTab + CStr(i + 1) + vbTab + sArrayError.Item(i).ToString)
        Next
        txtError.Document = myFlowDoc
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As RoutedEventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

End Class
