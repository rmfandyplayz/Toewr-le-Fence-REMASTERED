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

using UnityEditor;
using PersistentSaveData.Persistent;
using PersistentSaveData.Core.Formatters;
using PersistentSaveData.Formatters;
using System.Collections.Generic;
using System;
using UnityEngine;
using PersistentSaveData.DefaultNames;
using PersistentSaveData.Core;

namespace PersistentSaveDataEditor.Persistent
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PersistentReference), true)]
    public class PersistentReferenceEditor : ComponentReferenceEditor
    {
        string m_FileName = string.Empty;

        public SerializedProperty referenceProperty {  get { return m_ReferenceProperty; } }

        public override void OnInspectorGUI()
        {
            TryItCompatibilityEditor.ShowTryIt.DrawOpenTryItButton(PersistentSettings.Instance.GetTryIt());
            base.OnInspectorGUI();

            SetComponentReferenceAndFilename(m_ComponentReference);
            m_FileName = m_FileNameProperty.stringValue;
        }

        public static void SetComponentReferenceAndFilename(ComponentReference componentReference)
        {
            Type type = componentReference.reference ? componentReference.reference.GetType() : null;
            if (type != null)
            {
                PersistentReference persistentReference = (componentReference as PersistentReference);
                Component testComponent = persistentReference.GetComponent(type);
                if (testComponent)
                {
                    if (componentReference.reference != testComponent)
                    {
                        componentReference.reference = testComponent;
                        componentReference.fileName = string.Format(@"{0}", Guid.NewGuid());
                        //Debug.Log("m_ComponentReference: " + testComponent.gameObject);
                        EditorUtility.SetDirty(componentReference);
                    }
                }
            }
        }

        protected override void DeleteFileData()
        {
            FileFormatters.DeleteSavedData(m_FileName);
        }

        public override bool TryInitMemberInfosAndOptions()
        {
            if(!base.TryInitMemberInfosAndOptions())
            {
                return false;
            }
            m_MemberInfos = m_ComponentReference.GetAvailableMemberInfos();
            return true;
        }

        protected override FileCount SelectedFileTypesCount()
        {
            FileCount fileCount = new FileCount();
            FileFormatters.DoForFormatter(m_ComponentReference.savePersistence.fileFormat, (eValue, f) =>
            {
                fileCount.select++;
                if (f.HasData(m_FileNameProperty.stringValue)) fileCount.has++;
            });
            return fileCount;
        }
        
        protected override bool HasSaveFileFormat()
        {
            bool hasSaveFileFormat = false;
            FileFormatters.DoForFormatter(m_ComponentReference.savePersistence.fileFormat, (eValue, f) =>
            {
                hasSaveFileFormat |= f.fileFormat == m_ComponentReference.loadPersistence.fileFormat;
            });
            return hasSaveFileFormat;
        }

        protected override void AddFileFormats()
        {
            base.AddFileFormats();
            FileFormatters.DoForFormatter(m_ComponentReference.savePersistence.fileFormat, (eValue, f) =>
            {
                if(!string.IsNullOrEmpty(FileFormats.Identifiers[(FileFormat)eValue]))
                {
                    m_FileFormats.Add(f.HasData(m_FileNameProperty.stringValue) ? FileFormats.Identifiers[(FileFormat)eValue] : FileFormats.Identifiers[(FileFormat)eValue] + "*");
                }
            });
        }

        protected override void AddAllMembers()
        {
            Type typeKey = m_ComponentReference.reference.GetType();
            bool hasDefaultNames = ReferenceDefaults.TryGetDefaults(typeKey) != null;
            m_MemberInfos = m_ComponentReference.GetAvailableMemberInfos();
            for (int i = 0; i < m_MemberInfos.Length; i++)
            {
                if (hasDefaultNames && !ReferenceDefaults.DefaultNames[typeKey].names.Contains(m_MemberInfos[i].Name)) continue;

                int index = m_MembersProperty.arraySize;
                m_MembersProperty.InsertArrayElementAtIndex(index);
                SerializedMember.FindMemberProperties(m_MembersProperty.GetArrayElementAtIndex(index));
                SerializedMember.ClearPropertyValues();
                SerializedMember.Index = i;
                SerializedMember.Name = m_MemberInfos[i].Name;
                SerializedMember.Type = m_MemberInfos[i].MemberType;
                SerializedMember.Value = m_ComponentReference.ApplyModified(SerializedMember.Type, SerializedMember.Name);
            }
        }
    }
}
