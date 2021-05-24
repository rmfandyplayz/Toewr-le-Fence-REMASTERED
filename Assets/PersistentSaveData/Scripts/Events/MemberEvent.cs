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
using UnityEngine.Events;

namespace PersistentSaveData.Events
{
    [Serializable]
    public abstract class MemberEvent<T> : UnityEvent<T>, IMemberEvent
    {
        public readonly static IMemberEvent Empty = new EmptyEvent();

        [UnityEngine.SerializeField]
        string m_MemberName = string.Empty;
        [UnityEngine.SerializeField]
        MemberTypes m_MemberType = MemberTypes.Field;
        public string memberName { get { return m_MemberName; } set { m_MemberName = value; } }
        public MemberTypes memberType { get { return m_MemberType; } set { m_MemberType = value; } }

        /// <summary>
        /// PersistData members index.
        /// <para>Usage: PersistData.members[dataIndex]</para>
        /// </summary>
        public int dataIndex { get; set; }

        public virtual Type type { get { return typeof(T); } }

        public abstract void InvokeMemberValue(Member member);

        public virtual List<IMemberEvent> GetEventsList(NotificationEvents notificationEvents)
        {
            return new List<IMemberEvent>();
        }

        public abstract void SetEventsList(NotificationEvents notificationEvents, List<IMemberEvent> events);

        protected static void ClearAndAddEventsRange<V>(List<V> list, List<IMemberEvent> events) where V : IMemberEvent
        {
            list.Clear();
            for (int i = 0; i < events.Count; i++)
            {
                list.Add((V)events[i]);
            }
        }

        public int GetEventCount()
        {
            return base.GetPersistentEventCount();
        }

        public string GetMethodName(int index)
        {
            return base.GetPersistentMethodName(index);
        }

        public UnityEngine.Object GetMethodTarget(int index)
        {
            return base.GetPersistentTarget(index);
        }

        public class EmptyEvent : MemberEvent<object>
        {
            public override Type type { get { return typeof(object); } }

            public override List<IMemberEvent> GetEventsList(NotificationEvents notificationEvents)
            {
                return new List<IMemberEvent>();
            }

            public override void InvokeMemberValue(Member member)
            {
                Invoke(member.value);
            }

            public override void SetEventsList(NotificationEvents notificationEvents, List<IMemberEvent> events)
            {
                
            }
        }
    }
}