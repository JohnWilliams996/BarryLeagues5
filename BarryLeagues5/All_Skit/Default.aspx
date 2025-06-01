<%@ Page Title="" Language="VB" MasterPageFile="~/All_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Default1"  %>



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
        .style10
        {
            height: 24px;
            width: 200px;
            font-family: Arial;
        }
        .style12
        {
            width: 480px;
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
         <table style="margin-right: 5px;" cellpadding="4" cellspacing="4">
            <tr>
                <td  bgcolor="#1b1b1b" class="style12" >
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                        style="text-align:left" Text="Leagues"  Width="175px" 
                        BackColor="#1B1B1B" Height="30px" Font-Size="24px" BorderWidth="1px"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style12" >
                     <asp:GridView ID="gridTables" runat="server" GridLines="None" CssClass="gv"
                            style="margin-top: 0px; margin-right: 1px;" Height="136px" 
                        AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="12px" CellPadding="2" CellSpacing="2" BackColor="#1B1B1B">
                        <Columns>
                            <asp:BoundField DataField="Team" HeaderText="">
                                <ItemStyle BackColor="#1B1B1B" ForeColor="Cyan" HorizontalAlign="Left" wrap="false"/>
                            </asp:BoundField>   
                            <asp:BoundField DataField="Home Night" HeaderText="">
                                <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left" wrap="false"/>
                            </asp:BoundField>   
                            <asp:BoundField DataField="Venue" HeaderText="">
                                <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left" wrap="false"/>
                            </asp:BoundField>   
                        </Columns>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

                    </asp:GridView>           

                </td>
            </tr>
        </table>
    </div>

    <div id="divDefault">

        <table  style="width: 410px; margin-left: 130px;" cellpadding="3" cellspacing="5">

            <tr>
                <td class="style10" bgcolor="#1b1b1b" rowspan="2" valign="top" >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="#E4BB18" 
                                Text="Fixtures by Week" BorderWidth="1px" visible="true"
                                Width="200px" Font-Names="Arial" BackColor="#1B1B1B" Font-Size="24px"></asp:Label>
                            <br />
                            <br />

                            <asp:DropDownList ID="ddWeeks" runat="server"
                                BackColor="Black" BorderColor="#E4BB18"  AutoPostBack="true"
                                BorderStyle="Solid" Font-Names="Arial" Font-Size="17px" ForeColor="#E4BB18" 
                                Height="32px"  Width="263px"/>
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
                                <asp:Button ID="btn41" runat="server" BackColor="Black" BorderStyle="none"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="41" width="19px"
                                    Font-Bold="False" Height="23px"   />     
                                <asp:Button ID="btn42" runat="server" BackColor="Black" BorderStyle="none"
                                    Font-Size="11px" ForeColor="#E4BB18" Text="42" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn43" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="43" width="19px" 
                                    Font-Bold="False" Height="23px" />     
                                <asp:Button ID="btn44" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="44" width="19px" 
                                    Font-Bold="False" Height="23px"/>                                 
                                <asp:Button ID="btn45" runat="server" BackColor="Black" BorderStyle="none" 
                                    Font-Size="11px" ForeColor="#E4BB18" Text="45" width="19px" 
                                    Font-Bold="False" Height="23px"/>                                 
                            </div>
                            <br />
                            <br />
                            <asp:GridView ID="gridResults" runat="server" GridLines="None" CssClass="gv"
                                Height="136px"  BackColor="#1B1B1B" 
                                BorderStyle="None" AutoGenerateColumns="False" CellSpacing="3" 
                                Font-Names="Arial" Font-Size="12px">
                                <Columns>
                                    <asp:BoundField DataField="League" HeaderText="League">
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

                                    <asp:BoundField DataField="Home Result"  HeaderText="Result">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False"/>
                                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

<%--                                    <asp:BoundField DataField="Home Rolls Result"  HeaderText="Rolls">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False"/>
                                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

--%>                                    <asp:BoundField DataField="Away Team Name" HeaderText="Away Team">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B" BorderColor = "#1B1B1B"  HorizontalAlign="left" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>   

                                    <asp:BoundField DataField="Fixture ID" HeaderText = "ID">
                                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundField>

<%--                                    <asp:BoundField DataField="Fixture ID2" visible="true">
                                    <ItemStyle ForeColor="black" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" visible="false">
                                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Week" visible="False">
                                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Wrap="False"  />
                                    </asp:BoundField>

--%>                                <asp:BoundField DataField="Venue" HeaderText="Venue">
                                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False"/>
                                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B" BorderColor = "#1B1B1B"  HorizontalAlign="left" Wrap="False" CssClass="row" BorderWidth="1px"/>
                                    </asp:BoundField>   

                                </Columns>
                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>

            </tr>

         
            
        </table>
    </div>
    <br />


    
     <div class="clearboth">
     </div>
</asp:Content>
