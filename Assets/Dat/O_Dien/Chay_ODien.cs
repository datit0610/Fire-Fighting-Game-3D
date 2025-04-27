using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Chay_ODien : MonoBehaviour
{
    public List<Material> materialODien;
    
    
    private void Start()
    {
        // Only initialize if the list is null
        if (materialODien == null)
        {
            materialODien = new List<Material>();
        }

        FindAndAddAllMaterials();
        // Optional: Debug to check if list has items
        Debug.Log("List count: " + materialODien.Count);
        
    }

    
    
    public void chayODien()
    {
        if ( materialODien != null)
        {
            foreach (Material material in materialODien)
            {
                if (material != null)
                {
                    // Đối với Universal Render Pipeline materials
                    material.SetColor("_BaseColor", Color.black);
                    Debug.Log("Đã thay đổi material: " + material.name);
                }
            }
        }
    }
    public void FindAndAddAllMaterials()
    {
        if (materialODien == null)
        {
            materialODien = new List<Material>();
        }
    
        
        materialODien.Clear();
    
        
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
    
        foreach (Renderer renderer in renderers)
        {
            // Add each material to the list
            foreach (Material mat in renderer.materials)
            {
                if (!materialODien.Contains(mat))
                {
                    materialODien.Add(mat);
                    Debug.Log("Added material: " + mat.name);
                }
            }
        }
    
        Debug.Log("Total materials found: " + materialODien.Count);
    }
}