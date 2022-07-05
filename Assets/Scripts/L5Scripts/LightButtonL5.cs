using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightButtonL5 : MonoBehaviour
{
    public GameObject move;
    public Material buttonMat;
    public Material defaultMat;
    private void OnTriggerStay(Collider other)
    {
        move.GetComponent<PlayerL5Script>().isLightOff();
        GetComponent<MeshRenderer>().material = buttonMat;
        Invoke("ChangeMat", 3f);
    }
    void ChangeMat()
    {
        GetComponent<MeshRenderer>().material = defaultMat;
    }
}
