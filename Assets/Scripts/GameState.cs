using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool isFpv { get; set; }
    public static float flashCharge { get; set; }
    public static Dictionary<string, bool> collectedKeys { get; } = 
        new Dictionary<string, bool>();
}
