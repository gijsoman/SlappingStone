using UnityEngine;

[System.Serializable]
public class ButtonSequence
{
    public bool ButtonTimersAllowed = true;
    public bool SequenceTimerAllowed = false;
    public float SequenceTime;
    public Button[] ButtonsInSequence;

    [System.NonSerialized] public UITimer SequenceTimer;

    private bool selected;
    private GameObject sequenceObject;

    public void Initialize(GameObject _object)
    {
        sequenceObject = _object;
        SequenceTimer = sequenceObject.GetComponent<UITimer>();

        sequenceObject.GetComponent<ButtonSequenceScript>().buttonSequence = this;

        SequenceTimer.TargetTime = SequenceTime;
    }

    public void Select()
    {
        sequenceObject.SetActive(true);
    }

    public void Deselect()
    {
        sequenceObject.SetActive(false);
    }

    public void Reset()
    {
        if (SequenceTimerAllowed)
        {
            SequenceTimer.ResetTimer();
            SequenceTimer.StartTimer();
        }
    }

    public delegate void OnCompleted();
    public OnCompleted ICompleted;
    public void Complete()
    {
        ICompleted.Invoke();
    }
}


