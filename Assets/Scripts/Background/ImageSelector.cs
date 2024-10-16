using UnityEngine;

public class ImageSelector : MonoBehaviour {
    public Texture2D image1;
    public Texture2D image2;
    public Texture2D image3;

    public void SelectImage(int imageIndex) {
        switch(imageIndex) {
            case 1:
                ImageConfig.selectedImage = image1;
                break;
            case 2:
                ImageConfig.selectedImage = image2;
                break;
            case 3:
                ImageConfig.selectedImage = image3;
                break;
            default:
                Debug.LogError("Invalid image index!");
                break;
        }
    }
}