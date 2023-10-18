<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.ascx.cs" Inherits="navtezcms.adminw.Admin.UC.AdminMenu" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<% DataTable dtMenuMaster = new DataTable(); %>

<% if (Session["dtMenu"] != null)
    {
        dtMenuMaster = (DataTable)Session["dtMenu"];
    }
%>

<ul class="sidebar-menu" data-widget="tree">
    <li class="header"></li>
    <%
        try
        {
            DataRow[] dr1 = dtMenuMaster.Select("MenuLevel=1");
            for (int i = 0; i < dr1.Length; i++)
            {
    %>

    <%
        DataRow[] dr2 = dtMenuMaster.Select("MenuLevel=2 And ParentID='" + dr1[i]["id"].ToString() + "'");
    %>

    <%if (dr2.Length > 0)
        {%>
    <li class="treeview" style="height: auto;">
        <%}
            else
            { %>
    <li>
        <%} %>

        <a href="<%Response.Write(dr1[i]["MenuLink"]); %>">
            <i class="<%Response.Write(dr1[i]["Icon"].ToString()); %>"></i><span><%Response.Write(dr1[i]["MenuName"].ToString()); %></span>
            <%if (dr2.Length > 0)
                {%>
            <span class="pull-right-container">
                <i class="fa fa-angle-right pull-right"></i>
            </span>
            <%}%>
        </a>

        <ul class="treeview-menu" style="display: none;">
            <%
                for (int j = 0; j < dr2.Length; j++)
                {
            %>
            <%
                DataRow[] dr3 = dtMenuMaster.Select("MenuLevel=3 And ParentID='" + dr2[j]["ID"].ToString() + "'");
            %>


            <%if (dr3.Length > 0)
                {%>
            <li class="treeview" style="height: auto;">
                <%}
                    else
                    { %>
            <li>
                <%} %>

                <a href="<%Response.Write(dr2[j]["MenuLink"]); %>">
                    <i class="<%Response.Write(dr2[j]["Icon"].ToString()); %>"></i><span><%Response.Write(dr2[j]["MenuName"].ToString()); %></span>
                    <%if (dr3.Length > 0)
                        {%>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-right pull-right"></i>
                    </span>
                    <%}%>
                </a>

                <ul class="treeview-menu" style="display: none;">
                    <% 
                        for (int k = 0; k < dr3.Length; k++)
                        {
                    %>
                    <li><a href="<%Response.Write(dr3[k]["MenuLink"]); %>">
                        <i class="<%Response.Write(dr3[k]["Icon"].ToString()); %>"></i><span><%Response.Write(dr3[k]["MenuName"].ToString()); %></span></a>
                    </li>
                    <%
                        }
                    %>
                </ul>

            </li>
            <%}
            %>
        </ul>
    </li>
    <%

        }%>
    <%}
        catch (Exception ex)
        {
        }
    %>
</ul>