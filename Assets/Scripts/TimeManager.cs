using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int minutes;
    private int hours;
    private int days;

    private float tempSecond;

    public int Minutes {get {return minutes;} set {minutes = value; OnMinuteChange(value);}}
    public int Hours {get {return hours;} set {hours = value; OnHoursChange(value);}}
    public int Days {get {return Days;} set {days = value;}}
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempSecond = Time.deltaTime;

        if (tempSecond >= 1) {
            minutes += 1;
            tempSecond = 0;
        }
    }

    private void OnMinuteChange(int value) {
        if (value >= 60) {
            Hours++;
            minutes = 0;
        }

        if (Hours >= 24) {
            Hours = 0; 
            Days++;
        }
    }

    private void OnHoursChange(int value) {
        if (value == 6) {
            
        }
        else if (value == 8) {

        }
        else if (value == 18) {

        }
        else if (value == 22) {

        }
    }
}
