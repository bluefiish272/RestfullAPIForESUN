using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestfullAPIForESUN.Models;

namespace RestfullAPIForESUN.Controllers
{
    public class prodclassesController : ApiController
    {
        private ProductTestEntities db = new ProductTestEntities();

        // GET: api/prodclasses
        public IQueryable<prodclass> Getprodclass()
        {
            return db.prodclass;
        }

        // GET: api/prodclasses/5
        [ResponseType(typeof(prodclass))]
        public IHttpActionResult Getprodclass(string id)
        {
            prodclass prodclass = db.prodclass.Find(id);
            if (prodclass == null)
            {
                return NotFound();
            }

            return Ok(prodclass);
        }

        // PUT: api/prodclasses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putprodclass(string id, prodclass prodclass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prodclass.p_classid)
            {
                return BadRequest();
            }

            db.Entry(prodclass).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!prodclassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/prodclasses
        [ResponseType(typeof(prodclass))]
        public IHttpActionResult Postprodclass(prodclass prodclass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.prodclass.Add(prodclass);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (prodclassExists(prodclass.p_classid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = prodclass.p_classid }, prodclass);
        }

        // DELETE: api/prodclasses/5
        [ResponseType(typeof(prodclass))]
        public IHttpActionResult Deleteprodclass(string id)
        {
            prodclass prodclass = db.prodclass.Find(id);
            if (prodclass == null)
            {
                return NotFound();
            }

            db.prodclass.Remove(prodclass);
            db.SaveChanges();

            return Ok(prodclass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool prodclassExists(string id)
        {
            return db.prodclass.Count(e => e.p_classid == id) > 0;
        }
    }
}