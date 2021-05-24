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
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PersistentSaveData.Events
{
    public interface IMemberEvent
    {
        Type type { get; }
        string memberName { get; set; }
        MemberTypes memberType { get; set; }
        /// <summary>
        /// PersistData members index.
        /// <para>Usage: PersistData.members[dataIndex]</para>
        /// </summary>
        int dataIndex { get; set; }

        int GetEventCount();

        void InvokeMemberValue(Member member);

        List<IMemberEvent> GetEventsList(NotificationEvents notificationEvents);

        void SetEventsList(NotificationEvents notificationEvents, List<IMemberEvent> events);

        string GetMethodName(int index);

        UnityEngine.Object GetMethodTarget(int index);
    }
}