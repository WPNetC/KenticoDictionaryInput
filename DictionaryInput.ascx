<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DictionaryInput.ascx.cs" Inherits="CMSApp.CMSFormControls.DictionaryInput" ViewStateMode="Enabled" %>

<asp:TextBox runat="server" ID="txbKey" />
<asp:TextBox runat="server" ID="txbValue" />
<asp:Button runat="server" ID="btnAdd" OnClick="btnAdd_Click" />
<cms:CMSRepeater runat="server" ID="rptItems" OnItemDataBound="rptItems_ItemDataBound">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <h3 runat="server" id="txtKey"></h3>
            <p runat="server" id="txtValue"></p>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</cms:CMSRepeater>