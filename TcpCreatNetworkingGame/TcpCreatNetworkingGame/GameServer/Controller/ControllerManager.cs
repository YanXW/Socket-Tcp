using System;
using System.Collections.Generic;
using Common;
using System.Text;
using System.Reflection;
using GameServer.Servers;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Server server;

        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }
        public void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController); //初始化默认的controller
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode,string data,Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);//通过RequestCode获取对应的Controller
            if (!isGet)
            {

                Console.WriteLine("无法得到[" + requestCode + "]所对应的controller，无法处理请求！");
                return;
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode); //枚举名转化为方法
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[警告]在Controller [" + controller.GetType() + "]没有对应的处理方法：[" + methodName,"]");
            }
            object[] parametates = new object[] { data,client ,server };
            object o = mi.Invoke(controller,parametates);

            if (o == null || string.IsNullOrEmpty(o as string)) //如果请求为空，则不做处理
            {
                return;
            }//如果返回的字符串不为空则给客户端发送响应
            server.SendResponse(client, requestCode, o as string); 
       
        }


    }
}
