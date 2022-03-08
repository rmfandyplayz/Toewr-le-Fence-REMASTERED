using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Tweening_Dynamic_Transfer
{
    [SerializeField, NewLabel("Dynamic Values - Vector4"), Tooltip("If using float, use X (This could be used for Fade and Fill)\nIf using Vector3, use XY[Z] (This could be used for Move, Rotate [Use ONLY Z for 2D rotation], Scale))\nIf using color, use XYZW, which is RGBA (red, green, blue, alpha)")]
    private Vector4 dynamicValue;

    public float dynamicValueFloat
    {
        get { return dynamicValue.x; }
        private set { dynamicValue.x = value; }
    } //Allows to choose dynamic float value type
    public Tweening_Dynamic_Transfer(float value) : this()
    {
        dynamicValueFloat = value;
    }
    public Vector3 dynamicValueVector3
    {
        get { return dynamicValue; }
        private set { dynamicValue = value; }
    } //Allows to choose dynamic float value type
    public Tweening_Dynamic_Transfer(Vector3 value) : this()
    {
        dynamicValueVector3 = value;
    }
    public Color dynamicValueColor
    {
        get { return dynamicValue; }
        private set { dynamicValue = value; }
    } //Allows to choose dynamic float value type
    public Tweening_Dynamic_Transfer(Color value) : this()
    {
        dynamicValueColor = value;
    }

    //Auto Caster (Implicit Casting) Allows to convert some other type to dynamic transfer

    public static implicit operator Tweening_Dynamic_Transfer(float value)
    {
        return new Tweening_Dynamic_Transfer(value);
    }

    public static implicit operator Tweening_Dynamic_Transfer(Vector3 value)
    {
        return new Tweening_Dynamic_Transfer(value);
    }

    public static implicit operator Tweening_Dynamic_Transfer(Color color)
    {
        return new Tweening_Dynamic_Transfer(color);
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
