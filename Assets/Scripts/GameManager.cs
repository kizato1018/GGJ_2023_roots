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
    public Slider _progress;

    static public GameManager instance;
    private List<Character> players_;
    private List<Rabbit> rabbits_;

    private bool gameover_flag_;

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
        Pause();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        _progress.gameObject.SetActive(true);
        //用Slider 展示的数值
        int disableProgress = 0;
        int toProgress = 0;

        //异步场景切换
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        //不允许有场景切换功能
        op.allowSceneActivation = false;
        //op.progress 只能获取到90%，最后10%获取不到，需要自己处理
        while (op.progress < 0.9f)
        {
            //获取真实的加载进度
            toProgress = (int)(op.progress * 100);
            while (disableProgress < toProgress)
            {
                ++disableProgress;
                _progress.value = disableProgress / 100.0f;//0.01开始
                yield return new WaitForEndOfFrame();
            }
        }
        //因为op.progress 只能获取到90%，所以后面的值不是实际的场景加载值了
        toProgress = 100;
        while (disableProgress < toProgress)
        {
            ++disableProgress;
            _progress.value = disableProgress / 100.0f;
            yield return new WaitForEndOfFrame();
        }
        Resume();
        RootsManager.instance = null;
        TreeManager.instance = null;
        AudioManager.instance = null;
        GameManager.instance = null;
        op.allowSceneActivation = true;
        _progress.gameObject.SetActive(false);
    }
}