using Microsoft.AspNetCore.Http;
using PizzeriaMassagotti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        public List<SelectListItem> GetAllValidMonths()
        {
         
            var selectListMonths = new List<SelectListItem>();
            string s;

            for (int i = 1; i < 13; i++)
            {
                if (i<10)
                {
                    s = "0" + i.ToString(); 
                }
                else
                {
                    s = i.ToString();
                }
                selectListMonths.Add(new SelectListItem
                { Text = s, Value = s });
            }         
            return selectListMonths;
        }



        public List<SelectListItem> GetAllValidYears()
        {

            var selectListYears = new List<SelectListItem>();
            
            for (int i = 2017; i < 2030; i++)
            {
               
                selectListYears.Add(new SelectListItem
                {Text = i.ToString(), Value = i.ToString() });
            }
            return selectListYears;
        }

        public bool DateValidation(string month, string year)
        {
           DateTime expirationDate = new DateTime(int.Parse(year), int.Parse(month), 1);
           int result = DateTime.Compare(DateTime.Today, expirationDate);
            if (result > 0)
            {
                return false;
            }
            else {
                return true;
            }
            
        }
    }

}