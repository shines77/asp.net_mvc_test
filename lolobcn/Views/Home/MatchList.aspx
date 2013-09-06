<%@ Page Title="MatchList" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">MatchList</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>MatchList</h2>

<table>
    <tr>
        <th></th>
        <th>
            Id
        </th>
        <th>
            ServerId
        </th>
        <th>
            ServerName
        </th>
        <th>
            ServerIp
        </th>
        <th>
            GameId
        </th>
    </tr>
    <% foreach (var item in Model) { %>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <%: item.Id %>
        </td>
        <td>
            <%: item.ServerId %>
        </td>
        <td>
            <%: item.ServerName %>
        </td>
        <td>
            <%: item.ServerIp %>
        </td>
        <td>
            <%: item.GameId %>
        </td>
    </tr>
    <% } %>
</table>

<!--
<form ID="Form1" action="#" method="get" runat="server">
    <div>
        <asp:GridView ID="PlaceHolder1" runat="server">
        </asp:GridView>
    </div>
</form>
-->

</asp:Content>

