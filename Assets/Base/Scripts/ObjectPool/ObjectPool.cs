using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    Dictionary<string,ActivateAndUnActivateArraySet> activateAndUnActivateArraySets;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        activateAndUnActivateArraySets = new Dictionary<string, ActivateAndUnActivateArraySet>();
    }

    internal bool AddObjectInObjectPool(string prefabName)
    {
        string prefabFullName = "Prefabs/" + prefabName;
        if ( Resources.Load(prefabFullName) == null)
        {
            return false;
        }

        activateAndUnActivateArraySets.Add(prefabName, new ActivateAndUnActivateArraySet(prefabFullName));
        return true;
    }

    internal GameObject GetActiveObject(string objectName)
    {
        if(activateAndUnActivateArraySets.ContainsKey(objectName)) {
            return activateAndUnActivateArraySets[objectName].GetActiveObject();
        }
        return null;

    }

    internal void DestoryActiveObject(GameObject activeObject)
    {
        string objectName = activeObject.name.Substring(0, activeObject.name.IndexOf('('));
        activateAndUnActivateArraySets[objectName].DestoryActiveObject(activeObject);
    }

}


internal class ActivateAndUnActivateArraySet : Object
{
    ArrayList activateArray = new ArrayList();
    ArrayList unActivateArray = new ArrayList();
    string prefabFullName;
    GameObject prefab;

    internal ActivateAndUnActivateArraySet(string prefabFullName)
    {
        this.prefabFullName = prefabFullName;
        prefab = Resources.Load<GameObject>(prefabFullName);
    }

    internal void AddInstiateObejctInUnActiveArray()
    {
        GameObject instiateObject = Instantiate(prefab);
        instiateObject.SetActive(false);
        unActivateArray.Add(instiateObject);
    }

    internal GameObject GetActiveObject()
    {
        
        if(unActivateArray.Count == 0 )
        {
            AddInstiateObejctInUnActiveArray();
        }

        GameObject tossedObject = (GameObject)unActivateArray[0];
        unActivateArray.RemoveAt(0);
        activateArray.Add(tossedObject);
        tossedObject.SetActive(true);
        return tossedObject;
    }

    internal void TurnOffActiveObject(GameObject activeGameObject)
    {
        unActivateArray.Add(activateArray);
        activeGameObject.SetActive(false);
        activateArray.Remove(activeGameObject);
    }

    internal void DestoryActiveObject(GameObject activeObject)
    {
        activateArray.Remove(activeObject);
        Destroy(activeObject);
    }

}
