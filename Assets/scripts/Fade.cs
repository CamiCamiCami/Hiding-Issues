using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Fade : MonoBehaviour
{

    public Color FadeColor = new Color(0f, 0f, 0f, 0f);
    public float FadeSpeed = 0.05f;

    private UnityEngine.UI.Image fade;
    private bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        // Set RectTransform to fullscreen
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;

        fade = this.AddComponent<UnityEngine.UI.Image>();
        fade.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, 0f);
    }

    private void FixedUpdate()
    {
        if (fading)
        {
            float new_alpha = fade.color.a + FadeSpeed >= 1f ? 1f : fade.color.a + FadeSpeed;
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, new_alpha);
            fading = new_alpha != 1f;
        }
    }

    public void StartFade()
    {
        fading = true;
    }

    public void ResetFade()
    {
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0);
        fading = false;
    }
}

