﻿using Common;
using System.Collections;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class PowerBankView : MonoBehaviour, IObjectViewComponent, IMapViewComponent
    {
        public const string Path = "Prefabs/RoomObjects/powerBank";

        [SerializeField] private ScaleVisibility _powerScaleVisibility;
        [SerializeField] private Collider _collider;
        private PowerBank _powerBank;

        [SerializeField] private float _Power;
        [SerializeField] private float hits;
        [SerializeField] private GameObject destroyed;
        private GameObject spawnedDebris;
        private IEnumerator _despawnDebris;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _powerBank = roomObject as PowerBank;
        }

        public void Delta(JSONObject data)
        {
            if (_powerBank == null)
            {
                return;
            }
            var power = _Power > 0 ? _Power : _powerBank.Power;
            var percentage = power / _powerBank.PowerCapacity;

            var minVisibility = 0.001f; /*to keep it visible and selectable, also allows the resource to render again when regen hits*/

            float visibility = percentage == 0 ? minVisibility : percentage;

            _powerScaleVisibility.SetVisibility(visibility);
        }

        public void Unload(RoomObject roomObject)
        {
            // perhaps make this a couroutine? seems like the debris where instantly removed upon unload
            //Destroy(spawnedDebris);
            //if (_powerBank.Hits <= 0 || hits == 1)
            //{
                spawnedDebris = Instantiate(destroyed, transform.position, transform.rotation);
                Destroy(gameObject); // Would really like to spawn this just before the powerBank is hidden.
                _despawnDebris = DespawnDebris();
                //StartCoroutine(_despawnDebris); // This coroutine never triggered again.
            //}
        }

        private IEnumerator DespawnDebris()
        {

            while (spawnedDebris != null)
            {
                Debug.Log("waiting to despawn");
                yield return new WaitForSeconds(30);
                Debug.Log("Should be despawning");
                Destroy(spawnedDebris);
                spawnedDebris = null;
                StopCoroutine(_despawnDebris);
                _despawnDebris = null;
            }
        }
        
        // IMapViewComponent *****************
        public int roomPosX { get; set; }
        public int roomPosY { get; set; }
        public void Show()
        {
            _powerScaleVisibility?.Show();
            _collider.enabled = false;
        }
        public void Hide()
        {
            _powerScaleVisibility?.Hide();
            _collider.enabled = true;
        }
    }
}
