using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Services.TextureProvider.TextureLoader
{
    public class PicsumImageTestLoader : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private void Start()
        {
            StartCoroutine(GetTexture());
        }

        private IEnumerator GetTexture()
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://picsum.photos/120/90");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                SetImage(texture);
            }
        }

        private void SetImage(Texture2D texture)
        {
            _image.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);
        }
    }
}