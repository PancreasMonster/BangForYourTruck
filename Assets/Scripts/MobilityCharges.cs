using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilityCharges : MonoBehaviour
{
    public int currentCharges;

    public bool charge1;
    public float charge1Time = 0f;
    public GameObject light1;

    public bool charge2;
    public float charge2Time = 0f;
    public GameObject light2;

    public AudioSource aud;
    public bool charge3;
    public float charge3Time = 0f;
    public GameObject light3;

    public float currentDriftMultiplier;

    public float rechargeTime = 3f;
    public float maxDriftMultiplier;

    public GameObject emission1;
    Renderer emissionMat1;
    public GameObject emission2;
    Renderer emissionMat2;
    public GameObject emission3;
    Renderer emissionMat3;

    Material startingMat;

    ParticleSystem particles1;
    ParticleSystem particles2;
    ParticleSystem particles3;
    public float intensity = 7.5f;

    //SpriteRenderer sprite1;
    //SpriteRenderer sprite2;
    //SpriteRenderer sprite3;
    //public GameObject noChargeParticlesParent;
    //ParticleSystem[] noChargeParticles;
    //public GameObject useChargeParticlesParent;

    // Start is called before the first frame update
    void Start()
    {
        //startingMat = emission1.GetComponent<Renderer>().material;

        emissionMat1 = emission1.GetComponent<Renderer>();

        emissionMat2 = emission2.GetComponent<Renderer>();

        emissionMat3 = emission3.GetComponent<Renderer>();

        emissionMat1.material.SetColor("_EmissionColor", Color.green * intensity);

        emissionMat2.material.SetColor("_EmissionColor", Color.green * intensity);

        emissionMat3.material.SetColor("_EmissionColor", Color.green * intensity);

        /*noChargeParticles = noChargeParticlesParent.GetComponentsInChildren<ParticleSystem>();

        sprite1 = light1.GetComponent<SpriteRenderer>();
        sprite2 = light2.GetComponent<SpriteRenderer>();    
        sprite3 = light3.GetComponent<SpriteRenderer>();
        */
        particles1 = light1.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        particles2 = light2.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        particles3 = light3.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        
        currentCharges = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDriftMultiplier > maxDriftMultiplier)
        {
            currentDriftMultiplier = maxDriftMultiplier;
        }

        

        if (charge1 == false)
        {
            charge1Time += (1 + currentDriftMultiplier) * Time.deltaTime;

        }

        if (charge1Time >= rechargeTime)
        {
            charge1 = true;
            currentCharges++;
            aud.pitch = .9f;
            aud.Play();
            particles1.Play();
            //sprite1.color = Color.green;
            emissionMat1.material.SetColor("_EmissionColor", Color.green * intensity);
            charge1Time = 0f;
        }

        if (charge2 == false)
        {
            charge2Time += (1 + currentDriftMultiplier) * Time.deltaTime;

        }

        if (charge2Time >= rechargeTime)
        {
            charge2 = true;
            currentCharges++;
            aud.pitch = .95f;
            aud.Play();
            particles2.Play();
            //sprite2.color = Color.green;
            emissionMat2.material.SetColor("_EmissionColor", Color.green * intensity);
            charge2Time = 0f;
        }

        if (charge3 == false)
        {
            charge3Time += (1 + currentDriftMultiplier) * Time.deltaTime;

        }

        if (charge3Time >= rechargeTime)
        {
            charge3 = true;
            currentCharges++;
            aud.pitch = 1f;
            aud.Play();
            particles3.Play();
            //sprite3.color = Color.green;
            emissionMat3.material.SetColor("_EmissionColor", Color.green * intensity);
            charge3Time = 0f;
        }

        if (charge1Time >= (rechargeTime * .25f) && charge1Time <= (rechargeTime * .5f))
        {
            //sprite1.color = Color.yellow;
            emissionMat1.material.SetColor("_EmissionColor", new Color(0.99f, 0.2f, 0) * intensity);
        }

        if (charge2Time >= (rechargeTime * .25f) && charge2Time <= (rechargeTime * .75f))
        {
            //sprite2.color = Color.yellow;
            emissionMat2.material.SetColor("_EmissionColor", new Color(0.99f, 0.2f, 0) * intensity);
        }

        if (charge3Time >= (rechargeTime * .25f) && charge3Time <= (rechargeTime * .5f))
        {
            //sprite3.color = Color.yellow;
            emissionMat3.material.SetColor("_EmissionColor", new Color(0.99f, 0.2f, 0) * intensity);
        }

        if (charge1Time >= (rechargeTime * .5f) && charge1Time <= (rechargeTime * .75f))
        {
            //sprite1.color = new Color(1.0f, 0.64f, 0.0f);
            emissionMat1.material.SetColor("_EmissionColor", new Color(1.0f, 0.64f, 0.0f) * intensity);
            //emissionMat1.color = new Color(1.0f, 0.64f, 0.0f);
        }

        if (charge2Time >= (rechargeTime * .5f) && charge2Time <= (rechargeTime * .75f))
        {
            //sprite2.color = new Color(1.0f, 0.64f, 0.0f);
            emissionMat1.material.SetColor("_EmissionColor", new Color(1.0f, 0.64f, 0.0f) * intensity);
        }

        if (charge3Time >= (rechargeTime * .5f) && charge3Time <= (rechargeTime * .75f))
        {
            //sprite3.color = new Color(1.0f, 0.64f, 0.0f);
            emissionMat1.material.SetColor("_EmissionColor", new Color(1.0f, 0.64f, 0.0f) * intensity);
        }

        if (charge1Time >= (rechargeTime * .75f) && charge1Time < (rechargeTime))
        {
            //sprite1.color = new Color(.64f, 1f, 0.0f);
            emissionMat1.material.SetColor("_EmissionColor", new Color(.64f, 1f, 0.0f) * intensity);
            //emissionMat1.color = new Color(.64f, 1f, 0.0f);
        }

        if (charge2Time >= (rechargeTime * .75f) && charge2Time < (rechargeTime))
        {
            //sprite2.color = new Color(.64f, 1f, 0.0f);
            emissionMat2.material.SetColor("_EmissionColor", new Color(.64f, 1f, 0.0f) * intensity);
        }

        if (charge3Time >= (rechargeTime * .75f) && charge3Time < (rechargeTime))
        {
            //sprite3.color = new Color(.64f, 1f, 0.0f);
            emissionMat3.material.SetColor("_EmissionColor", new Color(.64f, 1f, 0.0f) * intensity);
        }
        
    }

    public void UseCharge()
    {
        if (currentCharges == 0)
        {
            Debug.Log("empty");
            //NoChargeTelegraph();
            
        }
        else
        {
            ToggleCharges();
            currentCharges--;

        }
    }
    /*
    void NoChargeTelegraph()
    {
        
        {
            noChargeParticlesParent.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            noChargeParticlesParent.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            noChargeParticlesParent.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        }
    }
    */
    void ToggleCharges()
    {
        if (charge1 == true)
        {
            charge1 = false;
            //useChargeParticlesParent.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            //sprite1.color = Color.red;
            emissionMat1.material.SetColor("_EmissionColor", Color.red * intensity);
            return;
        }

        if (charge2 == true)
        {
            charge2 = false;
            //useChargeParticlesParent.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            //sprite2.color = Color.red;
            emissionMat2.material.SetColor("_EmissionColor", Color.red * intensity);
            return;
        }

        if (charge3 == true)
        {
            charge3 = false;
            //useChargeParticlesParent.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
            //sprite3.color = Color.red;
            emissionMat3.material.SetColor("_EmissionColor", Color.red * intensity);
            return;
        }
    }
    
}