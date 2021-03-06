﻿using AutoMapper;
using CMS.Models;
using CMS.Models.Cinema;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaService cinemaService;
        private readonly IMapper mapper;

        public CinemaController(
            ICinemaService cinemaService,
            IMapper mapper)
        {
            this.cinemaService = cinemaService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int size = 10, string orderBy = "Name", bool order = true)
        {
            try
            {
                var cinemas = await cinemaService.GetPagedAsync(page - 1, size, orderBy, order);
                var count = await cinemaService.GetCountAsync();

                var dto = new PagerViewModel<CinemaIndexViewModel>()
                {
                    Items = mapper.Map<List<CinemaIndexViewModel>>(cinemas),
                    Pager = new Pager(page, size, orderBy, order, count)
                };

                return View(dto);
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Home"); 
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(id);
                if (cinema is null)
                {
                    return NotFound();
                }

                var result = mapper.Map<CinemaDetailsViewModel>(cinema);

                return View(result);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #pragma warning disable CS1998
        public async Task<IActionResult> Create()
        #pragma warning restore CS1998
        {
            var dto = new CinemaCreateViewModel();

            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CinemaCreateViewModel dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var cinema = mapper.Map<Cinema>(dto);
                await cinemaService.CreateAsync(cinema);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(id);
                var result = mapper.Map<CinemaEditViewModel>(cinema);

                return View(result);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CinemaEditViewModel dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var cinema = mapper.Map<Cinema>(dto);
                await cinemaService.UpdateAsync(cinema);

                return RedirectToAction(nameof(Details), new { id = cinema.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(id);
                var result = mapper.Map<CinemaDeleteViewModel>(cinema);

                return View(result);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CinemaDeleteViewModel dto)
        {
            try
            {
                var cinema = await cinemaService.GetByIdAsync(dto.Id);
                await cinemaService.DeleteAsync(cinema);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
