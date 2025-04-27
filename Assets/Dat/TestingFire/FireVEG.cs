using System;
using UnityEngine;
using UnityEngine.VFX;

public class FireVEG : MonoBehaviour
{
    public VisualEffect effect;
    public float flameRadius = 0.1f; // Bán kính ngọn lửa
    
    public FireState fireState;
    public float multiFire = 0.1f; // Tốc độ tăng kích thước lửa
    public float minFire = 0.1f; // Kích thước tối thiểu của lửa
    public float maxFire = 2.0f; // Kích thước tối đa của lửa
    

    
    // ID duy nhất cho mỗi đám cháy để theo dõi
    public int fireID;

    private void Start()
    {
        
        
        
        
        effect = gameObject.GetComponent<VisualEffect>();
        
        if (effect == null)
        {
            effect = GetComponentInChildren<VisualEffect>();
        }
        
        fireState = FireState.OnFire;
        
        // Gán ID duy nhất dựa trên thời gian và vị trí
        fireID = GetInstanceID();
    }

    private void Update()
    {
        if (fireState == FireState.OnFire)
        {
            
            flameRadius += multiFire * Time.deltaTime;
            flameRadius = Mathf.Clamp(flameRadius, minFire, maxFire);
            
            if (effect != null)
            {
                effect.SetFloat("Base_Radius_Flame", flameRadius);
            }
        }
        else if (fireState == FireState.CO2)
        {
        
            // Kiểm tra nếu lửa đã tắt hoàn toàn
            if (flameRadius <= 0)
            {
                fireState = FireState.Normal;
                
                
                // Đảm bảo hiệu ứng không hiển thị lửa
                if (effect != null)
                {
                    effect.SetFloat("Base_Radius_Flame", 0);
                }
                
                Debug.Log("Đám cháy " + fireID + " đã bị dập tắt");
            }
        }
    }
    
    // Phương thức để dập tắt lửa bằng CO2
    public void ExtinguishWithCO2()
    {
        fireState = FireState.CO2;
    }
}