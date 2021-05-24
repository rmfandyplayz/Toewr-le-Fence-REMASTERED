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

using PersistentSaveData.Core.DefaultNames;
using PersistentSaveData.Core.Formatters;
using PersistentSaveData.Core.Parsers;
using PersistentSaveData.Parsers;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PersistentSaveData.Persistent
{
    [System.Serializable]
    public class MemberData
    {
        public int Int = -1;
        public int[] Ints = null;

        public MemberTypes Types = MemberTypes.Field;

        public FileFormat Format = FileFormat.Custom1;
        public string Details = string.Empty;


        public MemberData(int _int, MemberTypes types, FileFormat format, string details)
        {
            Int = _int;
            Types = types;
            Format = format;
            Details = details;
        }
    }
    
    public class MemberDataParser : Parser
    {

        static string[] SplitValue(string value)
        {
            return value.Replace(" ", "").Split(',');
        }

        public override System.Type ParseType { get { return typeof(MemberData); } }

        public override string SaveParser(object o)
        {
            MemberData v = ((MemberData)o);
            
            return string.Format("{0}, {1}, {2}, {3}", v.Int, EnumParser.SaveParse(v.Types), EnumParser.SaveParse(v.Format), v.Details);
        }

        public override object LoadParser(string s)
        {
            string[] splitCords = SplitValue(s);

            if (splitCords.Length == 4)
            {
                int r = Int32Parser.LoadParse(splitCords[0]);
                MemberTypes g = (MemberTypes)EnumParser.LoadParse(splitCords[1]);
                FileFormat b = (FileFormat)EnumParser.LoadParse(splitCords[2]);
                string a = StringParser.LoadParse(splitCords[3]);

                return new MemberData(r, g, b, a);
            }
            throw new ParseException(typeof(MemberDataParser), s, ParseType);
        }
    }

    public class PersistentTestDefaults : DefaultName
    {
        public override System.Type type { get { return typeof(PersistentTest); } }

        public override List<string> names
        {
            get
            {
                return new List<string>()
                {
                    "memberData", "memberFileFormat", "memberIntegers"
                };
            }
        }
    }

    [AddComponentMenu("")]
    public class PersistentTest : MonoBehaviour
    {

        public MemberData memberData;

        public Vector3 memberVector3;
        public Color memberColor;
        public Vector2 memberVector2;
        public int memberInteger = -1;
        public int[] memberIntegers = null;

        public MemberTypes memberMemberTypes = MemberTypes.Field;

        public FileFormat memberFileFormat = FileFormat.Custom1;
        public string memberString = string.Empty;

    }
}