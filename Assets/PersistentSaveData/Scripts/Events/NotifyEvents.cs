#region Author
/*
     
     Jones St. Lewis Cropper (caLLow)
     
     Another caLLowCreation
     
     Visit us on Google+ and other social media outlets @caLLowCreation
     
     Thanks for using our product.
     
     Send questions/comments/concerns/requests to 
      e-mail: caLLowCreation@gmail.com
      subject: Persistent Save Data
     
*/
#endregion

using PersistentSaveData.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace PersistentSaveData.Events
{
    public class NotifyEvents : TypeLoader
    {
        static Dictionary<Type, IMemberEvent> s_Events = new Dictionary<Type, IMemberEvent>();

        public static Dictionary<Type, IMemberEvent> Events
        {
            get
            {
                if (s_Events.Count == 0)
                {
                    List<Type> types = new List<Type>();
                    List<Assembly> assemblys = new List<Assembly>()
                    {
                        Assembly.GetCallingAssembly(),
                        Assembly.GetAssembly(typeof(IMemberEvent))
                    };
                    for (int i = 0; i < assemblys.Count; i++)
                    {
                        List<Type> typeList = assemblys[i].GetTypes()
                            .Where(x =>
                            {
                                bool isAssignable = typeof(IMemberEvent).IsAssignableFrom(x);
                                return x != null && isAssignable;
                            }).ToList();
                        types.AddRange(typeList);
                    }
                    
                    CreateAssemblyEvents(types);
                }
                return s_Events;
            }
        }

        static void CreateAssemblyEvents(IEnumerable<Type> types)
        {
            s_Events.Clear();
            foreach (var type in types)
            {
                if (type.Equals(typeof(IMemberEvent)))
                    continue;
                IMemberEvent memberEvent = (IMemberEvent)Activator.CreateInstance(type);
                IMemberEvent tempEvent = null;
                if (!s_Events.TryGetValue(memberEvent.type, out tempEvent))
                {
                    s_Events.Add(memberEvent.type, memberEvent);
                }
            }
        }

        public static IMemberEvent TryGetEvent(Type type)
        {
            IMemberEvent memberEvent = null;
            if (Events.TryGetValue(type, out memberEvent))
            {
                return Events[type];
            }
            else
            {
                //Debug.LogException(new UnityException(string.Format("We're sorry, Formatters FileFormat {0} is not supported at this time.  Please contact the developer to request {0} type support.\nThank You.", fileFormat)));
                return MemberEvent<object>.Empty;
            }
        }
        
    }
    
}