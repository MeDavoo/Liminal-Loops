using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public static int Day { get; private set; }

    public float minuteToRealTime = 1f;
    private float timer;

    public int hour;
    public int minute;
    public int day;


    public static int SunRotationtTime { get; private set; }



    void Start()
    {
        Minute = 0;
        Hour = 0;
        Day = 1;
        timer = minuteToRealTime;
    }

    //
    void Update()
    {

        hour = Hour;
        minute = Minute;
        day = Day;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            SunRotationtTime++;
            OnMinuteChanged?.Invoke();
            if (Hour >= 23 && Minute >= 60)
            {
                Hour = 0;
                Minute = 0;
                Day++;
                OnDayChanged?.Invoke();
            }
            if (Minute >= 60)
            {
                Minute = 0;
                Hour++;
                OnHourChanged?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }
}
