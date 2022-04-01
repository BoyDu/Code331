using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private class Stone
    {
        public GameObject go;
        public Stone(GameObject go)
        { 
            this.go = go;
        }

        public Hurt GetHurt()
        { 
            return go.GetComponent<Hurt>();
        }
    }

    public float movespeed = 1.0f;
    public float rotatespeed = 1.0f;

    public PlayableAnimation playableAnimation;

    private bool isDirKeydown = false;
    private bool isAttackState = false;

    //一次攻击一共时长（击中前 + 击中 + 击中后）
    public float attackTotalTime = 1.0f;
    //击中的时间点
    public float attackHitTime = 0.5f;

    private float attackTimePassedTime = 0f;

    private Stone stone;

    // 碰撞开始
    void OnCollisionEnter(Collision collision)
    {
        /*
        var goName = collision.gameObject.name;
        Debug.Log("碰撞开始是：" + goName);
        if (goName == "stone")
        {
            stone = new Stone(collision.gameObject);
            attackTimePassedTime = 0f;
        }
        */
    }

    // 碰撞结束
    void OnCollisionExit(Collision collision)
    {
        /*
        Debug.Log("碰撞结束：" + collision.gameObject.name);
        if (stone !=null && collision.gameObject == stone.go)
        {
            //stone = null;
            //isAttackState = false ;
        }
        */
    }

    // 碰撞持续中
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log("碰撞持续中：" + collision.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        CheckRayCast();
        isDirKeydown = false;
        if (Input.GetKey(KeyCode.W))
        {
            if (stone == null)
            {
                transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
            }
            
            isDirKeydown = true;
        }

        /*
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movespeed * Time.deltaTime);
            isDirKeydown = true;
        }
        */

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

        

            /*
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
                    playableAnimation.state = PlayableAnimation.AniState.Only_Attack;
                }
            }
            */

        if (isAttackState)
        {
            /*
            if (isDirKeydown)
            {
                playableAnimation.ChangState(PlayableAnimation.AniState.Run_Attack);
                //playableAnimation.state = PlayableAnimation.AniState.Run_Attack;
                //Debug.Log("Run_Attack");
            }
            else
            {
                playableAnimation.ChangState(PlayableAnimation.AniState.Only_Attack);
                //playableAnimation.state = PlayableAnimation.AniState.Only_Attack;
                //Debug.Log("Only_Attack");
            }
            */
        }
        else
        {
            if (isDirKeydown)
            {
                playableAnimation.ChangState(PlayableAnimation.AniState.Run);
                //playableAnimation.state = PlayableAnimation.AniState.Run;
                //Debug.Log("Run");
            }
            else
            {
                playableAnimation.ChangState(PlayableAnimation.AniState.Idle);
                //playableAnimation.state = PlayableAnimation.AniState.Idle;
                //Debug.Log("Idle");
            }
        }

        UpdateHurt();
        

    }

    void CheckRayCast()
    {
        Ray ray = new Ray(transform.position + transform.up * 0.1f, transform.forward);

        float distance = 0.5f;
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            if (stone == null)
            {
                //Debug.Log("click object name is ---->" + hitInfo.collider.gameObject.name);
                stone = new Stone(hitInfo.collider.gameObject);
                attackTimePassedTime = 0;
            }
        }
        else
        { 
            stone = null;
            isAttackState = false;
        }
        Debug.DrawLine(transform.position, transform.position + transform.forward * distance, Color.red);
    }

    void UpdateHurt()
    {
        if (stone != null)
        {
            var hurt = stone.GetHurt();
            if (!hurt.IsDead())
            {
                isAttackState = true;

                if (attackTimePassedTime <= 0 || attackTimePassedTime >= attackTotalTime)
                {
                    playableAnimation.ChangState(PlayableAnimation.AniState.Empty);
                    if (isDirKeydown)
                    {
                        playableAnimation.ChangState(PlayableAnimation.AniState.Run_Attack);
                    }
                    else
                    {
                        playableAnimation.ChangState(PlayableAnimation.AniState.Only_Attack);
                    }
                }
                if (attackTimePassedTime > 0 && attackTimePassedTime >= attackTotalTime)
                {
                    attackTimePassedTime = attackTimePassedTime - attackTotalTime;
                    hurt.DoHurt();
                }
                attackTimePassedTime += Time.deltaTime;
            }
            else
            {
                stone = null;
                isAttackState = false;
            }
        }
    }
}
