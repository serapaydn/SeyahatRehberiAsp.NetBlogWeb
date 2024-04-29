<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPaneli/AdminPanel.Master" AutoEventWireup="true" CodeBehind="KategoriDuzenle.aspx.cs" Inherits="SeyahatRehberiBlogWeb.AdminPaneli.KategoriDuzenle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FormTasiyici">
        <div class="FormBaslik">
            <h3>Kategori Ekle</h3>
        </div>
        <div class="FormIcerik">
            <asp:Panel ID="pnl_basarisizpanel" runat="server" CssClass="basarisizpanel " Visible="false">
                <asp:Label ID="lbl_hatamesaj" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnl_basarilipanel" runat="server" Visible="false" CssClass="basarilipanel">
                <label>Kategori başarıyla eklenmiştir</label>
            </asp:Panel>
            <div class="satir ">
                <label class="FormEtiket" style="font-weight: 900;">Kategori Adı</label>
                <asp:TextBox ID="tb_isim" runat="server" CssClass="metinkutu "></asp:TextBox>
            </div>
            <div class="satir ">
                <label class="FormEtiket" style="font-weight: 900;">Açıklama</label>
                <asp:TextBox ID="tb_aciklama" runat="server" CssClass="metinkutu " TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="satir ">
                <label class="FormEtiket ">Yayın Durumu</label><br />
                <asp:CheckBox ID="cb_durum" runat="server" Text=" Yayınla" />
                <small style="color: dimgray">(Eğer işaretli ise kategori yayınlanır)</small>
            </div>
            <div class="satir ">
                <asp:Button ID="lbtn_kategoriekle" runat="server" CssClass="FormButton" Text="Kategori Ekle" OnClick="lbtn_kategoriekle_Click"/>
            </div>
        </div>

    </div>
</asp:Content>
