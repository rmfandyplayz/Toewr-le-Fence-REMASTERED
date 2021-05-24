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
using PersistentSaveData.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PersistentSaveData.Parsers
{
    public class ValueParsers : TypeLoader
    {
        public const BindingFlags BINDING_FLAGS = BindingFlags.Public /*| BindingFlags.NonPublic*/ | BindingFlags.Instance;

        static Dictionary<Type, Parser> s_Parsers = new Dictionary<Type, Parser>();

        public static Dictionary<Type, Parser> Parsers
        {
            get
            {
                if(s_Parsers.Count == 0)
                {
                    List<Type> types = TypeLoader.LoadAssemblyTypes<Parser>(Assembly.GetCallingAssembly(), Assembly.GetAssembly(typeof(Parser)));
                    LoadAssemblyParsers(types);
                }
                return s_Parsers;
            }
        }

        static void LoadAssemblyParsers(IEnumerable<Type> types)
        {
            s_Parsers.Clear();
            foreach (var type in types)
            {
                Parser parser = (Parser)Activator.CreateInstance(type);
                s_Parsers.Add(parser.ParseType, parser);
            }
        }

        public static string SaveParser(Component reference, MemberTypes memberType, string memberName)
        {
            if (reference)
            {
                if (memberType == MemberTypes.Field)
                {
                    FieldInfo fieldInfo = reference.GetType().GetField(memberName, BINDING_FLAGS);
                    if(fieldInfo == null)
                    {
                        return InternalSaveParser(Parser.Empty.GetType(), string.Empty);
                    }
                    return InternalSaveParser(fieldInfo.FieldType, fieldInfo.GetValue(reference));
                }
                else if (memberType == MemberTypes.Property)
                {
                    PropertyInfo propertyInfo = reference.GetType().GetProperty(memberName, BINDING_FLAGS);
                    if (propertyInfo == null)
                    {
                        return InternalSaveParser(Parser.Empty.GetType(), string.Empty);
                    }
                    return InternalSaveParser(propertyInfo.PropertyType, propertyInfo.GetValue(reference, null));
                }
            }
            return string.Empty;
            //throw new UnityException(string.Format("We're sorry, MemberTypes {0} is not supported at this time.  Please contact the developer to request {0} type support.\nThank You.", memberType));
        }

        /// <summary>
        /// Sets the value for the FieldInfo.SetValue or PropertyInfo.SetValue from the member variable memberValue
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberType"></param>
        /// <param name="memberName"></param>
        public static void LoadParser(Component reference, string memberValue, MemberTypes memberType, string memberName)
        {
            switch (memberType)
            {
                case MemberTypes.Field:
                    {
                        FieldInfo fieldInfo = reference.GetType().GetField(memberName, BINDING_FLAGS);
                        if (fieldInfo == null)
                        {
                            InternalLoadParser(Parser.Empty.GetType(), memberValue);
                        }
                        fieldInfo.SetValue(reference, InternalLoadParser(fieldInfo.FieldType, memberValue));
                    }
                    break;
                case MemberTypes.Property:
                    {
                        PropertyInfo propertyInfo = reference.GetType().GetProperty(memberName, BINDING_FLAGS);
                        if (propertyInfo == null)
                        {
                            InternalLoadParser(Parser.Empty.GetType(), memberValue);
                        }
                        propertyInfo.SetValue(reference, InternalLoadParser(propertyInfo.PropertyType, memberValue), null);
                    }
                    break;
                default:
                    //throw new UnityException(string.Format("We're sorry, MemberTypes {0} is not supported at this time.  Please contact the developer to request {0} type support.\nThank You.", memberType));
                    break;
            }
        }

        public static Type GetMemberInfoType(Component reference, MemberTypes memberType, string memberName)
        {
            Type returnType = null;
            switch (memberType)
            {
                case MemberTypes.Field:
                    {
                        FieldInfo fieldInfo = reference.GetType().GetField(memberName, BINDING_FLAGS);
                        returnType = fieldInfo.FieldType;
                    }
                    break;
                case MemberTypes.Property:
                    {
                        PropertyInfo propertyInfo = reference.GetType().GetProperty(memberName, BINDING_FLAGS);
                        returnType = propertyInfo.PropertyType;
                    }
                    break;
                default:
                    //throw new UnityException(string.Format("We're sorry, MemberTypes {0} is not supported at this time.  Please contact the developer to request {0} type support.\nThank You.", memberType));
                    break;
            }
            return returnType;
        }

        public static Func<MemberInfo, bool> GetSupportedMembers()
        {

            return x => {
                PropertyInfo propertyInfo = x as PropertyInfo;
                FieldInfo fieldInfo = x as FieldInfo;

                bool isProperty = x.MemberType == MemberTypes.Property;
                bool isField = x.MemberType == MemberTypes.Field;
                bool canReadWright = propertyInfo != null && propertyInfo.CanRead && propertyInfo.CanWrite;
                bool containsPropertyType = propertyInfo != null && Parsers.Keys.Contains(propertyInfo.PropertyType);
                bool containsFieldType = fieldInfo != null && Parsers.Keys.Contains(fieldInfo.FieldType);
                return
               // Property
               (isProperty && canReadWright && (containsPropertyType || propertyInfo.PropertyType.IsEnum)) ||
               // Field
               (isField && (containsFieldType || fieldInfo.FieldType.IsEnum));
            };
        }

        static Parser TryGetParser(Type type, Type realType)
        {
            Parser parser = null;
            if (Parsers.TryGetValue(type, out parser))
            {
                return Parsers[type];
            }
            else
            {
                throw new UnityException(string.Format("We're sorry, ValueParsers type conversion to {0} is not supported at this time.  Please contact the developer to request {0} type support.\nThank You.", realType));
            }
        }

        static object InternalLoadParser(Type type, string memberValue)
        {
            return GetParserByType(type).LoadParser(memberValue);
        }

        static string InternalSaveParser(Type type, object value)
        {
            return GetParserByType(type).SaveParser(value);
        }

        static Parser GetParserByType(Type type)
        {
            if (type.IsEnum)
            {
                return TryGetParser(typeof(Enum), type);
            }
            else
            {
                return TryGetParser(type, type);
            }
        }
    }
}