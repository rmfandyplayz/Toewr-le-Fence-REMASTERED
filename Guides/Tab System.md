# Creating a Tab System

## Idea
We will adapt the design from ["Creating a Custom Tab System in Unity"](https://youtu.be/211t6r12XPQ). You can refer to the code in the video, though there will be some minor changes to make switching tab a bit easier.

## Coding

### Tab Button - Part 1: Outline of class
* Create a new C# script for the **tab button** script, which will be the individual button for a given tab.
* In your `using` block before the class, make sure to include the three namespaces below:
    ```csharp
        UnityEngine.UI
        UnityEngine.EventSystem
        UnityEngine.Events
    ```
* Before the class, add a **RequireComponent(typeof(Image))** attribute, which will guarantee that an image will be associated with this script.
* Directly after the *: MonoBehavior*, you can add these three interfaces to allow this script to act like a button:
    ```csharp
        , IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    ```
* For now, add 5 SerializedField variables
    * **Image** for *background* image of tab
    * **GameObject** for the *tab object* to be shown/hidden
    * **UnityEvent** for *on tab selected* event
    * **UnityEvent** for *on tab deselected* event
* For the functionality, these are the methods and their associated comment summaries (no need to code anything inside the body just yet, we will do this in part 2).
    * void Start()
    * SetSprite
        ```csharp
            /// <summary>
            /// Set the background of the image to a given sprite
            /// </summary>
            /// <remarks>
            /// Type: public function
            /// </remarks>
            /// <param name="newSprite"> Sprite: this is the sprite the image will change to. </param>
            /// <return> Nothing </return> 
        ```
    * OnPointerEnter
        ```csharp
            /// <summary>
            /// Called when a pointer (such as mouse) enter the tab button
            /// </summary>
            /// <remarks>
            /// Type: public function
            /// </remarks>
            /// <param name="eventData"> PointerEventData: this provides information from the pointer </param>
            /// <return> Nothing </return> 
        ```
    * OnPointerClick
        ```csharp
            /// <summary>
            /// Called when a pointer (such as mouse) clicks the tab button
            /// </summary>
            /// <remarks>
            /// Type: public function
            /// </remarks>
            /// <param name="eventData"> PointerEventData: this provides information from the pointer </param>
            /// <return> Nothing </return> 
        ```
    * OnPointerExit
        ```csharp
            /// <summary>
            /// Called when a pointer (such as mouse) exits the tab button
            /// </summary>
            /// <remarks>
            /// Type: public function
            /// </remarks>
            /// <param name="eventData"> PointerEventData: this provides information from the pointer </param>
            /// <return> Nothing </return> 
        ```
    * Select
        ```csharp
            /// <summary>
            /// Called when a tab is selected
            /// </summary>
            /// <remarks>
            /// Type: public function
            /// </remarks>
            /// <return> Nothing </return> 
        ```
    * Deselect
        ```csharp
            /// <summary>
            /// Called when a tab is deselected
            /// </summary>
            /// <remarks>
            /// Type: public function
            /// </remarks>
            /// <return> Nothing </return> 
        ```
* This should finish the outline of the script, we will come back later to add the functionality.

### Tab Group
* Create another C# script for the **tab group** functionality, which will manage all tag buttons that are children of a group.
* For this script, you'll need 5 variables:
    * **List of Tab Buttons** for all the *tab buttons* assigned to this tab group
    * **Tab Button** for the currently *selected tab*
    * **Sprite** for *idle sprite* for tab image
    * **Sprite** for *hover sprite* for tab image
    * **Sprite** for *active sprite* for tab image
* These are the functions you will need for this script:
    * Subscribe
        ```csharp
        /// <summary>
        /// Add a tab button to the list of tab buttons
        /// </summary>
        /// <remarks>
        /// Type: public function
        /// </remarks>
        /// <param name="button"> TabButton: this is the tab button to add </param>
        /// <return> Nothing </return> 
        ```
        ```csharp
        // if tab button list is null...

            // then create a new empty list and assign it to the tab button list

        // add the tab button to the tab button list

        ```
     * ResetTabs
        ```csharp
        /// <summary>
        /// Reset all the tabs to idle
        /// </summary>
        /// <remarks>
        /// Type: public function
        /// </remarks>
        /// <return> Nothing </return> 
        ```
        ```csharp
        // for each tab button in the tab buttons list...

            // if the button is equal to the selected tab...

                // continue

            // otherwise, call SetSprite on the tab button, passing in the idle sprite

        ```
     * OnTabEnter
        ```csharp
        /// <summary>
        /// Run when a tab is being hovered over. 
        /// </summary>
        /// <remarks>
        /// Type: public function
        /// </remarks>
        /// <param name="button"> TabButton: current tab button of focus </param>
        /// <return> Nothing </return> 
        ```
        ```csharp
        // call ResetTabs

        // if selected tab is null or button is not equal to the selected tab...

            // call SetSprite on the button, passing in the hover sprite

        ```
    * OnTabSelect
        ```csharp
        /// <summary>
        /// Run when a tab is being selected. 
        /// </summary>
        /// <remarks>
        /// Type: public function
        /// </remarks>
        /// <param name="button"> TabButton: current tab button of focus </param>
        /// <return> Nothing </return> 
        ```
        ```csharp
        // if selected tab is not equal to null...

            // call Deselect for the selected tab

        // set selected tab equal to the button

        // call Select for the selected tab
        
        // call ResetTabs

        // call SetSprite on the button, passing in the active sprite

        ```
    * OnTabExit
        ```csharp
        /// <summary>
        /// Run when a tab is on longer being selected. 
        /// </summary>
        /// <remarks>
        /// Type: public function
        /// </remarks>
        /// <param name="button"> TabButton: the tab button that is no longer in focus </param>
        /// <return> Nothing </return> 
        ```
        ```csharp
        // call ResetTabs
        ```
* The code for the tab group is finished

### Tab Button - Part 2: Coding functionality
* Now, you can go back to the tab button script from part 1 and work on the functionality.
* Before adding the functions, add a SerializedField **Tab group** variable for the *main tab group*
* Functionality:
    * void Start()
        ```csharp
        // set the background equal to the get component of Image

        // if main tab group is null...

            // set main tab group equal to get component in parent of Tab Group

        // call Subscribe on main tab group with parameter this
        ```
    * SetSprite
        ```csharp
        // set the background's image equal to the new sprite
        ```
    * OnPointerEnter
        ```csharp
        // call OnTabEnter for main tab group, with parameter this
        ```
    * OnPointerClick
        ```csharp
        // call OnTabSelect for main tab group, with parameter this
        ```
    * OnPointerExit
        ```csharp
        // call OnTabExit for main tab group, with parameter this
        ```
    * Select
        ```csharp
        // null check tab object, if it exists, set active to true

        // null check the on tab selected event and Invoke it

        ```
    * Deselect
        ```csharp
        // null check tab object and set active to false

        // null check the on tab deselected event and Invoke it
        ```
* The code for the Tab Button is now finished.

## Wrap up
You have now finished up the code for the tab group and tab button scripts. You can now use them to create your tabbed menu. When setting it up, remember to have a single tab group component as the parent object, and as many tabs buttons as you need as children objects.