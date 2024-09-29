using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Rotacion : MonoBehaviour
{

    public ClockManager cm;
    public RoundManager gm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void rotar () {

        transform.Rotate(0, 0, 30.0f, Space.Self);

    }

    private void OnMouseDown () {

        Debug.Log("potato1");

        if (this.CompareTag("Horas")) {

            this.cm.addHoras();

        } else if (this.CompareTag("Minutos")) {

            this.cm.addMinutos();

        }

        rotar();

    }

}
