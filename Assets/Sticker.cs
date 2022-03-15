using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sticker : MonoBehaviour
{
    [SerializeField] TMP_Text StickerNameText;
    [SerializeField] TMP_Text StickerCountText;
    [SerializeField] Image stickerImage;
    public bool isUnlocked;
    public string stickerName
    {
        get
        {
            return StickerNameText.text;
        }
        set
        {
            StickerNameText.text = value;
        }
    }
    public int Count
    {
        get
        {
            return _count;
        }
        set
        {
            _count = value;
            stickerImage.color = new Color(1, 1, 1, .05f);

            stickerCountGO.SetActive(false);
            if (_count > 0)
                stickerImage.color = new Color(1, 1, 1, 1);
            if (_count > 1)
            {

                stickerCountGO.SetActive(true);
                StickerCountText.text = "+ " + _count.ToString();

            }
        }
    }
    int _star;
    int _count;
    [SerializeField] GameObject[] starsGO;
    [SerializeField] GameObject stickerCountGO;
    public int Stars
    {
        get
        {
            return _star;
        }
        set
        {
            _star = value;
            HideAllStars();
            SetStar();
        }
    }
    void SetStar()
    {
        for (int i = 0; i < Stars; i++)
        {
            starsGO[i].SetActive(true);
        }
    }
    void HideAllStars()
    {
        foreach (var star in starsGO)
        {
            star.SetActive(false);
        }
    }
}
