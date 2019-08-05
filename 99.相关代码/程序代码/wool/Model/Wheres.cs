using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wool.Model
{
    public class Wheres
    {
        public string key;
        public string operation;
        public object value;
        public object relation;
        public Wheres() { }
        public Wheres(string key, string operation, object value, object relation) {
            this.key = key;
            this.operation = operation;
            this.value = value;
            this.relation = relation;
        }
        public Wheres(string key, string operation, object value)
        {
            this.key = key;
            this.operation = operation;
            this.value = value;
            this.relation = "";
        }
        public void setField(string key, string operation, object value, object relation)
        {
            this.key = key;
            this.operation = operation;
            this.value =value;
            this.relation = relation;
        }
    }
}
