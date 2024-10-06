using System;
using UnityEngine;
using Zenject;

public class FlagCreator : MonoBehaviour
{
    private const int LeftMouseButtonID = 0;

    [SerializeField] private Flag _flag;
    [SerializeField] private LayerMask _hittableLayers;

    [Inject] private BaseCreator _baseCreator;

    private Camera _camera;
    private Flag _currentFlag;

    public Flag CurrentFlag => _currentFlag;

    private void Awake()
    {
        _camera = Camera.main;    
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButtonID))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, _hittableLayers))
            {
                if (_currentFlag == null)
                {
                    _currentFlag = Instantiate(_flag, hit.point, Quaternion.identity);
                    _currentFlag.Interacred += _baseCreator.CreateBase;
                }
                else
                {
                    _currentFlag.transform.position = hit.point;
                }

                enabled = false;
            }
        }
    }
}
