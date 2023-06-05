using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    //public Sprite first, fullAuto, ballzooka, gravity, crossbeaux, plasma;
    
    public ModeHandler modes;
    //private Image image;
    //public Sprite sprite;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        /*first = Resources.Load<Sprite>("UI_Sprites/StrikerVR_Sandbox_UI_First");
        fullAuto = Resources.Load<Sprite>("UI_Sprites/StrikerVR_Sandbox_UI_FullAuto");
        ballzooka = Resources.Load<Sprite>("UI_Sprites/StrikerVR_Sandbox_UI_Ballzooka");
        gravity = Resources.Load<Sprite>("UI_Sprites/StrikerVR_Sandbox_UI_Gravity");
        crossbeaux = Resources.Load<Sprite>("UI_Sprites/StrikerVR_Sandbox_UI_Crossbeaux_2");
        plasma = Resources.Load<Sprite>("UI_Sprites/StrikerVR_Sandbox_UI_Plasma");

        image = gameObject.GetComponent<Image>();*/

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //image.sprite = sprite; 

        if (modes.fireMode == 0)
        {
            //sprite = first;
            anim.SetInteger("Mode", 0); 
        }
        else if (modes.fireMode == 1)
        {
            //sprite = fullAuto;
            anim.SetInteger("Mode", 1);
        }
        else if (modes.fireMode == 2)
        {
            //sprite = ballzooka;
            anim.SetInteger("Mode", 2);
        }
        else if (modes.fireMode == 3)
        {

            //sprite = crossbeaux;
            anim.SetInteger("Mode", 3);
        }
        else if (modes.fireMode == 4)
        {
            //sprite = gravity;
            anim.SetInteger("Mode", 4);
        }
        else if (modes.fireMode == 5)
        {
            //sprite = plasma;
            anim.SetInteger("Mode", 5);
        }
    }
}
