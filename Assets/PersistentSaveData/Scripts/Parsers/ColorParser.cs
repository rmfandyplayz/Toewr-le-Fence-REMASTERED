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
    public class ColorParser : Parser
    {

        static string[] SplitColor(string value)
        {
            //RGBA(0.000, 0.000, 0.000, 0.000)
            return value.Replace("RGBA", "").Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
        }

        public override Type ParseType { get { return typeof(Color); } }

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
            Color v = ((Color)o);
            return string.Format("RGBA({0}, {1}, {2}, {3})", v.r, v.g, v.b, v.a);
        }

        public static Color LoadParse(string s)
        {
            string[] splitCords = SplitColor(s);

            if (splitCords.Length == 4)
            {
                float r = SingleParser.LoadParse(splitCords[0]);
                float g = SingleParser.LoadParse(splitCords[1]);
                float b = SingleParser.LoadParse(splitCords[2]);
                float a = SingleParser.LoadParse(splitCords[3]);

                return new Color(r, g, b, a);
            }
            throw new ParseException(typeof(ColorParser), s, typeof(Color));
        }
    }
}