using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    private float gameTime;

    void Start()
    {
        gameTime = 0.0f;
        clock = transform
            .Find("Content/Background/ClockTMP")
            .GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        int h = (int)gameTime / 3600;
        int m = ((int)gameTime % 3600) / 60;
        int s = (int)gameTime % 60;
        clock.text = $"{h:D2}:{m:D2}:{s:D2}";
    }
}
/* Д.З. Переробити систему збирання "ключів" (від дверей)
 * на ігрові повідомлення. Реалізувати відображення Toast-повідомлень
 * про одержання ключа (із зазначенням вчасності)
 */
