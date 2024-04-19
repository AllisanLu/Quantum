using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyContinue : MonoBehaviour
{
    public string next;
    private void Update()
    {
        if (Input.anyKey)
        {
            MoveOn();
        }
    }

    public void MoveOn()
    {
        GameManager.instance.cutscene = true;

        LevelLoader.instance.LoadLevelByName(next);
    }
}
