# Melee Attack Type
## Idea
For this first part, we'll finish up creating the melee attack type. We will do this by spawning a turret that has the animation on a prefab and then having it play the attack animation when needed.
***NOTE: I've added an update containing some test animations to Github***

## Coding
Before starting, go ahead and create a new script for the Melee Attack type. Remove the `Start()` and `Update()` and change `MonoBehavior` to the base tower attack type. Save the script, we will come back to this later.

### Turret Settings
There are 4 more variables to add to the turret settings:
1. A `public bool` lambda (i.e. "=>") which will return whether the turret attack is melee. Set it to `towerAttackType.Type == typeof(MELEE_SCRIPT_NAME);` (replace `MELEE_SCRIPT_NAME` with the name of the melee attack script).
* The rest of the variables will be shown if the above variable is true  
2. `public GameObject` for the melee turret prefab
3. `public AnimationClip` for the idle animation clip
4. `public AnimationClip` for the attacking animation clip

### Melee Attack Script
Open up the script you created for the Melee Attack. 
* This script has three variables:
    1. A `public const string` for the animation speed. This equals `speed`
        * `const` means that something is constant, so it will **never** change
    2. A `private GameObject` for the current target
    3. A `private Animator` for the melee animator controller
* Override the following functions:
    1. The initializing function:
        * This has a single temporary `GameObject` variable
        * After the `base` initialization is called, set your current target to null
        * Add an `if-statement`
            * Condition: Variable #1 from turret setting is true
            * Inside the `if` block, set the temporary variable equal to `Instantiate()` function with parameters: variable #2 from turret setting (melee turret prefab) and this transform. Then, set the melee animator controller equal to the get component `Animator` in children from the temporary variable.
    2. The update target list function:
        * This is the same as the one in the regular attack type, you can copy it from there and change any names if needed
    3. The aiming target function:
        * This is the same as the one in the regular attack type, you can copy it from there and change any names if needed
    4. The attack function:
        * First, call the `SetFloat` function from your melee animator controller, with animation speed variable and `1/cooldown` as the parameters. This will make sure the animation speed matches the fire rate.
        * Next, if the current target is not null, call the base attack with cooldown.
    5. The attacking coroutine:
        * Before `return base`, call `Play()` function from the melee animator controller, with parameter: `$"{animController.GetLayerName(0)}.{turretSettings.[variable #4].name}"` (replace [variable #4] with the name you gave for attacking animation clip)
        * After `return base`, call `Play()` function from the melee animator controller, with parameter: `$"{animController.GetLayerName(0)}.{turretSettings.[variable #3].name}"` (replace [variable #3] with the name you gave for idle animation clip)
        * Change `return` into `yield return`, so that the attack can go back to idle after cooldown

### Melee Damaging Script
Create another new C# script for the melee damager. We will keep this simple for now.
* Remove `Start()` and `Update()`
* Add a single `public float` variable for the damage.
* Add `private void OnTriggerEnter2D()` with parameter `Collider2D` for the target:
    * Inside, check if the target has an `EnemyController` component. If it does, make it take damage, with parameters being the damage variable and normie damage indicator. 

## Animation Setup
The primary coding section is done, so now we will just need to add the animations. You can see the test animation clips and prefab in `Animation > Test` folder. You can test it out by adding the prefab and clips into a melee turret settings scriptable object. Also remember to attach the melee damaging script to the blade object (child of base turret) so that it can deal damage to the enemies.
