using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIsBomb : MonoBehaviour
{
    GameObject[] myObjects;
    // Start is called before the first frame update
    void Start()
    {
        bombedObjects = new HashSet<GameObject>();

    }

    public float explodeDamage;
    public float explodeRadius;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetObjectsNearBy(10);
            foreach(GameObject obj in bombedObjects)
            {
                if(obj.GetComponent<Rigidbody>() == null)
                {
                    obj.AddComponent<Rigidbody>();  
                }
                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.GetComponent<Rigidbody>().AddExplosionForce(explodeDamage, transform.position, explodeRadius);

            }

        }
    }

    HashSet<GameObject> bombedObjects;

    void GetObjectsNearBy(float radius, int layermask)
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, layermask);
        
        foreach (Collider col in cols)
        {
            bombedObjects.Add(col.gameObject);
        }
    }

    void GetObjectsNearBy(float radius)
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
      

        foreach (Collider col in cols)
        {
            bombedObjects.Add(col.gameObject);
        }
    }

    void resetBombedObjectSet()
    {
        bombedObjects.Clear();
    }


}
