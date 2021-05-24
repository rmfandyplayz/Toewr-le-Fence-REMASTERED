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
using PersistentSaveData.Core.DefaultNames;
using PersistentSaveData.Core.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PersistentSaveData.DefaultNames
{

    public class ReferenceDefaults : TypeLoader
    {
        static Dictionary<Type, DefaultName> s_DefaultNames = new Dictionary<Type, DefaultName>();

        public static Dictionary<Type, DefaultName> DefaultNames
        {
            get
            {
                if (s_DefaultNames.Count == 0)
                {
                    List<Type> types = TypeLoader.LoadAssemblyTypes<DefaultName>(Assembly.GetCallingAssembly(), Assembly.GetAssembly(typeof(DefaultName)));
                    LoadAssemblyDefaultNames(types);
                }
                return s_DefaultNames;
            }
        }

        static void LoadAssemblyDefaultNames(IEnumerable<Type> types)
        {
            s_DefaultNames.Clear();
            foreach (var type in types)
            {
                DefaultName defaultName = (DefaultName)Activator.CreateInstance(type);
                s_DefaultNames.Add(defaultName.type, defaultName);
            }
        }

        public static DefaultName TryGetDefaults(Type defaultType)
        {
            DefaultName defaults = null;
            if (DefaultNames.TryGetValue(defaultType, out defaults))
            {
                return DefaultNames[defaultType];
            }
            else
            {
                //Debug.LogException(new UnityException(string.Format("We're sorry, DefaultName type {0} is not supported at this time.  Please contact the developer to request {0} type support.\nThank You.", defaultType)));
                return null;
            }
        }
    }

}