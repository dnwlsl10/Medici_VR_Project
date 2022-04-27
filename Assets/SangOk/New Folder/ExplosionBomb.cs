using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionBomb : MonoBehaviour
{

    public Image image;
    List<GameObject> explosionObject = new List<GameObject>();
    public float explodeDamage = 50;
    public float explodeRadius = 3;
    public GameObject explosion;
    public GameObject flame;
    // Start is called before the first frame update
    void Start()
    {
    }

    bool temp;
    // Update is called once per frame








    void Update()
    {

        BombManager.instance.OnFailed = () =>
        {

            BombManager.instance.isBombState = false;
            StartCoroutine(Explosion());
            ObjectCollect(10);
            foreach (GameObject obj in explosionObject)
            {
                if (obj.GetComponent<Rigidbody>() == true)
                {
                    if (obj.transform.name == "Player")
                    {
                        obj.GetComponent<CharacterController>().enabled = false;
                    }
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                    obj.GetComponent<Rigidbody>().AddExplosionForce(explodeDamage, transform.position, 5, explodeRadius, ForceMode.Impulse);
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            this.gameObject.transform.parent = null;
            BombManager.instance.isGameFail = true;

        };
    }


    IEnumerator Explosion()
    {
        Vector3 localposition = transform.position;
        Instantiate(flame, localposition, transform.rotation);
        Instantiate(explosion, transform.position, transform.rotation);

        yield return new WaitForSeconds(1.5f);

        Instantiate(explosion, transform.position, transform.rotation);

        yield return new WaitForSeconds(1.5f);

        Instantiate(explosion, transform.position, transform.rotation);
    }

    void ObjectCollect(float distance)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider coll in colls)
        {
            explosionObject.Add(coll.gameObject);
        }
        for (int i = 0; i < colls.Length; i++)
            print(colls[i].transform.name);
    }
    IEnumerator FadeOutOver()
    {

        image.gameObject.SetActive(true);
        float alphaCount = 0;
        while (alphaCount < 1.0f)
        {
            alphaCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, alphaCount);

        }
    }

}
