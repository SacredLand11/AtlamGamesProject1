using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfTheLevelL4 : MonoBehaviour
{
    public Text text;
    public GameObject player;
    public GameObject enemy1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerL4Script>().UserController(0);
            enemy1.GetComponent<EnemyL4Script>().enabled = false;
            this.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(SceneTransition());
        }
        IEnumerator SceneTransition()
        {
            text.GetComponent<Text>().enabled = true;
            text.GetComponent<Animator>().SetTrigger("LevelCompTrig");
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(sceneName: "LEVEL5");
        }
    }
}
