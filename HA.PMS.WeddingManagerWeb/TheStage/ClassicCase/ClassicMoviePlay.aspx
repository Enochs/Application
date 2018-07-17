<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassicMoviePlay.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.ClassicCase.ClassicMoviePlay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:10px">
    <table style="float:left;">
        <tr>
            <td style="vertical-align: top;">
                <span style="color: brown; font-size: large;">
                    <asp:Literal ID="ltlName" runat="server"></asp:Literal></span>
                <br />
                <video id="videoPlay" controls="controls" autoplay="autoplay" class="projekktor" width="800" height="530">
                    <source src='<%=ViewState["MoviePath"] %>' />
                </video>
            </td>
        </tr>
    </table>
    <div style="overflow:auto;height:600px;width:200px;vertical-align:middle;float:right;text-align:left;">
        <asp:Repeater runat="server" ID="rptMovieList" OnItemCommand="rptMovieList_ItemCommand">
                    <ItemTemplate>
                        <li>
                            <a class="MovePath" href="ClassicMoviePlay.aspx?CaseFileId=<%#Eval("CaseFileId") %>&Action=<%#Request["Action"] %>&CaseID=<%#Request["CaseID"] %>" style="font-size: 8px"><%#Eval("CaseFileName") %></a>
                            <a href='/Files/TheCase/TheCaseMovie/<%#Eval("CaseFileName") %>'>下载</a>
                        </li>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
    </div>
        </div>
</asp:Content>
