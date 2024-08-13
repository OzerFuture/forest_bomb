using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSizeWithTouch : MonoBehaviour
{

    [SerializeField, Range (0, 1)] private float _initialScale;
    private bool isTouching = false;
    private float _touchDuration;
    private float _currentScale;
    public float _scaleDecreaseSpeed { get; set; } = 0.1f;


    private void Start()
    {
        transform.localScale = Vector3.one * _initialScale;
        _currentScale = _initialScale;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
                _touchDuration = 0.0f;

            }


            if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;

            }


            if (isTouching)
            {
                _touchDuration += Time.deltaTime;

                ScaleChange();
            }
        }
    }

    private void ScaleChange()
    {
        float minScale = GetComponent<BombShooting>().InitialScale;
        float newScale = Mathf.Clamp(_currentScale - _touchDuration * _scaleDecreaseSpeed, minScale, _initialScale);
        transform.localScale = Vector3.one * newScale;
        _currentScale = newScale;
    }

}
