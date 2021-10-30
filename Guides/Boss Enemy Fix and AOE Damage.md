# Boss Enemy and AOE Damage

## Fixing Boss Enemy

The reason we had that weird issue with the enemies spawned by the boss was because there is a path movement script still attached to Enemy child object. Open up the `EnemyHolder` prefab. Go to the `NormalEnemy` and remove the `PathMovement` component. This will fix the issue.  
***Note: GetComponentInParent will first check the current object for the component before looking in its parent. This is why there was this issue.***

## Fixing Movement

With the spawning fixed, there will be another issue, which is that spawned enemies will just move in the same direction when spawned, but never go to the next waypoint. To fix this, we will just make sure to cache the previous position and then use it to automatically set the enemy to the correct path.

* First, open up the `PathMovement` script
* Add a new `private Vector3` for the previous position (place after `Vector3 velocity`)
* In `Move()`, set the previous position to the transform position
* In `Update()`, add an `else if` in between the if and else:
  * Note: waypoint = `path.nodes[targetIndex]`
  * Condition: (previous position - waypoint).sqrMagnitude is less than (transform position - waypoint).sqrMagnitude
  * Body Statement: Set transform position equal to waypoint
* This should now allow all enemies to follow the path correctly
* **Note: If enemy goes too far from waypoint, it will teleport to the current waypoint**

## Area of Effect Damage for Bullets

To create area of effect damage for bullets, we will need to modify the `BulletSetup` and `BulletController`.

* For `BulletSetup`, I added 2 variables and a property: public `bool`, [SerializeField] float, and float property that returns the [SerializeField] float when the bool is true, and 0 otherwise. This follows the same format as the damage chance we worked on.
* For `BulletSetup`, you'll only need to add a function and change a line inside `OnTriggerEnter2D`.
  * Add a private void function which will apply general damage and take three parameter, an EnemyController, a float, and a damageIndicatorType
    * The function uses `Physics2D.OverlapCircleAll` to find all collision within the range of the area of effect radius positioned from the enemy controller's position (if this is 0, then it acts like a point and will only damage the enemy it hit). For each of the colliders, if the collider hit has an enemy controller component, then apply damage to that enemy.
    * Inside the function, I had 1 `foreach loop` and 1 `if-statement`
  * You can now replace the `enemy.TakeDamage()` with the new function created above, placing the correct parameters
