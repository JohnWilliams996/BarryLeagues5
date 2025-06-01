<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Alan Rosser Cup.aspx.vb" Inherits="League_Tables" %>

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
            <asp:Label ID="Label9" Text="Click on a Competition to view the Draw" 
                runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                    style="text-align:center" Width="190px" Height="58px" 
                BackColor="#1B1B1B">
            </asp:Label>
         
            <asp:GridView ID="gridOptions" runat="server" CssClass="gv"
                    style="margin-top: 0px" Height="136px"   
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="14px" BackColor="#1B1B1B" 
                CellPadding="4" ShowHeader="False" ForeColor="#333333" 
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Comp Name" HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="Cyan" HorizontalAlign="Left" wrap="false"/>
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
            <asp:Label ID="lblARCup" runat="server" Font-Names="Arial" 
                ForeColor="LightBlue"  style="text-align:left" Font-Size="26px" 
                BackColor="#1B1B1B">ALAN ROSSER CUP
            </asp:Label>
            <br />
            <asp:Label ID="lblNoDraw" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                ForeColor="#E4BB18" Text="1st Round draw yet to be made">
            </asp:Label>
            <br />
            <br />
            <asp:GridView ID="gridResults" runat="server" AutoGenerateColumns="False" 
                Font-Names="Arial" Font-Size="14px" GridLines="None" BorderWidth="1px" 
                CellPadding="3"
                bACKColor="#1B1B1B" BorderColor="Black">
                <Columns>
                    <asp:BoundField DataField="Match" HeaderText="Match" SortExpression="Match">
                    <HeaderStyle ForeColor="Tan" />
                    <ItemStyle ForeColor="#00CC66" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Home Team" HeaderText="Home Team" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="#00CC66" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
 
                    <asp:BoundField DataField="Away Team" HeaderText="Away Team" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
    
                    <asp:BoundField DataField="Venue" HeaderText="Venue" SortExpression="Venue">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                 </Columns>
            </asp:GridView>
            <br />
            <br />
            <asp:Label ID="lblGroupATable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP A TABLE
            </asp:Label>
            <br />
            <asp:GridView ID="gridGroupATable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"

                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>

            <br />
            <br />            
            <br />            
            <br />
            <asp:Label ID="lblGroupBTable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP B TABLE
            </asp:Label>
            <br />
            <asp:GridView ID="gridGroupBTable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>

            <br />            
            <br />            
            <br />
            <br />
            <br />
            <asp:Label ID="lblGroupCTable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP C TABLE
            </asp:Label>
            <br />
            <asp:GridView ID="gridGroupCTable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"

                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>

            <br />
            <br />            
            <br />            
            <br />
            <br />
            <asp:Label ID="lblGroupDTable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP D TABLE
            </asp:Label>
            <br />
            <asp:GridView ID="gridGroupDTable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>

            <br />
            <br />
            <br />            
            <br />
            <asp:Label ID="lblGroupETable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP E TABLE
            </asp:Label>
            <br />
            <asp:GridView ID="gridGroupETable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>

            <br />            
            <br />            
            <br />
            <br />
            <br />
            <asp:Label ID="lblGroupFTable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP F TABLE
            </asp:Label>
            <br /> 
            <asp:GridView ID="gridGroupFTable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>

            <br />
            <br />            
            <br />            
            <br />
            <br />
            <asp:Label ID="lblGroupGTable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP G TABLE
            </asp:Label>
            <br />
            <asp:GridView ID="gridGroupGTable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>

            <br />
            <br />            
            <br />            
            <br />
            <asp:Label ID="lblGroupHTable" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">GROUP H TABLE
            </asp:Label>
            <br />
            <asp:GridView ID="gridGroupHTable" runat="server" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
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
              </Columns>
           </asp:GridView>
        </td>
        
        <td>
            <br />
            <br />
            <br />            
            <asp:Label ID="lblGroupAFixtures" Text="GROUP A FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupAFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                    <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>

            <br />
            <br />            
            <asp:Label ID="lblGroupBFixtures" Text="GROUP B FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupBFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                    <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>

            <br />
            <br />            
            <asp:Label ID="lblGroupCFixtures" Text="GROUP C FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupCFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                   <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>

            <br />
            <br />            
            <asp:Label ID="lblGroupDFixtures" Text="GROUP D FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupDFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                   <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>


            <br />
            <br />            
            <asp:Label ID="lblGroupEFixtures" Text="GROUP E FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupEFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                  <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                     <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                   <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>


            <br />
            <br />            
            <asp:Label ID="lblGroupFFixtures" Text="GROUP F FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupFFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                    <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>

            <br />
            <br />            
            <asp:Label ID="lblGroupGFixtures" Text="GROUP G FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupGFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                   <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                     <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                   <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>

            <br />
            <br />            
            <asp:Label ID="lblGroupHFixtures" Text="GROUP H FIXTURES - VENUE: " runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />

            <asp:GridView ID="gridGroupHFixtures" runat="server" BorderColor="Black" GridLines="None" CssClass="gv" CellSpacing="1"
                style="margin-top: 0px"  
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B">
                <Columns>
                    
                  <asp:BoundField DataField="Date" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
              
       
                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team1"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 1">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Right" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle forecolor = "White" backColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>

                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team2"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team 2">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>

                    <asp:BoundField DataField="RollsResult" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PinsResult" HeaderText="Pins">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FixtureID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
               </Columns>
            </asp:GridView>

   
        </td>
    </tr>


    </table>
        

</asp:Content>
