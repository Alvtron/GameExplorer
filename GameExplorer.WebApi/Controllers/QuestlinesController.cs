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
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.Questline}" />
    public class QuestlinesController : ControllerBase, IController<Questline>
    {
        // GET: api/Questlines
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Questline> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Questlines.Include(q => q.Quests);
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
        // GET: api/Questlines/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Questline))]
            public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var quest = Context.Quests.Find(uid);

            if (quest == null) return NotFound();

            return Ok(quest);
        }

        // PUT: api/Questlines/5
        /// <summary>
        /// Puts the specified questline.
        /// </summary>
        /// <param name="questline">The questline.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Questline questline)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Context.Entry(questline).State = EntityState.Modified;
                Context.Entry(questline.Game).State = EntityState.Modified;

                foreach (var item in questline.Quests)
                    Context.Entry(item).State = EntityState.Modified;

                Context.SaveChanges();
                return Ok(questline);
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

        // POST: api/Questlines
        /// <summary>
        /// Posts the specified questline.
        /// </summary>
        /// <param name="questline">The questline.</param>
        /// <returns></returns>
        [ResponseType(typeof(Questline))]
        public IHttpActionResult Post(Questline questline)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(questline.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Questlines.Add(questline);
                Context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = questline.Uid }, questline);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Questlines/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Questline))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var questline = Context.Questlines.Find(uid);

            if (questline == null) return NotFound();

            Context.Questlines.Remove(questline);
            Context.SaveChanges();

            return Ok(questline);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Questlines.Any(e => e.Uid == uid);
        }
    }
}