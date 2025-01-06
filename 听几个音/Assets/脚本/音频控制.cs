using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class 音频控制 : MonoBehaviour
{
    List<List<AudioSource>> 音组 = new();
    public List<AudioSource> List;
    public List<AudioSource> C大调;
    public List<AudioSource> D大调;
    public int 音数;
    private int 范围 = 36;
    List<AudioSource> 当前音组;
    float 间隔时间 = 1f;
    public TMP_Dropdown dropdown音数;  // 引用 TMP_Dropdown 组件
    public TMP_Dropdown dropdown音调;
    public Slider Slider;

    void Start()
    {
        当前音组 = List;
        音组.Add(List);
        音组.Add(C大调);
        音组.Add(D大调);

        // 监听 TMP_Dropdown 的值改变事件
        dropdown音数.onValueChanged.AddListener(音数更改);
        dropdown音调.onValueChanged.AddListener(音调更改);
        Slider.onValueChanged.AddListener(时间间隔更改);
    }

    // 处理 TMP_Dropdown 值改变的函数
    private void 音数更改(int value)
    {
        // value 参数是选择的索引
        Debug.Log("Selected Value: " + dropdown音数.options[value].text);
        音数 = value + 2;
        // 在这里处理下拉框选择改变后的逻辑
    }
    private void 音调更改(int 音)
    {
        if (音 != 0) { 范围 = 21; } else { 范围 = 36; }
        当前音组 = 音组[音];
    }
    private void 时间间隔更改(float value)
    {
        间隔时间 = value;
    }
    public void 随机几个音()
    {
        StartCoroutine(播放随机音效(音数));  // 启动协程来控制音效播放
    }

    // 协程来逐个播放音效并在之间加上1秒延时
    private IEnumerator 播放随机音效(int 音数)
    {
        for (int i = 0; i < 音数; i++)
        {
            播音(UnityEngine.Random.Range(0, 范围), 当前音组); // 播放随机音效
            yield return new WaitForSeconds(间隔时间); // 等待1秒
        }
    }
    public void 音检()
    {
        StartCoroutine(依次播放());
    }

    private IEnumerator 依次播放()
    {
        foreach (var item in 当前音组)
        {
            if (!item.isPlaying)
            {
                item.Play();  // 播放当前音源
            }

            yield return new WaitForSeconds(间隔时间); // 延迟指定时间后再播放下一个音源
        }
    }
    public void 播音(int 音调,List<AudioSource> 音组)
    {
        //print(音调);
        if (!音组[音调].isPlaying) 音组[音调].Play();
    }

}
