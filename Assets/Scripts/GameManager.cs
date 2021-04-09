using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : Singleton<GameManager>
{

    public float EatingTime;

    public float desiredSize;

    public float bulletSpeed;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(State s)
    {
        _state = s;
    }
}
