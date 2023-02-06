using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public int scene_number = 1;
    public Slider _progress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        _progress.gameObject.SetActive(true);
        //用Slider 展示的数值
        int disableProgress = 0;
        int toProgress = 0;

        //异步场景切换
        AsyncOperation op = SceneManager.LoadSceneAsync(scene_number);
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
        RootsManager.instance = null;
        TreeManager.instance = null;
        AudioManager.instance = null;
        GameManager.instance = null;
        op.allowSceneActivation = true;
        _progress.gameObject.SetActive(false);
    }
}
