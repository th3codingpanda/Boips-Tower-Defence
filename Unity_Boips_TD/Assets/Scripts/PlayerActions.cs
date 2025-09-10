using UnityEngine;


public class PlayerActions : MonoBehaviour
{
    private InputPackageManagerScript _inputScript; 
    [SerializeField]private GameObject gridDisplay;
    void Start()
    {
        _inputScript = InputPackageManagerScript.Instance;
        _inputScript.BuildMenuEvent.AddListener(ToggleBuildMode);
    }

    private void ToggleBuildMode()
    {
        gridDisplay.SetActive(!gridDisplay.activeSelf);
    }
}
