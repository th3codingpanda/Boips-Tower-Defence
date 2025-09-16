using UnityEngine;


public class PlayerActions : MonoBehaviour
{
    private InputPackageManagerScript _inputScript; 
    [SerializeField]private GameObject gridDisplay,gridIndicator;
    GridHandler _gridHandler;
    
    void Start()
    {
        _gridHandler = GridHandler.Instance;
        _inputScript = InputPackageManagerScript.Instance;
        _inputScript.BuildMenuEvent.AddListener(ToggleBuildMode);
        _inputScript.PlaceTowerEvent.AddListener(PlaceTower);
    }

    void Update()
    {
        if (gridDisplay.activeSelf)
        {

        }
    }

    private void ToggleBuildMode()
    {
        gridDisplay.SetActive(!gridDisplay.activeSelf);
        gridIndicator.SetActive(!gridIndicator.activeSelf);
        
    }

    private void PlaceTower()
    {
        
    }

}
