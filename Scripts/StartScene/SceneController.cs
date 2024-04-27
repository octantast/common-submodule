using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject playerPrefab;
    private PlayerMover player;
    public CharacterGenerator charGen;

    public PlanetsPositions planets;

    private AsyncOperation asyncOperation;
    void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync("Citrakis");
        asyncOperation.allowSceneActivation = false;

        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player = playerPrefab.GetComponent<PlayerMover>();
        player.setForMenu();
    }

    public void startGame()
    {
        asyncOperation.allowSceneActivation = true;
    }
}
