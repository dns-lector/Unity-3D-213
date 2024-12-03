using System.Linq;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
    [SerializeField]
    private string keyName = "1";

    private bool isOpen;
    private bool isLocked;
    private float inTimeTimeout = 2.0f;
    private float outTimeTimeout = 5.0f;
    private float timeout;
    private float openTime;
    private AudioSource hitSound;
    private AudioSource openSound;

    void Start()
    {
        isLocked = true;
        isOpen = false;
        openTime = 0.0f;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hitSound = audioSources[0];
        openSound = audioSources[1];
    }

    void Update()
    {
        if(openTime > 0.0f && !isLocked && !isOpen)
        {
            openTime -= Time.deltaTime;
            this.transform.Translate(Time.deltaTime / timeout * Vector3.left);
            if (openTime <= 0.0f)
            {
                isOpen = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Player")
        {
            if(GameState.collectedKeys.Keys.Contains(keyName))
            {
                if (isLocked)
                {
                    bool isInTime = GameState.collectedKeys[keyName];
                    timeout = (isInTime ? inTimeTimeout : outTimeTimeout) *
                        (GameState.difficulty switch
                        {
                            GameState.GameDifficulty.Easy => 0.5f,
                            GameState.GameDifficulty.Hard => 1.5f,
                            _ => 1.0f,
                        });
                    openTime = timeout;
                    // ToastScript.ShowToast($"Ключ \"{keyName}\" застосовано ");
                    GameState.TriggerEvent("Door", new TriggerPayload()
                    {
                        notification = $"Ключ \"{keyName}\" застосовано",
                        payload = "Opened"
                    });
                    isLocked = false;
                    openSound.Play();
                }
            }
            else
            {
                // ToastScript.ShowToast($"Для відкриття двері потрібен ключ \"{keyName}\"");
                GameState.TriggerEvent("Door", new TriggerPayload()
                {
                    notification = $"Для відкриття двері потрібен ключ \"{keyName}\"",
                    payload = "Close"
                });
                hitSound.Play();
            }            
        }
    }
}
/* Д.З. Реалізувати об'єкт "годинник", який дозволяє наступний ключ
 * зібрати "вчасно" незалежно від реально пройденого часу.
 * Розмістити на полі такий об'єкт
 * Забезпечити передачу від нього ігрових подій
 * Підписати на ці події скрипти, що відповідають за облік часу пошуку ключа
 * Реалізувати описану функціональність.
 */
