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

    public class InputGetAxisMover2D : InputGetAxisMover
    {
        [SerializeField]
        Vector3 m_InitialPosition = new Vector3(0, 1, -9);

        void Awake()
        {
            m_InitialPosition = transform.position;
        }

        protected override void MoveWithAxis(float h, float v)
        {
            transform.Translate(h * m_Speed * Time.deltaTime, v * m_Speed * Time.deltaTime, 0);
        }

        public void Reset()
        {
            transform.position = m_InitialPosition;
        }

    }
}