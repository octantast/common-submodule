using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPositionCalculator : MonoBehaviour
{
    public const float AU = 149597870.7f;

    public float CalculateSolarPosition(DateTime date)
    {
        float meanAnomaly = CalculateMeanAnomaly(date);
        float eccentricity = 0.0167f;
        float sunLongitude = CalculateSolarLongitude(meanAnomaly, eccentricity);
        float zodiacalLongitude = sunLongitude % 360f;

        return zodiacalLongitude;
    }

    private float CalculateMeanAnomaly(DateTime date)
    {        
        float daysSincePerihelion = (float)(date - new DateTime(date.Year, 1, 4)).TotalDays;
        float meanAnomaly = 360f / 365.25f * daysSincePerihelion;

        return meanAnomaly;
    }

    private float CalculateSolarLongitude(float meanAnomaly, float eccentricity)
    {
        float solarLongitude = meanAnomaly + (360f / Mathf.PI) * eccentricity * Mathf.Sin(Mathf.Deg2Rad * meanAnomaly);

        return solarLongitude;
    }
}

