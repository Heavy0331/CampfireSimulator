using UnityEngine;

public class ImageTools : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void RotateImage(Texture2D tex, float angleDegrees)
    {
        int width = tex.width;
        int height = tex.height;
        float halfHeight = height * 0.5f;
        float halfWidth = width * 0.5f;

        var texels = tex.GetRawTextureData<Color32>();
        var copy = System.Buffers.ArrayPool<Color32>.Shared.Rent(texels.Length);
        Unity.Collections.NativeArray<Color32>.Copy(texels, copy, texels.Length);

        float phi = Mathf.Deg2Rad * angleDegrees;
        float cosPhi = Mathf.Cos(phi);
        float sinPhi = Mathf.Sin(phi);

        int address = 0;
        for (int newY = 0; newY < height; newY++)
        {
            for (int newX = 0; newX < width; newX++)
            {
                float cX = newX - halfWidth;
                float cY = newY - halfHeight;
                int oldX = Mathf.RoundToInt(cosPhi * cX + sinPhi * cY + halfWidth);
                int oldY = Mathf.RoundToInt(-sinPhi * cX + cosPhi * cY + halfHeight);
                bool InsideImageBounds = (oldX > -1) & (oldX < width)
                                       & (oldY > -1) & (oldY < height);

                texels[address++] = InsideImageBounds ? copy[oldY * width + oldX] : default;
            }
        }

        // No need to reinitialize or SetPixels - data is already in-place.
        tex.Apply(true);

        System.Buffers.ArrayPool<Color32>.Shared.Return(copy);
    }
}
