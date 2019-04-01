using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSequenceManager : MonoBehaviour
{
    public GameObject ButtonSequencePrefab;
    public ButtonSequence[] buttonSequences;

    private ButtonSequence currentSequence;

    private void Start()
    {
        CreateAllSequenceObjects();
        currentSequence = buttonSequences[0];
        buttonSequences[0].Select();
    }

    private void CreateAllSequenceObjects()
    {
        for (int i = 0; i < buttonSequences.Length; i++)
        {
            GameObject currentSequenceObject = Instantiate(ButtonSequencePrefab, transform);
            buttonSequences[i].Initialize(currentSequenceObject);
            buttonSequences[i].ICompleted += JumpToNextSequence;
            buttonSequences[i].Deselect();
        }
    }

    private void JumpToNextSequence()
    {
        //jump to the next scene
        for (int i = 0; i < buttonSequences.Length; i++)
        {
            if (buttonSequences[i] == currentSequence && i < buttonSequences.Length - 1)
            {
                buttonSequences[i].Deselect();
                buttonSequences[i + 1].Select();
                currentSequence = buttonSequences[i + 1];
                break;
            }
            else if(buttonSequences[i] == currentSequence)
            {
                //if we win. Fix delegate here.
                buttonSequences[i].Deselect();
            }
        }
    }
}

