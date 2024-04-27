
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class PlanetsPositions : MonoBehaviour
{
    public SceneController scenes;

    public int year;
    public int month;
    public int day;

    public float sunDegree;
    public float mercuryDegree;
    public float venusDegree;
    public float marsDegree;
    public float jupiterDegree;
    public float saturnDegree;
    public float uranusDegree;
    public float neptuneDegree;
    public float plutoDegree;
    public float moonDegree;


    public string sunSign;
    public string mercurySign;
    public string venusSign;
    public string marsSign;
    public string jupiterSign;
    public string saturnSign;
    public string uranusSign;
    public string neptuneSign;
    public string plutoSign;
    public string moonSign;

    public GameObject sun;

    public string[] zodiacSigns = { "Овен", "Телец", "Близнецы", "Рак", "Лев", "Дева", "Весы", "Скорпион", "Стрелец", "Козерог", "Водолей", "Рыбы" };
    public List<GameObject> planetPrefabs;

    public SolarPositionCalculator solarPositionCalculator;
    public PlanetPositionCalculator mercuryPositionCalculator;
    public DateTime targetDate;

   public void generateByDate()
    {
        targetDate = new DateTime(year, month, day);

        float sunPosition = solarPositionCalculator.CalculateSolarPosition(targetDate) - 47;

        if(sunPosition < 0) { sunPosition = 360 + sunPosition; }

        int where = 360 - (int)sunPosition + 75;
        if (where > scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (sun == null)
        {

            var obj = Instantiate(planetPrefabs[0], transform.position, Quaternion.identity, scenes.charGen.dotPoint[where].transform);
            sun = obj;
            sun.transform.localPosition = new Vector3(0, 0, sun.transform.localPosition.z);
        }
        else
        {
            sun.transform.SetParent(scenes.charGen.dotPoint[where].transform);
        }

        sun.transform.localPosition = new Vector3(0, 0, 0);
        //sun.transform.SetParent(scenes.mainCam.transform);
        sun.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));

        float howMinus = sunPosition / 30;
        sunDegree = sunPosition - (int)howMinus * 30;
        sunSign = DetectSunSign(sunPosition);

        mercuryPositionCalculator.CalculateMercuryPosition(year, month, day, this);
        mercuryPositionCalculator.CalculateMarsPosition(year, month, day, this);
        mercuryPositionCalculator.CalculateVenusPosition(year, month, day, this);
        mercuryPositionCalculator.CalculateJupiterPosition(year, month, day, this);
        mercuryPositionCalculator.CalculateSaturnPosition(year, month, day, this);
        mercuryPositionCalculator.CalculateUranusPosition(year, month, day, this);
        mercuryPositionCalculator.CalculatePlutoPosition(year, month, day, this);
        mercuryPositionCalculator.CalculateMoonPosition(year, month, day, this);
        mercuryPositionCalculator.CalculateNeptunePosition(year, month, day, this);

        
           
    }

    public string DetectSunSign(float sunPosition)
    {
        int signIndex = Mathf.FloorToInt(sunPosition / 30f) % 12;

        return zodiacSigns[signIndex];

    }
}
