using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//UIをいじるために

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
    public int DeathCount;//失敗した回数

    public GameObject ScoreTextObj;//失敗した回数を表示するテキストを持つオブジェクト
    public Text ScoreText;
    public GameObject StageTextObj;//挑戦中のステージを表示するテキストを持つオブジェクト
    public Text StageText;

    void Awake()
    {
        if (instance == null)//1つだけ存在するようにする
        {
            instance = this;
            DeathCount = 0;//ゲーム開始時なので0にする
            transform.parent = null;//親要素を確実に消してDontDestroyOnLoad()できるようにする
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);//被っていたら消える
        }
        
    }
    void Start()
    {
        ScoreTextObj = GameObject.Find("GameManager/Canvas/ScoreText").gameObject;
        ScoreText = ScoreTextObj.GetComponent<Text>();
        StageTextObj = GameObject.Find("GameManager/Canvas/StageText").gameObject;
        StageText = StageTextObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = DeathCount.ToString();
        StageText.text = StageName[CurrentStageNum].ToString();
    }

    IEnumerator LoadScene(int Num){//対応した番号のステージを読み込む
        FadeCanvasClone = Instantiate(FadeCanvasPrefab);
        fadeCanvas = FadeCanvasClone.GetComponent<FadeCanvas>();
        fadeCanvas.FadeIn = true;
        yield return new WaitForSeconds(FadeWaitTime);
        yield return SceneManager.LoadSceneAsync(StageName[Num]);
        fadeCanvas.FadeOut = true;
    }

    public void RetryScene(){
        StartCoroutine(LoadScene(CurrentStageNum));//ステージ番号を変更せずに読み込む
        DeathCount++;
    }

    public void NextScene(){
        CurrentStageNum++;//次のシーンの番号に進む
        CurrentStageNum %= StageName.Length;
        StartCoroutine(LoadScene(CurrentStageNum));
    }
}
