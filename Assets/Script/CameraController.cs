using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset = Vector3.zero;
    public Vector3 NewPosition;
    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = GameObject.Find("Player");
            offset = this.transform.position - Player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        NewPosition = this.transform.position;
        NewPosition.x = Player.transform.position.x + offset.x;
        //NewPosition.y = Player.transform.position.y + offset.y;
        //NewPosition.z = Player.transform.position.z + offset.z;
        this.transform.position = Vector3.Lerp(transform.position, NewPosition, 5.0f * Time.deltaTime);//時間をかけて元居た地点から次に行く地点に移動する
    }
}
