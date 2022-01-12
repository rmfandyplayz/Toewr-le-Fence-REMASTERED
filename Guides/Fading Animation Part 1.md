# Tween Animation Scriptable Objects

## Idea

We will create two scripts to handle tween animation for images through the use of scriptable objects as presets. The first script will hold the full sequence of the tween events (e.g. fade in, fade out, holding, cancel tweens, etc.). The second script will be used to run the tween events in sequence and keep track of where it is currently at. This should provide a template to build other tween animation utilities for other objects. We will also refactor the health and status effect indictors to use this new system later on.

## Coding

### Script 1: Tweening Scriptable Object Class
* This scriptable object will let you order the sequence of Tween Events to run. 
* Before working on the scriptable object class itself, we will add two helper object below the class.
* First, create a `public enum` for these types of tween events (referred to as `ENUM_1`):
    1. **Hold**
    2. **Cancel**
    3. **Fade** (this handles both fade in and fade out)
    * You can come back to the enum to add more types later on.
* Next, create another `public class` for the tween event (referred to as `HELPER_CLASS_1`). This will act as one event for the tween sequence.
    * Remember to add `[System.Serializable]` attribute before the class to allow it to show in the inspector.
    * Inside this class, add these variables:
        1. public **ENUM_1** `INFO_1`: Defines the type of tween event
        2. public **bool** `INFO_2`: This will disallowing multiple events from running at the same time.
        3. public **bool** `INFO_3`: Use speed value instead of regular time value
        6. public **float** `INFO_4`: The target value of the tween.
        7. public **float** `INFO_5`: The speed value of the tween, show if `INFO_3` is true.
        8. public **float** `INFO_6`: The time value of the tween, show if `INFO_3` is false.
        9. public **Ease** `INFO_7`: This is the easing function to use for the tween.
* Now, let's go back to the original class (referred to as `SO_1`).
    * Change `MonoBehavior` to `ScriptableObject`
    * Add the `[CreateAssetMenu(fileName)]` attribute before the class. You can assign any name to `fileName` to represent the object in the menu.
    * At the moment, you will only need a public list of `HELPER_CLASS_1` to represent the tween sequence.

### Script 2: Image Tween Runner Script (still being worked on)
* This script will be used to run a provided tween effect object.
* Create a new C# script for Image Tween Runner.
* In the `using` section add `UnityEngine.UI`.
* The initial script will have five [SerializedField] variables:
    1. **Image** `VAR_1`: The image to apply the tween effects to.
    2. **`SO_1`** `VAR_2`: The tweening scriptable object to be used by the runner.
    3. **float** `VAR_3`: The starting alpha value (for fading), set it to 1.
    4. **float** `VAR_4`: The starting fill value, set it to 1.
    5. **int** `VAR_5`: This will track how many tween effects are still actively running, set it to 0. 
        * *Note: This technically does not need to have SerializedField, since it is only changes from the function, but it is useful to see the number update correctly*
* The functions for Image Tween Runner:
    * ***TO BE ADDED, FIXING BUGS***

