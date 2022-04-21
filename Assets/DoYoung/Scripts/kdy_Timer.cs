using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class kdy_Timer : MonoBehaviour
{
    public static kdy_Timer instance;
    private void Awake()
    {

        kdy_Timer.instance = this;

    }
    public int hours;
    public int minutes;
    public int seconds;
    public bool isHours = false;
    public bool isMinutes = true;
    public bool isSeconds = true;
    float remainTime;
    public TextMeshProUGUI timeText;
    Slider timeDial;

    public float TIME
    {
        get { return remainTime; }
        set
        {
            remainTime = value;

            if (remainTime <= 0)
            {
                timeText.text = "Time Over";
                timeDial.value = 0;
            }
            else
            {
                timeDial.value = remainTime;
                timeText.text = DisplayFormattedTime(remainTime);
            }
        }
    }
    void Start()
    {
        remainTime = ConvertToTotalSeconds(hours, minutes, seconds);
        timeDial = GetComponent<Slider>();
        timeDial.maxValue = remainTime;
        TIME = remainTime;
    }

    void Update()
    {
        TIME -= Time.deltaTime;
    }

    public float ConvertToTotalSeconds(int hours, int minutes, int seconds)
    {
        remainTime = hours * 60 * 60;
        remainTime += minutes * 60;
        remainTime += seconds;
        DisplayFormattedTime(remainTime);
        return remainTime;
    }

    public string DisplayFormattedTime(float remainTime)
    {
        string convertedNumber;
        float hours, minutes, seconds;

        hours = Mathf.FloorToInt(remainTime / 60 / 60);
        minutes = Mathf.FloorToInt(remainTime / 60 - hours * 60);
        seconds = Mathf.FloorToInt(remainTime - minutes * 60 - hours * 60 * 60);

        convertedNumber = HoursFormat(hours) + MinutesFormat(minutes) + SecondsFormat(seconds);
        return convertedNumber;
    }
    // string TimeFormat(float time, bool isTimeUse)
    // {
    //     if (isTimeUse)
    //     {
    //         string timeFormatted;
    //         timeFormatted = string.Format("{0:00}", time);
    //         return timeFormatted;
    //     }
    //     else
    //     {
    //         return null;
    //     }
    // }

    string HoursFormat(float hours)
    {
        if (isHours)
        {
            string hoursFormatted;
            hoursFormatted = string.Format("{0:00}", hours) + ":";
            return hoursFormatted;
        }
        else
        {
            return null;
        }
    }
    string MinutesFormat(float minutes)
    {
        if (isMinutes)
        {
            string minutesFormatted;
            minutesFormatted = string.Format("{0:00}", minutes) + ":";
            return minutesFormatted;
        }
        else
        {
            return null;
        }
    }
    string SecondsFormat(float seconds)
    {
        if (isSeconds)
        {
            string secondsFormatted;
            secondsFormatted = string.Format("{0:00}", seconds);
            return secondsFormatted;
        }
        else
        {
            return null;
        }
    }
}