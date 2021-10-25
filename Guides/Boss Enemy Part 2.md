# Boss Enemies Guide (Part 2)

## Idea

To fix the issue with the enemy spawning, we will want to update some scripts to handle delaying the spawning and movment. This will require updating `EnemySetup`, `PathMovement`, `SpawningManagement`, and `BossEnemy` scripts. The majority of the changes will be in `PathMovement`, with the others having only minor tweaks to the code to follow the updated movement code.

## Coding Guide

### EnemySetup

For this script, add two `float` variables: one for delaying how long the boss enemy starts to spawn (as in how long to wait before spawing the first enemy), and another for how long to delay the enemy movement after spawning (as in how long an enemy spawned by the boss has to wait before moving).

### Path Movement

1. Remove the `Start()` function, but keep a copy of `velocity = (path.nodes[targetIndex] - path.nodes[targetIndex - 1]).normalized * speed;` (we will use it later)
2. Add a `bool` to check for when the enemy is updating (set it initially to `false`)
3. We will add 5 functions to help improve the script
    * A `public int` funtion/property that just returns the `targetIndex` variable (allow other functions to read the value, but no outside changes)
    * A `public void` function to initialize the path, speed, and targetIndex variables, as well as the starting position (pass new values as parameters)
    * A `public void` function to actually start to move along the path. Has no parameters. 
        * Inside, paste the line `velocity = ...` from the old `Start()` and after that, set your updating bool variable to `true`
    * A `private IEnumerator` coroutine to delay starting the path. Has one `float` parameter for the delay time.
        * Inside, yield wait for the delay time. After that, call your start movement function
    * One last `public void` function that will just start the previous coroutine. Just has a single `float` parameter for the delay. 
4. Inisde the `if` condition in the `Update()` function, add a second condition (using **and**) to check if the updating bool (the one added above) is `true`
    * We only want to update when we haven't finished the path and we have actually started updating (basically, stay stopped until you call your start move function)

### Spawning Management

For the `static GameObject SpawnEnemy`:
* First, ***add*** three new parameters: an `int` for the starting index, a `float` for the start path delay, and a `Vector3` for a custom position
* Add a temporary variable for `Enemy.GetComponentInChildren<EnemyController>()` and replace any use of that in the function with that variable.
* Replace the `enemyPath.path` and `enemyPath.speed` with the `enemyPath` initialize function (passing in the appropriate values).
* After that, call the delayed start movement function (not the coroutine, since it is private) for `enemyPath` (passing in the appropriate values).

### Boss Enemy

In the `IEnumerator SpawnBoss` function:

* Before the loop, add a wait timer for starting time delay
* Add the three parameters in the `SpawningManagement.SpawnEnemy()`:
    * Remeber to use the "get current index" function from the parent's `PathMovement` component
    * Enemy Info has the delay timer
    * Use a temporary variable for the position so you can later on randomize the position with a function.
