<%@ Page Title="Associados" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="associados.aspx.vb" Inherits="associados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Inserir novo Associado</h3>
    <div class="cadastro">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Nome" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtBoxNome" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label4" runat="server" Text="Data de Nascimento" Font-Bold="True"></asp:Label>
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        </div>
        
        <div>
            <asp:Label ID="Label2" runat="server" Text="Cpf" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtBoxCpf" runat="server"></asp:TextBox>
        </div>
        <div class="associados-list">
            <asp:Label ID="Label3" runat="server" Text="Empresas" Font-Bold="True"></asp:Label>
            <asp:CheckBoxList ID="checkBoxEmpresas" runat="server" DataSourceID="BancoDeDadosEmpresas" DataTextField="nome" DataValueField="id">
            </asp:CheckBoxList>
        </div>
        
        <asp:Button ID="Button1" runat="server" Text="Inserir" Width="75px" />
    </div>
    <h3>Lista de Associados </h3>
    
    <asp:ListView ID="ListViewAssociados" runat="server" DataKeyNames="id" DataSourceID="BancoDeDadosAssociados" EnableModelValidation="True">
        <AlternatingItemTemplate>
            <tr style="background-color:#FFF8DC;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:Label ID="cpfLabel" runat="server" Text='<%# Eval("cpf") %>' />
                </td>
                <td>
                    <asp:Label ID="data_de_nascimentoLabel" runat="server" Text='<%# Eval("data_de_nascimento") %>' />
                </td>
                <td>
                    <asp:Label ID="nomeLabel" runat="server" Text='<%# Eval("nome") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color:#008A8C;color: #FFFFFF;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:TextBox ID="cpfTextBox" runat="server" Text='<%# Bind("cpf") %>' />
                </td>
                <td>
                    <asp:TextBox ID="data_de_nascimentoTextBox" runat="server" Text='<%# Bind("data_de_nascimento") %>' />
                </td>
                <td>
                    <asp:TextBox ID="nomeTextBox" runat="server" Text='<%# Bind("nome") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="cpfTextBox" runat="server" Text='<%# Bind("cpf") %>' />
                </td>
                <td>
                    <asp:TextBox ID="data_de_nascimentoTextBox" runat="server" Text='<%# Bind("data_de_nascimento") %>' />
                </td>
                <td>
                    <asp:TextBox ID="nomeTextBox" runat="server" Text='<%# Bind("nome") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color:#DCDCDC;color: #000000;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:Label ID="cpfLabel" runat="server" Text='<%# Eval("cpf") %>' />
                </td>
                <td>
                    <asp:Label ID="data_de_nascimentoLabel" runat="server" Text='<%# Eval("data_de_nascimento") %>' />
                </td>
                <td>
                    <asp:Label ID="nomeLabel" runat="server" Text='<%# Eval("nome") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                <th runat="server"></th>
                                <th runat="server">id</th>
                                <th runat="server">cpf</th>
                                <th runat="server">data_de_nascimento</th>
                                <th runat="server">nome</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:Label ID="cpfLabel" runat="server" Text='<%# Eval("cpf") %>' />
                </td>
                <td>
                    <asp:Label ID="data_de_nascimentoLabel" runat="server" Text='<%# Eval("data_de_nascimento") %>' />
                </td>
                <td>
                    <asp:Label ID="nomeLabel" runat="server" Text='<%# Eval("nome") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:CheckBoxList ID="checkBoxEditar" runat="server" DataSourceID="BancoDeDadosEmpresas" DataTextField="nome" DataValueField="id">
    </asp:CheckBoxList>
    <asp:SqlDataSource ID="BancoDeDadosAssociados" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Associado] WHERE [id] = @id" InsertCommand="INSERT INTO [Associado] ([cpf], [data_de_nascimento], [nome]) VALUES (@cpf, @data_de_nascimento, @nome)" SelectCommand="SELECT [id], [cpf], [data_de_nascimento], [nome] FROM [Associado]" UpdateCommand="UPDATE [Associado] SET [cpf] = @cpf, [data_de_nascimento] = @data_de_nascimento, [nome] = @nome WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="cpf" Type="String" />
            <asp:Parameter Name="data_de_nascimento" Type="DateTime" />
            <asp:Parameter Name="nome" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="cpf" Type="String" />
            <asp:Parameter Name="data_de_nascimento" Type="DateTime" />
            <asp:Parameter Name="nome" Type="String" />
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
    
    <asp:SqlDataSource ID="BancoDeDadosEmpresas" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Empresa]"></asp:SqlDataSource>
    
    
    <asp:Label ID="labelMensagem" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"></asp:Label>
</asp:Content>

