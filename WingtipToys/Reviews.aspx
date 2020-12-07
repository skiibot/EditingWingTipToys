<%@ Page Title="Reviews" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="WingtipToys.Reviews" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>&nbsp;</h2>
    <div> 
        <p/> 
        <p/>        
        <strong>
            <asp:Label ID="UserReviewsLabel" runat="server" Text="User Reviews" style="font-size: large"></asp:Label>
        </strong>
    </div>
    <div>
        <p/> 
         <strong>
            <asp:Label ID="LeaveAReviewLabel" runat="server" Text="Leave a review:" style="font-size: medium"></asp:Label>
        </strong>  
        <p/>    
            <asp:TextBox ID="ReviewTextbox" runat="server" Height="90px" Width="1045px"></asp:TextBox>
        <p/>

        <table style="width: 1045px">
        <thead>
          <tr>
            <th> 
                <asp:Label ID="FirstNameLabel" runat="server" Text="First Name:" style="font-size: medium; font-weight: normal;"></asp:Label>
                <asp:TextBox ID="FirstNameTextbox" runat="server" Height="16px"></asp:TextBox>
            </th>
            <th>
                <asp:Label ID="LastNameLabel" runat="server" Text="Last Name:" style="font-size: medium; font-weight: normal;"></asp:Label>
                <asp:TextBox ID="LastNameTextbox" runat="server"></asp:TextBox>

            </th>
            <th>
                <asp:Label ID="EmailAddressLabel" runat="server" Text="Email Address*:" style="font-size: medium; font-weight: normal;"></asp:Label>
                <asp:TextBox ID="EmailAddressTextbox" runat="server"></asp:TextBox>

            </th>
            <th>
                <asp:Label ID="ProductLabel" runat="server" Text="Review Item*:" style="font-size: medium; font-weight: normal;"></asp:Label>
                <asp:DropDownList ID="ProductDropDownList" runat="server"></asp:DropDownList>


            </th>
                
            <th>
                <asp:Button ID="SubmitReview" runat="server" Text="Submit" ItemType="WingtipToys.Models.Product" OnClick="SubmitReview_Click" style="height: 26px" />

            </th>                
          </tr>
        </thead>
        </table>
    </div>
    <div style ="float:right">
    <asp:Label ID="ErrorLabel" runat="server" style="font-weight: normal"></asp:Label>
    </div>
    <div>
        <HR WIDTH="98%" SIZE="3">
        <p/> 
        <p/>        
        <strong>
            <asp:Label ID="OtherReviewLabel" runat="server" Text="Other Reviews" style="font-size: medium"></asp:Label>
        </strong> 
        <p/> 
        <asp:Label ID="SearchLabel" runat="server" Text="Search Reviews" style="font-size: medium"></asp:Label>
        <p class="text-center"/> 
        <table style="width: 1045px">
        <thead>
          <tr>
            <th> 
                <asp:Label ID="SearchFNameLabel" runat="server" Text="First Name:" style="font-size: medium; font-weight: normal;"></asp:Label>
                <asp:DropDownList ID="FNameDropDownList" runat="server"></asp:DropDownList>
            </th>
            <th>
                <asp:Label ID="SearchLNameLabel" runat="server" Text="Last Name:" style="font-size: medium; font-weight: normal;"></asp:Label>
                <asp:DropDownList ID="LNameDropDownList" runat="server"></asp:DropDownList>

            </th>
            <th>
                <asp:Label ID="SearchItem" runat="server" Text="Search Items:" style="font-size: medium; font-weight: normal;"></asp:Label>
                <asp:DropDownList ID="SearchItemDropDownList" runat="server"></asp:DropDownList>
            </th>
                
            <th>
                <asp:Button ID="SearchButton" runat="server" Text="Search" ItemType="WingtipToys.Models.Product" OnClick="SearchReview_Click" />

            </th>                
          </tr>
        </thead>
        </table>

        <asp:Label ID="NoReivewLabel" runat="server" Text=""></asp:Label>
        <asp:GridView ID ="ReviewGridView" runat="server"></asp:GridView>
    </div>
</asp:Content>