
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:配置Jquery插件功能JS文件
 History: 
 
 Author:杨洋
 date:2013.3.12
 version:好爱1.0
 description:修改描述
 */

//初始化对象参数 objName对应的Jquery选择器
function fancyFunction(url, width, height, objNodeName) {
    this.url = url;
    this.width = width;
    this.height = height;
    this.objNodeName = objNodeName;
}
fancyFunction.prototype = {
    constructor: fancyFunction
}

//初始化调用 //  objName对应的Jquery选择器 
function showPopuWindows(url, width, height, objNodeName) {
    $(objNodeName).attr("href", url);
  
    $(objNodeName).fancybox({
        "width": width,
        "height": height,
        'autoScale': true,
        'autoDimensions': true,
        "centerOnScroll": true,
        "changeFade":"fast",
        'transitionIn': 'none',
        'transitionOut': 'none',
        'hideOnOverlayClick': false,
        'topRatio':0.2,
        'type': 'iframe',
        'onClosed': function () {
            alert("ff");
		   
        }, 'onComplete': function () {
            $.fancybox.center(true);
        }
    });
}

function EndRequestHandler() {
    loadfancybox();
    //            TableCss();
}

var widthValue, heightValue, callBackID;
function MainshowIF(url, W, H, CallBackID) {
    $("#Eject", document).attr("href", url);
    Showtanchu(W, H, CallBackID);
    $("#btn_Eject", document).click();
}

function showView() {
    $("#Eject").click();
}

function Showtanchu(w, h, callBack) {
    widthValue = w;
    heightValue = h;
    if (callBack != "")
        callBackID = "#" + callBack + "";
}


function loadfancybox() {
    $(".llb").fancybox({
        'width': widthValue,
        'height': heightValue,
        'autoScale': false,
        'autoDimensions': false,
        'transitionIn': 'none',
        'transitionOut': 'none',
        'topRatio':0,
        'type': 'iframe',
        'onClosed': function () {
            if (callBackID != null && callBackID != "") {
                $(callBackID).click();
            }
        },
        'onComplete': function () {
            $('#fancybox-wrap').width(widthValue + 20);
            $('#fancybox-content').width(widthValue);
            $('#fancybox-wrap').height(heightValue);
            $('#fancybox-content').height(heightValue);
            $.fancybox.center(true);
        }
    });
}
