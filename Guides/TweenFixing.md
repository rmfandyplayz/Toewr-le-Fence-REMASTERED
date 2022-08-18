# Fixing Tween Animation

## Idea for fixing tween: "Ticket" system
* When a `RunTween` calls the `TweeningScriptableObject` to run a tween, the scriptable object will return information needed to run the next tween.
* This information acts like a "ticket" that can be used to run the next tween when the previous one finishes.
* If a new tween comes along, or another different tween finishes, the `RunTween` should figure out what to do next based on the fact that they have different tickets.
* For this fix, you'll update the `TweeningScriptableObject` and `RunTween`
* ***NOTE: Updated GitHub with a new Testing Scene called Test Tween Anim***

## TweeningScriptableObject
* Update `RunTweenOnObjectUsingDynamicValue`: 
    * Add a thrid parameter for the `TweeningInformation`, which represents the previous tweening value and acts as the "ticket" for going to the next part of the tween.
    * Change the `else` to and `else if`, where if the tween info is not null, run the `RunTweenOnObj` with the values taken from `TweeningInformation` and return its result.
    * Otherwise, just run the original `RunTweenOnObj` with just the `checkedObject` and `dynamicValue`, and return its result.
* That should be it, now the scriptable object can now take information from the `TweeningInformation` and use that to guide the next steps.

## RunTween
* Update `RunTweenUniversal`:
    * Add another parameter: an optional int (`int?` `[VAR_ID]`) which represents the tween id that is currently running
    * Update the first `if`: it should follow this condition:
        1. currentRunningTween is not null (a tween is currently running)
        2. and either:
            1. `[VAR_ID]` is null (we're running a new tween)
            2. or `[VAR_ID].Value` does not match the currentRunningTween's ID.
            * **Note: AND has higher precendece than OR, so remember to wrap the OR condition in parantheses.**
    * After that, go to the `if` conditional where `multiType` == `runRecent`. Inside, after caneling the tween, set the currently running tween to null.
    * Before the `RunTweenOnObjectUsingDynamicValue`, create a temp `var` representing the previous tween and set it to `currentRunningTween`.
    * Next, update `RunTweenOnObjectUsingDynamicValue` to take in the previous tween.
    * Inside the `SetOnComplete()` change the lambda function to run the `RunTweenUniversal` with the same dynamic value and the currentRunningTween's ID.
        * This tells the function to continue the tween if there was no interruptions. 

* After this, the tween should now work on any object it is currently assigned to. *We still need to do apply the changes for the children objects.*
