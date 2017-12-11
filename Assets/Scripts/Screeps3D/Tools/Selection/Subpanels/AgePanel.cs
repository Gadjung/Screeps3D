﻿using System;
using Common.Utils;
using Screeps3D.RoomObjects;
using Screeps_API;
using TMPro;
using UnityEngine;

namespace Screeps3D.Tools.Selection.Subpanels
{
    public class AgePanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private ScaleVisAxes _meter;
        
        private Creep _creep;

        public override string Name
        {
            get { return "age"; }
        }

        public override Type ObjectType
        {
            get { return typeof(Creep); }
        }

        public override void Load(RoomObject roomObject)
        {
            _creep = roomObject as Creep;
            _creep.OnDelta += OnTick;
            UpdateLabel();
        }

        private void OnTick(JSONObject obj)
        {
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            var ttl = _creep.AgeTime - _creep.Room.GameTime;
            _label.text = string.Format("{0:n0}", ttl);
            _meter.Visible((float) ttl / 1500);
        }

        public override void Unload()
        {
            _creep = null;
            _creep.OnDelta -= OnTick;
        }
    }
}