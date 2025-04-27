using System;
using UnityEngine;

public class HeatSource : MonoBehaviour {
    public float heatRange; // Phạm vi nhiệt
    private FireVEG _fireVEG;
    private FireComponent fireComponent;
    public FireState state;
    public float multiRage=0.05f;
    private Chay_ODien _chayODien;
    
    // Flag to track if CO2 is currently affecting the fire
    private bool isInContactWithCO2 = false;
    
    private void Start()
    {
        _fireVEG = GameObject.FindObjectOfType<FireVEG>();
        _chayODien = GameObject.FindObjectOfType<Chay_ODien>();
        
        if (_fireVEG == null)
        {
            Debug.LogError("Không tìm thấy FireVEG trong các GameObject con!");
        }
    }
    
    private void Update()
    {
        if (_fireVEG == null)
            return;
        
        if (_fireVEG.fireState == FireState.OnFire)
        {
            heatRange += multiRage * Time.deltaTime;
        }
        
        // Reset CO2 contact flag at the start of each frame
        isInContactWithCO2 = false;
        
        // Kiểm tra các vật thể trong phạm vi nhiệt
        Collider[] colliders = Physics.OverlapSphere(transform.position, heatRange);
        foreach (Collider collider in colliders)
        {
            fireComponent = collider.GetComponent<FireComponent>();
            if (fireComponent != null && !fireComponent.HasStartedFire)
            {
                // Kích hoạt ngọn lửa
                fireComponent.StartFire(collider.transform.position);
            }
            
            if (collider.tag == "CO2")
            {
                isInContactWithCO2 = true;
                _fireVEG.fireState = FireState.CO2;
                Debug.Log("Trạng thái lửa trong HeatSource: CO2");
                Debug.Log("Trạng thái lửa trong FireVEG: " + _fireVEG.fireState);
                
                // Chỉ giảm lửa khi đang tiếp xúc với CO2
                heatRange -= 0.09f;
                _fireVEG.flameRadius -= 0.09f;
                _fireVEG.flameRadius = Mathf.Clamp(_fireVEG.flameRadius, _fireVEG.minFire, _fireVEG.maxFire);
                _fireVEG.effect.SetFloat("Base_Radius_Flame", _fireVEG.flameRadius);
            }
            
            if (collider.tag == "O_Dien")
            {
                if (_chayODien != null)
                {
                    Debug.Log(collider.name);
                    _chayODien.chayODien();
                }
            }
            else if(collider.tag == "Fire" && _fireVEG.fireState != FireState.CO2)
            {
                _fireVEG.fireState = FireState.OnFire;
            }
        }
        
        // Nếu không còn tiếp xúc với CO2 nhưng trạng thái vẫn là CO2, đặt lại trạng thái
        if (!isInContactWithCO2 && _fireVEG.fireState == FireState.CO2)
        {
            _fireVEG.fireState = FireState.OnFire;
        }
        
        // Cập nhật trạng thái của nguồn nhiệt khi lửa bị dập tắt
        if (_fireVEG.flameRadius <= 0.01f && _fireVEG.fireState == FireState.CO2)
        {
            heatRange = 0;
            _fireVEG.fireState = FireState.Normal;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "CO2")
            Debug.Log("ok");
    }

    private void OnDrawGizmos()
    {
        if (isActiveAndEnabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, heatRange);
        }
    }
}