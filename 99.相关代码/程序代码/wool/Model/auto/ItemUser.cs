using System;
namespace wool.Model{
public class ItemUser{

public string objectId  { get; set; }
public bool love  { get; set; }
public bool dislove  { get; set; }
public bool fav  { get; set; }
public DateTime updatedAt  { get; set; }
public DateTime createdAt  { get; set; }
public _User _User  { get; set; }
public item item  { get; set; }
}
}