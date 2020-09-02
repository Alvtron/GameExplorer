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
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.Wiki}" />
    public class WikisController : ControllerBase, IController<Wiki>
    {
        // GET: api/Wikis
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Wiki> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Wikis
                    .Include(a => a.Photo)
                    .Include(a => a.Banner)
                    .Include(a => a.Map)
                    .Include(a => a.Game)
                    .Include(a => a.Logs)
                    .Include(a => a.Comments)
                    .Include(a => a.Screenshots)
                    .Include(a => a.Videos);
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
        // GET: api/Wikis/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Wiki))]
        public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var wiki = Context.Wikis.Find(uid);

            if (wiki == null) return NotFound();

            return Ok(wiki);
        }

        // PUT: api/Wikis/5
        /// <summary>
        /// Puts the specified wiki.
        /// </summary>
        /// <param name="wiki">The wiki.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Wiki wiki)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Context.Entry(wiki).State = EntityState.Modified;
                Context.Entry(wiki?.Photo).State = EntityState.Modified;
                Context.Entry(wiki?.Banner).State = EntityState.Modified;
                Context.Entry(wiki?.Map).State = EntityState.Modified;

                foreach (var item in wiki?.Logs)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in wiki?.Comments)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in wiki?.Screenshots)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in wiki?.Videos)
                    Context.Entry(item).State = EntityState.Modified;
                Context.SaveChanges();
                return Ok(wiki);
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

        // POST: api/Wikis
        /// <summary>
        /// Posts the specified wiki.
        /// </summary>
        /// <param name="wiki">The wiki.</param>
        /// <returns></returns>
        [ResponseType(typeof(Wiki))]
        public IHttpActionResult Post(Wiki wiki)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(wiki.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Wikis.Add(wiki);
                Context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = wiki.Uid }, wiki);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Wikis/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Wiki))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var wiki = Context.Wikis.Find(uid);

            if (wiki == null) return NotFound();

            Context.Wikis.Remove(wiki);
            Context.SaveChanges();

            return Ok(wiki);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Wikis.Any(e => e.Uid == uid);
        }
    }
}