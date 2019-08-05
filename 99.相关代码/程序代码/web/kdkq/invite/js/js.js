$(function(){
    //获取推荐人
    //var url = window.location.href;
    var url = "https://www.neday.cn/api/v1/users/627d5312-e6c7-468a-8ccb-ca76e8a28992";
    var include = "_User\[nickname\]";
    var objectId = url.match(/users\/(\S*)/)[1];
    var api = "http://182.254.131.31:8080/api/v1/users?objectId=627d5312-e6c7-468a-8ccb-ca76e8a28992&include=_User[nickname]";
    $.ajax({
        url : api,
        type : "get",
        contentType : "application/json",
        data : {
        },
        success : function(data){
            $("#invite-people").html(data.nickname);
        },
        error : function(){
            alert("服务器开小差了，请重试（错误代码：e1）");
        }
    });
    //获取验证码按钮
    $("#getCode").click(function(){
        //手机号格式验证
        var phone = $("#username").val();
        var password = $("#password").val();
        var tip = $("#tip");
        tip.html("");
        if(phone == ""){
            tip.html("请输入手机号！");
            return false;
        }
        reg = /^1[3|4|5|7|8]\d{9}$/;
        if(!reg.test(phone)){
            tip.html("手机号格式有误！");
            return false;
        }
        if(password == ""){
            tip.html("请输入密码！");
            return false;
        }
        reg = /^[0-9A-Za-z]{6,16}$/;
        if(!reg.test(password)){
            tip.html("密码格式有误！(6-16位字母或数字)");
            return false;
        }
        //验证手机号是否可用
        var api = 'https://www.neday.cn/api/v1/users?where=[{"key":"username","value":"' + phone + '","operation":"=","relation":""}]&count=1&limit=0'
        $.ajax({
            url : api,
            type : "get",
            contentType : "application/json",
            data : {},
            success : function(data){
                if(data.count){
                    tip.html("该手机号已注册，请直接登录！");
                    return false;
                }else{
                    //发送短信
                    $.ajax({
                        url : "https://webapi.sms.mob.com/sms/sendmsg",
                        type : "post",
                        contentType : "application/json",
                        dataType : "json",
                        data : {
                            appkey : "1077112ae0d07",
                            phone : phone,
                            zone : 86
                        },
                        success : function(data){
                            if(data.status == 200){
                                //短信验证倒计时
								tip.html("验证短信已发送！");
                                var wait = 60;
                                var setTime = setInterval(function(){
                                    if(wait == 0){
                                        $("#getCode").removeClass("disabled");
                                        $("#getCode").removeAttr("disabled");
                                        $("#getCode").text("点击获取验证码");
                                        clearInterval(setTime);
                                    }else{
                                        $("#getCode").addClass("disabled");
                                        $("#getCode").attr("disabled","true");
                                        $("#getCode").text(wait + "s");
                                        wait--;
                                    }
                                },1000);
                            }else{
                                alert("服务器开小差了，请重试（错误代码：e6）");
                                return false;
                            }
                        },
                        error : function(){
                            alert("服务器开小差了，请重试（错误代码：e3）");
                            return false;
                        }
                    })
                }
            },
            error : function(){
                alert("服务器开小差了，请重试（错误代码：e2）");
                return false;
            }
        });
    });
    //注册验证
    $("#register").click(function(){
        //手机号格式验证
        var phone = $("#username").val();
        var password = $("#password").val();
        var code = $("#code").val();
        var tip = $("#tip");
        tip.html("");
        if(phone == ""){
            tip.html("请输入手机号！");
            return false;
        }
        reg = /^1[3|4|5|7|8]\d{9}$/;
        if(!reg.test(phone)){
            tip.html("手机号格式有误！");
            return false;
        }
        if(password == ""){
            tip.html("请输入密码！");
            return false;
        }
        reg = /^[0-9A-Za-z]{6,16}$/;
        if(!reg.test(password)){
            tip.html("密码格式有误！(6-16位字母或数字)");
            return false;
        }
        if(code == ""){
            tip.html("请输入短信验证码！");
            return false;
        }
        var api = "https://webapi.sms.mob.com/sms/checkcode";
        //验证号码
        $.ajax({
            url : api,
            type : "post",
            data : {
                appkey : "1077112ae0d07",
                phone : phone,
                zone : 86,
                code : code
            },
            success : function(data){
                if(data.status != 200){
                    alert(JSON.stringify(data));
                    return false;
                }
            },
            error : function(){
                alert("服务器开小差了，请重试（错误代码：e4）");
                return false;
            }
        });
        var api = "https://www.neday.cn/api/v1/users?code=" + code;
        var md5Psd = $.md5(password);
        //注册
        $.ajax({
            url : api,
            type : "post",
            contentType : "application/json",
            data : {
                username : phone,
                password : md5Psd
            },
            success : function(data){
                alert(JSON.stringify(data));
            },
            error : function(){
                alert("服务器开小差了，请重试（错误代码：e5）");
                return false;
            }
        });
    });
});