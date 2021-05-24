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
using UnityEngine;
using PersistentSaveData.Events;
using System.Reflection;
using System.Collections.Generic;
using System;
using PersistentSaveData.Core;
using PersistentSaveData.Parsers;
using System.Collections;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;

namespace PersistentSaveDataEditor.Persistent
{

    [CustomEditor(typeof(PersistentNotification), true)]
    public class PersistentNotificationEditor : Editor
    {
        static readonly FieldInfo[] s_NotificationEventsFieldInfos = typeof(NotificationEvents).GetFields();

        SerializedProperty m_PersistentReferenceProp = null;

        //SerializedProperty asStringEventProp = null;

        SerializedProperty m_BeforeSaveNotificationsProp = null;
        SerializedProperty m_AfterSaveNotificationsProp = null;

        SerializedProperty m_BeforeLoadNotificationsProp = null;
        SerializedProperty m_AfterLoadNotificationsProp = null;

        SerializedProperty m_BeforeSaveProperty;
        SerializedProperty m_AfterSaveProperty;

        SerializedProperty m_BeforeLoadProperty;
        SerializedProperty m_AfterLoadProperty;

        PersistentNotification m_PersistentNotification = null;

        void OnEnable()
        {
            m_PersistentNotification = (PersistentNotification)target;
            m_PersistentReferenceProp = serializedObject.FindProperty("m_PersistentReference");

            m_BeforeSaveNotificationsProp = serializedObject.FindProperty("m_BeforeSaveNotifications");
            m_AfterSaveNotificationsProp = serializedObject.FindProperty("m_AfterSaveNotifications");

            m_BeforeLoadNotificationsProp = serializedObject.FindProperty("m_BeforeLoadNotifications");
            m_AfterLoadNotificationsProp = serializedObject.FindProperty("m_AfterLoadNotifications");

            m_BeforeSaveProperty = serializedObject.FindProperty("m_BeforeSave");
            m_AfterSaveProperty = serializedObject.FindProperty("m_AfterSave");

            m_BeforeLoadProperty = serializedObject.FindProperty("m_BeforeLoad");
            m_AfterLoadProperty = serializedObject.FindProperty("m_AfterLoad");

            //UnityEventTools.AddPersistentListener(m_PersistentNotification.notificationEvents);
            AddNewNotifications();
            //m_PersistentNotification.afterSaveNotifications.TestPropertyPull();
        }

        public override void OnInspectorGUI()
        {
            TryItCompatibilityEditor.ShowTryIt.DrawOpenTryItButton(PersistentSettings.Instance.GetTryIt());
            //base.OnInspectorGUI();
            bool persistentReferenceChanged = false;
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_PersistentReferenceProp, true);
            if (EditorGUI.EndChangeCheck())
            {
                persistentReferenceChanged = true;
            }
            if(persistentReference)
            {
                //EditorGUILayout.PropertyField(asStringEventProp, true);

                Color openedColor = Color.gray;
                Color closedColor = Color.black;

                string buttonTitle = "Event Toggles";
                GUIContent buttonLabel = new GUIContent(buttonTitle, "Click to toggle expanded view.");
                GUIStyle buttonStyle = new GUIStyle(EditorStyles.helpBox);
                buttonStyle.fontSize = 11;
                buttonStyle.fontStyle = FontStyle.Bold;
                buttonStyle.alignment = TextAnchor.LowerCenter;
                ComponentReferenceEditor.DrawButtonExpandableBox(m_PersistentReferenceProp, buttonLabel, (prop) =>
                {
                    m_BeforeSaveProperty.boolValue = EditorGUILayout.Toggle(new GUIContent(m_BeforeSaveProperty.displayName, "Show Before Save Events List"), m_BeforeSaveProperty.boolValue);
                    m_AfterSaveProperty.boolValue = EditorGUILayout.Toggle(new GUIContent(m_AfterSaveProperty.displayName, "Show After Save Events List"), m_AfterSaveProperty.boolValue);
                    m_BeforeLoadProperty.boolValue = EditorGUILayout.Toggle(new GUIContent(m_BeforeLoadProperty.displayName, "Show Before Load Events List"), m_BeforeLoadProperty.boolValue);
                    m_AfterLoadProperty.boolValue = EditorGUILayout.Toggle(new GUIContent(m_AfterLoadProperty.displayName, "Show After Load Events List"), m_AfterLoadProperty.boolValue);
                }, closedColor, openedColor, buttonStyle);
            }

            for (int i = 0; i < s_NotificationEventsFieldInfos.Length; i++)
            {
                if (m_BeforeSaveProperty.boolValue)
                {
                    NotificationEventsEditor.DrawEventPropertyAtIndex("OnBeforeSave", m_BeforeSaveNotificationsProp, i);
                }
                if (m_AfterSaveProperty.boolValue)
                {
                    NotificationEventsEditor.DrawEventPropertyAtIndex("OnAfterSave", m_AfterSaveNotificationsProp, i);
                }
                if (m_BeforeLoadProperty.boolValue)
                {
                    NotificationEventsEditor.DrawEventPropertyAtIndex("OnBeforeLoad", m_BeforeLoadNotificationsProp, i);
                }
                if (m_AfterLoadProperty.boolValue)
                {
                    NotificationEventsEditor.DrawEventPropertyAtIndex("OnAfterLoad", m_AfterLoadNotificationsProp, i);
                }
            }

            serializedObject.ApplyModifiedProperties();
            if(persistentReferenceChanged)
            {
                m_PersistentNotification.beforeSaveNotifications.ClearEvents();
                m_PersistentNotification.afterSaveNotifications.ClearEvents();
                m_PersistentNotification.beforeLoadNotifications.ClearEvents();
                m_PersistentNotification.afterLoadNotifications.ClearEvents();

                AddNewNotifications();
            }
        }

        void AddNewNotifications()
        {
            UpdateNotificationEvents();
            EditorUtility.SetDirty(m_PersistentNotification);
            Repaint();
        }
        public PersistentReference persistentReference { get { return m_PersistentReferenceProp.objectReferenceValue as PersistentReference; } }

        public void UpdateNotificationEvents()
        {
            if (persistentReference)
            {
                Undo.RegisterCompleteObjectUndo(m_PersistentNotification, "Update Notification Events");
                List<Type> usedTypes = new List<Type>();
                for (int i = 0; i < persistentReference.persistData.members.Length; i++)
                {
                    Member member = persistentReference.persistData.members[i];
                    Type memberEventType = ValueParsers.GetMemberInfoType(persistentReference.reference, member.type, member.name);

                    NotificationEventsEditor.AddNotifications(member, memberEventType, i, m_PersistentNotification.beforeSaveNotifications, m_PersistentNotification.beforeLoadNotifications);
                    NotificationEventsEditor.AddNotifications(member, memberEventType, i, m_PersistentNotification.afterSaveNotifications, m_PersistentNotification.afterLoadNotifications);
                    usedTypes.Add(memberEventType);
                }
                NotificationEventsEditor.UpdateUnusedNotificationEvents(usedTypes, m_PersistentNotification.beforeSaveNotifications, m_PersistentNotification.beforeLoadNotifications);
                NotificationEventsEditor.UpdateUnusedNotificationEvents(usedTypes, m_PersistentNotification.afterSaveNotifications, m_PersistentNotification.afterLoadNotifications);
            }
        }
    }
}
