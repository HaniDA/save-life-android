using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{

    [SerializeField]
    private float objectSpeed = 1;
    [SerializeField]
    private float resetPos;
    [SerializeField]
    private float startPos;


    protected virtual void Update()
    {

        if (!GameManager.Instance._Gameover) // so if the gameOver is not true .. we move objects until Game Over
        {
            transform.Translate(Vector3.left * (objectSpeed * Time.deltaTime));

            if (transform.localPosition.x <= resetPos)
            {
                Vector3 newPos = new Vector3(startPos, transform.position.y, transform.position.z);
                transform.position = newPos;
            }
        }
    }
}
