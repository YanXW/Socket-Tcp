using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    abstract class BaseController
    {
        RequestCode requestCode = RequestCode.None ;
        ActionCode actionCode = ActionCode.None;
     
        public RequestCode RequestCode
        {// get => requestCode; set => requestCode = value;
            get { return requestCode; }
        }

        public virtual string DefaultHanDle(String data,Client client ,Server   servers) {
            return null;
        }
    }
}
