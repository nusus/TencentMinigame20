/*
 Attach this to main camera or some object which is always active.
 Copyright (C) 2016 hearst zhang, all rights reserved.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Drama : MonoBehaviour
{
    //The target guitext to show content.
    [Header("剧情文字")]
    [Tooltip("用于显示剧情文字")]
    public Text guiBackgroundText;

    public GameObject[] dramaImage;

    // use this to modify scene.
    private enum DramaScene
    {
        Chapter0,
        Chapter1,
        Mission1,
        Mission2,
        Mission3
    }

    //using this to select drama.
    private int dramaSceneSelect;

    //the index of image
    private int imageUI;
    //the index of text content
    private int textContent;

    //using to record touch status,
    //avoid to show the drama too quick.
    private bool touchStat;

    // Use this for initialization
    void Start()
    {
        imageUI = 0;
        textContent = 0;
        guiBackgroundText.text = "";
        dramaSceneSelect = GameDatabase.GetInstance().drama;

        //if a drama has multiple image background
        //using switch instead.
        imageUI = dramaSceneSelect;

        touchStat = false;
    }

    void ClearActive()
    {
        for (int i = 0; i < dramaImage.Length; i++)
        {
            //dramaImage[i].SetActive(false);
            guiBackgroundText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        dramaImage[imageUI].SetActive(true);

        if(Input.touchCount == 0)
        {
            touchStat = false;
        }

        if (touchStat == false && ( Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            touchStat = true;
            ClearActive();

            switch (dramaSceneSelect)
            {
                case (int)DramaScene.Chapter0:

                    //modify text and image, will take effect at next click.
                    switch (textContent)
                    {
                        //change to next text during click.
                        case 0:
                            guiBackgroundText.text = "";
                            textContent = 1;
                            break;
                        case 1:
                            guiBackgroundText.text = @"夜凉如水。新月悄悄地爬了上来，挂在枝头，俏皮地眨着眼睛。";
                            textContent = 2;
                            break;
                        case 2:
                            guiBackgroundText.text = "公园里，石子路上悄无一人，一旁的灌木丛中，似有什么小动物。";
                            textContent = 3;
                            break;
                        case 3:
                            guiBackgroundText.text = "待在一个摇篮里。在朦胧夜色的掩映下，它的身影若隐若现；它是谁，为什么会在这个地方？";
                            textContent = 4;
                            break;
                        case 4:
                            guiBackgroundText.text = "而此刻夜空中，一个神秘的影子一闪而过，化作光点在东方的夜空中消失了……";
                            textContent = 5;
                            break;
                        default:
                            SceneManager.LoadScene("mainui", LoadSceneMode.Single);
                            break;
                    }
                    break;
                case (int)DramaScene.Chapter1:
                    switch (textContent)
                    {
                        case 0:
                            guiBackgroundText.text = "";
                            textContent = 1;
                            break;
                        case 1:
                            guiBackgroundText.text = "小外星人究竟从何而来并不重要，重要的是，孤独的男孩儿终于有了伙伴。";
                            textContent = 2;
                            break;
                        case 2:
                            guiBackgroundText.text = "小外星人年纪不大，凡事都需要小男孩儿照顾。";
                            textContent = 3;
                            break;
                        case 3:
                            guiBackgroundText.text = "小男孩儿与单身母亲相依为命，可是由于工作忙碌加上心情糟糕，母亲时常会忽视和孩子的关爱和沟通。";
                            textContent = 4;
                            break;
                        case 4:
                            guiBackgroundText.text = "虽然小外星人和小男孩儿语言上还无法沟通，但是他们的感情却跨越了一切外在的障碍联系到一起";
                            textContent = 5;
                            break;
                        case 5:
                            guiBackgroundText.text = "尽管他们的外型有如此大的差异，但却都有着一颗善良敏感、渴望着爱和呵护的童心。";
                            textContent = 6;
                            break;
                        default:
                            SceneManager.LoadScene("mainui", LoadSceneMode.Single);
                            break;
                    }
                    break;
                case (int)DramaScene.Mission1:
                    switch (textContent)
                    {
                        case 0:
                            guiBackgroundText.text = "";
                            textContent = 1;
                            break;
                        case 1:
                            guiBackgroundText.text = "在他们之间，建立起一种奇妙的心灵感应，E.T.难过的时候，艾里奥特也会感觉忧郁，E.T.病了，艾里奥特也跟着不舒服。";
                            textContent = 2;
                            break;
                        case 2:
                            guiBackgroundText.text = "孤独的E.T.和孤独的艾里奥特成了最好的朋友，于是他们都不再孤独。";
                            textContent = 3;
                            break;
                        default:
                            SceneManager.LoadScene("mainui", LoadSceneMode.Single);
                            break;
                    }
                    break;
                case (int)DramaScene.Mission2:
                    switch (textContent)
                    {
                        case 0:
                            guiBackgroundText.text = "";
                            textContent = 1;
                            break;
                        case 1:
                            guiBackgroundText.text = "直到有一天，E.T.还是不可避免地被大人们发现了。于是警察、军队、FBI……蜂拥而至";
                            textContent = 2;
                            break;
                        case 2:
                            guiBackgroundText.text = "大人们不顾孩子们的苦苦哀求，无情地抓走了E.T.，根本无视此时的它是那么的无辜、脆弱和绝望，他们只想把这个外星人当成千载难逢的珍贵试验品进行研究。";
                            textContent = 3;
                            break;
                        case 3:
                            guiBackgroundText.text = "艾里奥特在哥哥和伙伴们的帮助下终于从研究中心救出了九死一生的E.T.，不料大人们根本没有放过他们，沿路设下重重关卡意图拦截这支“营救小队”";
                            textContent = 4;
                            break;
                        case 4:
                            guiBackgroundText.text = "就在大人的眼皮底下，E.T.展现了它不可思议的神奇力量，带着大家摆脱了“包围圈”";
                            textContent = 5;
                            break;
                        default:
                            SceneManager.LoadScene("mainui", LoadSceneMode.Single);
                            break;
                    }
                    break;
                case (int)DramaScene.Mission3:
                    switch (textContent)
                    {
                        case 0:
                            guiBackgroundText.text = "";
                            textContent = 1;
                            break;
                        case 1:
                            guiBackgroundText.text = "在当初发现E.T.的树林里，来接E.T.回去的外星飞船赶来了";
                            textContent = 2;
                            break;
                        case 2:
                            guiBackgroundText.text = "一直念念不忘要回家的E.T.终于要走了";
                            textContent = 3;
                            break;
                        case 3:
                            guiBackgroundText.text = "艾里奥特恋恋不舍地和他的外星朋友告别，望着远去的飞船划破天际艳丽的夕阳";
                            textContent = 4;
                            break;
                        case 4:
                            guiBackgroundText.text = "艾里奥特知道，他将会永远永远记住这段短暂却美丽的友谊";
                            textContent = 5;
                            break;
                        default:
                            SceneManager.LoadScene("mainui", LoadSceneMode.Single);
                            break;
                    }
                    break;
                default:
                    SceneManager.LoadScene("mainui", LoadSceneMode.Single);
                    break;
            }
        }
    }
}
