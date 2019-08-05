using System;
namespace wool.Model{
public class _User{

public string objectId  { get; set; }
public DateTime updatedAt  { get; set; }
public DateTime createdAt  { get; set; }
public string username  { get; set; }
public string password  { get; set; }
public string transaction_password  { get; set; }
public string sessionToken  { get; set; }
public string nickname  { get; set; }
public int credit  { get; set; }
public int overage  { get; set; }
public string avatar  { get; set; }
public bool sign_in  { get; set; }
public int shake_times  { get; set; }
public authData authData  { get; set; }
}
}