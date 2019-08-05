//用于验证表单 0-手机号 1-验证码 2-密码
var isTrue = [0,0,0]
$(function(){
	//手机号验证
	$("#phone").blur(function(){
		var res = /^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(17[0,3,5-8])|(18[0-9])|(147))\d{8}$/;
		var phone = $("#phone").val();
		var m = res.test(phone);
		if(phone == ""){
            $("#phone-msg").removeClass("hide");
            $("#phone-msg").addClass("msg");
            $("#phone-msg").text("请输入手机号!");
            isTrue[0] = 0;
        }else if(!m){
            $("#phone-msg").removeClass("hide");
            $("#phone-msg").addClass("msg");
            $("#phone-msg").text("手机号输入不合法!");
            isTrue[0] = 0;
        }else{
            $("#phone-msg").addClass("hide");
            $("#phone-msg").removeClass("msg");
            isTrue[0] = 1;
        }
	});
	//获取验证码按钮
	$("#getCode").click(function(){
		if(isTrue[0]){
			//在这里设置成功
			var phone = $("#phone").val();
			$.ajax({
	        	url : "https://www.neday.cn/api/v1/MobSMS?phone=" + phone,
	            type : "get",
	            success : function(data){
						$("#code-msg").text("验证短信已发送！");
	                    var wait = 60;
	                    var setTime = setInterval(function(){
	                        if(wait == 0){
	                            //$("#getCode").removeClass("disabled");
	                            $("#getCode").removeAttr("disabled");
	                            $("#getCode").val("获取验证码");
	                            clearInterval(setTime);
	                        }else{
	                            //$("#getCode").addClass("disabled");
	                            $("#getCode").attr("disabled","true");
	                            $("#getCode").val(wait + "s");
	                            wait--;
	                        }
	                    },1000);
	            },
	            error : function(errs){
	            	alert("验证码发送失败" + JSON.stringify(errs));
	                alert("数据传输中断，请查看网络连接！");
	                return false;
	            }
	        })
		}else{
			$("#code-msg").removeClass("hide");
	        $("#code-msg").addClass("msg");
	        $("#code-msg").text("手机号错误，无法获取验证码!");
		}
	});
	//验证码验证
	$("#code").blur(function(){
		if($("#code").val() == ""){
			$("#code-msg").removeClass("hide");
            $("#code-msg").addClass("msg");
            $("#code-msg").text("请输入验证码！");
			isTrue[1] = 0;
		}else{
			$("#code-msg").addClass("hide");
            $("#code-msg").removeClass("msg");
			isTrue[1] = 1;
		}
	});
	//密码验证
	$("#password").blur(function(){
		var res = /^[0-9a-zA-Z]{6,16}$/;
		var password = $("#password").val();
		var m = res.test(password);
		if(password == ""){
			$("#password-msg").removeClass("hide");
            $("#password-msg").addClass("msg");
            $("#password-msg").text("请输入密码!");
            isTrue[2] = 0;
		}else if(!m){
			$("#password-msg").removeClass("hide");
            $("#password-msg").addClass("msg");
            $("#password-msg").text("密码不符合规则!(6-16位字母+数字组合)");
            isTrue[2] = 0;
		}else{
			$("#password-msg").addClass("hide");
            $("#password-msg").removeClass("msg");
            isTrue[2] = 1;
		}
	});
	//var md5Psd = $.md5(password);
});

function submits(){
	//信息验证
	for(var i = 0;i < isTrue.length;i++){
		if(isTrue[i]){
			//全部验证完毕
			if(i == isTrue.length - 1){
				var phone  = $("#phone").val();
				var code = $("#code").val();
				var password = $("#password").val();
				var md5Psd = $.md5(password);
				var api = "https://www.neday.cn/api/v1/users?code=" + code;
		        var href = window.location.href;
		        //var openId = getUrlParam(href);
		        var openId = "123abee";
		        var inopenId = $("#inviteCode").val();
		        //手机验证码验证
		        $.ajax({
		            url : api,
		            type : "post",
		            data : {
		                password : md5Psd,
		                username : phone,
		                authData : {
		                	wechat : {
		                		openId : openId,
		                		inopenId : inopenId
		                	}
		                }
		            },
		            success : function(data){
		                alert(JSON.stringify(data));
		            },
		            error : function(err){
		            	alert(JSON.stringify(err.responseText.error));
		                alert("服务器出现了不可描述的错误");
		                return false;
		            }
		        });
			}
		}else{
			//错误提示
			switch(i){
				case 0:
					alert("手机号格式有误");
					break;
				case 1:
					alert("验证码格式有误");
					break;
				case 2:
					alert("密码格式有误");
					break;
			}
			return false;
		}
	}
}

//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}