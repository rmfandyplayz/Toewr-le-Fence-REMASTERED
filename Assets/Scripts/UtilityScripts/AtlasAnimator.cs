using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;
using UnityEngine.U2D;


public class AtlasAnimator : MonoBehaviour
{
	//Variables section. Try to use [Header("[text]")] to organize the code.
	[SerializeField] SpriteAtlas spriteAtlas; //Contains sprites for animation (spritesheet)
	[SerializeField] SpriteRenderer spriteRenderer;  //Actual sprite changed in-game
	[SerializeField] Image image; 
	[SerializeField] Texture2D texture; //File name for each image in spritesheet
	public float animationSpeed = 1;

	




	//Functions section
    void Update()
    {
		int spriteNumber = (int)(Time.time * animationSpeed) % spriteAtlas.spriteCount;
        if (spriteRenderer)
		{
            spriteRenderer.sprite = spriteAtlas.GetSprite($"{texture.name}_{spriteNumber}");
        }
        if (image)
        {
            image.sprite = spriteAtlas.GetSprite($"{texture.name}_{spriteNumber}");
		}
    }

    public void ReassignAnimation(SpriteAtlas spriteAtlas, Texture2D newTexture, float newAnimationSpeed)
    {
		this.spriteAtlas = spriteAtlas;
        this.animationSpeed = newAnimationSpeed;
        this.texture = newTexture;
    }
	
	
	//Coroutine Section
	
	
	
}

/*
Quick References:

interface - A class where you can't put variables in it. Can only put functions. (Inherit from multiple interfaces) Think of this like a set of tasks that the interface is able to do.
typeof - Returns object information
abstract - Basically means "it exists, but I'm not going to tell you how it's implemented"
virtual - Allows implimintation inside the function
protected - Lets the abstract class access items within the subclass
static - Allows other classes to use the target content without creating the class
override - Override the function from the previous (inherited) class
base - Call the super class from the previous (inherited) class
cont - Constant; this value will never change

Template lambda function:
[public/private/protected] [bool/float/int/void] [name] => [return statement];

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
