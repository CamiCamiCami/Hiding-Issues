using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallStatus : MonoBehaviour
{

    public bool alive = true;
    public bool winner = false;

    void Start()
    {
        
    }

    void Update()
    {
        
        //Debug.Log(this.alive + " " + this.winner);

    }

    private void OnCollisionEnter (Collision colission) {

        if (colission.gameObject.CompareTag("killer")) {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
            this.alive = false;

        }

        if (colission.gameObject.CompareTag("winner")) {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
            this.winner = true;

        }

    }

}
