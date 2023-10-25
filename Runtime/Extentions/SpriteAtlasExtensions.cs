using UnityEngine;
using UnityEngine.U2D;

namespace CommonUtility.Extensions
{
    public static class SpriteAtlasExtensions
    {
        private static readonly Sprite[] _sprites = new Sprite[1];

        public static Texture GetTexture(this SpriteAtlas spriteAtlas)
        {
            if (spriteAtlas.spriteCount == 0)
                return null;

            _sprites[0] = null;
            spriteAtlas.GetSprites(_sprites);
            return _sprites[0].texture;
        }
    }
}