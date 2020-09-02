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
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.Report}" />
    public class ReportsController : ControllerBase, IController<Report>
    {
        // GET: api/Reports
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Report> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Reports;
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
        // GET: api/Reports/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Report))]
        public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var report = Context.Reports.Find(uid);

            if (report == null) return NotFound();

            return Ok(report);
        }

        // PUT: api/Reports/5
        /// <summary>
        /// Puts the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Report report)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Context.Entry(report).State = EntityState.Modified;
                Context.Entry(report?.User).State = EntityState.Modified;

                Context.SaveChanges();
                return Ok(report);
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

        // POST: api/Reports
        /// <summary>
        /// Posts the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        [ResponseType(typeof(Report))]
        public IHttpActionResult Post(Report report)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(report.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Reports.Add(report);
                Context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = report.Uid }, report);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Reports/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Report))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var report = Context.Reports.Find(uid);

            if (report == null) return NotFound();

            Context.Reports.Remove(report);
            Context.SaveChanges();

            return Ok(report);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Reports.Any(e => e.Uid == uid);
        }
    }
}