<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">About Us</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <p>
        Put content here.
    </p>

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
</asp:Content>
