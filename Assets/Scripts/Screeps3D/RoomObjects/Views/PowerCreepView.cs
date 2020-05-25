﻿using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    internal class PowerCreepView : ObjectView
    {
        [SerializeField] private Renderer _body = default;
        [SerializeField] private Transform _rotationRoot = default;

        private Quaternion _rotTarget;
        private Vector3 _posTarget;
        private Vector3 _posRef;
        private PowerCreep _PowerCreep;

        internal override void Load(RoomObject roomObject)
        {
            base.Load(roomObject);
            _PowerCreep = roomObject as PowerCreep;
            _body.material.SetTexture("ColorTexture", _PowerCreep?.Owner?.Badge); // main texture
            _body.material.SetColor("BaseColor", new Color(0.5f, 0.5f, 0.5f, 1f));
            _body.material.SetFloat("ColorMix", 1);

            _rotTarget = transform.rotation;
            _posTarget = roomObject.Position;
        }

        internal override void Delta(JSONObject data)
        {
            base.Delta(data);

            var posDelta = _posTarget - RoomObject.Position;

            if (posDelta.sqrMagnitude > .01)
            {
                _posTarget = RoomObject.Position;
            } 
        }

        private void Update()
        {
            if (_PowerCreep == null)
                return;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _posTarget, ref _posRef, .5f);
            _rotationRoot.transform.rotation = Quaternion.Slerp(_rotationRoot.transform.rotation, _PowerCreep.Rotation, 
                Time.deltaTime * 5);
        }
    }
}