using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private List<Light> nightLights;
    private List<Light> dayLights;
    private bool isNight;

    void Start()
    {
        nightLights = new List<Light>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("NightLight"))
        {
            nightLights.Add(g.GetComponent<Light>());
        }
        dayLights = new List<Light>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("DayLight"))
        {
            dayLights.Add(g.GetComponent<Light>());
        }
        isNight = nightLights[0].isActiveAndEnabled;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            isNight = !isNight;
            foreach(Light nightLight in nightLights)
            {
                nightLight.enabled = isNight;
            }
            foreach(Light dayLight in dayLights)
            {
                dayLight.enabled = !isNight;
            }
        }
    }
}
/* Система день-ніч
 * Забезпечити перемикання усіх джерел світла в залежності
 * від вибору користувача: денний чи нічний режим гри обрано.
 * ** Також змінювати текстуру неба в залежності від обраного режиму
 */
