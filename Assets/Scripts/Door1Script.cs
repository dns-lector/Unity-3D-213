using System.Linq;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
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
        if(collision.gameObject.name == "Player")
        {
            if(GameState.collectedKeys.Keys.Contains("1"))
            {
                bool isInTime = GameState.collectedKeys["1"];
                timeout = isInTime ? inTimeTimeout : outTimeTimeout;
                openTime = timeout;
                isLocked = false;
                ToastScript.ShowToast("���� \"1\" ����������� " +
                    (isInTime ? "������" : "�� ������") );
                openSound.Play();
            }
            else
            {
                ToastScript.ShowToast("��� �������� ���� ������� ���� \"1\"");
                hitSound.Play();
            }            
        }
    }
}