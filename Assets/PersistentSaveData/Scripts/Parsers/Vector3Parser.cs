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

using PersistentSaveData.Core.Parsers;
using System;
using UnityEngine;

namespace PersistentSaveData.Parsers
{
    public class Vector3Parser : Parser
    {
        public override Type ParseType { get { return typeof(Vector3); } }

        public override string SaveParser(object o)
        {
            return SaveParse(o);
        }

        public override object LoadParser(string s)
        {
            return LoadParse(s);
        }

        public static string SaveParse(object o)
        {
            Vector3 v = ((Vector3)o);
            return string.Format("({0}, {1}, {2})", v.x, v.y, v.z);
        }

        public static Vector3 LoadParse(string s)
        {
            string[] splitCords = Vector2Parser.SplitVector(s);

            if (splitCords.Length == 3)
            {
                float x = SingleParser.LoadParse(splitCords[0]);
                float y = SingleParser.LoadParse(splitCords[1]);
                float z = SingleParser.LoadParse(splitCords[2]);
                return new Vector3(x, y, z);
            }
            throw new ParseException(typeof(Vector3Parser), s, typeof(Vector3));
        }
    }
}