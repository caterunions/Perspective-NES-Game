using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Debug Message", menuName = "Scriptable Objects/Trinket Effect Activations/Debug Message")]
public class DebugMessageActivation : TrinketEffectActivation
{
    [SerializeField]
    private string _message;

    public override void Activate()
    {
        Debug.Log(_message);
        Debug.Log($"player {_player}\nactivator {_activator}\nbullet {_bullet}\ntarget {_target}\ndmgEvent {_dmgEvent}\nspellInv {_spellInventoryData}\ntrinketInv {_trinketInventoryData}\nplayerStats {_playerStats}\nbulletLauncher {_bulletLauncher}");
    }
}