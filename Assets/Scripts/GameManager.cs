using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Utils;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Properties")]
    public float CameraSize;

    public float EatingTime;

    public float desiredSize;

    public float bulletSpeed;

    [Header("UI Management")]
    public Slider AvoidSlider;
    public Image BarFill;
    public GameObject InitGamePanel;

    [Header("Music Management")]
    public AudioMixerSnapshot MenuSnapshot;
    public AudioMixerSnapshot AvoidSnapShot;
    public AudioMixerSnapshot EatingSnapShot;
    public AudioMixerSnapshot EndSnapShot;


    [SerializeField]private State _state;

    private float _eatingTimer;

    public State State
    {
        get { return _state; }
    }

    private void Awake()
    {
        _state = State.Pause;
    }

    private void Start()
    {
        Camera.main.orthographicSize = CameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region EFFECTS

    public void AvoidEffect()
    {
        Sequence camSeq = DOTween.Sequence();
        StartCoroutine(SlowmoEffect());

        camSeq.Append(Camera.main.DOOrthoSize(CameraSize * 0.85f, 0.2f).SetEase(Ease.InQuad));

        //camSeq.Join(Camera.main.DOShakePosition(0.4f, 1).SetEase(Ease.OutQuad));

        camSeq.Append(Camera.main.DOOrthoSize(CameraSize, 0.2f).SetEase(Ease.InQuad));

        camSeq.Play();
    }

    IEnumerator SlowmoEffect()
    {
        Time.timeScale = 0.65f;

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

    #endregion

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
        ChangeMusic();

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
            ChangeMusic();

        }
    }

    #endregion

    #region UI METHODS

    public void SelectButton(GameObject button)
    {

        button.transform.DOScale(1.3f, 0.2f).SetEase(Ease.InQuad).Play();
    }

    public void DeselectButton(GameObject button)
    {

        button.transform.DOScale(1f, 0.2f).SetEase(Ease.InQuad).Play();
    }

    public void OpenPanel(GameObject panel)
    {

        panel.transform.DOScale(1f, 0.2f).SetEase(Ease.InQuad).Play();
    }

    public void ClosePanel(GameObject panel)
    {
        Sequence panelSeq = DOTween.Sequence();

        panelSeq.Append(panel.transform.DOScale(0.001f, 0.2f).SetEase(Ease.InQuad).Play());

        panelSeq.onComplete = () => { panel.SetActive(false); };


    }

    #endregion


    public void StartGame()
    {
        AvoidSlider.gameObject.SetActive(true);
        AvoidSlider.transform.GetComponent<CanvasGroup>().alpha = 0;
        AvoidSnapShot.TransitionTo(2f);

        Sequence panelSeq = DOTween.Sequence();

        panelSeq.Append(InitGamePanel.transform.GetComponent<CanvasGroup>().DOFade(0, 2f).SetEase(Ease.Linear));
        panelSeq.Join(AvoidSlider.transform.GetComponent<CanvasGroup>().DOFade(1, 2f).SetEase(Ease.Linear));

        panelSeq.Play();

        

        panelSeq.onComplete = () => { ChangeState(State.Avoiding);  };


    }

    public void ChangeState(State s)
    {
        _state = s;
    }

    public void ChangeMusic()
    {
        switch(_state)
        {
            case State.Pause: MenuSnapshot.TransitionTo(2f);break;
            case State.Avoiding: AvoidSnapShot.TransitionTo(2f);break;
            case State.Eating: EatingSnapShot.TransitionTo(2f);break;
            case State.End: EndSnapShot.TransitionTo(2f);break;
        }
    }
}
