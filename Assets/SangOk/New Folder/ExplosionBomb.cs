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
    public OVRInput.Controller hand;
    // Start is called before the first frame update
    void Start()
    {
    }

    bool temp;
    // Update is called once per frame


    public Transform target;





    void Update()
    {

        if (BombManager.instance.isGameSuccess)
        {
            this.gameObject.transform.parent = target.transform;
            this.gameObject.transform.localPosition = target.position;
        }

        BombManager.instance.OnFailed = () =>
        {

            BombManager.instance.isBombState = false;
            StartCoroutine(Explosion());
            ObjectCollect(10);
            foreach (GameObject obj in explosionObject)
            {
                if (obj.transform.name == "FinallyPlayer")
                {
                    obj.gameObject.AddComponent<Rigidbody>();
                    obj.GetComponent<CharacterController>().enabled = false;
                }
                if (obj.GetComponent<Rigidbody>() == true)
                {
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                    obj.GetComponent<Rigidbody>().AddExplosionForce(explodeDamage, transform.position, 5, explodeRadius, ForceMode.Impulse);
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }

            this.gameObject.transform.parent = null;
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100f, ForceMode.Impulse);
            this.gameObject.GetComponent<Rigidbody>().AddTorque(Vector3.forward * 100f, ForceMode.Impulse);
            PostProcessScript.instance.isPlayerDead = true;
            //게임 실패
            BombManager.instance.isGameFail = true;
            StartCoroutine(VibrateController(1f, 1f, 1f, hand));

        };
    }


    protected IEnumerator VibrateController(float waitTime, float frequenct, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequenct, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
        OVRInput.SetControllerVibration(0, 0, controller);

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
