using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public RenderTexture[] TextureList;
    public RawImage[] ImageList;
    int count;
    Camera MyCamera;
    void Start()
    {
        MyCamera = GetComponent<Camera>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        MyCamera.targetTexture = TextureList[count % TextureList.Length];
        for (int i=0; i < ImageList.Length; i++){
            int cal = (count % TextureList.Length + i)% ImageList.Length;
            ImageList[i].texture = TextureList[cal];
        }
    }
}
