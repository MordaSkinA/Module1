using UnityEngine;

public class NPCIdleController : MonoBehaviour, IPossessable
{
    [SerializeField] private Renderer bodyRenderer;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color inactiveColor = Color.red;

    public void OnPossess()
    {
        bodyRenderer.material.color = activeColor;
    }

    public void OnUnpossess()
    {
        bodyRenderer.material.color = inactiveColor;
    }
    private void Awake()
    {
        bodyRenderer.material.color = inactiveColor;
    }
}