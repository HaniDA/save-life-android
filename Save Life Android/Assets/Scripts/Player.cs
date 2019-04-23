using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravity = 3f;
    [SerializeField]
    private int _healthPoints;

    [SerializeField]
    private List<GameObject> _heartsUI = new List<GameObject>();

    private bool _move;
    private bool _newTarget = true;
    private bool _isFacingRight;
    private bool _isDead = false;
    private Vector3 _target;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Animator _anim;



    public int _HealthPoints
    {
        get
        {
            return _healthPoints;
        }
    }

    public bool _IsDead
    {
        get
        {
            return _isDead;
        }
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.Instance._Gameover && GameManager.Instance._GameStarted)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.Instance._TheScore += 1;
                _newTarget = true;
                GameManager.Instance._AudioSource.PlayOneShot(AudioManager.Instance._BirdFlapSFX);
                _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _target.z = transform.position.z;
                _anim.Play("Pelican_Fly");
                Flip(_target.x);

                if (_move == false)
                {
                    _move = true;
                }
            }
            if (_move == true && _newTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
                if (transform.position == _target)
                {
                    _newTarget = false;
                }
            }
            if (_newTarget == false)
            {
                if (_isFacingRight)
                {
                    transform.Translate(Vector3.down * _gravity * Time.deltaTime);
                    transform.Translate(Vector3.right * 0.5f * Time.deltaTime);
                }
                else if (!_isFacingRight)
                {
                    transform.Translate(Vector3.down * _gravity * Time.deltaTime);
                    transform.Translate(Vector3.left * 0.5f * Time.deltaTime);
                }
            }
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Right_Border")
        {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, 0);
        }
        else if (other.tag == "Left_Border")
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
        }
        else if (other.tag == "Bottom_Border")
        {
            Die();
        }
        else if (other.tag == "Upper_Border")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
        }
        else if (other.tag == "Bomb")
        {
            Bomb newBomb = other.GetComponent<Bomb>();
            PlayerHit(newBomb._Damage);
        }
    }

    public void Flip(float _targetX)
    {
        Vector3 _theScale = transform.localScale;
        if (_targetX < transform.position.x)
        {
            _isFacingRight = false;
            _theScale.x = -1;
        }
        else if (_targetX > transform.position.x)
        {
            _isFacingRight = true;
            _theScale.x = 1;
        }
        transform.localScale = _theScale;
    }


    public void PlayerHit(int _hitPoints)
    {
        if (_healthPoints - _hitPoints > 0)
        {
            _healthPoints -= _hitPoints;
            GameManager.Instance._AudioSource.PlayOneShot(AudioManager.Instance._HitSFX);
            HandleHearts(_hitPoints);
        }
        else
        {
            _healthPoints -= _hitPoints;
            if (!_isDead)
                Die();
        }
    }

    public void Die()
    {
        print("Bird Thug!");
        ClearHearts();
        transform.position = transform.position;
        _rigidbody2D.isKinematic = false;
        _collider2D.isTrigger = false;
        GameManager.Instance._AudioSource.PlayOneShot(AudioManager.Instance._BirdDieSFX);
        _anim.SetTrigger("isDead");
        GameManager.Instance._Gameover = true;
        _isDead = true;
        GameManager.Instance._WindowScoreText.text = GameManager.Instance._TheScore.ToString();
        GameManager.Instance._QuitBtn.SetActive(true);
        GameManager.Instance._PlayAgainBtn.SetActive(true);
        if (GameManager.Instance._TheScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", GameManager.Instance._TheScore);
            GameManager.Instance._WindowBestScoreText.text = GameManager.Instance._TheScore.ToString();
            PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_save_life_scores, GameManager.Instance._TheScore);
        }
        GameManager.Instance._ScoreWindow.SetActive(true);
        //StartCoroutine(WaitBeforShowAd());
    }

    public void ClearHearts()
    {
        foreach (GameObject heart in _heartsUI)
        {
            heart.SetActive(false);
        }
    }

    public void HandleHearts(int damage)
    {
        for (int i = damage; i >= 1; i--)
        {
            _heartsUI[_heartsUI.Count - 1].SetActive(false);
            _heartsUI.RemoveAt(_heartsUI.Count - 1);
        }
    }

    public IEnumerator WaitBeforShowAd()
    {
        yield return new WaitForSeconds(1.5f);
        AdManager.Instance.showInterstitialAd();
    }

}

