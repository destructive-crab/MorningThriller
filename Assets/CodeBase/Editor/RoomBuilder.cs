using System;
using System.Collections.Generic;
using Cinemachine;
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
            if (Selection.activeGameObject != null && Selection.activeGameObject.TryGetComponent(out RoomBase room))
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

                EditorGUI.BeginDisabledGroup(true);

                EditorGUILayout.ObjectField(LeftPassage, leftPassageTilemap, typeof(Passage), false);
                EditorGUILayout.ObjectField(RightPassage, rightPassageTilemap, typeof(Passage), false);
                EditorGUILayout.ObjectField(TopPassage, topPassageTilemap, typeof(Passage), false);
                EditorGUILayout.ObjectField(BottomPassage, bottomPassageTilemap, typeof(Passage), false);
                
                EditorGUI.EndDisabledGroup();
                GUILayout.Space(7);

                GUILayout.Label("Factories: ");

                if (GUILayout.Button("Update Factories Positions"))
                    UpdateFactoriesPositions();
                
                EditorGUI.BeginDisabledGroup(true);

                EditorGUILayout.ObjectField(LeftPassage + "Factory", leftFactory, typeof(RoomFactory), false);
                EditorGUILayout.ObjectField(RightPassage + "Factory", rightFactory, typeof(RoomFactory), false);
                EditorGUILayout.ObjectField(TopPassage + "Factory", topFactory, typeof(RoomFactory), false);
                EditorGUILayout.ObjectField(BottomPassage + "Factory", bottomFactory, typeof(RoomFactory), false);
                
                EditorGUI.EndDisabledGroup();

                GUILayout.Label("Camera: ");                
                
                if (GUILayout.Button("Create Camera"))
                    CreateCamera();
            }
        }

        private void TotalFix()
        {
            Refresh();
            UpdateRoomSize();
            UpdateFactoriesPositions();
        }

        private void CreatePassagesFromPrefabs()
        {
            ClearPassages();
            
            CreatePassageFromPrefab(Direction.Left);
            CreatePassageFromPrefab(Direction.Right);
            CreatePassageFromPrefab(Direction.Top);
            CreatePassageFromPrefab(Direction.Bottom);

        }

        private void Refresh()
        {
            RefreshGrid();
            RefreshTilemaps();
            RefreshFactories();
        }

        private void UpdateRoomSize()
        {
            mainMap.CompressBounds();

            this.room.RoomSize = new Vector2(mainMap.cellBounds.size.x, mainMap.cellBounds.size.y);
            
            if (room.TryGetComponent(out PolygonCollider2D polygonCollider2D))
            {
                UpdatePoints(polygonCollider2D);
            }
            else
            {
                polygonCollider2D = room.gameObject.AddComponent<PolygonCollider2D>();
                
                UpdatePoints(polygonCollider2D);
            }

            GameObject roomBounds = new GameObject("ROOM BOUNDS");
            roomBounds.transform.parent = room.transform;
            
            PolygonCollider2D collider = roomBounds.AddComponent<PolygonCollider2D>();

            List<Vector2> resultBounds = new List<Vector2>();

            foreach (var point in mainMap.cellBounds.allPositionsWithin)
            {
                if (mainMap.GetTile(new Vector3Int((int) point.x, (int) point.y)) != null && CheckTile(point))
                {
                    resultBounds.Add(mainMap.CellToWorld(point));
                    Debug.Log(mainMap.CellToWorld(point));
                    var g0 = new GameObject(mainMap.CellToWorld(point).ToString());
                    g0.transform.parent = room.transform;
                    g0.transform.position = mainMap.CellToWorld(point);
                }
            }

            collider.points = resultBounds.ToArray();

            bool CheckTile(Vector3Int position)
            {
                if(mainMap.GetTile(position + Vector3Int.up) != null && 
                   mainMap.GetTile(position + Vector3Int.down) != null && 
                   mainMap.GetTile(position + Vector3Int.right) != null && 
                   mainMap.GetTile(position + Vector3Int.left) != null && 
                   mainMap.GetTile(position + Vector3Int.up + Vector3Int.right) != null && 
                   mainMap.GetTile(position + Vector3Int.up + Vector3Int.left) != null && 
                   mainMap.GetTile(position + Vector3Int.down + Vector3Int.right) != null && 
                   mainMap.GetTile(position + Vector3Int.down + Vector3Int.left) != null)
                
                    return false;

                return true;
            }
            void UpdatePoints(PolygonCollider2D component)
            {
                component.points
                    = new[]
                    {
                        (Vector2) mainMap.CellToWorld(new Vector3Int(mainMap.cellBounds.xMax, mainMap.cellBounds.yMax)),
                        (Vector2) mainMap.CellToWorld(new Vector3Int(mainMap.cellBounds.xMax, mainMap.cellBounds.yMin)),
                        (Vector2) mainMap.CellToWorld(new Vector3Int(mainMap.cellBounds.xMin, mainMap.cellBounds.yMin)),
                        (Vector2) mainMap.CellToWorld(new Vector3Int(mainMap.cellBounds.xMin, mainMap.cellBounds.yMax)),
                    };
            }
        }

        private void RefreshFactories()
        {
            leftFactory = leftPassageTilemap.GetComponent<RoomFactory>();
            rightFactory = rightPassageTilemap.GetComponent<RoomFactory>();
            topFactory = topPassageTilemap.GetComponent<RoomFactory>();
            bottomFactory = bottomPassageTilemap.GetComponent<RoomFactory>();
        }

        private void RefreshGrid()
        {
            if (room != null)
            {
                grid = room.GetComponentInChildren<Grid>();
            }
        }

        private void UpdateFactoryFor(Tilemap tilemap, 
            Func<BoundsInt, Vector3Int> getFirstPoint,
            Func<BoundsInt, Vector3Int> getSecondPoint, 
            bool isHorizontal)
        {
            if (tilemap == null)
            {
                RefreshTilemaps();

                return;
            }

            RoomFactory factoryInstance = tilemap.GetComponent<RoomFactory>();
            
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
                factoryInstance = tilemap.gameObject.AddComponent<CommonRoomFactory>();
                           
                Undo.RegisterCreatedObjectUndo(factoryInstance, $"Spawn factory for {tilemap.name}");
            }
            
            factoryInstance.SetOffset(centerPosition);
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
        }
        
        private void UpdateFactoriesPositions()
        {
            UpdateFactoryFor(leftPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin + 1, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMin + 1, bounds.yMin), 
                false);
            
            UpdateFactoryFor(rightPassageTilemap, 
                bounds => new Vector3Int(bounds.xMax - 1, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMax - 1, bounds.yMin), 
                false);
  
            UpdateFactoryFor(topPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMax - 1), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMax - 1), 
                true);

            UpdateFactoryFor(bottomPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMin + 1), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMin + 1), 
                true);

            RefreshFactories();
        }

        private void CreateCamera()
        {
            var cameraPrefab = Resources.Load<CinemachineVirtualCamera>("RoomData/RoomCamera");

            var instance = PrefabUtility.InstantiatePrefab(cameraPrefab, room.transform) as CinemachineVirtualCamera;
            instance.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = room.GetComponent<Collider2D>();
        }
    }
}