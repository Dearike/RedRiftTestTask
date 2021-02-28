using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.TextureProvider
{
    public class PicsumTexturesProvider : ITexturesProvider
    {
        public Dictionary<CardTypeId, Texture2D> Textures { get; set; }

        public Texture2D GetTextureByTypeId(CardTypeId cardTypeId)
        {
            return Textures[cardTypeId];
        }
    }
}