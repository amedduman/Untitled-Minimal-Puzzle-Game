namespace amed.utils.serviceLoc
{
    using UnityEngine;

    [DefaultExecutionOrder(-10000)]
    public class AutoRegister : MonoBehaviour
    {
        [SerializeField] Component _cmp;

        void Awake()
        {
            ServiceLocator.Instance.Register(_cmp);
        }
    }
}