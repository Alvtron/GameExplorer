using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using GameExplorer.Model;

namespace GameExplorer.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.WebApi.Controllers.ControllerBase" />
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.Game}" />
    public class GamesController : ControllerBase, IController<Game>
    {
        // GET: api/Games
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Game> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Games
                    .Include(a => a.Photo)
                    .Include(a => a.Banner)
                    .Include(q => q.Quests)
                    .Include(q => q.Wikis)
                    .Include(q => q.Logs)
                    .Include(q => q.Comments)
                    .Include(q => q.Screenshots)
                    .Include(q => q.Videos);
            }
            catch (FileNotFoundException exception)
            {
                Debug.WriteLine("Error: " + exception.Message);
                return null;
            }
            catch (InvalidOperationException exception)
            {
                Debug.WriteLine("Error: " + exception.Message);
                return null;
            }
        }
        // GET: api/Games/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Game))]
        public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var game = Context.Games.Find(uid);

            if (game == null) return NotFound();

            return Ok(game);
        }

        // PUT: api/Games/5
        /// <summary>
        /// Puts the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Game game)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Context.Entry(game).State = EntityState.Modified;
                Context.Entry(game?.Photo).State = EntityState.Modified;
                Context.Entry(game?.Banner).State = EntityState.Modified;

                foreach (var item in game?.Quests)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in game?.Wikis)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in game?.Logs)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in game?.Comments)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in game?.Screenshots)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in game?.Videos)
                    Context.Entry(item).State = EntityState.Modified;

                Context.SaveChanges();
                return Ok(game);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("DbUpdateConcurrencyException");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("InvalidOperationException");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Games
        /// <summary>
        /// Posts the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        [ResponseType(typeof(Game))]
        public IHttpActionResult Post(Game game)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(game.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Games.Add(game);
                Context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = game.Uid }, game);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Games/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Game))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var game = Context.Games.Find(uid);

            if (game == null) return NotFound();

            Context.Games.Remove(game);
            Context.SaveChanges();

            return Ok(game);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Games.Any(e => e.Uid == uid);
        }
    }
}