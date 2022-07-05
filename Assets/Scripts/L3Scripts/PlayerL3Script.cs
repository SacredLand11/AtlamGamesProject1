using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerL3Script : MonoBehaviour
{
    public GameObject IntroText;
    public GameObject IntroImage;
    public bool getCaught = false;
    public bool lockStatus = false;
    bool startBool = true;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Enemy;
    private void Awake()
    {
        IntroText.SetActive(false);
    }
    public void IntroScene()
    {
        IntroImage.SetActive(false);
        IntroText.SetActive(true);
        startBool = false;
    }
    void Start()
    {
        Time.timeScale = 0;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && !startBool)
        {
            Time.timeScale = 1;
            IntroText.SetActive(false);
        }
        //Caught Update
        if (!getCaught && Input.GetMouseButton(0))
        {
            UserController(1.5f);
        }

        if (getCaught)
        {
            UserController(0);
        }

        //Restart Update
        if (Input.GetMouseButtonDown(0) && Enemy.GetComponent<EnemyL3Script>().DistanceToPlayer() < 0.5f)
        {
            SceneManager.LoadScene(sceneName: "LEVEL3");
        }

        //Door Update
        if (lockStatus)
        {
            LockDoor();
            Invoke("notLockDoor", 5f);
        }

    }
    public void UserController(float VelScale) //The definition of player movement ability with joystick
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
        GetComponent<NavMeshAgent>().speed = VelScale;
    }
    // Door Lock Mechanism
    public void isLocked()
    {
        lockStatus = true;
    }
    public void LockDoor()
    {

        if (Enemy.GetComponent<EnemyL3Script>().currentWaypointIndex != 3)
        {
            Door.GetComponent<BoxCollider>().enabled = false;
            Enemy.GetComponent<EnemyL3Script>().enabled = false;
            Door.GetComponent<Animator>().SetTrigger("CloseDoorTrig");
            Door.GetComponent<Animator>().ResetTrigger("isTriggered");
            Door.GetComponent<Animator>().ResetTrigger("isClosed");
            Door.GetComponent<Animator>().ResetTrigger("EndCloseDoorTrig");
        }
    }
    public void notLockDoor()
    {
        Door.GetComponent<BoxCollider>().enabled = true;
        Enemy.GetComponent<EnemyL3Script>().enabled = true;
        Door.GetComponent<Animator>().ResetTrigger("CloseDoorTrig");
        Door.GetComponent<Animator>().SetTrigger("EndCloseDoorTrig");
        lockStatus = false;
    }
    // Chest Voice
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chest")
        {
            this.GetComponent<AudioSource>().enabled = true;
        }
    }
}
