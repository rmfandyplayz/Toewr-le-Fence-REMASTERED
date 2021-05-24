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

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PersistentSaveData.Persistent
{
    [Serializable]
    public abstract class PersistentData : ISerializable
    {
        static readonly IFormatter s_BinaryFormatter = new BinaryFormatter();

        public static string MakeFullPath(string fileName)
        {
            //  Combines Unity persistent data path with 
            //  the incomming string to create a file name
            return Path.Combine(Application.persistentDataPath, fileName);
        }

        [SerializeField, Tooltip("Name of the settings file.\nNOTE: Do Not include the file path.")]
        protected string m_FileName = "";

        public PersistentData()
        {
        }

        public PersistentData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
        }

        public static T Load<T>(string fileName) where T : PersistentData, new()
        {
            fileName = GetFileName(fileName);

            if (!File.Exists(fileName)) return new T();

            T data = default(T);
            //  Loading game data from the file system.
            //  This is the recomended way so IDisposable can be
            //  call to close the file and release resources
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                //  Deserialize the data using the BinaryFormatter
                //  class to convert to custom class
                data = (T)s_BinaryFormatter.Deserialize(fileStream);
            }

            return data;
        }

        public abstract void Load();

        public void Save()
        {
            string fileName = GetFileName(m_FileName);

            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                s_BinaryFormatter.Serialize(fileStream, this);
            }
        }

        public static string GetFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("m_FileName PersistentData can not be null or empty.");
            }
            return MakeFullPath(fileName);
        }

        public static bool IsSaved(string fn)
        {
            return(File.Exists(GetFileName(fn)));
        }
    }
}