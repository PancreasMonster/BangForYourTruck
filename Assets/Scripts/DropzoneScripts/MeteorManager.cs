using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorManager : MonoBehaviour
{
    public GameObject meteor;
    public List<GameObject> dropZones = new List<GameObject>();
    public Text text;
    public float delayBetweenMeteorStrikes = 60;
    public float laserGap = 15;
    public float meteorSpawnHeight = 100;
    public float meteorSpawnCircleRadius = 10;

    private float timer;
    public ParticleSystem ps;
    public LineRenderer lr;
    public float lrWidth = 0, psLife = 0;
    float lrWidthMax = 50;
    private bool start, stop, startLaser, stopLaser;
    private float origWidth;
    float t;
    float f;
    float rand, rand2;
    Vector3 origTextPos;
    AudioSource aud;
   // public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        StartCoroutine(MeteorStrike());
        timer = delayBetweenMeteorStrikes;
        lr.startWidth = lrWidth;
        lr.endWidth = lrWidth;
        origWidth = lrWidth;
        origTextPos = text.rectTransform.localPosition;
        rand = Random.Range(3, 4.5f);
        rand2 = Random.Range(1, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ((int)timer).ToString();
        text.color = new Color(1, timer / delayBetweenMeteorStrikes, timer / delayBetweenMeteorStrikes, 1);
        t += Time.deltaTime * (1.0f - (timer / delayBetweenMeteorStrikes)) * 14 * rand;
        f += Time.deltaTime * (1.0f - (timer / delayBetweenMeteorStrikes)) * 14 * rand2;
        text.rectTransform.localPosition = new Vector3(origTextPos.x + ((1.0f - (timer / delayBetweenMeteorStrikes)) * rand2 * Mathf.Sin(t - rand)), origTextPos.y + ((1.0f - (timer / delayBetweenMeteorStrikes)) * rand * Mathf.Sin(f - rand2)), origTextPos.z);
        timer -= Time.deltaTime;
        SkyLaser();
    }

    void SkyLaser ()
    {
        lr.startWidth = lrWidth;
        lr.endWidth = lrWidth;

        var main = ps.main;
        main.startLifetime = psLife;
        if (startLaser && lrWidth < lrWidthMax && !stopLaser)
        {
            lrWidth += 1f * 100 * (lrWidthMax / 200) * Time.deltaTime;
        }

        if (stopLaser && lrWidth > 0)
        {
            lrWidth -= 1f * 100 * (lrWidthMax / 200) * Time.deltaTime;
        }

        if (start && psLife < .66f * 25f && !stop)
        {
            psLife += .16f * 25f * Time.deltaTime;
        }

        if (stop)
        {
            psLife = 0;
        }
    }

    IEnumerator MeteorStrike ()
    {
        yield return new WaitForSeconds (delayBetweenMeteorStrikes - laserGap);
        int rand = Random.Range(0, dropZones.Count-1);      
        Vector3 spawnHeight = new Vector3(dropZones[rand].transform.position.x, dropZones[rand].transform.position.y + meteorSpawnHeight, dropZones[rand].transform.position.z);
        Vector3 randCircle = Random.insideUnitCircle * meteorSpawnCircleRadius;
        Vector3 spawnVector = new Vector3(spawnHeight.x + randCircle.x, spawnHeight.y, spawnHeight.z + randCircle.y);       
        lrWidth = origWidth;
        lr.startWidth = lrWidth;
        lr.endWidth = lrWidth;
        lr.SetPosition(0, spawnHeight);
        lr.SetPosition(1, new Vector3(dropZones[rand].transform.position.x, dropZones[rand].transform.position.y - 5f, dropZones[rand].transform.position.z));
        StartCoroutine(StartLaser());
        yield return new WaitForSeconds(laserGap - 2f);
        aud.Play();
        yield return new WaitForSeconds(2f);
        timer = delayBetweenMeteorStrikes;
        
        GameObject clone = Instantiate(meteor, spawnVector, Quaternion.identity);
       
        clone.GetComponent<Meteor>().AssignTarget(dropZones[rand]);
        StartCoroutine(MeteorStrike());
    }

    public IEnumerator StartLaser()
    {
        //aud.Play();
        
        start = true;
        yield return new WaitForSeconds(4.5f);
        startLaser = true;
        yield return new WaitForSeconds(.2f);
        stop = true;
        stopLaser = true;
        yield return new WaitForSeconds(2);
        lrWidth = 0;        
        start = false;
        startLaser = false;
        stop = false;
        stopLaser = false;
    }
}
