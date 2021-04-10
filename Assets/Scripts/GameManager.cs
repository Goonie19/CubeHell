using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Utils;

public class GameManager : Singleton<GameManager>
{
    public float CameraSize;

    public float EatingTime;

    public float desiredSize;

    public float bulletSpeed;

    public Slider AvoidSlider;
    public Image BarFill;


    [SerializeField]private State _state;

    private float _eatingTimer;

    public State State
    {
        get { return _state; }
    }

    private void Awake()
    {
        _state = State.Avoiding;
    }

    private void Start()
    {
        Camera.main.orthographicSize = CameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AvoidEffect()
    {
        Sequence camSeq = DOTween.Sequence();
        StartCoroutine(SlowmoEffect());

        camSeq.Append(Camera.main.DOOrthoSize(CameraSize * 0.95f, 0.2f).SetEase(Ease.InQuad));

        //camSeq.Join(Camera.main.DOShakePosition(0.4f, 1).SetEase(Ease.OutQuad));

        camSeq.Append(Camera.main.DOOrthoSize(CameraSize, 0.2f).SetEase(Ease.InQuad));

        camSeq.Play();
    }

    IEnumerator SlowmoEffect()
    {
        Time.timeScale = 0.75f;

        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 1;
    }

    public void HitEffect()
    {
        Sequence camSeq = DOTween.Sequence();

        camSeq.Append(Camera.main.DOOrthoSize(CameraSize * 1.05f, 0.2f).SetEase(Ease.InQuad));

        camSeq.Join(Camera.main.DOShakePosition(0.4f, .5f).SetEase(Ease.OutQuad));

        camSeq.Append(Camera.main.DOOrthoSize(CameraSize, 0.2f).SetEase(Ease.InQuad));
    }

    #region BAR METHODS
    public void AddToAvoidBar(float add)
    {
        if(AvoidSlider.value >= AvoidSlider.minValue && AvoidSlider.value <= AvoidSlider.maxValue)
            AvoidSlider.value += add;
        if (AvoidSlider.value >= AvoidSlider.maxValue)
        {
            _state = State.Eating;
            StartCoroutine(EatingTimerCount());
        }
    }

    IEnumerator EatingTimerCount()
    {

        _eatingTimer = EatingTime;
        AvoidSlider.minValue = 0;
        AvoidSlider.maxValue = _eatingTimer;
        BarFill.color = Color.green;

        while (_eatingTimer >= 0)
        {
            _eatingTimer -= Time.deltaTime;
            AvoidSlider.value = _eatingTimer;
            yield return null;
        }
        if (GameManager.Instance.State != State.End)
        {
            _state = State.Avoiding;
            AvoidSlider.minValue = 0;
            AvoidSlider.maxValue = 1;
            AvoidSlider.value = 0;
            BarFill.color = Color.red;
        }
    }

    #endregion

    public void ChangeState(State s)
    {
        _state = s;
    }
}
