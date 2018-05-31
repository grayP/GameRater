using GameRaterData.Model;
using GameRaterServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameRater.Models
{
    public class OnLineGameViewModel : BaseModel.ViewModelBase, IOnLineGameViewModel
    {
        GameManager _gameManager;
        public OnLineGameViewModel() : base()
        {
            _gameManager = new GameManager();
        }



        public List<OnLineGame> OnLineGames { get; set; }
        public OnLineGame SearchEntity { get; set; }
        public OnLineGame Entity { get; set; }
        public IEnumerable<SelectListItem> Rating
        {
            get
            {
                var selectList = new List<SelectListItem>();
                for (int i = 1; i <= 5; i++)
                {
                    selectList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
                return selectList;
            }
        }


        protected override void Init()
        {
            OnLineGames = new List<OnLineGame>();
            SearchEntity = new OnLineGame();
            Entity = new OnLineGame();
            base.Init();
        }
        public override void HandleRequest()
        {
            base.HandleRequest();
        }
        protected override void ResetSearch()
        {
            SearchEntity = new OnLineGame();
        }

        protected override void Get()
        {

            OnLineGames = _gameManager.GetAllGames(SearchEntity);
        }

        protected override void Edit()
        {

            Entity = _gameManager.Find(Convert.ToInt32(EventArgument));
            base.Edit();

        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new OnLineGame()
            {
                GameTitle = "",
                GameDescription = "",
                GameRating = 0.0
            };
            base.Add();
        }
        protected override void Save()
        {
            if (Mode == "Add")
            {
                _gameManager.Insert(Entity);
            }
            else
            {
                _gameManager.Update(Entity);
            }
            ValidationErrors = _gameManager.ValidationErrors;
            base.Save();
        }

        protected override void Delete()
        {
            Entity = _gameManager.Find(Convert.ToInt32(EventArgument));
            _gameManager.Delete(Entity);
            Get();
            base.Delete();
        }
    }
}


