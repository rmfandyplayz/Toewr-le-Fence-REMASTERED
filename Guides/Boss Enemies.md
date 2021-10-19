# Boss Enemies Guide

## Idea

Boss enemies are a special type of enemy that can spawn other enemies inside the level. We will want to keep boss enemies as close to regular enemies as possible and only add extra items onto of it. This means keeping the **EnemySetup** and **EnemyController** scripts mostly the same (maybe make minor additions if needed).

## Coding Guide

### EnemySetup

* We just need to add some variables that respond to the `isBossEnemy` boolean (recall that we can use the `ShowIf()` attribute to hide/show boss enemy variables).
* We will need the following variables (all *public*):
  1. **[var_1]**: `int`, which reprsents the maxinum number of enemies to spawn while the boss is still alive
  2. **[var_2]**: `float`, which reprsents how long to wait between spawning enemies

### SpawningManagement (part 1)

* We will first refactor the spawning script to make it easier to create enemies from the boss enemy script
* Create a `public static GameObject` function (I called it `SpawnEnemy`) with parameters: `WaveObjects wave, GameObject enemyPrefab, Polyline path` (keep the same name since we will be copying and pasting the code)
  * Copy and cut the code inside the for-loop of `IEnumerator PauseBetweenSpawning` (starting from `int enemyChoice` and up to `enemyPath.speed` [before `yield return`]).
  * Place the copied code into the `SpawnEnemy` function and at the end, return `Enemy`.
  * Go back to inside the for-loop of `IEnumerator PauseBetweenSpawning` and call `SpawnEnemy` with parameters `wave`, `enemyPrefab`, and `path`
* We will come back to the script later, so just save and continue.

### [BossEnemy]
* I will refer to the component as `[BossEnemy]`, but you can chose a different script name
* The main differentiator for the boss enemy is that it can spawn other enemies, so we will make a component for that.
* Remove the `Start` and `Update` as we won't really need to use those functions
* Add three `[SerializeField]` variables:
  * `WaveObjects` **[var_b1]**: This is the wave object informaiton provided when the boss was spawned
  * `EnemySetup` **[var_b2]**: The boss enemy information (as the enemy setup scriptable object)
  * `SpawningManagement` **[var_b3]**: The spawner that spawned the boss enemy (used to provide the prefab and path info)
* Create a `private IEnumerator` `[func_1]` that has no parameters: This will be spawning the enemies in the boss
  * In the funtion, add a for-loop which starts at 0 and ends at `[var_b2].[var_1]`:
    * Call `SpawnEnemy` from `SpawningManagement` and pass in as paramter `[var_b1]`, `[var_b3].enemyPrefab`, and `[var_b3].path`. Create a temporary variable to capture the returned `enemy` GameObject.
    * For the temporary variable `enemy`, set its' transform position to the boss enemy's transform position
    * Then, *yield return* a **new** ***wait for seconds*** timer with parameter `escript.[var_2]`
* Create a `public void` function `[func_2]` with three parameter `WaveObjects [param1], EnemySetup [param2], SpawningManagement [param3]`: This will initialize the boss enemy
  * Inside, set:
    * `[var_b1]` = `[param1]`
    * `[var_b2]` = `[param2]`
    * `[var_b3]` = `[param3]`
  * After that, start the `[func_1]` coroutine

### SpawningManagement (part 2)

* Now, we will just need to spawn the boss enemy
* Create a `private void` function (I'll refer to it as `[func_3]`) with parameter `WaveObjects` (refered to as `[param_4]`): This will spawn the boss enemy with the wave information
  * First, we will need to spawn the enemy. Create a temporary variable `[temp_1]` and call `SpawnEnemy` with parameters: `param_4`, `enemyPrefab`, and `path`
  * Create another temporary variable `[temp_2]` that will be set to get component in children `EnemyController` of `[temp_1]`
  * Set the escript of `[temp_2]` equal to `wave.bossEnemies[0]` (to be customized later)
  * Get the `PathMovement` component from `[temp_1]` and set its' `speed` value equal to `[temp_2].escript.speed`
  * Create one more temporary variable `[temp_3]` and set it equal to `[temp_1].AddComponent<[BossEnemy]>()` (This add the defualt boss enemy componet).
  * Call `[func_2]` from `[temp_3]` with parameters `[param_4]`, `[temp_2]`, and `this`
* Go to the `void Spawn` function:
  * Before the `StartCoroutine`, add a `float` for a random number and a conditional statement (both `if` and `else`)
  * The `float` variable should be equal to a *Random* **Range** between 0.0 and 1.0
  * The `if` conditions are checking if the `float` variable is less than or equal to the `chanceOfBossWave` and the count of `wave.bossEnemies` is bigger than 0:
    * In the `if` block, call the `[func_3]` function with parameter `wave`
  * The `else` block will just have the original `StartCoroutine(PauseBetweenSpawning(0.3f, wave, waveNumber));`, signifying it as a regular wave

### Finishing
* You can create a boss enemy scriptable object
* Remember to add it to the wave manager and increase the `chanceOfBossWave`
* ***NOTE***: Currently, the enemy spawned from the boss enemy don't start at the same path as the boss. Will need to make some edits to the `PathMovement` script to add that functionality. 