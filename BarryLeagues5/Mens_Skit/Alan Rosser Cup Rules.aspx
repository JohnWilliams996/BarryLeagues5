<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Alan Rosser Cup Rules.aspx.vb" Inherits="League_Tables" %>

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
        .style8
        {
            width: 954px;
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
        <td class="style8">
            <asp:Label ID="Label0" runat="server" Font-Names="Arial" 
                ForeColor="Yellow"  style="text-align:left" Font-Size="26px" 
                BackColor="#1B1B1B">ALAN ROSSER CUP RULES </asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtRules" runat="server" Font-Names="Arial" 
                TextMode="MultiLine" Height="357px" 
                Width="877px" BackColor="Black" ForeColor="White" Font-Size="Medium" 
                style="margin-top: 0px">
            1. Coin toss to determine who rolls. Coin toss winner responsible for completing scorecard.

            2. Normal league skittles format applies, including league spare player rule.

            3. Scorecards to be completed and submitted to secretary as per league rules.

            4. Sticker up will be provided by venue for all games, teams pay £8 per team as per normal cup rules.

            5. Top 2 teams from each group will qualify for knockout stages of Alan Rosser Memorial Cup.

            6. Remaining teams from each group will qualify for knockout stages of Gary Mitchell Memorial Cup.

            7. Preliminary round of Gary Mitchell Memorial Cup will compromise of 5th placed teams in Groups F,G & H together
                with the 3 lowest scoring 4th placed teams.

            8. Positions determined by points, rolls, pins, head to head.
            </asp:TextBox>
            <br />
            <br />


        </td>
        
        
    </tr>


    </table>
        

</asp:Content>
