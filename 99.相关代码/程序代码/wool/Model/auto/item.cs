using System;
namespace wool.Model{
public class item{

public string objectId  { get; set; }
public string title  { get; set; }
public string price  { get; set; }
public string discount  { get; set; }
public string details  { get; set; }
public string type  { get; set; }
public string mall_name  { get; set; }
public string label  { get; set; }
public int hot  { get; set; }
public int love  { get; set; }
public int reward  { get; set; }
public string pic_a  { get; set; }
public string pic_b  { get; set; }
public string pic_c  { get; set; }
public string url  { get; set; }
public int commentCount  { get; set; }
public string state  { get; set; }
public string admin  { get; set; }
public DateTime updatedAt  { get; set; }
public DateTime createdAt  { get; set; }
public _User _User  { get; set; }
}
}