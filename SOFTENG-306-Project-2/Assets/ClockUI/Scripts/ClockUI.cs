/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using SunnyTown;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour {

    private const float REAL_SECONDS_PER_INGAME_DAY = 20f;
    private const float DAY_START_TIME = 8f;
    private const float DAY_END_TIME = 17f;
    private const float CLOCK_UPDATE_RATE_IN_MINS = 5f; // update clock every one minute
    private const float HOURS_PER_DAY = 24f;



    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private TextMeshProUGUI timeText;
    private float day = DAY_START_TIME / HOURS_PER_DAY;
    private float dayNormalized;

    private void Awake() {
        clockHourHandTransform = transform.Find("hourHand");
        clockMinuteHandTransform = transform.Find("minuteHand");
        timeText = transform.Find("timeText").GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if(CardManager.Instance.CurrentGameState == CardManager.GameState.WaitingForEvents)
        {
            if (dayNormalized >= DAY_END_TIME / HOURS_PER_DAY) {
                return;
            };

            day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

            dayNormalized = day % 1f;

            float rotationDegreesPerDay = 360f;

            float hours = Mathf.Floor(dayNormalized * HOURS_PER_DAY);

            float minutesPerHour = 60f;
            float minutes = Mathf.Floor(((dayNormalized * HOURS_PER_DAY) % 1f) * minutesPerHour);

            if (minutes % CLOCK_UPDATE_RATE_IN_MINS < 0.1)
            {
                clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * HOURS_PER_DAY);
                clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);

                timeText.text = hours.ToString("00") + ":" + minutes.ToString("00");
            }

            Debug.Log("Days Normalized: " + dayNormalized);
        }
    }

}