function playvideo() {
    var myVideo = document.getElementById("index");
    var u = navigator.userAgent;
    if (u.indexOf('Android') > -1 || u.indexOf('Linux') > -1) {//安卓手机
        $(".index_video").css("display", "");
    } else if (u.indexOf('iPhone') > -1) {//苹果手机
        // setInterval($(".index_video").css("display", ""), 30000);
    } else if (u.indexOf('Windows Phone') > -1) {//winphone手机
        $(".index_video").css("display", "");
    }
    //$(".index_video").css("display", "");
    myVideo.play();
}

//获取url参数
function getQueryString(name) {
    var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}

function GetqueryString(name) {
    var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
    var r = window.parent.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}

function showQrCode() {
    // iframe层 - 禁滚动条
    //layer.open({
    //    closeBtn: false,
    //    type: 2,
    //    title: false,
    //    area: ['100%', '100%'],
    //    skin: 'layui-layer-rim', //加上边框
    //    async: false,
    //    content: ['ShowQrCode.html', 'no']
    //});
    var strUrl = getQueryString("url");
    window.location.href = "ShowQrCode.html?url=" + strUrl;
}

function jump(url) {
    var strUrl = getQueryString("url");
    if (strUrl == null || strUrl == "null" || strUrl == "") {
        window.location.href = url;
    } else {
        window.location.href = url + "?url=" + strUrl;
    }
}

function jumps(url) {
    var index = parent.layer.getFrameIndex(window.name);
    var strUrl = GetqueryString("url");
    if (strUrl == null || strUrl == "null" || strUrl == "") {
        window.parent.location.href = url;
    } else {
        window.parent.location.href = url + "?url=" + strUrl;
    }
}

//回主页
function toMain() {
    var strUrl = getQueryString("url");
    if (strUrl == null || strUrl == "null" || strUrl == "") {
        window.location.href = "Main.html";
    } else {
        window.location.href = "Index.html?url=" + strUrl;
    }
}


