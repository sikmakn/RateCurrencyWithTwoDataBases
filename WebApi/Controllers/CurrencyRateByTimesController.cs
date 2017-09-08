using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;

namespace WebApi.Controllers
{

    public class CurrencyRateByTimesController : ODataController
    {
        private readonly ICurrencyRateByTimeService _currencyRateByTimeService;
        public CurrencyRateByTimesController(ICurrencyRateByTimeService currencyRateByTimeService)
        {
            _currencyRateByTimeService = currencyRateByTimeService;
        }

        private RateCurrencyContext db = new RateCurrencyContext();

        // GET: odata/CurrencyRateByTimes
        [EnableQuery]
        public IQueryable<CurrencyRateByTime> GetCurrencyRateByTimes()
        {
            return _currencyRateByTimeService.GetAll();
        }

        // GET: odata/CurrencyRateByTimes(5)
        [EnableQuery]
        public SingleResult<CurrencyRateByTime> GetCurrencyRateByTime([FromODataUri] int key)
        {
            return SingleResult.Create(db.CurrencyRateByTime.Where(currencyRateByTime => currencyRateByTime.Id == key));
        }

        // PUT: odata/CurrencyRateByTimes(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<CurrencyRateByTime> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CurrencyRateByTime currencyRateByTime = db.CurrencyRateByTime.Find(key);
            if (currencyRateByTime == null)
            {
                return NotFound();
            }

            patch.Put(currencyRateByTime);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyRateByTimeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(currencyRateByTime);
        }

        // POST: odata/CurrencyRateByTimes
        public IHttpActionResult Post(CurrencyRateByTime currencyRateByTime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CurrencyRateByTime.Add(currencyRateByTime);
            db.SaveChanges();

            return Created(currencyRateByTime);
        }

        // PATCH: odata/CurrencyRateByTimes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<CurrencyRateByTime> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CurrencyRateByTime currencyRateByTime = db.CurrencyRateByTime.Find(key);
            if (currencyRateByTime == null)
            {
                return NotFound();
            }

            patch.Patch(currencyRateByTime);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyRateByTimeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(currencyRateByTime);
        }

        // DELETE: odata/CurrencyRateByTimes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            CurrencyRateByTime currencyRateByTime = db.CurrencyRateByTime.Find(key);
            if (currencyRateByTime == null)
            {
                return NotFound();
            }

            db.CurrencyRateByTime.Remove(currencyRateByTime);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/CurrencyRateByTimes(5)/BankDepartment
        [EnableQuery]
        public SingleResult<BankDepartment> GetBankDepartment([FromODataUri] int key)
        {
            return SingleResult.Create(db.CurrencyRateByTime.Where(m => m.Id == key).Select(m => m.BankDepartment));
        }

        // GET: odata/CurrencyRateByTimes(5)/Currency
        [EnableQuery]
        public SingleResult<Currency> GetCurrency([FromODataUri] int key)
        {
            return SingleResult.Create(db.CurrencyRateByTime.Where(m => m.Id == key).Select(m => m.Currency));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurrencyRateByTimeExists(int key)
        {
            return db.CurrencyRateByTime.Count(e => e.Id == key) > 0;
        }
    }
}
