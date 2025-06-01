<%@ Page Title="" Language="VB" MasterPageFile="~/All_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="League Tables.aspx.vb" Inherits="League_Tables" %>

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
    <br />
  <div id="divCupList">
      <table>
        <tr>
          <td rowspan="2">
            <asp:Label ID="Label4" Text="Click on a Team to view the Fixtures" 
                runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:center"  Height="20px" Width="254px"
                BackColor="#1B1B1B"></asp:Label>     
            <br />
            <br />
          </td>
          <td>
              <asp:Label ID="lblLeague" runat="server" Font-Names="Arial" ForeColor="Yellow"  Text="LEAGUE"
                     Width="240px" Font-Size="18px" Height="22px" style="text-align:left" BackColor="#1B1B1B">
              </asp:Label>
            <br />
            <br />
          </td>

        </tr>


        <tr>
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
            <asp:GridView ID="gridFixtures1" runat="server" GridLines="None" CssClass="gv"
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

                <asp:BoundField DataField="Home Team Name" HeaderText="Opponents">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False"/>
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                           
                <asp:BoundField DataField="Away Team Name" HeaderText="H/A">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="cENTER" Wrap="False"/>
                    <ItemStyle ForeColor="LightGreen" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>     
                                        
                 </Columns>
              </asp:GridView>
           </td>

            <br />

        </table>
    </div>
        

</asp:Content>
