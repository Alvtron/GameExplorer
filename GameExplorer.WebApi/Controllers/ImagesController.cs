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
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.Image}" />
    public class ImagesController : ControllerBase, IController<Image>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagesController"/> class.
        /// </summary>
        public ImagesController()
        {
        }

        // GET: api/Images
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Image> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Images;
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
        // GET: api/Images/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Image))]
        public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var image = Context.Images.Find(uid);

            if (image == null) return NotFound();

            return Ok(image);
        }

        // PUT: api/Images/5
        /// <summary>
        /// Puts the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Image image)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Context.Entry(image).State = EntityState.Modified;
                Context.SaveChanges();
                return Ok(image);
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

        // POST: api/Images
        /// <summary>
        /// Posts the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        [ResponseType(typeof(Image))]
        public IHttpActionResult Post(Image image)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(image.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Images.Add(image);
                Context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = image.Uid }, image);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Images/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Image))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var image = Context.Images.Find(uid);

            if (image == null) return NotFound();

            Context.Images.Remove(image);
            Context.SaveChanges();

            return Ok(image);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Images.Any(e => e.Uid == uid);
        }
    }
}