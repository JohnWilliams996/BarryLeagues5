<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Honours.aspx.vb" Inherits="Honours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager runat="server" />
<asp:UpdatePanel runat="server">
    <ContentTemplate>


    <div id="divLeagueCupHonours" dir="ltr" runat="server">
        <asp:Label ID="Label1" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
            style="text-align:center" Text="Select Season"  Width="120px"
            BackColor="#1B1B1B" BorderWidth="1px"></asp:Label>
        <br />        
        <br />
        <asp:RadioButtonList ID="rbSeasons" runat="server" AutoPostBack="true" oncheckedchanged="rbSeasons_SelectedIndexChanged" 
                ForeColor="#E4BB18" BackColor="Black" Width="120px">
        </asp:RadioButtonList>
        <br />
        <br />  
        <asp:Label ID="lblLeague" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:left" Text="yyyy/yy League Honours"  Width="213px" 
            BackColor="#1B1B1B"></asp:Label>
        <br />
        <br />
       <asp:GridView ID="gridLeague" runat="server" Font-Size="14px" 
        GridLines="None" Height="0px" AutoGenerateColumns="False" CellPadding="3" 
          Width="400px" Font-Names="Arial" BackColor="#1B1B1B">
        <Columns>
            <asp:BoundField DataField="League" HeaderText="League" ShowHeader="True">
                <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="LightGreen" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Winners" HeaderText="Winners">
                <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="Cyan" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Runners-Up" HeaderText="Runners-Up">
                <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="Cyan" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="High Scores Lit" HeaderText="Highest Rolls">
                <HeaderStyle HorizontalAlign="Center" Wrap="False"  ForeColor="#FF9933" />
                <ItemStyle ForeColor="LightGreen" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="High Scores Player" HeaderText="Player">
                <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="White" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="High Scores Team" HeaderText="Team">
                <HeaderStyle HorizontalAlign="Center" Wrap="False"  ForeColor="#FF9933" />
                <ItemStyle ForeColor="Cyan" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="High Scores Score" HeaderText="Score">
                <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="Red" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>

            <br />        <br />  
    
    <asp:Label ID="lblCup" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:left" Text="yyyy/yy Cup Honours"  Width="186px" 
            BackColor="#1B1B1B"></asp:Label>
    
            <br />        <br />  
    
    <asp:GridView ID="gridCup" runat="server" Font-Size="14px" 
        GridLines="None" Height="0px" AutoGenerateColumns="False" CellPadding="3" 
          Width="400px" Font-Names="Arial" BackColor="#1B1B1B">
        <Columns>
            <asp:BoundField DataField="Cup" HeaderText="Competition" ShowHeader="True">
                <HeaderStyle HorizontalAlign="Center" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="LightGreen" Wrap="False" />
            </asp:BoundField>
             <asp:BoundField DataField="Winners" HeaderText="Winners">
                <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="Cyan" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Runners-Up" HeaderText="Runners-Up">
                <HeaderStyle HorizontalAlign="Center" Wrap="False" ForeColor="#FF9933"  />
                <ItemStyle ForeColor="Cyan" Wrap="False" />
            </asp:BoundField>
        </Columns>

    </asp:GridView>
 </div>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
