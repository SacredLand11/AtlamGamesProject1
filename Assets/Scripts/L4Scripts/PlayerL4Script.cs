using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerL4Script : MonoBehaviour
{
    Material defMat;
    public GameObject IntroText;
    public GameObject IntroImage;
    public bool getCaught = false;
    public bool increaseSpeed = false;
    bool startBool = true;
    [SerializeField] GameObject Enemy;
    [SerializeField] Material BlinkMat;
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

        //Restart Update
        if (Input.GetMouseButtonDown(0) && Enemy.GetComponent<EnemyL4Script>().DistanceToPlayer() < 0.5f)
        {
            SceneManager.LoadScene(sceneName: "LEVEL4");
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
    // Chest Voice
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chest")
        {
            this.GetComponent<AudioSource>().enabled = true;
        }
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
}
