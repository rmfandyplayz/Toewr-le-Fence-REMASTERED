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
using System.Collections.Generic;
using PersistentSaveData.Parsers;
using System.Reflection;
using System.Collections;

namespace PersistentSaveData.Events
{

    [Serializable]
    public partial class NotificationEvents
    {
        static readonly Type s_Type = typeof(NotificationEvents);

        public List<Int32Event> int32Event = null;
        public List<SingleEvent> singleEvent = null;
        public List<BooleanEvent> booleanEvent = null;
        public List<StringEvent> stringEvent = null;
        public List<Vector3Event> vector3Event = null;

        public void ClearEvents()
        {
            FieldInfo[] listFields = s_Type.GetFields();
            for (int fieldIndex = 0; fieldIndex < listFields.Length; fieldIndex++)
            {
                IEnumerable listObject = (IEnumerable)listFields[fieldIndex].GetValue(this);
                MethodInfo methodInfo = listObject.GetType().GetMethod("Clear");
                methodInfo.Invoke(listObject, null);
            }
        }

        public int CountEvents()
        {
            FieldInfo[] listFields = s_Type.GetFields();
            int count = 0;
            for (int fieldIndex = 0; fieldIndex < listFields.Length; fieldIndex++)
            {
                IEnumerable listObject = (IEnumerable)listFields[fieldIndex].GetValue(this);
                MethodInfo methodInfo = listObject.GetType().GetMethod("get_Count");
                count += (int)methodInfo.Invoke(listObject, null);
            }
            return count;
        }

        public int CountListsItems()
        {
            FieldInfo[] listFields = s_Type.GetFields();
            int count = 0;
            for (int fieldIndex = 0; fieldIndex < listFields.Length; fieldIndex++)
            {
                IEnumerable listObject = (IEnumerable)listFields[fieldIndex].GetValue(this);
                MethodInfo methodInfo = listObject.GetType().GetMethod("Clear");
                methodInfo.Invoke(listObject, null);
            }
            for (int fieldIndex = 0; fieldIndex < listFields.Length; fieldIndex++)
            {
                IEnumerable listObject = (IEnumerable)listFields[fieldIndex].GetValue(this);
                MethodInfo methodInfo = listObject.GetType().GetMethod("get_Count");
                count += (int)methodInfo.Invoke(listObject, null);
            }
            return count;
        }

        public void InvokeMemberValues(ComponentReference componentReference)
        {
            FieldInfo[] listFields = s_Type.GetFields();
            for (int fieldIndex = 0; fieldIndex < listFields.Length; fieldIndex++)
            {
                IEnumerable listObject = (IEnumerable)listFields[fieldIndex].GetValue(this);
                InvokeMemberValue(listObject, componentReference);
            }
        }

        static Dictionary<string, string> s_FormattedValues = new Dictionary<string, string>();

        void InvokeMemberValue(IEnumerable memberEvents, ComponentReference componentReference)
        {
            int index = 0;
            foreach (IMemberEvent item in memberEvents)
            {
                int dataIndex = item.dataIndex;
                //Debug.Log(item.memberName);
                Type memberEventType = ValueParsers.GetMemberInfoType(componentReference.reference,
                    componentReference.persistData.members[dataIndex].type,
                    componentReference.persistData.members[dataIndex].name);

                if(item.GetEventCount() > 0)
                {
                    string setMethodName = item.GetMethodName(index);
                    UnityEngine.Object methodTarget = item.GetMethodTarget(index);
                    Type methodType = item.GetMethodTarget(index).GetType();
                    MethodInfo setMethodInfo = methodType.GetMethod(setMethodName, new Type[] { typeof(string) });
                    
                    if(setMethodInfo != null)
                    {
                        ParameterInfo[] setParameterInfos = setMethodInfo.GetParameters();
                        if (setParameterInfos.Length == 1 &&
                            setParameterInfos[0].ParameterType.Equals(typeof(string)) &&
                            !setParameterInfos[0].ParameterType.Equals(memberEventType))
                        {

                            string getMethodName = setMethodName.Replace("set", "get");
                            MethodInfo getMethodInfo = methodType.GetMethod(getMethodName);
                            string getValue = (string)getMethodInfo.Invoke(methodTarget, null);

                            string setValue = componentReference.persistData.members[dataIndex].value;

                            string keyName = componentReference.GetInstanceID() + "." + methodTarget.name + "." + setMethodName;
                            //Debug.Log("key: " + keyName);
                            if (s_FormattedValues.ContainsKey(keyName))
                            {
                                setValue = string.Format(s_FormattedValues[keyName], setValue);
                            }
                            else if (getValue.Contains("{0}"))
                            {
                                s_FormattedValues.Add(keyName, getValue);
                                setValue = string.Format(getValue, setValue);
                            }

                            setMethodInfo.Invoke(methodTarget, new object[] { setValue });
                            return;
                        }
                    }

                    setMethodInfo = methodType.GetMethod(setMethodName, new Type[] { typeof(bool) });
                    if (setMethodInfo != null)
                    {
                        ParameterInfo[] setParameterInfos = setMethodInfo.GetParameters();
                        if (setParameterInfos.Length == 1 &&
                            setParameterInfos[0].ParameterType.Equals(typeof(bool)) &&
                            !setParameterInfos[0].ParameterType.Equals(memberEventType))
                        {
                            bool finalValue = false;
                            string setValue = componentReference.persistData.members[dataIndex].value;
                            int intNum;
                            float floatNum;
                            bool boolNum;
                            if (int.TryParse(setValue, out intNum))
                            {
                                if (intNum != 0) finalValue = true;
                            }
                            else if (float.TryParse(setValue, out floatNum))
                            {
                                if (floatNum != 0.0f) finalValue = true;
                            }
                            else if (bool.TryParse(setValue, out boolNum))
                            {
                                finalValue = boolNum;
                            }
                            setMethodInfo.Invoke(methodTarget, new object[] { finalValue });
                            return;
                        }
                    }
                    if (memberEventType == item.type)
                    {
                        item.InvokeMemberValue(componentReference.persistData.members[dataIndex]);
                    }
                }
                index++;
            }
        }
    }
}