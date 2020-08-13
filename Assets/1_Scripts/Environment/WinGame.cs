using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Interaction.bombRigged)
        {
            GameWin();
        }
    }


    void GameWin()
    {
        SceneManager.LoadScene(3);
    }
}
