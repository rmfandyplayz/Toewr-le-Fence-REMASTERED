using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CustomUnityEvent
{
    [System.Serializable]
    public class UEventFloat : UnityEvent<float>
    {

    }

    [System.Serializable]
    public class UEventInt : UnityEvent<int>
    {

    }

    // [System.Serializable]
    // public class UEventFloatVector3 : UnityEvent<float, Vector3>
    // {

    // }

    [System.Serializable]
    public class UEventGameObject : UnityEvent<GameObject>
    {

    }

    [System.Serializable]
    public class UEventDamageInfo: UnityEvent<DamageInfo>
    {
        
    }
}
