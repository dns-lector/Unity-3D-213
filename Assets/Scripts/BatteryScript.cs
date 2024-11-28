using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            // GameState.flashCharge = 1.0f;
            GameState.TriggerEvent("Battery", Random.Range(0.5f, 1.0f));
            Destroy(gameObject);
        }
    }
}
/* Д.З. Реалізувати випадкову величину заряду "батарейки" - 
 * при контакті з гравцем заряд поновлюється не обов'язково на 1,
 * а на довільне (випадкове) число з діапазону 0,75-1,0
 * Розмістити префаб батарейки по сцені, підібрати час розряду таким
 * чином, щоб вистачало на шлях між ними.
 */
