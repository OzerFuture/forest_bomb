using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameWinning : MonoBehaviour
{
    public List<Rigidbody> houseRB = new List<Rigidbody>();
    public static bool won;
    public GameObject resultText;
    public GameObject endingText;
    public GameObject explosionEffect;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            houseRB[i].isKinematic = true;
        }

        won = false;
    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended && won == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {

            explosionEffect.GetComponent<ParticleSystem>().Play();
            explosionEffect.GetComponent<AudioSource>().Play();

            for (int i = 0; i < 10; i++)
            {
                houseRB[i].isKinematic = false;
            }

            won = true;

            other.enabled = false;

            resultText.transform.localPosition = new Vector3(0.64f, -0.3f, 1.93f);
            resultText.GetComponent<TextMeshPro>().fontSize = 20;

            endingText.SetActive(true);
        }
    }
}
