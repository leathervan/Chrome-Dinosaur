using UnityEngine;
using UnityEngine.EventSystems;

public class UpDownButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController playerController;
    
    public void upButtonClick()
    {
        playerController.Jump();
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.Bend();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.UnBend();
    }
}
