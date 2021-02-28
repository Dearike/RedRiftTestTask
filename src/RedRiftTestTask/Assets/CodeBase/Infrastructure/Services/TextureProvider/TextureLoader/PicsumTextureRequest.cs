using System;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Infrastructure.Services.TextureProvider.TextureLoader
{
    public class PicsumTextureRequest
    {
        private const string Url = "https://picsum.photos";

        private string _uri;

        private UnityWebRequest _request;
        private UnityWebRequestAsyncOperation _asyncOperation;

        private Action<Texture2D> _responseAction;

        public Texture2D Texture { get; private set; }

        public PicsumTextureRequest(int width, int height)
        {
            _uri = $"{Url}/{width}/{height}";
        }

        public AsyncOperation Run(Action<Texture2D> responseAction = null)
        {
            _responseAction = responseAction;

            _request = UnityWebRequestTexture.GetTexture(_uri);

            _asyncOperation = _request.SendWebRequest();
            _asyncOperation.completed += AsyncOperationOnCompleted;

            return _asyncOperation;
        }

        private void AsyncOperationOnCompleted(AsyncOperation _)
        {
            if (_request.isHttpError)
                HandleHttpError();

            else if (_request.isNetworkError)
                HandleNetworkError();

            else
                Response();
        }

        private void Response()
        {
            Texture = ((DownloadHandlerTexture)_request.downloadHandler).texture;

            //ClearRequestData();

            _responseAction?.Invoke(Texture);
        }

        private void ClearRequestData()
        {
            _request?.Dispose();

            if (_asyncOperation != null)
            {
                _asyncOperation.completed -= AsyncOperationOnCompleted;
                _asyncOperation = null;
            }

            _request = null;
        }

        private void HandleNetworkError()
        {
            throw new NotImplementedException();
        }

        private void HandleHttpError()
        {
            throw new NotImplementedException();
        }
    }
}
