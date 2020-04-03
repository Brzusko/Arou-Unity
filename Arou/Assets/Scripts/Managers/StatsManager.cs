using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    private static StatsManager _instance;

    public string playerName = "";
    public Text playerNameField;
    public int playerScore;
    
    public static StatsManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    public void OnStart()
    {
        if (_instance.playerNameField.text.Length == 0)
            return;
        _instance.playerName = _instance.playerNameField.text;
        SceneManager.LoadScene("MainGameScene");
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
