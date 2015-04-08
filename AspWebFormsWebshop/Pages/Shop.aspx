<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="AspWebFormsWebshop.Pages.Shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <asp:Label ID="lblResult" runat="server"></asp:Label>
    <br />
    <asp:Button ID="btnOk" runat="server" Text="Ok" Visible="False" Width="100px" 
        onclick="btnOk_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="False" 
        Width="100px" onclick="btnCancel_Click" />
    <br />
    <asp:Button ID="btnOrder" runat="server" Text="Order!" 
        onclick="btnOrder_Click" />
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <asp:Panel ID="pnlProducts" runat="server">
    </asp:Panel>
    <br />



</asp:Content>
