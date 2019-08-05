//用于验证表单 0-姓名 1-联系电话 2-宿舍楼 3-快递信 4-物品订单编号 5-代取劵编号
var isTrue = [0,0,0,0,0,0];
$(function(){
    //判断地址来源
    //口袋快爆用户
    if(window.localStorage.getItem("source") == "other"){
        $("#label-useTicket").hide();
        $("#label-notUseTicket").show();
        $("#label-source").hide();
        isTrue[4] = 0;
    }else if(window.localStorage.getItem("source") == "kdkb"){
        $("#label-useTicket").show();
        $("#label-notUseTicket").hide();
        isTrue[5] = 0;
    }
    //验证当日快递数量是否超过限制
    //卖个萌
    ﾟωﾟﾉ= /｀ｍ´）ﾉ ~┻━┻   //*´∇｀*/ ['_']; o=(ﾟｰﾟ)  =_=3; c=(ﾟΘﾟ) =(ﾟｰﾟ)-(ﾟｰﾟ); (ﾟДﾟ) =(ﾟΘﾟ)= (o^_^o)/ (o^_^o);(ﾟДﾟ)={ﾟΘﾟ: '_' ,ﾟωﾟﾉ : ((ﾟωﾟﾉ==3) +'_') [ﾟΘﾟ] ,ﾟｰﾟﾉ :(ﾟωﾟﾉ+ '_')[o^_^o -(ﾟΘﾟ)] ,ﾟДﾟﾉ:((ﾟｰﾟ==3) +'_')[ﾟｰﾟ] }; (ﾟДﾟ) [ﾟΘﾟ] =((ﾟωﾟﾉ==3) +'_') [c^_^o];(ﾟДﾟ) ['c'] = ((ﾟДﾟ)+'_') [ (ﾟｰﾟ)+(ﾟｰﾟ)-(ﾟΘﾟ) ];(ﾟДﾟ) ['o'] = ((ﾟДﾟ)+'_') [ﾟΘﾟ];(ﾟoﾟ)=(ﾟДﾟ) ['c']+(ﾟДﾟ) ['o']+(ﾟωﾟﾉ +'_')[ﾟΘﾟ]+ ((ﾟωﾟﾉ==3) +'_') [ﾟｰﾟ] + ((ﾟДﾟ) +'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ ((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+((ﾟｰﾟ==3) +'_') [(ﾟｰﾟ) - (ﾟΘﾟ)]+(ﾟДﾟ) ['c']+((ﾟДﾟ)+'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ (ﾟДﾟ) ['o']+((ﾟｰﾟ==3) +'_') [ﾟΘﾟ];(ﾟДﾟ) ['_'] =(o^_^o) [ﾟoﾟ] [ﾟoﾟ];(ﾟεﾟ)=((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟДﾟ) .ﾟДﾟﾉ+((ﾟДﾟ)+'_') [(ﾟｰﾟ) + (ﾟｰﾟ)]+((ﾟｰﾟ==3) +'_') [o^_^o -ﾟΘﾟ]+((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟωﾟﾉ +'_') [ﾟΘﾟ]; (ﾟｰﾟ)+=(ﾟΘﾟ); (ﾟДﾟ)[ﾟεﾟ]='\\'; (ﾟДﾟ).ﾟΘﾟﾉ=(ﾟДﾟ+ ﾟｰﾟ)[o^_^o -(ﾟΘﾟ)];(oﾟｰﾟo)=(ﾟωﾟﾉ +'_')[c^_^o];(ﾟДﾟ) [ﾟoﾟ]='\"';(ﾟДﾟ) ['_'] ( (ﾟДﾟ) ['_'] (ﾟεﾟ+(ﾟДﾟ)[ﾟoﾟ]+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (c^_^o)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (o^_^o))+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟoﾟ]) (ﾟΘﾟ)) ('_');
        //获取当前时间 并设置时间戳
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var dateline;
    if(month < 10){
        if(day < 10){
            dateline = year + "-0" + month + "-0" + day;
        }else{
            dateline = year + "-0" + month + "-" + day;
        }
    }else{
        if(day < 10){
            dateline = year + "-" + month + "-0" + day;
        }else{
            dateline = year + "-" + month + "-" + day;
        }
    }
    //存储记录
    var array = new Array();
    var ExpressOrder = Bmob.Object.extend("ExpressOrder");
    var query = new Bmob.Query(ExpressOrder);
    query.select("ExpressOrder","createdAt");
    query.find({
        success : function(results){
            for(var i = 0;i < results.length;i++){
                var string = results[i].createdAt;
                //将当天记录放在数组中
                if(string.substr(0,10) == dateline){
                    array.push(string);
                }
            }
            //判断是否超出最大数量
            var ParameterList = Bmob.Object.extend("ParameterList");
            var query_list = new Bmob.Query(ParameterList);
            query_list.get("pCbA0002",{
                success : function(results){
                    //开启代取服务
                    if(results.get("switch")){
                        //超出要求 跳转页面
                        if(array.length > results.get("number")){
                            $("#submit").attr("disabled",true);
                            self.location = 'stop.html';
                        }
                    }else{
                        $("#submit").attr("disabled",true);
                        self.location = 'stop.html';
                    }
                },
                error : function(error){
                }
            });
        },
        error : function(error){
        }
    });
    //姓名验证
    $("#name").blur(function(){
        var res = /[0-9]/;
        var name = $("#name").val();
        var m = res.test(name);
        if(name == ""){
            $("#name-msg").removeClass("hide");
            $("#name-msg").addClass("msg");
            $("#name-msg").text("请输入姓名!");
            isTrue[0] = 0;
        }else if(m){
            $("#name-msg").removeClass("hide");
            $("#name-msg").addClass("msg");
            $("#name-msg").text("姓名输入不合法!");
            isTrue[0] = 0;
        }else{
            $("#name-msg").addClass("hide");
            $("#name-msg").removeClass("msg");
            isTrue[0] = 1;
        }
    });
    //手机号验证
    $("#phone").blur(function(){
        var res = /^1[3|4|5|7|8]\d{9}$/;
        var phone = $("#phone").val();
        var m = res.test(phone);
        if(phone == ""){
            $("#phone-msg").removeClass("hide");
            $("#phone-msg").addClass("msg");
            $("#phone-msg").text("请输入手机号!");
            isTrue[1] = 0;
        }else if(!m){
            $("#phone-msg").removeClass("hide");
            $("#phone-msg").addClass("msg");
            $("#phone-msg").text("手机号输入不合法!");
            isTrue[1] = 0;
        }else{
            $("#phone-msg").addClass("hide");
            $("#phone-msg").removeClass("msg");
            isTrue[1] = 1;
        }
    });
    //宿舍楼验证
    $("#dormitory").blur(function(){
        var dorm = $("#dormitory").val();
        if(dorm == ""){
            $("#dorm-msg").removeClass("hide");
            $("#dorm-msg").addClass("msg");
            $("#dorm-msg").text("请输入宿舍楼!");
            isTrue[2] = 0;
        }else{
            $("#dorm-msg").addClass("hide");
            $("#dorm-msg").removeClass("msg");
            isTrue[2] = 1;
        }
    });
    //快递信验证
    $("#letter").blur(function(){
        var letter = $("#letter").val();
        if(letter == ""){
            $("#letter-msg").removeClass("hide");
            $("#letter-msg").addClass("msg");
            $("#letter-msg").text("请输入快递信!");
            isTrue[3] = 0;
        }else{
            $("#letter-msg").addClass("hide");
            $("#letter-msg").removeClass("msg");
            isTrue[3] = 1;
        }
    });
    //快递券验证
    $("#voucherId").blur(function(){
        var voucherId = $("#voucherId").val();
        if(voucherId == ""){
            $("#voucherId-msg").removeClass("hide");
            $("#voucherId-msg").addClass("msg");
            $("#voucherId-msg").text("请输入快递券!");
            isTrue[5] = 0;
        }else{
            $("#voucherId-msg").addClass("hide");
            $("#voucherId-msg").removeClass("msg");
            isTrue[5] = 1;
        }
    });
    //物品订单编号验证
    $("#orderId").blur(function(){
        var orderId = $("#orderId").val();
        if(orderId == ""){
            $("#orderId-msg").removeClass("hide");
            $("#orderId-msg").addClass("msg");
            $("#orderId-msg").text("请输入物品订单编号!");
            isTrue[4] = 0;
        }else{
            $("#orderId-msg").addClass("hide");
            $("#orderId-msg").removeClass("msg");
            isTrue[4] = 1;
        }
    });
});
//提交检查
function toVaild(){
    //只检查前四项
    for(var i = 0;i < 4;i++){
        if(!isTrue[i]){
            alert("信息填写有误，请重新检查");
            return false;
        }else{
            //前四项检查完毕
            if(i == 3){
                if(isTrue[4] || isTrue[5]){
                    submit();
                }else{
                    alert("信息填写有误，请重新检查");
                    return false;
                }
            }
        }
    }
}

function submit(){
    //黑名单验证
    //再卖一个萌
    ﾟωﾟﾉ= /｀ｍ´）ﾉ ~┻━┻   //*´∇｀*/ ['_']; o=(ﾟｰﾟ)  =_=3; c=(ﾟΘﾟ) =(ﾟｰﾟ)-(ﾟｰﾟ); (ﾟДﾟ) =(ﾟΘﾟ)= (o^_^o)/ (o^_^o);(ﾟДﾟ)={ﾟΘﾟ: '_' ,ﾟωﾟﾉ : ((ﾟωﾟﾉ==3) +'_') [ﾟΘﾟ] ,ﾟｰﾟﾉ :(ﾟωﾟﾉ+ '_')[o^_^o -(ﾟΘﾟ)] ,ﾟДﾟﾉ:((ﾟｰﾟ==3) +'_')[ﾟｰﾟ] }; (ﾟДﾟ) [ﾟΘﾟ] =((ﾟωﾟﾉ==3) +'_') [c^_^o];(ﾟДﾟ) ['c'] = ((ﾟДﾟ)+'_') [ (ﾟｰﾟ)+(ﾟｰﾟ)-(ﾟΘﾟ) ];(ﾟДﾟ) ['o'] = ((ﾟДﾟ)+'_') [ﾟΘﾟ];(ﾟoﾟ)=(ﾟДﾟ) ['c']+(ﾟДﾟ) ['o']+(ﾟωﾟﾉ +'_')[ﾟΘﾟ]+ ((ﾟωﾟﾉ==3) +'_') [ﾟｰﾟ] + ((ﾟДﾟ) +'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ ((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+((ﾟｰﾟ==3) +'_') [(ﾟｰﾟ) - (ﾟΘﾟ)]+(ﾟДﾟ) ['c']+((ﾟДﾟ)+'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ (ﾟДﾟ) ['o']+((ﾟｰﾟ==3) +'_') [ﾟΘﾟ];(ﾟДﾟ) ['_'] =(o^_^o) [ﾟoﾟ] [ﾟoﾟ];(ﾟεﾟ)=((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟДﾟ) .ﾟДﾟﾉ+((ﾟДﾟ)+'_') [(ﾟｰﾟ) + (ﾟｰﾟ)]+((ﾟｰﾟ==3) +'_') [o^_^o -ﾟΘﾟ]+((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟωﾟﾉ +'_') [ﾟΘﾟ]; (ﾟｰﾟ)+=(ﾟΘﾟ); (ﾟДﾟ)[ﾟεﾟ]='\\'; (ﾟДﾟ).ﾟΘﾟﾉ=(ﾟДﾟ+ ﾟｰﾟ)[o^_^o -(ﾟΘﾟ)];(oﾟｰﾟo)=(ﾟωﾟﾉ +'_')[c^_^o];(ﾟДﾟ) [ﾟoﾟ]='\"';(ﾟДﾟ) ['_'] ( (ﾟДﾟ) ['_'] (ﾟεﾟ+(ﾟДﾟ)[ﾟoﾟ]+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (c^_^o)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ ((ﾟｰﾟ) + (o^_^o))+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) +(o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (c^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ ((ﾟｰﾟ) + (o^_^o))+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟΘﾟ)+ (ﾟｰﾟ)+ (o^_^o)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟｰﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((o^_^o) +(o^_^o))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+(ﾟｰﾟ)+ ((o^_^o) - (ﾟΘﾟ))+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (ﾟΘﾟ))+ (ﾟΘﾟ)+ (ﾟДﾟ)[ﾟεﾟ]+((ﾟｰﾟ) + (o^_^o))+ (o^_^o)+ (ﾟДﾟ)[ﾟoﾟ]) (ﾟΘﾟ)) ('_');
    var BlackList = Bmob.Object.extend("BlackList");
    var query = new Bmob.Query(BlackList);
    query.equalTo("phone", $("#phone").val());
    query.find({
        success : function(results){
            //查询到数据
            if(results.length >= 1){
                //黑名单功能开启
                if(results[0].get("switch")){
                    self.location = 'black.html';
                    return false;
                }
            }
        },
        error : function(error){
            return false;
        }
    });
    //使用快递券 查询是否有效
    if(isTrue[5]){
        var ExpressVoucher = Bmob.Object.extend("ExpressVoucher");
        var query = new Bmob.Query(ExpressVoucher);
        query.get($("#voucherId").val(), {
            success: function(express) {
                // 查询成功
                var isUtility = express.get("isUtility");
                var isActivation = express.get("isActivation");
                //判断是否激活
                if(isActivation){
                    //已激活
                    if(!isUtility){
                            //验证成功
                            query.get($("#voucherId").val(),{
                                success: function(obj){
                                    //修改状态
                                    obj.set("isUtility",true);
                                    //obj.save();
                                    obj.save(null, {
                                        success : function(successful){
                                                //写入数据
                                            var ExpressOrder = Bmob.Object.extend("ExpressOrder");
                                            var order = new ExpressOrder();
                                            order.set("phone", $("#phone").val());
                                            order.set("name", $("#name").val());
                                            order.set("region", $("#region").find("option:selected").text());
                                            order.set("dormitory", $("#dormitory").val());
                                            order.set("type", $("#type").find("option:selected").text());
                                            order.set("size", $("#size").find("option:selected").text());
                                            order.set("letter", $("#letter").val());
                                            order.set("voucherId", $("#voucherId").val());
                                            order.set("com", $("#com").find("option:selected").text());
                                            order.set("school","天津工业大学");
                                            order.set("state",0);
                                            order.save(null, {
                                                success : function(sus){
                                                    self.location = 'success.html';
                                                },
                                                error : function(err){
                                                    return false; 
                                                }
                                            });
                                        },
                                        error : function(fail){
                                            return false;
                                        }
                                    });
                                    
                                },
                                error: function(obj,error){
                                    alert("一脸懵逼的错误");
                                    return false;
                                }
                            })
                    }else{
                        alert("代取券已使用！");
                        return false;
                    }
                }else{
                    alert("代取券尚未激活，无法使用！");
                    return false;
                }
            },
            error: function(object, error) {
                // 查询失败
                alert("代取券有误，请重新填写！");
                return false;
            }
        });
    }
    //使用口袋快爆
    if(isTrue[4]){
        //查询订单号是否存在
        var orderList = Bmob.Object.extend("Order");
        var orderResults = new Bmob.Query(orderList);
        orderResults.equalTo("id",$("#orderId").val());
        orderResults.find({
            success : function(results){
                //不存在
                if(results.length == 0){
                    alert("未找到对应物品订单编号！");
                    return false;
                }else if(results[0].get("state")){
                    self.location = "resubmit.html"
                    return false;
                }else{
                    var ids = results[0].id;
                    var orderList = Bmob.Object.extend("Order");
                    var query = new Bmob.Query(orderList);
                    query.get(ids, {
                        success: function(gameScore) {
                          gameScore.set('state', true);
                          gameScore.save(null,{
                            success : function(suc){
                                    //添加数据
                                var ExpressOrder = Bmob.Object.extend("ExpressOrder");
                                var order = new ExpressOrder();
                                order.set("phone", $("#phone").val());
                                order.set("name", $("#name").val());
                                order.set("region", $("#region").find("option:selected").text());
                                order.set("dormitory", $("#dormitory").val());
                                order.set("type", $("#type").find("option:selected").text());
                                order.set("size", $("#size").find("option:selected").text());
                                order.set("letter", $("#letter").val());
                                order.set("orderId", $("#orderId").val());
                                order.set("com", $("#com").find("option:selected").text());
                                order.set("school","天津工业大学");
                                order.set("state",0);
                                order.save(null,{
                                    success : function(successful){
                                        self.location = 'success.html';
                                    },
                                    error : function(err){
                                        return false;
                                    }
                                });
                                
                            },
                            error : function(err){
                                return false;
                            }
                          });
                        },
                        error: function(object, error) {
                            alert("error");
                            return false;
                        }
                    });
                }
            },
            error : function(error){
                return false;
            }
        });
    }
}

function getVoucherId(){
    //获取openId
    var openId = window.localStorage.getItem("openId");
    console.log(openId);
    //查询用户username
    $.ajax({
        url : 'https://www.neday.cn/api/v1/users?where=[{"key":"openId","value":"' + openId + '","operation":"=","relation":""}]&isNew=true',
        type : "get",
        success : function(suc){
            if(suc.objectId == "null" || suc.objectId == null){
                alert("查询用户不存在");
                return false;
            }else{
                //查询是否有可用代取券
                var username = JSON.stringify(suc.username);
                Bmob.initialize("417ad80af2de7cc79d976ed980d308a0", "ce5bf94363345371af655621a8d57c41");
                //查询是否有可用
                var ExpressVoucher= Bmob.Object.extend("ExpressVoucher");
                var query = new Bmob.Query(ExpressVoucher);
                query.equalTo("owner", username);
                query.equalTo("isActivation",true);
                query.equalTo("isUtility",true);
                query.ascending("objectId");
                query.find({
                    success : function(results){
                        //不存在
                        console.log(username);
                        if(results.length == 0){
                            if(confirm("该账户在下没有获取到可用代取券，是否消耗30积分兑换该券?")){
                                //确定
                                Bmob.Cloud.run('exchange_credits', {"username": username }, {
                                    success: function(result) {
                                      var message = JSON.parse(result);
                                      message = JSON.stringify(message.objectId);
                                      console.log(message);
                                      switch(message){
                                            case '"积分不足30"':
                                                alert("兑换失败：" + message);
                                                return false;
                                                break;
                                            case '"扣除积分失败"':
                                                alert("兑换失败：" + message);
                                                return false;
                                                break;
                                            case '"用户不存在"':
                                                alert("兑换失败：" + message);
                                                return false;
                                                break;
                                            case '"查询用户异常"':
                                                alert("兑换失败：" + message);
                                                return false;
                                                break;
                                            default:
                                                console.log(message);
                                                alert("兑换成功：" + message);
                                                var objectId = JSON.stringify(succes.objectId);
                                                $("#voucherId").val(objectId);
                                                break;
                                        }
                                        return false;
                                    },
                                    error: function(error) {
                                        alert("尝试兑换代取券编码失败，请重新尝试");
                                        return false;
                                    }
})
                                // $.ajax({
                                //     url : 'http://cloud.bmob.cn/ae43dc799f82c889/exchange_credits?username=' + username + '&cache=666',
                                //     type : "get",
                                //     success : function(succes){
                                //         var message = JSON.stringify(succes.message);
                                //         switch(message){
                                //             case "积分不足30":
                                //                 alert("兑换失败：" + message);
                                //                 return false;
                                //                 break;
                                //             case "扣除积分失败":
                                //                 alert("兑换失败：" + message);
                                //                 return false;
                                //                 break;
                                //             case "用户不存在":
                                //                 alert("兑换失败：" + message);
                                //                 return false;
                                //                 break;
                                //             case "查询用户异常":
                                //                 alert("兑换失败：" + message);
                                //                 return false;
                                //                 break;
                                //             default:
                                //                 alert("兑换成功：" + message);
                                //                 var objectId = JSON.stringify(succes.objectId);
                                //                 $("#voucherId").val(objectId);
                                //                 break;
                                //         }
                                //         return false;
                                //     },
                                //     error : function(errs){
                                //         alert("尝试兑换代取券编码失败，请重新尝试");
                                //         return false;
                                //     }
                                // });
                            }else{
                                //取消
                                return false;
                            }
                        }else{
                            //存在
                            var objectId = results[0];
                            $("#voucherId").val(objectId);
                            alert("获取成功！");
                            return false;
                        }
                    },
                    error : function(error){
                        alert("查询用户代取券异常！");
                        return false;
                    }
                });
                // $.ajax({
                //     //加头
                //     url : 'https://api.bmob.cn/1/classes/ExpressVoucher?where={"$and":[{"owner":"' + username + '"},{"$and":[{"isActivation":true},{"isUtility":false}]}]}&order=updatedAt&limit=1',
                //     type : "get",
                //     success : function(success){
                //         //不存在
                //         if(success.results[0] == "" || success.results[0] == null){
                //             if(confirm("该账户在下没有获取到可用代取券，是否消耗30积分兑换该券?")){
                //                 //确定
                //                 $.ajax({
                //                     url : 'http://cloud.bmob.cn/ae43dc799f82c889/exchange_credits?username=' + username + '&cache=666',
                //                     type : "get",
                //                     success : function(succes){
                //                         var message = JSON.stringify(succes.message);
                //                         switch(message){
                //                             case "积分不足30":
                //                                 alert("兑换失败：" + message);
                //                                 return false;
                //                                 break;
                //                             case "扣除积分失败":
                //                                 alert("兑换失败：" + message);
                //                                 return false;
                //                                 break;
                //                             case "用户不存在":
                //                                 alert("兑换失败：" + message);
                //                                 return false;
                //                                 break;
                //                             case "查询用户异常":
                //                                 alert("兑换失败：" + message);
                //                                 return false;
                //                                 break;
                //                             default:
                //                                 alert("兑换成功：" + message);
                //                                 var objectId = JSON.stringify(succes.objectId);
                //                                 $("#voucherId").val(objectId);
                //                                 break;
                //                         }
                //                         return false;
                //                     },
                //                     error : function(errs){
                //                         alert("尝试兑换代取券编码失败，请重新尝试");
                //                         return false;
                //                     }
                //                 });
                //             }else{
                //                 //取消
                //                 return false;
                //             }
                //         }else{
                //             //存在
                //             var objectId = JSON.stringify(success.results[0].objectId);
                //             $("#voucherId").val(objectId);
                //             alert("获取成功！");
                //             return false;
                //         }
                //     },
                //     error : function(){
                //         alert("查询用户代取券异常！");
                //         return false;
                //     }
                // });
            }
        },
        error : function(){
            alert("查询用户异常！");
            return false;
        }

    });
}