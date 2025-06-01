<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Website Activity.aspx.vb" Inherits="Admin_Website_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox ID="txtSQL" BackColor="Black" ForeColor="White" Font-Size="14px" 
        runat="server" Width="1000px" Font-Names="Courier New" Height="30px" 
        Rows="4" style="margin-top: 0px"></asp:TextBox>
    <asp:button ID="btnRefresh" runat="server" Text="Refresh" BackColor="Black" 
        ForeColor="White" Font-Size="16px" Height="30px" style="margin-top: 5px"/>

    <asp:GridView ID="gridActivity" runat="server" AutoGenerateColumns="False" 
        Font-Names="Arial" Font-Size="12px" GridLines="Horizontal" DataKeyNames="ID" 
        BorderStyle="None" BorderWidth="1px" AllowPaging="true" PageSize="50" PagerStyle-ForeColor="Tan"
        CellPadding="3"  DataSourceID="SqlDataSource1" 
        BackColor="Black">
        <Columns>

                <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id">
                <HeaderStyle ForeColor="Tan" />
                <ItemStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="web_page" HeaderText="WebPage" SortExpression="web_page">
                <HeaderStyle ForeColor="Tan" />
                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="ip_address" HeaderText="IPAddress" SortExpression="ip_address">
                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="visit_date" HeaderText="VisitDate" SortExpression="visit_date">
                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="is_me" HeaderText="Me?" SortExpression="is_me">
                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                <ItemStyle ForeColor="White" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="admin_user" HeaderText="AdminUser?" SortExpression="admin_user">
                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                <ItemStyle ForeColor="White" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="location" HeaderText="Location" SortExpression="location">
                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                <ItemStyle ForeColor="White" HorizontalAlign="Center" />
                </asp:BoundField>

        </Columns>
        <HeaderStyle Font-Size="12px" />
    </asp:GridView>



   <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        SelectCommand="SELECT TOP 1000 a.id,a.web_page,a.ip_address,a.visit_date,a.is_me,a.admin_user,b.Location from ladies_skit.page_visits a LEFT OUTER JOIN ladies_skit.my_ip_addresses b ON b.ip_address = a.ip_address  WHERE a.web_page <> 'GoogleBot Crawler' ORDER BY a.id DESC" >
   </asp:SqlDataSource>


    <br />
    <br />
  <asp:Label ID="Label1" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Add My IP Address"> </asp:Label>
    <br />
    <br />

        <asp:GridView ID="gridIP" runat="server" AutoGenerateColumns="False" 
        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
        BorderStyle="None" BorderWidth="1px" 
        CellPadding="3"  DataSourceID="SqlDataSourcemy_ip_addresses" 
        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
        <PagerStyle  ForeColor="Red" />

        <Columns>
                <asp:CommandField ShowEditButton="True" ControlStyle-ForeColor="Tan" />
                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                <asp:BoundField DataField="ip_address" HeaderText="IPAddress" SortExpression="ip_address">
                <HeaderStyle ForeColor="Tan" />
                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="location" HeaderText="Location" SortExpression="location">
                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID">
                <HeaderStyle ForeColor="Tan" />
                <ItemStyle ForeColor="White" />
                </asp:BoundField>

        </Columns>
        <HeaderStyle Font-Size="12px" />
    </asp:GridView>

    <br />
    <br />


  <asp:Label ID="IPAddress" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="IP Address"> </asp:Label>
  <asp:TextBox ID="txtIPAddress" runat="server" Width="100px"></asp:TextBox>  
  <asp:Label ID="Location" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Location"> </asp:Label>
  <asp:TextBox ID="txtLocation" runat="server" Width="100px"></asp:TextBox>  
  <asp:Button ID="btnAdd" runat="server" Text="Add Location" />
    <br />
  <asp:Label ID="lblIPExists" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="IP Address Already Exists"> </asp:Label>
  <asp:Label ID="lblNoIP" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="No IP Address Entered"> </asp:Label>

   <asp:SqlDataSource ID="SqlDataSourcemy_ip_addresses" runat="server"         
        SelectCommand="SELECT id,ip_address,location FROM ladies_skit.my_ip_addresses ORDER BY ip_address"
        DeleteCommand="DELETE FROM ladies_skit.my_ip_addresses WHERE id = @ID" 
        UpdateCommand="UPDATE ladies_skit.my_ip_addresses SET ip_address = @ip_address, location = @Location WHERE id = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="ip_address" />
            <asp:Parameter Name="location" />
            <asp:Parameter Name="id" />
        </UpdateParameters>
     </asp:SqlDataSource>

</asp:Content>

