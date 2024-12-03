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
                    // ToastScript.ShowToast($"���� \"{keyName}\" ����������� ");
                    GameState.TriggerEvent("Door", new TriggerPayload()
                    {
                        notification = $"���� \"{keyName}\" �����������",
                        payload = "Opened"
                    });
                    isLocked = false;
                    openSound.Play();
                }
            }
            else
            {
                // ToastScript.ShowToast($"��� �������� ���� ������� ���� \"{keyName}\"");
                GameState.TriggerEvent("Door", new TriggerPayload()
                {
                    notification = $"��� �������� ���� ������� ���� \"{keyName}\"",
                    payload = "Close"
                });
                hitSound.Play();
            }            
        }
    }
}
/* �.�. ���������� ��'��� "��������", ���� �������� ��������� ����
 * ������ "������" ��������� �� ������� ���������� ����.
 * ��������� �� ��� ����� ��'���
 * ����������� �������� �� ����� ������� ����
 * ϳ������� �� �� ��䳿 �������, �� ���������� �� ���� ���� ������ �����
 * ���������� ������� ���������������.
 */
