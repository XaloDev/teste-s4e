Imports System.Configuration.Configuration
Imports System.Configuration.ConnectionStringSettings
Imports System.Data.SqlClient

Partial Class empresas
    Inherits System.Web.UI.Page


    Protected Sub BancoDeDadosEmpresas_Inserted(sender As Object, e As SqlDataSourceStatusEventArgs) Handles BancoDeDadosEmpresas.Inserted
        If (e.Exception IsNot Nothing) Then
            labelMensagem.Text = e.Exception.Message
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rootWebConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot")
        Dim connString As System.Configuration.ConnectionStringSettings
        connString = rootWebConfig.ConnectionStrings.ConnectionStrings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection()
        con.ConnectionString = connString.ToString()
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = con
        cmd.CommandText = "Insert into Empresa (nome,cnpj) values (@nome,@cnpj); SELECT SCOPE_IDENTITY()"
        Dim cnpj As String = txtBoxCnpj.Text
        If cnpj.Length > 14 Then
            cnpj = cnpj.Substring(0, 14)
        End If
        Dim nome As String = txtBoxNome.Text
        If nome.Length > 200 Then
            nome = cnpj.Substring(0, 200)
        End If
        cmd.Parameters.AddWithValue("cnpj", txtBoxCnpj.Text)
        cmd.Parameters.AddWithValue("nome", txtBoxNome.Text)
        txtBoxCnpj.Text = ""
        txtBoxNome.Text = ""

        con.Open()
        Dim idInserido As Integer = cmd.ExecuteScalar()
        If checkBoxAssociados.Items.Count <> 0 Then
            ' percorre todos os itens marcados
            ' e verifica se um item especifico foi marcado
            For i As Integer = 0 To checkBoxAssociados.Items.Count - 1
                If checkBoxAssociados.Items(i).Selected Then
                    Dim idAssociado As String = checkBoxAssociados.Items(i).Value.ToString()
                    Dim cmd2 As SqlCommand = New SqlCommand()
                    cmd2.Connection = con
                    cmd2.CommandText = "Insert into associado_empresa (associado_id,empresa_id) values (@associado_id,@empresa_id)"
                    cmd2.Parameters.AddWithValue("associado_id", idAssociado)
                    cmd2.Parameters.AddWithValue("empresa_id", idInserido)
                    cmd2.ExecuteNonQuery()
                End If
            Next
        End If
        con.Close()
        ListViewEmpresas.DataBind()
    End Sub

    Protected Sub BancoDeDadosEmpresas_Deleting(sender As Object, e As SqlDataSourceCommandEventArgs) Handles BancoDeDadosEmpresas.Deleting
        Dim rootWebConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot")
        Dim connString As System.Configuration.ConnectionStringSettings
        connString = rootWebConfig.ConnectionStrings.ConnectionStrings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection()
        con.ConnectionString = connString.ToString()
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = con
        cmd.CommandText = "DELETE FROM associado_empresa WHERE empresa_id = @empresa_id"
        cmd.Parameters.AddWithValue("empresa_id", e.Command.Parameters(0).Value)
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

    Protected Sub ListViewEmpresas_ItemEditing(sender As Object, e As ListViewEditEventArgs) Handles ListViewEmpresas.ItemEditing
        Dim rootWebConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot")
        Dim connString As System.Configuration.ConnectionStringSettings
        connString = rootWebConfig.ConnectionStrings.ConnectionStrings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection()
        con.ConnectionString = connString.ToString()
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = con
        cmd.CommandText = "SELECT * FROM Associado a JOIN associado_empresa ae ON ae.associado_id = a.id AND ae.empresa_id = @empresa_id"

        cmd.Parameters.AddWithValue("empresa_id", DirectCast(ListViewEmpresas.Items(e.NewEditIndex).Controls(5), System.Web.UI.WebControls.Label).[Text])
        con.Open()
        Dim registro As SqlDataReader = cmd.ExecuteReader()
        checkBoxEditar.Visible = True
        checkBoxEditar.DataBind()
        For i As Integer = 0 To checkBoxEditar.Items.Count - 1
            checkBoxEditar.Items(i).Selected = False
        Next
        While registro.Read()
            Dim id As Integer = registro.GetInt32(0)
            For i As Integer = 0 To checkBoxEditar.Items.Count - 1
                If checkBoxEditar.Items(i).Value Like id Then
                    checkBoxEditar.Items(i).Selected = True
                End If
            Next
        End While

        con.Close()

    End Sub


    Protected Sub ListViewEmpresas_ItemCanceling(sender As Object, e As ListViewCancelEventArgs) Handles ListViewEmpresas.ItemCanceling
        checkBoxEditar.Visible = False
    End Sub
    Protected Sub ListViewEmpresas_ItemUpdated(sender As Object, e As ListViewUpdatedEventArgs) Handles ListViewEmpresas.ItemUpdated
        checkBoxEditar.Visible = False
    End Sub

    Protected Sub BancoDeDadosEmpresas_Updated(sender As Object, e As SqlDataSourceStatusEventArgs) Handles BancoDeDadosEmpresas.Updated
        If (e.Exception IsNot Nothing) Then
            labelMensagem.Text = e.Exception.Message
            e.ExceptionHandled = True
        End If
    End Sub
End Class
