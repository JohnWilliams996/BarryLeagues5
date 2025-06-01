<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Add Edit Result.aspx.vb" Inherits="Admin_Add_Edit_Result" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
            <asp:Label ID="lblCurrentHomePlayer" runat="server" ForeColor="LightBlue" BorderWidth="1px" 
                width="247px" Font-Names="Arial" visible="False"></asp:Label>
            <asp:Label ID="lblCurrentAwayPlayer" runat="server" ForeColor="LightBlue" BorderWidth="1px" 
                width="247px" Font-Names="Arial" visible="False"></asp:Label>
            <br />
            <asp:Button ID="btn0" runat="server" Text="0" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn1" runat="server" Text="1" BackColor="Green" ForeColor="White" Width="26px" visible="false" />
            <asp:Button ID="btn2" runat="server" Text="2" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn3" runat="server" Text="3" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn4" runat="server" Text="4" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn5" runat="server" Text="5" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn6" runat="server" Text="6" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn7" runat="server" Text="7" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn8" runat="server" Text="8" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn9" runat="server" Text="9" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn10" runat="server" Text="10" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn11" runat="server" Text="11" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn12" runat="server" Text="12" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn13" runat="server" Text="13" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn14" runat="server" Text="14" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <br />
            <asp:Button ID="btn15" runat="server" Text="15" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn16" runat="server" Text="16" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn17" runat="server" Text="17" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn18" runat="server" Text="18" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn19" runat="server" Text="19" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn20" runat="server" Text="20" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn21" runat="server" Text="21" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn22" runat="server" Text="22" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn23" runat="server" Text="23" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn24" runat="server" Text="24" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn25" runat="server" Text="25" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn26" runat="server" Text="26" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn27" runat="server" Text="27" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn28" runat="server" Text="28" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <asp:Button ID="btn29" runat="server" Text="29" BackColor="Green" ForeColor="White" Width="26px" Height="36px" Font-Size="14px" visible="false"/>
            <br />
            <asp:Button ID="btn30" runat="server" Text="30" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn31" runat="server" Text="31" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn32" runat="server" Text="32" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn33" runat="server" Text="33" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn34" runat="server" Text="34" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn35" runat="server" Text="35" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn36" runat="server" Text="36" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn37" runat="server" Text="37" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn38" runat="server" Text="38" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn39" runat="server" Text="39" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>
            <asp:Button ID="btn40" runat="server" Text="40" BackColor="Green" ForeColor="White" Width="26px" visible="false"/>

           <br />
           <br />
           <asp:Button ID="btnSkittlesAutoScores" runat="server" Text="Skittles Auto-Entry" 
                Visible="false" BackColor="LightBlue" Font-Size="Large" />
           <br />
           <asp:Button ID="btnCribAutoScores" runat="server" Text="Crib Auto-Entry" 
                Visible="false" BackColor="LightBlue" Font-Size="Large" />
            <asp:Button ID="btn5_0" runat="server" Text="5-0" BackColor="Green" ForeColor="White" Width="60px" visible="false"/>
            <asp:Button ID="btn4_1" runat="server" Text="4-1" BackColor="Green" ForeColor="White" Width="60px" visible="false"/>
            <asp:Button ID="btn3_2" runat="server" Text="3-2" BackColor="Green" ForeColor="White" Width="60px" visible="false"/>
            <asp:Button ID="btn2_3" runat="server" Text="2-3" BackColor="Green" ForeColor="White" Width="60px" visible="false"/>
            <asp:Button ID="btn1_4" runat="server" Text="1-4" BackColor="Green" ForeColor="White" Width="60px" visible="false"/>
            <asp:Button ID="btn0_5" runat="server" Text="0-5" BackColor="Green" ForeColor="White" Width="60px" visible="false"/>
           <br />
           <asp:Button ID="btnSnookerAutoScores" runat="server" Text="Snooker Auto-Entry" 
                Visible="false" BackColor="LightBlue" Font-Size="Large" />
            <asp:Button ID="btn1_0" runat="server" Text="1-0" BackColor="Green" ForeColor="White" Width="78px" visible="false"/>
            <asp:Button ID="btn0_1" runat="server" Text="0-1" BackColor="Green" ForeColor="White" Width="78px" visible="false"/>
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
            <asp:Label ID="lblHomePlayersAvailable" Text="Home Players Available" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="180px" Font-Names="Arial" Visible="True" ></asp:Label>
            <br />

            <asp:Label ID="lblHomeAvailPlayer1" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight1" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer2" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight2" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer3" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight3" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer4" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight4" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer5" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight5" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer6" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight6" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer7" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight7" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer8" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight8" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer9" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight9" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer10" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight10" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer11" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight11" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer12" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight12" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer13" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight13" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer14" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight14" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer15" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight15" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer16" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight16" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer17" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight17" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer18" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight18" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer19" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight19" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer20" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight20" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer21" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight21" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer22" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight22" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer23" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight23" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer24" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight24" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer25" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight25" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer26" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight26" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer27" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight27" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer28" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight28" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer29" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight29" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer30" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight30" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />

            <asp:Label ID="lblHomeAvailPlayer31" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight31" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer32" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight32" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer33" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight33" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer34" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight34" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer35" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight35" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer36" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight36" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer37" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight37" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer38" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight38" runat="server" Text="&gt; &gt; " Visible="False" />
            <br />
            <asp:Label ID="lblHomeAvailPlayer39" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <asp:Button ID="btAvailRight39" runat="server" Text="&gt; &gt; " Visible="False" />
        </td>
       <td valign="top">
            <asp:Label ID="lblHomePlayersSelected"
                Text="Home Players Selected" runat="server" ForeColor="#E4BB18" 
                BorderWidth="0px" width="213px" Font-Names="Arial" ></asp:Label>
            <asp:Label ID="lblHomeScore" 
                Text="Score" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="50px" Font-Names="Arial" ></asp:Label>
            <asp:Label ID="lblAwayPlayersSelected" 
                Text="Away Players Selected" runat="server" ForeColor="#E4BB18" 
                BorderWidth="0px" width="177px" Font-Names="Arial" ></asp:Label>
            <asp:Label ID="lblAwayScore" 
                Text="Score" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="50px" Font-Names="Arial" ></asp:Label>
<%--            <asp:Label ID="lblNines" 
                Text="9+'s" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="41px" Font-Names="Arial" ></asp:Label>
--%>            
            <asp:Label ID="lblNines" 
                Text="" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="41px" Font-Names="Arial" ></asp:Label>
            <br />

            <asp:Button ID="btLeft1" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer1" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."  ></asp:Label>
            <asp:TextBox ID="txtHomePoints1" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true" ></asp:TextBox>
            <asp:Label ID="lblAwayPlayer1" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints1" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines1" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight1" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft2" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer2" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints2" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
             <asp:Label ID="lblAwayPlayer2" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints2" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines2" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight2" runat="server" Text="&gt;" />
                <br />
  
            <asp:Button ID="btLeft3" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer3" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints3" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer3" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints3" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines3" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight3" runat="server" Text="&gt;" />
                <br />
  
            <asp:Button ID="btLeft4" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer4" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints4" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer4" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints4" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines4" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight4" runat="server" Text="&gt;" />
                <br />
  
            <asp:Button ID="btLeft5" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer5" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints5" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer5" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints5" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines5" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight5" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft6" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer6" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints6" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer6" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints6" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines6" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight6" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft7" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer7" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints7" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer7" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints7" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines7" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight7" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft8" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer8" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints8" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer8" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints8" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines8" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight8" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft9" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer9" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints9" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer9" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints9" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines9" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight9" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft10" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer10" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints10" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer10" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints10" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines10" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight10" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft11" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer11" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints11" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer11" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints11" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayNines11" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
            <asp:Button ID="btRight11" runat="server" Text="&gt;" />
                <br />

            <asp:Button ID="btLeft12" runat="server" Text="&lt;" />
            <asp:Label ID="lblHomePlayer12" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtHomePoints12" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayPlayer12" runat="server" ForeColor="White" BorderWidth="1px" 
                width="200px" Font-Names="Arial" Text = "."></asp:Label>
            <asp:TextBox ID="txtAwayPoints12" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green" AutoPostBack="true" Font-Bold="true"></asp:TextBox>
           <asp:TextBox ID="txtAwayNines12" runat="server" ForeColor="White" BorderWidth="1px" 
                width="20px" Font-Names="Arial" BackColor="Green"></asp:TextBox>
             <asp:Button ID="btRight12" runat="server" Text="&gt;" />
                <br />
                <br />

            <asp:Label ID="Label10"  runat="server" width="170px"></asp:Label>
            <asp:Label ID="lblHomeTotal"  Text="TOTAL" Font-Bold="True" runat="server" ForeColor="#E4BB18" BorderWidth="1px" 
                width="55px" Font-Names="Arial"></asp:Label>
            <asp:TextBox ID="txtHomePoints0" runat="server" ForeColor="White" BorderWidth="1px" 
                width="22px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:TextBox>
            <asp:TextBox ID="txtAwayPoints0" runat="server" ForeColor="White" BorderWidth="1px" 
                width="22px" Font-Names="Arial" BackColor="Green" Font-Bold="true"></asp:TextBox>
            <asp:Label ID="lblAwayTotal" runat="server" Text="TOTAL" Font-Bold="true" ForeColor="#E4BB18" BorderWidth="1px" 
                width="55px" Font-Names="Arial"></asp:Label>
                <br />
                <br />
            <asp:Label ID="Label11"  runat="server" width="165px"></asp:Label>
            <asp:Label ID="lblSkittlesResults"  Text="POINTS" Font-Bold="True" runat="server" ForeColor="#E4BB18" BorderWidth="1px" 
                width="60px" Font-Names="Arial" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddSkittlesResult" runat="server" Visible="false" BackColor="#333333" ForeColor="Red"></asp:DropDownList>
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
                        Width="125px" Height="50px" Font-Size="Medium" />
        </td>
        <td valign="top">
            <asp:Label ID="lblAwayPlayersAvailable" Text="Away Players Available" runat="server" ForeColor="#E4BB18" BorderWidth="0px" width="200px" Font-Names="Arial" Visible="True" ></asp:Label>
            <br />

            <asp:Button ID="btAvailLeft1" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer1" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft2" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer2" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft3" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer3" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft4" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer4" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft5" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer5" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft6" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer6" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft7" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer7" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft8" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer8" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft9" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer9" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft10" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer10" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft11" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer11" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft12" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer12" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft13" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer13" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft14" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer14" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft15" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer15" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft16" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer16" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft17" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer17" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft18" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer18" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft19" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer19" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft20" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer20" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft21" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer21" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft22" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer22" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft23" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer23" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft24" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer24" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft25" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer25" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft26" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer26" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft27" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer27" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft28" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer28" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft29" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer29" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft30" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer30" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            
            <br />
            <asp:Button ID="btAvailLeft31" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer31" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft32" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer32" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft33" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer33" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft34" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer34" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft35" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer35" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft36" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer36" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft37" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer37" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft38" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer38" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
            <br />
            <asp:Button ID="btAvailLeft39" runat="server" Text="&lt; &lt; " Visible="False" />
            <asp:Label ID="lblAwayAvailPlayer39" runat="server" ForeColor="White" BorderWidth="1px" width="180px" Font-Names="Arial" Visible="False" ></asp:Label>
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

