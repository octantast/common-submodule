using UnityEngine;
using System;
using SwissEphNet;

public class PlanetPositionCalculator : MonoBehaviour
{
    private PlanetsPositions scriptus;

    public double mercuryLongitude;

    public GameObject mercury;
    public GameObject venus;
    public GameObject mars;
    public GameObject moon;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject neptune;
    public GameObject uranus;
    public GameObject pluto;
    public void CalculateMercuryPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_MERCURY, flags, positions, ref serr);
        mercuryLongitude = positions[0] + 55;

        if (mercuryLongitude < 0) { mercuryLongitude = 360 + mercuryLongitude; }
        int where = 360 - (int)mercuryLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (mercury == null)
        {
            
            var obj = Instantiate(scriptus.planetPrefabs[2], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            mercury = obj;
            mercury.transform.localPosition = new Vector3(0, 0, mercury.transform.localPosition.z);
        }
        else
        {
            mercury.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);          
        }

        mercury.transform.localPosition = new Vector3(0, 0, 0);
        //mercury.transform.SetParent(scriptus.scenes.mainCam.transform);
        mercury.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));

        float howMinus = (int) mercuryLongitude / 30;
        script.mercuryDegree = (int)mercuryLongitude - (int)howMinus * 30;

        string mercurySign = GetZodiacSign(mercuryLongitude);

        script.mercurySign = mercurySign;
    }

    public void CalculateVenusPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_VENUS, flags, positions, ref serr);
        double venusLongitude = positions[0] + 55;
        int where = 360 - (int)venusLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (venus == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[4], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            venus = obj;
            venus.transform.localPosition = new Vector3(0, 0, venus.transform.localPosition.z);
        }
        else
        {
            venus.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        venus.transform.localPosition = new Vector3(0, 0, 0);
        //venus.transform.SetParent(scriptus.scenes.mainCam.transform);
        venus.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (venusLongitude < 0) { venusLongitude = 360 + venusLongitude; }
        float howMinus = (int)venusLongitude / 30;
        script.venusDegree = (int)venusLongitude - (int)howMinus * 30;

        string venusSign = GetZodiacSign(venusLongitude);

        script.venusSign = venusSign;
    }

    public void CalculateMarsPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_MARS, flags, positions, ref serr);
        double marsLongitude = positions[0] + 55;
        int where = 360 - (int)marsLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (mars == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[3], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            mars = obj;
            mars.transform.localPosition = new Vector3(0, 0, mars.transform.localPosition.z);
        }
        else
        {
            mars.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        mars.transform.localPosition = new Vector3(0, 0, 0);
        //mars.transform.SetParent(scriptus.scenes.mainCam.transform);
        mars.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (marsLongitude < 0) { marsLongitude = 360 + marsLongitude; }
        float howMinus = (int)marsLongitude / 30;
        script.marsDegree = (int)marsLongitude - (int)howMinus * 30;

        string marsSign = GetZodiacSign(marsLongitude);

        script.marsSign = marsSign;
    }

    public void CalculateJupiterPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_JUPITER, flags, positions, ref serr);
        double jupiterLongitude = positions[0] + 54;
        int where = 360 - (int)jupiterLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (jupiter == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[5], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            jupiter = obj;
            jupiter.transform.localPosition = new Vector3(0, 0, jupiter.transform.localPosition.z);
        }
        else
        {
            jupiter.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        jupiter.transform.localPosition = new Vector3(0, 0, 0);
        //jupiter.transform.SetParent(scriptus.scenes.mainCam.transform);
        jupiter.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (jupiterLongitude < 0) { jupiterLongitude = 360 + jupiterLongitude; }
        float howMinus = (int)jupiterLongitude / 30;
        script.jupiterDegree = (int)jupiterLongitude - (int)howMinus * 30;

        string jupiterSign = GetZodiacSign(jupiterLongitude);

        script.jupiterSign = jupiterSign;
    }

    public void CalculateSaturnPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_SATURN, flags, positions, ref serr);
        double saturnLongitude = positions[0] + 54;
        int where = 360 - (int)saturnLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (saturn == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[6], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            saturn = obj;
            saturn.transform.localPosition = new Vector3(0, 0, saturn.transform.localPosition.z);
        }
        else
        {
            saturn.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        saturn.transform.localPosition = new Vector3(0, 0, 0);
        //saturn.transform.SetParent(scriptus.scenes.mainCam.transform);
        saturn.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (saturnLongitude < 0) { saturnLongitude = 360 + saturnLongitude; }
        float howMinus = (int)saturnLongitude / 30;
        script.saturnDegree = (int)saturnLongitude - (int)howMinus * 30;

        string saturnSign = GetZodiacSign(saturnLongitude);

        script.saturnSign = saturnSign;
    }

    public void CalculateUranusPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_URANUS, flags, positions, ref serr);
        double uranusLongitude = positions[0] + 54;
        int where = 360 - (int)uranusLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (uranus == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[8], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            uranus = obj;
            uranus.transform.localPosition = new Vector3(0, 0, uranus.transform.localPosition.z);
        }
        else
        {
            uranus.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        uranus.transform.localPosition = new Vector3(0, 0, 0);
        //uranus.transform.SetParent(scriptus.scenes.mainCam.transform);
        uranus.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (uranusLongitude < 0) { uranusLongitude = 360 + uranusLongitude; }
        float howMinus = (int)uranusLongitude / 30;
        script.uranusDegree = (int)uranusLongitude - (int)howMinus * 30;

        string uranusSign = GetZodiacSign(uranusLongitude);

        script.uranusSign = uranusSign;
    }

    public void CalculatePlutoPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_PLUTO, flags, positions, ref serr);
        double plutoLongitude = positions[0] + 54;
        int where = 360 - (int)plutoLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (pluto == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[9], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            pluto = obj;
            pluto.transform.localPosition = new Vector3(0, 0, pluto.transform.localPosition.z);
        }
        else
        {
            pluto.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        pluto.transform.localPosition = new Vector3(0, 0, 0);
        //pluto.transform.SetParent(scriptus.scenes.mainCam.transform);
        pluto.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (plutoLongitude < 0) { plutoLongitude = 360 + plutoLongitude; }
        float howMinus = (int)plutoLongitude / 30;
        script.plutoDegree = (int)plutoLongitude - (int)howMinus * 30;

        string plutoSign = GetZodiacSign(plutoLongitude);

        script.plutoSign = plutoSign;
    }

    public void CalculateMoonPosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_MOON, flags, positions, ref serr);
        double moonLongitude = positions[0] + 54;
        int where = 360 - (int)moonLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (moon == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[1], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            moon = obj;
            moon.transform.localPosition = new Vector3(0, 0, moon.transform.localPosition.z);
        }
        else
        {
            moon.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        moon.transform.localPosition = new Vector3(0, 0, 0);
        //moon.transform.SetParent(scriptus.scenes.mainCam.transform);
        moon.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (moonLongitude < 0) { moonLongitude = 360 + moonLongitude; }
        float howMinus = (int)moonLongitude / 30;
        script.moonDegree = (int)moonLongitude - (int)howMinus * 30;

        string moonSign = GetZodiacSign(moonLongitude);

        script.moonSign = moonSign;
    }

    public void CalculateNeptunePosition(int year, int month, int day, PlanetsPositions script)
    {
        scriptus = script;
        SwissEph swissEph = new SwissEph();
        int flags = SwissEph.SEFLG_SIDEREAL;
        double jd = swissEph.swe_julday(year, month, day, 12, SwissEph.SE_GREG_CAL);
        double[] positions = new double[6];
        string serr = "";
        swissEph.swe_calc_ut(jd, SwissEph.SE_NEPTUNE, flags, positions, ref serr);
        double neptuneLongitude = positions[0] + 54;
        int where = 360 - (int)neptuneLongitude + 75;
        if (where > scriptus.scenes.charGen.dotPoint.Count)
        {
            where -= 360;
        }

        if (neptune == null)
        {

            var obj = Instantiate(scriptus.planetPrefabs[7], transform.position, Quaternion.identity, scriptus.scenes.charGen.dotPoint[where].transform);
            neptune = obj;
            neptune.transform.localPosition = new Vector3(0, 0, neptune.transform.localPosition.z);
        }
        else
        {
            neptune.transform.SetParent(scriptus.scenes.charGen.dotPoint[where].transform);
        }

        neptune.transform.localPosition = new Vector3(0, 0, 0);
        //neptune.transform.SetParent(scriptus.scenes.mainCam.transform);
        neptune.transform.localRotation = Quaternion.Euler(new Vector3(90, 45, 90));
        if (neptuneLongitude < 0) { neptuneLongitude = 360 + neptuneLongitude; }
        float howMinus = (int)neptuneLongitude / 30;
        script.neptuneDegree = (int)neptuneLongitude - (int)howMinus * 30;

        string neptuneSign = GetZodiacSign(neptuneLongitude);

        script.neptuneSign = neptuneSign;
    }


    string GetZodiacSign(double longitude)
    {
        int signIndex = (int)Math.Floor(longitude / 30) % 12;
        return scriptus.zodiacSigns[signIndex];
    }
}
