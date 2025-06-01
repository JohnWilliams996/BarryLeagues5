<%@ Page Title="" Language="VB" MasterPageFile="~/All_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Club Fixtures.aspx.vb" Inherits="Club_Fixtures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="btnBack1" runat="server" Text="&lt; Back" BackColor="Black" 
            ForeColor="White" Height="32px" />
    <br />
    <br />
    <asp:Label ID="lblClub" runat="server" BackColor="#1D1D1D" 
            Font-Names="Arial" ForeColor="Cyan" Text="Label" Font-Size="18pt">
    </asp:Label>
    <br />
    <br />
    
    <asp:Label ID="Label17" runat="server" BackColor="#1B1B1B" Font-Names="Arial" 
                        ForeColor="#E4BB18" 
                        Text="Select Venue: "
                        Font-Size="16px">
    </asp:Label>
    <asp:DropDownList ID="ddlVenues" runat="server" BackColor="Black" 
        ForeColor="#E4A519">
    </asp:DropDownList>

    <asp:Button ID="btnGo" runat="server" BackColor="Black" BorderColor="#E4BB18" 
        Font-Names="Arial" Font-Size="15px" ForeColor="#E4BB18" 
        Height="32px" Text="Go" BorderStyle="Solid"  />

                    
    <br />
    <br />

    <asp:GridView ID="gridTeams" runat="server" AutoGenerateColumns="False" 
        Font-Names="Arial" Font-Size="14px" GridLines="Horizontal" BorderWidth="1px" 
        CellSpacing="1" ShowHeader="false"
        bACKColor="Black" BorderColor="Black">
        <Columns>
            <asp:BoundField DataField="Week" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="HiddenDate" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="Various" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K1" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K2" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K3" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K4" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K5" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K6" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K7" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K8" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K9" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

            <asp:BoundField DataField="K10" HeaderText="">
            <HeaderStyle ForeColor="Tan" HorizontalAlign="Center" Wrap="False" Font-Size = "Small" />
            <ItemStyle ForeColor="White" HorizontalAlign="Center" Wrap="False" Font-Size="Small"  />
            </asp:BoundField>

 
         </Columns>

        
    </asp:GridView>

    <br />
    <br />

     <asp:Button ID="btnBack2" runat="server" Text="&lt; Back" BackColor="Black" 
            ForeColor="White" Height="32px" />



</asp:Content>

