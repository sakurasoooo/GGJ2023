using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BigTree : MonoBehaviour
{
    private void Death()
    {
        
         GameManager.gameManager.Lose();
        Destroy(gameObject);
    }

    // void Load()
    // {
    //     GameManager.Lose();
    // }
}
