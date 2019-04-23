using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    [SerializeField]
    private float _speed;

    private Vector2 _offcet;

    void Update()
    {
        _offcet = new Vector2(Time.time * _speed, 0);
        GetComponent<SpriteRenderer>().material.mainTextureOffset = _offcet;
    }
}
