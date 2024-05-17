<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReport.aspx.cs" Inherits="Reports.View.Views.PurchaseOrderReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <CR:CrystalReportViewer ID="PurchaseOrderReportViewer" runat="server" AutoDataBind="true"  HasCrystalLogo="False" 
     Height="50px" EnableParameterPrompt="false" EnableDatabaseLogonPrompt="false" ToolPanelWidth="200px" 
     Width="350px" ToolPanelView="None" />   
</asp:Content>
 