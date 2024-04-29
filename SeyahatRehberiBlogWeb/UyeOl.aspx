<%@ Page Title="" Language="C#" MasterPageFile="~/Arayuz.Master" AutoEventWireup="true" CodeBehind="UyeOl.aspx.cs" Inherits="SeyahatRehberiBlogWeb.UyeOl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="UyeFormTasiyici">
        <div class="UyeFormBaslik">
            <h3>ÜYE OL</h3>
        </div>
        <div class="UyeFormIcerik">
            <asp:Panel ID="pnl_basarisiz" runat="server" CssClass="basarisizpanel" Visible="false">
                <asp:Label ID="lbl_hatamesaj" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnl_basarili" runat="server" CssClass="basarilipanel" Visible="false">
                <label>Üyelik başarıyla oluşturulmuştur</label>
            </asp:Panel>    <div class="satir ">
        <label class="UyeFormetiket">İsim</label>
        <asp:TextBox ID="tb_isim" runat="server" CssClass="metinkutu" placeholder="İsminiz"></asp:TextBox>
    </div>
    <div class="satir ">
        <label class="UyeFormetiket">Soyisim</label>
        <asp:TextBox ID="tb_soyisim" runat="server" CssClass="metinkutu" placeholder="Soyisminiz"></asp:TextBox>
    </div>
    <div class="satir ">
        <label class="UyeFormetiket">Kullanıcı Adı</label>
        <asp:TextBox ID="tb_kullanici" runat="server" CssClass="metinkutu" placeholder="Kullanıcı Adınız"></asp:TextBox>
    </div>
    <div class="satir ">
        <label class="UyeFormetiket">E-mail</label>
        <asp:TextBox ID="tb_mail" runat="server" CssClass="metinkutu" placeholder="E-mail"></asp:TextBox>
    </div>
    <div class="satir ">
        <label class="UyeFormetiket">Şifre</label>
        <asp:TextBox ID="tb_sifre" runat="server" CssClass="metinkutu" placeholder="Şifreniz" TextMode="Password"></asp:TextBox>
    </div>
    <asp:CheckBox ID="cb_durum" runat="server"/><small style="color: dimgray">(Eğer işaretli ise üyelik aktif olur)</small>
    <asp:Button ID="btn_tikla" runat="server" Text="Üyelik Oluştur" CssClass="tiklabutton" OnClick="btn_tikla_Click" />

</div>
        </div>
       

</asp:Content>
