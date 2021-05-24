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

using System.Linq;
using PersistentSaveData.Core;
using PersistentSaveData.Formatters;
using PersistentSaveData.Parsers;
using System.Reflection;
using System;

namespace PersistentSaveData.Persistent
{
    public class PersistentReference : ComponentReference
    {
        public static event Action<PersistentReference> OnBeforeSave;
        public static event Action<PersistentReference> OnBeforeLoad;
        public static event Action<PersistentReference> OnAfterSave;
        public static event Action<PersistentReference> OnAfterLoad;

        public override void Load()
        {
            base.Load();
            if(OnBeforeLoad != null)
            {
                OnBeforeLoad.Invoke(this);
            }
            PersistData loadedPersistData = default(PersistData);

            FileFormatters.DoForFormatter(m_LoadPersistence.fileFormat, (eValue, f) => loadedPersistData = f.Deserialize(m_FileName));

            ApplyFromLoadedData(loadedPersistData);

            for (int index = 0; index < m_PersistData.members.Length; index++)
            {
                ValueParsers.LoadParser(m_Reference,
                    m_PersistData.members[index].value,
                    m_PersistData.members[index].type,
                    m_PersistData.members[index].name);
            }
            if(OnAfterLoad != null)
            {
                OnAfterLoad.Invoke(this);
            }
        }

        public override void Save()
        {
            base.Save();
            if (OnBeforeSave != null)
            {
                OnBeforeSave.Invoke(this);
            }

            ApplyForSaveData(ValueParsers.SaveParser);

            FileFormatters.DoForFormatter(m_SavePersistence.fileFormat, (eValue, f) => f.Serialize(m_FileName, m_PersistData));
            if (OnAfterSave != null)
            {
                OnAfterSave.Invoke(this);
            }
        }

        public override string ApplyModified(MemberTypes memberType, string memberName)
        {
            return ValueParsers.SaveParser(m_Reference, memberType, memberName);
        }

        public override MemberInfo[] GetAvailableMemberInfos()
        {
            if (!m_Reference) return null;
            return m_Reference.GetType().GetMembers(ValueParsers.BINDING_FLAGS)
                .Where(ValueParsers.GetSupportedMembers()).ToArray();
        }
    }
}