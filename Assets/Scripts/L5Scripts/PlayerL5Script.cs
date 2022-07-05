using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerL5Script : MonoBehaviour
{
    [SerializeField] Material BlinkMat;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject otherEnemy;
    [SerializeField] GameObject dirLight;

    Material defMat;
    public GameObject UV;
    public GameObject IntroText;
    public GameObject IntroImage;

    public bool increaseSpeed = false;
    public bool getCaught = false;
    public bool lockStatus = false;
    public bool lightStatus = false;
    bool startBool = true;
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
        defMat = GetComponent<MeshRenderer>().material;
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
            if (!increaseSpeed)
            {
                UserController(1.5f);
            }
            if (increaseSpeed)
            {
                UserController(3f);
                Invoke("notPressed", 3f);
            }
        }

        if (getCaught)
        {
            UserController(0);
        }

        //Door Update
        if (lockStatus)
        {
            LockDoor();
            Invoke("notLockDoor", 5f);
        }

        //Light Update
        if (lightStatus)
        {
            turnOffTheLights();
            Invoke("turnOnTheLights", 3f);
        }

        //Restart Update
        if (Input.GetMouseButtonDown(0))
        {
            if(Enemy.GetComponent<EnemyL5Script>().DistanceToPlayer() < 0.5f || otherEnemy.GetComponent<EnemyL5Script>().DistanceToPlayer() < 0.5f)
            SceneManager.LoadScene(sceneName: "LEVEL5");
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

    // Blink Ability
    public void isPressed()
    {
        increaseSpeed = true;
        GetComponent<MeshRenderer>().material = BlinkMat;
    }
    public void notPressed()
    {
        increaseSpeed = false;
        GetComponent<MeshRenderer>().material = defMat;
    }

    // Door Lock Mechanism
    public void isLocked()
    {
        lockStatus = true;
    }
    public void LockDoor()
    {
        Door.GetComponent<BoxCollider>().enabled = false;
        if (Enemy.GetComponent<EnemyL5Script>().currentWaypointIndex == 2 || Enemy.GetComponent<EnemyL5Script>().currentWaypointIndex == 3)
        {
            Enemy.GetComponent<EnemyL5Script>().enabled = false;
            Door.GetComponent<Animator>().SetTrigger("CloseDoorTrig");
            Door.GetComponent<Animator>().ResetTrigger("isTriggered");
            Door.GetComponent<Animator>().ResetTrigger("isClosed");
            Door.GetComponent<Animator>().ResetTrigger("EndCloseDoorTrig");
        }
    }
    public void notLockDoor()
    {
        Door.GetComponent<BoxCollider>().enabled = true;
        Enemy.GetComponent<EnemyL5Script>().enabled = true;
        Door.GetComponent<Animator>().ResetTrigger("CloseDoorTrig");
        Door.GetComponent<Animator>().SetTrigger("EndCloseDoorTrig");
        lockStatus = false;
    }

    // Light Mechanism
    public void isLightOff()
    {
        lightStatus = true;
    }
    public void turnOffTheLights()
    {
        dirLight.GetComponent<Light>().intensity = 0f;
        UV.GetComponent<BoxCollider>().enabled = false;
    }
    public void turnOnTheLights()
    {
        dirLight.GetComponent<Light>().intensity = 0.45f;
        lightStatus = false;
        UV.GetComponent<BoxCollider>().enabled = true;
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
