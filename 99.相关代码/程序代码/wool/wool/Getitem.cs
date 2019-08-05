using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wool.Model;
using wool.BLL;

namespace wool
{
    public class Getitem
    {
        public item GetItem(string objectId)
        {
            itemBLL bll = new itemBLL();
            try
            {
                var model = bll.QuerySingleById(objectId);
                return model;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}