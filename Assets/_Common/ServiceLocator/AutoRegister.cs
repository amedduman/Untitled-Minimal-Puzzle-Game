using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class AutoRegister : MonoBehaviour
{
    [SerializeField] bool _isSingleton;

    [SerializeField] Component _cmp;
    
    [ShowIf(nameof(_isSingleton), false)]
    [SearchableEnum]
    [SerializeField] SerLocID _id;
    
    void Awake()
    {
        if (_cmp == null && _id == SerLocID.none && _isSingleton == false)
        {
            Debug.LogWarning($"You added the {nameof(AutoRegister)} component on {gameObject.name} but fields need to be filled. <color=green>Click to ping the game object that produce warning</color>", gameObject);
            return;
        }
        if (_cmp == null && _id != SerLocID.none && _isSingleton == false)
        {
            Debug.Break();
            Debug.LogError($"Component field for {nameof(AutoRegister)} cannot be null. Please fill out. <color=green>Click to ping the game object that produce error</color>", gameObject);
        }
        if (_cmp == null && _isSingleton == true)
        {
            Debug.Break();
            Debug.LogError($"Component field for {nameof(AutoRegister)} cannot be null. Please fill out. <color=green>Click to ping the game object that produce error</color>", gameObject);
        }
        
        if (_isSingleton)
        {
            ServiceLocator.Register(_cmp); 
        }
        else
        {
            if (_id == SerLocID.none)
            {
                Debug.Break();
                Debug.LogError($"You need to specify a valid id for {_cmp.gameObject.name}. <color=green>Click to ping the game object that produce error.</color>", _cmp.gameObject);
            }
            ServiceLocator.Register(_cmp, _id); 
        }
    }
}
