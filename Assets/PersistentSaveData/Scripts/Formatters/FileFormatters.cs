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
using PersistentSaveData.Core.Formatters;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PersistentSaveData.Formatters
{

    public class FileFormatters : TypeLoader
    {
        static Dictionary<FileFormat, Formatter> s_Formatters = new Dictionary<FileFormat, Formatter>();

        public static Dictionary<FileFormat, Formatter> Formatters
        {
            get
            {
                if (s_Formatters.Count == 0)
                {
                    Formatter.persistentDataPath = Application.persistentDataPath;

                    List<Type> types = TypeLoader.LoadAssemblyTypes<Formatter>(Assembly.GetCallingAssembly(), Assembly.GetAssembly(typeof(Formatter)));
                    CreateAssemblyFormatters(types);
                }
                return s_Formatters;
            }
        }

        public static void DeleteSavedData(string fileKey)
        {
            foreach (var formatter in FileFormatters.Formatters)
            {
                bool deleted = formatter.Value.DeleteData(fileKey);
                if (deleted)
                {
                    string formatName = formatter.Value.fileFormatName;
                    Debug.LogFormat("{0} deleted = {1} {2}", formatName, deleted, formatter.Value.GetFullFileKey(fileKey));
                }
            }
        }

        static void CreateAssemblyFormatters(IEnumerable<Type> types)
        {
            s_Formatters.Clear();
            foreach (var type in types)
            {
                Formatter formatter = (Formatter)Activator.CreateInstance(type);
                s_Formatters.Add(formatter.fileFormat, formatter);
            }
        }

        static Formatter TryGetFormatter(FileFormat fileFormat)
        {
            Formatter formatter = null;
            if (Formatters.TryGetValue(fileFormat, out formatter))
            {
                return Formatters[fileFormat];
            }
            else
            {
                //Debug.LogException(new UnityException(string.Format("We're sorry, Formatters FileFormat {0} is not supported at this time.  Please contact the developer to request {0} type support.\nThank You.", fileFormat)));
                return Formatter.Empty;
            }
        }

        public static void DoForFormatter(FileFormat fileFormat, Action<int, Formatter> action)
        {
            Array values = Enum.GetValues(typeof(FileFormat));
            for (int i = 0; i < values.Length; i++)
            {
                FileFormat checkFileFormat = (FileFormat)values.GetValue(i);
                if ((fileFormat & checkFileFormat) == checkFileFormat)
                {
                    action.Invoke((int)values.GetValue(i), TryGetFormatter(checkFileFormat));
                }
            }
        }
    }

}