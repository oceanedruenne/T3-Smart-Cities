using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonCommencer : MonoBehaviour
{
   public void BtnNextScene()
   {
       SceneManager.LoadScene("SignUp");
   }
}
