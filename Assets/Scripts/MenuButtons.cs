using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que controla los botones del menú principal.
/// </summary>
public class MenuButtons : MonoBehaviour
{
    /// <summary>
    /// Los dos diamantes que pueden aparecer en el menú.
    /// </summary>
    [SerializeField] Image[] diamonds = null;

    private void OnEnable()
    {
        OnDeselect();
    }

    /// <summary>
    /// Función que se activa cuando se coloca el cursor sobre los textos.
    /// </summary>
    public void OnSelect()
    {
        diamonds[0].enabled = true;
        diamonds[1].enabled = true;
    }

    /// <summary>
    /// Función que se activa cuando se retira el cursor de los textos.
    /// </summary>
    public void OnDeselect()
    {
        diamonds[0].enabled = false;
        diamonds[1].enabled = false;
    }
}