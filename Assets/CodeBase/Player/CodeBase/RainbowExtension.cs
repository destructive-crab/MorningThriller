using destructive_code.ExtensionSystem;
using UnityEngine;

namespace destructive_code.PlayerCodeBase
{
    [DisallowMultipleExtensions]
    public sealed class RainbowExtension : Extension
    {
        private SpriteRenderer spriteRenderer;

        public override void StartExtension()
        {
            spriteRenderer = Owner.CachedComponents.Get<SpriteRenderer>();
        }

        public override void Update()
        {
            spriteRenderer.color 
                = new Color(Random.Range(0, 100) / (float)100, Random.Range(0, 100) / (float)100, Random.Range(0, 100) / (float)100, 1);
        }

        public override void Dispose()
        {
            spriteRenderer.color = Color.white;
        }
    }
}