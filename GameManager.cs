using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class GameManager : MonoBehaviour
    {
        public int maxHeight = 15;
        public int maxWidth = 17;

        public Color color1;
        public Color color2;
        public Color playerColor = Color.black;

        public Transform cameraHolder;

        GameObject playerObj;
        GameObject mapObject;
        SpriteRenderer mapRenderer;

        Node playerNode;
        Node[,] grid;

        bool up, left, right, down;
        bool movePlayer;

        Direction curDirection;
        public enum Direction
        {
            up,down,left,right
        }

        #region Init
        private void Start()
        {
            CreateMap();
            PlacePlayer();
            PlaceCamera();
        }
        void CreateMap()
        {
            mapObject = new GameObject("map");
            mapRenderer = mapObject.AddComponent<SpriteRenderer>();

            grid = new Node[maxWidth, maxHeight];

            Texture2D txt = new Texture2D(maxWidth, maxHeight);

            for (int i = 0; i < maxWidth; i++)
            {
                for (int j = 0; j < maxHeight; j++)
                {
                    Vector3 tp = Vector3.zero;
                    tp.x = i;
                    tp.y = j;

                    Node n = new Node()
                    {
                        x = i,
                        y = j,
                        worldPosition = tp
                    };

                    grid[i, j] = n;

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
            Sprite sprite = Sprite.Create(txt, rect, Vector2.zero, 1, 0, SpriteMeshType.FullRect);
            mapRenderer.sprite = sprite;
        }
        void PlacePlayer()
        {
            playerObj = new GameObject("Player");
            SpriteRenderer playerRender = playerObj.AddComponent<SpriteRenderer>();
            playerRender.sprite = CreateSprite(playerColor);
            playerRender.sortingOrder = 1;
            playerNode = GetNode(3, 3);
            playerObj.transform.position = playerNode.worldPosition;
        }
        void PlaceCamera()
        {
            Node n = GetNode(maxWidth / 2, maxHeight / 2);
            Vector3 p = n.worldPosition;
            p += Vector3.one * .5f;
            cameraHolder.position = p;
        }
        #endregion

        #region Update        
        private void Update()
        {
            GetInput();
            SetPlayerDirection();
            MovePlayer();
        }

        void GetInput()
        {
            up = Input.GetButtonDown("Up");
            left = Input.GetButtonDown("Left");
            right = Input.GetButtonDown("Right");
            down = Input.GetButtonDown("Down");
        }
        void SetPlayerDirection()
        {
            if (up)
            {
                curDirection = Direction.up;
                movePlayer = true;
            }
            else if (down)
            {
                curDirection = Direction.down;
                movePlayer = true;
            }
            else if (left)
            {
                curDirection = Direction.left;
                movePlayer = true;
            }
            else if (right)
            {
                curDirection = Direction.right;
                movePlayer = true;
            }
        }
        void MovePlayer()
        {
            if (!movePlayer)
                return;

            movePlayer = false;

            int x = 0;
            int y = 0;

            switch (curDirection)
            {
                case Direction.up:
                    y = 1;
                    break;
                case Direction.down:
                    y = -1;
                    break;
                case Direction.left:
                    x = -1;
                    break;
                case Direction.right:
                    x = 1;
                    break;
            }

            Node targetNode = GetNode(playerNode.x + x, playerNode.y + y);
            if (targetNode == null)
            {
                //Game Over
            }
            else
            {
                playerObj.transform.position = targetNode.worldPosition;
                playerNode = targetNode;
            }
        }
        #endregion

        #region Utilities
        Node GetNode(int x, int y)
        {
            if (x < 0 || x > maxWidth - 1 || y < 0 || y > maxHeight - 1)
                return null;

            return grid[x, y];
        }       
        Sprite CreateSprite(Color targetColor)
        {
            Texture2D txt = new Texture2D(1, 1);
            txt.SetPixel(0, 0, targetColor);
            txt.filterMode = FilterMode.Point;
            txt.Apply();
            Rect rect = new Rect(0, 0, 1, 1);
            return Sprite.Create(txt, rect, Vector2.zero, 1, 0, SpriteMeshType.FullRect);
        }
        #endregion
    }
}
