using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Com.SoftToysFighting.Settings;
using Com.SoftToysFighting.DoozyUI;


namespace Com.SoftToysFighting.Managers
{
    public class ProfileManager : ManagerBase<ProfileSettings>
    {
        [SerializeField]
        private Transform _transformContainerLevels;
        [SerializeField]
        private GameObject _prefabToyChooseObject;
        [SerializeField]
        private ToyPresentor _selectedToy;
        [SerializeField]
        private ToyPresentor _visualizeToy;
        [SerializeField]
        private List<ToyPresentor> _toyChoosePresentors;
        protected override void InitManager()
        {
            InstantiateToys();
        }

        private void InstantiateToys()
        {
            if (Settings.PlayerSettings.Players == null)
            {
                throw new NullReferenceException("Players is null");
            }
            foreach (var toy in Settings.PlayerSettings.Players)
            {
                ToyPresentor toyPresentor = 
                    Instantiate(_prefabToyChooseObject, _transformContainerLevels).GetComponent<ToyPresentor>();
                _toyChoosePresentors.Add(toyPresentor);
                toyPresentor.EntityName = toy.Name;
                if (toy.IsAvailable)
                {
                    toyPresentor.Available();
                }
                else
                {
                    toyPresentor.Unavailable();
                }
                VisualizeToyPresentor(false, toyPresentor);
                if (toy == Settings.PlayerSettings.CurrentPlayer)
                {
                    toyPresentor.Select();
                    _selectedToy = toyPresentor;
                    _visualizeToy = _selectedToy;
                    VisualizeToyPresentor(true, _visualizeToy);
                }
            }
            if (_visualizeToy == null)
            {
                _visualizeToy = _toyChoosePresentors.First();
                VisualizeToyPresentor(true, _visualizeToy);
            }
            Settings.SaveSettings();
        }
        public void SelectToy()
        {
            if (_selectedToy != null)
            {
                _selectedToy.Deselect();
            }
            _visualizeToy.Select();
            _selectedToy = _visualizeToy;
            int indexToy = _toyChoosePresentors.IndexOf(_visualizeToy);
            Settings.PlayerSettings.CurrentPlayer = Settings.PlayerSettings.Players[indexToy];
            Settings.SaveSettings();
            
        }
        public void MoveRight()
        {
            if (_toyChoosePresentors == null)
            {
                Debug.LogError($"{_toyChoosePresentors} is null");
                return;
            }
            int currentVisualizeIndex = _toyChoosePresentors.IndexOf(_visualizeToy);

            VisualizeToyPresentor(false, _visualizeToy);

            if (currentVisualizeIndex == _toyChoosePresentors.Count - 1)
            {
                currentVisualizeIndex = 0;
                _visualizeToy = _toyChoosePresentors.First();
            }
            else
            {
                currentVisualizeIndex++;
                _visualizeToy = _toyChoosePresentors[currentVisualizeIndex];
            }

            VisualizeToyPresentor(true, _visualizeToy);
        }
        public void MoveLeft()
        {
            if (_toyChoosePresentors == null)
            {
                Debug.LogError($"{_toyChoosePresentors} is null");
                return;
            }
            int currentVisualizeIndex = _toyChoosePresentors.IndexOf(_visualizeToy);

            VisualizeToyPresentor(false, _visualizeToy);

            if (currentVisualizeIndex == 0)
            {
                currentVisualizeIndex = _toyChoosePresentors.Count - 1;
                _visualizeToy = _toyChoosePresentors.Last();
                
            }
            else
            {
                currentVisualizeIndex--;
                _visualizeToy = _toyChoosePresentors[currentVisualizeIndex];
            }

            VisualizeToyPresentor(true, _visualizeToy);
        }
        private void VisualizeToyPresentor(bool isVisualize, ToyPresentor presentor)
        {
            if (isVisualize)
            {
                presentor.gameObject.SetActive(true);
            }
            else
            {
                presentor.gameObject.SetActive(false);
            }
        }
    }
}

