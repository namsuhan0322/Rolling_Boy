using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    private TextMeshProUGUI load_Text;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        int fillAmount;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                fillAmount = Mathf.CeilToInt(op.progress * 100);
                load_Text.text = string.Format("{0}%", fillAmount.ToString());
            }
            else
            {
                timer += Time.unscaledDeltaTime * 0.3f;
                fillAmount = Mathf.CeilToInt(Mathf.Lerp(90f, 100f, timer));
                load_Text.text = string.Format("{0}%", fillAmount.ToString());

                if (fillAmount == 100)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
