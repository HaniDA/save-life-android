using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField]
    private AudioClip _gameMusic;
    [SerializeField]
    private AudioClip _bombExplosionSFX;
    [SerializeField]
    private AudioClip _bombDropSFX;
    [SerializeField]
    private AudioClip _birdFlapSFX;
	[SerializeField]
    private AudioClip _birdDieSFX;
    [SerializeField]
    private AudioClip _clickBtnSFX;
    [SerializeField]
    private AudioClip _closeBtnSFX;
    [SerializeField]
    private AudioClip _hitSFX;
    

    public AudioClip _GameMusic
    {
        get
        {
            return _gameMusic;
        }
    }

    public AudioClip _BombExplosionSFX
    {
        get
        {
            return _bombExplosionSFX;
        }
    }

    public AudioClip _BombDropSFX
    {
        get
        {
            return _bombDropSFX;
        }
    }

    public AudioClip _BirdFlapSFX
    {
        get
        {
            return _birdFlapSFX;
        }
    }

	public AudioClip _BirdDieSFX
    {
        get
        {
            return _birdDieSFX;
        }
    }

    public AudioClip _ClickBtnSFX
    {
        get
        {
            return _clickBtnSFX;
        }
    }

    public AudioClip _CloseBtnSFX
    {
        get
        {
            return _closeBtnSFX;
        }
    }

    public AudioClip _HitSFX
    {
        get
        {
            return _hitSFX;
        }
    }


	void Awake(){
		Assert.IsNotNull(_gameMusic);
		Assert.IsNotNull(_bombExplosionSFX);
		Assert.IsNotNull(_bombDropSFX);
		Assert.IsNotNull(_birdFlapSFX);
		Assert.IsNotNull(_birdDieSFX);
        Assert.IsNotNull(_clickBtnSFX);
        Assert.IsNotNull(_closeBtnSFX);
        Assert.IsNotNull(_hitSFX);
	}


}
