using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStarting : MonoBehaviour
{
    public GameObject timeText;
    public GameObject startText;
    void Start()
    {
        timeText.SetActive(false);
        timeText.GetComponent<TimeCount>().enabled = false;
        startText.SetActive(true);

   }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            timeText.SetActive(true);
            timeText.GetComponent<TimeCount>().enabled = true;
            startText.SetActive(false);
        }
    }
}
