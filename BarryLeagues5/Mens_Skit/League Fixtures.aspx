<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="League Fixtures.aspx.vb" Inherits="League_Fixtures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #divLeagues
        {
            width: 184px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



        <asp:Label ID="Label4" Text="Click on a League to view the Fixtures" 
            runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:center" Width="180px" Height="39px" 
            BackColor="#1B1B1B">
        </asp:Label>
        
         <asp:GridView ID="gridLeagues" runat="server" CssClass="gv"
                style="margin-top: 0px" Height="136px"  width="180px" 
            AutoGenerateColumns="False"
            Font-Names="Arial" Font-Size="14px" BackColor="#1B1B1B" 
            CellPadding="4" ShowHeader="False" ForeColor="#333333" 
            GridLines="None">
            <Columns>
                <asp:BoundField DataField="League" HeaderText="">
                    <HeaderStyle ForeColor="" HorizontalAlign="Left" Wrap="False"/>
                    <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Center" Wrap="False"/>
                </asp:BoundField>   
            </Columns>
            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

        </asp:GridView>   

       <br />

    
        <asp:Button ID="btnBack1" runat="server" Text="&lt; Home" BackColor="Black" 
            ForeColor="White" Height="32px" PostBackUrl="~/Mens_Skit/Default.aspx"/>
        <br />
        <br />

        <asp:Label ID="lblLeague" runat="server" BackColor="#1D1D1D" 
            Font-Names="Arial" ForeColor="Cyan" Text="Label" Font-Size="18pt"></asp:Label>      
 
 
        <br />


    <asp:GridView ID="gridResults" runat="server" AutoGenerateColumns="False" 
        Font-Names="Arial" Font-Size="14px" GridLines="None" BorderWidth="1px" 
        CellPadding="1"
        bACKColor="Black" BorderColor="Black">
        <Columns>
            <asp:BoundField DataField="Team" HeaderText="Home Team" SortExpression="Team">
            <HeaderStyle ForeColor="Tan" />
            <ItemStyle ForeColor="cyan" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk1" HeaderText="W1">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk2" HeaderText="W2">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk3" HeaderText="W3">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk4" HeaderText="W4">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk5" HeaderText="W5">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk6" HeaderText="W6">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk7" HeaderText="W7">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk8" HeaderText="W8">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk9" HeaderText="W9">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk10" HeaderText="W10">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk11" HeaderText="W11">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk12" HeaderText="W12">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk13" HeaderText="W13">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk14" HeaderText="W14">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk15" HeaderText="W15">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk16" HeaderText="W16">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk17" HeaderText="W17">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk18" HeaderText="W18">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk19" HeaderText="W19">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk20" HeaderText="W20">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk21" HeaderText="W21">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk22" HeaderText="W22">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk23" HeaderText="W23">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk24" HeaderText="W24">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk25" HeaderText="W25">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk26" HeaderText="W26">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk27" HeaderText="W27">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk28" HeaderText="W28">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk29" HeaderText="W29">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk30" HeaderText="W30">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk31" HeaderText="W31">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk32" HeaderText="W32">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk33" HeaderText="W33">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk34" HeaderText="W34">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk35" HeaderText="W35">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk36" HeaderText="W36">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk37" HeaderText="W37">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk38" HeaderText="W38">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk39" HeaderText="W39">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk40" HeaderText="W40">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Wk41" HeaderText="W41">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>

            <asp:BoundField DataField="Wk42" HeaderText="W42">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "X-Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="True" Font-Size="X-Small" Width="25px" />
            </asp:BoundField>



         </Columns>

        
    </asp:GridView>

    <br />
    <br />

     <asp:Button ID="btnBack2" runat="server" Text="&lt; Home" BackColor="Black" 
            ForeColor="White" Height="32px" PostBackUrl="~/Mens_Skit/Default.aspx" />



</asp:Content>

