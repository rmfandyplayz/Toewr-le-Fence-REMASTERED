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
using System.Reflection;
using UnityEngine;

namespace PersistentSaveData.Formatters
{
    /// <summary>
    /// Formats data in PlayerPrefs format for save/load operations
    /// </summary>
    public class PlayerPrefsFileFormatter : Formatter
    {
        const int MAX_PREFS_CHECK_FOR_DELETE_COUNT = 50;

        const string MEMBER_INDEX = "MemberIndex";
        const string MEMBER_TYPE = "MemberType";
        const string MEMBER_NAME = "MemberName";
        const string MEMBER_VALUE = "MemberValue";

        /// <summary>
        /// Gets the FileFormat type for this formatter
        /// </summary>
        public override FileFormat fileFormat
        {
            get
            {
                FileFormats.Identifiers[FileFormat.Custom1] = fileFormatName;
                return FileFormat.Custom1;
            }
        }
        
        /// <summary>
        /// Gets the FileFormat name for this formatter
        /// </summary>
        public override string fileFormatName { get { return "PlayerPrefs"; } }

        /// <summary>
        /// Creates a full path file from the key name
        /// </summary>
        /// <param name="fileKey">Key name to use for file path reference</param>
        /// <returns>Full path file name</returns>
        public override string GetFullFileKey(string fileKey)
        {
            return string.Format("{0}_Length", fileKey);
        }

        /// <summary>
        /// Loads file data from file using the unique key
        /// </summary>
        /// <param name="fileKey">Key name to use for file path reference</param>
        /// <returns>Persistent data from file format</returns>
        public override PersistData Deserialize(string fileKey)
        {
            string keyLength = GetFullFileKey(fileKey);
            if (PlayerPrefs.HasKey(keyLength))
            {
                PersistData loadedPersistData = new PersistData();
                int length = PlayerPrefs.GetInt(keyLength);
                loadedPersistData.members = new Member[length];
                for (int i = 0; i < length; i++)
                {
                    loadedPersistData.members[i] = new Member();
                    loadedPersistData.members[i].index = GetInt(fileKey, i, MEMBER_INDEX);
                    loadedPersistData.members[i].type = (MemberTypes)GetInt(fileKey, i, MEMBER_TYPE);
                    loadedPersistData.members[i].name = GetString(fileKey, i, MEMBER_NAME);
                    loadedPersistData.members[i].value = GetString(fileKey, i, MEMBER_VALUE);
                }
                return loadedPersistData;
            }
            return default(PersistData);
        }

        /// <summary>
        /// Saves file data to file using the unique key
        /// </summary>
        /// <param name="fileKey">Key name to use for file path reference</param>
        /// <param name="persistData">Data to persist</param>
        public override void Serialize(string fileKey, PersistData persistData)
        {
            string keyLength = GetFullFileKey(fileKey);
            PlayerPrefs.SetInt(keyLength, persistData.members.Length);
            for (int i = 0; i < persistData.members.Length; i++)
            {
                SetInt(fileKey, i, MEMBER_INDEX, persistData.members[i].index);
                SetInt(fileKey, i, MEMBER_TYPE, (int)persistData.members[i].type);
                SetString(fileKey, i, MEMBER_NAME, persistData.members[i].name);
                SetString(fileKey, i, MEMBER_VALUE, persistData.members[i].value);
            }
        }

        /// <summary>
        /// Check that the file exists and contains data
        /// </summary>
        /// <param name="fileKey">Key name to use for file path reference</param>
        /// <returns>True if the file exists and has data</returns>
        public override bool HasData(string fileKey)
        {
            return PlayerPrefs.HasKey(GetFullFileKey(fileKey));
        }

        /// <summary>
        /// Deletes a data file
        /// </summary>
        /// <param name="fileKey">Key name to use for file path reference</param>
        /// <returns>True is the file was removed</returns>
        public override bool DeleteData(string fileKey)
        {
            if (HasData(fileKey))
            {
                for (int i = 0; i < MAX_PREFS_CHECK_FOR_DELETE_COUNT; i++)
                {
                    DeleteKey(fileKey, i, MEMBER_INDEX);
                    DeleteKey(fileKey, i, MEMBER_TYPE);
                    DeleteKey(fileKey, i, MEMBER_NAME);
                    DeleteKey(fileKey, i, MEMBER_VALUE);
                }

                PlayerPrefs.DeleteKey(GetFullFileKey(fileKey));

                return true;
            }
            return false;
        }

        string GetSubKey(string fileKey, int index, string name)
        {
            return string.Format("{0}_{1}_{2}", GetFullFileKey(fileKey), index, name);
        }

        void DeleteKey(string fileKey, int index, string name)
        {
            string key = GetSubKey(fileKey, index, name);
            if (PlayerPrefs.HasKey(key)) PlayerPrefs.DeleteKey(key);
        }

        int GetInt(string fileKey, int index, string name)
        {
            return PlayerPrefs.GetInt(GetSubKey(fileKey, index, name));
        }

        string GetString(string fileKey, int index, string name)
        {
            return PlayerPrefs.GetString(GetSubKey(fileKey, index, name));
        }

        void SetInt(string fileKey, int index, string name, int value)
        {
            PlayerPrefs.SetInt(GetSubKey(fileKey, index, name), value);
        }

        void SetString(string fileKey, int index, string name, string value)
        {
            PlayerPrefs.SetString(GetSubKey(fileKey, index, name), value);
        }
    }

}