using System;
using destructive_code.Level;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace rainy_morning
{
    public class RoomBuilder : EditorWindow
    {
        private SerializedProperty roomProp;

        private RoomBase room;
        private Transform grid;

        private Tilemap leftPassageTilemap;
        private Tilemap rightPassageTilemap;
        private Tilemap topPassageTilemap;
        private Tilemap bottomPassageTilemap;

        [MenuItem("My Tools/Room Builder")]
        private static void ShowWindow()
        {
            GetWindow(typeof(RoomBuilder));
        }

        private void OnGUI()
        {
            if (Selection.activeObject != null && Selection.activeObject is GameObject gameObject &&
                gameObject.TryGetComponent(out RoomBase room))
                this.room = room;

            if (this.room != null)
            {
                GUILayout.Label("SELECTED: " + this.room.name);
                
                if(GUILayout.Button("Create Passages"))
                    BuildPassages(this.room.transform);
                if(GUILayout.Button("Create Factories"))
                    CreateFactories();
            }
        }

        private void CreateFactories()
        {
            CreateNewFactoryFor(leftPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin + 1, bounds.yMax), 
              bounds => new Vector3Int(bounds.xMin + 1, bounds.yMin), 
                false);
            
            CreateNewFactoryFor(rightPassageTilemap, 
                bounds => new Vector3Int(bounds.xMax - 1, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMax - 1, bounds.yMin), 
                false);
            
            CreateNewFactoryFor(topPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMax - 1), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMax - 1), 
                true);
            
            CreateNewFactoryFor(bottomPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMin + 1), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMin + 1), 
                true);
        }

        private void CreateNewFactoryFor(Tilemap tilemap, Func<BoundsInt, Vector3Int> getFirstPoint, Func<BoundsInt, Vector3Int> getSecondPoint, bool isHorizontal)
        {
            if(tilemap == null) return;

            var previousFactory = tilemap.GetComponentInChildren<RoomFactory>();

            if(previousFactory != null)
            {
                DestroyImmediate(previousFactory.gameObject);
            }

            tilemap.CompressBounds();

            BoundsInt boundsInt = tilemap.cellBounds;
                
            Vector3 from = tilemap.CellToWorld(getFirstPoint.Invoke(tilemap.cellBounds));
            Vector3 to = tilemap.CellToWorld(getSecondPoint.Invoke(tilemap.cellBounds));

            Vector3 centerPosition = Vector3.zero;
            
            if (isHorizontal)
            {
                centerPosition = new Vector3((from.x + to.x) / 2, from.y);    
            }
            else
            {
                centerPosition = new Vector3(from.x, (from.y + to.y) / 2);
            }
            
            Instantiate(new GameObject($"[{tilemap.name} Factory]", typeof(CommonRoomFactory)), centerPosition, 
                quaternion.identity, tilemap.transform);
        }
        
        private void ClearPassages(Transform grid)
        {
            var allMaps = grid.GetComponentsInChildren<Tilemap>();

            foreach (var map in allMaps)
            {
                if(map.name.Contains("["))
                    DestroyImmediate(map.gameObject);
            }
        }
        
        public void BuildPassages(Transform room)
        {
            grid = room.GetComponentInChildren<Grid>().transform;
        
            ClearPassages(grid);
            
            leftPassageTilemap = CreatePassage("[Passage Left]", Vector2.left).GetComponent<Tilemap>();
            rightPassageTilemap = CreatePassage("[Passage Right]", Vector2.right).GetComponent<Tilemap>();
            topPassageTilemap = CreatePassage("[Passage Top]", Vector2.up).GetComponent<Tilemap>();
            bottomPassageTilemap = CreatePassage("[Passage Bottom]", Vector2.down).GetComponent<Tilemap>();
        }
        
        private GameObject CreatePassage(string name, Vector3 offset)
        {
            var passage = Instantiate(new GameObject(), grid.position + offset, Quaternion.identity, grid);
            passage.AddComponent<Tilemap>();
            passage.AddComponent<TilemapRenderer>();
            passage.AddComponent<Passage>();

            passage.name = name;

            return passage;
        }
    }
}
