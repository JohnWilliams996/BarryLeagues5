
<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Add Edit Result 2.aspx.vb" Inherits="Admin_Add_Edit_Result_2" %>



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
                    Font-Names="Arial" Font-Size="22px"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
               <asp:Label ID="lblDateLiteral" runat="server" ForeColor="#E4BB18" 
                    Text="Date:" BorderWidth="0px" 
                    Font-Names="Arial"> </asp:Label>
                <asp:Label ID="lblDate" runat="server" ForeColor="#E4BB18" 
                    Text="" BorderWidth="0px" 
                    Font-Names="Arial"> </asp:Label>
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
                     Font-Names="Arial" Font-Size="20px"> </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAwayTeamLiteral" runat="server" ForeColor="#E4BB18" 
                    Text="Away Team" BorderWidth="0px" 
                    width="460px" Font-Names="Arial"> </asp:Label>
            </td>
        </tr>

        <tr>
             <td align="right">
                 <asp:Label ID="lblHomeTeam" runat="server" ForeColor="cyan" 
                    Text="Home Team" BorderWidth="0px" 
                    width="460px" Font-Names="Arial"> </asp:Label>
            </td>


           <td class="style15" align="center">

                <asp:Label ID="lblResult" runat="server" ForeColor="#E4BB18" 
                    Text="Result" BorderWidth="0px"
                     Font-Names="Arial" Width="100px" Font-Size="Larger"></asp:Label>

            </td>
 

            <td align="left" class="style11">

                 <asp:Label ID="lblAwayTeam" runat="server" ForeColor="cyan" 
                    Text="Away Team" BorderWidth="0px" 
                    width="470px" Font-Names="Arial"> </asp:Label>
            </td>
        </tr>


        <tr>
             <td align="right">
                 <asp:Label ID="Label3" runat="server" ForeColor="cyan" 
                    Text="Rolls" BorderWidth="0px" 
                    width="460px" Font-Names="Arial"> </asp:Label>
            </td>


           <td class="style15" align="center">

                <asp:Label ID="lblRolls" runat="server" ForeColor="#E4BB18" 
                    Text="Result" BorderWidth="0px"
                     Font-Names="Arial" Width="100px" Font-Size="Larger"></asp:Label>

            </td>
 

            <td align="left" class="style11">

                 <asp:Label ID="Label15" runat="server" ForeColor="cyan" 
                    Text="Rolls" BorderWidth="0px" 
                    width="470px" Font-Names="Arial"> </asp:Label>
            </td>
        </tr>

        <tr>
            <td align="right">

<%--                <asp:Label ID="lblHomePointsDeducted" runat="server" ForeColor="#E4BB18" 
                    Text="Points Deducted" BorderWidth="0px" 
                     Font-Names="Arial" style="margin-left: 0px"></asp:Label>
                <asp:DropDownList ID="ddHomePointsDeducted" runat="server" BackColor="#333333" 
                    ForeColor="Red">
                </asp:DropDownList>
--%>            </td>
            <td class="style14" align="right">
                <asp:RadioButtonList ID="rbResults" runat="server" Font-Size="14px" 
                    ForeColor="Red" Width="90px" BackColor="#333333" Height="22px" 
                    AutoPostBack="True">
                </asp:RadioButtonList>
            </td>
            <td align="left">

  <%--              <asp:Label ID="lblAwayPointsDeducted" runat="server" ForeColor="#E4BB18" 
                    Text="Points Deducted" BorderWidth="0px" 
                    Font-Names="Arial"></asp:Label>
                <asp:DropDownList ID="ddAwayPointsDeducted" runat="server" BackColor="#333333" 
                    ForeColor="Red">
                </asp:DropDownList>
 --%>           </td>
        </tr>

    </table>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

    <table cellpadding="3" cellspacing="5" style="margin-left: 0px">
    <tr valign="top">
        <td class="style10">
           <asp:Label ID="lblAddNewHomePlayer" Text="Add New Home Player" runat="server" ForeColor="#E4BB18" BorderWidth="0px" Font-Names="Arial" Visible="True" ></asp:Label>        
                <br />
            <asp:TextBox ID="txtAddHomePlayer" runat="server" Width="193px"></asp:TextBox>
            <asp:Button ID="btnAdd1" runat="server" Text="Go" Visible="True" />
                <br />
            <asp:Label ID="lblHomeExists" Text="Player Already Exists in Team" runat="server" ForeColor="red" BorderWidth="1px" Font-Names="Arial" Visible="False" ></asp:Label>        
        </td>

        <td >
           <asp:Button ID="btnAutoScores" runat="server" Text="Auto-Entry" 
                Visible="false" BackColor="LightBlue" Font-Size="Large" Width="150px" />
            <asp:Button ID="btnRandom" runat="server" BackColor="LightBlue" 
                Font-Size="Large" Text="Randomize Scores" Visible="true" Width="156px" 
                Height="35px" AccessKey="R" />
                <br />
        </td>
        <td>
            <asp:Label ID="lblAddNewAwayPlayer" Text="Add New Away Player" runat="server" ForeColor="#E4BB18" BorderWidth="0px" Font-Names="Arial" Visible="True" ></asp:Label>        
            <br />
            <asp:TextBox ID="txtAddAwayPlayer" runat="server" Width="193px"></asp:TextBox>
            <asp:Button ID="btnAdd2" runat="server" Text="Go" Visible="True" />
            <br />
            <asp:Label ID="lblAwayExists" Text="Player Already Exists in Team" runat="server" ForeColor="red" BorderWidth="1px" Font-Names="Arial" Visible="False" ></asp:Label>        
        </td>
    </tr>
    <tr >
        <td><asp:Label ID="Label13" runat="server" width="200px"></asp:Label></td>
        <td align="center">
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
        </td>
        <td><asp:Label ID="Label14" runat="server" width="200px"></asp:Label></td>
    </tr>
        <caption>
            <tr>
                <td colspan="3" valign="top">

                    <asp:GridView ID="gridResult" runat="server" AutoGenerateColumns="False" 
                        BackColor="black" CellPadding="3" EnableViewState="true" ForeColor="White" 
                        GridLines="Horizontal" Height="0px">
                        <Columns>
                            <asp:TemplateField headerStyle-Font-Bold="false" 
                                HeaderStyle-ForeColor="#E4BB18" HeaderText="Available">
                                <ItemTemplate>
                                    <asp:Button ID="HomePlayerAvailable" runat="server" BackColor="LightGray" 
                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                        CommandName="HomePlayerAvailable" Font-Size="Medium" Width="160px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField headerStyle-Font-Bold="false" 
                                HeaderStyle-ForeColor="#E4BB18" HeaderText="Selected">
                                <ItemTemplate>
                                    <asp:Button ID="HomePlayerSelected" runat="server" BackColor="DarkCyan" 
                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                        CommandName="HomePlayerSelected" Font-Size="Medium" ForeColor="White" 
                                        Width="160px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField headerStyle-Font-Bold="false" 
                                HeaderStyle-ForeColor="#E4BB18" HeaderText="Score" ShowHeader="True">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHomeRollTotal" runat="server" autocomplete="off" style="text-align: center;"
                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                        CommandName="txtHomeRollTotal" Font-Size="Medium" ForeColor="White" 
                                        AutoPostBack="false" BackColor="Black"
                                        HorizontalAlign="Left" MaxLength="2" 
                                        Width="50px" Wrap="False"> 
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <asp:BoundField DataField="Match" HeaderText="Match" ShowHeader="True">
                            <HeaderStyle Font-Bold="false" ForeColor="#E4BB18" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="True" ForeColor="Gold" HorizontalAlign="Center" 
                                Wrap="False" />
                            </asp:BoundField>
                              <asp:TemplateField headerStyle-Font-Bold="false" 
                                HeaderStyle-ForeColor="#E4BB18" HeaderText="Score" ShowHeader="True">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAwayRollTotal" runat="server" autocomplete="off" style="text-align: center;"
                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                        CommandName="txtAwayRollTotal" Font-Size="Medium" ForeColor="White" 
                                        AutoPostBack="false" BackColor="Black" 
                                        HorizontalAlign="Left" MaxLength="2" 
                                        Width="50px" Wrap="False"> 
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField headerStyle-Font-Bold="false" 
                                HeaderStyle-ForeColor="#E4BB18" HeaderText="Selected">
                                <ItemTemplate>
                                    <asp:Button ID="AwayPlayerSelected" runat="server" BackColor="DarkCyan" 
                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                        CommandName="AwayPlayerSelected" Font-Size="Medium" ForeColor="White" 
                                        Width="160px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField headerStyle-Font-Bold="false" 
                                HeaderStyle-ForeColor="#E4BB18" HeaderText="Available">
                                <ItemTemplate>
                                    <asp:Button ID="AwayPlayerAvailable" runat="server" BackColor="LightGray" 
                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                        CommandName="AwayPlayerAvailable" Font-Size="Medium" Width="160px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                    <asp:Label ID="Label5" runat="server" width="360px"></asp:Label>
                    <asp:Label ID="lblHomePoints" runat="server" BackColor="orange" 
                        ForeColor="Black" style="text-align: center;" Text="0" Width="28px"></asp:Label>
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" 
                        backcolor="Black" ForeColor="#E4BB18" Text="POINTS" width="60px"></asp:Label>
                    <asp:Label ID="lblAwayPoints" runat="server" BackColor="orange" 
                        ForeColor="Black" style="text-align: center;" Text="0" Width="28px"></asp:Label>
                    <br />
                    <asp:Label ID="Label7" runat="server" width="360px"></asp:Label>
                    <asp:Label ID="lblHomeRolls" runat="server" BackColor="orange" 
                        ForeColor="Black" style="text-align: center;" Text="0" Width="28px"></asp:Label>
                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" 
                        backcolor="Black" ForeColor="#E4BB18" style="text-align: center;" Text="ROLLS" width="60px"></asp:Label>
                    <asp:Label ID="lblAwayRolls" runat="server" BackColor="orange" 
                        ForeColor="Black" style="text-align: center;" Text="0" Width="28px"></asp:Label>
                    <br />
                    <asp:Label ID="Label1" runat="server" width="360px"></asp:Label>
                    <asp:Label ID="lblHomeTotal" runat="server" BackColor="orange" 
                        ForeColor="Black" style="text-align: center;" Text="000" Width="28px"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" 
                        backcolor="Black" ForeColor="#E4BB18" style="text-align: center;" Text="TOTAL" width="60px"></asp:Label>
                    <asp:Label ID="lblAwayTotal" runat="server" BackColor="orange" 
                        ForeColor="Black" style="text-align: center;" Text="000" Width="28px"></asp:Label>
                   <br />        
                    <asp:Label ID="Label21" runat="server" width="360px"></asp:Label>
                    <asp:Button ID="btnUpdate" runat="server" Font-Size="Medium" Height="50px" 
                        Text="Update Result" Width="125px" />
                   <br />
                    <br />
                    <asp:Label ID="Label9" runat="server" width="360px"></asp:Label>
                    <asp:ListBox ID="lstErrors" runat="server" BackColor="Red" 
                    ForeColor="White" Visible="False" Font-Size="Large"></asp:ListBox>
                    <br />
                    <asp:Label ID="Label12" runat="server" width="360px"></asp:Label>
                    <asp:Button ID="btnReset" runat="server" Text="Reset Result" Visible="True" 
                        Width="125px" Height="50px" Font-Size="Medium" />

                        <br />
                        <br />
                    <asp:Label ID="Label11" runat="server" width="360px"></asp:Label>
                        <asp:Button ID="btnReUpdate" runat="server" Height="50px" 
                        Text="Re-Update Result" Width="176px" Font-Size="Medium" Visible="false" />
                        <br />
                        <br />         
                </td>
            </tr>
        </caption>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

