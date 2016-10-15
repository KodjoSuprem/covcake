<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BadPassword</title>
</head>
<body>
    <div>
        Bad Password : <%= ViewData["pass"] %>
    </div>
    
    <% for (int i = 0; i < 10; i++) { %>
               <!-- Bad Password -->
    <%  } %>

</body>
</html>
