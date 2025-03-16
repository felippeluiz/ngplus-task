using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleImage : MonoBehaviour
{
    [SerializeField] private Sprite _spriteStart, _spriteToChange;
    [SerializeField] private Image _image;
    private bool _on=true;

    public void OnPressButton()
    {
        _on = !_on;
        _image.sprite = _on ? _spriteStart : _spriteToChange;
    }
}
