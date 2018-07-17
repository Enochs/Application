//***********************************************************
//if the attribute of control check="1",the value of the TextBox is allowed null,if user input data that it will validate by RegExp.
//if the attribute of control check="2",it will validate by RegExp direct.
//***********************************************************
//Get All the lable which need to validate
(function($){
	$(document).ready(function(){
		$('select[tip],select[check],input[tip],input[check],textarea[tip],textarea[check]').tooltip();
	});
})(jQuery);

(function ($) {
    $.fn.tooltip = function (options) {
        var getthis = this;
        var opts = $.extend({}, $.fn.tooltip.defaults, options);
        //Create a prompt box
        $('body').append('<table id="tipTable" class="tableTip"><tr><td  class="leftImage"></td> <td class="contenImage" align="left"></td> <td class="rightImage"></td></tr></table>');
        //移Move the mouse to hide just created prompt dialog box
        $(document).mouseout(function () { $('#tipTable').hide() });

        this.each(function () {
            if ($(this).attr('tip') != '') {
                $(this).mouseover(function () {
                    $('#tipTable').css({ left: $.getLeft(this) + 'px', top: $.getTop(this) + 'px' });
                    $('.contenImage').html($(this).attr('tip'));
                    $('#tipTable').fadeIn("fast");
                },
                function () {
                    $('#tipTable').hide();
                });
            }
            if ($(this).attr('check') != '') {

                $(this).focus(function () {
                    $(this).removeClass('tooltipinputerr');
                }).blur(function () {
                    if ($(this).attr('toupper') == 'true') {
                        this.value = this.value.toUpperCase();
                    }
                    if ($(this).attr('check') != '') {

                        if ($(this).attr('check') == "1") {


                            if ($(this).attr('value') == null) {

                                $(this).removeClass('tooltipinputerr').addClass('tooltipinputok');
                            } else {

                                var thisReg = new RegExp($(this).attr('reg'));
                                if (thisReg.test(this.value)) {
                                    $(this).removeClass('tooltipinputerr').addClass('tooltipinputok');
                                }
                                else {
                                    $(this).removeClass('tooltipinputok').addClass('tooltipinputerr');
                                }

                            }
                        }
                        if ($(this).attr('check') == "2") {
                            var thisReg = new RegExp($(this).attr('reg'));
                            if (thisReg.test(this.value)) {
                                $(this).removeClass('tooltipinputerr').addClass('tooltipinputok');
                            }
                            else {
                                $(this).removeClass('tooltipinputok').addClass('tooltipinputerr');
                            }
                        }
                    }

                });
            }
        });
        if (opts.onsubmit) {
            $('form').submit(function () {
                var isSubmit = true;
                var ReturnValue = true;
                getthis.each(function () {
                    if ($(this).attr('check') == "1") {
                        if ($(this).attr('value') == null) {

                            $(this).removeClass('tooltipinputerr').addClass('tooltipinputok');
                        } else {

                            var thisReg = new RegExp($(this).attr('reg'));
                            if (thisReg.test(this.value)) {
                                $(this).removeClass('tooltipinputerr').addClass('tooltipinputok');
                                isSubmit = true;
                                
                            }
                            else {
                                $(this).removeClass('tooltipinputok').addClass('tooltipinputerr');
                                isSubmit = false;
                                $(this).focus();
                                ReturnValue = false;
                            }
                        }  
                    }
                    if ($(this).attr('check') == "2") {
                        

                        var thisReg = new RegExp($(this).attr('reg'));
                        if (thisReg.test(this.value) && $(this).val() != "") {
                            $(this).removeClass('tooltipinputerr').addClass('tooltipinputok');
                             
                        }
                        else {
                            $(this).removeClass('tooltipinputok').addClass('tooltipinputerr');

                            isSubmit = false;
                            $(this).focus();
                            ReturnValue = false;
 
                        }
                    }
                });

    
                if (ReturnValue)
                {
                    return true;
                } else
                {
                    return false;
                }
           
            });
        }
    };

    $.extend({
        getWidth: function (object) {
            return object.offsetWidth;
        },

        getLeft: function (object) {
            var go = object;
            var oParent, oLeft = go.offsetLeft;
            while (go.offsetParent != null) {
                oParent = go.offsetParent;
                oLeft += oParent.offsetLeft;
                go = oParent;
            }
            return oLeft;
        },

        getTop: function (object) {
            var go = object;
            var oParent, oTop = go.offsetTop;
            while (go.offsetParent != null) {
                oParent = go.offsetParent;
                oTop += oParent.offsetTop;
                go = oParent;
            }
            return oTop + $(object).height() + 5;
        },

        onsubmit: true
    });
    $.fn.tooltip.defaults = { onsubmit: true };
})(jQuery);

//***************************************************************************************************************************************************
//The label attribute set expressions using JQuery function
//Incoming label group ID must be "name1 name2: name3" separated by ':' delimited.

//positive integer
function CheckPositiveInteger(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
		    $("#" + validatorStrings[i]).attr("reg", "^[1-9]\\d*$");
		}
	}
}

//integer
function CheckInteger(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", "^-?[1-9]\\d*$");
        }
    }
}

//对所有需要金额验证的标签进行设置正则表达式
function CheckMoney(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
			$("#"+validatorStrings[i]).attr("reg","^(-)?(([1-9]{1}\\d*)|([0]{1}))(\\.(\\d){1,2})?$");
		}
	}
}

//对所有需要正浮点验证的标签进行设置正则表达式
function CheckFloat(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
			$("#"+validatorStrings[i]).attr("reg","^[0-9]+\\.[0-9]+$");
		}
	}
}

//对所有需要电子邮件验证的标签进行设置正则表达式
function CheckEMail(validatorString)
{
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$");
        }
    }
}

//对所有需要邮编验证的标签进行设置正则表达式
function CheckZipcode(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
			$("#"+validatorStrings[i]).attr("reg","^\\d{6}$");
		}
	}
}

//对所有需要手机验证的标签进行设置正则表达式
function CheckMobile(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
		    $("#" + validatorStrings[i]).attr("reg", "^(13|15|18)[0-9]{9}$");
		    //^13[0-9]{9}|15[012356789][0-9]{8}|18[0256789][0-9]{8}|147[0-9]{8}$
		}
	}
}

//对所有需要身份证验证的标签进行设置正则表达式
function CheckID(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
			$("#"+validatorStrings[i]).attr("reg","^[1-9]([0-9]{14}|[0-9]{17})$");
		}
	}
}

//对所有需要登录帐号验证的标签进行设置正则表达式
function CheckUserID(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
			$("#"+validatorStrings[i]).attr("reg","^\\w+$");
		}
	}
}

//对所有需要非空验证的标签进行设置正则表达式
function CheckEmpty(validatorString)
{
	
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
			$("#"+validatorStrings[i]).attr("reg",'.*\\S.*');
		}
	}
}

//对所有需要中文验证的标签进行设置正则表达式
function CheckChinese(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
		    $("#" + validatorStrings[i]).attr("reg", "^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$");
		}
	}
}

//对所有需要URL验证的标签进行设置正则表达式
function CheckUrl(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
		    $("#" + validatorStrings[i]).attr("reg", "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$");
		}
	}
}

//验证座机（匹配国内电话号码(0511-4405222 或 021-87888822) ）
function CheckTell(validatorString)
{
	var validatorStrings="";
	if(validatorString!="")
	{
		validatorStrings=validatorString.split(":");
		for(i=0;i<validatorStrings.length;i++)
		{
		    $("#" + validatorStrings[i]).attr("reg", ".");
		    //\\d{3}-\\d{8}|\\d{4}-\\d{7}
		}
	}
}

//短日期
function CheckDate(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
            ////^\\d{4}(\\-|\\/|\.)\\d{1,2}\\1\\d{1,2}$
            //// /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/
            ///^((((((0[48])|([13579][26])|([2468][048]))00)|([0-9][0-9]((0[48])|([13579][26])|([2468][048]))))-02-29)|(((000[1-9])|(00[1-9][0-9])|(0[1-9][0-9][0-9])|([1-9][0-9][0-9][0-9]))-((((0[13578])|(1[02]))-31)|(((0[1,3-9])|(1[0-2]))-(29|30))|(((0[1-9])|(1[0-2]))-((0[1-9])|(1[0-9])|(2[0-8]))))))$/
        }
    }
}

//验证短时间
function CheckTime(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
            //  /^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/
            // /^(\d{2,2})(:)?(\d{2,2})\2(\d{2,2})$/
        }
    }
}

//验证长时间datatime
function CheckDateTime(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
            // ^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$
        }
    }
}

//验证QQ
function CheckQQ(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", "^[1-9]\d{4,9}$");
            //^[1-9]*[1-9][0-9]*$
        }
    }
}

//validate the name of chinese.e.g 毛泽东，卡尔·马克思
function CheckName(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".*\\S.*");
            //^[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*$
            //^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$"
        }
    }
}

//验证地址
function CheckAddress(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
        }
    }
}

//验证渠道名称
function CheckSourceName(validatorString) {
    debugger;
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".*\\S.*");
        }
    }
}

//验证短文本 20字符
function CheckShortText(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
        }
    }
}

//验证文本 200个字符
function CheckText(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
        }
    }
}

//验证文本
function CheckTextArea(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
        }
    }
}

//验证微博
function CheckWeibo(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
        }
    }
}

//验证微信
function CheckWeiXin(validatorString) {
    var validatorStrings = "";
    if (validatorString != "") {
        validatorStrings = validatorString.split(":");
        for (i = 0; i < validatorStrings.length; i++) {
            $("#" + validatorStrings[i]).attr("reg", ".");
        }
    }
}

//***************************************************************************************************************************************************