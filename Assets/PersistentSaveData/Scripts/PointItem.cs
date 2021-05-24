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

using UnityEngine;

namespace PersistentSaveData
{

    public class PointItem : MonoBehaviour
    {
        public int points = 0;

        public void ResetPoints()
        {
            points = 0;
        }
    }
}