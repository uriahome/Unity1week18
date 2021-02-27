using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public bool FadeIn = false;
    public bool FadeOut = false;
    [SerializeField]
    Image PanelImage;
    float FadeSpeed = 0.02f;
    float red,green,blue,alpha;//画像の赤、緑、青、透明度

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        red = PanelImage.color.r;
        green = PanelImage.color.g;
        blue = PanelImage.color.b;
        alpha = PanelImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if(FadeIn){
            StartFadeIn();
        }else if(FadeOut){
            StartFadeOut();
        }
    }

    void StartFadeIn(){
        alpha += FadeSpeed;
        PanelImage.color = new Color(red, green, blue, alpha);
        if(alpha >= 1){
            FadeIn = false;
        }
    }

    void StartFadeOut(){
        alpha -= FadeSpeed;
        PanelImage.color = new Color(red, green, blue, alpha);
        if(alpha <= 0){
            FadeOut = false;
            Destroy(gameObject);
        }
    }
}
