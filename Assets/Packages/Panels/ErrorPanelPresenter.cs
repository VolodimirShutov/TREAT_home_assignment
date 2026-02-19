using UnityEngine;
using UnityEngine.UI;

namespace Packages.Panels
{
    public class ErrorPanelPresenter: MonoBehaviour
    {
        
        [SerializeField] private Button okButton;

        private void Awake()
        {
            okButton.onClick.AddListener(OnOkClicked);
        }
        
        private void OnOkClicked()
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}