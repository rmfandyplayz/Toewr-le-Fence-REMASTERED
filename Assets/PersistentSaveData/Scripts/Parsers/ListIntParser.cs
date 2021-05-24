using PersistentSaveData.Core.Parsers;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace PersistentSaveData.Parsers
{
    public class ListIntParser : Parser
    {

        static string[] SplitList(string value)
        {
            //RGBA(0.000, 0.000, 0.000, 0.000)
            return value.Replace("List<int>", "").Replace(" ", "").Replace("[", "").Replace("]", "").Split(',');
        }

        public override Type ParseType { get { return typeof(List<int>); } }

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
            List<int> v = ((List<int>)o);
            string temp = "List<int>[";
            foreach(int numer in v)
            {
                temp += numer.ToString() + ",";
            }
            temp = temp.TrimEnd(',') + "]";
            return string.Format(temp);
        }

        public static List<int> LoadParse(string s)
        {
            string[] splitCords = SplitList(s);
            List<int> newList = new List<int>();
            foreach (string sc in splitCords)
            {
                int value;
                if (int.TryParse(sc, out value))
                {
                    newList.Add(value);
                }
            }
            return newList;

            //if (splitcords.length == 4)
            //{
            //    float r = singleparser.loadparse(splitcords[0]);
            //    float g = singleparser.loadparse(splitcords[1]);
            //    float b = singleparser.loadparse(splitcords[2]);
            //    float a = singleparser.loadparse(splitcords[3]);

            //    return new list<int>(r, g, b, a);
            //}
            //throw new parseexception(typeof(listintparser), s, typeof(list<int>));
        }
    }
}