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
    public class Vector2Parser : Parser
    {
        internal static string[] SplitVector(string value)
        {
            //(0.0, 0.5)
            //(0.0, 0.5, -2.0)
            return value.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
        }

        public override Type ParseType { get { return typeof(Vector2); } }

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
            Vector2 v = ((Vector2)o);
            return string.Format("({0}, {1})", v.x, v.y);
        }

        public static Vector2 LoadParse(string s)
        {
            string[] splitCords = SplitVector(s);

            if (splitCords.Length == 2)
            {
                float x = SingleParser.LoadParse(splitCords[0]);
                float y = SingleParser.LoadParse(splitCords[1]);
                return new Vector2(x, y);
            }
            throw new ParseException(typeof(Vector2Parser), s, typeof(Vector2));
        }
    }
}