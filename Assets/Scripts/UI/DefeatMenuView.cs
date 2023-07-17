using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenuView : MonoBehaviour
{
    [SerializeField] private GameObject _defeatMenu;
    [SerializeField] private GameObject _rocket;
    [SerializeField] private GameObject _map;
    

    [UsedImplicitly]
    public void OnDefeatMenu()
    {
        _defeatMenu.SetActive(true);
        _rocket.SetActive(false);
        _map.SetActive(false);
    }

    private void OffDefeatMenu()
    {
        _map.SetActive(true);
        _rocket.SetActive(false);
        _defeatMenu.SetActive(true);
    }

    public void RestartGame()
    {
        OffDefeatMenu();
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
