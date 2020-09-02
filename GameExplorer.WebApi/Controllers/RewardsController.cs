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
    /// <seealso cref="GameExplorer.WebApi.Controllers.IController{GameExplorer.Model.Reward}" />
    public class RewardsController : ControllerBase, IController<Reward>
    {
        // GET: api/Rewards
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Reward> GetAll()
        {
            if (!Context.Database.Exists()) return null;

            try
            {
                return Context.Rewards;
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
        // GET: api/Rewards/5
        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Reward))]
        public IHttpActionResult Get(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var reward = Context.Rewards.Find(uid);

            if (reward == null) return NotFound();

            return Ok(reward);
        }

        // PUT: api/Rewards/5
        /// <summary>
        /// Puts the specified reward.
        /// </summary>
        /// <param name="reward">The reward.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Reward reward)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Context.Entry(reward).State = EntityState.Modified;

                Context.SaveChanges();
                return Ok(reward);
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

        // POST: api/Rewards
        /// <summary>
        /// Posts the specified reward type.
        /// </summary>
        /// <param name="rewardType">Type of the reward.</param>
        /// <returns></returns>
        [ResponseType(typeof(Reward))]
        public IHttpActionResult Post(Reward rewardType)
        {
            if (!Context.Database.Exists()) return InternalServerError();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (Exist(rewardType.Uid)) return BadRequest("User already exists!");

            try
            {
                Context.Rewards.Add(rewardType);
                Context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = rewardType.Uid }, rewardType);
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException");
            }
        }

        // DELETE: api/Rewards/5
        /// <summary>
        /// Deletes the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [ResponseType(typeof(Reward))]
        public IHttpActionResult Delete(Guid uid)
        {
            if (!Context.Database.Exists()) return InternalServerError();

            var reward = Context.Rewards.Find(uid);

            if (reward == null) return NotFound();

            Context.Rewards.Remove(reward);
            Context.SaveChanges();

            return Ok(reward);
        }

        /// <summary>
        /// Exists the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public bool Exist(Guid uid)
        {
            return Context.Database.Exists() && Context.Rewards.Any(e => e.Uid == uid);
        }
    }
}