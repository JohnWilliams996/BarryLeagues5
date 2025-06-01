<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Cup Fixtures List.aspx.vb" Inherits="Cup_Fixtures_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8{width: 800px;}

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table>
    <tr valign="top">
        <td>
             <asp:Label ID="Label4" Text="Click on a Team/Competition to view the Fixtures" 
                runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                    style="text-align:center" Width="190px" Height="58px" 
                BackColor="#1B1B1B">
            </asp:Label>
        </td>
        <td>
            <asp:Label ID="lblCompName" runat="server" BackColor="#1B1B1B" 
                Font-Names="Arial" ForeColor="#E4BB18" Text="Label" Font-Size="18pt">
            </asp:Label>      
            <br/>
            <asp:Label ID="lblNoDraw" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                ForeColor="#E4BB18" Text="1st Round draw yet to be made">
            </asp:Label>
            <br/>
            <asp:button ID="btnPDF" runat="server" Text="View/Print Cup Draw (PDF)"
                    BackColor="Red"  ForeColor="white" Font-Size="18px" Visible ="true" Width="240px" ></asp:button>
            <br/>
        </td>
    </tr>
    <tr valign="top">
        <td>
            <asp:GridView ID="gridOptions" runat="server" CssClass="gv"
                    style="margin-top: 0px" Height="136px"   
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="14px" BackColor="#1B1B1B" 
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
             <br />
              <br />  
            <asp:Label ID="lblLibs" runat="server" 
                Text="Contact your delegate if you have any issues arranging a match"
                BackColor="Red" BorderStyle="Double" Font-Size="Large" ForeColor="White" style="text-align:center"
                Height="70px" Width="215px"></asp:Label>
        
        </td>
        <td>
            <asp:GridView ID="gridResults_Player" runat="server" AutoGenerateColumns="False" 
                Font-Names="Arial" Font-Size="14px" GridLines="None" BorderWidth="1px" 
                CellPadding="3"
                bACKColor="Black" BorderColor="Black">
                <Columns>
                    <asp:BoundField DataField="MatchNo" HeaderText="Match" SortExpression="MatchNo">
                    <HeaderStyle ForeColor="Tan" />
                    <ItemStyle ForeColor="#00CC66" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Home Player" HeaderText="Home Player" SortExpression="Player">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Home Team" HeaderText="Home Team" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Outcome" HeaderText="Result" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="Green" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
 
                     <asp:BoundField DataField="Away Player" HeaderText="Away Player" SortExpression="Player">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Away Team" HeaderText="Away Team" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Result" HeaderText="" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="Green" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
    
                    <asp:BoundField DataField="HomeDraw" HeaderText="" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="AwayDraw" HeaderText="" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Info" HeaderText="" SortExpression="Team">
                    <HeaderStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="Tan" HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
                 </Columns>
            </asp:GridView>

            <asp:GridView ID="gridResults_Team" runat="server" AutoGenerateColumns="False" 
                Font-Names="Arial" Font-Size="14px" GridLines="None" BorderWidth="1px" 
                CellPadding="3"
                bACKColor="Black" BorderColor="Black">
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

                    <asp:BoundField DataField="Winner" HeaderText="">
                    </asp:BoundField>
 
                    </Columns>
            </asp:GridView>        </td>


<%--        <td>
            <asp:Button ID="btnListView" runat="server" Text="List View  #" Height="30px" 
                Width="120px" Font-Names="Arial" BackColor="tan" Font-Size="Medium"
                ForeColor="Black" UseSubmitBehavior="False" Visible="False"/>                
                &nbsp;
                &nbsp;
            <asp:Button ID="btnDrawView" runat="server" Text="Draw View  }" Height="30px" 
                Width="120px" Font-Names="Arial" BackColor="tan" Font-Size="Medium"
                ForeColor="Black" UseSubmitBehavior="False" Visible="False"/>

            <br />


        </td>
--%>  
    </tr>
</table>

<div id="divResults">
        <br />

</div>



</asp:Content>

