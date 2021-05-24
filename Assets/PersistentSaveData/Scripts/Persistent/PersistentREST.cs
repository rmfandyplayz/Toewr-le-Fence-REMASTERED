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

using PersistentSaveData.Core;
using PersistentSaveData.Core.Formatters;
using System.Collections;
using UnityEngine;
using PersistentSaveData.Parsers;
using System;
using UnityEngine.Networking;

namespace PersistentSaveData.Persistent
{
    [RequireComponent(typeof(PersistentReference))]
    public class PersistentREST : MonoBehaviour
    {
        [SerializeField, Tooltip("The url/site to post/send the data to.")]
        string m_PostUrl = "http://YOUR_POST_URL";

        [SerializeField, Tooltip("The url/site to receive/request the data from.")]
        string m_RequestUrl = "http://YOUR_REQUEST_URL";

        [SerializeField, Tooltip("The name of the json object containing the data.")]
        string m_FieldName = "data";
        
        PersistentReference m_PersistentReference = null;

        void Awake()
        {
            m_PersistentReference = GetComponent<PersistentReference>();
        }

        public void PostData()
        {
            StartCoroutine("PostDataRoutine");
        }

        public void RequestData()
        {
            StartCoroutine("RequestDataRoutine");
        }
        
        IEnumerator PostDataRoutine()
        {
            string jsonMembers = JSONFileFormatter.ToJson(m_PersistentReference.persistData);
            
            // Works but must attach a field name
            WWWForm form = new WWWForm();
            form.AddField(m_FieldName, jsonMembers);
     
            using (UnityWebRequest www = UnityWebRequest.Post(m_PostUrl, form))
            {
                // Request and wait for the desired page.
                yield return www.SendWebRequest();

                string[] pages = m_PostUrl.Split('/');
                int page = pages.Length - 1;

                if (www.isNetworkError)
                {
                    Debug.LogError(pages[page] + ": Error: " + www.error);
                }
            }

            // Works but must attach a field name
            //  WWWForm form = new WWWForm();
            //  form.AddField(m_FieldName, jsonMembers);

            //  WWW www = new WWW(m_PostUrl, form);
            //  yield return www;   

            /*
            // Not receiving post data
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");

            byte[] postData = Encoding.ASCII.GetBytes(jsonMembers.ToCharArray());

            WWW www = new WWW(m_PostUrl, postData, headers);
            yield return www;
            */

            //Debug.Log("www.text: " + www.text);
            //  if (!string.IsNullOrEmpty(www.error))
            //  {
            //     Debug.LogError("Failed to Send:" + www.error);
            // }
        }

        IEnumerator RequestDataRoutine()
        {

            using (UnityWebRequest www = UnityWebRequest.Get(m_RequestUrl))
            {
                // Request and wait for the desired page.
                yield return www.SendWebRequest();

                string[] pages = m_PostUrl.Split('/');
                int page = pages.Length - 1;

                if (www.isNetworkError)
                {
                    Debug.LogError(pages[page] + ": Error: " + www.error);
                }
                else
                {
                    //Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);

                    string rawResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    string result = rawResult.Replace("\n", "").Replace("\r", "").Replace(" ", "");

                    try
                    {
                        PersistData persistData = JSONFileFormatter.FromJSON(www.downloadHandler.text);

                        for (int i = 0; i < persistData.members.Length; i++)
                        {
                            Member member = persistData.members[i];
                            ValueParsers.LoadParser(m_PersistentReference.reference, member.value, member.type, member.name);
                        }

                        yield break;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                    }
                }
            }
            
            // WWW www = new WWW(m_RequestUrl);
            // yield return www;
            // 
            // if (string.IsNullOrEmpty(www.error))
            // {
            //     PersistData persistData = JSONFileFormatter.FromJSON(www.text);
            // 
            //     for (int i = 0; i < persistData.members.Length; i++)
            //     {
            //         Member member = persistData.members[i];
            //         ValueParsers.LoadParser(m_PersistentReference.reference, member.value, member.type, member.name);
            //     }
            // }
            // else
            // {
            //     Debug.LogError("Failed to Receive:" + www.error);
            // }         
        }
    }
}