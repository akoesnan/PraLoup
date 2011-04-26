<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectSafari.Models.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>
        
    </p>

    <% using (Html.BeginForm()) { %>
    <% if (Model != null)
       { %>
    <br />
    <%=Model.first_name%>
    <br />
    <%=Model.last_name%>
    <br />
    <%=Model.middle_name%>
    <br />
    <%=Model.name%>
    <br />
    <%=Model.id%>
    <% }
       } %>
</asp:Content>
