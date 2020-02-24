using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    public float deathTimer = 3;

    public GameObject player1;
    public GameObject player1Cam;
    public Text player1Text;
    Vector3 player1StartPos;
    Quaternion origPlayer1Rot;
    bool player1Death;
    float player1Timer;

    public GameObject player2;
    public GameObject player2Cam;
    public Text player2Text;
    Vector3 player2StartPos;
    Quaternion origPlayer2Rot;
    bool player2Death;
    float player2Timer;

    public GameObject player3;
    public GameObject player3Cam;
    public Text player3Text;
    Vector3 player3StartPos;
    Quaternion origPlayer3Rot;
    bool player3Death;
    float player3Timer;

    public GameObject player4;
    public GameObject player4Cam;
    public Text player4Text;
    Vector3 player4StartPos;
    Quaternion origPlayer4Rot;
    bool player4Death;
    float player4Timer;

    public Transform deathHeight;


    // Start is called before the first frame update
    void Start()
    {
        player1StartPos = new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z);
        player2StartPos = new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z);
        player3StartPos = new Vector3(player3.transform.position.x, player3.transform.position.y, player3.transform.position.z);
        player4StartPos = new Vector3(player4.transform.position.x, player4.transform.position.y, player4.transform.position.z);
        origPlayer1Rot = player1.transform.rotation;
        origPlayer2Rot = player2.transform.rotation;
        origPlayer3Rot = player3.transform.rotation;
        origPlayer4Rot = player4.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.transform.position.y <= deathHeight.position.y && !player1Death)
            StartCoroutine(FallPlayer1(null));

        if (player2.transform.position.y <= deathHeight.position.y && !player2Death)
            StartCoroutine(FallPlayer2(null));

        if (player3.transform.position.y <= deathHeight.position.y && !player3Death)
            StartCoroutine(FallPlayer3(null));

        if (player4.transform.position.y <= deathHeight.position.y && !player4Death)
            StartCoroutine(FallPlayer4(null));

        if (player1Death)
        {
            player1Timer -= Time.deltaTime;
         //   player1Text.text = "Respawning in: " + Mathf.RoundToInt(player1Timer).ToString();
        }

        if (player2Death)
        {
            player2Timer -= Time.deltaTime;
           // player2Text.text = "Respawning in: " + Mathf.RoundToInt(player2Timer).ToString();
        }

        if (player3Death)
        {
            player3Timer -= Time.deltaTime;
         //   player3Text.text = "Respawning in: " + Mathf.RoundToInt(player3Timer).ToString();
        }

        if (player4Death)
        {
            player4Timer -= Time.deltaTime;
          //  player4Text.text = "Respawning in: " + Mathf.RoundToInt(player4Timer).ToString();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1)
        {
            player1.transform.position = player1StartPos;
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player1.transform.rotation = Quaternion.Euler(0, 0, 0);
            player1.GetComponent<FlagHolder>().FallOff();
        }

        if (other.gameObject == player2)
        {
            player2.transform.position = player2StartPos;
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player2.transform.rotation = Quaternion.Euler(0, 0, 0);
            player2.GetComponent<FlagHolder>().FallOff();
        }
    } */

    public void playerDeath(int playerNum, Transform carRB)
    {
        if (playerNum == 1)
            StartCoroutine(FallPlayer1(carRB));
        if (playerNum == 2)
            StartCoroutine(FallPlayer2(carRB));
        if (playerNum == 3)
            StartCoroutine(FallPlayer3(carRB));
        if (playerNum == 4)
            StartCoroutine(FallPlayer4(carRB));
    }

    public IEnumerator FallPlayer1(Transform carRB)
    {
        player1.GetComponent<TagHolder>().dropTags();
        player1.transform.position = new Vector3(0, -2000, 00);        
        if (player1Cam.GetComponent<BuildModeCamera>().enabled == true)
            player1Cam.GetComponent<BuildModeCamera>().SwapMode();
        player1Cam.GetComponent<Orbit>().death = true;
        player1Cam.GetComponent<Orbit>().carDeath = carRB;
        player1Timer = deathTimer;
        player1Death = true;
        yield return new WaitForSeconds(deathTimer);
        player1Cam.GetComponent<Orbit>().death = false;
        player1.transform.position = player1StartPos;
        player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player1.transform.rotation = origPlayer1Rot;
        player1.GetComponent<FlagHolder>().FallOff();
        player1.GetComponent<Health>().health = player1.GetComponent<Health>().maxHealth;
        player1Death = false;
       // player1Text.text = "";
    }

    public IEnumerator FallPlayer2(Transform carRB)
    {
        player2.GetComponent<TagHolder>().dropTags();
        player2.transform.position = new Vector3(0, -2000, 00);       
        if (player2Cam.GetComponent<BuildModeCamera>().enabled == true)
            player2Cam.GetComponent<BuildModeCamera>().SwapMode();
        player2Cam.GetComponent<Orbit>().death = true;
        player2Cam.GetComponent<Orbit>().carDeath = carRB;
        player2Timer = deathTimer;
        player2Death = true;
        yield return new WaitForSeconds(deathTimer);
        player2Cam.GetComponent<Orbit>().death = false;
        player2.transform.position = player2StartPos;
        player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player2.transform.rotation = origPlayer2Rot;
        player2.GetComponent<FlagHolder>().FallOff();
        player2.GetComponent<Health>().health = player2.GetComponent<Health>().maxHealth;
        player2Death = false;
       // player2Text.text = "";
    }

    public IEnumerator FallPlayer3(Transform carRB)
    {
        player3.GetComponent<TagHolder>().dropTags();
        player3.transform.position = new Vector3(0, -2000, 00);       
        if (player3Cam.GetComponent<BuildModeCamera>().enabled == true)
            player3Cam.GetComponent<BuildModeCamera>().SwapMode();
        player3Cam.GetComponent<Orbit>().death = true;
        player3Cam.GetComponent<Orbit>().carDeath = carRB;
        player3Timer = deathTimer;
        player3Death = true;
        yield return new WaitForSeconds(deathTimer);
        player3Cam.GetComponent<Orbit>().death = false;
        player3.transform.position = player3StartPos;
        player3.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player3.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player3.transform.rotation = origPlayer3Rot;
        player3.GetComponent<FlagHolder>().FallOff();
        player3.GetComponent<Health>().health = player3.GetComponent<Health>().maxHealth;
        player3Death = false;
       // player3Text.text = "";
    }

    public IEnumerator FallPlayer4(Transform carRB)
    {
        player4.GetComponent<TagHolder>().dropTags();
        player4.transform.position = new Vector3(0, -2000, 00);
       
        if (player4Cam.GetComponent<BuildModeCamera>().enabled == true)
            player4Cam.GetComponent<BuildModeCamera>().SwapMode();
        player4Cam.GetComponent<Orbit>().death = true;
        player4Cam.GetComponent<Orbit>().carDeath = carRB;
        player4Timer = deathTimer;
        player4Death = true;
        yield return new WaitForSeconds(deathTimer);
        player4Cam.GetComponent<Orbit>().death = false;
        player4.transform.position = player4StartPos;
        player4.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player4.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player4.transform.rotation = origPlayer4Rot;
        //player4.GetComponent<FlagHolder>().FallOff();
        player4.GetComponent<Health>().health = player4.GetComponent<Health>().maxHealth;
        player4Death = false;
       // player4Text.text = "";
    }


}
