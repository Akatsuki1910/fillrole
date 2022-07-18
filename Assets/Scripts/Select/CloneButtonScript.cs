using UnityEngine;
using UnityEngine.UI;

public class CloneButtonScript : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scroll;

    [SerializeField]
    private GameObject Button;

    [SerializeField]
    private GameObject BackButton;

    [SerializeField]
    private GameObject InfoButton;

    GameObject ObjK;

    void Start()
    {
        int stage = PlayerPrefs.GetInt("stage", 1);
        // int stage = 500;
        int numX = 5;

        float oldh = scroll.GetComponent<RectTransform>().rect.height;
        float sw = scroll.verticalScrollbar.GetComponent<RectTransform>().rect.width;
        float w = scroll.GetComponent<RectTransform>().rect.width;
        float size = (w - sw) / numX;

        var sc = scroll.content.GetComponent<RectTransform>();
        sc.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size * (Mathf.FloorToInt((stage - 1) / numX)) + size);

        float h = scroll.content.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < stage; i++)
        {
            ObjK = Instantiate(Button);
            var p = ObjK.GetComponent<RectTransform>();
            p.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
            p.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
            ObjK.transform.position = new Vector2(-w / 2 + (w - size * 5) / 2 + (i % numX) * size + size / 2, h / 2 - Mathf.FloorToInt(i / numX) * size - size / 2);
            ObjK.GetComponentInChildren<Text>().text = (i + 1).ToString();
            ObjK.transform.SetParent(scroll.content, false);
        }
        scroll.verticalNormalizedPosition = 1;

        var bb = BackButton.GetComponent<RectTransform>();
        bb.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w / 2);
        bb.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
        BackButton.transform.localPosition = new Vector2(-w / 4, -this.GetComponent<RectTransform>().rect.height / 2 + 50);

        var ib = InfoButton.GetComponent<RectTransform>();
        ib.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w / 2);
        ib.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
        InfoButton.transform.localPosition = new Vector2(w / 4, -this.GetComponent<RectTransform>().rect.height / 2 + 50);
    }
}
