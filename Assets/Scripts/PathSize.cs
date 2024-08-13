using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSize : MonoBehaviour
{
    public GameObject bomb;
    private float _newsize;

    void Update()
    {
        if (GameWinning.won == false)
        {
            _newsize = bomb.transform.localScale.x;
            Vector3 newScale = transform.localScale;
            newScale.x = _newsize / 10;
            transform.localScale = newScale;
        }
    }
}
