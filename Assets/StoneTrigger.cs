using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("����ô������Ķ����ǣ�" + other.gameObject.name);
    }
    void OnTriggerStay(Collider other)    //ÿ֡����һ��OnTriggerStay()����
    {
        //Debug.Log("���ڴ������Ķ����ǣ�" + other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("�뿪�������Ķ����ǣ�" + other.gameObject.name);
    }

    // ��ײ��ʼ
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("��ײ��ʼ�ǣ�" + collision.gameObject.name);
    }

    // ��ײ����
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("��ײ������" + collision.gameObject.name);
    }

    // ��ײ������
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("��ײ�����У�" + collision.gameObject.name);
    }

}
