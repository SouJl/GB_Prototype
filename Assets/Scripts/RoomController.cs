using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BattleState
{
    none,
    onFight,
    fightEnd
}

public class RoomController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public List<GameObject> Doors;
    public GameObject SpawnerGo;

    [Header("Set Dynamically")]
    public BattleState _battleState;

    private EnemySpawner _spawner;


    void Start()
    {
        _battleState = BattleState.none;
        if (SpawnerGo != null)
            _spawner = (EnemySpawner)SpawnerGo.GetComponent(typeof(EnemySpawner));
    }


    void Update()
    {
        if (Doors == null) return;
        if (_spawner == null) return;

        if (_spawner.isTriggered)
        {
            _spawner.IsActive = false;

            switch (_battleState)
            {
                case BattleState.none:
                    {
                        foreach (var door in Doors)
                        {
                            DoorController doorScript = (DoorController)door.GetComponent(typeof(DoorController));
                            if (doorScript.state == DoorState.Default)
                                door.GetComponent<DoorController>().UpdateState(DoorState.OnEnemy);
                        }
                        _battleState = BattleState.onFight;
                        break;
                    }
                case BattleState.onFight:
                    {
                        if (!_spawner.enemys.Any())
                        {
                            foreach (var door in Doors)
                            {
                                DoorController doorScript = (DoorController)door.GetComponent(typeof(DoorController));
                                if (doorScript.state == DoorState.OnEnemy)
                                    door.GetComponent<DoorController>().UpdateState(DoorState.Default);
                            }
                            _battleState = BattleState.fightEnd;
                        }
                        break;
                    }
                case BattleState.fightEnd:
                    {
                        break;
                    }
            }
        }
    }

    private void ChangeDoorState()
    {

    }
}
