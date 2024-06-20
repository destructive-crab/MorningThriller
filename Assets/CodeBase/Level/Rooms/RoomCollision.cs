using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace destructive_code.LevelGeneration
{
    [RequireComponent(typeof(RoomCollision))]
    public sealed class RoomCollision
    {
        private Tilemap[] maps;
        private Tilemap collisionMap;
        private RoomBase room;

        public RoomCollision(RoomBase room)
        {
            this.room = room;
        }

        public void Generate()
        {
            maps = room.GetComponentsInChildren<Tilemap>();

            TileBase collisionTile = Resources.Load<TileBase>("RoomData/CollisionTile");

            Dictionary<Vector2, object> allPoints = new Dictionary<Vector2, object>();

            foreach (var map in maps)
            {
                foreach (var point in map.cellBounds.allPositionsWithin)
                    allPoints.TryAdd(map.CellToWorld(point), map.GetTile(point));
            }

            collisionMap = room.transform.Find("|Collision Map|")?.GetComponent<Tilemap>();


            if (this.collisionMap == null)
            {
                collisionMap = new GameObject("|Collision Map|").AddComponent<Tilemap>();
                collisionMap.gameObject.AddComponent<TilemapCollider2D>();
                collisionMap.transform.parent = room.GetComponentInChildren<Grid>().transform;
                collisionMap.transform.localPosition = Vector3.zero;
            }
            
            //определяем границы
            foreach (var point in allPoints)
            {
                if(point.Value == null)
                    continue;

                var points = CheckTile(point.Key);

                foreach (var pointForCollision in points)
                {
                    if(room.PassageHandler.Contains(Direction.Left) && Math.Abs(pointForCollision.x - ((Vector2)room.transform.position + room.PassageHandler.GetPassage(Direction.Left).Factory.Offset).x + 1) < 0.05f)
                        continue;
                    
                    if(room.PassageHandler.Contains(Direction.Top) && Math.Abs(pointForCollision.y - ((Vector2)room.transform.position + room.PassageHandler.GetPassage(Direction.Top).Factory.Offset).y) < 0.05f)
                        continue;
                    
                    if(room.PassageHandler.Contains(Direction.Right) && Math.Abs(pointForCollision.x - ((Vector2)room.transform.position + room.PassageHandler.GetPassage(Direction.Right).Factory.Offset).x) < 0.05f)
                        continue;
                    
                    if(room.PassageHandler.Contains(Direction.Bottom) && Math.Abs(pointForCollision.y - ((Vector2)room.transform.position + room.PassageHandler.GetPassage(Direction.Bottom).Factory.Offset).y + 1) < 0.05f)
                        continue;
                    
                    collisionMap.SetTile(collisionMap.WorldToCell(pointForCollision), collisionTile);
                }
            }

            Vector3[] CheckTile(Vector3 position)
            {
                List<Vector3> pointsList = new();

                Vector3Int offset = Vector3Int.zero;

                offset = Vector3Int.up;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3.up);
                
                offset = Vector3Int.down;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3Int.down);   
               
                offset = Vector3Int.right;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3Int.right);

                offset = Vector3Int.left;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3Int.left);   
               
                offset = Vector3Int.up + Vector3Int.right;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3Int.up + Vector3Int.right);

                offset = Vector3Int.up + Vector3Int.left;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3Int.up + Vector3Int.left);   
               
                offset = Vector3Int.down + Vector3Int.right;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3Int.down + Vector3Int.right);   
                
                offset = Vector3Int.down + Vector3Int.left;
                if(!allPoints.ContainsKey(position + offset) || allPoints[position + offset] == null)
                    pointsList.Add(position + Vector3Int.down + Vector3Int.left);

                return pointsList.ToArray();
            }
        }
    }
}