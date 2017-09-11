using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Routing;
using DataAccess.DataBase;

namespace WebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DataAccess.DataBase;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<BankDepartment>("BankDepartment");
    builder.EntitySet<Bank>("Bank"); 
    builder.EntitySet<City>("City"); 
    builder.EntitySet<CurrencyRateByTime>("CurrencyRateByTime"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BankDepartmentController : ODataController
    {
        private RateCurrencyContext db = new RateCurrencyContext();

        // GET: odata/BankDepartment
        [EnableQuery]
        public IQueryable<BankDepartment> GetBankDepartment()
        {
            return db.BankDepartment;
        }

        // GET: odata/BankDepartment(5)
        [EnableQuery]
        public SingleResult<BankDepartment> GetBankDepartment([FromODataUri] int key)
        {
            return SingleResult.Create(db.BankDepartment.Where(bankDepartment => bankDepartment.Id == key));
        }

        // PUT: odata/BankDepartment(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<BankDepartment> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BankDepartment bankDepartment = db.BankDepartment.Find(key);
            if (bankDepartment == null)
            {
                return NotFound();
            }

            patch.Put(bankDepartment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankDepartmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bankDepartment);
        }

        // POST: odata/BankDepartment
        public IHttpActionResult Post(BankDepartment bankDepartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BankDepartment.Add(bankDepartment);
            db.SaveChanges();

            return Created(bankDepartment);
        }

        // PATCH: odata/BankDepartment(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BankDepartment> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BankDepartment bankDepartment = db.BankDepartment.Find(key);
            if (bankDepartment == null)
            {
                return NotFound();
            }

            patch.Patch(bankDepartment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankDepartmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bankDepartment);
        }

        // DELETE: odata/BankDepartment(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BankDepartment bankDepartment = db.BankDepartment.Find(key);
            if (bankDepartment == null)
            {
                return NotFound();
            }

            db.BankDepartment.Remove(bankDepartment);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BankDepartment(5)/Bank
        [EnableQuery]
        public SingleResult<Bank> GetBank([FromODataUri] int key)
        {
            return SingleResult.Create(db.BankDepartment.Where(m => m.Id == key).Select(m => m.Bank));
        }

        // GET: odata/BankDepartment(5)/City
        [EnableQuery]
        public SingleResult<City> GetCity([FromODataUri] int key)
        {
            return SingleResult.Create(db.BankDepartment.Where(m => m.Id == key).Select(m => m.City));
        }

        // GET: odata/BankDepartment(5)/CurrencyRateByTime
        [EnableQuery]
        public IQueryable<CurrencyRateByTime> GetCurrencyRateByTime([FromODataUri] int key)
        {
            return db.BankDepartment.Where(m => m.Id == key).SelectMany(m => m.CurrencyRateByTime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankDepartmentExists(int key)
        {
            return db.BankDepartment.Count(e => e.Id == key) > 0;
        }
    }
}
