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
        private ToyPresentor _visualizedToy;
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
                toyPresentor.gameObject.SetActive(false);
                if (toy == Settings.PlayerSettings.CurrentPlayer)
                {
                    VisualizeToyPresentor(toyPresentor);
                    Select(_visualizedToy);
                }
            }
            if (_visualizedToy == null)
            {
                ToyPresentor toyPresentor = _toyChoosePresentors.FirstOrDefault(presentor => presentor.IsAvailable);
                if (toyPresentor == null)
                {
                    VisualizeToyPresentor(_toyChoosePresentors.First());
                }
                else
                {
                    VisualizeToyPresentor(toyPresentor);
                    Select(_visualizedToy);
                }
                
            }
        }
        public void SelectToy()
        {
            Select(_visualizedToy);
        }
        public void MoveRight()
        {
            if (_toyChoosePresentors == null)
            {
                Debug.LogError($"{_toyChoosePresentors} is null");
                return;
            }
            int currentVisualizeIndex = _toyChoosePresentors.IndexOf(_visualizedToy);
            ToyPresentor nextPresentor;

            if (currentVisualizeIndex == _toyChoosePresentors.Count - 1)
            {
                nextPresentor = _toyChoosePresentors.First();
            }
            else
            {
                currentVisualizeIndex++;
                nextPresentor = _toyChoosePresentors[currentVisualizeIndex];
            }

            VisualizeToyPresentor(nextPresentor);
        }
        public void MoveLeft()
        {
            if (_toyChoosePresentors == null)
            {
                Debug.LogError($"{_toyChoosePresentors} is null");
                return;
            }
            int currentVisualizeIndex = _toyChoosePresentors.IndexOf(_visualizedToy);
            ToyPresentor nextPresentor;


            if (currentVisualizeIndex == 0)
            {
                nextPresentor = _toyChoosePresentors.Last();
                
            }
            else
            {
                currentVisualizeIndex--;
                nextPresentor = _toyChoosePresentors[currentVisualizeIndex];
            }

            VisualizeToyPresentor(nextPresentor);
        }
        private void VisualizeToyPresentor(ToyPresentor toyPresentor)
        {
            if (toyPresentor == null)
            {
                throw new ArgumentNullException("toyPresentor", "ToyPresentor is null");
            }
            if (_visualizedToy == null)
            {
                _visualizedToy = _toyChoosePresentors.First();
            }
            ToyPresentor oldVisualizedToy = _visualizedToy;
            oldVisualizedToy.gameObject.SetActive(false);
            toyPresentor.gameObject.SetActive(true);
            _visualizedToy = toyPresentor;
        }
        private void Select(ToyPresentor toyPresentor)
        {
            if (toyPresentor == null)
            {
                throw new ArgumentNullException("toyPresentor", "ToyPresentor is null");
            }
            if (_selectedToy != null)
            {
                _selectedToy.Deselect();
            }
            int indexToyPresentor = _toyChoosePresentors.IndexOf(toyPresentor);
            Settings.PlayerSettings.CurrentPlayer = Settings.PlayerSettings.Players[indexToyPresentor];
            toyPresentor.Select();
            _selectedToy = toyPresentor;
            Settings.SaveSettings();
        }
    }
}

