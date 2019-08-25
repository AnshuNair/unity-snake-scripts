using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class GameManager : MonoBehaviour
    {
        public int maxHeight = 15;
        public int maxWidth = 15;

        public Color color1;
        public Color color2;

        GameObject mapObject;
        SpriteRenderer mapRenderer;

        private void Start()
        {
            CreateMap();
        }

        void CreateMap()
        {
            mapObject = new GameObject("map");
            mapRenderer = mapObject.AddComponent<SpriteRenderer>();

            Texture2D txt = new Texture2D(maxWidth, maxHeight);

            for (int i = 0; i < maxWidth; i++)
            {
                for (int j = 0; j < maxHeight; j++)
                {
                    #region Visual
                    if (i % 2 != 0)
                    {
                        if (j % 2 != 0)
                        {
                            txt.SetPixel(i, j, color1);
                        }
                        else
                        {
                            txt.SetPixel(i, j, color2);
                        }
                    }
                    else
                    {
                        if (j % 2 != 0)
                        {
                            txt.SetPixel(i, j, color2);
                        }
                        else
                        {
                            txt.SetPixel(i, j, color1);
                        }
                    }
                    #endregion
                }
            }

            txt.filterMode = FilterMode.Point;
            txt.Apply();
            Rect rect = new Rect(0, 0, maxWidth, maxHeight);
            Sprite sprite = Sprite.Create(txt, rect, Vector2.one * .5f, 1, 0, SpriteMeshType.FullRect);
            mapRenderer.sprite = sprite;
        }
    }
}
