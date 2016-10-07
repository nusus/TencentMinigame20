using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Drama : MonoBehaviour
{
    //The target guitext to show content.
    [Header("剧情文字")]
    [Tooltip("用于显示剧情文字")]
    public Text guiText;

    public GameObject[] dramaImage;

    //the index of image
    private int imageUI;
    //the index of text content
    private int textContent;

    // Use this for initialization
    void Start()
    {
        imageUI = 0;
        textContent = 0;
        guiText.text = "";
    }

    void clearActive()
    {
        for (int i = 0; i < dramaImage.Length; i++)
        {
            dramaImage[i].SetActive(false);
            guiText.text = "";
        }
    }
    // Update is called once per frame
    void Update()
    {
        dramaImage[0].SetActive(true);

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            //clear the image and text.
            clearActive();

            switch (imageUI)
            {
                case 0:
                    dramaImage[0].SetActive(true);
                    break;
                case 1:
                    dramaImage[1].SetActive(true);
                    break;
            }

            //modify text and image, will take effect at next click.
            switch (textContent)
            {
                case 0:
                    guiText.text = "";
                    //change to next text during click.
                    textContent = 1;
                    break;

                case 1:
                    guiText.text = "夜凉如水。新月悄悄地爬了上来，挂在枝头，俏皮地眨着眼睛。\n公园里，石子路上悄无一人，一旁的灌木丛中，似有什么小动物\n待在一个摇篮里。在朦胧夜色的掩映下，它的身影若隐若现；它是谁，\n为什么会在这个地方？\n而此刻夜空中，一个神秘的影子一闪而过，化作光点在东方的夜空中消失了……";
                    //change to next ui image during next click.
                    imageUI = 1;
                    textContent = 2;
                    break;
                case 2:

                    break;
            }
        }
    }
}
