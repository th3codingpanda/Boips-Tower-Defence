using UnityEngine;

public class PlayerMove : MonoBehaviour

{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _tower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, _mask))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");

            }
        }
    }
}
