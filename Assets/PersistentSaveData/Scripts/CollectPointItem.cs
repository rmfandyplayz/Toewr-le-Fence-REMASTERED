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

using System.Collections;
using UnityEngine;

namespace PersistentSaveData
{

    public class CollectPointItem : PointItem
    {

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PointItem>().points += points;
                Vector3 newPosition = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 2.0f), -9);
                transform.position = newPosition;
                StopCoroutine("StartAutoMove");
                StartCoroutine("StartAutoMove");
            }
        }

        IEnumerator StartAutoMove()
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 2.0f), -9);
        }

    }
}