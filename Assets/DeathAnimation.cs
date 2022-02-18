using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DeathAnimation : MonoBehaviour
{
    public Animator anim;

    
    public GameOver gameover;


    public void trigger()
    {
        if (gameObject.name == "Enemy Crystal")
        {
            gameover.endGame(1);
        }
        
        if(gameObject.name == "Player Crystal")
        {
            gameover.endGame(0);
        }

    }

    
}
