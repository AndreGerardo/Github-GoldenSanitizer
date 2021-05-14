using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ServerModels;
using System;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Text textEnergy;
    public Text textTimer;


    private DateTime EnergyTime; //nextEnergyTime
    private DateTime lastAddedTime;
    private int totalEnergy;
    private int maxEnergy;

    private int restoreDuration = 4;

    private bool isRestoring = false;
    private bool isStarting = true;

    private void Start()
    {       
        maxEnergy = 5;
        GetCurrentTime();
        load(); //total energy = 5;
        Debug.Log("total energy = " + totalEnergy);
        StartCoroutine(restoreRoutine());
    }

    private IEnumerator restoreRoutine()
    {
        updateEnergy();
        updateTimer();
        isRestoring = true;
        while(totalEnergy < maxEnergy)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = EnergyTime;
            bool isAdding = false;
            while(currentTime > counter)
            {
                if (totalEnergy < maxEnergy)
                {
                    isAdding = true;
                    if (!isStarting) {
                        totalEnergy++;
                    } //cek apakah baru mulai aplikasi
                    isStarting = false;

                    DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                    counter = addDuration(timeToAdd, restoreDuration);
                }
                else
                    break;
            }

            if (isAdding)
            {
                Debug.Log("is adding : "+EnergyTime);
                lastAddedTime = DateTime.Now;
                EnergyTime = counter;
            }

            updateTimer();
            updateEnergy();
            save();
            yield return null;
        }


        isRestoring = false;
    } 

    public void useEnergy()
    {
        if(totalEnergy <= 0)
        {
            return;
        }
        totalEnergy--;
        updateEnergy();

        if (!isRestoring)
        {
            if (totalEnergy + 1 == maxEnergy)
            {
                //if energy is full now
                EnergyTime = addDuration(DateTime.Now, restoreDuration);
            }
            StartCoroutine(restoreRoutine());
        }
    }

    private DateTime addDuration(DateTime time, int duration)
    {
        return time.AddSeconds(duration);
    }

    private void updateTimer()
    {
        if (totalEnergy >= maxEnergy)
        {
            textTimer.text = "Full";
            return;
        }

        TimeSpan t = EnergyTime - DateTime.Now;
        //Debug.Log("Energy time : " + EnergyTime + "Date Time now : " + DateTime.Now);
        string value = String.Format("{0}:{1}:{2}", t.Hours, t.Minutes, t.Seconds);
       // Debug.Log("t : "+t+" value : "+value);
        textTimer.text = value;
    }
    private void updateEnergy()
    {
        textEnergy.text = totalEnergy.ToString();
    }
    // Start is called before the first frame update
    void GetCurrentTime()
    {      
        PlayFabServerAPI.GetTime(new GetTimeRequest(), OnGetTimeSuccess, LogFailure);
    }
    void OnGetTimeSuccess(GetTimeResult result)
    {
        Debug.Log("The time is: " + result.Time.AddHours(7));
        saveTime(result);
    }
    void LogFailure(PlayFabError error)
    {
        Debug.Log("There was a problem getting the time. Error: " + error.GenerateErrorReport());
    }
   
    public void save()
    {
        PlayerPrefs.SetInt("saveEnergy", totalEnergy);
        PlayerPrefs.SetString("SaveTime", EnergyTime.ToString());
        PlayerPrefs.Save();
    }

    public void load()
    {
        totalEnergy = PlayerPrefs.GetInt("totalEnergy",2);
        EnergyTime = stringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastAddedTime = stringToDate(PlayerPrefs.GetString("lastAddedTime"));
    }
    void saveTime(GetTimeResult result)
    {
        EnergyTime = result.Time;//result.Time.AddHours(7);
        Debug.Log("Enegy Time : "+EnergyTime);
        PlayerPrefs.SetInt("saveEnergy",totalEnergy);
        PlayerPrefs.SetString("SaveTime",EnergyTime.ToString());
        PlayerPrefs.Save();
    }

    private DateTime stringToDate(string date)
    {
        if (String.IsNullOrEmpty(date))
        {
            Debug.Log("datetime.now : "+ DateTime.Now);
            return DateTime.Now;

        }
        return DateTime.Parse(date);
    }
}
