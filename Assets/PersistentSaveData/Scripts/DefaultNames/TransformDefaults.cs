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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentSaveData.DefaultNames
{

    public class TransformDefaults : DefaultName
    {
        public override Type type { get { return typeof(Transform); } }

        public override List<string> names
        {
            get
            {
                return new List<string>()
                {
                    "position", "eulerAngles", "localScale"
                };
            }
        }
    }
}