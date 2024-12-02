using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    private float gameTime;
    private Image key1Image;

    void Start()
    {
        gameTime = 0.0f;
        clock = transform
            .Find("Content/Background/ClockTMP")
            .GetComponent<TMPro.TextMeshProUGUI>();
        key1Image = transform
            .Find("Content/Background/Key1Image")
            .GetComponent<Image>();

        GameState.SubscribeTrigger(BroadcastTriggerListener);
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

    private void BroadcastTriggerListener(string type, object payload)
    {
        switch (type)
        {
            case "KeyCollected":
                key1Image.enabled = true;
                Debug.Log(string.Join(",", GameState.collectedKeys.Keys));
                break;
        }
    }

    private void OnDestroy()
    {
        GameState.UnSubscribeTrigger(BroadcastTriggerListener);
    }
}

