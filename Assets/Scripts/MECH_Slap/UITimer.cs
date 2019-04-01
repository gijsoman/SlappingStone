using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public bool AllowedToGo = false;
    public float TargetTime = 60.0f;

    private float CurrentTime;
    private Text timerText;

    private void Start()
    {
        CreateChildText();
        CurrentTime = TargetTime;        
    }

    private void Update()
    {
        if (CurrentTime <= 0.0f && AllowedToGo)
        {
            TimerEnd();
        }
        else if (AllowedToGo)
        {
            CurrentTime -= Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = ((int)(CurrentTime * 10.0f) / 10.0f).ToString();
    }

    public delegate void OnTimerStarted();
    public OnTimerStarted IStarted;
    public void StartTimer()
    {
        AllowedToGo = true;
        if (IStarted != null)
            IStarted.Invoke();
    }

    public delegate void OnTimerStopped();
    public OnTimerStarted IStopped;
    public void StopTimer()
    {
        AllowedToGo = false;
        timerText.text = "";
        if (IStopped != null)
            IStopped.Invoke();
    }

    public delegate void OnTimerReset();
    public OnTimerStarted IReset;
    public void ResetTimer()
    {
        StopTimer();
        CurrentTime = TargetTime;
        timerText.text = "";
        if (IReset != null)
            IReset.Invoke();
    }

    public delegate void OnTimerEnded();
    public OnTimerStarted IEnded;
    public void TimerEnd()
    {
        if (IEnded != null)
            IEnded.Invoke();
    }

    private void CreateChildText()
    {
        timerText = new GameObject("TimerText", typeof(Text), typeof(LayoutElement)).GetComponent<Text>();
        timerText.gameObject.transform.SetParent(transform);
        timerText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        timerText.fontSize = 36;
        timerText.alignment = TextAnchor.MiddleCenter;
        timerText.rectTransform.anchorMin = new Vector2(0, 1);
        timerText.rectTransform.anchorMax = new Vector2(0, 1);
        timerText.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        timerText.color = Color.red;
        LayoutElement lm = timerText.gameObject.GetComponent<LayoutElement>();
        lm.ignoreLayout = true;        
    }
}
