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

        private void Start()
        {
            
        }

        void CreateMap()
        {
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
        }
    }
}
