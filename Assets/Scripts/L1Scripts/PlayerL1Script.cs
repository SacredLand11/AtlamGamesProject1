using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerL1Script : MonoBehaviour
{
    public GameObject IntroText;
    public GameObject IntroImage;
    public GameObject Enemy;
    public bool getCaught = false;
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
        if (Input.GetMouseButtonDown(0) && Enemy.GetComponent<EnemyL1Script>().DistanceToPlayer() < 0.5f)
        {
            SceneManager.LoadScene(sceneName: "LEVEL1");
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
}
