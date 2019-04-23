using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    private string _bombName;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _bombScore;

    private Animator anim;
    private Collider2D _collider2D;


    public int _Damage
    {
        get
        {
            return _damage;
        }
    }



    void Start()
    {
        anim = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DestroyBomb();
        }
        else if (other.tag == "End_Point")
        {
            Destroy(gameObject);
        }
    }

    public void DestroyBomb()
    {
        print("Bomb Destroyed !");
        anim.SetTrigger("isExploded");
        if (!FindObjectOfType<Player>()._IsDead)
            Destroy(gameObject, 0.3f);
    }
}
