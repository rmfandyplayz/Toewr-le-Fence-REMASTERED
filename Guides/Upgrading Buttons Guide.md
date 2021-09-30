# Upgrading Buttons Guide

## Problem Space:

When a tower is clicked, we want the upgrading panel to initialize buttons so that when they are clicked, they will update an upgradable value in the dictionary found in the tower configuration file. Once the upgrading is done, we need to make sure the values are updated correctly in both the text display, as well as the Tower Configuration script, and that the cost of the upgrade is applied to the gold manager.

## Idea:

### Tower Configuration

This should handle calculating and applying the price of the upgrade before the change (otherwise it will get the next counter's price). After finishing the upgrade, we want to do is make sure all the upgrade references are updated.

### Upgrading UI

Place all the button you need at the **start** of the game in an array or list. Deactivate all of them until they are needed. Since every button is equivalent to each other, we don't need to care about the order of the button.

Once a tower is clicked, we want to get it's upgrading counter (a serialized dictionary) and active enough buttons for every upgrade there. Once a button is clicked, there are three steps for completing the upgrade: a buying step (which removes the gold), an incrementing step (upgrading the counter value), and then applying the changes.

## Code Guide:

Before continuing, check GitHub for updates. I added a `UpgradeCounterInfo` class to help provide more info for the button. Also made minor edits to `UpgradableType` and `TurretRangeHandler` to make some functions more convenient to use. 

### UpgradeCounterInfo Script Guide

* Found in the TurretConfiguration script. No need to code anything for this class, this is just what has been updated
* Has 4 variables: a name (to be used on the button), a counter (for upgrading), a price (quick way to get calculated price), and a maxCounter (to prevent the counter from going over).
* Below the variables are just their Properties fields (used to control getting and setting the variables) and a Constructor (for initializing the UpgradeableType to modify)

### TurretConfiguration Script

* This should be simple to implement. We just need two small function: BuyUpgrade and ApplyUpgrade
* BuyUpgrade function

  * Why: We need to take away gold when the upgrade is called
  * Add a new `public void` function called `BuyUpgrade()` with a single input parameter `TypeOfUpgrade upgradeKey`
  * Get the `GoldManager` instance and call `AddGold` with negative "price" value.
    * The price can be determined by using `GetPrice()` in `upgradesCounter[upgradeKey].Upgrade`. For the input, use `CounterValue(upgradeKey)`.
* ApplyUpgrade function

  * Why: We want to apply all the changes to the upgrades counter.
  * Add a new `public void` function called `ApplyUpgrade()`
  * *Note: The variables for rotation and fire rate always read from the dictionary, so they already have the most up-to-date value (see `CounterValue()` function for each)*
  * We will just need to update the range
    * For the `rangeHolder` we want to call `UpdateRange()` where we pass in the `tsetting.range.GetUpgradedValue()` with the input parameter being `CounterValue(TypeOfUpgrade.Range)`.

### UpgradingUI Script:

* Initializing Buttons

  * First create a `List<Button>` variable (either *[SerializeField]* or *public*) to hold all the buttons. Remember to add them in the inspector after you are done.
  * In the `Start()`, you can use a `foreach` loop to **set active** each button's gameObject to `false`.
* UpgradeClick function

  * Why: This will need to handle what happens when the button is clicked.
  * Add a new `public void` function called `UpgradeClick()`. This will have 1 input parameter, `TypeOfUpgrade upgradeKey`, which is the upgrade type that will be applied
  * Inside, do a conditional check to see if `tconfig.upgradesCounter` **contains the key** `upgradeKey`.
  * If it passes (contains key is true), then you can do these steps:
    * Call `BuyUpgrade` for the `tconfig` with parameter `upgradeKey`
    * Increase the `Counter` in `tconfig.upgradesCounter[upgradeKey]` by 1
    * Call `ApplyUpgrade` for the `tconfig` with no parameter
    * Call `UpdateUI()` (the function in the next step, has no parameters)
* UpdateUI function

  * Why: We will be using this in two main situations: when the panel opens and when a button is clicked. Having a single function lets us easily use the same code for both and will keep changes in-sync.
  * Add a new `public void` function called `UpdateUI()`
  * First, move `tconfig` check and all the `textConfig` assignments (lines 50-60) into the function. After the move, you can replace that code block with just `UpdateUI()` (nothing should change or break for the game with this move).
  * Next, for each of the `textConfig` variables that have a `GetUpgradedValue`, you can replace the 0 with `tconfig.CounterValue(TypeOfUpgrade)`. This will automatically get the current counter value for the upgrade.
  * Add an int called `buttonIndex` = 0. We will use it in the loop to keep track of the currently selected button.
  * The last big part is creating the buttons. Setup a foreach-loop that gets every `counterPair` in `tconfig.upgradesCounter`. iInside the loop, do these steps:
    * Before the actual code, check if the index value (`buttonIndex`) is bigger your list of buttons. If it is, break out of the loop (there are no more buttons to assign).
    * (Optional) Create a temporary variable called `button` to hold the the button from the buttons list at index `buttonIndex`. This just makes the code a bit more managable, but the code can still work with the long form variable.
    * Set the button's gameObject to active
    * For the button's onClick event, **remove all the listeners** (removes old references to turret config upgrades).
    * Create a price variable = `counterPair.Value.Upgrade.GetPrice()` using the parameter `counterPair.Value.Counter`
    * Finally, there are three main button states. You can use a conditional block for each state:

      1. Not Enough Money State (button should not be `interactable`)
      * Condition: When the price is bigger than the gold in the GoldManager

      * Set the button's child `TextMeshProUGUI`'s text to have the upgrade name and price variable information

      2. Maxed Out Level State (button should not be `interactable`)
      * Condition: When the counter in the `upgradesCounter[counterPair.Key]` is bigger than or equal to the upgrade's max level in `upgradesCounter[counterPair.Key]`

      * Set the button's child `TextMeshProUGUI`'s text to have the upgrade name and say "MAXED OUT"

      3. Defualt State (button should be `interactable`, i.e. allow player to upgrade)

      * Set the button's child `TextMeshProUGUI`'s text to have the upgrade name and price variable information
      * **Add a listener** for the button's onClick event. It should be the lamdba: `() => {UpgradeClick(tconfig, counterPair.Key)}`

***With this, the upgrade system should now be working.***
