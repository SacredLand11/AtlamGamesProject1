using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVMechanism : MonoBehaviour
{
    public bool alarm = false;
    AudioSource Warning;
    public GameObject alarmLabel;
    private void Start()
    {
        Warning = GetComponent<AudioSource>();
        alarm = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            alarm = true;
            Warning.enabled = true;
            alarmLabel.GetComponent<SpriteRenderer>().enabled = true;
            alarmLabel.GetComponent<Animator>().SetTrigger("AlarmTrig");
        }
    }
}
