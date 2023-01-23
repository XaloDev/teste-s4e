Imports Microsoft.VisualBasic

Public Class Empresa
    Private _id As Integer
    Private _nome As String
    Private _cnpj As String

    Sub New()

    End Sub
    Sub New(ByVal nome As String, ByVal cnpj As String)
        _nome = nome
        _cnpj = cnpj
    End Sub

    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return _nome
        End Get
        Set(ByVal value As String)
            _nome = value
        End Set
    End Property

    Public Property Cnpj() As String
        Get
            Return _cnpj
        End Get
        Set(ByVal value As String)
            _cnpj = value
        End Set
    End Property
End Class
