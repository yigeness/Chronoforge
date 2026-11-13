using System;
using UnityEngine;

public class M0Runner : MonoBehaviour
{
    //Awake的作用是在创建物体的时候给物体一个预设
    private void Awake()
    {
        untreatedTimes = 0f;
        finishedTickCnt = 0;
        ticksOfEachLog = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SimWorld simword = new SimWorld();
        //第一步取得没有经过处理的实际现实时间
        float dt = Time.unscaledDeltaTime;
        untreatedTimes += dt;
        ticksOfEachLog += dt;

        //根据累计的时间来执行逻辑步
        int steps = 0;//初始化当前时间步数为0
        while(untreatedTimes >= fixStepLen && steps < maxStepsPerFrame)
        {
            steps ++;
            finishedTickCnt ++;
            untreatedTimes -= fixStepLen;
            simword.DoStep();
        }

        if(untreatedTimes >= fixStepLen && steps >= maxStepsPerFrame)
        {
            Debug.LogWarning($"达到单帧逻辑步上限{maxStepsPerFrame}，丢弃{untreatedTimes:F3}秒累计时间");
            untreatedTimes = 0f;
        }

        if(ticksOfEachLog >= 1f)
        {
            ticksOfEachLog -= 1f;
            Debug.Log($"finishedTickCnt = {finishedTickCnt}");
        }
    }

    float untreatedTimes = 0f;//用来统计没有经过处理的时间
    int finishedTickCnt = 0;//已经完成的tick计数
    float ticksOfEachLog = 0f;//每一个输出log需要经过的间隔
    const float fixStepLen = 1f / 60f;//固定一个步长为1/60s
    const int maxStepsPerFrame = 5;//这里来设置每一帧需要执行多少个逻辑步
}
