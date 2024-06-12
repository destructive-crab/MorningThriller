// using System.Collections.Generic;
// using UnityEngine;
//
// namespace destructive_code.Level
// {
//     [RequireComponent(typeof(RoomBase))]
//     public sealed class PassageHandler : DepressedBehaviour
//     {
//         [SerializeField] private Dictionary<Directions, Passage> passages;
//         private RoomBase _room;
//         
//         private void Awake()
//         {
//             _room = GetComponent<RoomBase>();
//             
//             foreach (var passage in passages)
//                 passage.Value.Factory.Init(_room, passage.Key);
//             
//             OpenAllDoors();
//         }
//
//         public bool Contains(Directions direction) => passages.ContainsKey(direction);
//         
//         public void OpenAllDoors()
//         {
//             foreach (var passage in passages)
//                 passage.Value.OpenDoor();
//         }
//
//         public void CloseAllDoors()
//         {
//             foreach (var passage in passages)
//                 passage.Value.CloseDoor();
//         }
//         
//         public Passage GetPassage(Directions direction)
//             => passages.TryGetValue(direction, out var passage) ? passage : null;
//
//         public bool TryGetFree(out Passage freePassage)
//         {
//             foreach (var passage in passages)
//             {
//                 if (passage.Value.ConnectedRoom == null)
//                 {
//                     freePassage = passage.Value;
//                     return true;
//                 }
//             }
//
//             freePassage = null;
//             return false;
//         }
//
//         public void BuildPassages(List<Directions> directions)
//         {
//             int firstToDelete = Random.Range(0, 4);            
//             int second = Random.Range(0, 4);
//
//             if (!directions.Contains((Directions)firstToDelete) && passages.ContainsKey((Directions)firstToDelete))
//             {
//                 passages[(Directions)firstToDelete].DisablePassage();
//                 passages.Remove((Directions)firstToDelete);
//             }
//
//             if (firstToDelete != second && !directions.Contains((Directions)second) && passages.ContainsKey((Directions)second))
//             {
//                 passages[(Directions)second].DisablePassage();
//                 passages.Remove((Directions) second);
//             }
//         }
//
//         public void CloseAllFree()
//         {
//             List<Directions> toRemove = new();
//
//             foreach (var passage in passages)
//             {
//                 if (passage.Value.ConnectedRoom == null)
//                 {
//                     passage.Value.DisablePassage();                    
//                     toRemove.Add(passage.Key);
//                 }
//             }
//
//             foreach (var direction in toRemove)
//                 passages.Remove(direction);
//         }
//     }
// }