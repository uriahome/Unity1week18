using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance = null;
    float FadeWaitTime = 1.0f;//フェード時の待ち時間
    [SerializeField]
    GameObject FadeCanvasPrefab;
    GameObject FadeCanvasClone;
    FadeCanvas fadeCanvas;
    public string[] StageName;//stageの名前リスト
    public int CurrentStageNum = 0;//現在挑んでいるステージ番号

    void Awake()
    {
        if (instance == null)//1つだけ存在するようにする
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);//被っていたら消える
        }
        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene(int Num){//対応した番号のステージを読み込む
        FadeCanvasClone = Instantiate(FadeCanvasPrefab);
        fadeCanvas = FadeCanvasClone.GetComponent<FadeCanvas>();
        fadeCanvas.FadeIn = true;
        yield return new WaitForSeconds(FadeWaitTime);
        yield return SceneManager.LoadSceneAsync(StageName[Num%4]);
        fadeCanvas.FadeOut = true;
    }

    public void RetryScene(){
        StartCoroutine(LoadScene(CurrentStageNum));//ステージ番号を変更せずに読み込む
    }

    public void NextScene(){
        CurrentStageNum++;//次のシーンの番号に進む
        StartCoroutine(LoadScene(CurrentStageNum));
    }
}
