using System;
namespace wool.Model{
public class RechargeHistory{

public string objectId  { get; set; }
public DateTime updatedAt  { get; set; }
public DateTime createdAt  { get; set; }
public string userId  { get; set; }
public string name  { get; set; }
public string body  { get; set; }
public string create_time  { get; set; }
public string out_trade_no  { get; set; }
public string transaction_id  { get; set; }
public string pay_type  { get; set; }
public double? total_fee  { get; set; }
public string trade_state  { get; set; }
}
}