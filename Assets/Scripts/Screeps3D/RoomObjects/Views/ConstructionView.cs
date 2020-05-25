﻿using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.RoomObjects.Views
{
    public class ConstructionView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private Image _circleOutline = default;
        [SerializeField] private Image _circleFill = default;
        private ConstructionSite _site;
        private float _offset;
        private float _fillTarget;
        private float _fillRef;

        public void Init()
        {
            _offset = Random.value;
        }

        public void Load(RoomObject roomObject)
        {
            _site = roomObject as ConstructionSite;
            _circleFill.fillAmount = 0;
            UpdateFill();
        }

        public void Delta(JSONObject data)
        {
            UpdateFill();
        }

        private void UpdateFill()
        {
            var fillAmount = 0f;

            if (_site != null && _site.ProgressMax > 0)
            {
                fillAmount = _site.Progress / _site.ProgressMax;
            }

            _fillTarget = fillAmount;
        }

        public void Unload(RoomObject roomObject)
        {
            _site = null;
        }

        private void Update()
        {
            if (_site == null)
                return;

            _circleFill.fillAmount = Mathf.SmoothDamp(_circleFill.fillAmount, _fillTarget, ref _fillRef, .2f);

            var floor = .5f;
            var alpha = floor + Mathf.PingPong((Time.time + _offset) * .2f, 1 - floor);
            var color = _circleOutline.color;
            color.a = alpha;
            _circleOutline.color = color;
            _circleFill.color = color;
        }
    }
}