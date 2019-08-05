using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wool.Model;
using wool.BLL;

namespace wool
{
    public class Getuser
    {
        _UserBLL bll = new _UserBLL();
        public _User GetUser(string objectId)
        {
            
            try
            {
                    _User model = bll.QuerySingleById(objectId);
                    return model;


            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}