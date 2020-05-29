using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashCode : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string splashSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene ()
    {
        yield return new WaitForSeconds(1.5f);
        FMODUnity.RuntimeManager.PlayOneShot(splashSound);
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("0_MainMenu", LoadSceneMode.Single);
    }
}
