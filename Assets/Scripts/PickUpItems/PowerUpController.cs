using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    none,
    damage,
    speed
}

public class PowerUpController : BaseItemController
{
    [Header("Set in Inspector: PowerUp")]
    public PowerUpType powerUp;
}
