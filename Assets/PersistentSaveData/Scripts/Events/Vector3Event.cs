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
using System.Linq;
using UnityEngine;
using PersistentSaveData.Parsers;
using System.Collections.Generic;

namespace PersistentSaveData.Events
{
    [Serializable]
    public class Vector3Event : MemberEvent<Vector3>
    {
        public override void InvokeMemberValue(Member member)
        {
            Invoke(Vector3Parser.LoadParse(member.value));
        }

        public override List<IMemberEvent> GetEventsList(NotificationEvents notificationEvents)
        {
            return notificationEvents.vector3Event.Select(x => x as IMemberEvent).ToList();
        }

        public override void SetEventsList(NotificationEvents notificationEvents, List<IMemberEvent> events)
        {
            ClearAndAddEventsRange(notificationEvents.vector3Event, events);
        }
    }
}