using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonValider : MonoBehaviour
{
   public void BtnNextScene()
   {  
    SceneManager.LoadScene("GameScene");
   }
}
