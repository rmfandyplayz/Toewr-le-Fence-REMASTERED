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
using System.Collections.Generic;
using System.Linq;

namespace PersistentSaveData.Events
{
    [Serializable]
    public class StringEvent : MemberEvent<string>
    {
        public override void InvokeMemberValue(Member member)
        {
            Invoke(StringParser.LoadParse(member.value));
        }

        public override List<IMemberEvent> GetEventsList(NotificationEvents notificationEvents)
        {
            return notificationEvents.stringEvent.Select(x => x as IMemberEvent).ToList();
        }

        public override void SetEventsList(NotificationEvents notificationEvents, List<IMemberEvent> events)
        {
            ClearAndAddEventsRange(notificationEvents.stringEvent, events);
        }
    }
}