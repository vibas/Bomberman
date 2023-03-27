using System;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static Action OnNewRoundRequestedEvent;
    public static Action<Dictionary<Vector3Int,bool>> OnNewLevelGeneratedEvent;

    public static Action OnPlayerDeathEvent;
    public static Action OnPlayerDeathSequenceCompleteEvent;

    public static Action<GameObject> OnEnemyDeathEvent;
    public static Action OnEnemyDeathSequenceCompleteEvent;
    public static Action OnAllEnemiesClearedEvent;
    public static Action<GameObject,GameObject> OnEnemyFoundBlockerAheadEvent;
    public static Action<GameObject> OnEnemyFoundNoBlockerAheadEvent;

    public static Action<bool> OnGameOverEvent;
    public static Action OnGameResetRequestedEvent;
}
