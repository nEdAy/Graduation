using System;
namespace wool.Model{
public class OverageHistory{

public string objectId  { get; set; }
public DateTime createdAt  { get; set; }
public DateTime updatedAt  { get; set; }
public string userId  { get; set; }
public string originUserId  { get; set; }
public string type  { get; set; }
public int before  { get; set; }
public int after  { get; set; }
public int change  { get; set; }
}
}