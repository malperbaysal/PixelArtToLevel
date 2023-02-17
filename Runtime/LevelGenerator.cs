using UnityEditor;
using UnityEngine;

namespace Alper.PixelArtToLevelGenerator
{
    public class LevelGenerator : MonoBehaviour
    {
        //Kısa bi not Sazın içinde şeytan yok,77 üsküdar yani bu plaka yerli.
        //Bu mapi içeri alırken yapılacaklar:
        //Texture Type = Sprite (2D and UI);
        //Advanced -> Read/Write = true;
        //Filter Mode=Point(No Filter);
        //Compression=None;
        //Bu kodun altından renk seçerken Alphayı 1 yapmayı unutmayın
        //Ben https://www.pixilart.com/ kullandım İsteğe bağlı kafanıza göre takılın.
        //Startta değil de editörde görmek için OnValidate() fonksiyonunu kullanabilirsiniz.
        
        [SerializeField] private Texture2D map;
        [SerializeField] ColorToPrefab[] colorMappings;
        [SerializeField] Transform planeParent;

        void Start()
        {
            GenerateLevel();
        }
        void GenerateLevel()
        {
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = (-0.5f + (map.height / 2)) * Vector3.forward + (-0.5f + (map.width / 2)) * Vector3.right;
            plane.transform.localScale = new Vector3(map.width / 10f, 1, map.height / 10f);
            plane.transform.parent = planeParent;
            for (int i = 0; i < map.width; i++)
            {
                for (int j = 0; j < map.height; j++)
                {
                    GenerateTile(i, j);
                }
            }
        }
        void GenerateTile(int x, int y)
        {
            Color pixelColor = map.GetPixel(x, y);
            foreach (ColorToPrefab colorMapping in colorMappings)
            {
                if (pixelColor == colorMapping.color)
                {
                    Vector3 position = new Vector3(x, 0, y);
                    var InstantiatedPrefab = PrefabUtility.InstantiatePrefab(colorMapping.prefab, colorMapping.parent) as GameObject;
                    InstantiatedPrefab.transform.position = position;
                }
            }
        }
    }


}