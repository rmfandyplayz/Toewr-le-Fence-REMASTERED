using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling objPoolReference;
    [SerializeField] private SerializedDictionary<string, List<GameObject>> objectPool = new SerializedDictionary<string, List<GameObject>>();

    private void Awake()
    {
        if(objPoolReference == null)
        {
            objPoolReference = this;
        }
    }

    public GameObject NewObject(GameObject prefab, Transform parent = null)
    {
        GameObject newObj = Instantiate(prefab);
        newObj.transform.SetParent(parent != null ? parent : this.transform);
        newObj.name = prefab.name;
        return newObj;
    }

    public static void Preload(GameObject prefabObject, int preloadAmount)
    {
        if(objPoolReference == null)
        {
            return;
        }
        if (objPoolReference.objectPool.ContainsKey(prefabObject.name))
        {
            return;
        }

        List<GameObject> objectList = new List<GameObject>();
        for(int i = 0; i < preloadAmount; i++)
        {
            var newObj = objPoolReference.NewObject(prefabObject);
            objectList.Add(newObj);
            newObj.SetActive(false);
        }
        objPoolReference.objectPool.Add(prefabObject.name, objectList);
    }

    public static GameObject GetGameObject(GameObject prefab, Transform parent = null)
    {
        if(objPoolReference == null)
        {
            Debug.Log(prefab);
            return Instantiate(prefab);
        }
        List<GameObject> list;
        GameObject obj;
        if (objPoolReference.objectPool.TryGetValue(prefab.name, out list) && list.Count > 0)
        {
            obj = list[list.Count-1];
            list.RemoveAt(list.Count - 1);
            obj.SetActive(true);
            if (parent != null)
            {
                obj.transform.SetParent(parent);
            }
            return obj;
        }
        else
        {
            return objPoolReference.NewObject(prefab, parent);
        }
    }

    public static void ReturnObject(GameObject prefab)
    {
        if (objPoolReference == null)
        {
            Destroy(prefab);
            return;
        }
        List<GameObject> objectList;
        if (objPoolReference.objectPool.TryGetValue(prefab.name, out objectList))
        {
            objectList.Add(prefab);
        }
        else
        {
            objectList = new List<GameObject>();
            objectList.Add(prefab);
            objPoolReference.objectPool.Add(prefab.name, objectList);
        }
        prefab.SetActive(false);
        prefab.transform.SetParent(objPoolReference.transform);
    }
}
