<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Fixture Result AR.aspx.vb" Inherits="Admin_Fixture_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
       <div id="divFixtureTable">
        <asp:Label ID="lblTableBefore" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                style="text-align:center" Width="308px" Font-Size="18px" 
            BackColor="#1B1B1B" Height="17px">LEAGUE TABLE BEFORE RESULT      
        </asp:Label>
        <asp:GridView ID="gridTable" runat="server" GridLines="None" 
            style="margin-top: 0px" Height="136px"
            AutoGenerateColumns="False"
            Font-Names="Arial" Font-Size="14px" CellPadding="3" BackColor="#1B1B1B">
            <Columns>
                <asp:BoundField DataField="Pos" HeaderText="Pos">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="#FFC000" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Team"  ShowHeader="True"   HeaderText="Team" 
                    HtmlEncode="False">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" /> 
                </asp:BoundField>
                <asp:BoundField DataField="Pld" HeaderText="Pld">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="W" HeaderText="W">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt" />
                </asp:BoundField>               
                <asp:BoundField DataField="D"  HeaderText="D">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>               
                <asp:BoundField DataField="L"  headerText="L">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
                 <asp:BoundField DataField="Pts" SortExpression="Pts" HeaderText="Pts" >
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Right" />
                </asp:BoundField>
               <asp:BoundField DataField="Rolls"  headerText="Rolls">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
               <asp:BoundField DataField="Pins"  headerText="Pins">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
 
                <asp:BoundField />

            </Columns>

       </asp:GridView>
       <br />
       <asp:Button ID="btnOK" runat="server" Text="OK" BackColor="Black" 
               ForeColor="White"  Width="124px" Height="37px" />           
       <br />
       <br />    
        <asp:Label ID="lblTableAfter" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                style="text-align:center" Width="299px" Font-Size="18px" 
            BackColor="#1B1B1B" Height="17px">LEAGUE TABLE AFTER RESULT      
        </asp:Label>
        <asp:GridView ID="gridTable2" runat="server" GridLines="None" 
            style="margin-top: 0px" Height="136px"
            AutoGenerateColumns="False"
            Font-Names="Arial" Font-Size="14px" CellPadding="3" BackColor="#1B1B1B">
            <Columns>
                <asp:BoundField DataField="Pos" HeaderText="Pos">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="#FFC000" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Team"  ShowHeader="True"   HeaderText="Team" 
                    HtmlEncode="False">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" /> 
                </asp:BoundField>
                <asp:BoundField DataField="Pld" HeaderText="Pld">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="W" HeaderText="W">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt" />
                </asp:BoundField>               
                <asp:BoundField DataField="D"  HeaderText="D">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>               
                <asp:BoundField DataField="L"  headerText="L">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
                <asp:BoundField DataField="Pts" SortExpression="Pts" HeaderText="Pts" >
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Rolls" SortExpression="Rolls" 
                    HeaderText="Rolls">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="20px" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
               <asp:BoundField DataField="Pins"  headerText="Pins">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>

                <asp:BoundField HeaderText="Move">
                <HeaderStyle ForeColor="DarkKhaki" />
                <ItemStyle ForeColor="DarkKhaki" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                </asp:BoundField>

            </Columns>


       </asp:GridView>

    </div>

               

        <br />

       <br />
       <br />

                 <asp:Label ID="lblFixture" runat="server" ForeColor="Cyan" 
                    Text="Team1 v Team2" BorderWidth="0px" 
                    width="549px" Font-Names="Arial" Font-Bold="True"></asp:Label>

       <br />
       <asp:Button ID="btnBack" runat="server" Text="&lt;Back" BackColor="Black" 
               ForeColor="White" Height="37px" />           
       

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<div id = "Admin">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate> 
     
    <table >
        <tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" ForeColor="#E4BB18" Text="Result" BorderWidth="0px" Font-Names="Arial"></asp:Label>
                <asp:RadioButtonList ID="rbResults" runat="server" Font-Size="14px" 
                    ForeColor="Red" Width="90px" BackColor="#333333" Height="22px" 
                    AutoPostBack="False">
                </asp:RadioButtonList>
            </td>
               
            <td valign="bottom">
                <asp:Label ID="Label12" runat="server" width="115px" Text="" Font-Names="Arial" > </asp:Label>
                <asp:Label ID="Label13" runat="server" ForeColor="#E4BB18" Text="Rolls" BorderWidth="0px" Font-Names="Arial"></asp:Label>
                <br />
                <asp:Button ID="btn20" runat="server" Text="20" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn21" runat="server" Text="21" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn22" runat="server" Text="22" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn23" runat="server" Text="23" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn24" runat="server" Text="24" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn25" runat="server" Text="25" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn26" runat="server" Text="26" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn27" runat="server" Text="27" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn28" runat="server" Text="28" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn29" runat="server" Text="29" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <br />
                <asp:Button ID="btn30" runat="server" Text="30" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn31" runat="server" Text="31" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn32" runat="server" Text="32" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn33" runat="server" Text="33" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn34" runat="server" Text="34" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn35" runat="server" Text="35" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn36" runat="server" Text="36" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn37" runat="server" Text="37" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn38" runat="server" Text="38" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn39" runat="server" Text="39" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <br />
                <asp:Button ID="btn40" runat="server" Text="40" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn41" runat="server" Text="41" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn42" runat="server" Text="42" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn43" runat="server" Text="43" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn44" runat="server" Text="44" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn45" runat="server" Text="45" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn46" runat="server" Text="46" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn47" runat="server" Text="47" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn48" runat="server" Text="48" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn49" runat="server" Text="49" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <br />
                <asp:Button ID="btn50" runat="server" Text="50" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn51" runat="server" Text="51" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn52" runat="server" Text="52" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn53" runat="server" Text="53" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn54" runat="server" Text="54" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn55" runat="server" Text="55" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn56" runat="server" Text="56" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn57" runat="server" Text="57" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn58" runat="server" Text="58" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn59" runat="server" Text="59" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <br />
                <asp:Button ID="btn60" runat="server" Text="60" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn61" runat="server" Text="61" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn62" runat="server" Text="62" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn63" runat="server" Text="63" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn64" runat="server" Text="64" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn65" runat="server" Text="65" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn66" runat="server" Text="66" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn67" runat="server" Text="67" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn68" runat="server" Text="68" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn69" runat="server" Text="69" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <br />
                <asp:Button ID="btn70" runat="server" Text="70" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn71" runat="server" Text="71" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn72" runat="server" Text="72" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn73" runat="server" Text="73" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn74" runat="server" Text="74" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn75" runat="server" Text="75" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn76" runat="server" Text="76" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn77" runat="server" Text="77" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn78" runat="server" Text="78" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <asp:Button ID="btn79" runat="server" Text="79" BackColor="Green" ForeColor="White" width="24px" Height="36px" Font-Size="14px" BorderWidth="1px" visible="true"/>
                <br />
            </td>
        
           <td align="right"valign="bottom">
                 <asp:Label ID="Label3" runat="server" ForeColor="#E4BB18" Text="Home" Font-Names="Arial" Font-Size="12px" ></asp:Label><br />
                 <asp:TextBox ID="txtHomeRoll1" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />
                 <asp:TextBox ID="txtHomeRoll2" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />   
                 <asp:TextBox ID="txtHomeRoll3" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />   
                 <asp:TextBox ID="txtHomeRoll4" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />   
                 <asp:TextBox ID="txtHomeRoll5" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br /> 
                 <asp:TextBox ID="txtHomeRollsPoints" runat="server" Width="24px" Font-Size="14px" BackColor="Blue" ForeColor="White"></asp:TextBox><br />   
                 <asp:TextBox ID="txtHomeTotal" runat="server" Width="24px" Font-Size="14px" BackColor="Blue" ForeColor="White"></asp:TextBox><br />   
           </td>
           <td align="center"valign="bottom">
                 <asp:Label ID="Label41" runat="server" ForeColor="#E4BB18" Text="Roll" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
                 <asp:Label ID="Label11" runat="server" ForeColor="#E4BB18" Text="1" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
                 <asp:Label ID="Label4"  runat="server" ForeColor="#E4BB18" Text="2" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
                 <asp:Label ID="Label5"  runat="server" ForeColor="#E4BB18" Text="3" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
                 <asp:Label ID="Label6"  runat="server" ForeColor="#E4BB18" Text="4" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
                 <asp:Label ID="Label7"  runat="server" ForeColor="#E4BB18" Text="5" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
                 <asp:Label ID="Label10"  runat="server" ForeColor="#E4BB18" Text="Points" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
                 <asp:Label ID="Label2"  runat="server" ForeColor="#E4BB18" Text="Total" Font-Names="Arial" Font-Size="12px" Height="22px"></asp:Label><br />
            </td>
            <td align="left"valign="bottom">
                 <asp:Label ID="Label9" runat="server" ForeColor="#E4BB18" Text="Away" Font-Names="Arial" Font-Size="12px" ></asp:Label><br />
                 <asp:TextBox ID="txtAwayRoll1" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />
                 <asp:TextBox ID="txtAwayRoll2" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />   
                 <asp:TextBox ID="txtAwayRoll3" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />   
                 <asp:TextBox ID="txtAwayRoll4" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />   
                 <asp:TextBox ID="txtAwayRoll5" runat="server" Width="24px" Font-Size="14px"></asp:TextBox><br />   
                 <asp:TextBox ID="txtAwayRollsPoints" runat="server" Width="24px" Font-Size="14px" BackColor="Blue" ForeColor="White"></asp:TextBox><br />   
                 <asp:TextBox ID="txtAwayTotal" runat="server" Width="24px" Font-Size="14px" BackColor="Blue" ForeColor="White"></asp:TextBox><br />   
            </td>
        </tr>
    </table>
 
 
    </ContentTemplate>
</asp:UpdatePanel>
    <asp:Label ID="Label8" runat="server" width="372px"
        Text="" Font-Names="Arial" > </asp:Label>

    <asp:Button ID="btnUpdateHeader" runat="server" Text="Update Header" Height="38px" 
        Font-Names="Arial" BackColor="Red" 
        ForeColor="White" Width="113px"/>
    <br />
    
    <br />

    

    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="34px" 
    Width="87px" Font-Names="Arial" BackColor="Black" 
    ForeColor="White"/>

</div>
</asp:Content>

