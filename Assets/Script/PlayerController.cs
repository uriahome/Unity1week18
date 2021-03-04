using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update]
    public float MouseScroll;
    public Rigidbody2D rb;
    public Vector3 MoveForce;
    public float Speed;
    public bool MouseWheelClick;
    public bool Death;
    public bool Jump;//ジャンプしているかどうか

    //効果音の設定
    public AudioSource audio;
    public AudioClip JumpSE;

    //GameManager GM;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Death = false;
        audio = GetComponent<AudioSource>();
        audio.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //基本処理
        //MouseWheelClick = false;
        MouseScroll = Input.GetAxis("Mouse ScrollWheel");//マウススクロールを取得する。手前で-0.1,奥にやると0.1くらい
        if(Input.GetMouseButtonDown(2) && !Jump){
            Jump = true;
            MouseWheelClick = true;
            Debug.Log("Click");
            audio.PlayOneShot(JumpSE);//効果音の再生
        }

        if(this.transform.position.y <= -3.0 && !Death){
            Death = true;
            PlayerDeath();
        }
    }

    void FixedUpdate()
    {
        //物理演算に関する処理
        if(MouseWheelClick){//ホイールクリックしていたら上向きの力も加える
            MoveForce = new Vector3(MouseScroll*300,400,0);
            Debug.Log("Jump");
            MouseWheelClick = false;
        }else{
            MoveForce = new Vector3(MouseScroll*300,0,0);
        }
        rb.AddForce(MoveForce);
        Speed = rb.velocity.magnitude;
    }

    void PlayerDeath(){
        GameManager.instance.RetryScene();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Mushroom":
                GameManager.instance.NextScene();//次のシーンを読み込む
                break;
            case "Ground":
                Jump = false;
                break;
            default:
            break;
        }
    }
}
