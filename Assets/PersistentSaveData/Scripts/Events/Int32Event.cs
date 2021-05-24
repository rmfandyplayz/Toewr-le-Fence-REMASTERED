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
using PersistentSaveData.Core.Parsers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PersistentSaveData.Events
{
    [Serializable]
    public class Int32Event : MemberEvent<int>
    {
        public override void InvokeMemberValue(Member member)
        {
            Invoke(Int32Parser.LoadParse(member.value));
        }

        public override List<IMemberEvent> GetEventsList(NotificationEvents notificationEvents)
        {
            return notificationEvents.int32Event.Select(x => x as IMemberEvent).ToList();
        }

        public override void SetEventsList(NotificationEvents notificationEvents, List<IMemberEvent> events)
        {
            ClearAndAddEventsRange(notificationEvents.int32Event, events);
        }
    }
}