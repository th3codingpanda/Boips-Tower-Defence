using UnityEngine;
using UnityEngine.Rendering;


public class PlayerActions : MonoBehaviour
{
    private InputPackageManagerScript _inputScript;
    [SerializeField] private GameObject gridDisplay;
    [SerializeField] private GameObject towerPrefab;
    void Start()
    {
        _inputScript = InputPackageManagerScript.Instance;
        _inputScript.BuildMenuEvent.AddListener(ToggleBuildMode);
        _inputScript.PlaceTowerEvent.AddListener(PlaceTower);
    }

    void Update()
    {

    }
    private void ToggleBuildMode()
    {
        gridDisplay.SetActive(!gridDisplay.activeSelf);
    }

    private void PlaceTower()
    {
        Debug.Log("Placing tower");
        Instantiate(towerPrefab, gridDisplay.transform.position, Quaternion.identity);

    }
}
