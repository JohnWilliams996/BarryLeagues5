<%@ Page Title="" Language="VB" Debug="true" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Team Fixtures.aspx.vb" Inherits="Team_Fixtures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    a:link {text-decoration: none }
    a:active {text-decoration: none }
    a:visited {text-decoration: none }
    a:hover {text-decoration: underline }

    /** GRIDVIEW STYLES **/

    .gv tr.row:hover 
    {
    	background-color:Black;
    	text-decoration:underline
    }
    .gv td cell:hover 
    {
    	background-color:Black;
    	text-decoration:underline
    }
    
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>


         <asp:TextBox ID="txtInfo" runat="server" Height="45px" Width="1042px" 
             BackColor="Red" Font-Names="Arial" Font-Size="Large" ForeColor="White" 
             Rows="3" TextMode="MultiLine"></asp:TextBox>
        
    <br />
    <br />

    <div id="divCupList">
      <table>
        <tr>
          <td rowspan="2">
            <asp:Label ID="Label4" Text="Click on a Team/Competition to view the Fixtures" 
                runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:center"  Height="40px" Width="228px"
                BackColor="#1B1B1B">
            </asp:Label>     
          </td>
          <td>
              <asp:Label ID="lblTeam" runat="server" Font-Names="Arial" ForeColor="Yellow"  Text="TEAM"
                     Width="240px" Font-Size="18px" Height="22px" style="text-align:left" BackColor="#1B1B1B">
              </asp:Label>
          </td>
          <td>
               <asp:Label ID="lblStatus" runat="server" Text="STATUS" Font-Names="Arial" 
                    ForeColor="Red" Font-Size="18px" Width="281px" style="text-align:center" 
                    BackColor="#1B1B1B">
                </asp:Label>
               <asp:Label ID="lblNeutral" runat="server" Text="(N) = NEUTRAL FIXTURE" Font-Names="Arial" 
                    ForeColor="LightGreen" Font-Size="18px" Width="281px" style="text-align:center" 
                    BackColor="#1B1B1B">
                </asp:Label>
          </td>
          <td>
                <asp:Label ID="lblLeague" runat="server" Text="LEAGUE" Font-Names="Arial" 
                    ForeColor="Yellow" Font-Size="18px"  Width="260px" style="text-align:left" BackColor="#1B1B1B" >
                </asp:Label>
          </td>
        </tr>

        <tr>
          <td>
                <asp:HyperLink ID="hlTeamStats" Text="Team Stats" runat="server" height="16px"
                BackColor="#99CCFF" BorderColor="Silver" BorderStyle="Solid" Font-Bold="True" ForeColor="Black" ></asp:HyperLink>
          </td>
          <td>
                <asp:button ID="btnPDF" runat="server" Text="View/Print Fixtures (PDF)"
                    BackColor="Red"  ForeColor="white" Font-Size="18px" Visible ="true" Width="219px" ></asp:button>

                <asp:Label ID="lblFiller1" runat="server" Font-Names="Arial" 
                     Width="10px" Font-Size="18px" Height="22px" style="text-align:left" BackColor="#333333"></asp:Label>

                <asp:button ID="btnCard" runat="server" Text="Download/Print Result Cards (PDF)"
                    BackColor="Red"  ForeColor="white" Font-Size="18px" Visible ="true" Width="303px" ></asp:button>

           </td>
         <td>
                <asp:HyperLink ID="hlLeagueStats" Text="League Stats" runat="server" height="16px"
                BackColor="#99CCFF" BorderColor="Silver" BorderStyle="Solid" Font-Bold="True" ForeColor="Black" ></asp:HyperLink>
          </td>
         </tr>
          

         <tr>
           <td valign="top">
            <asp:GridView ID="gridOptions" runat="server" CssClass="gv"
                    style="margin-top: 0px" Height="136px"   
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" BackColor="#1B1B1B" 
                CellPadding="4" ShowHeader="False" ForeColor="#333333" 
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Long Name" HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="Cyan" HorizontalAlign="Left" wrap="false"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Home Night" HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left" wrap="false"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Venue" HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left" wrap="false"/>
                    </asp:BoundField>   
                </Columns>
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

            </asp:GridView>
           </td>

           <td colspan="2" valign = "top">
            <asp:GridView ID="gridFixtures1" runat="server" GridLines="None" CssClass="gv" CellSpacing="2"
             AutoGenerateColumns="False" Font-Names="Arial" Font-Size="14px" CellPadding="2" BackColor="#1B1B1B">
            <Columns>
                <asp:BoundField DataField="Week Number" HeaderText="Wk" >
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="Yellow" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="Fixture Calendar" HeaderText="Fixture Date">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="League Cup" HeaderText="League/Cup">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="Yellow" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="Home Team Name" HeaderText="Opponents">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False"/>
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                           
                <asp:BoundField DataField="Venue" HeaderText="Venue">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False"/>
                    <ItemStyle ForeColor="LightGreen" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>     
                                        
                <asp:BoundField DataField="Home Result"  headerText="Result">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                    <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False"/>
                </asp:BoundField>

                <asp:BoundField DataField="Away Result" HeaderText="" >
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False"/>
                </asp:BoundField>

                <asp:BoundField DataField="Home Points Deducted" SortExpression="Ded" 
                    HeaderText="Ded">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="10px" />
                    <ItemStyle ForeColor="red" HorizontalAlign="Right"/>
                </asp:BoundField>

                <asp:BoundField DataField="Away Points Deducted" SortExpression="" 
                    HeaderText="">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="10px" />
                    <ItemStyle ForeColor="red" HorizontalAlign="Right"/>
                </asp:BoundField>
 
                <asp:BoundField DataField="Total Points" SortExpression="" 
                    HeaderText="Tot Pts">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="20px" />
                    <ItemStyle ForeColor="cyan" HorizontalAlign="Right" Wrap="false"/>
                </asp:BoundField>
 
                <asp:BoundField DataField="League Position" SortExpression="" 
                    HeaderText="Pos">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="20px" />
                    <ItemStyle ForeColor="#FFC000" HorizontalAlign="Right" Font-Bold="true"/>
                </asp:BoundField>

                <asp:BoundField DataField="Fixture ID">
                <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False" 
                    Font-Size="11px" />
                </asp:BoundField>

                <asp:BoundField DataField="Fixture ID2" visible="False">
                <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Status" visible="False">
                <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False"  />
                </asp:BoundField>
                </Columns>
              </asp:GridView>

<%--           <asp:Label ID="lblFiller2"  runat="server" width="263px"></asp:Label>--%>

            <br />

           <br />
           <br />


            </td>


           <td valign="top">
            <asp:GridView ID="gridPlayers" runat="server" GridLines="None" CssClass="gv"
             AutoGenerateColumns="False" Font-Names="Arial" Font-Size="14px" CellPadding="2" BackColor="#1B1B1B" >
            <Columns>
                <asp:BoundField DataField="Players" HeaderText="">
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                <asp:BoundField DataField="In/Out" HeaderText="" Visible="false">
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                           
             </Columns>
             </asp:GridView>
            <asp:GridView ID="grid6aside" runat="server" GridLines="None" CssClass="gv"
             AutoGenerateColumns="False" Font-Names="Arial" Font-Size="14px" CellPadding="3" BackColor="#1B1B1B" >
            <Columns>
                <asp:BoundField DataField="player1" HeaderText="">
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                <asp:BoundField DataField="player2" HeaderText="">
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                <asp:BoundField DataField="player3" HeaderText="">
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                           
             </Columns>
             </asp:GridView>
            </td>
           </tr>

           <tr>
                <td></td>
                <td>
                </td>
           </tr>
        </table>
    </div>
    


</asp:Content>


