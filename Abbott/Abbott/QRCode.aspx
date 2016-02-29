<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCode.aspx.cs" Inherits="Abbott.QRCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <title></title>
    <script type="text/javascript">
        function checkinput() {
            var pcode = $("#pCode").val();
            if (pcode == "" || pcode == null || pcode == "null") {
                alert("请输入工号");
                return;
            }
        }

        //$(function () {
        //    $("#pCode").blur(function () {
        //        var data = "{strWhere:'" + $("#pCode").val() + "'}";
        //        $.ajax({
        //            type: "post",
        //            contentType: "application/json",
        //            dataType: 'json',
        //            data: data,
        //            url: "../WebService1.asmx/GetList",
        //            async: false,
        //            success: function (r) {
        //                $("#hospital").html("");
        //                var data = r.d;
        //                if (data == null) {
        //                    $("#hospital").append("<option value='0'>无医院信息</option>");
        //                } else {
        //                    $.each(data, function (i, e) {
        //                        $("#hospital").append("<option value='" + e.Hospital_Code + "'>" + e.HospitalName + "</option>");
        //                    });

        //                }
        //            }
        //        });

        //    });
        //});
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            请输入工号:<input type="text" id="pCode" style="width: 200px;" runat="server" />&nbsp;&nbsp;
            <asp:Button runat="server" Text="下载二维码" ID="download" OnClick="download_Click" OnClientClick="checkinput()" />
            <%--  <br />
            <br />
            &nbsp;医院名称:
            <select id="hospital" style="width: 196px;">
            </select>--%>
        </div>
    </form>
</body>
</html>
