using System.Collections;
using destructive_code.LevelGeneration.PlayerCode;
using destructive_code.Scenes;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public sealed class RoomSwitcher : DepressedBehaviour
    {
        private Player player;
        private bool entered = false;
        private LevelScene levelScene;

        private void Start()
        {
            player = GetComponent<Player>();
            levelScene = SceneSwitcher.LevelScene;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out RoomBase roomBase))
            {
                player.Disable();
                levelScene.CameraSwitcher.Transition.Enable();
                var passage = roomBase.PassageHandler.FitIn(player.transform);

                if (passage.Direction == Direction.Top)
                {
                    StartCoroutine(MovePlayer(Vector3.up));
                }
                else if(passage.Direction == Direction.Bottom)
                {
                    StartCoroutine(MovePlayer(Vector3.down));
                }
                else if(passage.Direction == Direction.Left)
                {
                    StartCoroutine(MovePlayer(Vector3.left));
                }
                else if(passage.Direction == Direction.Right)
                {
                    StartCoroutine(MovePlayer(Vector3.right));
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out RoomBase roomBase))
            {
                levelScene.CameraSwitcher.Transition.Disable();
                
                roomBase.CameraManager.VirtualCamera.Follow = player.transform;
                
                levelScene.CameraSwitcher.SwitchTo(roomBase.CameraManager.VirtualCamera);
                
                entered = true;
            }
        }

        private IEnumerator MovePlayer(Vector3 direction)
        {
            entered = false;
            
            while (!entered)
            {
                player.transform.position += direction * 0.1f;
                yield return new WaitForFixedUpdate();
            }

            Vector3 startPosition = player.transform.position;

            while (Vector3.Distance(startPosition, player.transform.position) < 1.5f)
            {
                player.transform.position += direction * 0.05f;
                yield return new WaitForFixedUpdate();
            }               
             
            player.Enable();
        }
    }
}