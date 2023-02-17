using UnityEngine;

namespace Alper.PixelArtToLevelGenerator
{
    [System.Serializable]
    public class ColorToPrefab
    {
        public Color color;
        public GameObject prefab;
        public Transform parent;
    }
}