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


    static public GameManager instance;
    private List<Character> players_;
    private List<Rabbit> rabbits_;

    private bool gameover_flag_;

    public List<Character> players{
        get { return players_; }
    }
    public List<Rabbit> rabbits{
        get { return rabbits_;}
    }
    
    public int total_seconds;                 //遊戲總時長
    // 遊戲進行時長
    [HideInInspector] public int m_seconds;

    public TMP_Text m_timer;           //設定畫面倒數計時的文字

    private void Awake() {
        
    }
    void Start() {
        m_seconds = 0;
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        int m_min = 0;
        int m_sec = 0;

        while (m_seconds < total_seconds)                   //如果時間尚未結束
        {
            m_seconds += 1;
            m_min = m_seconds / 60;
            m_sec = m_seconds % 60;
            m_timer.text = string.Format("{0}:{1}", m_min.ToString("00"), m_sec.ToString("00"));
            print(m_seconds);

            yield return new WaitForSeconds(1); //等候一秒再次執行
                       
        }
        Finish();                   //時間結束時，控制遊戲暫停無法操作
    }
    public void Finish() {
        print("Game End");
        Time.timeScale = 0;
    }

    public void Pause() {
        Time.timeScale = 0;
    }
    public void Resume() {
        Time.timeScale = 1;
    }
}