<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DictionaryInput.ascx.cs" Inherits="CMSApp.CMSFormControls.DictionaryInput" ViewStateMode="Enabled" %>

<asp:TextBox runat="server" ID="txbKey" />
<asp:TextBox runat="server" ID="txbValue" />
<asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" />
<cms:CMSRepeater runat="server" ID="rptItems" OnItemDataBound="rptItems_ItemDataBound">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:Button runat="server" ID="btnDel" CssClass="btn btn-warning" Text="Remove" CommandArgument='<%# DataBinder.Eval((System.Collections.Generic.KeyValuePair<string, string>)Container.DataItem,"Key") %>' OnClick="btnDel_Click" />
            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" CommandArgument='<%# DataBinder.Eval((System.Collections.Generic.KeyValuePair<string, string>)Container.DataItem,"Key") %>' OnClick="btnSave_Click" />
            <h4 runat="server" id="txtKey"></h4>
            <p runat="server" id="txtValue"></p>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</cms:CMSRepeater>