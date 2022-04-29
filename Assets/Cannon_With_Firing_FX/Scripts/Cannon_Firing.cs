using UnityEngine;
using System.Collections;

public class Cannon_Firing : MonoBehaviour {
	
public GameObject CannonObject;
public AnimationClip CannonFireAnim;
public ParticleSystem CannonMuzzleFlash;
public Light MuzzleFlashLight;
public ParticleSystem SparkParticles;
public ParticleSystem SmokeParticles;
public GameObject FuseObject;
public GameObject FuseCentre;
public ParticleSystem FuseSmokeParticles;
public GameObject FuseSmokeParticlesNode;	
public Light FuseLight;
public AudioSource CannonFireAudio;
public AudioSource BurningFuseAudio;

private Renderer fuseObjectRenderer;

private float offset = 0;
private float fuselightintensity = 0.6f;	
private float explodeset = 0;
private float explodehalt = 0;
private float cannonfired = 0;
private float fadeStart = 3;
private float fadeEnd = 0;
private float fadeTime = 1;
private float t = 0.0f;
private float fuseLit = 0;

  
void Start (){
	MuzzleFlashLight.intensity = 0;

	fuseObjectRenderer = FuseObject.GetComponent<Renderer>();

}  
  
  
void Update (){
 
	if (Input.GetButtonDown("Fire1")) //check to see if the left mouse was pressed - lights fuse
    {
              
    	if (cannonfired != 1)
    	{
    	
    		if (fuseLit != 1)
    		{  
    		offset = 0;   	
			StartCoroutine("Fuse");
     		BurningFuseAudio.Play();
     		FuseCentre.SetActive(true);
     		FuseSmokeParticlesNode.SetActive(true);
    		explodehalt = 0;
			explodeset = 0;
			}
		
     	}
     	 
    }
     
    if (Input.GetButtonDown("Fire2")) //check to see if the right mouse was pushed - resets cannon
    {
    
    	explodehalt = 1;
     	explodeset = 0;
     	offset = 0.5f;
     	FuseObject.SetActive(true);
     	FuseCentre.SetActive(false);
     	fuselightintensity = 0;
     	// FuseObject.GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", Vector2(0,0));

		// Vector2 offsetVector = new Vector2 (0, 0);
		fuseObjectRenderer.material.SetTextureOffset ("_MainTex", new Vector2(0,0));

     	SparkParticles.Clear();
     	FuseSmokeParticlesNode.SetActive(false);
     	BurningFuseAudio.Stop();
     	MuzzleFlashLight.intensity = 0;
     	cannonfired = 0; 
     	fuseLit = 0;
     	
	}


	FuseSmokeParticlesNode.transform.position = new Vector3(FuseCentre.transform.position.x, FuseCentre.transform.position.y, FuseCentre.transform.position.z);
      
    fuselightintensity = (Random.Range(0.2f,0.4f));
    FuseLight.intensity = fuselightintensity;
     
     
    if (explodeset == 1)
    {
    	FireCannon();
    }
           
 }
 
 
IEnumerator Fuse (){


              
	while (offset < 0.43f)
    {  
    	offset += (Time.deltaTime * 0.11f);
     	// FuseObject.GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", Vector2(offset,0));

		fuseObjectRenderer.material.SetTextureOffset ("_MainTex", new Vector2(offset,0));

     	fuseLit = 1;
     	yield return 0;
    }
 
    if (explodehalt != 1)
    {
    	explodeset = 1; 
    }
     
    offset = 0;


}


void FireCannon (){
    
    FuseCentre.SetActive(false);
    FuseSmokeParticles.Stop();
    fuselightintensity = 0;
    explodeset = 0;
	CannonMuzzleFlash.Play();
    SparkParticles.Play();
    SmokeParticles.Play();
    CannonFireAudio.Play();
	StartCoroutine("FadeLight");
    // CannonObject.transform.GetComponent<Animation>().GetComponent.<Animation>().Play("CannonFireAnim");

	CannonObject.GetComponent<Animation>().Play ();

    cannonfired = 1;


}


IEnumerator FadeLight (){
	while (t < fadeTime) 
	{
    	t += Time.deltaTime;
		MuzzleFlashLight.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
        yield return 0;  
    }              
            
	t = 0;  
}
 
 

 
}