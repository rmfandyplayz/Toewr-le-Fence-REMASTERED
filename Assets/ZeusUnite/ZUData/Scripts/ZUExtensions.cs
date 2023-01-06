using System;
using System.Linq;
using UnityEngine;

/// <summary> **** Zeus Unite Info ****
/// Check https://zeusunite.stussegames.com out
/// </summary>  **** All Rights Reserved ® Zeus Unite ****


namespace ZeusUnite.Ext
{
    public static class ZUExtensions
    {
        public static void SetActive(this Component target, bool active)
        {
            target.gameObject.SetActive(active);
        }

        public static void SetActive(this Component target, bool active, float time)
        {
            MyTimer.CreateTimer(() => target.SetActive(active), time);
        }

        public static Vector3 Position(this Component target)
        {
            return target.transform.position;
        }

        //Check the Performance Post for the sqrMagnitude Reason
        public static float GetDistance(this Component target, Vector3 other)
        {
            return (target.transform.position - other).sqrMagnitude;
        }
    }

    /// <summary>
    /// Create a Timer on a MonoBehaviour GameObject
    /// After Time runs out a Action can be called.
    /// </summary>
    public class MyTimer
    {
        /// <summary>
        /// We Call this Function from other Scripts to Create a Timer at Runtime
        /// </summary>
        /// <param name="action">The Action that gets Performed after the Time runs out.</param>
        /// <param name="timer">The amount of Time that has to pass until the Timer gets Triggered</param>
        /// <returns></returns>
        public static MyTimer CreateTimer(Action action, float timer)
        {
            GameObject obj = new GameObject("AudioShot", typeof(MonoBehaviourHook));
            MyTimer stTimer = new MyTimer(obj, action, timer);
            obj.GetComponent<MonoBehaviourHook>().OnUpdate = stTimer.UpdateTimer;

            return stTimer;
        }

        //We Store the GameObject so we can Destroy it later
        GameObject gameObject;

        float timer;
        Action TimeAction;


        public MyTimer(GameObject gameObject, Action action, float timer)
        {
            this.gameObject = gameObject;
            this.timer = timer;
            this.TimeAction = action;
        }

        /// <summary>
        /// Update the Time with OnUpdate from Monobehaviour.
        /// </summary>
        void UpdateTimer()
        {
            timer -= Time.deltaTime;

            if (timer > 0)
                return;

            TimeAction();
            DestroySelf();
        }

        /// <summary>
        /// GameObject gets Destroyed after TimeAction is performed
        /// </summary>
        void DestroySelf()
        {
            GameObject.Destroy(gameObject);
        }

        /// <summary>
        /// We add a Simple Script on our GameObject
        /// With this we have the Update Method available which will Trigger our Action
        /// After a certain amount of Time.
        /// </summary>
        class MonoBehaviourHook : MonoBehaviour
        {
            public Action OnUpdate;

            private void Update()
            {
                if (OnUpdate != null) OnUpdate();
            }
        }
    }
}
