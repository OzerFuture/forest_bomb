using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BombShooting : MonoBehaviour
{
    public GameObject bombPrefab;

    public Transform groundPlane;

    public Color startColor = Color.red;

    public Color endColor = Color.green;

    private float dis = 0.4f;

    private float _maxScale;

    private float _touchDuration;

    private float _currentScale;

    private bool isMin = false;

    private bool _isTouching = false;

    private Rigidbody rb;

    private GameObject currentBomb;

    private Vector3 hitPoint;

    private Vector3 pos;
    public float ScaleDecreaseSpeed { get; set; } = 0.1f;

    public float ColorChangeSpeed { get; set; } = 0.002f;

    public float InitialScale => 0.1f;
    private Camera mainCamera => Camera.main;
    private Vector3 mousePos => Input.mousePosition;

    private void Update()
    {

        HandleInput();


        if (GameWinning.won == true)

        {
            gameObject.SetActive(false);
        }


    }

    private void HandleInput()
    {
        if (CheckRange(mousePos.x, Screen.width) && CheckRange(mousePos.y, Screen.height))

        {
            GetHitPoint();

            if (Input.touchCount > 0)

            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _currentScale = InitialScale;
                    CheckScale();
                }

                if (touch.phase == TouchPhase.Ended)
                {

                    _isTouching = false;

                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
                    mousePosition.y = 3;

                    if (isMin)
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    else  
                        MoveBomb();
                }

                if (_isTouching)
                {
                    _touchDuration += Time.deltaTime;

                    IncreaseBomb();

                    if (currentBomb.transform.localScale.x == _maxScale)
                        touch.phase = TouchPhase.Ended;

                    if (_maxScale < InitialScale)
                        isMin = true;
                }

            }

        }
    }

    private bool CheckRange(float variable, int bound)
    {
        return Mathf.Clamp(variable, 0, bound) == variable;
    }

    private void GetHitPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        Plane ground = new(groundPlane.up, groundPlane.position);

        if (ground.Raycast(ray, out float rayDistance))
        {
            hitPoint = ray.GetPoint(rayDistance);
        }
    }

    private void CheckScale()
    {
        if (transform.localScale.x > InitialScale)
        {
            _isTouching = true;

            _touchDuration = 0.0f;

            CreateBomb();

            _maxScale = transform.localScale.x - InitialScale;
        }

        else if (transform.localScale.x == InitialScale)

        {
            isMin = true;
        }
    }

    private void CreateBomb()
    {
        pos = (hitPoint - transform.position).normalized;

        Vector3 direction = transform.position + (pos * dis);

        currentBomb = Instantiate(bombPrefab, direction, Quaternion.identity);

        currentBomb.transform.localScale = Vector3.one * InitialScale;

    }

    private void IncreaseBomb()
    {
        float newScale = Mathf.Clamp(_currentScale + _touchDuration * ScaleDecreaseSpeed, InitialScale, _maxScale);
        currentBomb.transform.localScale = Vector3.one * newScale;
        _currentScale = newScale;
    }

    private void MoveBomb()
    {

        rb = currentBomb.GetComponent<Rigidbody>();
        rb.velocity = hitPoint - currentBomb.transform.position;
        rb.AddTorque(new Vector3(pos.z, 0, -1 * pos.x), ForceMode.Force);
    }

    private void ChangeColor()
    {
        Color newColor = Color.Lerp(startColor, endColor, _touchDuration * ColorChangeSpeed);
        currentBomb.GetComponent<Renderer>().material.color = newColor;
    }

}