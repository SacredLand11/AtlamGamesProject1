using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestMechanism : MonoBehaviour
{
    public Image image;
    public Text text;
    public Text textEndGame;
    public GameObject EndCollision;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            image.GetComponent<Image>().enabled = true;
            text.GetComponent<Text>().enabled = true;
            textEndGame.GetComponent<Text>().enabled = true;
            EndCollision.GetComponent<BoxCollider>().enabled = true;
            Destroy(gameObject);
        }
    }
}
