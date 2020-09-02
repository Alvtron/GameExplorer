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
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.Quest}" />
    public class QuestsController : ControllerBase, IController<Quest>
    {
        // GET: api/Quests
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Quest> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Quests
                    .Include(a => a.Photo)
                    .Include(a => a.Banner)
                    .Include(q => q.Steps)
                    .Include(q => q.Rewards)
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
        // GET: api/Quests/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Quest))]
        public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var quest = Context.Quests.Find(uid);

            if (quest == null) return NotFound();

            return Ok(quest);
        }

        // PUT: api/Quests/5
        /// <summary>
        /// Puts the specified quest.
        /// </summary>
        /// <param name="quest">The quest.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Quest quest)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Context.Entry(quest).State = EntityState.Modified;
                Context.Entry(quest?.Photo).State = EntityState.Modified;
                Context.Entry(quest?.Banner).State = EntityState.Modified;
                Context.Entry(quest?.Map).State = EntityState.Modified;


                foreach (var item in quest?.Steps)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in quest?.Logs)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in quest?.Rewards)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in quest?.Comments)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in quest?.Screenshots)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in quest?.Videos)
                    Context.Entry(item).State = EntityState.Modified;

                Context.SaveChanges();
                return Ok(quest);
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

        // POST: api/Quests
        /// <summary>
        /// Posts the specified quest.
        /// </summary>
        /// <param name="quest">The quest.</param>
        /// <returns></returns>
        [ResponseType(typeof(Quest))]
        public IHttpActionResult Post(Quest quest)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(quest.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Quests.Add(quest);
                Context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = quest.Uid }, quest);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Quests/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Quest))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var quest = Context.Quests.Find(uid);

            if (quest == null) return NotFound();

            Context.Quests.Remove(quest);
            Context.SaveChanges();

            return Ok(quest);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Quests.Any(e => e.Uid == uid);
        }
    }
}