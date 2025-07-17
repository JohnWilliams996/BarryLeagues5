<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Default"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    a:link {text-decoration: none }
    a:active {text-decoration: none }
    a:visited {text-decoration: none }
    a:hover {text-decoration: underline }

    /** GRIDVIEW STYLES **/

    .gv tr.row:hover
    {
    	background-color:black;
    	
    }    
    
    a:hover
    {   
    	background-color:black;
    }
    
    </style>

    <style type="text/css">
        .style4
        {
            height: 24px;
        }
        .style5
        {
            height: 24px;
            width: 210px;
        }
        .style10
        {
            height: 24px;
            width: 200px;
            font-family: Arial;
        }
        .style11
        {
            height: 4px;
            width: 250px;
            font-family: Arial;
        }
        .style13
        {
            width: 250px;
            font-family: Arial;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

 
    <div >
         <table style="margin-right: 5px;" cellpadding="3" cellspacing="3">
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" ForeColor="#E4BB18" 
                        Text=""  BorderWidth="0px" 
                        style="margin-left: 25px"
                        Width="250px" BackColor="#1B1B1B"> </asp:Label>                            
                </td>
<%--                <td>
                    <asp:Image ID="Image7" runat="server" Height="119px" 
                        ImageUrl="~/Clubs/Images/merry xmas.gif" Width="500" />
                </td>
--%>            </tr>
        </table>        

        <asp:Label ID="Label1" runat="server" ForeColor="#E4BB18" 
            Text="Website Visits"  BorderWidth="0px" 
            style="margin-top: 6px"
            Font-Names="Arial" BackColor="#1B1B1B"> </asp:Label>  
        <br />
       <asp:Label ID="Label23" runat="server" ForeColor="#E4BB18" 
            Text="Today : "  BorderWidth="0px" 
            style="margin-top: 6px"
            Font-Names="Arial" BackColor="#1B1B1B"> </asp:Label>  
        <asp:TextBox ID="txtStatsToday" runat="server" BackColor="#FFCC00" 
            Enabled="False" Font-Names="Courier New" Font-Size="Medium" 
             ForeColor="Black"  Font-Bold="true" style="text-align:right"
              Width="40px" BorderStyle="None"></asp:TextBox>

       <asp:Label ID="Label21" runat="server" ForeColor="#E4BB18" 
            Text="This Week : "  BorderWidth="0px" 
            style="margin-top: 6px"
            Font-Names="Arial" BackColor="#1B1B1B"> </asp:Label>  
        <asp:TextBox ID="txtStatsWeek" runat="server" BackColor="#FFCC00" 
            Enabled="False" Font-Names="Courier New" Font-Size="Medium" 
             ForeColor="Black"  Font-Bold="true" style="text-align:right"
              Width="50px" BorderStyle="None"></asp:TextBox>

       <asp:Label ID="Label22" runat="server" ForeColor="#E4BB18" 
            Text="This Season : "  BorderWidth="0px" 
            style="margin-top: 6px"
            Font-Names="Arial" BackColor="#1B1B1B"> </asp:Label>  
        <asp:TextBox ID="txtStatsSeason" runat="server" BackColor="#FFCC00" 
            Enabled="False" Font-Names="Courier New" Font-Size="Medium" 
             ForeColor="Black"  Font-Bold="true" style="text-align:right"
              Width="60px" BorderStyle="None"></asp:TextBox>


        
                                  
-        <!-- Start of StatCounter Code for Microsoft Publisher -->
<%--       <script type="text/javascript">
            var sc_project = 7481713;
            var sc_invisible = 0;
            var sc_security = "27d25d00"; 
        </script>

        <script type="text/javascript"
            src="http://www.statcounter.com/counter/counter.js">
        </script>
        <noscript>
            <div class="statcounter"  >
                <a title="hit counter" 
                    href="http://statcounter.com/" target="_blank" >
                    class="statcounter"
                    src="http://c.statcounter.com/7481713/0/27d25d00/0/"
                    alt="hit counter">
                </a>
            </div>
        </noscript>
--%>        <!-- End of StatCounter Code for Microsoft Publisher -->




     </div>


   <div>
        <asp:Label ID="lblLiveTest" runat="server" ForeColor="#E4BB18" 
            Text="www Live-Test:"  BorderWidth="0px" 
            style="margin-top: 6px"
            Width="231px" Font-Names="Arial" BackColor="#003399"> </asp:Label>        
        <asp:Label ID="lblWorkHome" runat="server" ForeColor="#E4BB18" 
            Text="Work-Home:"  BorderWidth="0px" 
            style="margin-top: 6px"
            Width="231px" Font-Names="Arial" BackColor="#003399"> </asp:Label> 
                   
        
                   
        <br />
                   
    </div>

      <div id="divTables" dir="ltr">
         <table style="margin-right: 5px;" cellpadding="3" cellspacing="3">
            <tr>
                <td  bgcolor="#1b1b1b" >
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                        style="text-align:left" Text="League Tables"  Width="160px" 
                        BackColor="#1B1B1B" Height="30px" Font-Size="24px" BorderWidth="1px"> </asp:Label>

                    <asp:Label ID="Label12" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                            style="text-align:left; margin-top: 0px;" 
                          Text="(* = Points Deducted)"  Width="180px" 
                          BackColor="#1B1B1B" Font-Size="12px"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:GridView ID="gridTables" runat="server" GridLines="None" CssClass="gv"
                            style="margin-top: 0px; margin-right: 1px;" Height="136px" 
                        AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="12px" CellPadding="1" CellSpacing="1" BackColor="#1B1B1B">
                        <Columns>

                            <asp:BoundField DataField="Stats" HeaderText="">
                                <ItemStyle ForeColor="Black" Font-Size="10px" BackColor="#99CCFF" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>               

                            <asp:BoundField DataField="Team" >
                            <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap = "false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Pld" >
                            <ItemStyle ForeColor="DarkKhaki" HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Pts"  >
                            <ItemStyle ForeColor="white" BackColor="DarkGreen" HorizontalAlign="Right" />
                            </asp:BoundField>
 
 <%--                           <asp:BoundField DataField="Number Nines"  >
                            <ItemStyle ForeColor="White" HorizontalAlign="Center" />
                            </asp:BoundField>
 --%>
                           <asp:BoundField DataField="Show Champions"  HeaderText="" Visible="False" >
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
 
            <tr>
                <td  bgcolor="#1b1b1b">
                    <asp:Label ID="Label25" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                        style="text-align:left" Text="Stats Leaders"  Width="163px" 
                        BackColor="#1B1B1B" Height="30px" Font-Size="24px" BorderWidth="1px"> </asp:Label>
                </td>
            </tr>

            <tr>
                <td >
                    <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" ForeColor="Yellow" 
                        NavigateUrl="~/Clubs/Stats.aspx?League=CRIB DIVISION 1" 
                        style="text-align:Left; font-size: small" Text="CRIB DIVISION 1"
                        Font-Names="Arial" Height="19px" BackColor="#1B1B1B"> </asp:HyperLink>

                 <asp:Chart ID="chtLeague_Crib" runat="server" ViewStateContent="Appearance" 
                    BackColor="#1B1B1B" 
                    BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                    BorderlineColor="Transparent" Palette="SeaGreen" Width="240px" 
                    Visible="False" >
                       <Series>
                         <asp:Series ChartType="Bar" Name="League">
                         </asp:Series>
                     </Series>
                     <ChartAreas>
                         <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true"  >
                         </asp:ChartArea>
                     </ChartAreas>          
                </asp:Chart>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink9" runat="server" Font-Bold="True" ForeColor="#6699FF" 
                        NavigateUrl="~/Clubs/Stats.aspx?League=SKITTLES DIVISION 1" 
                        style="text-align:Left; font-size: small" text="SKITTLES DIVISION 1"
                        Font-Names="Arial" Height="19px" BackColor="#1B1B1B"> </asp:HyperLink>

                     <asp:Chart ID="chtLeague_Skittles" runat="server" ViewStateContent="Appearance" 
                        BackColor="#1B1B1B" 
                        BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                        BorderlineColor="Transparent" Palette="SeaGreen" Width="240px" 
                        Visible="False" >
                         <Series>
                             <asp:Series ChartType="Bar" Name="League">
                             </asp:Series>
                         </Series>
                         <ChartAreas>
                             <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true"  >
                             </asp:ChartArea>
                         </ChartAreas>          
                    </asp:Chart>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink13" runat="server" Font-Bold="True" ForeColor="#00CC66" 
                        NavigateUrl="~/Clubs/Stats.aspx?League=SNOOKER DIVISION 1"  
                        style="text-align:Left; font-size: small" text="SNOOKER DIVISION 1"
                        Font-Names="Arial" Height="19px" BackColor="#1B1B1B"> </asp:HyperLink>

                     <asp:Chart ID="chtLeague_Snooker1" runat="server" ViewStateContent="Appearance" 
                        BackColor="#1B1B1B" 
                        BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                        BorderlineColor="Transparent" Palette="SeaGreen" Width="240px" 
                        Visible="False"  >
                         <Series>
                             <asp:Series ChartType="Bar" Name="League">
                             </asp:Series>
                         </Series>
                         <ChartAreas>
                             <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true"  >
                             </asp:ChartArea>
                         </ChartAreas>          
                    </asp:Chart>

                  </td>
            </tr>
            <tr>
                <td>
                      <asp:HyperLink ID="HyperLink15" runat="server" Font-Bold="True" ForeColor="#00CC66" 
                            NavigateUrl="~/Clubs/Stats.aspx?League=SNOOKER DIVISION 2"   
                            style="text-align:Left; font-size: small"  text="SNOOKER DIVISION 2"
                            Font-Names="Arial" Height="19px" BackColor="#1B1B1B"> </asp:HyperLink>

                     <asp:Chart ID="chtLeague_Snooker2" runat="server" ViewStateContent="Appearance" 
                        BackColor="#1B1B1B" 
                        BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                        BorderlineColor="Transparent" Palette="SeaGreen" Width="240px" 
                        Visible="False"  >
                         <Series>
                             <asp:Series ChartType="Bar" Name="League">
                             </asp:Series>
                         </Series>
                         <ChartAreas>
                             <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true"  >
                             </asp:ChartArea>
                         </ChartAreas>          
                     </asp:Chart>
  
                </td>
            </tr>
        </table>
    </div>

    <div id="divDefault">

        <table  style="width: 450px; margin-left: 5px;" cellpadding="3" cellspacing="5">

            <tr>
                <td class="style13" bgcolor="#1b1b1b" valign="top">
                    <asp:Label ID="Label13"  Text="League Sponsors" Width="197px" Height="27px" 
                            runat="server" ForeColor="#E4BB18" BorderWidth="1px" 
                        Font-Names="Arial" BackColor="#1B1B1B" Font-Size="24px"></asp:Label>
                    <br />
                    <br />

                   <asp:ImageButton ID="Image1" runat="server" OnClientClick="Navigate1()" 
                        ImageUrl="~/Clubs/Images/InnHouse.jpg" Width="104px" Height="70px" 
                        BorderColor="#666699" BorderStyle="Solid" BorderWidth="1px" />

  
                   <asp:ImageButton ID="Image2" runat="server" OnClientClick="Navigate2()" 
                        ImageUrl="~/Clubs/Images/J J Windows.jpg" Width="123px" Height="70px" 
                        BorderColor="#666699" BorderStyle="Solid" BorderWidth="1px" />

                    <br />
                    <br />
                    <asp:Label ID="Label5"  Text="Contact League Secretary to advertise on the website" 
                        Width="300px" Height="44px" 
                            runat="server" ForeColor="#E4BB18" BorderWidth="0px" 
                        Font-Names="Arial" BackColor="#1B1B1B"></asp:Label>
                    <br />
                    <br />
                    
                    <asp:ImageButton ID="Image3" runat="server" OnClientClick="Navigate3()" 
                        ImageUrl="~/Clubs/Images/IJP Automotive.jpg" Width="107px" Height="70px" 
                        BorderColor="#666699" BorderStyle="Solid" BorderWidth="1px" Visible="false" />

                    <asp:ImageButton ID="Image4" runat="server" OnClientClick="Navigate4()" 
                        ImageUrl="~/Clubs/Images/rim motors.jpg" Width="107px" Height="70px" 
                        BorderColor="#666699" BorderStyle="Solid" BorderWidth="1px" />


                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server" ForeColor="#E4BB18" 
                        Text="Facebook Group" BorderWidth="1px" 
                        Width="194px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="24px"></asp:Label>
                    <br />
                    <br />
                   <asp:Label ID="Label2" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px"  Text="Click below to open the league Facebook group"></asp:Label>
                      <br />
                    <br />
                    <asp:ImageButton ID="Image5" runat="server" OnClientClick="Navigate5()" 
                        ImageUrl="~/Clubs/Images/Facebook1.png" Width="44px" Height="44px" 
                        BorderColor="#666699" BorderStyle="Solid" BorderWidth="1px" />
                    <br />
                  <br />
                    <br />

                    <asp:Label ID="Label4" runat="server" ForeColor="#E4BB18" 
                        Text="Upcoming Events" BorderWidth="1px" 
                        Width="194px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="24px"></asp:Label>
                    <br />
                    <br />
    
                    <asp:Label ID="Label7" runat="server" BackColor="#1B1B1B" Font-Names="Arial" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px"  Text="Next Meeting"> </asp:Label>
                    <br />
                   <asp:Label ID="lblNextMeeting" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="16px"> </asp:Label>
                    <br />
                    <br />
<%--                   <asp:Label ID="Label33" runat="server" ForeColor="#E4BB18" 
                        Text="A.G.M." BorderWidth="1px"
                        Width="185px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label44" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="To be held at the Liberals Club on Monday 14th July - 7pm start."></asp:Label>
                    <br />
                    <br />
                    <br />
                   <asp:Label ID="lblPresentation" runat="server" ForeColor="#E4BB18" 
                        Text="Presentation Night" BorderWidth="1px"
                        Width="185px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblPresentationDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="To be held at the Football Club on Saturday 5th July - 7.30pm."></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblLabel98" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="Featuring:"></asp:Label>
                    <br />
                    <asp:Label ID="lblBand" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="20px" Width = "300px" Text="The Jane Williams Band"></asp:Label>
                    <br />
                    <br />
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="Navigate4()" 
                        ImageUrl="~/Clubs/Images/The Jane Williams Band.jpg" Width="265px" Height="240px" 
                        BorderColor="#666699" BorderStyle="Solid" BorderWidth="1px" />                    
                    <br />
                    <br />
                    <asp:Label ID="Label99" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="Tickets (£5) available at the Football Club and RIM Motors and on the door on the night (subject to availability).">
                    </asp:Label>
                    <br />
                    <br />--%>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label16" runat="server" ForeColor="#E4BB18" 
                        Text="League Fixtures by Venue" BorderWidth="1px" 
                        Width="200px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="24px"></asp:Label>
                    <br />
                    <br />

                    <asp:Label ID="Label17" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" 
                        Text="Venue : "
                        Font-Size="16px"> </asp:Label>

                    <asp:DropDownList ID="ddlVenues" runat="server" BackColor="Black" 
                        ForeColor="#E4A519">
                    </asp:DropDownList>

                    <asp:Button ID="btnGo" runat="server" BackColor="Black" BorderColor="#E4BB18" 
                        Font-Names="Arial" Font-Size="15px" ForeColor="#E4BB18" 
                        Height="32px" Text="Go" BorderStyle="Solid"  />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnRules" runat="server" BackColor="#1B1B1B" BorderColor="#E4BB18" 
                        Font-Names="Arial" Font-Size="20px" Font-Bold="false" ForeColor="#E4BB18" Font-Underline = "true"
                        Height="32px" Text="League Rules   (PDF)" BorderStyle="None" ToolTip="Click to view the league rules" Width="200px"/>
                    <br />

                </td>
                <td class="style10" bgcolor="#1b1b1b" rowspan="2" valign="top" >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label14"  
                                Text="Fixtures &amp; Results"  Height="27px" runat="server" 
                                ForeColor="#E4BB18" BorderWidth="1px" 
                                Font-Names="Arial" BackColor="#1B1B1B" style="margin-top: 0px" 
                                Font-Size="24px"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblTextResult"  
                                Text="Both teams to text result on completion of match"
                                Width="417px" Height="20px" 
                                    runat="server" ForeColor="#E4BB18" BorderWidth="0px" 
                                Font-Names="Arial" BackColor="#1B1B1B">
                            </asp:Label>    
                            <br />

  
                            <asp:Label ID="Label26" runat="server" BackColor="#1B1B1B" Height="32px" 
                                style="margin-right: 7px" Width="14px"></asp:Label>
                            <asp:DropDownList ID="ddWeeks" runat="server"
                                BackColor="Black" BorderColor="#E4BB18"  AutoPostBack="true"
                                BorderStyle="Solid" Font-Names="Arial" Font-Size="17px" ForeColor="#E4BB18" 
                                Height="32px"  Width="263px"/>

                                   
                            <asp:Label ID="Label19" runat="server" BackColor="#1B1B1B" Height="32px" 
                                style="margin-right: 7px" Width="14px"></asp:Label>
                            <asp:Button ID="btnOutstanding" runat="server" BackColor="Black" BorderColor="#E4BB18" 
                                BorderStyle="None" Font-Names="Arial" Font-Size="15px" ForeColor="#E4BB18" 
                                Height="32px" Text="Outstanding League Matches" />

                            <br />
                            <br />
                            <div id="Weeks">
                                <asp:Label ID="lblWeek1" runat="server" BackColor="#1B1B1B" Height="32px" 
                                    style="margin-right: 7px" Width="18px" ForeColor="#E4BB18" 
                                    Font-Size="11px"> Week
                                </asp:Label>
                                <asp:Button ID="btn1" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="1" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn2" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="2" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn3" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="3" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn4" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="4" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn5" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="5" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn6" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="6" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn7" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="7" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn8" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="8" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn9" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="9" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn10" runat="server" BackColor="Black" BorderStyle="None"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="10" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn11" runat="server" BackColor="Black" BorderStyle="none"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="11" width="19px"
                                    Font-Bold="False" Height="23px"   />     
                                <asp:Button ID="btn12" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="12" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn13" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="13" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn14" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="14" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn15" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="15" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <br />
                               <asp:Label ID="lblWeek2" runat="server" BackColor="#1B1B1B" Height="32px" 
                                    style="margin-right: 7px" Width="18px" ForeColor="#E4BB18" 
                                    Font-Size="11px"> Week
                                </asp:Label>
                                 <asp:Button ID="btn16" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="16" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn17" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="17" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn18" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="18" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn19" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="19" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn20" runat="server" BackColor="Black" BorderStyle="None"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="20" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn21" runat="server" BackColor="Black" BorderStyle="none"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="21" width="19px"
                                    Font-Bold="False" Height="23px"   />     
                                <asp:Button ID="btn22" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="22" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn23" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="23" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn24" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="24" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn25" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="25" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn26" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="26" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn27" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="27" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn28" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="28" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn29" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="29" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn30" runat="server" BackColor="Black" BorderStyle="None"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="30" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <br />
                                <asp:Label ID="lblWeek3" runat="server" BackColor="#1B1B1B" Height="32px" 
                                    style="margin-right: 7px" Width="18px" ForeColor="#E4BB18" 
                                    Font-Size="11px"> Week 
                                </asp:Label>
                                <asp:Button ID="btn31" runat="server" BackColor="Black" BorderStyle="none"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="31" width="19px"
                                    Font-Bold="False" Height="23px"   />     
                                <asp:Button ID="btn32" runat="server" BackColor="Black" BorderStyle="none"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="32" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn33" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="33" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn34" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="34" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn35" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="35" width="19px" 
                                    Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn36" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="36" width="19px" 
                                    Font-Bold="False" Height="23px"  />                                 
                                <asp:Button ID="btn37" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="37" width="19px" 
                                    Font-Bold="False" Height="23px"  />                                 
                                <asp:Button ID="btn38" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="38" width="19px" 
                                    Font-Bold="False" Height="23px"  />                                 
                                <asp:Button ID="btn39" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="39" width="19px" 
                                        Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn40" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="40" width="19px" 
                                        Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn41" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="41" width="19px" 
                                        Font-Bold="False" Height="23px" />                                 
                                <asp:Button ID="btn42" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="42" width="19px" 
                                        Font-Bold="False" Height="23px" />                                 
                                 <asp:Button ID="btn43" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="43" width="19px" 
                                        Font-Bold="False" Height="23px" />                                 
                                 <asp:Button ID="btn44" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="44" width="19px" 
                                        Font-Bold="False" Height="23px" />                                 
                                 <asp:Button ID="btn45" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="45" width="19px" 
                                        Font-Bold="False" Height="23px" />                                 
                           </div>
                            <br />
                            <asp:Label ID="lblLibs" runat="server" 
                                    Text="Skiittles Teams: All games to now start at 8pm.King William XII are now playing on Thursday's at the Football Club."
                                    BackColor="Red" BorderStyle="Double" Font-Size="Large" 
                                    ForeColor="White"  Font-Bold="False" style="text-align: center;"
                                    Height="76px" Width="534px" Visible="False" ></asp:Label>
  <%--                          <br />
                            <br />--%>
                            <asp:GridView ID="gridResults" runat="server" GridLines="None" CssClass="gv"
                                Height="136px"  BackColor="#1B1B1B" 
                                BorderStyle="None" AutoGenerateColumns="False" CellSpacing="3" 
                                Font-Names="Arial" Font-Size="12px">
                                <Columns>
                                    <asp:BoundField DataField="League" HeaderText="League/Cup">
                                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" />
                                    <ItemStyle ForeColor="LightGreen" HorizontalAlign="Left" Wrap="false" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Fixture Date" HeaderText="">
                                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                    <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Home Team Name" HeaderText="Home Team" >
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Right" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BorderColor = "#1B1B1B" BackColor="#1B1B1B"  HorizontalAlign="Right" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>  

                                    <asp:BoundField DataField="Home Result" HeaderText="Result">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False"/>
                                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Away Team Name" HeaderText="Away Team">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B" BorderColor = "#1B1B1B"  HorizontalAlign="left" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>   

                                    <asp:BoundField DataField="Fixture ID" visible="True">
                                    <ItemStyle ForeColor="#1B1B1B" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fixture ID2">
                                    <ItemStyle ForeColor="#1B1B1B" HorizontalAlign="Right" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" visible="False">
                                    <ItemStyle ForeColor="#1B1B1B" HorizontalAlign="Right" Wrap="True"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Week Number" visible="False">
                                    <ItemStyle ForeColor="#1B1B1B" HorizontalAlign="Right" Wrap="True"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fixture Type" visible="false">
                                    <ItemStyle ForeColor="#1B1B1B" HorizontalAlign="Right" Wrap="True"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Venue" HeaderText="Venue">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B" BorderColor = "#1B1B1B"  HorizontalAlign="left" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>   

                                </Columns>
                            </asp:GridView>

<%--                           <asp:Label ID="lblFiller"  runat="server" width="263px"></asp:Label>--%>

                           <br />
                           <br />
                          <asp:Timer ID="TimerRefresh" runat="server" Interval="3000">
                            </asp:Timer>
                           <asp:Label ID="lblLatest" runat="server" ForeColor="#E4BB18" 
                                Text="" BorderWidth="1px"
                                Width="215px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                            <asp:Label ID="lblFiller"  runat="server" width="30px"></asp:Label>
                           <br />
                            <asp:CheckBox ID="chkRefresh" runat="server" Checked="True" ForeColor="#E4BB18" Text="Automatically Refresh Results" AutoPostBack="True" CausesValidation="True" />
                           <br />
                           <br />
                         <asp:button ID="btnView" runat="server" Text="View Card" BackColor="Green" ForeColor="White" Font-Size="16px" BorderWidth="2px"  CausesValidation="False" Height="43px" Width="166px" BorderColor="White"/>
                            <asp:Label ID="lblCurrent" BackColor="Green" ForeColor="White"  runat="server" BorderWidth="1px"></asp:Label>


                            <asp:GridView ID="gridSkittlesResult" runat="server" GridLines="Both" 
                                style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                                AutoGenerateColumns="False" BackColor="#99CCFF" visible="false"
                                Font-Names="Arial" Font-Size="14px" CellPadding="2">
                                <Columns>
                                    <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false" Width = "15px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="lEFT" Wrap="false" Width = "160px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false"  Width = "150px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gridCribResult" runat="server" GridLines="Both" 
                                style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                                AutoGenerateColumns="False" BackColor="#FFFF99" visible="false"
                                Font-Names="Arial" Font-Size="14px" CellPadding="2">
                                <Columns>
                                    <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false" Width = "15px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Left" Wrap="false" Width = "160px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false"  Width = "150px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gridSnookerResult" runat="server" GridLines="Both" 
                                style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                                AutoGenerateColumns="False" BackColor="#CCFFCC" visible="false"
                                Font-Names="Arial" Font-Size="14px" CellPadding="2">
                                <Columns>
                                    <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false" Width = "15px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="lEFT" Wrap="false" Width = "160px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false"  Width = "150px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>

            </tr>


            <tr>
<%--                <td>
                   <asp:Label ID="Label20" runat="server" ForeColor="#E4BB18" 
                        Text="Presentation Night" BorderWidth="1px" 
                        Width="194px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="22px"></asp:Label>
                    <br />
                    <br />
                   <asp:Label ID="Label18" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" 
                        Text="Saturday 21st July @ Sea View 7.30pm"
                        Font-Size="16px">
                    </asp:Label>
                    <br />
                    <br />
                      <asp:Label ID="Label24" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" 
                        Text="Featuring comedian Gary Le Vell and Disco"
                        Font-Size="16px">
                    </asp:Label>
                    <br />
                    <br />
                 </td>
--%>            </tr>
            
            <tr  valign="top">
                <td bgcolor="#1b1b1b">
                     <asp:Label ID="lblCrib" runat="server" ForeColor="Yellow"
                        Text="Crib Cups" BorderWidth="1px" 
                        Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblCribKO" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblCribKODate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:HyperLink ID="hlCribKO" runat="server" Font-Bold="false" ForeColor="white"  visible="false"
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    


                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblCribPairs" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblCribPairsDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:HyperLink ID="hlCribPairs" runat="server" Font-Bold="false" ForeColor="white"  visible="false"
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    
                    <br />
                    <br />

                </td>

                <td bgcolor="#1b1b1b" >
                   <asp:Label ID="lblSkittles" runat="server" ForeColor="#6699FF"
                        Text="Skittles Cups" BorderWidth="1px"
                        Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblSkittles12aSide" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSkittles12aSideDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:HyperLink ID="hlSkittles12aSide" runat="server" Font-Bold="false" ForeColor="white"  visible="false"
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    
                    <br />
                    <br />
                    <br />


                    <asp:Label ID="lblSkittles6aSide" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSkittles6aSideDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:HyperLink ID="hlSkittles6aSide" runat="server" Font-Bold="false" ForeColor="white" Visible="false" 
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    
                    <br />
                    <br />
                    <br />
<%--                    <asp:Label ID="Label3" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="true" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="Stats Playoff"> </asp:Label>
                    <br />
                    <asp:Label ID="Label9" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="Winner: A Jones (Barries Boys). Runners-Up: D Carpenter (Cambrian Flyers) & A Ellis (Slammers)"> </asp:Label>
                    <br />
                    <br />--%>
                </td>
            </tr>

            <tr  valign="top">
                <td bgcolor="#1b1b1b">
                    <asp:Label ID="lblSnooker1" runat="server" ForeColor="LightGreen" 
                        Text="Snooker Division 1 Cups" BorderWidth="1px"
                        Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblSnooker1KO" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSnooker1KODate" runat="server" BackColor="#1B1B1B" Font-Names="Arial" visible="false" 
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker1KO" runat="server" Font-Bold="false" ForeColor="white" visible="false" 
                        NavigateUrl="~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - TEAM KO" 
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />                  
                    <br />                  
                    <br />

 
                    <asp:Label ID="lblSnooker1Singles" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSnooker1SinglesDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial" visible="false" 
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker1Singles" runat="server" Font-Bold="false" ForeColor="white" visible="false" 
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />
                    <br />
                    <br />


                    <asp:Label ID="lblSnooker1Doubles" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSnooker1DoublesDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial" visible="false"  
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker1Doubles" runat="server" Font-Bold="false" ForeColor="white" visible="false"  
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />
                    <br />
                    <br />


                    <asp:Label ID="lblSnooker13aSide" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSnooker13aSideDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial" visible="false"  
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker13aSide" runat="server" Font-Bold="false" ForeColor="white" visible="false"  
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />
                    <br />
                </td>
 
 
                 <td bgcolor="#1b1b1b">
                     <asp:Label ID="lblSnooker2" runat="server" ForeColor="LightGreen"  
                        Text="Snooker Division 2 Cups" BorderWidth="1px" 
                        Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblSnooker2KO" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSnooker2KODate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"  
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker2KO" runat="server" Font-Bold="false" ForeColor="white" visible="false"   
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />                  
                    <br />                  
                    <br />

 
                   <asp:Label ID="lblSnooker2Singles" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                   <asp:Label ID="lblSnooker2SinglesDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial" visible="false"   
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker2Singles" runat="server" Font-Bold="false" ForeColor="white" visible="false"   
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />
                    <br />
                    <br />


                   <asp:Label ID="lblSnooker2Doubles" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSnooker2DoublesDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial" visible="false"   
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker2Doubles" runat="server" Font-Bold="false" ForeColor="white" 
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />
                    <br />
                    <br />


                   <asp:Label ID="lblSnooker23aSide" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false" Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label>
                    <br />
                    <asp:Label ID="lblSnooker23aSideDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"  
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"> </asp:Label> 
                    <br />
                    <asp:HyperLink ID="hlSnooker23aSide" runat="server" Font-Bold="false" ForeColor="white" visible="false"   
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>  
                    <br />
                    <br />
                </td>
            </tr>

            <tr valign="top">
                <td bgcolor="#1b1b1b" height="240px" >
                    <asp:Label ID="lblHonours" runat="server" ForeColor="#E4BB18" 
                        Text="yyyy/yy Honours"  BorderWidth="1px" 
                        style="margin-top: 6px"
                        Width="176px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="22px"></asp:Label>        

                        &nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="MoreHL" runat="server" Font-Names="Arial" 
                        ForeColor="#E4BB18" NavigateUrl="~/Clubs/Honours.aspx" Height="26px" 
                        style="margin-top: 6px"
                        BackColor="#1B1B1B" Font-Size="22px">More ... </asp:HyperLink>  
                    <br />  
                   <asp:GridView ID="gridHonours" runat="server" GridLines="None" 
                        style="margin-top: 1px" Height="136px" BackColor="#1B1B1B" 
                            Font-Size="12px" CellPadding="2" CellSpacing="5" Width="240px">
                    </asp:GridView>   
                    <br />
                    <br />
                </td>

                <td bgcolor="#1b1b1b" height="240px" valign="top">
                    <asp:HyperLink ID="HyperLinkCD" runat="server" Font-Names="Arial" 
                        ForeColor="#E4BB18" NavigateUrl="~/Clubs/Delegates.aspx" BackColor="#1B1B1B" 
                        Font-Size="22px" BorderWidth="1px"> yyyy/yy Clubs & Delegates </asp:HyperLink>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                
                   <br />
               </td>
           </tr>
            
        </table>
    </div>
    <br />


    
     <div class="clearboth">
     </div>
</asp:Content>
