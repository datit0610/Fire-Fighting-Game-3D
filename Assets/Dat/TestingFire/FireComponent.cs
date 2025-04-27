using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;

public enum FireState
{
    Normal,
    OnFire,
    BurntDown,
    CO2,
    O_Dien
    
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HeatSource))]
public class FireComponent : MonoBehaviour
{
    public GameObject firePrefab;
    public FireSetting fireSettings;
    public int gridSizeX;

    public FireState _state;
 
    private Renderer _meshRenderer;
    private SpatialGrid _grid;
    private List<FireCell> _burningCells;
    private Dictionary<Vector3, GameObject> _flames;
    private HeatSource _heatSource;
    private bool _hasStartedFire = false;
    private bool _hasStopFire = false;
    public float flameRadius = 0.1f; // Bán kính ngọn lửa
    public float radiusIncreaseRate = 0.1f; // Tốc độ tăng bán kính
    private FireVEG _fireVEG;
   

    public bool HasStartedFire
    {
        get { return _hasStartedFire; }
    }
    
    public bool HasStopFire
    {
        get { return _hasStopFire; }
    }

    public FireState State
    {
        get { return _state; }
    }

    
    private void Start()
    {

        _state = FireState.Normal;
        _heatSource = GetComponent<HeatSource>();
        _heatSource.enabled = false;
        _meshRenderer = GetComponent<Renderer>();
        _burningCells = new List<FireCell>();
        _fireVEG = GameObject.FindObjectOfType<FireVEG>();
       // _fireVEG = GetComponentInChildren<FireVEG>();
        
    }

    private void Update()
    {
        if (_grid != null)
        {
            foreach (var cell in _grid.gridCells)
            {
                if (_burningCells.Contains(cell.Value))
                {
                    if (cell.Value.state == FireState.Normal && cell.Value.HasReachedBurningTemperature())
                    {
                        var fireobj = Instantiate(firePrefab, cell.Value.position, Quaternion.identity);
                        _flames.Add(cell.Value.position,fireobj);
                        _burningCells.AddRange(GetCellNeighbours(cell.Value));
                        _state = FireState.OnFire;
                        _heatSource.enabled = true;
                        break;
                    }else if (!cell.Value.IsStillBurning(Time.deltaTime) )
                    {
                        Destroy(_flames[cell.Key]);
                        _flames.Remove(cell.Key);
                        _burningCells.Remove(cell.Value);
                    }
                }
            }
        }

        if (_burningCells.Count == 0 && _state == FireState.OnFire)
        {
            _hasStartedFire = false;
            _state = FireState.BurntDown;
        }
    }
    

    public void StartFire(Vector3 heatSourcePosition)
    {
        if (_state == FireState.Normal)
        {
            _state = FireState.OnFire;
            _hasStartedFire = true;
            _flames = new Dictionary<Vector3, GameObject>();
            FindAllCells();
            FireCell nearesCell = FindNearesCell(heatSourcePosition);
            
            _burningCells.Add(nearesCell);
            
            
        }
    }

    

    

    private void FindAllCells()
    {
        _grid = new SpatialGrid();
        _grid.radius = _meshRenderer.bounds.extents.x/gridSizeX;
        _grid.radius /= 2;

        float multiplier = gridSizeX % 2 == 0 ? 1.5f : 2;
        
        float startPointX = _grid.radius*multiplier*(gridSizeX/2);
        float startPointY = ((_meshRenderer.bounds.size.y/(_grid.radius*2))/2)*multiplier*_grid.radius;
        float startPointZ = ((_meshRenderer.bounds.size.z/(_grid.radius*2))/2)*multiplier*_grid.radius;
        float interval = _grid.radius * 2;
        for (float x = -startPointX; x <= startPointX; x += interval)
        {
            for (float y = -startPointY; y <= startPointY; y += interval)
            {
                for (float z = -startPointZ; z <= startPointZ; z += interval)
                {
                    Vector3 position = new Vector3(x,y,z) + _meshRenderer.bounds.center;
                    Collider[] colliders = Physics.OverlapSphere(position, _grid.radius);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject == gameObject)
                        {
                            AddCellGrid(position);
                        }
                    }
                }
            }
        }
    }

    public void AddCellGrid(Vector3 position)
    {
        FireCell cell = new FireCell();
        if (cell != null)
        {
            cell.radius = _grid.radius;
            cell.position = position;
            cell.settings = fireSettings;
            if (!_grid.gridCells.ContainsKey(position))
            {
                _grid.gridCells.Add(position, cell);
            }
        }
    }
    
    private FireCell FindNearesCell(Vector3 heatSourcePosition)
    {
        FireCell nearsCell = null;
        float nearstDistance = 0;
        foreach (var cell in _grid.gridCells)
        {
            if (nearsCell == null)
            {
                nearsCell = cell.Value;
                nearstDistance = Vector3.Distance(cell.Key, heatSourcePosition);
            }
            else
            {
                if (Vector3.Distance(cell.Key, heatSourcePosition) < nearstDistance)
                {
                    nearsCell = cell.Value;
                    nearstDistance = Vector3.Distance(cell.Key, heatSourcePosition);
                }
            }
        }
        return nearsCell;
    }

    public List<FireCell> GetCellNeighbours(FireCell cell)
    {
        List<FireCell> neighbours = new List<FireCell>();
        float epsilon = 0.001f; //Nguong Sai So De ss Vector3

        for (int i = -1; i <= 1; i += 2)
        {
            Vector3[] neighbourPositions =
            {
                cell.position + new Vector3(i*_grid.radius*2,0,0),
                cell.position + new Vector3(0, i*_grid.radius*2, 0),
                cell.position + new Vector3(0, 0, i*_grid.radius*2)
            };

            //duyet qua cac vi tri lan can
            foreach (var neighbourPosition in neighbourPositions)
            {
                //tim o lan can trong luoi
                if (_grid.gridCells.TryGetValue(neighbourPosition, out FireCell neighbourCell) &&
                    !_burningCells.Contains(neighbourCell))
                {
                    neighbours.Add(neighbourCell);
                }
            }
        }
        return neighbours;
    }

    
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (_grid != null)
        {
            foreach (var cell in _grid.gridCells)
            {
                Gizmos.DrawWireSphere(cell.Key,cell.Value.radius);
            }
        }
    }
}