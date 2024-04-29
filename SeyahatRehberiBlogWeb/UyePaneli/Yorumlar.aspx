<%@ Page Title="" Language="C#" MasterPageFile="~/UyePaneli/UyePanel.Master" AutoEventWireup="true" CodeBehind="Yorumlar.aspx.cs" Inherits="SeyahatRehberiBlogWeb.UyePaneli.Yorumlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FormTasiyici">
         <div class="FormBaslik">
     <h3>Yorumlar</h3>
 </div>
 <asp:ListView ID="lv_yorumlar" runat="server" OnItemCommand="lv_yorumlar_ItemCommand">
     <LayoutTemplate>
         <table cellpading="0" cellpacing="0" class="tablo">
             <tr>
                 <th style="text-align: center">ID</th>
                 <th>Makale ID</th>
                 <th>İçerik</th>
                 <th>Tarih</th>
                 <th>Durum</th>
                 <th>Seçenekler</th>
             </tr>
             <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
         </table>
     </LayoutTemplate>
     <ItemTemplate>
         <tr>
             <td align="center"><%# Eval("ID") %></td>
             <td><%# Eval("Makale_ID") %></td>
             <td><%# Eval("Icerik") %></td>
             <td><%# Eval("TarihveSaat") %></td>
             <td><%# Eval("Durum") %></td>
             <td>
                 <asp:LinkButton ID="lbtn_sil" runat="server" CssClass="tablobutonsil" CommandArgument='<%# Eval("ID") %>' CommandName="sil">Sil</asp:LinkButton>
                 <asp:LinkButton ID="lbtn_durum" runat="server" CssClass="tablobutondurum " CommandArgument='<%# Eval("ID") %>' CommandName="durum">Durum Değiştir</asp:LinkButton>
             </td>
         </tr>
     </ItemTemplate>
 </asp:ListView>
    </div>
   
</asp:Content>
