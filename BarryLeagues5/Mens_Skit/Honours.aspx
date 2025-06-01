<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Honours.aspx.vb" Inherits="Honours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<table id="Honours" cellpadding="4">
    <tr valign="top">
        <td>
            <asp:Label 
                ID="lblSeason" 
                runat="server" 
                Font-Names="Arial"
                ForeColor="#E4BB18" 
                style="text-align:center" 
                Text="Season:"  
                BackColor="#1B1B1B" 
                BorderWidth="1px">
            </asp:Label>
            <asp:DropDownList 
                ID="ddSeasons" 
                runat="server" 
                AutoPostBack="True" 
                Font-Size="16px" 
                OnSelectedIndexChanged="ddSeasons_SelectedIndexChanged"
                ForeColor="#E4BB18"
                BackColor="Black">
            </asp:DropDownList>
            <br />        
            <br />
            <asp:RadioButtonList
                ID="rbAll" 
                runat="server" 
                AutoPostBack="true"  
                oncheckedchanged="rbAll_SelectedIndexChanged"
                foreColor="#E4BB18" 
                BackColor="Black" 
                RepeatDirection="Horizontal" RepeatLayout="Flow">
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr valign="top">
        <td>
            <div id="divLeague" runat="server">
                <asp:Label 
                    ID="lblLeague" 
                    runat="server" 
                    Font-Names="Arial" 
                    ForeColor="#E4BB18"
                    style="text-align:left" 
                    Text="League Honours"  
                    Width="200px" 
                    BackColor="#1B1B1B">
                </asp:Label>
                <br />
                <br />
                <asp:GridView 
                    ID="gridLeague" 
                    runat="server" 
                    Font-Size="14px" 
                    GridLines="None" 
                    AutoGenerateColumns="False" 
                    CellPadding="3" 
                    Width="400px" 
                    Font-Names="Arial" 
                    BackColor="#1B1B1B">
                   <Columns>
                        <asp:BoundField DataField="Season" HeaderText="Season" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="League" HeaderText="League" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Winners" HeaderText="Winners">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Runners-Up" HeaderText="Runners-Up">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Rolls Winner" HeaderText="Rolls Winners">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
              <br />
            </div>
            <div id="divAllLeagueHonours" runat="server">
                <asp:Label 
                    ID="Label2" 
                    runat="server" 
                    Font-Names="Arial" 
                    ForeColor="#E4BB18"
                    style="text-align:left" 
                    Text="League Honours"  
                    Width="200px" 
                    BackColor="#1B1B1B">
                </asp:Label>
                <br />
                <br />
                <asp:GridView 
                    ID="gridAllLeagueHonours" 
                    runat="server" 
                    Font-Size="14px" 
                    GridLines="None" 
                    AutoGenerateColumns="False" 
                    CellPadding="3" 
                    Width="400px" 
                    Font-Names="Arial" 
                    BackColor="#1B1B1B">
                   <Columns>
                        <asp:BoundField DataField="Season" HeaderText="Season" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Division 1" HeaderText="Division 1" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Division 2" HeaderText="Division 2" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Division 3" HeaderText="Division 3" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Division 4" HeaderText="Division 4" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
              <br />
            </div>
            <div id="divAllRollsHonours" runat="server">
                <asp:Label 
                    ID="Label3" 
                    runat="server" 
                    Font-Names="Arial" 
                    ForeColor="#E4BB18"
                    style="text-align:left" 
                    Text="Rolls Honours"  
                    Width="200px" 
                    BackColor="#1B1B1B">
                </asp:Label>
                <br />
                <br />
                <asp:GridView 
                    ID="gridAllRollsHonours" 
                    runat="server" 
                    Font-Size="14px" 
                    GridLines="None" 
                    AutoGenerateColumns="False" 
                    CellPadding="3" 
                    Width="400px" 
                    Font-Names="Arial" 
                    BackColor="#1B1B1B">
                   <Columns>
                        <asp:BoundField DataField="Season" HeaderText="Season" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Division 1" HeaderText="Division 1" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Division 2" HeaderText="Division 2" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Division 3" HeaderText="Division 3" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
              <br />
            </div>
            <div id="divCup" runat="server">
               <asp:Label 
                   ID="lblCup" 
                   runat="server" 
                   Font-Names="Arial" 
                   ForeColor="#E4BB18" 
                   style="text-align:left" 
                   Text="Cup Honours"  
                   Width="200px" 
                   BackColor="#1B1B1B">
                </asp:Label>
                <br />  
                <br />  
                <asp:GridView ID="gridCup" runat="server" Font-Size="14px" 
                    GridLines="None" Height="0px" AutoGenerateColumns="False" CellPadding="3" 
                      Width="400px" Font-Names="Arial" BackColor="#1B1B1B">
                    <Columns>
                        <asp:BoundField DataField="Season" HeaderText="Season" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cup" HeaderText="Competition" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                         <asp:BoundField DataField="Winners" HeaderText="Winners">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Runners-Up" HeaderText="Runners-Up">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <br />
            </div>
            <div id="divHighScores" runat="server">
                <asp:Label 
                    ID="lblHighScores" 
                    runat="server" 
                    Font-Names="Arial" 
                    ForeColor="#E4BB18" 
                    style="text-align:left" 
                    Text="Highest Scores"  
                    Width="200px" 
                    BackColor="#1B1B1B">
                </asp:Label>
                <br />
                <br />  
                <asp:GridView ID="gridHighScores" runat="server" Font-Size="14px" 
                    GridLines="None" Height="0px" AutoGenerateColumns="False" CellPadding="3" 
                      Width="400px" Font-Names="Arial" BackColor="#1B1B1B">
                    <Columns>
                        <asp:BoundField DataField="Season" HeaderText="Season" >
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="High Scores Lit" HeaderText="H/A">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False"  ForeColor="#FF9933" />
                            <ItemStyle ForeColor="LightGreen" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="High Scores Player" HeaderText="Player">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="White" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="High Scores Team" HeaderText="Team">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False"  ForeColor="#FF9933" />
                            <ItemStyle ForeColor="Cyan" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="High Scores Score" HeaderText="Score">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                            <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </td>
        <td>
            <div id="divSummary" runat="server">
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" ForeColor="#E4BB18"  AutoPostBack="true"
                    style="text-align:left" Text="Summary of League & Cup Winners." 
                    BackColor="#1B1B1B">
                </asp:Label>
                <br />
                <asp:Label ID="Label4" runat="server" Font-Names="Arial" ForeColor="#E4BB18"  AutoPostBack="true"
                    style="text-align:left" Text="Click on a team to view team honours." 
                    BackColor="#1B1B1B">
                </asp:Label>
                <br />
               <asp:GridView ID="gridSummary" runat="server" Font-Size="14px" 
                    GridLines="None" Height="0px" AutoGenerateColumns="False" CellPadding="3" 
                      Width="400px" Font-Names="Arial" BackColor="#1B1B1B" cssclass="gv">
                   <Columns>
                    <asp:BoundField DataField="Team" HeaderText="Team" >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="Cyan" Wrap="False"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="League Wins" HeaderText="League Wins" >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Allform Wins" HeaderText="Allform Wins" >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Holme Towers Wins" HeaderText="Holme Towers Wins" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Champions Wins" HeaderText="Champions Wins" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Brains Cup Wins" HeaderText="Brains Cup Wins" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Total" HeaderText="Total">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
              </asp:GridView>
              <br />
            </div>
            <div id="divTeamHonours" runat="server">
                <asp:Label ID="lblTeamHonours" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                    style="text-align:left" Text="???? Honours" 
                    BackColor="#1B1B1B" width="349px">
                </asp:Label>
                <asp:Button ID="btnClose" runat="server" Text="Back to Summary" BackColor="Black" autopostback="False"
                    ForeColor="#E4BB18" font-size="16px" borderstyle="Solid" bordercolor="#E4BB18"/>
                <br />
                <br />
               <asp:GridView ID="gridTeamHonours" runat="server" Font-Size="14px" 
                    GridLines="None" Height="0px" AutoGenerateColumns="False" CellPadding="3" 
                      Width="400px" Font-Names="Arial" BackColor="#1B1B1B" cssclass="gv">
                   <Columns>
                    <asp:BoundField DataField="Season" HeaderText="Season" >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"/>
                        <ItemStyle ForeColor="Cyan" Wrap="False"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="Div 1 Winner" HeaderText="Div 1 Winner"  >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Div 2 Winner" HeaderText="Div 2 Winner" >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Div 3 Winner" HeaderText="Div 3 Winner" >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Div 4 Winner" HeaderText="Div 4 Winner" >
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Allform Winner" HeaderText="Allform Winner" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Holme Towers Winner" HeaderText="Holme Towers Winner" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Champions Winner" HeaderText="Champions Winner" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Brains Cup Winner" HeaderText="Brains Cup Winner" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Season Total" HeaderText="Season Total" >
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" ForeColor="#FF9933"  />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Center" Wrap="false" />
                    </asp:BoundField>
                </Columns>
              </asp:GridView>
              <br />
            </div>
        </td>
    </tr>
    </table>

</asp:Content>
