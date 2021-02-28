using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.TextureProvider
{
    public interface ITexturesProvider : IService
    {
        Dictionary<CardTypeId, Texture2D> Textures { get; set; }
        Texture2D GetTextureByTypeId(CardTypeId cardTypeId);
    }
}