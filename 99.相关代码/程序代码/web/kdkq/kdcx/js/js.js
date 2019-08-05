$(function(){
	//查询快递信息
       Bmob.initialize("417ad80af2de7cc79d976ed980d308a0", "ce5bf94363345371af655621a8d57c41");
       var ExpressOrder = Bmob.Object.extend("ExpressOrder");
    var query = new Bmob.Query(ExpressOrder);
    query.equalTo("phone",getUrlParam("username"));
	query.descending("createdAt");
    query.find({
    	success : function(results){
    		//初始化
    		$(".tip-max").remove();
    		for(var i = 0;i < 5;i++){
    			$(".info").remove();
    		}
    		//未找到记录
    		if(results == ""){
    			$("#tip-name").text("");
    			$("#tip-welcome").text("没有找到任何记录");
    			return false;
    		}
    		//名字格式化
    		var name = "*" + results[0].get("name").substring(1,10);
    		$("#tip-name").text(name);
    		$("#tip-welcome").text("的代取记录");
    		for(var i = 0;i < 5;i++){
    			if(results[i] == undefined){
    				return false;
    			}
    			//刷新列表
    			if(results[i].get("state") != 0){
    				$("#expressInfo").append("<div class='info'><span class='tip-state'>状态：<span id='state_" + i + "' class='state'></span></span><span id='time_" + i + "' class='time'></span><p class='tip-orderId'><span id='type_" + i + "'>快递单号：</span><span id='orderId_" + i + "' class='orderId'></span></p></div>");
    			}else{
    				$("#expressInfo").append("<div class='info'><span class='tip-state'>状态：<span id='state_" + i + "' class='state'></span></span><span id='time_" + i + "' class='time'></span><p class='tip-orderId'><span id='type_" + i + "'>快递单号：</span><span id='orderId_" + i + "' class='orderId'></span></p><a href='javascript:;' class='cancel' alt='" + results[i].id + "'>撤销订单</a></div>");
    				//撤销订单操作
					$(".cancel").on("click",function(){
					   	if(confirm("您确定要撤销吗？")){
					   		$.ajax({
					   			url : "http://cloud.bmob.cn/ae43dc799f82c889/cancel_express_order?username=" + getUrlParam("username") + "&objectId=" + $(this).attr("alt"),
					   			type : "get",
					   			success : function(result){
					   				history.go(0);
					   			},
					   			error : function(error){
					   				history.go(0);
					   			}
					   		});
					   	}
					});
    			}
    			$("#state_" + i).text(stateUtil(results[i].get("state"),"state_" + i));
    			$("#time_" + i).text(timeUtil(results[i].createdAt));
    			//使用的快递券
    			if(results[i].get("orderId") == undefined){
    				$("#type_" + i).text("快递券号：");
					$("#orderId_" + i).text(results[i].get("voucherId"));
    			}else{
    				$("#orderId_" + i).text(results[i].get("orderId"));
    			}
    			if(i == 4 && results[5] != undefined){
    				$("#expressInfo").append("<div class='tip-max'>仅显示最近五条记录</div>")
    			}
    		}
    	},
    	error : function(error){
    		alert("系统错误");
    		return false;
    	}
    });
});




//时间转换
function timeUtil(time){
	//处理时间
	var year = parseInt(time.substring(0,4));
	var month = parseInt(time.substring(5,7));
	var day = parseInt(time.substring(8,10));
	var hour = parseInt(time.substring(11,13));
	var minute = parseInt(time.substring(14,16));
	var seconds = parseInt(time.substring(17,19));
	//获取查询时间
	var date = new Date();
	var thisYear = date.getFullYear().toString();
	var thisMonth = (date.getMonth() + 1).toString();
	var thisDay = date.getDate().toString();
	var thisHour = date.getHours().toString();
	var thisMinute = date.getMinutes().toString();
	var thisSeconds = date.getSeconds().toString();
	var timeStr;
	//转化语义
	if(thisYear > year){
		timeStr = thisYear - year + "年前";
	}else if(thisMonth > month){
		timeStr = thisMonth - month + "个月前";
	}else if(thisDay > day){
		timeStr = thisDay - day + "天前";
	}else if(thisHour > hour){
		timeStr = thisHour - hour + "小时前";
	}else if(thisMinute > minute){
		timeStr = thisMinute - minute + "分钟前";
	}else{
		timeStr = "刚刚";
	}
	return timeStr;
}

//状态转换
function stateUtil(obj,id){
	if(obj == -1){
		$("#" + id).css("color","#996600");
		return "订单错误";	
	}else if(obj == 0){
		$("#" + id).css("color","purple");
		return "快递正在代取中";
	}else if(obj == 1){
		$("#" + id).css("color","#FF9900");
		return "快递正在配送中";
	}else if(obj == 2){
		$("#" + id).css("color","blue");
		return "快递已成功配送";
	}else{
		return "订单异常";
	}
}

//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}