using UnityEngine;

public class InitializationCheck : MonoBehaviour
{
    public bool isInitialized = false;

    public void SetObjectToInitialized()
    {
        isInitialized = true;
        GameManager.Instance.UpdateInitizalizedObjects();
    }
}
