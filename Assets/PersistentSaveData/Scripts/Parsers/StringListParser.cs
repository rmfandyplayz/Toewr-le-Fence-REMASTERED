using PersistentSaveData.Core.Parsers;
using System;
using UnityEngine;
using System.Collections.Generic;

public class StringListParser : Parser
{
    internal static string[] SplitList(string value)
    {
        //(0.0, 0.5)
        //(0.0, 0.5, -2.0)
        return value.Replace("[", "").Replace("]", "").Split('|');
    }

    public override Type ParseType { get { return typeof(List<string>); } }

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
        List<string> v = ((List<string>)o);
        string temp = "[";
        foreach(string str in v)
        {
            temp += $"{str}|";
        }
        temp.TrimEnd('|');
        temp += "]";
        return temp;
    }

    public static List<string> LoadParse(string s)
    {
        string[] splitCords = SplitList(s);
        List<string> v = new List<string>();
        foreach(string sc in splitCords)
        {
            v.Add(sc);
        }
        return v;
    }
}
