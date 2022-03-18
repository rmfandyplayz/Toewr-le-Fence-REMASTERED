using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesHolder : MonoBehaviour
{
    //Variables section. Try to use [Header("[text]")] to organize the code.
    [EditorButton(nameof(TestLoading))]
    public List<string> keys = new List<string>();
    public List<TurretSettings> turretsList = new List<TurretSettings>();

    public static SerializedDictionary<string, List<ScriptableObject>> scriptableObjectDictionary = new SerializedDictionary<string, List<ScriptableObject>>(); //String = key
    private const string SCRIPTOBJ = "ScriptableObject";

    private AsyncOperationHandle<IList<TurretSettings>> trackLoadedObjects; //tracks what objects are loaded from the addressables and loads it into the lists above

    //Functions section
    
    void Awake()
    {
        StartCoroutine(LoadItemsAsync());
        Debug.LogError("Started loading objects! From AddressablesHolder.cs");
    }

    private void OnDestroy()
    {
        //if(trackLoadedObjects != null)
        //{
            Addressables.Release(trackLoadedObjects);
        //}
    }

    public void TestLoading()
    {
        StartCoroutine(LoadItemsAsync());
    }

    public static List<ScriptableObject> FilterByType(System.Type objectType)
    {
        if(scriptableObjectDictionary.TryGetValue(objectType.Name, out var cachedList))
        {
            return cachedList;
        }
        var objectList = new List<ScriptableObject>();
        if (scriptableObjectDictionary.TryGetValue(SCRIPTOBJ, out var currentObjectList)){
            foreach (var item in currentObjectList)
            {
                if (objectType.IsAssignableFrom(item.GetType()))
                {
                    objectList.Add(item);
                }
            }
        }
        scriptableObjectDictionary.Add(objectType.Name, objectList);
        return objectList;
    }

    //Coroutines section

    public IEnumerator LoadItemsAsync()
    {
        var loadedItems = Addressables.LoadAssetsAsync<ScriptableObject>(keys, (items) =>
        {
        //Debug.LogError(items.name + " AddressablesHolder.cs");
            if (!scriptableObjectDictionary.ContainsKey(SCRIPTOBJ))
            {
                scriptableObjectDictionary[SCRIPTOBJ] = new List<ScriptableObject>();
            }
            if (!scriptableObjectDictionary[SCRIPTOBJ].Contains(items))
            {
                scriptableObjectDictionary[SCRIPTOBJ].Add(items);
            }
        }, Addressables.MergeMode.UseFirst, true);
        yield return loadedItems ;
        //Debug.LogError("Finished loading objects! From AddressablesHolder.cs");
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
