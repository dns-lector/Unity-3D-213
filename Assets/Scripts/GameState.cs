using System;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class GameState
{
    public static bool isFpv { get; set; }
    public static Dictionary<string, bool> collectedKeys { get; } = 
        new Dictionary<string, bool>();

    #region sensitivityLook
    private static float _sensitivityLookX = 0.5f;
    public static float sensitivityLookX
    {
        get => _sensitivityLookX;
        set
        {
            if (_sensitivityLookX != value)
            {
                _sensitivityLookX = value;
                NotifySubscribers(nameof(sensitivityLookX));
            }
        }
    }

    private static float _sensitivityLookY = 0.5f;
    public static float sensitivityLookY
    {
        get => _sensitivityLookY;
        set
        {
            if (_sensitivityLookY != value)
            {
                _sensitivityLookY = value;
                NotifySubscribers(nameof(sensitivityLookY));
            }
        }
    }
    #endregion

    #region effectsVolume
    private static float _effectsVolume = 1.0f;
    public static float effectsVolume
    {
        get => _effectsVolume;
        set
        {
            if (_effectsVolume != value)
            {
                _effectsVolume = value;
                NotifySubscribers(nameof(effectsVolume));
            }
        }
    }
    #endregion

    #region ambientVolume
    private static float _ambientVolume = 1.0f;
    public static float ambientVolume
    {
        get => _ambientVolume;
        set
        {
            if (_ambientVolume != value)
            {
                _ambientVolume = value;
                NotifySubscribers(nameof(ambientVolume));
            }
        }
    }
    #endregion

    #region isMuted ( Mute All )
    private static bool _isMuted = false;
    public static bool isMuted
    {
        get => _isMuted;
        set
        {
            if (_isMuted != value)
            {
                _isMuted = value;
                NotifySubscribers(nameof(isMuted));
            }
        }
    }
    #endregion

    #region difficulty
    private static GameDifficulty _difficulty = GameDifficulty.Middle;
    public static GameDifficulty difficulty
    {
        get => _difficulty;
        set
        {
            if (_difficulty != value)
            {
                _difficulty = value;
                NotifySubscribers(nameof(difficulty));
            }
        }
    }
    public enum GameDifficulty
    {
        Easy,
        Middle,
        Hard
    }
    #endregion

    #region score
    private static int _score = 0;
    public static int score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
                NotifySubscribers(nameof(score));
            }
        }
    }
    #endregion

    #region Change Notifier
    private static readonly Dictionary<string, List<Action>> subscribers = 
        new Dictionary<string, List<Action>>();
    private static void NotifySubscribers(String propertyName)
    {
        if (subscribers.ContainsKey(propertyName))
        {
            foreach (var action in subscribers[propertyName])
            {
                action();
            }
            // subscribers[propertyName].ForEach(action => action());
        }
    }
    public static void Subscribe(string propertyName, Action action)
    {
        if( ! subscribers.ContainsKey(propertyName)) 
        {
            subscribers[propertyName] = new List<Action>();
        }
        subscribers[propertyName].Add(action);
    }
    public static void Subscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0) throw new ArgumentException(
            $"{nameof(propertyNames)} must have at least 1 value");
        foreach (var propertyName in propertyNames)
        {
            Subscribe(propertyName, action);
        }
    }
    public static void UnSubscribe(string propertyName, Action action)
    {
        if (subscribers.ContainsKey(propertyName))
        {
            subscribers[propertyName].Remove(action);
        }
    }
    public static void UnSubscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0) throw new ArgumentException(
            $"{nameof(propertyNames)} must have at least 1 value");
        foreach (var propertyName in propertyNames)
        {
            UnSubscribe(propertyName, action);
        }
    }
    #endregion

    #region Game events
    // GameState.TriggerEvent(new Event{...} / raw data)  [Post Emit Raise Send Dispatch Trigger]
    private const string broadcastKey = "Broadcast";
    public static void TriggerEvent(string type, object payload = null)
    {
        if(eventListeners.ContainsKey(type))
        {
            lock (eventListeners[type])
            {
                foreach (var eventListener in eventListeners[type])
                {
                    eventListener(type, payload);
                }
            }
        }

        if (eventListeners.ContainsKey(broadcastKey))
        {
            lock (eventListeners)
            {
                foreach (var eventListener in eventListeners[broadcastKey])
                {
                    eventListener(type, payload);
                }
            }
        }
    }
    private static Dictionary<String, List<Action<string, object>>> eventListeners = new();
    
    public static void SubscribeTrigger(Action<string, object> action, params string[] types)
    {
        if(types.Length == 0)
        {
            types = new string[1] { broadcastKey };
        }
        foreach (var type in types)
        {
            if(!eventListeners.ContainsKey(type))
            {
                eventListeners[type] = new List<Action<string, object>>();
            }
            eventListeners[type].Add(action);
        }
    }
    public static void UnSubscribeTrigger(Action<string, object> action, params string[] types)
    {
        if (types.Length == 0)
        {
            types = new string[1] { broadcastKey };
        }
        foreach (var type in types)
        {
            if (eventListeners.ContainsKey(type))
            {
                eventListeners[type].Remove(action);
                if(eventListeners[type].Count == 0)
                {
                    eventListeners.Remove(type);
                }
            }
        }
    }
    #endregion
}
/* ChangeNotifier / Observer - ідея "активних" даних, коли зміна даних
 * формує повідомлення про цей факт
 * 
 * GameState    ->     DoorScript [sound]
 * volume       ->     MusicScript [clip]
 *  {set}    <------   SettingScript [slider]
 *  
 *  
 * Варіанти реалізації
 * - повідомлення про зміни (не зазначаючи джерело)
 * - повідомлення із зазначенням зміненої властивості
 * - підписка на зміни конкретної властивості
 */
