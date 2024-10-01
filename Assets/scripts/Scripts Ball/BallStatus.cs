using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallStatus : MonoBehaviour
{

    public bool alive = true;
    public bool winner = false;

    private void OnCollisionEnter (Collision colission) {

        if (colission.gameObject.CompareTag("killer")) {

            SceneManager.LoadScene("Level");
            this.alive = false;

        }

        if (colission.gameObject.CompareTag("winner")) {

            SceneManager.LoadScene("Level");
            this.winner = true;

        }

    }

}
