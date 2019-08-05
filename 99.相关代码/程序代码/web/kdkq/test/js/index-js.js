$(function(){
	window.localStorage.removeItem("source");
	window.localStorage.removeItem("school");
})
$("#kdkb").click(function(){
	$("#kdkb").addClass("border");
	$("#other").removeClass("border");
	window.localStorage.setItem("source","kdkb");
	if(window.localStorage.getItem("school") == "tjlg"){
		window.location.href = "tjlg-index.html";
	}else if(window.localStorage.getItem("school") == "tjgd"){
		window.location.href = "tjgd-index.html";
	}else{
		return false;
	}
});

$("#other").click(function(){
	$("#other").addClass("border");
	$("#kdkb").removeClass("border");
	window.localStorage.setItem("source","other");
	if(window.localStorage.getItem("school") == "tjlg"){
		window.location.href = "tjlg-index.html";
	}else if(window.localStorage.getItem("school") == "tjgd"){
		window.location.href = "tjgd-index.html";
	}else{
		return false;
	}
});

$("#tjlg").click(function(){
	$("#tjlg").addClass("border");
	$("#tjgd").removeClass("border");
	window.localStorage.setItem("school","tjlg");
	if(window.localStorage.getItem("source") == "kdkb" || window.localStorage.getItem("source") == "other"){
		window.location.href = "tjlg-index.html";
	}else{
		return false;
	}
});

$("#tjgd").click(function(){
	$("#tjgd").addClass("border");
	$("#tjlg").removeClass("border");
	window.localStorage.setItem("school","tjgd");
	if(window.localStorage.getItem("source") == "kdkb" || window.localStorage.getItem("source") == "other"){
		window.location.href = "tjgd-index.html";
	}else{
		return false;
	}
});