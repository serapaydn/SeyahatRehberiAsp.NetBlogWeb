<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPaneli/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SeyahatRehberiBlogWeb.AdminPaneli.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="yonetimpaneli">
    <div class="panel yoneticisayisi">
        <img src="../assests/siteresimleri/3018587_admin_administrator_ajax_options_permission_icon.png" />
        <p>Admin/Moderatör:</p>
        <asp:Label ID="lblYoneticiSayisi" runat="server"></asp:Label>
    </div>
    <div class="panel makalesayisi">
        <img src="../assests/siteresimleri/7696007_newspaper_news_daily_business_media_icon.png" />
        <p>Makale:</p>
        <asp:Label ID="lblMakaleSayisi" runat="server"></asp:Label>
    </div>
    <div class="panel yorumsayisi">
        <img src="../assests/siteresimleri/8664876_comment_dots_icon.png" />
        <p>Yorum:</p>
        <asp:Label ID="lblYorumSayisi" runat="server"></asp:Label>
    </div>
    <div class="panel uyesayisi">
        <img src="../assests/siteresimleri/4737448_account_user_avatar_profile_icon(1).png" />
        <p>Üye:</p>
        <asp:Label ID="lblUyeSayisi" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>
