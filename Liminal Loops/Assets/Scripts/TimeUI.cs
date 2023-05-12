using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
        TimeManager.OnDayChanged += UpdateTime;
    }


    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
        TimeManager.OnDayChanged -= UpdateTime;
    }


    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
        dayText.text = $"Day {TimeManager.Day}";
    }
}


