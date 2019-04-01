using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ControllerVibrations : MonoBehaviour
{
    #region !!Singleton!!
    public static ControllerVibrations Instance { get; private set; }
    [System.NonSerialized] public int Value;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private float amountOfLeftVibration;
    private float amountOfRightVibration;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Update is called once per frame
    void Update()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    private void FixedUpdate()
    {
        GamePad.SetVibration(playerIndex, amountOfLeftVibration, amountOfRightVibration);
    }


    IEnumerator ResetVibrationCoroutine()
    {
        amountOfRightVibration = 0.5f;
        yield return new WaitForSeconds(0.2f);
        amountOfRightVibration = 0f;
    }

    public void ResetVibration()
    {
        StartCoroutine(ResetVibrationCoroutine());
    }
}
