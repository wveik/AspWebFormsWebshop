<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Coffee.aspx.cs" Inherits="AspWebFormsWebshop.Pages.Coffee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Выберите тип: <asp:DropDownList ID="_DropDownListType" runat="server" AutoPostBack="True" DataSourceID="sds_type" DataTextField="type" DataValueField="type" OnSelectedIndexChanged="_DropDownListType_SelectedIndexChanged"></asp:DropDownList>
    <asp:SqlDataSource ID="sds_type" runat="server" ConnectionString="<%$ ConnectionStrings:CoffeeConnection %>" SelectCommand="SELECT DISTINCT [type] FROM [coffee] ORDER BY [type]"></asp:SqlDataSource>
    <asp:Label ID="lblOutput" runat="server" Text="Label"></asp:Label>
</asp:Content>
