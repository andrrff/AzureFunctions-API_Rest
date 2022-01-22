using System;
using System.Collections.Generic;
using System.Text;

namespace AndrrffCRUD.Model
{
    class Routers
    {
        public string Data
        {
            get { return "/data"; }
        }

        public string ViewRaw(string _value)
        {
            return "/" + this.Data + "/" + _value + "/view_raw";
        }
        public string Update
        {
            get { return "/mode_edit"; }
        }

        public string Delete(string _value)
        {
            return "/" + this.Data + "/" + _value;
        }
    }
}
