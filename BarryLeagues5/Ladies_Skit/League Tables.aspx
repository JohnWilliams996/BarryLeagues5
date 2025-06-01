<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="League Tables.aspx.vb" Inherits="League_Tables" %>

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
    
   <table id="League" cellpadding="4">
    <tr valign="top">
        <td rowspan="2">
            <asp:Label ID="Label4" Text="Click on a Team/Competition to view the Fixtures" 
                runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                    style="text-align:center" Width="211px" Height="58px" 
                BackColor="#1B1B1B">
            </asp:Label>
         
         
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
        <td>
            <asp:Label ID="lblLeague" runat="server" Font-Names="Arial" ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">LEAGUE TABLE
            </asp:Label>
            <br />
             <asp:HyperLink ID="hlLeagueStats" Text="League Stats" runat="server" height="16px"
            BackColor="#99CCFF" BorderColor="Silver" BorderStyle="Solid" Font-Bold="True" ForeColor="Black" ></asp:HyperLink>
            <asp:GridView ID="gridTable" runat="server" GridLines="None" CssClass="gv"

                style="margin-top: 0px" AutoGenerateColumns="False" CellSpacing="2"
                Font-Names="Arial" Font-Size="12px" CellPadding="3" BackColor="#1B1B1B">
                <Columns>
                    <asp:BoundField DataField="Stats" HeaderText="">
                        <ItemStyle ForeColor="Black" Font-Size="10px" BackColor="#99CCFF" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>               
                    <asp:BoundField  DataField="Last 6"  HeaderText="Last 6">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle BackColor="#1B1B1B" ForeColor="LightGray" HorizontalAlign="Right"  Wrap="False" Font-Names="Consolas" Font-Size="medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Pos" HeaderText="Pos">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="White"  BackColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="Team" HeaderText="Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" Font-Size="14px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="Pld" HeaderText="Pld">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="W" HeaderText="W">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right"  />
                    </asp:BoundField>               
                    <asp:BoundField DataField="D"  HeaderText="D">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right"/>
                    </asp:BoundField>               
                    <asp:BoundField DataField="L"  headerText="L">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                    </asp:BoundField>
                   <asp:BoundField DataField="Pts" SortExpression="Pts" HeaderText="Pts" >
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Right" Font-Size="14px" />
                        </asp:BoundField>
                     <asp:BoundField DataField="Rolls" SortExpression="Rolls" HeaderText="Rolls" >
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="12px" />
                        </asp:BoundField>
                    <asp:BoundField DataField="Pins" SortExpression="Pins" HeaderText="Pins" >
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="12px"/>
                        </asp:BoundField>
                     <asp:BoundField DataField="Number_Thirties" SortExpression="Number_Thirties" 
                        ItemStyle-ForeColor="#0099FF" HeaderText="Away 30+">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Center" Font-Size="12px"/>
                    </asp:BoundField>
              </Columns>
           </asp:GridView>
            <br />
           <asp:Label ID="Label1" runat="server" Font-Names="Arial" ForeColor="Cyan"  style="text-align:left" Font-Size="15px" 
                BackColor="#1B1B1B">SORT TABLE BY : 
            </asp:Label>
            <asp:Button ID="btnPoints" runat="server" Text="Points" Font-Size="Small" 
                BorderStyle="Solid" />
            <asp:Button ID="btnRolls" runat="server" Text="Rolls"  Font-Size="Small" 
                BorderStyle="Solid"/>
            <asp:Button ID="btnPins" runat="server" Text="Pins"  Font-Size="Small" 
                BorderStyle="Solid"/>
            <asp:Button ID="btn30s" runat="server" Text="Away 30+"  Font-Size="Small" 
                BorderStyle="Solid"/>
            <br />
           <br />
           <br />

        </td>
        
        <td>
            <asp:Label ID="lblRecentResults" Text="RECENT RESULTS" runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />
           <br />

            <asp:GridView ID="gridRecentResults" runat="server" BorderColor="Black" GridLines="None" CssClass="gv"
                style="margin-top: 0px" 
                AutoGenerateColumns="False" CellSpacing = "2"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                    <asp:BoundField DataField="Fixture Calendar" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
<%--       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Home Team Name"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Home Team">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>--%>

                   <asp:BoundField DataField="Home Team Name" HeaderText="Home Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Right" Wrap="False" Font-Size="12px"/>
                    </asp:BoundField>


                    <asp:BoundField DataField="Home Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

<%--                      <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Away Team Name"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Away Team">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>
--%>
                     <asp:BoundField DataField="Away Team Name" HeaderText="Away Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" Font-Size="12px"/>
                    </asp:BoundField>



                    <asp:BoundField DataField="Home Rolls Result" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

<%--                  <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="More"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="">
                        <ControlStyle Font-Underline="True" ForeColor="White"></ControlStyle>
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

--%>                    <asp:BoundField DataField="More" HeaderText="">
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Left" Wrap="False" Font-Size="12px"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Fixture ID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>
<%--          <asp:Label ID="lblFiller"  runat="server" width="263px"></asp:Label>--%>

           <asp:button ID="btnClose" runat="server" Text="Hide Card" BackColor="Red" width="163px"  Font-Size="20px"/>
           
            <asp:Image ID="imgCard" runat="server" ImageAlign="Left" Height="450px" Width="450px" />
            <asp:GridView ID="gridResult" runat="server" GridLines="Both" 
                style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                AutoGenerateColumns="False" BackColor="#99CCFF" 
                Font-Names="Arial" Font-Size="14px" CellPadding="2">
                <Columns>
                    <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="lEFT" Wrap="false" />
                    </asp:BoundField>
<%--                    <asp:BoundField DataField="H1"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False"/> 
                    </asp:BoundField>
                    <asp:BoundField DataField="H2"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="H3"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="H4"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="H5"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
--%>                    <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false" />
                    </asp:BoundField>
<%--                    <asp:BoundField DataField="A1"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="A2"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False"/> 
                    </asp:BoundField>
                    <asp:BoundField DataField="A3"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False"/> 
                    </asp:BoundField>
                    <asp:BoundField DataField="A4"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False"/> 
                    </asp:BoundField>
                    <asp:BoundField DataField="A5"  ShowHeader="False" HtmlEncode="False" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
--%>                    <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                </Columns>
           </asp:GridView>
 
 
            <br />

            <asp:Label ID="lblPlayerStats" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                    style="text-align:center" Font-Size="18px" BorderWidth = "2PX"
                BackColor="#1B1B1B" Height="75px" Width="400px">Click on a Player to see her Stats for the Season. If any of the names are wrong, leave a message on the league WhatsApp group.
            </asp:Label>       
           <br />
           <br />
 
        </td>
    </tr>

    <tr valign="top">
        <td>
            <asp:Label ID="lblHighScores" Text="HIGH SCORES" runat="server" 
                BorderColor="Black" Font-Names="Arial" ForeColor="Yellow" 
                    style="text-align:left"  Font-Size="20px"
                BackColor="#1B1B1B">
            </asp:Label>

            <asp:GridView ID="gridHS" runat="server" GridLines="None" CssClass="gv"
                style="margin-top: 0px" 
                AutoGenerateColumns="False" CellSpacing="2"
                Font-Names="Arial" Font-Size="14px" CellPadding="3" BackColor="#1B1B1B">

                <Columns>
 <%--                   <asp:BoundField DataField="League" HeaderText="League">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>          
   --%>                   
                   <asp:BoundField DataField="Team" HeaderText="Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>          

                    <asp:BoundField DataField="Player" HeaderText="Player">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>          
                      
                    <asp:BoundField DataField="Score" HeaderText="Score">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Right" />
                    </asp:BoundField>               
               </Columns>
            </asp:GridView>

        </td>

        <td>
           <asp:Label ID="lblLateResults" Text="LATE RESULTS" runat="server" CssClass="gv"
                Font-Names="Arial" ForeColor="Yellow" 
                    style="text-align:left" Width="148px"  Font-Size="20px" 
                BackColor="#1B1B1B">
            </asp:Label>
            <asp:GridView ID="gridLateResults" runat="server" CssClass="gv"
                BorderStyle="None" BorderWidth="1px" GridLines="None" 
                style="margin-top: 0px;"
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="5" BackColor="#1B1B1B">
                <Columns>
                    <asp:BoundField DataField="Fixture Calendar" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>   

                    <asp:BoundField DataField="Home Team Name" HeaderText="Home Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Wrap="False"/>
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Right" Wrap="False"/>
                    </asp:BoundField>   

                    <asp:BoundField DataField="Home Result" HeaderText="">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="center" Wrap="False"   />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Right" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Away Team Name" HeaderText="Away Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False"/>
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
             
               </Columns>

            </asp:GridView>
        </td>
    </tr>
    </table>
        

</asp:Content>
