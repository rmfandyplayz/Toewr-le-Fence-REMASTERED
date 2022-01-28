using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Toolbox;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	//Variables section. Try to use [Header("[text]")] to organize the code.
	[SerializeField] private Image background;
	[SerializeField] private GameObject tabToModify;
	[SerializeField] private TabGroup tabGroup;
	public UnityEvent onTabSelect;
	public UnityEvent onTabDeselect;




	//Functions section
    void Start()
    {
		background = GetComponent<Image>();
		if(tabGroup == null)
        {
			tabGroup = GetComponentInParent<TabGroup>();
        }
		tabGroup.AddButtonToTab(this);
	}

	/// <summary>
	/// Set the background of the image to a given sprite
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="sprite"> Sprite: this is the sprite the image will change to. </param>
	/// <return> Nothing </return>
	public void SetSprite(Sprite sprite)
    {
		background.sprite = sprite;
    }

	/// <summary>
	/// Called when a tab is selected
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <return> Nothing </return>
	public void SelectTab()
    {
			tabToModify?.SetActive(true);
			onTabSelect?.Invoke();
	}

	/// <summary>
	/// Called when a tab is deselected
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <return> Nothing </return>
	public void DeselectTab()
    {
		tabToModify?.SetActive(false);
		onTabDeselect?.Invoke();
	}

	/// <summary>
	/// Called when a pointer (such as mouse) clicks the tabbutton
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="eventData"> PointerEventData: this  information from the pointer</param>
	/// <return> Nothing </return>
	public void OnPointerClick(PointerEventData eventData)
    {
		tabGroup.OnTabSelect(this);
    }

	/// <summary>
	/// Called when a pointer (such as mouse) enter the tab button
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="eventData"> PointerEventData: this provides information from the pointer</param>
	/// <return> Nothing </return>
	public void OnPointerEnter(PointerEventData eventData)
    {
		tabGroup.OnHoverTab(this);
    }

	/// <summary>
	/// Called when a pointer (such as mouse) exits the tab button
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="eventData"> PointerEventData: this provides information from the pointer</param>
	/// <return> Nothing </return>
	public void OnPointerExit(PointerEventData eventData)
    {
		tabGroup.OnTabDeselect(this);
    }
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
