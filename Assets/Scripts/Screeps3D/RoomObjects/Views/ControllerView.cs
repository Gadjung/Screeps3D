﻿using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class ControllerView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private Renderer _rend;
        [SerializeField] private Renderer _playerRend;
        private Texture2D _texture;
        private Color _controllerWhite;
        private Controller _controller;

        public void Init()
        {
            InitTexture();
        }

        public void Load(RoomObject roomObject)
        {
            _controller = roomObject as Controller;
            UpdateTexture();
        }

        private void InitTexture()
        {
            _texture = new Texture2D(8, 1);
            _texture.filterMode = FilterMode.Point;
            _rend.materials[1].mainTexture = _texture;
            _rend.materials[1].SetTexture("_EmissionMap", _texture);
            ColorUtility.TryParseHtmlString("#FDF5E6", out _controllerWhite);
        }

        public void Delta(JSONObject data)
        {
            if (data["level"] == null && data["owner"] == null)
                return;
            UpdateTexture();
        }

        public void Unload(RoomObject roomObject)
        {
        }

        private void UpdateTexture()
        {
            var level = 0;
            for (var i = 0; i < 8; i++)
            {
                if (level < _controller.Level)
                {
                    _texture.SetPixel(i, 1, _controllerWhite);
                } else
                {
                    _texture.SetPixel(i, 1, Color.black);
                }
                level++;
            }
            _texture.Apply();

            if (_controller.Owner != null)
            {
                _playerRend.materials[0].mainTexture = _controller.Owner.badge;
                _playerRend.materials[0].color = Color.white;
            }
            else
            {
                _playerRend.materials[0].mainTexture = null;
                _playerRend.materials[0].color = Color.grey;
            }
        }

        private void Update()
        {
            if (_controller == null)
                return;
            
            float floor = 0.6f;
            float ceiling = 1.0f;
            float emission = floor + Mathf.PingPong (Time.time * .2f, ceiling - floor);
            Color finalColor = Color.white * emission;
            _rend.materials[1].SetColor ("_EmissionColor", finalColor);
        }
    }
}