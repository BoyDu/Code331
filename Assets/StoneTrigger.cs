using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("进入该触发器的对象是：" + other.gameObject.name);
    }
    void OnTriggerStay(Collider other)    //每帧调用一次OnTriggerStay()函数
    {
        //Debug.Log("留在触发器的对象是：" + other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("离开触发器的对象是：" + other.gameObject.name);
    }

    // 碰撞开始
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("碰撞开始是：" + collision.gameObject.name);
    }

    // 碰撞结束
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("碰撞结束：" + collision.gameObject.name);
    }

    // 碰撞持续中
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("碰撞持续中：" + collision.gameObject.name);
    }

}
