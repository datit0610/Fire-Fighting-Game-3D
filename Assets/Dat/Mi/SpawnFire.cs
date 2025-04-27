using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnFire : MonoBehaviour
{
    public GameObject firePrefab; 
    public float spreadDelay = 5f; 
    public float checkRadius = 0.4f; 
    public LayerMask obstacleMask;
    
    //Hop nhat cac tap hop co cung kieu du lieu
    private HashSet<Vector3> burnedPositions = new HashSet<Vector3>();
    private int activeFire = 0;
    public bool playSpawn = false;
    public float timeSpread = 3f, timer;
    public void StartFire(Vector3 position)
    {
        
        // if (activeFire == 0)
        // {
        //     burnedPositions.Clear();
        // }
        // else
        {
            burnedPositions.Add(position);
            //StartCoroutine(SpreadFire(position));
            Test(position);
        }
    }
    
    private IEnumerator SpreadFire(Vector3 startPosition)
    {
        Queue<Vector3> fireQueue = new Queue<Vector3>();
        fireQueue.Enqueue(startPosition);

        while (fireQueue.Count > 0 && playSpawn == true)
        {
            Vector3 currentPos = fireQueue.Dequeue();

            // Lấy lửa từ pool và đặt vào vị trí hiện tại
            GameObject fire = Pooling_Mi.Instance.GetObjectFromPool(currentPos, Quaternion.identity);
            activeFire++;
            Debug.Log("Lua dang hoat dong" + activeFire);
            yield return null;
            //yield return new WaitForSeconds(spreadDelay);
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= timeSpread)
            {
                 Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
                
                            foreach (Vector3 dir in directions)
                            {
                                Vector3 newPos = currentPos + dir;
                
                                if (!burnedPositions.Contains(newPos) && !Physics.CheckSphere(newPos, checkRadius, obstacleMask))
                                {
                                    burnedPositions.Add(newPos);
                                    fireQueue.Enqueue(newPos);
                                }
                            }
            }


            

            // Sau 5 giây, trả lửa về pool
            //StartCoroutine(ReturnFireToPool(fire, 10f));
        }
    }

    private IEnumerator ReturnFireToPool(GameObject fire, float delay)
    {
        yield return new WaitForSeconds(delay);
        Pooling_Mi.Instance.AddObjectToPool(fire);
        activeFire--;
        //burnedPositions.Remove();
        if (activeFire == 0)
        {
            Debug.Log("Lửa đã được dập cháy hoàn toàn!");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeSpread)
        {
            timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            playSpawn = true;
            StartFire(transform.position);
            Debug.Log("On Spawn fire");
            
        }
        
        
        
    }

    public void Test(Vector3 startPosition)
    {
        Queue<Vector3> fireQueue = new Queue<Vector3>();
        fireQueue.Enqueue(startPosition);
        
        while (fireQueue.Count > 0 && playSpawn == true)
        {
            Vector3 currentPos = fireQueue.Dequeue();

            // Lấy lửa từ pool và đặt vào vị trí hiện tại
            GameObject fire = Pooling_Mi.Instance.GetObjectFromPool(currentPos, Quaternion.identity);
            activeFire++;
            Debug.Log("Lua dang hoat dong" + activeFire);

            //yield return new WaitForSeconds(spreadDelay);
            
            
            if (timer%timeSpread == 0)
            {
                Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

                foreach (Vector3 dir in directions)
                {
                    Vector3 newPos = currentPos + dir;

                    if (!burnedPositions.Contains(newPos) && !Physics.CheckSphere(newPos, checkRadius, obstacleMask))
                    {
                        burnedPositions.Add(newPos);
                        fireQueue.Enqueue(newPos);
                    }
                }
            }
        }
    }
}
