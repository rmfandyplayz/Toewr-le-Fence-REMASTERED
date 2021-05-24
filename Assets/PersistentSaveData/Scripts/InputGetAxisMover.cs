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

    public abstract class InputGetAxisMover : MonoBehaviour
    {
        [SerializeField]
        protected float m_Speed = 3;

        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            MoveWithAxis(h, v);
        }

        protected abstract void MoveWithAxis(float h, float v);
    }
}