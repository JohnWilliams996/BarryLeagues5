<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="High Scores.aspx.vb" Inherits="Admin_High_Scores" %>

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

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="HomeAway" HeaderText="H/A" SortExpression="HomeAway">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="Yellow" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
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
        
        SelectCommand="SELECT * FROM mens_skit.vw_high_scores  ORDER BY league, home_away DESC" 
        DeleteCommand="DELETE FROM mens_skit.vw_high_scores WHERE ID = @ID" 
        InsertCommand="INSERT INTO mens_skit.vw_high_scores VALUES (@League,@HomeAway,@Player,@Team,@Score,@Print)" 
        UpdateCommand="UPDATE mens_skit.vw_high_scores SET League = @League, HomeAway = @HomeAway, Player = @Player, Team = @Team, Score = @Score, _Print = @Print WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="HomeAway" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Score" />
            <asp:Parameter Name="_Print" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="HomeAway" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Score" />
            <asp:Parameter Name="_Print" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>

