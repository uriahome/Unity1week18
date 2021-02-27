using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    float FadeWaitTime = 1.0f;//フェード時の待ち時間
    [SerializeField]
    GameObject FadeCanvasPrefab;
    GameObject FadeCanvasClone;
    FadeCanvas fadeCanvas;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene(){
        FadeCanvasClone = Instantiate(FadeCanvasPrefab);
        fadeCanvas = FadeCanvasClone.GetComponent<FadeCanvas>();
        fadeCanvas.FadeIn = true;
        yield return new WaitForSeconds(FadeWaitTime);
        yield return SceneManager.LoadSceneAsync("SampleScene");
        fadeCanvas.FadeOut = true;
    }

    public void RetryScene(){
        StartCoroutine(LoadScene());
    }
}
