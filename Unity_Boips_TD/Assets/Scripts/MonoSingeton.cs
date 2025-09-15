using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType<T>();
            } return _instance;
        }
        private set { 
            _instance = value; 
        }
    }
}