using System.Collections;
using MorningThriller.PlayerLogic;
using MothDIed;
using UnityEngine;

namespace MorningThriller.LevelGeneration
{
    public sealed class RoomSwitcher : DepressedBehaviour
    {
        private PlayerRoot _playerDummy;
        private bool entered = false;
        private LevelScene levelScene;

        private void Start()
        {
            _playerDummy = GetComponentInParent<PlayerRoot>();
            levelScene = Game.LevelScene;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out RoomBase roomBase))
            {
                //TODO: INPUTS DISABLE
                
                levelScene.CameraSwitcher.Transition.Enable();
                var passage = roomBase.PassageHandler.FitIn(_playerDummy.transform);

                if(passage == null) return;

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
                
                roomBase.CameraManager.VirtualCamera.Follow = _playerDummy.transform;
                
                levelScene.CameraSwitcher.SwitchTo(roomBase.CameraManager.VirtualCamera);
                
                entered = true;
            }
        }

        private IEnumerator MovePlayer(Vector3 direction)
        {
            entered = false;

            while (!entered)
            {
                //_playerDummy.body.MovePosition(transform.position + direction * 0.15f);
                yield return new WaitForFixedUpdate();
            }
            
            Vector3 startPosition = _playerDummy.transform.position;

            while (Vector3.Distance(startPosition, _playerDummy.transform.position) < 1.5f)
            {
              //  _playerDummy.body.MovePosition(transform.position + direction * 0.05f);
                yield return new WaitForFixedUpdate();
            }               
             
            //_playerDummy.Enable();
        }
    }
}