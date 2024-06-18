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

            List<Vector2> dirtyPoints = new List<Vector2>();

            //определяем границы
            foreach (var point in mainMap.cellBounds.allPositionsWithin)
            {
                if (mainMap.GetTile(point) != null && CheckTile(point))
                {
                    dirtyPoints.Add(mainMap.CellToWorld(point) + GetOffset(point));

                    var test = new GameObject(mainMap.CellToWorld(point).ToString());
                    test.transform.parent = room.transform;
                    test.transform.position = mainMap.CellToWorld(point) + GetOffset(point);
                }
            }
            
            //сортируем точки
            List<Vector2> resultPoints = new();

            Vector2 current = dirtyPoints[0];
            
            while(dirtyPoints.Count > 0)
            {
                var next = FindWithMinDistance(current, dirtyPoints.ToArray());
                
                dirtyPoints.Remove(next);
                resultPoints.Add(next);
                
                current = next;
            }

            //левый проход
            List<Vector2> dirtDownLeft = new List<Vector2>();

            Vector2 startDownLeft =
                bottomPassageTilemap.CellToWorld(new Vector3Int(bottomPassageTilemap.cellBounds.xMin, bottomPassageTilemap.cellBounds.yMin));
            
            Vector2 endDownLeft =
                leftPassageTilemap.CellToWorld(new Vector3Int(leftPassageTilemap.cellBounds.xMin, leftPassageTilemap.cellBounds.yMin));

            Bounds downLeftBounds = new Bounds();
            downLeftBounds.SetMinMax(new Vector3(endDownLeft.x, startDownLeft.y), new Vector3(startDownLeft.x, endDownLeft.y));
            
            foreach (var point in resultPoints)
            {
                if(downLeftBounds.Contains(point))
                    dirtDownLeft.Add(point);
            }
            
            List<Vector2> sortedDownLeft = new List<Vector2>();
            
            Vector2 currentDownLeft = Find((vector1, vector2) => vector1.x > vector2.x ||  vector1.y < vector2.y, dirtDownLeft.ToArray());
            
            while(dirtDownLeft.Count > 0)
            {
                var next = FindWithMinDistance(currentDownLeft, dirtDownLeft.ToArray());
                
                dirtDownLeft.Remove(next);
                sortedDownLeft.Add(next);
                
                currentDownLeft = next;
            }

            bottomPassageTilemap.CompressBounds();
            leftPassageTilemap.CompressBounds();
            
            sortedDownLeft.Insert(0, 
                new Vector2(bottomPassageTilemap.CellToWorld(new(bottomPassageTilemap.cellBounds.xMin, 0, 0)).x, sortedDownLeft[0].y));
            
            sortedDownLeft
                .Insert(0, bottomPassageTilemap.CellToWorld(new(bottomPassageTilemap.cellBounds.xMin, bottomPassageTilemap.cellBounds.yMin + 1, 0)));
            sortedDownLeft.Add( 
                new Vector2(sortedDownLeft[sortedDownLeft.Count-1].x, bottomPassageTilemap.CellToWorld(new(leftPassageTilemap.cellBounds.yMin, 0, 0)).x));
            
            sortedDownLeft
               .Add(leftPassageTilemap.CellToWorld(new(leftPassageTilemap.cellBounds.xMin + 1, leftPassageTilemap.cellBounds.yMin, 0)));
            
            sortedDownLeft
                .Add(new Vector2(leftPassageTilemap.CellToWorld(new Vector3Int(leftPassageTilemap.cellBounds.xMin + 1, 0)).x, bottomPassageTilemap.CellToWorld(new Vector3Int(0, bottomPassageTilemap.cellBounds.yMin + 1)).y));

            // //finish
            // for (int i = sortedDownLeft.Count-1; i >= 0; i--)
            // {
            //     sortedDownLeft.Add(sortedDownLeft[i] + new Vector2(0.1f, 0.1f));
            // }
            
            collider.transform.position += new Vector3(0, 0, 0);
            collider.points = sortedDownLeft.ToArray();

            Vector2 Find(Func<Vector2, Vector2, bool> comparator, Vector2[] points)
            {
                Vector2 best = points[0];
                
                foreach (var point in points)
                {
                    if (comparator.Invoke(point, best))
                    {
                        best = point;
                    }
                }

                return best;
            }
            
            Vector2 FindWithMinDistance(Vector2 start, Vector2[] points)
            {
                Vector2 best = points[0];

                for (int i = 0; i < points.Length; i++)
                {
                    if (Vector2.Distance(points[i], start) < Vector2.Distance(start, best))
                        best = points[i];
                }

                return best;
            }
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
            Vector3 GetOffset(Vector3Int position)
            {
                if (mainMap.GetTile(position + Vector3Int.up) != null &&
                    mainMap.GetTile(position + Vector3Int.left) != null &&
                    mainMap.GetTile(position + Vector3Int.right) == null)
                    return Vector3.right;
                
                else if (mainMap.GetTile(position + Vector3Int.down) != null &&
                    mainMap.GetTile(position + Vector3Int.left) != null &&
                    mainMap.GetTile(position + Vector3Int.right) == null)
                    return Vector3.right + Vector3Int.up;
                
                else if (mainMap.GetTile(position + Vector3Int.right) != null &&
                         mainMap.GetTile(position + Vector3Int.left) == null &&
                         mainMap.GetTile(position + Vector3Int.up) == null &&
                         mainMap.GetTile(position + Vector3Int.down) != null)
                    return Vector3.up;
                
                else if (mainMap.GetTile(position + Vector3Int.right) != null && 
                         mainMap.GetTile(position + Vector3Int.left) != null && 
                         mainMap.GetTile(position + Vector3Int.up) == null )
                    return Vector3Int.up + Vector3Int.right / 2;
                
                else if (mainMap.GetTile(position + Vector3Int.right) != null && 
                         mainMap.GetTile(position + Vector3Int.left) != null && 
                         mainMap.GetTile(position + Vector3Int.down) == null)
                    return Vector3.right / 2;
                
                else if (mainMap.GetTile(position + Vector3Int.up) != null && 
                         mainMap.GetTile(position + Vector3Int.down) != null)
                    return Vector3.up / 2;

                return Vector3Int.zero;
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
                bounds => new Vector3Int(bounds.xMin, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMin, bounds.yMin), 
                false);
            
            UpdateFactoryFor(rightPassageTilemap, 
                bounds => new Vector3Int(bounds.xMax, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMin), 
                false);
  
            UpdateFactoryFor(topPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMax), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMax), 
                true);

            UpdateFactoryFor(bottomPassageTilemap, 
                bounds => new Vector3Int(bounds.xMin, bounds.yMin), 
                bounds => new Vector3Int(bounds.xMax, bounds.yMin), 
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