using UnityEngine;
using UnityEngine.SceneManagement;

public class BackTitleScript : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
