using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartGameOnTap : MonoBehaviour, IPointerClickHandler
{
    public GameObject gameManager;

    private bool gameStarted = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!gameStarted)
        {

            gameManager.SetActive(true);
            gameStarted = true;

            gameObject.SetActive(false);
        }
    }
}
