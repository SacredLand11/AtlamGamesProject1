using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            GetComponent<Animator>().SetTrigger("isTriggered");
            GetComponent<Animator>().ResetTrigger("isClosed");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            GetComponent<Animator>().ResetTrigger("isTriggered");
            GetComponent<Animator>().SetTrigger("isClosed");
        }
    }
}
