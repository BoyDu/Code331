using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public float movespeed = 1.0f;
    public float rotatespeed = 1.0f;

    public PlayableAnimation playableAnimation;

    private bool isDirKeydown = false;

    // Update is called once per frame
    void Update()
    {
        isDirKeydown = false;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
            isDirKeydown = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movespeed * Time.deltaTime);
            isDirKeydown = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-1 * Vector3.up * rotatespeed * Time.deltaTime);
            isDirKeydown = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotatespeed * Time.deltaTime);
            isDirKeydown = true;
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (isDirKeydown)
            {
                playableAnimation.state = PlayableAnimation.AniState.Run_Attack;
            }
            else
            {
                playableAnimation.state = PlayableAnimation.AniState.Only_Attack;
            }
        }
        else
        {
            if (isDirKeydown)
            {
                playableAnimation.state = PlayableAnimation.AniState.Run;
            }
            else
            {
                playableAnimation.state = PlayableAnimation.AniState.Idle;
            }
        }

    }
}
