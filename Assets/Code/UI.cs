using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _remainningEnemies;

    [SerializeField] private GameObject _aim;

    [SerializeField] private MouseLook _hor;
    [SerializeField] private MouseLook _vert;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Start()
    {
        _healthBar.maxValue = _playerHealth.maxHealth;
    }

    void Update()
    {
        _healthBar.value = _playerHealth.health;
        _level.text = "Level: " + LevelCotroller.singleton.level;
        _remainningEnemies.text = "Enemies lost: " + (LevelCotroller.singleton.remainingEnemies + LevelCotroller.singleton.enemies.Count);
    }

    public void ShowDefeatPanel()
    {
        _panel.SetActive(true);
        _aim.SetActive(false);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _hor.enabled = false;
        _vert.enabled = false;
    }
    public void OnClickRestart()
    {
        Time.timeScale = 1;
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    public void ClearLevel()
    {
        PlayerPrefs.SetInt("Level", 1);
        LevelCotroller.singleton.level = 1;
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
