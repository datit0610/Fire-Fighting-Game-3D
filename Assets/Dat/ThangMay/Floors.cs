using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Floors : MonoBehaviour
{
    public bool canOpen = true;
    public Transform[] points;
    public GameObject model;
    public Animator animator1;
    public Animator animator2;
    public float speed = 1f;
    public bool[] floorbools = new bool[6];

    //private bool open = true;

    private void Awake()
    {
        floorbools = new bool[6] { false, false, false, false, false, false };
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < floorbools.Length; i++)
        {
            if (floorbools[i])
            {
                MoveToFloor(i);
            }
        }

    }

    private void MoveToFloor(int index)
    {
        canOpen = false;
        model.transform.position = Vector3.MoveTowards(model.transform.position, points[index].position, speed * Time.deltaTime);

        if (Vector3.Distance(model.transform.position, points[index].position) < 0.001f)
        {
            floorbools[index] = false;
            AudioManager.instance.PlayFX("Elevator");
            Debug.Log($"Đã đến tầng {index + 1}");
            canOpen = true;
            StartCoroutine(WaitAndAnimate());
        }
    }

    public void floor1(int index)
    {
        floorbools[index] = true;
        StartCoroutine(WaitAndAnimate());
    }

    private IEnumerator WaitAndAnimate()
    {
        if (canOpen)
        {
            yield return new WaitForSeconds(1);
            animator1.SetBool("isOpen", true);
            animator2.SetBool("isOpen", true);

            yield return new WaitForSeconds(3);
            animator1.SetBool("isOpen", false);
            animator2.SetBool("isOpen", false);
        }
    }
}