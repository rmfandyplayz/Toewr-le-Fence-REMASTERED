# Tween Animation Scriptable Objects (Part 2)

## Idea

Same as part 1.

***Note: Trying something new with document comments and regular comments instead of lists of bullet points. See if this one is better.***

## Example of new format using `RunTween method`
* This is what the documentation code will look like. Placing this above the method will give you improved Intellisense. 
```csharp
    /// <summary>
    /// Run the current tween event from a Tweening Scriptable Object
    /// </summary>
    /// <remarks>
    /// Type: public function
    /// </remarks>
    /// <param name="VAR_1">TweeningScriptObj: The tween object preset to use.</param>
    /// <param name="VAR_2">Image: The image object to apply the effect to.</param>
    /// <param name="VAR_3">int: The number representing the current tween event</param>
    /// <param name="VAR_4">bool: Whether the image is a child of the main image runner</param>
    /// <return> Nothing </return> (if this is not given, it means return nothing (void))
```
* Some notes:
    * Replace any `VAR` with the actual variable name. This will auto assign the information to that variable.
    * If you see curly braces `{}`, it will actually mean angular brackets `<>` in the code (this is an XML limitation).


* This would be the code outline for this method (you can optionally place it inside the body of the method to make it easy to reference when coding).
```csharp
    // If tween or target object is null or
    // current is at or above the count of 
    // tween's helpers ...

        // then, decrement running tween effects and exit the function
    

    // Create a new temporary variable for the current tween
    // which equals the value at current in tween's helpers
    

    // If current tween disallows multiple events and
    // the number of running tween events is bigger than 1 ...

        // then, decrement running tween effects and exit the function
```
## Coding

### Script: Image Tween Runner Script
1. `COROUTINE_A`: Create a new coroutine for the holding tween
    * Document Comment:
    ```csharp
        /// <summary>
        /// Holds a tween for a given time
        /// </summary>
        /// <remarks>
        /// Type: public coroutine
        /// </remarks>
        /// <param name="VAR_1A">float: time to hold the tween.</param>
        /// <param name="VAR_2A">System.Action: the function to callback when finished.</param>
        /// <returns>IEnumerator for coroutine</returns>
    ```
    * Code Outline:
    ```csharp
        // yield return a new wait for seconds with param VAR_A1

        // Invoke VAR_A2 (use ? to do null check before .Invoke())
            
    ```

2. `METHOD_B`: Create a new method for applying tween effect
    * Document Comment:
    ```csharp
        /// <summary>
        /// Will apply a given tween event to a given image. Will run callback function when the tween is finished.
        /// </summary>
        /// <remarks>
        /// Type: public function
        /// </remarks>
        /// <param name="VAR_1B"> Image: The image to apply the effect to</param>
        /// <param name="VAR_2B"> TweeningHelper: The tween effect that will be applied.</param>
        /// <param name="VAR_3B"> System.Action: A function to run when the tween is completed. </param>
        /// <return> Nothing </return>
    ```
    * Code Outline:
    ```csharp
        // Note: function chaining for Platino Tween
        // Referred to as PT Chain
        // The format of this is as follows 
        // TWEEN_FUNC is specially defined for each tween type
        // (new lines added for easier formatting):
        // VAR_B1
        //  .TWEEN_FUNC(VAR_B2.targetValueOfTween, 
        //              VAR_B2.(speed/time)ValueOfTween)
        //  .SetEase(VAR_B2.ease)
        //  .SetOwner(this.gameObject)
        //  .SetOnComplete(VAR_B3);


        // If the type of tween event in VAR_B2 is tween event fade...

            // If VAR_B2's use speed value is true...
            
                // Apply the PT Chain (see first comment) 
                // where TWEEN_FUNC is FadeAtSpeed
                // and use speedValueOfTween
                
            // Otherwise...
            
                // Apply the PT Chain 
                // where TWEEN_FUNC is Fade
                // and use timeValueOfTween
             
        // Else if type of tween event in VAR_B2 is tween event cancel...
        
            // call the CancelAllTweens() method on VAR_B1's game object
            
            // Run COROUTINE_A with parameters:
            // VAR_B2's timeValueOfTween and VAR_B3
            
        // Otherwise...
        
            // Run COROUTINE_A with parameters:
            // VAR_B2's timeValueOfTween and VAR_B3

    ```

3. In the variables section, add two new Serialized bools (referred to as `BOOL_1` and `BOOL_2`) 
    1. `BOOL_1`: Controls whether to apply effects to children images (the default value is up to you to decide).
    2. `BOOL_2`: Checks whether the Runner is active
    * Inside the `Awake` method, after all the current code, add this:
    Code outline:
    ```csharp
        // If targetObjectToTween is not null and BOOL_1 is true...
        
            // Foreach image in targetObjectToTween's get components in children...

                // Call Fade function on image with params:
                // startingAlphaValue, and 0

                // Call Fill function on image with params:
                // startingFillValue, and 0

        // Set BOOL_2 to false

        // Run COROUTINE_A with params: 1 and () => BOOL_2 = true
        // The 1 can be replaced with any other number or a variable timer
        // This will temporarily inactivate the tween system when spawned

    ```

4. Updating `RunTween()`:
* Add two new parameters:
    1. `Image imageToTween` in-between the `tween` and `current`
        * We can now allow child images to respond to the effects of the parent
    2. `bool isChild` at the end, default to `false`
        * Prevents child images from being counted for the `runningTweenEffects` variable
* Change `targetObjectToTween` to `imageToTween`
* Add a new if-statement before the first if-statement, 
    * Condition: Either `tween` or `imageToTween` is null 
    * Statement: exit the function, return nothing
* The now second if-statement should only have `current >= tween.helpers.Count` in the condition
    * Replace `runningTweenEffects--` with `if(!isChild) runningTweenEffects--` 
* Add one more if-statement:
    * Condition: `current` is less than or equal to 0
    * Statement: set `current` equal to 0 and  `if(!isChild) runningTweenEffects++` 
* In the last if-statement:
    * Replace `runningTweenEffects--` with `if(!isChild) runningTweenEffects--` 
* Below last if-statement, call `METHOD_B` with parameters:
    1. `currentTween`
    2. `imageToTween`
    3. `() => RunTween(tween, current + 1)`
        * This will run the next tween after the current one finishes
    4. `isChild`

5. `METHOD_C`: Go to `RunTweenDefault()` and add following code below
    * Code Outline:
    ```csharp
    // If BOOL_2 is false, exit the function

    // Call METHOD_B with params:
    // tweenScriptObj, targetObjectToTween, and 0

    // If targetObjectToTween is not null and BOOL_1 is true...
        
        // Foreach image in targetObjectToTween's get components in children...

            // Call METHOD_B with params:
            // tweenScriptObj, image, 0, and true
    
    ```

6. `METHOD_D`: This function is similar to `METHOD_C` but has overridden the preset value
    * Copy `RunTweenDefault()` and rename it to `RunTweenOverride(TweeningScriptObj newPreset)`
    * Replace `tweenScriptObj` with `newPreset`

### Updating **HealthBar** script

* Add `CustomUnityEvent` to the `using` block
* Add a new variable `public UEventFloat`, which is a event that runs *on health bar update*.
* You can remove all the code in the `Awake()` method 
* In both `UpdateValue()` methods:
```csharp
    // Replace the lines below...
    RunTween(newValue / maxValue);
    // ... with Invoke on the UEventFloat variable with param: (newValue / maxValue)

```

### Bug fix
In `Melee` script, go to the `Aim()` method. In the `if`, add `|| currentTarget == null` to prevent the Melee from aiming when there is no current target.