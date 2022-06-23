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
    public List<GameObject> SpawnerGo;

    [Header("Set Dynamically")]
    public BattleState _battleState;

    private List<EnemySpawner> _spawners;


    void Start()
    {
        _battleState = BattleState.none;
        _spawners = new List<EnemySpawner>();
        if (SpawnerGo != null) 
        {
            foreach (var spawner in SpawnerGo) 
            {
                _spawners.Add((EnemySpawner)spawner.GetComponent(typeof(EnemySpawner)));
            }
        }
           
    }


    void Update()
    {
        if (Doors == null) return;
        if (_spawners == null) return;

        var triggeredSpawner = _spawners.Where(s => s.isTriggered).FirstOrDefault();

        if (triggeredSpawner != null && triggeredSpawner.isTriggered)
        {
            _spawners.ForEach(s => s.IsActive = false);

            switch (_battleState)
            {
                case BattleState.none:
                    {
                        foreach (var door in Doors)
                        {
                            DoorController doorScript = (DoorController)door.GetComponent(typeof(DoorController));
                            if (doorScript.state == DoorState.Default) 
                            {
                                doorScript.UpdateState(DoorState.OnEnemy);
                                doorScript.doorAnimator.SetBool("IsOpening", false);
                            }
                                
                        }
                        if(triggeredSpawner.isEnemySpawn)
                            _battleState = BattleState.onFight;
                        break;
                    }
                case BattleState.onFight:
                    {
                        if (!triggeredSpawner.enemys.Any())
                        {
                            foreach (var door in Doors)
                            {
                                DoorController doorScript = (DoorController)door.GetComponent(typeof(DoorController));
                                if (doorScript.state == DoorState.OnEnemy)
                                    doorScript.UpdateState(DoorState.Default);
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
}
