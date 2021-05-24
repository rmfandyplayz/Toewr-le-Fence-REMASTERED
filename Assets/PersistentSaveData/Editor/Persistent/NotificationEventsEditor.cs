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
using System.Linq;
using UnityEngine;
using PersistentSaveData.Events;
using System.Reflection;
using System;
using System.Collections.Generic;
using PersistentSaveData.Core;
using System.Collections;

namespace PersistentSaveDataEditor.Persistent
{
    public class NotificationEventsEditor
    {
        static readonly FieldInfo[] s_NotificationEventsFieldInfos = typeof(NotificationEvents).GetFields();

        internal static void DrawEventProperty(string title, SerializedProperty memberEventsProp)
        {
            if (memberEventsProp.arraySize > 0)
            {
                string typeName = memberEventsProp.type;
                GUIContent buttonLabel = new GUIContent(string.Format("{0} {1}[{2}]", title, typeName, memberEventsProp.arraySize), string.Format("Click to toggle expand {0} {1}.", title, typeName));
                Color openedColor = Color.gray;
                Color closedColor = Color.black;
                ComponentReferenceEditor.DrawButtonExpandableBox(memberEventsProp, buttonLabel, (prop) =>
                {
                    for (int i = 0; i < memberEventsProp.arraySize; i++)
                    {
                        SerializedProperty memberEventProperty = memberEventsProp.GetArrayElementAtIndex(i);

                        SerializedProperty memberNameProp = memberEventProperty.FindPropertyRelative("m_MemberName");
                        SerializedProperty memberTypeProp = memberEventProperty.FindPropertyRelative("m_MemberType");

                        GUIContent label = new GUIContent(string.Format("{0} {1} {2}", title, memberNameProp.stringValue, (MemberTypes)memberTypeProp.intValue));
                        EditorGUILayout.PropertyField(memberEventProperty, label, true);
                    }
                }, closedColor, openedColor);
            }
        }

        internal static void DrawEventPropertyAtIndex(string titlePrefix, SerializedProperty notificationsProp, int fieldInfoIndex)
        {
            SerializedProperty prop = notificationsProp.FindPropertyRelative(s_NotificationEventsFieldInfos[fieldInfoIndex].Name);
            DrawEventProperty(titlePrefix, prop);
        }

        static Type GetBaseTypeOfEnumerable(IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                //you'll have to decide what to do in this case
            }

            var genericEnumerableInterface = enumerable
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (genericEnumerableInterface == null)
            {
                //If we're in this block, the type implements IEnumerable, but not IEnumerable<T>;
                //you'll have to decide what to do here.

                //Justin Harvey's (now deleted) answer suggested enumerating the 
                //enumerable and examining the type of its elements; this 
                //is a good idea, but keep in mind that you might have a
                //mixed collection.
            }

            var elementType = genericEnumerableInterface.GetGenericArguments()[0];
            return elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? elementType.GetGenericArguments()[0]
                : elementType;
        }

        public delegate List<IMemberEvent> GetEventsListDelegate(NotificationEvents notificationEvents);
        public delegate void SetEventsListDelegate(NotificationEvents notificationEvents, List<IMemberEvent> events);

        internal static void AddNotifications(Member member, Type memberEventType, int dataIndex, NotificationEvents saveNotifications, NotificationEvents loadNotifications)
        {
            FieldInfo[] listFields = typeof(NotificationEvents).GetFields();
            for (int fieldIndex = 0; fieldIndex < listFields.Length; fieldIndex++)
            {
                IEnumerable saveListObject = (IEnumerable)listFields[fieldIndex].GetValue(saveNotifications);
                IEnumerable loadListObject = (IEnumerable)listFields[fieldIndex].GetValue(loadNotifications);

                Type saveListType = GetBaseTypeOfEnumerable(saveListObject);
                IMemberEvent saveItemInstance = (IMemberEvent)Activator.CreateInstance(saveListType);
                Type loadListType = GetBaseTypeOfEnumerable(loadListObject);
                IMemberEvent loadItemInstance = (IMemberEvent)Activator.CreateInstance(loadListType);

                if (memberEventType == saveItemInstance.type)
                {
                    MethodInfo loadGetEventsListMethodInfo = loadListType.GetMethod("GetEventsList");
                    MethodInfo saveGetEventsListMethodInfo = saveListType.GetMethod("GetEventsList");

                    GetEventsListDelegate saveGetEventsListDelegate = (GetEventsListDelegate)Delegate.CreateDelegate(typeof(GetEventsListDelegate), saveItemInstance, saveGetEventsListMethodInfo);
                    GetEventsListDelegate loadGetEventsListDelegate = (GetEventsListDelegate)Delegate.CreateDelegate(typeof(GetEventsListDelegate), loadItemInstance, loadGetEventsListMethodInfo);
                    List<IMemberEvent> saveList = saveGetEventsListDelegate.Invoke(saveNotifications);
                    List<IMemberEvent> loadList = loadGetEventsListDelegate.Invoke(loadNotifications);

                    AddToNotificationsMembers(member, dataIndex, saveList, saveItemInstance.GetType(), loadList, loadItemInstance.GetType());

                    MethodInfo loadSetEventsListMethodInfo = loadListType.GetMethod("SetEventsList");
                    MethodInfo saveSetEventsListMethodInfo = saveListType.GetMethod("SetEventsList");

                    SetEventsListDelegate saveSetEventsListDelegate = (SetEventsListDelegate)Delegate.CreateDelegate(typeof(SetEventsListDelegate), saveItemInstance, saveSetEventsListMethodInfo);
                    SetEventsListDelegate loadSetEventsListDelegate = (SetEventsListDelegate)Delegate.CreateDelegate(typeof(SetEventsListDelegate), loadItemInstance, loadSetEventsListMethodInfo);

                    saveSetEventsListDelegate.Invoke(saveNotifications, saveList);
                    loadSetEventsListDelegate.Invoke(loadNotifications, loadList);
                }
            }
        }

        internal static void UpdateUnusedNotificationEvents(List<Type> usedTypes, NotificationEvents saveNotifications, NotificationEvents loadNotifications)
        {
            FieldInfo[] listFields = typeof(NotificationEvents).GetFields();
            for (int fieldIndex = 0; fieldIndex < listFields.Length; fieldIndex++)
            {
                IEnumerable saveListObject = (IEnumerable)listFields[fieldIndex].GetValue(saveNotifications);
                IEnumerable loadListObject = (IEnumerable)listFields[fieldIndex].GetValue(loadNotifications);

                Type saveListType = GetBaseTypeOfEnumerable(saveListObject);
                IMemberEvent saveItemInstance = (IMemberEvent)Activator.CreateInstance(saveListType);
                Type loadListType = GetBaseTypeOfEnumerable(loadListObject);
                IMemberEvent loadItemInstance = (IMemberEvent)Activator.CreateInstance(loadListType);

                if (!usedTypes.Contains(saveItemInstance.type))
                {
                    MethodInfo clearMethodInfo = saveListObject.GetType().GetMethod("Clear");
                    clearMethodInfo.Invoke(saveListObject, null);
                }
                if (!usedTypes.Contains(loadItemInstance.type))
                {
                    MethodInfo clearMethodInfo = loadListObject.GetType().GetMethod("Clear");
                    clearMethodInfo.Invoke(loadListObject, null);
                }
            }
        }
        
        static IMemberEvent CreateInstance(Member member, int dataIndex, Type type)
        {
            IMemberEvent instance = (IMemberEvent)Activator.CreateInstance(type);
            instance.memberName = member.name;
            instance.memberType = member.type;
            instance.dataIndex = dataIndex;
            return instance;
        }

        static void AddToNotificationsMembers(Member member, int dataIndex, List<IMemberEvent> saveMemberEvents, Type saveType, List<IMemberEvent> loadMemberEvents, Type loadType)
        {
            Func<IMemberEvent, bool> _hasMemberName = (x) =>
            {
                return x.memberName.Equals(member.name);
            };

            if (saveMemberEvents.Where(_hasMemberName).Count() == 0)
            {
                IMemberEvent saveItem = CreateInstance(member, dataIndex, saveType);
                saveItem.memberName = member.name;
                saveItem.memberType = member.type;
                saveItem.dataIndex = dataIndex;
                saveMemberEvents.Add(saveItem);
            }

            if (loadMemberEvents.Where(_hasMemberName).Count() == 0)
            {
                IMemberEvent loadItem = CreateInstance(member, dataIndex, loadType);
                loadItem.memberName = member.name;
                loadItem.memberType = member.type;
                loadItem.dataIndex = dataIndex;
                loadMemberEvents.Add(loadItem);
            }
        }

        /*static void AddToNotifications<T>(Member member, int dataIndex, params List<T>[] notification)
            where T : IMemberEvent, new()
        {
            if (notification[0].Where(x => x.memberName.Equals(member.name)).Count() == 0)
            {
                T saveItem = new T();
                saveItem.memberName = member.name;
                saveItem.memberType = member.type;
                saveItem.dataIndex = dataIndex;
                notification[0].Add(saveItem);
            }

            if (notification[1].Where(x => x.memberName.Equals(member.name)).Count() == 0)
            {
                T loadItem = new T();
                loadItem.memberName = member.name;
                loadItem.memberType = member.type;
                loadItem.dataIndex = dataIndex;
                notification[1].Add(loadItem);
            }
        }*/

    }
}
