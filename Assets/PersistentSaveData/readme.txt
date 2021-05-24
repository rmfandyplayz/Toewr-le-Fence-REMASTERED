Persistent Save Data Version 1.0.0 02/01/2017


GENERAL USAGE NOTES
==============================================
* Save and Load any component values at any time
* Unity Component based persistent system


INSTALLATION
==============================================
* Down the package from Unity Asset Store
* Import all the assets
* Add a Reference Component to the GameObject
* Select the component on that GameObject to persist
* Select all the properties to persist
* Select how and when to save/load
* Press play




EXAMPLE
==============================================
This example show how to create a custom data type, here Unity’s Color data type is used.


        public override string SaveParser(object o)
        {
            Color v = ((Color)o);
            return string.Format("RGBA({0}, {1}, {2}, {3})", v.r, v.g, v.b, v.a);
        }


        public override object LoadParser(string s)
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




CONTACT
==============================================
Developer: caLLowCreation
Author: Jones S. Cropper
E-mail: callowcreation@gmail.com 
Subject: PersistentSaveData
Website: www.caLLowCreation.com