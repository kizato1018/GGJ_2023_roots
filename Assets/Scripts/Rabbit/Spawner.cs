using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Spawner : MonoBehaviour
{
    public static int rabbit_live_count = 0;
    public Wave[] waves;
    public List<Transform> instantiate_positions;
 
 
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; ++i) {
            instantiate_positions.Add(transform.GetChild(i).transform);
        }
        //启动迭代器
        StartCoroutine(SpawnerRabbits());
    }
 
    //迭代器
    private IEnumerator SpawnerRabbits() {
 
        //依次循环第几波
        foreach (Wave wave in waves)
        {
            //依次循环 第几波的 第几个敌人
            for (int i = 0; i < wave.rabbit_count; i++)
            {
                //生成敌人
                GameObject.Instantiate(wave.rabbit, instantiate_positions[UnityEngine.Random.Range(0, 12)].position, Quaternion.identity);
                //敌人存活的数量
                rabbit_live_count++;
                //不是最后一波就暂停
                if (i != waves.Length - 1) {
                    //生成一个敌人后 暂停多少秒 再继续生成
                    yield return new WaitForSeconds(wave.interval_in_wave);
                }
            }
            //如果场上还有敌人 存活就不出 下一波怪物
            while (rabbit_live_count > 0)
            {
                yield return 0;
            }
            //生成每一波 敌人之间的 时间间隔
            yield return new WaitForSeconds(0.2f);
        }
    }  
}