<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Register Players.aspx.vb" Inherits="Register_Players" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style11
        {
            width: 70px;
        }
        .style12
        {
            width: 66px;
        }
        .style13
        {
            width: 80px;
        }
        .style14
        {
            width: 75px;
        }
        .style15
        {
            width: 76px;
        }
        .style16
        {
            width: 106px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="divFields" style="font-size:small">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblAddUpdateDelete" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Add New Player">
                    </asp:Label>
                </td>
                <td class="style13">
                </td>
                <td>
                </td>
                <td class="style14">
                </td>
                <td class="style15">
                </td>
                <td class="style12">
                </td>
                <td class="style11">
                </td>
                <td class="style16">
                </td>
                <td>
                    <asp:Label ID="lblChangePlayerName" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Change Player Name">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddLeague" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="League"> </asp:Label>
                    <asp:DropDownList ID="ddAddLeague" runat="server"
                        OnSelectedIndexChanged="ddAddLeague_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style13">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style14">
                </td>
                <td class="style15">
                </td>
                <td class="style12">
                </td>
                <td class="style11">
                </td>
                <td class="style16">
                </td>
               <td>
                    <asp:Label ID="lblChangeLeague" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="League"> </asp:Label>
                    <asp:DropDownList ID="ddChangeLeague" runat="server"
                        OnSelectedIndexChanged="ddChangeLeague_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td>
                    <asp:Label ID="lblAddTeam" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Team&nbsp;&nbsp;&nbsp;"> </asp:Label>
                    <asp:DropDownList ID="ddAddTeam" runat="server"
                        OnSelectedIndexChanged="ddAddTeam_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style13">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style14">
                    &nbsp;</td>
                <td class="style15">
                    &nbsp;</td>
                <td class="style12">
                    &nbsp;</td>
                <td class="style11">
                    &nbsp;</td>
               <td class="style16">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblChangeTeam" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Team&nbsp;&nbsp;&nbsp;"> </asp:Label>
                    <asp:DropDownList ID="ddChangeTeam" runat="server"
                        OnSelectedIndexChanged="ddChangeTeam_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
          <tr>
                <td> 
                    <asp:Label ID="lblAddPlayer" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Player&nbsp;&nbsp;"> </asp:Label>
                    <asp:TextBox ID="txtAddPlayer" runat="server" Width="126px"></asp:TextBox>
                </td>
                <td class="style13">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style14">
                    &nbsp;</td>
                <td class="style15">
                    &nbsp;</td>
                 <td class="style12">
                     &nbsp;</td>
                 <td class="style11">
                </td>
                <td class="style16">
                    &nbsp;</td>
              <td> 
                    <asp:Label ID="lblChangePlayer" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Player&nbsp;&nbsp;"> </asp:Label>
                     <asp:DropDownList ID="ddChangePlayer" runat="server"
                        OnSelectedIndexChanged="ddChangePlayer_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Phone&nbsp;&nbsp;"> </asp:Label>
                    <asp:TextBox ID="txtPhone" runat="server" Width="126px"></asp:TextBox>
                </td>
                 <td class="style13">
                </td>
                 <td>
                </td>
                 <td class="style14">
                </td>
                 <td class="style15">
                </td>
                 <td class="style12">
                </td>
                 <td class="style11">
                </td>
                <td class="style16">
                </td>
               <td> 
                    <asp:Label ID="lblNewName" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="New Name"> </asp:Label>
                    <asp:TextBox ID="txtChangeNewName" runat="server" Width="126px"></asp:TextBox>
                </td>
             </tr>
        
             <tr>
                <td>
                    <asp:Button ID="btnAddPlayer" runat="server" Text="Add Player" 
                        font-Bold="False" Font-Size="Large" Height="47px" Width="187px" />
                </td>
                 <td class="style13">
                </td>
                 <td>
                </td>
                 <td class="style14">
                </td>
                 <td class="style15">
                </td>
                 <td class="style12">
                </td>
                 <td class="style11">
                </td>
                <td class="style16">
                </td>
                <td>
                    <asp:Button ID="btnChangePlayer" runat="server" Text="Change Name" 
                        font-Bold="False" Font-Size="Large" Height="47px" Width="197px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                 <td class="style13">
                </td>
                 <td>
                </td>
                 <td class="style14">
                </td>
                 <td class="style15">
                </td>
                 <td class="style12">
                </td>
                 <td class="style11">
                </td>
                <td class="style16">
                </td>
              <td>
                    <asp:Label ID="lblChangeInstr" runat="server" BackColor="#1B1B1B" 
                        Font-Names="Arial" ForeColor="#E4BB18" 
                        Text="Name Changed." 
                        Font-Bold="True" Font-Size="Medium" Width="200px"></asp:Label>
              </td>
            </tr>
        </table>
        <br />
        <br />
            
        <asp:Label ID="lblRegisteredPlayers" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Registered Players"></asp:Label>
    </div>

                    <asp:GridView ID="gridDivision1" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceDivision1" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>

                    <asp:GridView ID="gridDivision2" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceDivision2" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>

                  <asp:GridView ID="gridDivision3" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceDivision3" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>

                    <asp:GridView ID="gridDivision4" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceDivision4" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>
  
   <asp:SqlDataSource ID="SqlDataSourceDivision1" runat="server"         
        SelectCommand="SELECT * FROM ladies_skit.vw_players WHERE league = 'DIVISION 1' ORDER BY league, team, player "
        DeleteCommand="DELETE FROM ladies_skit.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE ladies_skit.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone, Contact = @Contact WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>

   <asp:SqlDataSource ID="SqlDataSourceDivision2" runat="server"         
        SelectCommand="SELECT * FROM ladies_skit.vw_players WHERE league = 'DIVISION 2' ORDER BY league, team, player"
        DeleteCommand="DELETE FROM ladies_skit.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE ladies_skit.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone, Contact = @Contact WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="SqlDataSourceDivision3" runat="server"         
        SelectCommand="SELECT * FROM ladies_skit.vw_players WHERE league = 'DIVISION 3' ORDER BY league, team, player"
        DeleteCommand="DELETE FROM  ladies_skit.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE ladies_skit.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone, Contact = @Contact WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="SqlDataSourceDivision4" runat="server"         
        SelectCommand="SELECT * FROM ladies_skit.vw_players WHERE league = 'DIVISION 4' ORDER BY league, team, player"
        DeleteCommand="DELETE FROM  ladies_skit.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE ladies_skit.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone, Contact = @Contact WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
   </asp:SqlDataSource>



</asp:Content>

