using Microsoft.AspNetCore.Http;
using PizzeriaMassagotti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;

namespace PizzeriaMassagotti.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;
        private readonly CartService _service;

        public PaymentService(ApplicationDbContext context, ISession session, CartService service)
        {
            _context = context;
            _session = session;
            _service = service;
        }
    }

}