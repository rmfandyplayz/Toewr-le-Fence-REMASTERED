using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKillDamage : MonoBehaviour
{
    //This is the script for the new instant kill damage system from what's previously called "360 No Scope Damage." This type of damage will be known as "instant kill" damage now!

    //Brainstorming starts here!
    /*
    OVERALL GOAL:
    Make it so when instant kill damage happens, the enemy affected will instead be "smacked" out of the camera's bounds in the opposite direction that it was hit by the bullet. After it goes out of bounds, the super smash bros particle will play with it being when a player goes out of bounds.

    The particle can be found here:
    https://www.youtube.com/watch?v=NEEb2ellrkU
    It will be hand-drawn animated to avoid copyright

    
    Approximately how should this be accomplished?

    ? Change the damage system so the old 360 Noscope damage no longer kills the enemy instantly
    ? Code it so when an enemy gets hit, it flies out of camera bounds in a direction opposite of the bullet that it got hit with
    ? Once again, code it so when the enemy do go out of bounds, it is killed and the particle is played in the respective rotation.


    Subcategories:
    • Change the damage system so the old 360 Noscope damage no longer kills the enemy instantly
        ? Get rid of the damage indicator prefab itself
        ? Change all the code that makes it so the enemy dies when this type of damage is applied
    
    ? Code it so when an enemy gets hit, it flies out of camera bounds in a direction opposite of the bullet that it got hit with
        ? Unsure, so this is a prediction!
        ? Maybe make it so when an enemy gets hit, it saves the direction of the bullet in which it got hit in. If noscope damage applies, it will use said saved content and apply force on the enemy in the opposite direction.
    
    ? Once again, code it so when the enemy do go out of bounds, it is killed and the particle is played in the respective rotation.
        ? Like the one above, very unsure of how this can be accomplished.
        ? Let's pretend that the particle/animation thing is already done and in an animation prefab
        ? The first thing is that there could be a box collider 2D that is outside the camera's boundaries
        ? When an enemy hits the collider, the particle will play
            ? The way the particle plays could vary. But it will follow some set rules...
                ? If it gets hit in any one of the four sides of the collider, the particle will play in a direction parallel and opposite to the side. The particle will either be horizontal or vertical
                ? If an enemy hits the collider near the corners, the particle will play in a direction diagonal and opposite to the angles.
    */


}
