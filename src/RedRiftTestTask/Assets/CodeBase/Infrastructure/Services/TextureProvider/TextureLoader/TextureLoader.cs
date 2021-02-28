using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Extensions;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.TextureProvider.TextureLoader
{
    public class TextureLoader
    {
        private const int ImageWidth = 180;
        private const int ImageHeight = 120;

        private readonly Guid TextureLoadingGroupId =
            new Guid("2884CC8C-7CDF-41AF-84AE-A10E60DF6BDB");

        private readonly ICoroutineRunner _coroutineRunner;

        public Dictionary<CardTypeId, Texture2D> Textures { get; private set; } =
            new Dictionary<CardTypeId, Texture2D>();

        public TextureLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(
                LoadTextures(onLoaded));
        }

        private IEnumerator LoadTextures(Action onLoaded = null)
        {
            foreach (CardTypeId cardTypeId in Enum.GetValues(typeof(CardTypeId)))
            {
                LoadTexture(cardTypeId)
                    .ParallelCoroutinesGroup(TextureLoadingGroupId, _coroutineRunner);
            }

            while (CoroutineExtension.IsGroupProcessing(TextureLoadingGroupId))
                yield return null;


            onLoaded?.Invoke();
        }

        private IEnumerator LoadTexture(CardTypeId cardTypeId)
        {
            var textureRequest = new PicsumTextureRequest(ImageWidth, ImageHeight);
            AsyncOperation waitTexture = textureRequest.Run();

            while (!waitTexture.isDone)
                yield return null;

            yield return new WaitForSeconds(1f);

            Textures.Add(cardTypeId, textureRequest.Texture);
        }
    }
}