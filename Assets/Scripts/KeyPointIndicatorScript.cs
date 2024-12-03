using UnityEngine;
using UnityEngine.UI;

public class KeyPointIndicatorScript : MonoBehaviour
{
    [SerializeField]
    private float keyTimeout = 5.0f;

    private Image indicator;
    private GameObject content;
    private KeyPointScript parentScript;   // ~ local state
    private float activeTime;

    void Start()
    {
        content = this
            .transform
            .Find("Content")
            .gameObject;
        content.SetActive(false);

        indicator = this
            .transform
            .Find("Content/Indicator")
            .gameObject
            .GetComponent<Image>();

        parentScript = this
            .transform
            .parent
            .GetComponent<KeyPointScript>();
        parentScript.isInTime = true;
    }

    void Update()
    {
        if (content.activeInHierarchy)
        {
            activeTime += Time.deltaTime * 
                (GameState.difficulty switch { 
                    GameState.GameDifficulty.Easy => 0.5f,
                    GameState.GameDifficulty.Hard => 1.5f,
                    _ => 1.0f,
                });

            if (activeTime >= keyTimeout)
            {
                parentScript.isInTime = false;
                Destroy(this.gameObject);
            }
            else
            {
                indicator.fillAmount = (keyTimeout - activeTime) / keyTimeout;
                indicator.color = new Color(
                    1 - indicator.fillAmount,
                    indicator.fillAmount,
                    0.2f,
                    0.25f
                );
            }
        }
        if(parentScript.isKeyGot)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && !content.activeInHierarchy)
        {
            content.SetActive(true);
            activeTime = 0.0f;
        }
    }
}
