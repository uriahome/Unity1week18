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
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //基本処理
        //MouseWheelClick = false;
        MouseScroll = Input.GetAxis("Mouse ScrollWheel");//マウススクロールを取得する。手前で-0.1,奥にやると0.1くらい
        if(Input.GetMouseButtonDown(2)){
            MouseWheelClick = true;
            Debug.Log("Click");
        }
    }

    void FixedUpdate()
    {
        //物理演算に関する処理
        if(MouseWheelClick){//ホイールクリックしていたら上向きの力も加える
            MoveForce = new Vector3(MouseScroll*500,300,0);
            Debug.Log("Jump");
            MouseWheelClick = false;
        }else{
            MoveForce = new Vector3(MouseScroll*500,0,0);
        }
        rb.AddForce(MoveForce);
        Speed = rb.velocity.magnitude;
    }
}
