using UnityEngine;

public class ImageSetter : MonoBehaviour {
    public Renderer targetRenderer;
    public SpriteRenderer spriteRenderer; // Для отображения спрайта

    void Start() {
        if (ImageConfig.selectedImage != null && targetRenderer != null && spriteRenderer != null) {
            Material material = targetRenderer.material;
            material.mainTexture = ImageConfig.selectedImage;
            targetRenderer.material = material;

            // Установка спрайта в spriteRenderer
            Texture2D texture = ImageConfig.selectedImage as Texture2D;
            if (texture) {
                float pixelsPerUnit = 100; // Значение Pixels Per Unit для спрайта
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
                spriteRenderer.sprite = sprite;

                // Установка масштаба объекта
                spriteRenderer.transform.localScale = new Vector3(145.6636f, 145.6636f, 145.6636f);
            }
        }
    }
}