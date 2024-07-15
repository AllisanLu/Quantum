using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    public bool world1 = false; // if world1 = player1 :D
    public bool isActivePlayer = false;
    public bool dead = false;

    public PlayerJump jump;
    public PlayerAnimation anim;

    private void Awake()
    {
        jump = GetComponent<PlayerJump>();
        anim = GetComponentInChildren<PlayerAnimation>();
    }

    public void Die()
    {
        dead = true;
        LevelLoader ll = LevelLoader.instance;
        GameManager gm = GameManager.instance;
        PlayerMovement pm = GetComponent<PlayerMovement>();

        pm.canMove = false;

        if (gm.IsNetworked()) {
            ll.ReloadLevelServerRpc();
        } else {
            ll.ReloadLevel();
        }

        anim.SetTrigger("death");
    }
}
