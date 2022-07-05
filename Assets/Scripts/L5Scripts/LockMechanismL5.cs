using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockMechanismL5 : MonoBehaviour
{
    public GameObject move;
    public Material buttonMat;
    public Material defaultMat;
    private void OnTriggerStay(Collider other)
    {
        move.GetComponent<PlayerL5Script>().isLocked();
        GetComponent<MeshRenderer>().material = buttonMat;
        Invoke("ChangeMat", 6f);
    }
    private void OnTriggerExit(Collider other)
    {
        move.GetComponent<PlayerL5Script>().lockStatus = false;
    }
    void ChangeMat()
    {
        GetComponent<MeshRenderer>().material = defaultMat;
    }
}
