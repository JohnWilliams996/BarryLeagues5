<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Default1"  %>



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
            width: 600px;
            font-family: Arial;
        }
        .style11
        {
            height: 24px;
            width: 250px;
            font-family: Arial;
        }
        .style12
        {
            width: 450px;
        }
        .auto-style1 {
            width: 520px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
            </tr>

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
         <table  cellpadding="2" cellspacing="2">
            <tr>
                <td  bgcolor="#1b1b1b" class="auto-style1" >
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                        style="text-align:left" Text="League Tables"  
                        BackColor="#1B1B1B" Height="30px" Font-Size="24px" BorderWidth="1px"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:GridView ID="gridTables" runat="server" GridLines="None" Width="180px"
                            style="margin-top: 0px; margin-right: 3px;" Height="136px"
                        AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="12px" CellPadding="2" CellSpacing="2" BackColor="#1B1B1B">
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

                            <asp:BoundField DataField="Pts">
                            <ItemStyle ForeColor="White" BackColor="DarkGreen"  HorizontalAlign="Right" />
                            </asp:BoundField>
 
                            <asp:BoundField DataField="Number_Rolls"  >
                            <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                            </asp:BoundField>
 
                            <asp:BoundField DataField="Pins"  >
                            <ItemStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                            </asp:BoundField>
 
<%--                            <asp:BoundField DataField="Number_Thirties"  >
                            <ItemStyle ForeColor="White" HorizontalAlign="Center" />
                            </asp:BoundField>
 --%>
                           <asp:BoundField DataField="ShowChampions"  HeaderText="" Visible="False" >
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
 
            <tr>
                <td  bgcolor="#1b1b1b" class="auto-style1">
                    <asp:Label ID="Label25" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                        style="text-align:left" Text="Stats Leaders"  Width="163px" 
                        BackColor="#1B1B1B" Height="30px" Font-Size="24px" BorderWidth="1px"> </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <asp:HyperLink ID="HyperLink9" runat="server" Font-Bold="True" ForeColor="#6699FF" 
                        NavigateUrl="~/Ladies_Skit/Stats.aspx?League=DIVISION 1" 
                        style="text-align:Left; font-size: small" text="DIVISION 1"
                        Font-Names="Arial" Height="19px" BackColor="#1B1B1B"> </asp:HyperLink>
                    <br />

                     <asp:Chart ID="chtLeague_Division1" runat="server" ViewStateContent="Appearance" 
                        BackColor="#1B1B1B" 
                        BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                        BorderlineColor="Transparent" Palette="SeaGreen" Width="400px" Height="400px"
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
<%--            </tr>

            <tr>--%>
                <td class="auto-style1">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" ForeColor="#6699FF" 
                        NavigateUrl="~/Ladies_Skit/Stats.aspx?League=DIVISION 2" 
                        style="text-align:Left; font-size: small" text="DIVISION 2"
                        Font-Names="Arial" Height="19px" BackColor="#1B1B1B"> </asp:HyperLink>
                    <br />

                     <asp:Chart ID="chtLeague_Division2" runat="server" ViewStateContent="Appearance" 
                        BackColor="#1B1B1B" 
                        BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                        BorderlineColor="Transparent" Palette="SeaGreen" Width="400px" Height="400px"
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
<%--            </tr>

            <tr>--%>
                  <td class="auto-style1">
                    <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" ForeColor="#6699FF" 
                        NavigateUrl="~/Ladies_Skit/Stats.aspx?League=DIVISION 3"
                        style="text-align:Left; font-size: small" text="DIVISION 3"
                        Font-Names="Arial" Height="19px" BackColor="#1B1B1B"> </asp:HyperLink>
                    <br />

                     <asp:Chart ID="chtLeague_Division3" runat="server" ViewStateContent="Appearance" 
                        BackColor="#1B1B1B" 
                        BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                        BorderlineColor="Transparent" Palette="SeaGreen" Width="400px" Height="400px"
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
        </table>
    </div>

    <div id="divDefault">

    

        <table  style=" margin-left: 85px;" cellpadding="2" cellspacing="2">

            <tr>
                <td class="style11" bgcolor="#1b1b1b" valign="top">                   
                   <asp:Label ID="Label2" runat="server" ForeColor="#E4BB18" Text="Upcoming Events" BorderWidth="1px" 
                        Width="194px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="24px"></asp:Label>
                    <br />
                    <br />
                    <br />
                   <asp:Label ID="Label7" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="16px"  Text="Next Meeting"> </asp:Label>
                    <br />
                    <br />
                   <asp:Label ID="lblNextMeeting" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="16px"> </asp:Label>
                    <br />                   
                    <br />
                    <asp:HyperLink ID="HLMintues" runat="server" Font-Names="Arial" 
                        ForeColor="#E4BB18" NavigateUrl="~/Ladies_Skit/Meeting Minutes.aspx" Height="26px" 
                        style="margin-top: 6px"
                        BackColor="#1B1B1B" Font-Size="20px" Font-Underline="True">Meeting Minutes & League Rules</asp:HyperLink>
                    <br />
                    <br />
                    <br />
                    <%--                    <asp:Label ID="Label3" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  Font-Underline = "true"
                        ForeColor="#E4BB18" Font-Size="20px"  Text="End of Season Events"> </asp:Label>
 -                  
                    <br />
                    <br />
                   <asp:Label ID="Label8" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="16px" Text="Champion of Champions - Friday 9th June at the Football Club"> </asp:Label>
                    <br />
                    <br />
                   <asp:Label ID="Label9" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="16px" Text="Jubilee Cup (Top Average) -  Wednesday 7th June at the Football Club"> </asp:Label>
                    <br />
                    <br />
                   <asp:Label ID="Label10" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="16px" Text="Presentation Night - Saturday 1st July at the Wyndham Cons"> </asp:Label>
                    <br />
                    <br />
                   <asp:Label ID="Label11" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="16px" Text="A.G.M. - Friday 7th July at the Liberals"> </asp:Label>
                    <br />                   
                   <asp:Label ID="Label12" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="14px" Text="19:00 - A.G.M."> </asp:Label>
                    <br />                   
                   <asp:Label ID="Label13" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="14px" Text="19:30 - Registration for the 2023/24 season"> </asp:Label>
                    <br />                   
                   <asp:Label ID="Label18" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="14px" Text="19:45 - Secretaries night"> </asp:Label>
                   <br />                   
                    <br />--%>
                    <br />                   
                    <br />
                    <%--                   <asp:Label ID="Label3" runat="server" ForeColor="#E4BB18" 
                        Text="A.G.M." BorderWidth="1px"
                        Width="185px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                   <asp:Label ID="lblAGM" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" Font-Size="16px" Text = "To be held at the Liberals on Wednesday 3rd July - 7.00pm start"> </asp:Label>
                      <br />
                    <br />
                    <br />
                    <br />
                  <asp:Label ID="lblPresentation" runat="server" ForeColor="#E4BB18" 
                        Text="Presentation Night" BorderWidth="1px"
                        Width="185px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblPresentationDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="To be held at the Wyndham on Saturday 29th June - 7.30pm start"></asp:Label>
                    <br />                   
                    <br />
                    <br />
                    <br />
--%>
                    <br />
                    <br />
                    <br />
                    <br />                 
                    <asp:Label ID="Label16" runat="server" ForeColor="#E4BB18" 
                        Text="League Fixtures by Venue" BorderWidth="1px" visible="true"
                        Width="200px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="24px"></asp:Label>
                    <br />
                    <br />

                    <asp:Label ID="Label17" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" visible="true"
                        Text="Venue : "
                        Font-Size="16px"> </asp:Label>

                    <asp:DropDownList ID="ddlVenues" runat="server" BackColor="Black" 
                        ForeColor="#E4A519" visible="true" >
                    </asp:DropDownList>

                    <asp:Button ID="btnGo" runat="server" BackColor="Black" BorderColor="#E4BB18" 
                        Font-Names="Arial" Font-Size="15px" ForeColor="#E4BB18" 
                        Height="32px" Text="Go" BorderStyle="Solid"  visible="true" />

                    <br />
                    <br />                 
                    <br />                 
                </td>                
                <td  bgcolor="#1b1b1b" rowspan="3" valign="top" >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label14"  
                                Text="Fixtures &amp; Results" Height="27px" runat="server" 
                                ForeColor="#E4BB18" BorderWidth="1px" 
                                Font-Names="Arial" BackColor="#1B1B1B" style="margin-top: 0px" 
                                Font-Size="24px"></asp:Label>
                            <br />
                            <br />

                             <asp:Label ID="Label4" runat="server" BackColor="#1B1B1B" Height="32px" 
                                style="margin-right: 7px" Width="14px"></asp:Label>
                            <asp:DropDownList ID="ddWeeks" runat="server"
                                BackColor="Black" BorderColor="#E4BB18"  AutoPostBack="true"
                                BorderStyle="Solid" Font-Names="Arial" Font-Size="17px" ForeColor="#E4BB18" 
                                Height="32px"  Width="263px"/>

                            <asp:Label ID="Label19" runat="server" BackColor="#1B1B1B" Height="32px" 
                                style="margin-right: 7px" Width="18px"></asp:Label>
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
 <%--                               <asp:Button ID="btn0" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="0" width="19px" 
                                    Font-Bold="False" Height="23px" />     
--%>                            <asp:Button ID="btn1" runat="server" BackColor="Black" BorderStyle="none" 
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
                               <br />

                                <asp:Label ID="lblWeek2" runat="server" BackColor="#1B1B1B" Height="32px" 
                                    style="margin-right: 7px" Width="18px" ForeColor="#E4BB18" 
                                    Font-Size="11px"> Week 
                                </asp:Label>
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
                                    Font-Bold="False" Height="23px"/>                                 
                                <asp:Button ID="btn35" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="35" width="19px" 
                                    Font-Bold="False" Height="23px"/>                                 
                                <asp:Button ID="btn36" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="36" width="19px" 
                                    Font-Bold="False" Height="23px"/>                                 
                                <asp:Button ID="btn37" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="37" width="19px" 
                                    Font-Bold="False" Height="23px"/>                                 
                                <asp:Button ID="btn38" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="38" width="19px" 
                                    Font-Bold="False" Height="23px"/>                                 
                                <asp:Button ID="btn39" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="39" width="19px" 
                                        Font-Bold="False" Height="23px"/>                                 
                                <asp:Button ID="btn40" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="40" width="19px" 
                                        Font-Bold="False" Height="23px"/>                                                            
                              </div>
                            <br />
                            <asp:Label ID="lblCorona" runat="server" visible="false"
                                    Text="League update: 9th September."
                                    BackColor="Red" BorderStyle="Double" Font-Size="Large" 
                                    ForeColor="White"  Font-Bold="False" style="text-align: left;"
                                    Height="29px" Width="433px" ></asp:Label>
                            <br />
                            <asp:Label ID="lblBunkerBoys" runat="server" visible="false"
                                    Text="Following the passing of the Queen, the committee have decided that any team wishing to postpone today's games as a mark of respect can do so, provided they inform their opposition and the pub in which they were due to play. The same will also apply on the day(s) of national mourning, once they are announced."
                                    BackColor="Red" BorderStyle="Double" Font-Size="Large" 
                                    ForeColor="White"  Font-Bold="False" style="text-align: left;"
                                    Height="147px" Width="433px" ></asp:Label>

                            <br />
                            <br />
                            <asp:GridView ID="gridResults" runat="server" GridLines="None" CssClass="gv"
                                Height="136px"  BackColor="#1B1B1B" 
                                BorderStyle="None" AutoGenerateColumns="False" CellSpacing="3" 
                                Font-Names="Arial" Font-Size="12px">
                                <Columns>
                                    <asp:BoundField DataField="League" HeaderText="League/Cup">
                                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" />
                                    <ItemStyle ForeColor="LightGreen" HorizontalAlign="Left" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Fixture Date" HeaderText="">
                                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                    <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Home Team Name" HeaderText="Home Team" >
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Right" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BorderColor = "#1B1B1B" BackColor="#1B1B1B"  HorizontalAlign="Right" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>  

                                    <asp:BoundField DataField="Home Result"  HeaderText="Result">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False"/>
                                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Away Team Name" HeaderText="Away Team">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B" BorderColor = "#1B1B1B"  HorizontalAlign="left" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>   

                                    <asp:BoundField DataField="Home Rolls Result"  HeaderText="Rolls">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False"/>
                                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Fixture ID" visible="true">
                                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fixture ID2">
                                    <ItemStyle ForeColor="#1B1B1B" HorizontalAlign="Right" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" visible="false">
                                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Week" visible="False">
                                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Venue" HeaderText="Venue">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B" BorderColor = "#1B1B1B"  HorizontalAlign="left" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>   

                                </Columns>
                            </asp:GridView>


                           <asp:button ID="btnClose" runat="server" Text="Hide Card" BackColor="Red" Width="163px"  Font-Size="20px"/>

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
                                    <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                                    </asp:BoundField>
                                 </Columns>
                           </asp:GridView>
                           <br />

                            <asp:Label ID="lblPlayerStats" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                                    style="text-align:center" Font-Size="18px" BorderWidth = "2px"
                                BackColor="#1B1B1B" Height="75px" Width="400px">Click on a Player to see her Stats for the Season. If any of the names are wrong, leave a message on the league WhatsApp group.
                            </asp:Label>       
                           <br />
                           <br />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <br />
                    <br />
   

                </td>

            </tr>


            <tr>
                  <td bgcolor="#1b1b1b" valign="top">
                   <asp:Label ID="lblComps" runat="server" ForeColor="#E4BB18" 
                        Text="Competitions" BorderWidth="1px"
                        Width="185px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="20px"></asp:Label>
                    <br />
                    <br />
                    <br />
                     <asp:Label ID="lblAllform" runat="server" BackColor="#1B1B1B" 
                          Font-Names="Arial" Font-Underline = "True"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:Label ID="lblAllformDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:HyperLink ID="hlAllform" runat="server" Font-Bold="false" ForeColor="white"  visible="false"
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblHolmeTowers" runat="server" BackColor="#1B1B1B" 
                          Font-Names="Arial" Font-Underline = "True"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:Label ID="lblHolmeTowersDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:HyperLink ID="hlHolmeTowers" runat="server" Font-Bold="false" ForeColor="white"  
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    
                    <br />
                    <br />
                    <br />
                   <asp:Label ID="lblAlanRosser" runat="server" BackColor="#1B1B1B" 
                          Font-Names="Arial" Font-Underline = "True" visible="false"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:Label ID="lblAlanRosserDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:HyperLink ID="hlAlanRosser" runat="server" Font-Bold="false" ForeColor="white"  visible="false"
                        ToolTip="Click to see playoff draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    
                    <br />
                    <br />
                    <br />
                      <asp:Label ID="lblGaryMitchell" runat="server" BackColor="#1B1B1B" visible="false"
                          Font-Names="Arial" Font-Underline = "True"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:Label ID="lblGaryMitchellDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"  visible="false"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px"></asp:Label>
                    <br />
                    <asp:HyperLink ID="hlGaryMitchell" runat="server" Font-Bold="false" ForeColor="white"  visible="false"
                        ToolTip="Click to see draw" style="text-decoration:underline"
                        Font-Names="Arial" Height="19px"> </asp:HyperLink>                    

                    <br />
                    <br />
                    <br />
                      <%--                  <asp:Label ID="lblJubilee" runat="server" BackColor="#1B1B1B" 
                          Font-Names="Arial" Font-Underline = "True"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="Jubilee Cup (Best Averages)"></asp:Label>
                    <br />
                    <asp:Label ID="lblJubileeDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="To be played at the King Billy on Tuesday 28th May - 8.00pm"></asp:Label>

                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblSecretaries" runat="server" BackColor="#1B1B1B" 
                          Font-Names="Arial" Font-Underline = "True"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="Secretaries Cup"></asp:Label>
                    <br />
                    <asp:Label ID="lblSecratariesDate" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="To be played at the Liberals on Wednesday 29th May - 7.30pm"></asp:Label>


                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblChampions" runat="server" BackColor="#1B1B1B" 
                          Font-Names="Arial" Font-Underline = "True"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" Text="Champion of Champions Cup"></asp:Label>
                    <br />
                    <asp:Label ID="lblChampions1" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" 
                        Text="1st:  Park Rangers">
                    </asp:Label>
                    <br />
                    <asp:Label ID="Label5" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" 
                        Text="2nd: 49ers">
                    </asp:Label>
                    <br />
                    <asp:Label ID="Label8" runat="server" BackColor="#1B1B1B" Font-Names="Arial"
                        ForeColor="#E4BB18" Font-Size="16px" Width = "300px" 
                        Text="3rd:  Park Lads">
                    </asp:Label>

                    <br />
                    <br />
                    <br />
                    <br />
--%>
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
                        ForeColor="#E4BB18" NavigateUrl="~/Ladies_Skit/Honours.aspx" Height="26px" 
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

<%--                <td bgcolor="#1b1b1b" height="240px" valign="top">
                    <asp:HyperLink ID="HyperLinkCD" runat="server" Font-Names="Arial" 
                        ForeColor="#E4BB18" NavigateUrl="~/Ladies_Skit/Delegates.aspx" BackColor="#1B1B1B" 
                        Font-Size="22px" BorderWidth="1px"> yyyy/yy Delegates </asp:HyperLink>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                
                   <br />
               </td>
--%>           </tr>
            
        </table>
    </div>
    <br />

     <div class="clearboth">
     </div>

    </asp:Content>
