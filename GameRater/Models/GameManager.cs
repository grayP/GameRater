using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameRaterData;
using GameRaterData.Interfaces;
using GameRaterData.Model;

namespace GameRater.Models
{
    public class GameManager : IGameManager , IDisposable
    {
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }
       GameRaterEntities _context;

        public GameManager()
        {
            _context = new GameRaterEntities();
        }


        public bool Validate(OnLineGame entity)
        {
            ValidationErrors.Clear();
            if (!string.IsNullOrEmpty(entity.GameTitle))
            {
                //Sample validation rule
                if (entity.GameTitle.ToLower() == entity.GameTitle)
                {
                    ValidationErrors.Add(new KeyValuePair<string, string>("Game Name", "Game Name cannot be all lower case"));
                }
            }
            return (ValidationErrors.Count == 0);
        }

        public OnLineGame Find(int GameId)
        {
            return _context.OnLineGames.FirstOrDefault(x => x.Id == GameId);
        }
        public List<OnLineGame> GetAllGames(OnLineGame entity)
        {
            var ret = _context.OnLineGames.OrderBy(x => x.GameTitle).ToList<OnLineGame>();
            if (!string.IsNullOrEmpty(entity.GameTitle))
            {
                ret = ret.FindAll(p => p.GameTitle.ToLower().Contains(entity.GameTitle.ToLower()));
            }
            return ret;
        }
        public List<OnLineGame> GetAllGamesByRating(int order)
        {
            return order == 0 ? _context.OnLineGames.OrderBy(x => x.GameRating).ToList() : _context.OnLineGames.OrderByDescending(x => x.GameRating).ToList(); ;
        }

        public bool Update(OnLineGame entity)
        {
            if (!Validate(entity)) return false;
            try
            {
                using (var db = new GameRaterEntities())
                {
                    db.OnLineGames.Attach(entity);
                    var ModifiedGame = db.Entry(entity);
                    ModifiedGame.Property(e => e.GameTitle).IsModified = true;
                    ModifiedGame.Property(e => e.GameDescription).IsModified = true;
                    ModifiedGame.Property(e => e.GameRating).IsModified = true;
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                //Log4net.Add(ex.InnerException,"Update");
                return false;
            }
        }

        public bool Insert(OnLineGame entity)
        {
            try
            {
                if (!Validate(entity)) return false;
                var newGame = new OnLineGame()
                {
                    GameTitle = entity.GameTitle.Trim(),
                    GameDescription = entity.GameDescription,
                    GameRating = entity.GameRating,
                    UserId = entity.UserId
                };
                _context.OnLineGames.Add(newGame);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Log4net.Add(ex.InnerException,"Insert");
                return false;
            }
        }

        public bool Delete(OnLineGame entity)
        {
            _context.OnLineGames.Attach(entity);
            _context.OnLineGames.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GameManager() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
