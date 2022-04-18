using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;
using UnityEngine.Scripting;

[SerializeField] public class StatusEffectCenter
{
	//Variables section. Try to use [Header("[text]")] to organize the code.

	[Header("Turret Effects")]
	public float effectChance;
	public float effectDuration;
	public float effectLevel;
	[Header("Effect Image")]
	public Image activeEffectSprite;
    public Image inmmuneEffectSprite;

    /*
	public bool slow;
	[ShowIf(nameof(slow),true)] public float slowChance;
	[ShowIf(nameof(slow), true)] public float slowLevel;
	[ShowIf(nameof(slow), true)] public float slowDuration;

	public bool freeze;
	[ShowIf(nameof(freeze), true)] public float freezeChance;
	[ShowIf(nameof(freeze), true)] public float freezeDuration;

	public bool knockback;
	[ShowIf(nameof(knockback),true)] public float knockbackChance;
	[ShowIf(nameof(knockback),true)] public float knockbackDistance;

	public bool defenseDown;
	[ShowIf(nameof(defenseDown),true)] public float defenseDownChance;
	[ShowIf(nameof(defenseDown), true)] public float defenseDownDuration;
	[ShowIf(nameof(defenseDown), true)] public float defenseDownLevel;

	public bool incendiaryDamage;
	[ShowIf(nameof(incendiaryDamage),true)] public float incendiaryDamageChance;
	[ShowIf(nameof(incendiaryDamage),true)] public float incendiaryDamageAmount;

	public bool cringe;
	[ShowIf(nameof(cringe),true)] public float cringeChance;
	[ShowIf(nameof(cringe), true)] public float cringeDuration;
	[ShowIf(nameof(cringe), true)] public float cringeLevel;

	public bool curse;
	[ShowIf(nameof(curse),true)] public float curseChance;
	[ShowIf(nameof(curse), true)] public float curseDuration;

	public bool tranquilize;
	[ShowIf(nameof(tranquilize),true)] public float tranquilizeChance;
	[ShowIf(nameof(tranquilize), true)] public float tranquilizeDuration;

	[Header("Enemy Buffs")]
	public bool defenseUp;
	[ShowIf(nameof(defenseUp), true)] public float percentOfHealthWhenDefenseUpApplies;
	[ShowIf(nameof(defenseUp),true)] public float defenseUpLevel;

	public bool adrenalineRush;
	[ShowIf(nameof(adrenalineRush),true)] public float percentOfHealthWhenAdrenalineRushApplies;
	[ShowIf(nameof(adrenalineRush), true)] public float adrenalineRushLevel;

	public bool regeneration;
	[ShowIf(nameof(regeneration),true)] public float percentOfHealthWhenRegenerationApplies;
	[ShowIf(nameof(regeneration), true)] public float percentOfHealthWhenRegenerationStops;
	[ShowIf(nameof(regeneration), true)] public float regenerationLevel;

	public bool dodge;
	[ShowIf(nameof(dodge), true)] public float dodgeChance;
	[ShowIf(nameof(dodge),true)] public float dodgeDuration;

	public bool heavyweight;
	*/
    //Functions section



    //Coroutine Section



}

/*
Quick References:

abstract - A template for other classes
virtual - Allows implimintation inside the function
protected - Lets the abstract class access items within the subclass
static - Allows other classes to use the target content without creating the class
override - Override the function from the previous (inherited) class
base - Call the super class from the previous (inherited) class
cont - Constant; this value will never change


To create objects: Instantiate([prefab], [position], [rotation (quaternion)])
To destroy objects: Destroy([gameObject])
Accessing objects: GetComponent<[Script]>()

Physics:
-OnCollisionEnter
-OnCollisionExit
-OnCollisionStay
-OnTriggerEnter
-OnTriggerStay
-OnTriggerExit

Inputs:
Input.GetKeyDown([KeyCode]) - Any key being pressed
Input.GetKeyUp([KeyCode]) - Any key being released
Input.GetKey([KeyCode]) - Any key being pressed & held down

General foreach() format:
foreach([variable type] [variable name] in [target]){}

General for() format:
for([variable type] [variable name] = [starting index]; [variable name] [</>/= (condition)] [target variable to set condition], [variable name] [+/-] [value (step; indicates how much the index moves with the conditions met)]){} 

*/
