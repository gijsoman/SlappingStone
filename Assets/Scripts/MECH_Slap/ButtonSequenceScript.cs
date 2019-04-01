using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSequenceScript : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;

    public ButtonSequence buttonSequence;

    public List<string> PressedButtons = new List<string>();
    private HorizontalLayoutGroup horizontalLayoutGroup;

    IEnumerator WaitForOnGuiToDrawOnce()
    {
        yield return new WaitForEndOfFrame();
        horizontalLayoutGroup.childControlWidth = false;
        horizontalLayoutGroup.childForceExpandWidth = false;
    }

    private void Start()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        CreateButtonsObjects();
        StartCoroutine(WaitForOnGuiToDrawOnce());

        for (int i = 0; i < buttonSequence.ButtonsInSequence.Length; i++)
        {
            buttonSequence.ButtonsInSequence[i].TimersAllowed = buttonSequence.ButtonTimersAllowed;
        }

        buttonSequence.ButtonsInSequence[0].Select();

        if (buttonSequence.SequenceTimerAllowed)
        {
            buttonSequence.SequenceTimer = GetComponent<UITimer>();
            buttonSequence.SequenceTimer.IEnded += ResetButtonSequence;
            buttonSequence.SequenceTimer.StartTimer();
        }
    }

    private void Update()
    {
        foreach (Button.ButtonType PressedButton in Enum.GetValues(typeof(Button.ButtonType)))
        {
            if (Input.GetKeyDown((KeyCode)PressedButton))
            {
                if (PressedButtons.Count < buttonSequence.ButtonsInSequence.Length)
                {
                    PressedButtons.Add(PressedButton.ToString());
                }
                CompareButtons();
            }
        }
    }

    private void CreateButtonsObjects()
    {
        for (int i = 0; i < buttonSequence.ButtonsInSequence.Length; i++)
        {
            GameObject currentButton = Instantiate(ButtonPrefab, transform);
            buttonSequence.ButtonsInSequence[i].Initialize(currentButton);
            buttonSequence.ButtonsInSequence[i].ButtonTimer.IEnded += ResetButtonSequence;
            buttonSequence.SequenceTimer.IEnded += ResetButtonSequence;
        }
    }

    private void CompareButtons()
    {
            if (PressedButtons[PressedButtons.Count-1] == buttonSequence.ButtonsInSequence[PressedButtons.Count-1].ButtonText)
            {
                if (PressedButtons.Count == buttonSequence.ButtonsInSequence.Length)
                {
                    CompleteButtonSequence();
                    return;
                }
                buttonSequence.ButtonsInSequence[PressedButtons.Count-1].Deselect();

                if (PressedButtons.Count < buttonSequence.ButtonsInSequence.Length)
                {
                    buttonSequence.ButtonsInSequence[PressedButtons.Count].Select();
                }
            }
            else
            {
                ResetButtonSequence();
            }
    }

    private void ResetButtonSequence()
    {
        PressedButtons.Clear();
        for (int i = 0; i < buttonSequence.ButtonsInSequence.Length; i++)
        {
            buttonSequence.ButtonsInSequence[i].Reset();
            buttonSequence.Reset();
        }
        buttonSequence.ButtonsInSequence[0].Select();
        ControllerVibrations.Instance.ResetVibration();
    }

    private void CompleteButtonSequence()
    {
        if (buttonSequence.SequenceTimerAllowed)
        {
            buttonSequence.SequenceTimer.ResetTimer();
        }
        buttonSequence.Complete();
    }
}
