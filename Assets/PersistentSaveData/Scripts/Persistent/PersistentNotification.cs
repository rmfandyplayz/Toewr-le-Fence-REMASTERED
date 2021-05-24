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
using PersistentSaveData.Events;
using System.Collections.Generic;
using PersistentSaveData.Core;
using System.Reflection;

namespace PersistentSaveData.Persistent
{
    public class PersistentNotification : MonoBehaviour
    {
        [SerializeField]
        PersistentReference m_PersistentReference = null;

        [SerializeField]
        NotificationEvents m_BeforeSaveNotifications = null;
        [SerializeField]
        NotificationEvents m_AfterSaveNotifications = null;

        [SerializeField]
        NotificationEvents m_BeforeLoadNotifications = null;
        [SerializeField]
        NotificationEvents m_AfterLoadNotifications = null;

        public NotificationEvents beforeSaveNotifications { get { return m_BeforeSaveNotifications; } set { m_BeforeSaveNotifications = value; } }
        public NotificationEvents afterSaveNotifications { get { return m_AfterSaveNotifications; } set { m_AfterSaveNotifications = value; } }
        public NotificationEvents beforeLoadNotifications { get { return m_BeforeLoadNotifications; } set { m_BeforeLoadNotifications = value; } }
        public NotificationEvents afterLoadNotifications { get { return m_AfterLoadNotifications; } set { m_AfterLoadNotifications = value; } }

        protected bool beforeLoad { get { return m_BeforeLoad; } set { m_BeforeLoad = value; } }
        protected bool beforeSave { get { return m_BeforeSave; } set { m_BeforeSave = value; } }
        protected bool afterLoad { get { return m_AfterLoad; } set { m_AfterLoad = value; } }
        protected bool afterSave { get { return m_AfterSave; } set { m_AfterSave = value; } }
        
        [SerializeField]
        bool m_BeforeLoad = false;
        [SerializeField]
        bool m_BeforeSave = false;
        [SerializeField]
        bool m_AfterLoad = true;
        [SerializeField]
        bool m_AfterSave = true;

        void OnEnable()
        {
            PersistentReference.OnBeforeLoad += OnBeforeLoad;
            PersistentReference.OnBeforeSave += OnBeforeSave;
            PersistentReference.OnAfterLoad += OnAfterLoad;
            PersistentReference.OnAfterSave += OnAfterSave;
        }

        void OnDisable()
        {
            PersistentReference.OnBeforeLoad -= OnBeforeLoad;
            PersistentReference.OnBeforeSave -= OnBeforeSave;
            PersistentReference.OnAfterLoad -= OnAfterLoad;
            PersistentReference.OnAfterSave -= OnAfterSave;
        }

        void OnBeforeLoad(PersistentReference persistentReference)
        {
            if (persistentReference != m_PersistentReference) return;

            m_BeforeLoadNotifications.InvokeMemberValues(m_PersistentReference);
        }

        void OnBeforeSave(PersistentReference persistentReference)
        {
            if (persistentReference != m_PersistentReference) return;

            m_BeforeSaveNotifications.InvokeMemberValues(m_PersistentReference);
        }

        void OnAfterLoad(PersistentReference persistentReference)
        {
            if (persistentReference != m_PersistentReference) return;

            m_AfterLoadNotifications.InvokeMemberValues(m_PersistentReference);
        }

        void OnAfterSave(PersistentReference persistentReference)
        {
            if (persistentReference != m_PersistentReference) return;

            m_AfterSaveNotifications.InvokeMemberValues(m_PersistentReference);
        }

        public void AsString(Object value)
        {
            Debug.Log(value);
        }

    }
}