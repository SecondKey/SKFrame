using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    public class MsgBindingInitializer : IInitializer
    {
        public void Initialization(IInitializedObject initializedObject)
        {
            INotifiedObject notifiedObject = initializedObject as INotifiedObject;
            notifiedObject.MsgHandlers = new Dictionary<string, IMsgHandler>();
            foreach (MethodInfo method in notifiedObject.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(MsgBindingAttribute), true)))
            {
                MsgBindingAttribute attribute = method.GetCustomAttribute(typeof(MsgBindingAttribute)) as MsgBindingAttribute;
                if (!notifiedObject.MsgHandlers.ContainsKey(attribute.TargetHandler))
                {
                    notifiedObject.MsgHandlers.Add(attribute.TargetHandler, Basic.Instence.GetInstence<IMsgHandler>("", attribute.TargetHandler));
                }
                notifiedObject.MsgHandlers[attribute.TargetHandler].MsgEventHandlers.Add(attribute.MsgToken, Delegate.CreateDelegate(typeof(MsgEventHandler), method) as MsgEventHandler);
                Basic.Instence.GetSingleton<MsgManager>(AppConst.RootLevel, notifiedObject.MsgStstem).RegistMsg(notifiedObject, attribute.MsgToken);
            }
        }
    }
}