<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Register Players.aspx.vb" Inherits="Register_Players" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style11
        {
            width: 70px;
        }
        .style12
        {
            width: 66px;
        }
        .style13
        {
            width: 91px;
        }
        .style15
        {
            width: 76px;
        }
        .style16
        {
            width: 106px;
        }
        .style17
        {
            width: 99px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="divFields" style="font-size:small">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblAddUpdateDelete" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Add New Player">
                    </asp:Label>
                </td>
                <td class="style13">
                </td>
                <td class="style17">
                </td>
                <td class="style13">
                </td>
                <td class="style15">
                </td>
                <td class="style12">
                </td>
                <td class="style11">
                </td>
                <td class="style16">
                </td>
                <td>
                    <asp:Label ID="lblChangePlayerName" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Change Player Name">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddLeague" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="League"> </asp:Label>
                    <asp:DropDownList ID="ddAddLeague" runat="server"
                        OnSelectedIndexChanged="ddAddLeague_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style13">
                    <asp:CheckBox ID="chkSingles" runat="server" Text = "Singles"  
                        ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller" />
                </td>
                <td class="style17">
                    <asp:CheckBox ID="chkStillInSingles" runat="server" Text = "Still in Singles"  
                        ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style13">
                    <asp:CheckBox ID="chkContact" runat="server" Text = "Contact"  
                        ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller" />
                </td>
                <td class="style15">
                </td>
                <td class="style12">
                </td>
                <td class="style11">
                </td>
                <td class="style16">
                </td>
               <td>
                    <asp:Label ID="lblChangeLeague" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="League"> </asp:Label>
                    <asp:DropDownList ID="ddChangeLeague" runat="server"
                        OnSelectedIndexChanged="ddChangeLeague_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td>
                    <asp:Label ID="lblAddTeam" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Team&nbsp;&nbsp;&nbsp;"> </asp:Label>
                    <asp:DropDownList ID="ddAddTeam" runat="server"
                        OnSelectedIndexChanged="ddAddTeam_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style13">
                    <asp:CheckBox ID="chkPairs1" runat="server" Text = "Pairs1"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style17">
                    <asp:CheckBox ID="chkPairs2" runat="server" Text = "Pairs2"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style13">
                    <asp:CheckBox ID="chkPairs3" runat="server" Text = "Pairs3"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style15">
                    <asp:CheckBox ID="chkPairs4" runat="server" Text = "Pairs4"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style12">
                    <asp:CheckBox ID="chkPairs5" runat="server" Text = "Pairs5"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style11">
                    <asp:CheckBox ID="chkPairs6" runat="server" Text = "Pairs6"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
               <td class="style16">
                    <asp:CheckBox ID="chkStillInPairs" runat="server" Text = "Still in Pairs"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td>
                    <asp:Label ID="lblChangeTeam" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Team&nbsp;&nbsp;&nbsp;"> </asp:Label>
                    <asp:DropDownList ID="ddChangeTeam" runat="server"
                        OnSelectedIndexChanged="ddChangeTeam_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
          <tr>
                <td> 
                    <asp:Label ID="lblAddPlayer" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Player&nbsp;&nbsp;"> </asp:Label>
                    <asp:TextBox ID="txtAddPlayer" runat="server" Width="126px"></asp:TextBox>
                </td>
                <td class="style13">
                    <asp:CheckBox ID="chk3aSide1" runat="server" Text = "3-a-Side1"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style17">
                    <asp:CheckBox ID="chk3aSide2" runat="server" Text = "3-a-Side2"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style13">
                    <asp:CheckBox ID="chk3aSide3" runat="server" Text = "3-a-Side3"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                <td class="style15">
                    <asp:CheckBox ID="chk3aSide4" runat="server" Text = "3-a-Side4"  ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
                 <td class="style12">
                     &nbsp;</td>
                 <td class="style11">
                </td>
                <td class="style16">
                    <asp:CheckBox ID="chkStillIn3aSide" runat="server" Text = "Still in 3-a-Side"  
                        ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller"/>
                </td>
              <td> 
                    <asp:Label ID="lblChangePlayer" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Player&nbsp;&nbsp;"> </asp:Label>
                     <asp:DropDownList ID="ddChangePlayer" runat="server"
                        OnSelectedIndexChanged="ddChangePlayer_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Phone&nbsp;&nbsp;"> </asp:Label>
                    <asp:TextBox ID="txtPhone" runat="server" Width="126px"></asp:TextBox>
                </td>
                 <td class="style13">
                    <asp:CheckBox ID="chk6aSide1" runat="server" Text = "6-a-Side1 T1"  
                         ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller" 
                         Visible="False"/>
                </td>
                 <td class="style17">
                    <asp:CheckBox ID="chk6aSide2" runat="server" Text = "6-a-Side1 T2"  
                         ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller" 
                         Visible="False"/>
                </td>
                 <td class="style13">
                    <asp:CheckBox ID="chk6aSide3" runat="server" Text = "6-a-Side1 T3"  
                         ForeColor="#E4BB18" BackColor = "#1B1B1B" Font-Size="Smaller" 
                         Visible="False"/>
                </td>
                 <td class="style15">
                </td>
                 <td class="style12">
                </td>
                 <td class="style11">
                </td>
                <td class="style16">
                </td>
               <td> 
                    <asp:Label ID="lblNewName" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="New Name"> </asp:Label>
                    <asp:TextBox ID="txtChangeNewName" runat="server" Width="126px"></asp:TextBox>
                </td>
             </tr>
        
             <tr>
                <td>
                    <asp:Button ID="btnAddPlayer" runat="server" Text="Add Player" 
                        font-Bold="False" Font-Size="Large" Height="47px" Width="187px" />
                </td>
                 <td class="style13">
                </td>
                 <td class="style17">
                </td>
                 <td class="style13">
                </td>
                 <td class="style15">
                </td>
                 <td class="style12">
                </td>
                 <td class="style11">
                </td>
                <td class="style16">
                </td>
                <td>
                    <asp:Button ID="btnChangePlayer" runat="server" Text="Change Name" 
                        font-Bold="False" Font-Size="Large" Height="47px" Width="197px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                 <td class="style13">
                </td>
                 <td class="style17">
                </td>
                 <td class="style13">
                </td>
                 <td class="style15">
                </td>
                 <td class="style12">
                </td>
                 <td class="style11">
                </td>
                <td class="style16">
                </td>
              <td>
                    <asp:Label ID="lblChangeInstr" runat="server" BackColor="#1B1B1B" 
                        Font-Names="Arial" ForeColor="#E4BB18" 
                        Text="Name Changed - Pairs/Doubles/3-a-Side Entries MUST be Changed in vw_entries in the Database" 
                        Font-Bold="True" Font-Size="Medium" Width="200px"></asp:Label>
              </td>
            </tr>
        </table>
        <br />
        <br />
            
        <asp:Label ID="lblRegisteredPlayers" runat="server" BackColor="#1B1B1B" Font-Names="Arial" ForeColor="#E4BB18" Text="Registered Players"></asp:Label>
    </div>


                    <asp:GridView ID="gridCrib1" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceCrib1" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Pairs1" HeaderText="Pairs1" SortExpression="Pairs1">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs2" HeaderText="Pairs2" SortExpression="Pairs2">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs3" HeaderText="Pairs3" SortExpression="Pairs3">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs4" HeaderText="Pairs4" SortExpression="Pairs4">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs5" HeaderText="Pairs5" SortExpression="Pairs5">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs6" HeaderText="Pairs6" SortExpression="Pairs6">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Still_In_Pairs" HeaderText="StillInPairs" SortExpression="StillInPairs">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="DarkOrange" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>

                    <asp:GridView ID="gridSkittles1" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceSkittles1" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="_6aside_team1" HeaderText="6/side T1" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="_6aside_team2" HeaderText="6/side T2" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="_6aside_team3" HeaderText="6/side T3" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>

                    <asp:GridView ID="gridSnooker1" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceSnooker1" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Singles" HeaderText="Singles" SortExpression="Singles">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Pairs1" HeaderText="Pairs1" SortExpression="Pairs1">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                
                               <asp:BoundField DataField="Pairs2" HeaderText="Pairs2" SortExpression="Pairs2">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs3" HeaderText="Pairs3" SortExpression="Pairs3">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs4" HeaderText="Pairs4" SortExpression="Pairs4">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs5" HeaderText="Pairs5" SortExpression="Pairs5">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                              <asp:BoundField DataField="Pairs6" HeaderText="Pairs6" SortExpression="Pairs6">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
  
                               <asp:BoundField DataField="Triples1" HeaderText="3aSide1" SortExpression="3aSide1">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                
                               <asp:BoundField DataField="Triples2" HeaderText="3aSide2" SortExpression="3aSide2">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Triples3" HeaderText="3aSide3" SortExpression="3aSide3">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Triples4" HeaderText="3aSide4" SortExpression="3aSide4">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Still_In_Singles" HeaderText="StillInSingles" SortExpression="StillInSingles">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="DarkOrange" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Still_In_Pairs" HeaderText="StillInPairs" SortExpression="StillInPairs">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="DarkOrange" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Still_In_Triples" HeaderText="StillIn3aSide" SortExpression="StillIn3aSide">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="DarkOrange" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>

                    <asp:GridView ID="gridSnooker2" runat="server" AutoGenerateColumns="False" 
                        Font-Names="Arial" Font-Size="14px" GridLines="None" DataKeyNames="ID" 
                        BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3"  DataSourceID="SqlDataSourceSnooker2" 
                        BackColor="Black" AllowPaging="FALSE" AllowSorting="True">

                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast" Position="Bottom" />  
                        <PagerStyle  ForeColor="Red" />

                        <Columns>
                                <asp:CommandField  ShowEditButton="True"  ControlStyle-ForeColor="Tan" />

                                <asp:BoundField DataField="League" HeaderText="League" SortExpression="League">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="#00CC66" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Singles" HeaderText="Singles" SortExpression="Singles">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Pairs1" HeaderText="Pairs1" SortExpression="Pairs1">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                
                               <asp:BoundField DataField="Pairs2" HeaderText="Pairs2" SortExpression="Pairs2">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs3" HeaderText="Pairs3" SortExpression="Pairs3">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs4" HeaderText="Pairs4" SortExpression="Pairs4">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Pairs5" HeaderText="Pairs5" SortExpression="Pairs5">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
  
                               <asp:BoundField DataField="Pairs6" HeaderText="Pairs6" SortExpression="Pairs6">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
  
                              <asp:BoundField DataField="Triples1" HeaderText="3aSide1" SortExpression="3aSide1">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                
                               <asp:BoundField DataField="Triples2" HeaderText="3aSide2" SortExpression="3aSide2">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Triples3" HeaderText="3aSide3" SortExpression="3aSide3">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Triples4" HeaderText="3aSide4" SortExpression="3aSide4">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="Magenta" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Still_In_Singles" HeaderText="StillInSingles" SortExpression="StillInSingles">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="DarkOrange" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Still_In_Pairs" HeaderText="StillInPairs" SortExpression="StillInPairs">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="DarkOrange" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                               <asp:BoundField DataField="Still_In_Triples" HeaderText="StillIn3aSide" SortExpression="StillIn3aSide">
                                <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" />
                                <ItemStyle ForeColor="DarkOrange" HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID">
                                <HeaderStyle ForeColor="Tan" />
                                <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Tan" />

                        </Columns>
                        <HeaderStyle Font-Size="12px" />
                    </asp:GridView>


   <asp:SqlDataSource ID="SqlDataSourceCrib1" runat="server"         
        SelectCommand="SELECT * FROM clubs.vw_players WHERE league = 'CRIB DIVISION 1' ORDER BY league, team, player"
        DeleteCommand="DELETE FROM clubs.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE clubs.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone, Contact = @Contact, Pairs1 = @Pairs1, Pairs2 = @Pairs2, Pairs3 = @Pairs3, Pairs4 = @Pairs4, Pairs5 = @Pairs5, Pairs6 = @Pairs6, still_in_pairs=@still_in_pairs WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="Pairs1" />
            <asp:Parameter Name="still_in_pairs" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
     </asp:SqlDataSource>

   <asp:SqlDataSource ID="SqlDataSourceSkittles1" runat="server"         
        SelectCommand="SELECT * FROM clubs.vw_players WHERE league = 'SKITTLES DIVISION 1' ORDER BY league, team, player"
        DeleteCommand="DELETE FROM clubs.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE clubs.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone ,Contact = @Contact, _6aside_team1 = @_6aside_team1, _6aside_team2 = @_6aside_team2, _6aside_team3 = @_6aside_team3 WHERE ID = @ID" >
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="_6aside_team1" />
            <asp:Parameter Name="_6aside_team2" />
            <asp:Parameter Name="_6aside_team3" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
   </asp:SqlDataSource>

   
    <asp:SqlDataSource ID="SqlDataSourceSnooker1" runat="server"         
        SelectCommand="SELECT * FROM clubs.vw_players WHERE league = 'SNOOKER DIVISION 1' ORDER BY league, team, player"
        DeleteCommand="DELETE FROM clubs.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE clubs.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone, Contact = @Contact, Singles = @Singles, Pairs1 = @Pairs1, Pairs2 = @Pairs2, Pairs3 = @Pairs3, Pairs4 = @Pairs4, Pairs5 = @Pairs5, Pairs6 = @Pairs6, triples1 = @triples1, triples2 = @triples2, triples3 = @triples3, triples4 = @triples4, still_in_singles = @still_in_singles, still_in_pairs = @still_in_pairs, still_in_triples = @still_in_triples WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="Singles" />
            <asp:Parameter Name="Pairs1" />
            <asp:Parameter Name="Pairs2" />
            <asp:Parameter Name="Pairs3" />
            <asp:Parameter Name="Pairs4" />
            <asp:Parameter Name="Pairs5" />
            <asp:Parameter Name="Pairs6" />
            <asp:Parameter Name="triples1" />
            <asp:Parameter Name="triples2" />
            <asp:Parameter Name="triples3" />
            <asp:Parameter Name="triples4" />
            <asp:Parameter Name="still_in_singles" />
            <asp:Parameter Name="still_in_pairs" />
            <asp:Parameter Name="still_in_triples" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
     </asp:SqlDataSource>

   <asp:SqlDataSource ID="SqlDataSourceSnooker2" runat="server"         
        SelectCommand="SELECT * FROM clubs.vw_players WHERE league = 'SNOOKER DIVISION 2' ORDER BY league, team, player"
        DeleteCommand="DELETE FROM clubs.vw_players WHERE ID = @ID" 
        UpdateCommand="UPDATE clubs.vw_players SET League = @League, Team = @Team,  Player = @Player, Phone = @Phone, Contact = @Contact, Singles = @Singles, Pairs1 = @Pairs1, Pairs2 = @Pairs2, Pairs3 = @Pairs3, Pairs4 = @Pairs4, Pairs5 = @Pairs5, Pairs6 = @Pairs6, triples1 = @triples1, triples2 = @triples2, triples3 = @triples3, triples4 = @triples4, still_in_singles = @still_in_singles, still_in_pairs = @still_in_pairs, still_in_triples = @still_in_triples WHERE ID = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
         <UpdateParameters>
            <asp:Parameter Name="League" />
            <asp:Parameter Name="Team" />
            <asp:Parameter Name="Player" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Contact" />
            <asp:Parameter Name="Singles" />
            <asp:Parameter Name="Pairs1" />
            <asp:Parameter Name="Pairs2" />
            <asp:Parameter Name="Pairs3" />
            <asp:Parameter Name="Pairs4" />
            <asp:Parameter Name="Pairs5" />
            <asp:Parameter Name="Pairs6" />
            <asp:Parameter Name="triples1" />
            <asp:Parameter Name="triples2" />
            <asp:Parameter Name="triples3" />
            <asp:Parameter Name="triples4" />
            <asp:Parameter Name="still_in_singles" />
            <asp:Parameter Name="still_in_pairs" />
            <asp:Parameter Name="still_in_triples" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>



</asp:Content>

