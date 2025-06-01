<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="High Scores.aspx.vb" Inherits="Admin_High_Scores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 200px;
        }
        .style9
        {
            width: 139px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div id="divFields">
        <table class="style8" cellspacing="10" id="tblFields">
            <tr>
                <td>
                </td>
                <td class="style9">
                    <asp:Label ID="lblAdd" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Add New High Score"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPrint" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Print"></asp:Label>
                </td>
                <td class="style9">
                    <asp:DropDownList ID="ddPrint" runat="server">
                    </asp:DropDownList>
                </td>
                <td rowspan="8">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="6"  DataSourceID="SqlDataSource1" 
                        BackColor="Black">
                        <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:CheckBoxField DataField="_Print" HeaderText="Print?" SortExpression="Print">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:CheckBoxField>

                                <asp:BoundField DataField="league" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="home_away" HeaderText="H/A" SortExpression="Home_Away">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="Yellow" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="score" HeaderText="Score" SortExpression="Score">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="date_added" HeaderText="Date Added" InsertVisible="False" ReadOnly="True" SortExpression="Date_Added">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="Green" Wrap="false" />
                                </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="lblLeague" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="League"> </asp:Label>
                </td>
                <td class="style9">
                    <asp:DropDownList ID="ddLeague" runat="server"
                    OnSelectedIndexChanged="ddLeague_SelectedIndexChanged"
                    AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHomeAway" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="H/A"> </asp:Label>
                </td>
                <td class="style9">
                    <asp:DropDownList ID="ddHomeAway" runat="server">
                     </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTeam" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Team"> </asp:Label>
                </td>
                <td class="style9">
                    <asp:DropDownList ID="ddTeam" runat="server"
                    OnSelectedIndexChanged="ddTeam_SelectedIndexChanged"
                    AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPlayer" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Player"> </asp:Label>
                </td>
               <td class="style9">
                    <asp:DropDownList ID="ddPlayer" runat="server">
                     </asp:DropDownList>
                </td>
             </tr>

            <tr>
                <td>
                    <asp:Label ID="lblScore" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Score"> </asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtScore" runat="server" Width="34px"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="style9">
                    <asp:Button ID="btnAddHS" runat="server" Text="Add High Score" />
                </td>
            </tr>
        </table>
</div>




    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        
        SelectCommand="SELECT _print,league,home_away,player,team,score,ID,date_added FROM clubs.vw_high_scores  ORDER BY league, home_away DESC" 
        DeleteCommand="DELETE FROM  clubs.vw_high_scores WHERE ID = @ID" 
        UpdateCommand="UPDATE clubs.vw_high_scores SET league = @League, home_away = @Home_Away, player = @Player, team = @Team, score = @Score, _print = @_Print, date_added = GETUTCDATE() WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="_Print" />
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Home_Away" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Score" />
            <asp:Parameter Name="ID" />
            <asp:Parameter Name="Date_Added" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>

