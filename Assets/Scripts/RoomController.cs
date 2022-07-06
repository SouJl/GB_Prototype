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

    private List<SpawnManager> _spawners;


    void Start()
    {
        _battleState = BattleState.none;
        _spawners = new List<SpawnManager>();
        if (SpawnerGo != null) 
        {
            foreach (var spawner in SpawnerGo) 
            {
                _spawners.Add((SpawnManager)spawner.GetComponent(typeof(SpawnManager)));
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
                                SoundManager.instance.Play("Door");
                                doorScript.doorAnimator.SetBool("IsOpening", false);
                            }
                                
                        }
                        if (triggeredSpawner.isEnemySpawn) 
                        {
                            MinimapZoom.instance.ZoomOut();
                            _battleState = BattleState.onFight;
                        }
 
                        break;
                    }
                case BattleState.onFight:
                    {                  
                        if (Main.instance.EnemyCount == 0)
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
                        MinimapZoom.instance.ZoomIn();
                        _spawners = null;
                        _battleState = BattleState.none;
                        break;
                    }
            }
        }
    }
}
