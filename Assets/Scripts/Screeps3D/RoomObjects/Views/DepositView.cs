﻿using Common;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    internal class DepositView : MonoBehaviour, IObjectViewComponent/*, IMapViewComponent*/
    {
        public const string Path = "Prefabs/RoomObjects/mineral";

        [SerializeField] private Renderer _deposit;
        [SerializeField] private Collider _collider;
        [SerializeField] private ScaleVisibility _vis;
        //[SerializeField] private Transform _rotationRoot;

        private Quaternion _rotTarget;
        private Vector3 _posTarget;
        private Vector3 _posRef;
        private Deposit _depositObject;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _depositObject = roomObject as Deposit;
            //if (_depositObject != null)
            //{

            //    var mineralcolor = _deposit.material.color;
            //    // TODO: should color change based on density aswell? e.g. MORE green / less green
            //    // we could vary value for brigther or darker colors
            //    // saturation can vary the color aswell
            //    switch (_depositObject.DepositType)
            //    {
            //        case Constants.BaseMineral.Hydrogen:
            //            // cdcdcd hsv 0 0 80
            //            mineralcolor = Random.ColorHSV(0f, 0f, 0f, 0f, 0.80f, 0.80f);
            //            break;
            //        case Constants.BaseMineral.Oxygen:
            //            // cdcdcd hsv 0 0 80
            //            mineralcolor = Random.ColorHSV(0f, 0f, 0f, 0f, 0.80f, 0.80f);
            //            break;
            //        case Constants.BaseMineral.Utrium:
            //            // 50d7f9 hsv 192 68 98
            //            mineralcolor = Random.ColorHSV(192f / 359f, 192f / 359f, 0.68f, 0.68f, 0.98f, 0.98f);
            //            break;
            //        case Constants.BaseMineral.Keanium:
            //            // #a071ff hsv 260 56 100
            //            mineralcolor = Random.ColorHSV(260f / 359f, 260f / 359f, 0.56f, 0.56f, 1f, 1f);
            //            break;
            //        case Constants.BaseMineral.Lemergium:
            //            // should be lime-greenish #00f4a2 hsv 160 100 96

            //            mineralcolor = Random.ColorHSV(160f / 359f, 160f / 359f, 1f, 1f, 0.96f, 0.96f);
            //            break;
            //        case Constants.BaseMineral.Zynthium:
            //            // Should be sand/yellow fdd388 hsv 38 46 99
            //            mineralcolor = Random.ColorHSV(38f / 359f, 38f / 359f, 0.46f, 0.46f, 0.99f, 0.99f);
            //            break;
            //        case Constants.BaseMineral.Catalyst:
            //            // Catalyst should be red ff7777 hsv 0 53 100
            //            mineralcolor = Random.ColorHSV(0f, 0f, 0.5f, 0.5f, 1f, 1f);
            //            break;
            //    }

            //    _deposit.material.color = mineralcolor;
            //}
            //_body.material.mainTexture = _mineral.Owner.Badge;

            _rotTarget = transform.rotation;
            _posTarget = roomObject.Position;

            // Move deposit up "above" the terrain
            transform.localPosition = roomObject.Position + (Vector3.up * 0.3f);
        }

        public void Delta(JSONObject data)
        {
        }

        public void Unload(RoomObject roomObject)
        {
        }

        //// IMapViewComponent *****************
        //public int roomPosX { get; set; }
        //public int roomPosY { get; set; }
        //public void Show()
        //{
        //    _vis.Show();
        //    _collider.enabled = false;
        //}
        //public void Hide()
        //{
        //    _vis.Hide();
        //    _collider.enabled = true;
        //}
    }
}