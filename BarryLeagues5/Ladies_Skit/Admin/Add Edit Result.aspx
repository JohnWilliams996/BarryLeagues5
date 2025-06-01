<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Add Edit Result.aspx.vb" Inherits="Admin_Add_Edit_Result" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style8
        {
            width: 335px;
        }
        .style9
        {
            width: 323px;
        }
        .style10
        {
            width: 244px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;z<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table class="style8">
        <tr>
            <td colspan="3" align="center" class="style9">
                <asp:Label ID="lblLeague" runat="server" ForeColor="#E4BB18" 
                    Text="League" BorderWidth="0px" 
                    Font-Names="Arial" Font-Size="22px">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
               <asp:Label ID="lblDateLiteral" runat="server" ForeColor="#E4BB18" 
                    Text="Date:" BorderWidth="0px" 
                    Font-Names="Arial">
                </asp:Label>
                <asp:Label ID="lblDate" runat="server" ForeColor="#E4BB18" 
                    Text="" BorderWidth="0px" 
                    Font-Names="Arial">
                </asp:Label>
             </td>
        </tr>


        <tr>
            <td align="right" >
                <asp:Label ID="lblHomeTeamLiteral" runat="server" ForeColor="#E4BB18" 
                    Text="Home Team" BorderWidth="0px" 
                    width="460px" Font-Names="Arial"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lblID" runat="server" ForeColor="Red" 
                    Text="" BorderWidth="2px"  BorderColor="yellow"
                     Font-Names="Arial" Font-Size="20px">
                </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAwayTeamLiteral" runat="server" ForeColor="#E4BB18" 
                    Text="Away Team" BorderWidth="0px" 
                    width="460px" Font-Names="Arial">
                </asp:Label>
            </td>
        </tr>

        <tr>
             <td align="right">
                 <asp:Label ID="lblHomeTeam" runat="server" ForeColor="cyan" 
                    Text="Home Team" BorderWidth="0px" 
                    width="460px" Font-Names="Arial">
                </asp:Label>
            </td>


           <td class="style15" align="center">

                <asp:Label ID="lblResult" runat="server" ForeColor="#E4BB18" 
                    Text="Result" BorderWidth="0px"
                     Font-Names="Arial" Width="100px" Font-Size="Larger"></asp:Label>

            </td>
 

            <td align="left" class="style11">

                 <asp:Label ID="lblAwayTeam" runat="server" ForeColor="cyan" 
                    Text="Away Team" BorderWidth="0px" 
                    width="470px" Font-Names="Arial">
                </asp:Label>
            </td>
        </tr>


        <tr>
            <td align="right">

                <asp:Label ID="lblHomePointsDeducted" runat="server" ForeColor="#E4BB18" 
                    Text="Points Deducted" BorderWidth="0px" 
                     Font-Names="Arial" style="margin-left: 0px"></asp:Label>
                <asp:DropDownList ID="ddHomePointsDeducted" runat="server" BackColor="#333333" 
                    ForeColor="Red">
                </asp:DropDownList>
            </td>
            <td class="style14" align="right">
                <asp:RadioButtonList ID="rbResults" runat="server" Font-Size="14px" 
                    ForeColor="Red" Width="90px" BackColor="#333333" Height="22px" 
                    AutoPostBack="True">
                </asp:RadioButtonList>
            </td>
            <td align="left">

                <asp:Label ID="lblAwayPointsDeducted" runat="server" ForeColor="#E4BB18" 
                    Text="Points Deducted" BorderWidth="0px" 
                    Font-Names="Arial"></asp:Label>
                <asp:DropDownList ID="ddAwayPointsDeducted" runat="server" BackColor="#333333" 
                    ForeColor="Red">
                </asp:DropDownList>
            </td>
        </tr>

    </table>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <table cellpadding="3" cellspacing="5" style="margin-left: 0px">
    <tr valign="top">
        <td class="style10">
           <asp:Label ID="lblAddNewHomePlayer" Text="Add New Home Player (no comps)" runat="server" ForeColor="#E4BB18" BorderWidth="0px" Font-Names="Arial" Visible="True" ></asp:Label>        
                <br />
            <asp:TextBox ID="txtAddHomePlayer" runat="server" Width="193px"></asp:TextBox>
            <asp:Button ID="btnAdd1" runat="server" Text="Go" Visible="True" />
                <br />
            <asp:Label ID="lblHomeExists" Text="Player Already Exists in Team" runat="server" ForeColor="red" BorderWidth="1px" Font-Names="Arial" Visible="False" ></asp:Label>        
        </td>

        <td >
           <asp:Button ID="btnAutoScores" runat="server" Text="Auto-Entry" 
                Visible="false" BackColor="LightBlue" Font-Size="Large" Width="150px" />
           <br />
            <asp:Button ID="btnCalc" runat="server" BackColor="LightBlue" Font-Size="Large" 
                Text="Manual Calc" Visible="false" Width="150px" />
            <asp:Button ID="btnRandom" runat="server" BackColor="LightBlue" 
                Font-Size="Large" Text="Randomize Scores" Visible="false" Width="168px" />
        </td>
        <td>
            <asp:Label ID="lblAddNewAwayPlayer" Text="Add New Away Player (no comps)" runat="server" ForeColor="#E4BB18" BorderWidth="0px" Font-Names="Arial" Visible="True" ></asp:Label>        
            <br />
            <asp:TextBox ID="txtAddAwayPlayer" runat="server" Width="193px"></asp:TextBox>
            <asp:Button ID="btnAdd2" runat="server" Text="Go" Visible="True" />
            <br />
            <asp:Label ID="lblAwayExists" Text="Player Already Exists in Team" runat="server" ForeColor="red" BorderWidth="1px" Font-Names="Arial" Visible="False" ></asp:Label>        
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:Label ID="lblHomePlayersAvailable" Text="Home Players Available" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="200px" Font-Names="Arial" Visible="True" ></asp:Label>
            <br />

            <asp:Label ID="lblHomeAvailPlayer1" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight1" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer2" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight2" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer3" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight3" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer4" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight4" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer5" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight5" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer6" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight6" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer7" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight7" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer8" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight8" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer9" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight9" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer10" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight10" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer11" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight11" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer12" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight12" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer13" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight13" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer14" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight14" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer15" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight15" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer16" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight16" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer17" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight17" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer18" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight18" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer19" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight19" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer20" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight20" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer21" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight21" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer22" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight22" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer23" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight23" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer24" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight24" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer25" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight25" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer26" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight26" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer27" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight27" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer28" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight28" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer29" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight29" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer30" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight30" runat="server" Text="&gt;" Visible="False" />
            <br />

            <asp:Label ID="lblHomeAvailPlayer31" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight31" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer32" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight32" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer33" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight33" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer34" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight34" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer35" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight35" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer36" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight36" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer37" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight37" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer38" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight38" runat="server" Text="&gt;" Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer39" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight39" runat="server" Text="&gt;" Visible="False" />
        </td>
       <td valign="top">
            <br />
            <asp:Label ID="Label20"  runat="server" width="90px"></asp:Label>
            <asp:Label ID="lblHomeTotal"  Text="HOME TOTALS" Font-Bold="False" runat="server" ForeColor="#E4BB18" BorderWidth="1px" 
                width="172px" Font-Names="Arial"></asp:Label>
            <asp:Label ID="Label44"  runat="server" width="60px"></asp:Label>
            <asp:Label ID="lblAwayTotal"  Text="AWAY TOTALS" Font-Bold="False" runat="server" ForeColor="#E4BB18" BorderWidth="1px" 
                width="172px" Font-Names="Arial"></asp:Label>
            <br />
            <asp:Label ID="Label42"  runat="server" width="90px"></asp:Label>
            <asp:Label ID="lblHomeRollTotal1" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblHomeRollTotal2" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblHomeRollTotal3" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblHomeRollTotal4" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblHomeRollTotal5" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblHomePoints0" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="27px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
            <asp:Label ID="Label43"  runat="server" width="60px"></asp:Label>
            <asp:Label ID="lblAwayRollTotal1" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblAwayRollTotal2" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblAwayRollTotal3" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblAwayRollTotal4" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblAwayRollTotal5" runat="server" ForeColor="Black" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblAwayPoints0" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="27px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>

                <br />
                <br />

            <asp:Button ID="btLeft1" runat="server" Text="<1" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer1" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer1" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight1" runat="server" Text="1>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label1" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls1" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" 
                BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off"  ></asp:TextBox>
            <asp:Label ID="lblHomeRoll1_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll1_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll1_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll1_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll1_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints1" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls1" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll1_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll1_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll1_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll1_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll1_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints1" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />

            <asp:Button ID="btLeft2" runat="server" Text="<2" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer2" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer2" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight2" runat="server" Text="2>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label2" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls2" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll2_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll2_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll2_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll2_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll2_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints2" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls2" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll2_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll2_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll2_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll2_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll2_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints2" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />

            <asp:Button ID="btLeft3" runat="server" Text="<3" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer3" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer3" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight3" runat="server" Text="3>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label3" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls3" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll3_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll3_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll3_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll3_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll3_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints3" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls3" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll3_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll3_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll3_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll3_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll3_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints3" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />


            <asp:Button ID="btLeft4" runat="server" Text="<4" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer4" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer4" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight4" runat="server" Text="4>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label4" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls4" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll4_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll4_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll4_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll4_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll4_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints4" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls4" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll4_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll4_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll4_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll4_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll4_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints4" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />

            <asp:Button ID="btLeft5" runat="server" Text="<5" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer5" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer5" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight5" runat="server" Text="5>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label5" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls5" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll5_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll5_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll5_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll5_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll5_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints5" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls5" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll5_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll5_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll5_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll5_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll5_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints5" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />

            <asp:Button ID="btLeft6" runat="server" Text="<6" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer6" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer6" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight6" runat="server" Text="6>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label6" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls6" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll6_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll6_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll6_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll6_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll6_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints6" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls6" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll6_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll6_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll6_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll6_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll6_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints6" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />

            <asp:Button ID="btLeft7" runat="server" Text="<7" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer7" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer7" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight7" runat="server" Text="7>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label7" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls7" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll7_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll7_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll7_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll7_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll7_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints7" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls7" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll7_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll7_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll7_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll7_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll7_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints7" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />

            <asp:Button ID="btLeft8" runat="server" Text="<8" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer8" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer8" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight8" runat="server" Text="8>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label8" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls8" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll8_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll8_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll8_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll8_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll8_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints8" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls8" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll8_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll8_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll8_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll8_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll8_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints8" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />

            <asp:Button ID="btLeft9" runat="server" Text="<9" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer9" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer9" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight9" runat="server" Text="9>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label9" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls9" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll9_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll9_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll9_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll9_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll9_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints9" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls9" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll9_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll9_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll9_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll9_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll9_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints9" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />


            <asp:Button ID="btLeft10" runat="server" Text="<10" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer10" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer10" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight10" runat="server" Text="10>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label10" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls10" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll10_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll10_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll10_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll10_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll10_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints10" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls10" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll10_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll10_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll10_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll10_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll10_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints10" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />


            <asp:Button ID="btLeft11" runat="server" Text="<11" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer11" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer11" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight11" runat="server" Text="11>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label11" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls11" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll11_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll11_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll11_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll11_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll11_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints11" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls11" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll11_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll11_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll11_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll11_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll11_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints11" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />


            <asp:Button ID="btLeft12" runat="server" Text="<12" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
            <asp:Label ID="lblHomePlayer12" runat="server" ForeColor="White" BorderWidth="1px" 
                width="214px" Font-Names="Arial" Text = "."  ></asp:Label>
             <asp:Label ID="lblAwayPlayer12" runat="server" ForeColor="White" BorderWidth="1px" 
                width="210px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:Button ID="btRight12" runat="server" Text="12>" BackColor="Gold" Width = "50px" Font-Size="Medium" Height="25px"/>
                <br />
            <asp:Label ID="label12" runat="server" Width="40px" />
            <asp:TextBox ID="txtHomeRolls12" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="46px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblHomeRoll12_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll12_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll12_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll12_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomeRoll12_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblHomePoints12" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true" ></asp:Label>
            <asp:TextBox ID="txtAwayRolls12" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="54px" Font-Names="Arial" BackColor="Black" Font-Bold="True" MaxLength="5" autocomplete="off" ></asp:TextBox>
            <asp:Label ID="lblAwayRoll12_1" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll12_2" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll12_3" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll12_4" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayRoll12_5" runat="server" ForeColor="White" BorderWidth="0px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Black" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblAwayPoints12" runat="server" ForeColor="White" BorderWidth="1px" 
                style="text-align:center" width="25px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:Label>
                <br />


                <br />
                <br />
            <asp:Label ID="Label21"  runat="server" width="120px"></asp:Label>
            <asp:Label ID="lblResults"  Text="POINTS" Font-Bold="True" runat="server" ForeColor="#E4BB18" BorderWidth="1px" 
                width="60px" Font-Names="Arial" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddResult" runat="server" Visible="false"
                BackColor="#333333" ForeColor="Red" Font-Size="Large"></asp:DropDownList>
            <asp:Label ID="Label14"  runat="server" width="20px"></asp:Label>
            <asp:Label ID="lblRolls"  Text="ROLLS" Font-Bold="True" runat="server" ForeColor="#E4BB18" BorderWidth="1px" 
                width="60px" Font-Names="Arial" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddHomeRolls" runat="server" Visible="false" BackColor="#333333" ForeColor="Red" Font-Size="Large" disabled="disabled"></asp:DropDownList>
            <asp:DropDownList ID="ddAwayRolls" runat="server" Visible="false" BackColor="#333333" ForeColor="Red" Font-Size="Large" disabled="disabled"></asp:DropDownList>
                <br />
                <br />
            <asp:Button ID="btnUpdate" runat="server" Height="100px" 
                Text="Update Result" Width="137px" Font-Size="Medium" />

                <br />
                      
         
            <br />            <br />
            <asp:Label ID="lblReset" 
                    Text="Click to Reset result to 0-0 on Fixture Result page which will also delete the fixture details." 
                    runat="server" ForeColor="#E4BB18" BorderWidth="0px" Font-Names="Arial" 
                    Height="54px" Width="237px" ></asp:Label>        
                <br />
           <asp:Button ID="btnReset" runat="server" Text="Reset Result" Visible="True" 
                Width="177px" />
        </td>
        <td valign="top">
            <asp:Label ID="lblAwayPlayersAvailable" Text="Away Players Available" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="200px" Font-Names="Arial" Visible="True" ></asp:Label>
            <br />

            <asp:Button ID="btAvailLeft1" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer1" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft2" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer2" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft3" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer3" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft4" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer4" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft5" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer5" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft6" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer6" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft7" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer7" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft8" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer8" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft9" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer9" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft10" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer10" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft11" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer11" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft12" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer12" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft13" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer13" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft14" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer14" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft15" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer15" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft16" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer16" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft17" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer17" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft18" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer18" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft19" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer19" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft20" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer20" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft21" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer21" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft22" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer22" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft23" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer23" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft24" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer24" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft25" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer25" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft26" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer26" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft27" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer27" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft28" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer28" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft29" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer29" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft30" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer30" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            
            <br />
            <asp:Button ID="btAvailLeft31" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer31" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft32" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer32" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft33" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer33" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft34" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer34" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft35" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer35" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft36" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer36" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft37" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer37" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft38" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer38" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft39" runat="server" Text="&lt;" Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer39" runat="server" ForeColor="White" BorderWidth="1px" width="200px" Font-Names="Arial" Visible="False" ></asp:Label>
        </td>
     </tr>
     <tr>
         <td>
         </td>
         <td>
            <asp:Label ID="Label13"  runat="server" width="165px"></asp:Label>
            <asp:ListBox ID="lstErrors" runat="server" BackColor="Red" 
            ForeColor="White" Visible="False" Font-Size="Large"></asp:ListBox>
                <br />
                <br />
            <asp:Label ID="Label15"  runat="server" width="165px"></asp:Label>
                <asp:Button ID="btnReUpdate" runat="server" Height="50px" 
                Text="Re-Update Result" Width="176px" Font-Size="Medium" Visible="false" />
         
         </td>
        <td>
         </td>
     </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

