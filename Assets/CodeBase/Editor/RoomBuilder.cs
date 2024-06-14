using System;
using destructive_code.LevelGeneration;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace rainy_morning
{
    public class RoomBuilder : EditorWindow
    {
        private RoomBase room;
        
        private Grid grid;
        private Transform GridTransform => grid.transform;

        private Tilemap mainMap;
        
        private Tilemap leftPassageTilemap;
        private Tilemap rightPassageTilemap;
        private Tilemap topPassageTilemap;
        private Tilemap bottomPassageTilemap;

        private RoomFactory leftFactory;
        private RoomFactory rightFactory;
        private RoomFactory topFactory;
        private RoomFactory bottomFactory;

        private const string LeftPassage = "LeftPassage";
        private const string RightPassage = "RightPassage";
        private const string TopPassage = "TopPassage";
        private const string BottomPassage = "BottomPassage";
        
        [MenuItem("My Tools/Room Builder")]
        private static void ShowWindow()
        {
            GetWindow(typeof(RoomBuilder));
        }

        private void OnGUI()
        {
            if (Selection.activeObject != null && Selection.activeObject is GameObject gameObject &&
                gameObject.TryGetComponent(out RoomBase room))
            {
                this.room = room;
                
                Refresh();
            }

            if (this.room != null)
            {
                GUILayout.Label("[]-[ROOM BUILDER]-[]");
                
                EditorGUI.BeginDisabledGroup(true);
                
                EditorGUILayout.ObjectField("Selected: ", this.room, typeof(RoomBase), false);
                
                EditorGUI.EndDisabledGroup();
                
                GUILayout.Space(15);
             
                if(GUILayout.Button("Refresh Data"))
                    Refresh();
                
                if(GUILayout.Button("Total Fix"))
                    TotalFix();
                
                GUILayout.Space(15);

                if (GUILayout.Button("Update Room Size"))
                {
                    UpdateRoomSize();
                }
                
                EditorGUI.BeginDisabledGroup(true);
                
                EditorGUILayout.ObjectField("Main Tilemap: ", mainMap, typeof(Tilemap), false);
                
                EditorGUI.EndDisabledGroup();
                GUILayout.Space(7);

                GUILayout.Label("Passages: ");
                
                if(GUILayout.Button("Build Empty Passages"))
                    CreateClearPassages();

                if (GUILayout.Button("Build Passages From Prefabs"))
                    CreatePassagesFromPrefabs();

                if (GUILayout.Button("Update Passages"))
                    UpdatePassages();
                
                EditorGUI.BeginDisabledGroup(true);

                EditorGUILayout.ObjectField(LeftPassage, leftPassageTilemap, typeof(Passage), false);
                EditorGUILayout.ObjectField(RightPassage, rightPassageTilemap, typeof(Passage), false);
                EditorGUILayout.ObjectField(TopPassage, topPassageTilemap, typeof(Passage), false);
                EditorGUILayout.ObjectField(BottomPassage, bottomPassageTilemap, typeof(Passage), false);
                
                EditorGUI.EndDisabledGroup();
                GUILayout.Space(7);

                GUILayout.Label("Factories: ");

                if(GUILayout.Button("Build Default Factories"))
                    CreateDefaultFactories();

                if (GUILayout.Button("Update Factories Positions"))
                    UpdateFactoriesPositions();
                
                EditorGUI.BeginDisabledGroup(true);

                EditorGUILayout.ObjectField(LeftPassage + "Factory", leftFactory, typeof(RoomFactory), false);
                EditorGUILayout.ObjectField(RightPassage + "Factory", rightFactory, typeof(RoomFactory), false);
                EditorGUILayout.ObjectField(TopPassage + "Factory", topFactory, typeof(RoomFactory), false);
                EditorGUILayout.ObjectField(BottomPassage + "Factory", bottomFactory, typeof(RoomFactory), false);
                
                EditorGUI.EndDisabledGroup();
            }
        }

        private void UpdateRoomSize()
        {
            mainMap.CompressBounds();

            this.room.RoomSize = new Vector2(mainMap.cellBounds.size.x, mainMap.cellBounds.size.y);
        }

        private void TotalFix()
        {
            Refresh();
            UpdateRoomSize();
            UpdatePassages();
            UpdateFactoriesPositions();
        }

        private void CreatePassagesFromPrefabs()
        {
            ClearPassages();
            
            CreatePassageFromPrefab(Direction.Left);
            CreatePassageFromPrefab(Direction.Right);
            CreatePassageFromPrefab(Direction.Top);
            CreatePassageFromPrefab(Direction.Bottom);

            UpdatePassages();
        }

        private void Refresh()
        {
            RefreshGrid();
            RefreshTilemaps();
            RefreshFactories();
        }

        private void RefreshFactories()
        {
            leftFactory = leftPassageTilemap.GetComponentInChildren<RoomFactory>();
            rightFactory = rightPassageTilemap.GetComponentInChildren<RoomFactory>();
            topFactory = topPassageTilemap.GetComponentInChildren<RoomFactory>();
            bottomFactory = bottomPassageTilemap.GetComponentInChildren<RoomFactory>();
        }

        private void RefreshGrid()
        {
            if (room != null)
            {
                grid = room.GetComponentInChildren<Grid>();
            }
        }

        private void CreateDefaultFactories()
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
            
            RefreshFactories();
        }

        private void CreateNewFactoryFor(Tilemap tilemap, 
            Func<BoundsInt, Vector3Int> getFirstPoint,
            Func<BoundsInt, Vector3Int> getSecondPoint, 
            bool isHorizontal, GameObject factoryInstance = null)
        {
            if (tilemap == null)
            {
                RefreshTilemaps();

                return;
            }

            if (factoryInstance == null)
            {
                var previousFactory = tilemap.GetComponentInChildren<RoomFactory>();
                
                if(previousFactory != null)
                {
                    DestroyImmediate(previousFactory.gameObject);
                }
            }

            tilemap.CompressBounds();   

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

            if(factoryInstance == null)
            {
                factoryInstance = new GameObject($"[{tilemap.name} Factory]");
                
                factoryInstance.transform.SetParent(tilemap.transform);
                
                factoryInstance.AddComponent<CommonRoomFactory>();
                           
                Undo.RegisterCreatedObjectUndo(factoryInstance, $"Spawn factory for {tilemap.name}");
            }
          
            factoryInstance.transform.position = centerPosition;
            factoryInstance.GetComponent<RoomFactory>().SetOffset(centerPosition);
        }

        private void RefreshTilemaps()
        {
            mainMap = grid.transform.Find("-Tilemap-").GetComponent<Tilemap>();
            var passages = GridTransform.GetComponentsInChildren<Passage>();

            foreach (var passage in passages)
            {
                if (passage.Direction == Direction.Left)
                {
                    leftPassageTilemap = passage.GetComponent<Tilemap>();
                }
                if (passage.Direction == Direction.Right)
                {
                    rightPassageTilemap = passage.GetComponent<Tilemap>();
                }
                if (passage.Direction == Direction.Top)
                {
                    topPassageTilemap = passage.GetComponent<Tilemap>();
                }
                if (passage.Direction == Direction.Bottom)
                {
                    bottomPassageTilemap = passage.GetComponent<Tilemap>();
                }
            }
        }

        public void CreateClearPassages()
        {
            ClearPassages();
            
            leftPassageTilemap = CreateClearPassage("[Passage Left]", Vector2.left).GetComponent<Tilemap>();
            rightPassageTilemap = CreateClearPassage("[Passage Right]", Vector2.right).GetComponent<Tilemap>();
            topPassageTilemap = CreateClearPassage("[Passage Top]", Vector2.up).GetComponent<Tilemap>();
            bottomPassageTilemap = CreateClearPassage("[Passage Bottom]", Vector2.down).GetComponent<Tilemap>();
            
            UpdatePassages();
        }

        public void CreatePassageFromPrefab(Direction direction)
        {
            Object passagePrefab;
            
            switch (direction)
            {
                case Direction.Top:
                    passagePrefab = PrefabUtility.InstantiatePrefab(Resources.Load<Passage>("[Passage Top]"), GridTransform);
                    break;
                case Direction.Bottom:
                    passagePrefab = PrefabUtility.InstantiatePrefab(Resources.Load<Passage>("[Passage Bottom]"), GridTransform);
                    break;
                case Direction.Left:
                    passagePrefab = PrefabUtility.InstantiatePrefab(Resources.Load<Passage>("[Passage Left]"), GridTransform);
                    break;
                case Direction.Right:
                    passagePrefab = PrefabUtility.InstantiatePrefab(Resources.Load<Passage>("[Passage Right]"), GridTransform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            Refresh();
            
            Undo.RegisterCreatedObjectUndo(passagePrefab, $"Spawn {passagePrefab.name}");
        }

        private GameObject CreateClearPassage(string name, Vector3 offset)
        {
            var passage = new GameObject
            {
                transform =
                {
                    position = GridTransform.position + offset,
                    parent = mainMap.transform.parent
                }
            };

            passage.AddComponent<Passage>();

            passage.AddComponent<Tilemap>();
            passage.AddComponent<TilemapRenderer>();

            passage.name = name;
            
            Undo.RegisterCreatedObjectUndo(passage.gameObject, $"Spawn {name}");

            return passage;
        }

        private void ClearPassages()
        {
            var allMaps = GridTransform.GetComponentsInChildren<Tilemap>();

            foreach (var map in allMaps)
            {
                if (map.name.Contains("["))
                {
                    Undo.DestroyObjectImmediate(map.gameObject);
                }
            }
            
            room.GetComponent<PassageHandler>().ClearPassages();
        }

        private void UpdatePassages()
        {
            room.GetComponent<PassageHandler>().ClearPassages();
            
            room.GetComponent<PassageHandler>().SetPassage(leftPassageTilemap.GetComponent<Passage>());
            room.GetComponent<PassageHandler>().SetPassage(bottomPassageTilemap.GetComponent<Passage>());
            room.GetComponent<PassageHandler>().SetPassage(rightPassageTilemap.GetComponent<Passage>());
            room.GetComponent<PassageHandler>().SetPassage(topPassageTilemap.GetComponent<Passage>());
        }

        private void UpdateFactoriesPositions()
        {
            CreateNewFactoryFor(leftPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin + 1, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMin + 1, bounds.yMin), 
                false,
                leftFactory.gameObject);
            
            leftPassageTilemap.GetComponent<Passage>().UpdateFactory();
            
            CreateNewFactoryFor(rightPassageTilemap, 
                bounds => new Vector3Int(bounds.xMax - 1, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMax - 1, bounds.yMin), 
                false,
                rightFactory.gameObject);
            
            rightPassageTilemap.GetComponent<Passage>().UpdateFactory();
            
            CreateNewFactoryFor(topPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMax - 1), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMax - 1), 
                true,
                topFactory.gameObject);
            
            bottomPassageTilemap.GetComponent<Passage>().UpdateFactory();
            
            CreateNewFactoryFor(bottomPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMin + 1), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMin + 1), 
                true,
                bottomFactory.gameObject);
            
            topPassageTilemap.GetComponent<Passage>().UpdateFactory();
        }
    }
}