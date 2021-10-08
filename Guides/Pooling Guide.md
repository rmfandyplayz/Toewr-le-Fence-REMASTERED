# Object Pooling Guide

## Idea:

To prevent preformance slowdown when spawning and despawning lots of objects, we will use pooling to keep some objects inactive, but ready to use, and reactivate them when ready to use.


## Overview

We will need to create 1 new script and modify some others to get this working:

* New Script: **GameObjectPooler**
* Modify these scripts:
  * *DamageIndicatorManager*
  * *DamageIndicator*
  * *BulletController*
  * *TurretConfiguration*
  * *ObjectShake*

## Code Guide

### GameObjectPooler

* Create a new C# script called `GameObjectPooler`
* There are 2 variable and 5 function:
    * Variables:
        1. A **public static** *GameObjectPooler* called `instance`, which will act as a quick reference for other scripts
        2. A **private/[SerializedField]** *SerializedDictionary<string, List<GameObject>>* called `gameObjectPool`, which hold all pooled objects. Remember to set it equal to a new `SerializedDictionary<string, List<GameObject>>()`
            * Note: Using a list so that we can see it in the inspector
    * Functions:
        1. `private void Awake()`: This will predefine the instance to the current script
            * If `instance` is null, set `instance` equal to `this`
        2.  `private GameObject CreateNewObject()` with parameter `GameObject prefabObject`: Creates new object when needed by the pool
            * Create a GameObject variable called `newObject` and set it equal to `Instantiate(prefabObject)`
            * Set `newObject.transform.parent` to `this.transform` (places the new objects as a child of the pool)
            * Set the `newObject.name` to the `prefabObject.name` (consistent naming for the dictionary)
            * return `newObject`
        3. `public static void PreloadPrefab()` with parameters `GameObject prefabObject` and `int amount`: Preload the dictionary with a specific size
            * First, if `instance` is null, return nothing (if there is no pool, no need to preload prefab)
            * Second, if `instance.gameObjectPool` already **contains key** `prefabObject.name`, return nothing (the pool is already preloaded, no need to do it again)
            * Create a new `List<GameObject>` called `objList` and set it to an empty list (i.e. `new List<GameObject>()`).
            * Using a for loop from 0 up to (but not including) `amount`:
                * Call `instance.CreateNewObject` with parameter `prefabObject` and set it to a `GameObject newObject`
                * **Add** the `newObject` to `objList`
                * Set `newObject` active to `false`
            * After the loop, **add** the pair `prefabObject.name` and `objList` to `instance.gameObjectPool` (*Dictionary also has a .Add(), but with two parameters*)
        4. `public static GameObject GetGameObject` with parameter `GameObject prefabObject`: Get the specific prefab object inside the pool or create it
            * First, if `instance` is null, just instantiate the `prefabObject` and return it (if no pooling, just spawn as normal)
            * Next, create two new temporary variables: `List<GameObject> objList` and `GameObject object`
            * In an if-else statement, put these two conditions in this order (use `&&` to bind them together):
                1. `instance.gameObjectPool.TryGetValue` with parameters `prefabObject.name` and `out objList` (*out is required*)
                2. `objList` Count is greater than 0
                * In the `if` block: 
                    * Create a temp *GameObject* variable called `object` and set it to `objList[objList.Count-1]` (get the last item as it is the fastest to remove)
                    * Call `objList.RemoveAt(objList.Count-1)` to remove it from the list (so it becomes unavailable from the pool)
                    * Set `object` active to `true`
                    * return `object`
                * In the `else` block, we don't have an available object, so just return `instance.CreateNewObject` with parameter `prefabObject` to create a new one
        5. `public static void ReturnGameObject` with parameter `GameObject prefabObject`: Return an object to the pool
            * First, if `instance` is null, just destroy the `prefabObject` (if no pooling, just destroy as normal)
            * Then, create a new temporary variables `List<GameObject> objList`
            * In an if-else statement, use `instance.gameObjectPool.TryGetValue` with parameters `prefabObject.name` and `out objList`
                * In the `if` block, go ahead and just **add** `prefabObject` to `objList`.
                * In the `else` block:
                    * Set `objList` equal to a **new** `List<GameObject>()`
                    * **Add** `prefabObject` to `objList`
                    * **Add** `prefabObject.name` and `objList` `instance.gameObjectPool`
            * At the end of the conditional statement, set `prefabObject` active to `false`
* You should now have a working pooling system for any object
### Updating other scripts
* **DamageIndicatorManager**: This the only script that has breaking changes (you will need to setup the inspector again) 
    * Replace all the `public GameObject` prefab variables with a **[SerializeField] SerializedDictionary<damageIndicatorType, DamageIndicatorPoolInfo>** called `damageIndicatorDictionary` (this will automatically find the correct prefab based on the indicator type)
    * These are the changes that will go in the `void GenerateIndicator` function:
        * Remove the whole switch statement
        * Before the `if (damagePrefab != null)` add these line:
            * `if(!damageIndicatorDictionary.ContainsKey(damageInfo.damageType)) return;` (cannot spawn an unknown indicator type)
            * Set `GameObject damagePrefab` equal to `GameObjectPooler.GetGameObject(damageIndicatorDictionary[damageInfo.damageType].indicatorPrefab)`
        * Everything else should remain the same
* **DamageIndicator**
    * First, add a **boolean** variable called `isUpdating` and set it to `false` (we will use this to only update the indicator when it is activated)
    * Inside the `void InitializeIndicator`, set `isUpdating` equal to `true`
    * Change the `void Start()` to  `void OnEnable`, leaving the body section (inside curly braces) the same
        * Also, inside the function `damageIndicatorText.alpha = 1` so that the text show up correctly
    * Create `void OnDisable()` function with only thing it does is set `isUpdating` back to `false`
    * Inside the `Update()`, put all the code inside into an `if` statement, with the condition being `isUpdating` being `true`.
    * Inside the `IEnumerator IndicatorMovement` function, replace `Destroy(this.gameObject, + 0.01f);` with `GameObjectPooler.ReturnGameObject(this.gameObject)`
* **BulletController**
    * For all of the `Destroy(this.gameObject)`, replace it with `GameObjectPooler.ReturnGameObject(this.gameObject)`
* **TurretConfiguration**
    * Go into the foreach-loop in `IEnumerator Shoot()`:
        * Replace `Instantiate(bulletPrefab, firePoint.position, firePoint.rotation)` with `GameObjectPooler.GetGameObject(bulletPrefab);`
        * After that line, add `bullet.transform.position = firePoint.position` (puts bullet back into the correct location)
        * Also add `bullet.transform.rotation = firePoint.rotation` (puts bullet back into the correct rotation)
* **ObjectShake**
    * Just change the `void Start()` to  `void OnEnable` (leave the body of the function the same)