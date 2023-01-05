using UnityEngine;
using UnityEngine.UI;

public class TurnPointTutorialController : MonoBehaviour
{
    [SerializeField] private Transform _tutorialScreen;
    [SerializeField] private Button _closeBtn;
    
    private Player _player;
    private bool _tutShown = false;

    private void OnEnable()
    {
        _closeBtn.onClick.AddListener(CloseTutorialBehaviour);
    }

    private void OnDisable()
    {
        _closeBtn.onClick.RemoveAllListeners();
    }

    void Start()
    {
        _player = ServiceLocator.Get<Player>();
        _tutorialScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        Tile tile = _player.GetCurrentTilePlayerOn();
        if (tile.gameObject.TryGetComponent(out TurnPoint tp))
        {
            if (_tutShown == false)
            {
                _tutShown = true;
                ServiceLocator.Get<GameController>().ManipulateTime(0);
                _tutorialScreen.gameObject.SetActive(true);
            }
        }
        else
        {
            _tutShown = false;
        }
    }

    void CloseTutorialBehaviour()
    {
        ServiceLocator.Get<GameController>().ManipulateTime(1);
        _tutorialScreen.gameObject.SetActive(false);

    }
}
