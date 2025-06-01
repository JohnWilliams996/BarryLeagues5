<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Cup Results.aspx.vb" Inherits="Admin_Cup_Results" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 200px;
        }
        .style9
        {
            width: 139px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="divResults">
    
    <table class="style8"  id="tblFields" style="margin-top: 0px;">
        <tr>
            <td valign="top" >
                <asp:ListBox ID="lbComps" runat="server" Height="273px"
                 OnTextChanged="lbComps_TextChanged" AutoPostBack="True" BackColor="Black" 
                    ForeColor="Aqua">
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td valign="top" >
               <asp:Label ID="lblPrelim" Text="Prelim Results to be Updated in .NET Program" runat="server" visible="false"
                Font-Names="Arial" ForeColor="White"  style="text-align:left"   Font-Size="20px" 
                BackColor="Red" BorderColor="Silver" BorderStyle="Solid" BorderWidth="3px"></asp:Label>
            </td>
        </tr>
 
        <tr>
            <td>
                <asp:GridView ID="gridResults" runat="server" AutoGenerateColumns="False" 
                    Font-Names="Arial" Font-Size="14px" GridLines="None" BorderWidth="1px" 
                    CellPadding="3"
                    backColor="Black" BorderColor="Black" style="margin-top: 0px">
                    <Columns>
                        <asp:BoundField DataField="MatchNo" HeaderText="match" SortExpression="MatchNo">
                        <HeaderStyle ForeColor="Tan" />
                        <ItemStyle ForeColor="#00CC66" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Home Player" HeaderText="home_player" SortExpression="Player">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Home Team" HeaderText="Home Team" SortExpression="Team">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundField>

                        <asp:ButtonField ButtonType="Button" CommandName="HomeWin" Text="Home Win" >
                            <ControlStyle  Font-Size="X-Small" />
                        </asp:ButtonField>

                        <asp:ButtonField ButtonType="Button" CommandName="NotPlayed" Text="Not Played" >
                            <ControlStyle  Font-Size="X-Small" />
                        </asp:ButtonField>

                        <asp:ButtonField ButtonType="Button" CommandName="Reset" Text="Reset" >
                            <ControlStyle  Font-Size="X-Small" />
                        </asp:ButtonField>

                        <asp:ButtonField ButtonType="Button" CommandName="AwayWin" Text="Away Win" >
                            <ControlStyle  Font-Size="X-Small" />
                        </asp:ButtonField>

                        <asp:BoundField DataField="Away Player" HeaderText="away_player" SortExpression="Player">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Away Team" HeaderText="Away Team" SortExpression="Team">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Result" HeaderText="" SortExpression="Team">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" width="0"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="HomeDraw" HeaderText="" SortExpression="Team">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" width="0"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="AwayDraw" HeaderText="" SortExpression="Team">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" width="0"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="Info" HeaderText="" SortExpression="Team">
                        <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundField>
 
                       <asp:BoundField DataField="RoundNo" HeaderText="round" SortExpression="RoundNo">
                        <HeaderStyle ForeColor="Tan" />
                        <ItemStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundField>
                        </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
    
</asp:Content>

