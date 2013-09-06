<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
            Welcome <strong><%: Page.User.Identity.Name %></strong>!
            [ <%: Html.ActionLink("Log Off", "Logoff", "Account") %> ]
<%
    }
    else {
%> 
            [ <%: Html.ActionLink("Log On", "Logon", "Account") %> ]
<%
    }
%>
