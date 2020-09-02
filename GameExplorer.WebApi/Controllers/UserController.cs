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
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.User}" />
    public class UsersController : ControllerBase, IController<User>
    {
        // GET: api/Users
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Users
                    .Include(a => a.Photo)
                    .Include(a => a.Friends)
                    .Include(a => a.Logs)
                    .Include(a => a.Games);
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

        // GET: api/Users/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var user = Context.Users.Find(uid);

            if (user == null) return NotFound();

            return Ok(user);
        }

        // PUT: api/Users/5
        /// <summary>
        /// Puts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(User user)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = Context.Users.Find(user.Uid);

            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.Entry(entity).CurrentValues.SetValues(user);
                Context.SaveChanges();
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

        // POST: api/Users
        /// <summary>
        /// Posts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult Post(User user)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(user.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Entry(user).State = EntityState.Modified;
                Context.Entry(user?.Photo).State = EntityState.Modified;

                foreach (var item in user?.Friends)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in user?.Logs)
                    Context.Entry(item).State = EntityState.Modified;
                foreach (var item in user?.Games)
                    Context.Entry(item).State = EntityState.Modified;
                Context.SaveChanges();
                return Ok(User);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var user = Context.Users.Find(uid);

            if (user == null) return NotFound();

            Context.Users.Remove(user);
            Context.SaveChanges();

            return Ok(user);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Users.Any(e => e.Uid == uid);
        }
    }
}