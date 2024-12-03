using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyName = "1";

    public bool isInTime { get; set; }

    private bool _isKeyGot;
    public bool isKeyGot { 
        get => _isKeyGot; 
        set {
            _isKeyGot = value;
            if (value)
            {
                GameState.collectedKeys.Add(keyName, isInTime);
                GameState.TriggerEvent("KeyCollected", new TriggerPayload()
                {
                    notification = $"Ключ \"{keyName}\" знайдено " +
                            (isInTime ? "вчасно" : "не вчасно"),
                    payload = isInTime
                });

                GameState.score += (isInTime ? 2 : 1) *
                    (GameState.difficulty switch
                    {
                        GameState.GameDifficulty.Easy => 1,
                        GameState.GameDifficulty.Hard => 3,
                        _ => 2,
                    });
            }
        } 
    }
}

