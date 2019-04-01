using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Button
{
    public enum ButtonType
    {
        A = KeyCode.JoystickButton0,
        B = KeyCode.JoystickButton1,
        X = KeyCode.JoystickButton2,
        Y = KeyCode.JoystickButton3,
    }
    public ButtonType ButtonToPress;
    public float TimeToPress = 5.0f;
    public bool TimersAllowed;

    [System.NonSerialized] public UITimer ButtonTimer;
    [System.NonSerialized] public string ButtonText;

    private bool Selected;
    private GameObject ButtonObject;

    public void Initialize(GameObject _object)
    {
        ButtonObject = _object;
        ButtonTimer = ButtonObject.GetComponent<UITimer>();

        //set the button text
        ButtonText = ButtonToPress.ToString();
        ButtonObject.GetComponentInChildren<Text>().text = ButtonText;

        //set the button time;
        ButtonTimer.TargetTime = TimeToPress;
    }

    public void Select()
    {
        Selected = true;
        if (TimersAllowed)
        {
            ButtonTimer.StartTimer();
        }
    }

    public void Deselect()
    {
        Selected = false;
        if (TimersAllowed)
        {
            ButtonTimer.ResetTimer();
        }
        ButtonObject.SetActive(false);
    }

    public void Reset()
    {
        Deselect();
        ButtonObject.SetActive(true);
    }
}