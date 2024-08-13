using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    private TMP_Text time;
    private float seconds;

    private void OnEnable()
    {
        InvokeRepeating(nameof(IncreaseTime), 1f, 1f);
    }
    public void IncreaseTime()
    {
        if (!GameWinning.won)
        {
            seconds++;
            GetComponent<TMP_Text>().text = seconds.ToString();
        }

    }
}
