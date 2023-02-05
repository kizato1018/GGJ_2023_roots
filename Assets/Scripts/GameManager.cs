// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

[System.Serializable]


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas_go_;

    public List<GameObject> rabbits_gameobject_;
    public List<GameObject> players_gameobject_;
    public GameObject game_over_obj;
    public GameObject game_win_obj;

    static public GameManager instance;
    private List<Character> players_;
    private List<Rabbit> rabbits_;

    private bool gameover_flag_;

    public int scene_number = 0;

    public List<Character> players
    {
        get { return players_; }
    }
    public List<Rabbit> rabbits
    {
        get { return rabbits_; }
    }

    public int total_seconds;                 //遊戲總時長
    // 遊戲進行時長

    public Text m_timer;           //設定畫面倒數計時的文字

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        game_over_obj.SetActive(false);
        game_win_obj.SetActive(false);
        StartCoroutine(Countdown());
        AudioManager.instance.PlayBgm("Happy Alley");
    }
    IEnumerator Countdown()
    {
        int m_min = total_seconds / 60;
        int m_sec = total_seconds % 60;

        while (total_seconds >= 0)                   //如果時間尚未結束
        {
            m_timer.text = string.Format("{0}:{1}", m_min.ToString("00"), m_sec.ToString("00"));
            total_seconds -= 1;
            m_min = total_seconds / 60;
            m_sec = total_seconds % 60;
            // print(m_seconds);
            yield return new WaitForSeconds(1); //等候一秒再次執行  
        }
        Finish();                   //時間結束時，控制遊戲暫停無法操作
    }
    public void Finish()
    {
        print("Game End");
        game_over_obj.SetActive(true);
        game_win_obj.SetActive(false);
        Time.timeScale = 0;
        AudioManager.instance.StopBgm();
    }

    /// <summary>
    /// 判斷勝利條件達成時call這個
    /// </summary>
    public void GameWin()
    {
        StopCoroutine(Countdown());
        print("Game Win");
        game_over_obj.SetActive(false);
        game_win_obj.SetActive(true);
        Time.timeScale = 0;
        AudioManager.instance.PlayBgm("The Forest and the Trees");
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ReStart()
    {
        Resume();
        RootsManager.instance = null;
        TreeManager.instance = null;
        AudioManager.instance = null;
        GameManager.instance = null;
        SceneManager.LoadScene(scene_number);
    }
}