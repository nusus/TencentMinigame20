using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class weishuiPanelCtrl: MonoBehaviour {

	public Button exitButton;
	public GameObject weishuiPanel;
	public GameObject quitweishuiPanel;
	public GameObject weishuijieguoPanel;

    private long startTime = DateTime.Now.Ticks;
    private long tickTime = 0;
    private bool isInTick = true;

	void Start () {
		EventTriggerListener.Get(exitButton.gameObject).onClick = OnExitClick;
	}

    void Update()
    {
        if (isInTick)
        {
            tickTime += DateTime.Now.Ticks - startTime;

            if (tickTime >= 5 * 10000000)
            {
                LaunchweishuijieguoPanel();
                tickTime = 0;
                StopTick();
            }
        }

        startTime = DateTime.Now.Ticks;
    }

	public void Awake () {
        StartTick();
	}

    /// <summary>
    /// 开启计时
    /// </summary>
    public void StartTick()
    {
        isInTick = true;
        startTime = DateTime.Now.Ticks;
    }

    /// <summary>
    /// 暂停计时
    /// </summary>
    public void PauseTick()
    {
        isInTick = false;
        tickTime += DateTime.Now.Ticks - startTime;
        startTime = DateTime.Now.Ticks;
    }

    /// <summary>
    /// 恢复计时
    /// </summary>
    public void ResumeTick()
    {
        isInTick = true;
		startTime = DateTime.Now.Ticks;
    }

    /// <summary>
    /// 停止计时
    /// </summary>
    public void StopTick()
    {
        isInTick = false;
        tickTime = 0;
        startTime = DateTime.Now.Ticks;
    }

	void LaunchweishuijieguoPanel(){
		weishuiPanel.SetActive(false);
		weishuijieguoPanel.SetActive(true);
		StopTick();
	}

	private void OnExitClick(GameObject go)
	{
		quitweishuiPanel.SetActive(true);
        PauseTick();
	}
}
