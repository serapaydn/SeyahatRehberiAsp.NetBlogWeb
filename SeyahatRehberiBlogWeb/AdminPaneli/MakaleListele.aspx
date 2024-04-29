<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPaneli/AdminPanel.Master" AutoEventWireup="true" CodeBehind="MakaleListele.aspx.cs" Inherits="SeyahatRehberiBlogWeb.AdminPaneli.MakaleListele" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FormTasiyici">
        <div class="FormBaslik">
            <h3>Makaleler</h3>
        </div>
        <asp:ListView ID="lv_makaleler" runat="server" OnItemCommand ="lv_makaleler_ItemCommand">
            <LayoutTemplate>
                <table cellpadding="0" cellspacing="0" class="tablo">
                    <tr>
                        <th style="text-align: center">ID</th>
                        <th>Resim</th>
                        <th>Başlık</th>
                        <th>Kategori</th>
                        <th>Yazar</th>
                        <th>Görüntüleme Sayısı</th>
                        <th>Durum</th>
                        <th>Seçenekler</th>
                    </tr>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center"><%# Eval("ID") %></td>
                    <td>
                        <img src='../Resimler/MakaleResimleri/<%# Eval("KapakResim") %>' height="40" /></td>
                    <td><%# Eval("Baslik") %></td>
                    <td><%# Eval("Kategori") %></td>
                    <td><%# Eval("Yazar") %></td>
                    <td><%# Eval(" GoruntulemeSayi") %></td>
                    <td><%# Eval("Durum") %></td>
                    <td>
                        <a href='MakaleDuzenle.aspx?makaleID=<%# Eval("ID") %>' class="tablobutonduzenle">Düzenle</a>
                        <asp:LinkButton ID="lbtn_sil" runat="server" CssClass="tablobutonsil" CommandArgument='<%# Eval("ID") %>' CommandName="sil">Sil</asp:LinkButton>
                        <asp:LinkButton ID="lbtn_durum" runat="server" CssClass="tablobutondurum " CommandArgument='<%# Eval("ID") %>' CommandName="durum">Durum Değiştir</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
