using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public string number;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Button Cell;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Sprite fillSprite;

    [SerializeField]
    private Sprite sprite;

    private int cellNum = 5;

    private int num;

    private float size;

    private bool startFlag = false;

    private Button[][] button;

    private Button[][] buttonMirror;

    void Start()
    {
        num = int.Parse(number);

        panel.SetActive(false);

        Random.InitState(num);

        cellNum += Mathf.FloorToInt(num / 100);

        button = new Button[cellNum][];

        float w = canvas.GetComponent<RectTransform>().sizeDelta.x;
        float h = canvas.GetComponent<RectTransform>().sizeDelta.y;
        int span = 100;
        size = (h - span) / 2 / cellNum;


        for (int l = 0; l < cellNum; l++)
        {
            button[l] = new Button[cellNum];
            for (int i = 0; i < cellNum; i++)
            {
                Button ObjK = CreateButton();
                ObjK.name = (l * cellNum + i).ToString();
                ObjK.transform.position = new Vector2(-w / 2 + (w - size * cellNum) / 2 + i * size + size / 2, -h / 2 + cellNum * size - l * size - size / 2);
                ObjK.transform.SetParent(canvas.transform, false);
                ObjK.onClick.AddListener(() => OnClick(ObjK.name, button));
                button[l][i] = ObjK;
            }
        }

        Random.InitState(num);
        buttonMirror = new Button[cellNum][];
        for (int l = 0; l < cellNum; l++)
        {
            buttonMirror[l] = new Button[cellNum];
            for (int i = 0; i < cellNum; i++)
            {
                Button ObjK = CreateButton();
                ObjK.name = (l * cellNum + i).ToString();
                ObjK.transform.position = new Vector2(-w / 2 + (w - size * cellNum) / 2 + i * size + size / 2, h / 2 - l * size - size / 2);
                ObjK.transform.SetParent(canvas.transform, false);
                buttonMirror[l][i] = ObjK;
            }
        }

        Random.InitState(num);
        for (int l = 0; l < Random.Range(10, 100); l++)
        {
            int x = Random.Range(0, cellNum);
            int y = Random.Range(0, cellNum);
            OnClick(buttonMirror[x][y].name, buttonMirror);
        }

        startFlag = true;
    }

    Button CreateButton()
    {
        Button ObjK = Instantiate(Cell);
        ObjK.gameObject.GetComponent<Image>().sprite = Random.value > 0.8 ? fillSprite : sprite;
        // ObjK.gameObject.GetComponent<Image>().SetNativeSize();
        var p = ObjK.GetComponent<RectTransform>();
        p.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        p.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        return ObjK;
    }

    void OnClick(string name, Button[][] arr)
    {
        int[] pos = FindIndex(name, arr);

        int[][] arrowArr = new int[][]{
            new int[] {0, -1},
            new int[] {-1, -1},
            new int[] {-1, 0},
            new int[] {-1, 1},
            new int[] {0, 1},
            new int[] {1, 1},
            new int[] {1, 0},
            new int[] {1, -1},
        };

        List<Button> mem = new List<Button>();
        List<int[]> memArr = new List<int[]>();
        List<Vector3> memPos = new List<Vector3>();
        foreach (int[] a in arrowArr)
        {
            int x = pos[0] + a[0];
            int y = pos[1] + a[1];
            if (OverBoard(x, y))
            {
                mem.Add(arr[pos[0] + a[0]][pos[1] + a[1]]);
                memArr.Add(a);
                memPos.Add(arr[pos[0] + a[0]][pos[1] + a[1]].GetComponent<RectTransform>().transform.position);
            }
        }

        List<int[]> copy = new List<int[]>(memArr);
        for (int i = 0; i < memArr.Count; i++)
        {
            int a = (i + 1) % memArr.Count;
            int mx = pos[0] + memArr[a][0];
            int my = pos[1] + memArr[a][1];
            arr[mx][my] = mem[i];
            arr[mx][my].GetComponent<RectTransform>().transform.position = new Vector2(memPos[a].x, memPos[a].y);
        }

        if (startFlag)
        {
            IsClear();
        }
    }

    private void IsClear()
    {
        bool isFlg = true;

        for (int l = 0; l < cellNum; l++)
        {
            for (int i = 0; i < cellNum; i++)
            {
                if (button[l][i].GetComponent<Image>().sprite != buttonMirror[l][i].GetComponent<Image>().sprite)
                {
                    isFlg = false;
                    break;
                }
            }
            if (!isFlg) break;
        }

        if (isFlg)
        {
            var stage = PlayerPrefs.GetInt("Stage", 1);
            PlayerPrefs.SetInt("stage", Mathf.Max(num + 1, stage));
            PlayerPrefs.Save();
            panel.SetActive(true);
        }
    }

    private int[] FindIndex(string name, Button[][] arr)
    {
        int x = -1;
        int y = -1;
        int[] r = new int[] { x, y };

        for (int l = 0; l < arr.Length; l++)
        {
            for (int i = 0; i < arr[l].Length; i++)
                if (arr[l][i].name == name)
                {
                    r[0] = l;
                    r[1] = i;
                    break;
                }
        }

        return r;
    }

    private bool OverBoard(int x, int y)
    {
        return x >= 0 && x < cellNum && y >= 0 && y < cellNum;
    }
}
