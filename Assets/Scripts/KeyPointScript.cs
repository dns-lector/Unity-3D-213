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
            if (value) GameState.collectedKeys.Add(keyName, isInTime); 
        } 
    }
}
/* Д.З. Розмістити на ігровому полі декілька об'єктів типу "KeyPoint"
 * Реалізувати "двері", що відкриваються відповідними ключами
 * Забезпечити взаємодію - відкриття дверей за наявності відповідного ключа.
 */
