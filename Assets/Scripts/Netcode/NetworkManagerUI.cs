using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class NetworkManagerUI : MonoBehaviour
{
    //[SerializeField]private Button serverBtn;
    [SerializeField]private Button hostBtn;
    [SerializeField]private Button clientBtn;
    public GameObject NetworkManagerPrefab;
    public GameObject LocalPlayerControllerPrefab;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    private void Awake() 
    {
        if (GameObject.Find("NetworkManager(Clone)") == null) { Instantiate(NetworkManagerPrefab); }
        hostBtn.onClick.AddListener(() => {
            Debug.Log("Host connecting");
            //RemovePlayerControls();
            GameManager.instance.SetNetworkedScreen(true);
            PlayerManager.instance.isHost = true;
            PlayerManager.instance.currPlayer = 1;
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.OnClientConnectedCallback += SetupTempNetworking;
            //PlayerManager.instance.currPlayerObject = GameObject.Find("Player 1");
        });

        clientBtn.onClick.AddListener(() => {
            Debug.Log("Client connecting");
            //RemovePlayerControls();
            GameManager.instance.SetNetworkedScreen(true);
            PlayerManager.instance.isHost = false;
            PlayerManager.instance.currPlayer = 2;
            NetworkManager.Singleton.StartClient();
            NetworkManager.Singleton.OnClientConnectedCallback += SetupTempNetworking;
            //PlayerManager.instance.currPlayerObject = GameObject.Find("Player 2");
        });
    }


    private void SetupTempNetworking(ulong clientId)
    {
        GameManager.instance.networkingOn = true;
        SceneManager.sceneLoaded -= GameManager.instance.SetUpLevel;
        GameManager.instance.GameEnable();
        Instantiate(LocalPlayerControllerPrefab);
        LevelLoader.instance.ReloadLevel();
    }

    private ParticleSystem FindParticleSystem(string systemName, GameObject playerObject)
    {
        Transform systemTransform = playerObject.transform.Find(systemName);
        if (systemTransform != null)
        {
            return systemTransform.GetComponent<ParticleSystem>();
        }
        else
        {
            Debug.LogError("ParticleSystem with name " + systemName + " not found on Player.");
            return null;
        }
    }

    private void RemovePlayerControls()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Destroy(player.GetComponent<PlayerMovement>());
        }
    }

}
