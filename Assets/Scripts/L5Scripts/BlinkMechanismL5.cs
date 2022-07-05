using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkMechanismL5 : MonoBehaviour
{
    public GameObject move;
    public Material buttonMat;
    public Material defaultMat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            move.GetComponent<PlayerL5Script>().isPressed();
            GetComponent<MeshRenderer>().material = buttonMat;
            Invoke("ChangeMat", 3f);
        }
    }
    void ChangeMat()
    {
        GetComponent<MeshRenderer>().material = defaultMat;
    }
}
