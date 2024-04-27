using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public SceneController scenes;

    public int characterGenerated;
    public int maxDate;

    public GameObject generatorUi;
    public GameObject generatorChart;
    public GameObject generatorChartEnter;
    public GameObject delimiter;
    public int delimiterCurrent;
    public List<Vector3> delimiterPositions;

    // date input
    public TMP_Text dayInput;
    public TMP_Text monthInput;
    public TMP_Text yearInput;

    public string whatToChange;
    public string monthNaming;
    public List<string> monthNamings;

    public KeyCode currentKeyCode;

    public Color32 white;
    private bool changedStart = true;

    public int numPoints = 360;
    public float radius = 5f;
    public GameObject pointPrefab;
    public GameObject pointHolder;
    public List<GameObject> dotPoint;
    public List<Vector3> dotPointPosition;
    public Transform centerPoint;

    private void Start()
    {
        GenerateCirclePoints();

        generatorUi.SetActive(false);
        generatorChart.SetActive(false);
        generatorChartEnter.SetActive(false);
        characterGenerated = PlayerPrefs.GetInt("characterGenerated");
        if (characterGenerated == 0)
        {
            characterGenerated = 1;
            PlayerPrefs.SetInt("characterGenerated", characterGenerated);
            PlayerPrefs.Save();
            generatorUi.SetActive(true);
           
            scenes.planets.year = System.DateTime.Now.Year;
            scenes.planets.month = System.DateTime.Now.Month;
            scenes.planets.day = System.DateTime.Now.Day;
            
            onChangeYear(0);
            onChangeMonth(0);
            onChangeDay(0);

            generatorChartEnter.SetActive(true);

            delimiterCurrent = 0;
            delimiter.transform.localPosition = delimiterPositions[delimiterCurrent];
        }
        changedStart = false;
    }
    private void GenerateCirclePoints()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * Mathf.PI * 2f / numPoints; // Рассчитываем угол для каждой точки
            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float z = centerPoint.position.z + Mathf.Sin(angle) * radius;

            Vector3 position = new Vector3(x, centerPoint.position.y, z); // Позиция точки на круге
            GameObject point = Instantiate(pointPrefab, position, Quaternion.identity); // Создаем точку
            point.transform.SetParent(pointHolder.transform); // Родитель точки
            dotPoint.Add(point);
        }
        pointHolder.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        foreach (var obj in dotPoint)
        {
            dotPointPosition.Add(obj.transform.position);
        }
    }
    public void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if(whatToChange == "day")
            {
                onChangeDay(1);
            }
            else if(whatToChange == "month")
            {
                onChangeMonth(1);
            }
            else
            {
                onChangeYear(1);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (whatToChange == "day")
            {
                onChangeDay(-1);
            }
            else if (whatToChange == "month")
            {
                onChangeMonth(-1);
            }
            else
            {
                onChangeYear(-1);
            }
        }

        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1))
        {
            KeyCode keyCode = GetPressedKeyCode();
            string keyString = keyCode.ToString().ToLower();
            keyString = keyString.Substring(keyString.Length - 1);
            //Debug.Log(keyString);

            char testChar = keyString[0];

            if (char.IsDigit(testChar))
            {
                if (delimiterCurrent == 0)
                {
                    string currentDay = dayInput.text;
                    currentDay = keyString + currentDay.Substring(1);
                    dayInput.text = currentDay;
                    int keyNumber;
                    if (int.TryParse(currentDay, out keyNumber))
                    {
                        scenes.planets.day = keyNumber;
                    }
                }
                else if (delimiterCurrent == 1)
                {
                    string currentDay = dayInput.text;
                    string firstPart = "";
                    char firstChar = currentDay[0];
                    if (firstChar != 0)
                    {
                        firstPart = firstChar.ToString();
                    }
                    currentDay = firstPart + keyString;
                    dayInput.text = currentDay;
                    int keyNumber;
                    if (int.TryParse(currentDay, out keyNumber))
                    {
                        scenes.planets.day = keyNumber;
                    }
                    if (!changedStart)
                    {
                        dayInput.color = white;
                    }
                    dayCheck();
                }
                else if (delimiterCurrent == 2 && (keyString == "0" || keyString == "1"))
                {
                    string currentMonth = keyString;

                    int keyNumber;
                    if (int.TryParse(currentMonth, out keyNumber))
                    {
                        scenes.planets.month = keyNumber;
                    }
                }
                else if (delimiterCurrent == 3 || (delimiterCurrent == 2 && keyString != "0" && keyString != "1"))
                {
                    if (delimiterCurrent == 2 && keyString != "0" && keyString != "1")
                    {
                        string currentMonth;
                        currentMonth = keyString;

                        int keyNumber;
                        if (int.TryParse(currentMonth, out keyNumber))
                        {
                            scenes.planets.month = keyNumber;
                        }
                        delimiterCurrent = 3;
                    }
                    else
                    {
                        string currentMonth = scenes.planets.month.ToString();
                        if (currentMonth == "0")
                        {
                            currentMonth = "";
                        }
                        currentMonth = currentMonth + keyString;

                        int keyNumber;
                        if (int.TryParse(currentMonth, out keyNumber))
                        {
                            scenes.planets.month = keyNumber;
                        }
                    }

                    if (!changedStart)
                    {
                        monthInput.color = white;
                    }

                    onChangeMonth(0);
                }
                else if (delimiterCurrent == 4)
                {
                    string currentYear = yearInput.text;
                    currentYear = keyString + currentYear.Substring(currentYear.Length - 3);
                    yearInput.text = currentYear;
                    int keyNumber;
                    if (int.TryParse(currentYear, out keyNumber))
                    {
                        scenes.planets.year = keyNumber;
                    }
                }
                else if (delimiterCurrent == 5)
                {
                    string currentYear = yearInput.text;
                    string firstPart = "";
                    char firstChar = currentYear[0];
                    if (firstChar != 0)
                    {
                        firstPart = firstChar.ToString();
                    }
                    currentYear = firstPart + keyString + currentYear.Substring(currentYear.Length - 2);
                    yearInput.text = currentYear;
                    int keyNumber;
                    if (int.TryParse(currentYear, out keyNumber))
                    {
                        scenes.planets.year = keyNumber;
                    }
                }
                else if (delimiterCurrent == 6)
                {
                    string currentYear = yearInput.text;
                    string firstPart = "";
                    string secondPart = "";
                    char firstChar = currentYear[0];
                    char secondChar = currentYear[1];
                    if (firstChar != 0)
                    {
                        firstPart = firstChar.ToString();
                    }
                    if (secondChar != 0)
                    {
                        secondPart = secondChar.ToString();
                    }
                    currentYear = firstPart + secondPart + keyString + currentYear.Substring(currentYear.Length - 1);
                    yearInput.text = currentYear;
                    int keyNumber;
                    if (int.TryParse(currentYear, out keyNumber))
                    {
                        scenes.planets.year = keyNumber;
                    }
                }
                else if (delimiterCurrent == 7)
                {
                    string currentYear = yearInput.text;
                    string firstPart = "";
                    string secondPart = "";
                    string thirdPart = "";
                    char firstChar = currentYear[0];
                    char secondChar = currentYear[1];
                    char thirdChar = currentYear[2];
                    if (firstChar != 0)
                    {
                        firstPart = firstChar.ToString();
                    }
                    if (secondChar != 0)
                    {
                        secondPart = secondChar.ToString();
                    }
                    if (thirdChar != 0)
                    {
                        thirdPart = thirdChar.ToString();
                    }
                    currentYear = firstPart + secondPart + thirdPart + keyString;
                    yearInput.text = currentYear;
                    int keyNumber;
                    if (int.TryParse(currentYear, out keyNumber))
                    {
                        scenes.planets.year = keyNumber;
                    }
                    if (!changedStart)
                    {
                        yearInput.color = white;
                    }
                    onChangeYear(0);
                }

                delimiterChange();
            }

            else
            {
                if (delimiterCurrent < 2)
                {
                    delimiterSet(2);
                }
                else if (delimiterCurrent < 4)
                {
                    delimiterSet(4);
                }
                else
                {
                    delimiterSet(0);
                }
            }
        }
    }
    KeyCode GetPressedKeyCode()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode;
            }
        }
        return KeyCode.None;
    }
    public void delimiterChange()
    {

        if (delimiterCurrent < delimiterPositions.Count - 1)
        {
            delimiterCurrent += 1;
        }
        else
        {
            delimiterCurrent = 0;
        }
        delimiter.transform.localPosition = delimiterPositions[delimiterCurrent];

    }
    public void delimiterSet(int positionSet)
    {
        delimiterCurrent = positionSet;
        delimiter.transform.localPosition = delimiterPositions[delimiterCurrent];
    }

    public void onChangeDay(int changeValue)
    {
        whatToChange = "day";
        scenes.planets.day += changeValue;
        dayCheck();
        if (!changedStart && changeValue != 0)
        {
            dayInput.color = white;
        }
    }
    public void dayCheck()
    {
        if (scenes.planets.day > maxDate)
        {
            scenes.planets.day = 1;
        }
        else if (scenes.planets.day <= 0)
        {
            scenes.planets.day = maxDate;
        }
        string beforeSign = "";
        if(scenes.planets.day < 10)
        {
            beforeSign = "0";
        }
        dayInput.text = beforeSign + scenes.planets.day.ToString("F0");
        checkIfInputDone();
    }
    public void onChangeMonth(int changeValue)
    {
        whatToChange = "month";
        scenes.planets.month += changeValue;
        if (scenes.planets.month > 12)
        {
            scenes.planets.month = 1;
        }
        else if (scenes.planets.month <= 0)
        {
            scenes.planets.month = 12;
        }
        monthNaming = monthNamings[scenes.planets.month];
        switch (scenes.planets.month)
        {
            case 1:
                maxDate = 31;
                break;
            case 2:
                if ((scenes.planets.year % 4 == 0 && scenes.planets.year % 100 != 0) || scenes.planets.year % 400 == 0)
                {
                    maxDate = 29;
                }
                else
                {
                    maxDate = 28;
                }
                break;
            case 3:
                maxDate = 31;
                break;
            case 4:
                maxDate = 30;
                break;
            case 5:
                maxDate = 31;
                break;
            case 6:
                maxDate = 30;
                break;
            case 7:
                maxDate = 31;
                break;
            case 8:
                maxDate = 31;
                break;
            case 9:
                maxDate = 30;
                break;
            case 10:
                maxDate = 31;
                break;
            case 11:
                maxDate = 30;
                break;
            case 12:
                maxDate = 31;
                break;

        }
        dayCheck();
        monthInput.text = monthNaming;
        if (!changedStart && changeValue != 0)
        {
            monthInput.color = white;
        }
        checkIfInputDone();
    }
    public void onChangeYear(int changeValue)
    {
        whatToChange = "year";
        scenes.planets.year += changeValue;
        scenes.planets.year = Mathf.Clamp(scenes.planets.year, 1000, 10000);
        yearInput.text = scenes.planets.year.ToString("F0");
        if (!changedStart && changeValue != 0)
        {
            yearInput.color = white;
        }
        checkIfInputDone();
    }

    public void checkIfInputDone()
    {
        if (yearInput.color == white && monthInput.color == white && dayInput.color == white)
        {
            generateChart();
        }
        if (generatorChart.activeSelf)
        {
            scenes.planets.generateByDate();
        }
    }

    public void generateChart()
    {
        generatorChartEnter.SetActive(false);
        generatorChart.SetActive(true);
        scenes.planets.generateByDate();
    }
}
