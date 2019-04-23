using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private Transform[] _spawnPoints;
    [SerializeField]
    private Bomb[] _bombs;
    [SerializeField]
    GameObject[] _background;
    [SerializeField]
    private Text _theScoreText;
    [SerializeField]
    private Text _windowScoreText;
    [SerializeField]
    private Text _windowBestScoreText;
    [SerializeField]
    private GameObject _playButtonObject;
    [SerializeField]
    private GameObject _infoWindow;
    [SerializeField]
    private GameObject _scoreWindow;
    [SerializeField]
    private GameObject _comingSoonWindow;
    [SerializeField]
    private GameObject _quitBtn;
    [SerializeField]
    private GameObject _playAgainBtn;
    [SerializeField]
    private GameObject _soundBtn;
    [SerializeField]
    private Sprite _muteSprite;
    [SerializeField]
    private Sprite _notMuteSprite;
    [SerializeField]
    private Camera _secondCamera;
    [SerializeField]
    private Color _dayColor;
    [SerializeField]
    private Color _sunsetColor;
    [SerializeField]
    private Color _nightColor;
    [SerializeField]
    private Color _defultColor;




    private bool _gameStarted = false;
    private bool _gameover = false;
    private float _spawnDelay = 0.3f;
    private int _theScore = 0;
    private int _bestScore;
    private int _backgroundNumber;
    //private int _counter;
    //private int _maxCounter = 2;
    private AudioSource _audioSource;



    public int _TheScore
    {
        get
        {
            return _theScore;
        }
        set
        {
            _theScore = value;
        }
    }

    public int _BestScore
    {
        get
        {
            return _bestScore;
        }
        set
        {
            _bestScore = value;
        }
    }

    /*public int _Counter
    {
        get
        {
            return _counter;
        }
        set
        {
            _counter = value;
        }
    }

    public int _MaxCounter
    {
        get
        {
            return _maxCounter;
        }
    }*/

    public bool _Gameover
    {
        get
        {
            return _gameover;
        }
        set
        {
            _gameover = value;
        }
    }

    public bool _GameStarted
    {
        get
        {
            return _gameStarted;
        }
        set
        {
            _gameStarted = value;
        }
    }

    public Text _WindowScoreText
    {
        get
        {
            return _windowScoreText;
        }
        set
        {
            _windowScoreText = value;
        }
    }

    public Text _WindowBestScoreText
    {
        get
        {
            return _windowBestScoreText;
        }
        set
        {
            _windowBestScoreText = value;
        }
    }

    public AudioSource _AudioSource
    {
        get
        {
            return _audioSource;
        }
    }

    public GameObject _ScoreWindow
    {
        get
        {
            return _scoreWindow;
        }
    }

    public GameObject _QuitBtn
    {
        get
        {
            return _quitBtn;
        }
    }

    public GameObject _PlayAgainBtn
    {
        get
        {
            return _playAgainBtn;
        }
    }


    void Awake()
    {
        Assert.IsNotNull(_spawnPoints);
        Assert.IsNotNull(_bombs);
        Assert.IsNotNull(_theScoreText);
        Assert.IsNotNull(_playButtonObject);
        Assert.IsNotNull(_infoWindow);
        Assert.IsNotNull(_muteSprite);
        Assert.IsNotNull(_notMuteSprite);
        Assert.IsNotNull(_background);
        Assert.IsNotNull(_secondCamera);
        _backgroundNumber = Random.Range(0, 3);
        _background[_backgroundNumber].SetActive(true);
        ChoseCameraBackgroundColor(_backgroundNumber);
    }


    // Use this for initialization
    void Start()
    {
        AdManager.Instance.showBannerAd();
        _windowBestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        _audioSource = GetComponent<AudioSource>();
        Image audioBtnImage = _soundBtn.GetComponent<Image>();
        if (AudioListener.pause == true)
        {
            audioBtnImage.sprite = _muteSprite;
        }
        else if (AudioListener.pause == false)
        {
            audioBtnImage.sprite = _notMuteSprite;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _spawnDelay = Random.Range(0.6f, 1.1f);
        _theScoreText.text = _theScore.ToString();
    }

    public IEnumerator SpawnBombs()
    {
        Bomb newBomb = Instantiate(_bombs[Random.Range(0, _bombs.Length)]);
        newBomb.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
        yield return new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnBombs());
    }

    public void PlayButtonTask()
    {
        _GameStarted = true;
        _audioSource.PlayOneShot(AudioManager.Instance._ClickBtnSFX);
        StartCoroutine(GameManager.Instance.SpawnBombs());
        _playButtonObject.SetActive(false);
        GameManager.Instance._QuitBtn.SetActive(false);
        GameManager.Instance._PlayAgainBtn.SetActive(false);
    }

    public void MuteButton()
    {
        AudioListener.pause = !AudioListener.pause;
        Image audioBtnImage = _soundBtn.GetComponent<Image>();
        if (AudioListener.pause == true)
        {
            audioBtnImage.sprite = _muteSprite;
        }
        else if (AudioListener.pause == false)
        {
            audioBtnImage.sprite = _notMuteSprite;
        }
    }

    public void InfoButton()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._ClickBtnSFX);
        _infoWindow.SetActive(true);
        _quitBtn.SetActive(true);
        _playAgainBtn.SetActive(true);
    }

    public void CloseInfoWindow()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._CloseBtnSFX);
        _infoWindow.SetActive(false);
        _quitBtn.SetActive(false);
        _playAgainBtn.SetActive(false);
    }

    public void CloseScoreWindow()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._CloseBtnSFX);
        _scoreWindow.SetActive(false);
    }

    public void CloseComingSoonWindow()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._CloseBtnSFX);
        _comingSoonWindow.SetActive(false);
    }

    public void PlayAgainButton()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._ClickBtnSFX);
        SceneManager.LoadScene("GameScene");
    }

    public void LeaderboardButton()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._ClickBtnSFX);
        PlayGamesScript.ShowLeaderboardsUI();
    }

    public void ShareButton()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._ClickBtnSFX);
        _comingSoonWindow.SetActive(true);
    }

    public void QuitButton()
    {
        _audioSource.PlayOneShot(AudioManager.Instance._ClickBtnSFX);
        Application.Quit();
    }

    public void ChoseCameraBackgroundColor(int backgroundNumber)
    {
        switch (backgroundNumber)
        {
            case 0:
                _secondCamera.backgroundColor = _dayColor;
                break;
            case 1:
                _secondCamera.backgroundColor = _sunsetColor;
                break;
            case 2:
                _secondCamera.backgroundColor = _nightColor;
                break;
            default:
                _secondCamera.backgroundColor = _defultColor;
                break;

        }

    }
}
