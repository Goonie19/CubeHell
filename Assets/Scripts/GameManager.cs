using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : Singleton<GameManager>
{

    private State _state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case State.Avoiding:;
                
                break;
            case State.Eating:;
                
                break;
            case State.Pause:;

                break;
            case State.End:;

                break;
        }
    }
}
