/**** TIme manager monB, created from World manager *****/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    public static TimeManager _instance;                                        // singleton
    public TimeManager()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("DUPLICATED SINGLETON - TIME MANAGER");
            Debug.Break();
        }
    }

    private sbyte timeScale = 1;                                                // global time multiplier for evenrything
    public sbyte getTimeScale
    {
        get
        {
            return timeScale;
        }
    }

    public string dateTime                                                      // returns the current (in-game) date & time formatted
    {
        get
        {
            string sHour, sMin;
            sHour = String.Format("{0:00}", hours);
            sMin = String.Format("{0:00}", mins);
            return "year " + year + 
                " month " + month +
                " day " + day +
                "\n" + sHour + ":" + sMin;
        }
    }

    private int year = 1, month = 1, day = 1, hours, mins;

    public void Tick()                                                          // ticks minutes & rolls over to hours, days, months etc...
    {
        if (mins < 59) { mins++; }
            else { mins = 0; if (hours < 23) { hours++;         }
                else { hours = 0; if (day < 30) { day++;        }
                    else { day = 1; if (month < 12) { month++;  }
                        else { month = 1; year++;               }
                }
            }
        }        
    }

    public void AdjustTime(sbyte newTimeScale, bool directSet)                  // time scale adjustment, called from input handler
    {
        if (!directSet)                                                         // if input from number keys
        {
            timeScale += newTimeScale;
            if(timeScale == 3) { timeScale = 5; }
            else if(timeScale == 4) { timeScale = 2; }

            if(timeScale <= 0) { timeScale = 1; }
            else if (timeScale >= 6) { timeScale = 5; }
        }
        else
        {
            timeScale = newTimeScale;                                           // if input from up/down keys
        }
    }
}