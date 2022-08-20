using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio.TweenEngine;

public class TweenInformation
{
    public BaseTween currentRunningTween;
    public int nextIndex;
    public int currentLoop; //If there are tween repeats, then this variable specifies what loop it is running.
    public float carryDelay; //Delay that carrys over if there are delays within the previous tween.
    public int ?gotoIndexWhenInterrupted;
}
