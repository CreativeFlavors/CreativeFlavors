﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="StockAdjustment.aspx.cs" Inherits="Reports.View.Views.StockAdjustment" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <CR:CrystalReportViewer ID="stockAdjustmentDisplay" runat="server" HasCrystalLogo="False"
        AutoDataBind="True" Height="50px" EnableParameterPrompt="false" EnableDatabaseLogonPrompt="false"
        ToolPanelWidth="200px" Width="350px" ToolPanelView="None" />
</asp:Content>