﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.Api.V1.Models.BillBoards;
using BackendTest.Api.V1.Models.Pagination;
using BackendTest.Infrastructure.Data.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteMeController : ControllerBase
    {
        private readonly beezycinemaContext _context;

        public DeleteMeController(beezycinemaContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        [ProducesResponseType(typeof(PagedList<IntelligentBillboardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var testCinemas = await _context.Cinema
                .Include(c => c.City)
                .Include(c => c.Room)
                .ToListAsync();

            var testRooms = await _context.Room.Include(r => r.Cinema).ToListAsync();

            var testCity = await _context.City.Include(c => c.Cinema).ToListAsync();

            var testGenre = await _context.Genre.ToListAsync();

            var testSessions = await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .ToListAsync();

            var testMovies = await _context.Movie.Include(m => m.Session).ToListAsync();

            return NotFound("Under construction...");
        }
    }
}