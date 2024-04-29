<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminGiris.aspx.cs" Inherits="SeyahatRehberiBlogWeb.AdminPaneli.AdminGiris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Paneli</title>    
    <link href="assets/css/Girisstyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="tasiyici ">
                <div class="baslik">
                    <label>Admin Giriş Paneli</label>
                    <div class="sus"></div>
                    <div class="icerik">
                        <asp:Panel ID="pnl_basarisiz" runat="server" CssClass="basarisizpanel" Visible="false">
                            <asp:Label ID="lbl_mesaj" runat="server"></asp:Label>
                        </asp:Panel>
                    </div>
                    <div class="satir">
                        <label>Kullanıcı Adı</label>
                        <asp:TextBox ID="tb_kullanici" runat="server" CssClass="metinkutu" placeholder="Kullanıcı Adınız"></asp:TextBox><br />
                        <label>Şifre</label>
                        <asp:TextBox ID="tb_sifre" runat="server" CssClass="metinkutu" placeholder="Şifreniz" TextMode="Password"></asp:TextBox>
                        <div class="sus"></div>
                        <asp:Button ID="btn_tikla" runat="server" Text="Giriş Yap" CssClass="tiklabutton" OnClick="btn_tikla_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
