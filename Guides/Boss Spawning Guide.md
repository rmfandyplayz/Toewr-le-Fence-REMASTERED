# Boss Spawning Enemy Guide

## Idea
For this guide, we will get the boss enemy to spawn other enemies around them and have them move to the path.

## Boss Enemy

### Spawning Around
To spawn the enemies around the boss, you'll need to group them all together and specify a random location to spawn from. 
* Open your enemy setup script and boss enemy script
* Enemy Setup Script:
    * Add two floats that can be shown if `boxShapedSpawningPattern` is set to `true`.
        1. A public float to specify the min and max "radius" for the random position spawning.
        2. A public float to specify the speed of the enemy to when trying to go down the path.
* Boss Enemy Script:
    * Add a new function for generating a random position. This function will return a `Vector3`
        * My function has 3 temporary variables (1 `Vector3` and 2 `floats`) and 1 `if statement`
        * The temporary Vector3 is equal to `transform.position - transform.localPosition`
        * In the function, if the box spawn pattern is `true`, choose a random x and y position value (these are my two temporary floats) relative to the temporary Vector3 and return this as a new Vector3. Otherwise, just return the temporary Vector3.
    * Once the function is finished, you can change `var pos =` in `SpawnBoss` to equal the result of your function.


### Moving onto the Path
To make sure the spawned enemies move along to the start of the path, we will just add a small script to pause the path movement until it is on the path.
* Create a new C# script that will align enemies onto the path. Remove the `Start()` and `Update()` functions.
* For this script:
    * We will need 2 variables
        1. private PathMovement to store the path movement script to enable and disable at certain points
        2. private Vector3 for the target position.
    * We will write 2 functions
        1. A public function which takes a `PathMovement`, a `Vector3`, and two `floats` (one for delay and one for speed) as parameter and returns nothing. It will initialize the variables described above and then start the coroutine described in the next part.
        2. A private coroutine, which take one Vector3 (the current position) and two `floats` as parameters (delay and speed from the previous part).
            * My coroutine has 1 temporary float variable and 1 `for` loop
            * For this function, you will disable the path movement script and yield for the delay time. Then, calculate the temp float movementDistance, which is the distance from current to target, multiplied by 100, all divided by speed [basically: Distance(current, target)*100/speed]. After that, add a for loop from 0 to movementDistance, which will lerp (Vector3 version) from the current position to the target position, with value `i` divided movementDistance. Also yield for 0.01 seconds. After the loop, reactivate the path movement script.
* Once finished with the script, go back to the boss enemy script and call the public function (use get component in parent), passing in the correct parameters:
    1. Path movement is also a component in the parent gameObject
    2. target position is the boss enemy's current position (minus its local position)
    3. Delay and speed are the two new floats from the `enemyInfo`
* Save all the scripts and add the new script to the `EnemyHolder` prefab.
* ***Final Note: There are sometimes teleportation issues when the boss spawns enemies near the waypoint.***