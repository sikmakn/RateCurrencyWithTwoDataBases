﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.BnrbApiAccess.Services.Interfacies
{
    public interface IRateNbrbService
    {
        Task<IEnumerable<RateShortNbrb>> ReadAllCurrencyBnrbs(int currencyId, DateTime startDate, DateTime endDate);
    }
}