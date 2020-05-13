using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
{
    Text timerText;
    float currentHour = 6f;
    float currentMinute = 50f;

    private void Start()
    {
        timerText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMinute += (float)1 * Time.deltaTime;
        if(((int)currentMinute) == 60 )
        {
            currentHour += 1f;
            currentMinute = 0f;
        }

        if(((int)currentHour) == 22)
        {
            currentHour = 6f;
            currentMinute = 50f;
        }

        string minute = ((int)currentMinute).ToString("00");
        string hour = ((int)currentHour).ToString("00");

        timerText.text = hour + ":" + minute;
    }
}
