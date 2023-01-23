Imports System.Data.SqlClient

Partial Class associados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        checkBoxEditar.Visible = False
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rootWebConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot")
        Dim connString As System.Configuration.ConnectionStringSettings
        connString = rootWebConfig.ConnectionStrings.ConnectionStrings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection()
        con.ConnectionString = connString.ToString()
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = con
        cmd.CommandText = "Insert into Associado (nome,cpf,data_de_nascimento) values (@nome,@cpf,@data_de_nascimento); SELECT SCOPE_IDENTITY()"
        Dim cpf As String = txtBoxCpf.Text
        If cpf.Length > 11 Then
            cpf = cpf.Substring(0, 11)
        End If
        Dim nome As String = txtBoxNome.Text
        If nome.Length > 200 Then
            nome = cpf.Substring(0, 200)
        End If
        cmd.Parameters.AddWithValue("cpf", txtBoxCpf.Text)
        cmd.Parameters.AddWithValue("nome", txtBoxNome.Text)
        cmd.Parameters.AddWithValue("data_de_nascimento", Calendar1.SelectedDate)
        txtBoxCpf.Text = ""
        txtBoxNome.Text = ""


        con.Open()
        Dim idInserido As Integer = cmd.ExecuteScalar()
        If checkBoxEmpresas.Items.Count <> 0 Then
            ' percorre todos os itens marcados
            ' e verifica se um item especifico foi marcado
            For i As Integer = 0 To checkBoxEmpresas.Items.Count - 1
                If checkBoxEmpresas.Items(i).Selected Then
                    Dim idEmpresa As String = checkBoxEmpresas.Items(i).Value.ToString()
                    Dim cmd2 As SqlCommand = New SqlCommand()
                    cmd2.Connection = con
                    cmd2.CommandText = "Insert into associado_empresa (associado_id,empresa_id) values (@associado_id,@empresa_id)"
                    cmd2.Parameters.AddWithValue("associado_id", idInserido)
                    cmd2.Parameters.AddWithValue("empresa_id", idEmpresa)
                    cmd2.ExecuteNonQuery()
                    checkBoxEmpresas.Items(i).Selected = False
                End If
            Next
        End If
        con.Close()
        ListViewAssociados.DataBind()
    End Sub

    Protected Sub BancoDeDadosAssociados_Inserted(sender As Object, e As SqlDataSourceStatusEventArgs) Handles BancoDeDadosAssociados.Inserted
        If (e.Exception IsNot Nothing) Then
            labelMensagem.Text = e.Exception.Message
            e.ExceptionHandled = True
        End If
    End Sub
    Protected Sub BancoDeDadosAssociados_Updated(sender As Object, e As SqlDataSourceStatusEventArgs) Handles BancoDeDadosAssociados.Updated
        If (e.Exception IsNot Nothing) Then
            labelMensagem.Text = e.Exception.Message
            e.ExceptionHandled = True
        End If
    End Sub
    Protected Sub BancoDeDadosAssociados_Deleting(sender As Object, e As SqlDataSourceCommandEventArgs) Handles BancoDeDadosAssociados.Deleting
        Dim rootWebConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot")
        Dim connString As System.Configuration.ConnectionStringSettings
        connString = rootWebConfig.ConnectionStrings.ConnectionStrings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection()
        con.ConnectionString = connString.ToString()
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = con
        cmd.CommandText = "DELETE FROM associado_empresa WHERE associado_id = @associado_id"
        cmd.Parameters.AddWithValue("associado_id", e.Command.Parameters(0).Value)
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
    Protected Sub ListViewAssociados_ItemEditing(sender As Object, e As ListViewEditEventArgs) Handles ListViewAssociados.ItemEditing
        Dim rootWebConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot")
        Dim connString As System.Configuration.ConnectionStringSettings
        connString = rootWebConfig.ConnectionStrings.ConnectionStrings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection()
        con.ConnectionString = connString.ToString()
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = con
        cmd.CommandText = "SELECT * FROM Empresa e JOIN associado_empresa ae ON ae.empresa_id = e.id AND ae.associado_id = @associado_id"

        cmd.Parameters.AddWithValue("associado_id", DirectCast(ListViewAssociados.Items(e.NewEditIndex).Controls(5), System.Web.UI.WebControls.Label).[Text])
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
    Protected Sub ListViewAssociados_ItemCanceling(sender As Object, e As ListViewCancelEventArgs) Handles ListViewAssociados.ItemCanceling
        checkBoxEditar.Visible = False
    End Sub
    Protected Sub ListViewAssociados_ItemUpdated(sender As Object, e As ListViewUpdatedEventArgs) Handles ListViewAssociados.ItemUpdated
        checkBoxEditar.Visible = False
    End Sub
End Class
