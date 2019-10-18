/* 
    Base code retrieved from: https://unitycodemonkey.com/video.php?v=pbTysQw-WNs
    Modified to fit game requirements
 */

using SunnyTown;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{

    private const float REAL_SECONDS_PER_INGAME_DAY = 20f;
    private const float DAY_START_TIME = 8f;
    private const float DAY_END_TIME = 17f;
    private const float CLOCK_UPDATE_RATE_IN_MINS = 1f; // update clock every one minute
    private const float HOURS_PER_DAY = 24f;

    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private TextMeshProUGUI timeText;
    private float dayCompletion = DAY_START_TIME / HOURS_PER_DAY;
    private float randomTimeInDay;
    private float prevHours;

    private void Awake()
    {
        randomTimeInDay = GenerateRandomTimeInDay();
        clockHourHandTransform = transform.Find("hourHand");
        clockMinuteHandTransform = transform.Find("minuteHand");
        timeText = transform.Find("timeText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {

    }

    private static float GenerateRandomTimeInDay()
    {
        var result = Random.Range(DAY_START_TIME / HOURS_PER_DAY, DAY_END_TIME / HOURS_PER_DAY);
        return result;
    }

    public void ResetDay()
    {
        dayCompletion = DAY_START_TIME / HOURS_PER_DAY;
        randomTimeInDay = GenerateRandomTimeInDay();
        RenderClockUpdate();
    }

    private void RenderClockUpdate()
    {
        float rotationDegreesPerDay = 360f;

        float hours = Mathf.Floor(dayCompletion * HOURS_PER_DAY);

        float minutesPerHour = 60f;

        if (hours != prevHours)
        {
            clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayCompletion * rotationDegreesPerDay * HOURS_PER_DAY);
            clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayCompletion * rotationDegreesPerDay);

            timeText.text = hours.ToString("00") + ":00";
            prevHours = hours;
        }
    }
}