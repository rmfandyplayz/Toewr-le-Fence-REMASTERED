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

using UnityEditor;
using PersistentSaveData.Persistent;
using PersistentSaveData.Core;

namespace PersistentSaveDataEditor.Persistent
{

    [CustomEditor(typeof(PersistentREST), true)]
    public class PersistentRESTEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TryItCompatibilityEditor.ShowTryIt.DrawOpenTryItButton(PersistentSettings.Instance.GetTryIt());
            base.OnInspectorGUI();
        }
    }
}
