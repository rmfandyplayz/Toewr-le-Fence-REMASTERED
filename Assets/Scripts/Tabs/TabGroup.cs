using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;

public class TabGroup : MonoBehaviour
{
	//Variables section. Try to use [Header("[text]")] to organize the code.
	[SerializeField] private List<TabButton> tabButtonsList = new List<TabButton>();
	[SerializeField] private TabButton selectedTabButton;
	[SerializeField] private Sprite idleTab;
	[SerializeField] private Sprite hoverTab;
	[SerializeField] private Sprite pressedTab;
	//[SerializeField] private Sprite disabledTab; //(Implement this feature if needed in the future)




	//Functions section

	/// <summary>
	/// Add a tab button to the list of tab buttons
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="tab"> TabButton: this is the tab button to add</param>
	/// <return> Nothing </return>
	public void AddButtonToTab(TabButton tab)
    {
		if (tabButtonsList == null)
		{
			tabButtonsList = new List<TabButton> { tab };
		}
        else
        {
			tabButtonsList.Add(tab);
        }
    }

	/// <summary>
	/// Reset all the tabs to idle
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <return> Nothing </return>
	public void ResetTabs()
    {
		foreach(TabButton tab in tabButtonsList)
        {
			if(tab == selectedTabButton)
            {
				continue;
            }
            else
            {
				tab.SetSprite(idleTab);
            }
        }
	}
	
	/// <summary>
	/// Run when a tab is being hovered over.
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="button"> TabButton: current tab button of focus </param>
	/// <return> Nothing </return>
	public void OnHoverTab(TabButton button)
    {
		ResetTabs();
		if(selectedTabButton == null || button != selectedTabButton)
        {
			button.SetSprite(hoverTab);
        }
	}

	/// <summary>
	/// Run when a tab is being selected.
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="button"> TabButton: current tab button of focus </param>
	/// <return> Nothing </return>
	public void OnTabSelect(TabButton button)
    {
		if(selectedTabButton != null)
        {
			selectedTabButton.DeselectTab();
        }
		selectedTabButton = button;
		selectedTabButton.SelectTab();
		ResetTabs();
		button.SetSprite(pressedTab);
	}

	/// <summary>
	/// Run when a tab is on longer being selected.
	/// </summary>
	/// <remarks>
	/// Type: public function
	/// </remarks>
	/// <param name="button"> TabButton: the tab button that is no longer in focus</param>
	/// <return> Nothing </return>
	public void OnTabDeselect(TabButton button)
    {
		ResetTabs();
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
