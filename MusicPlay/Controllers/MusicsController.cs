﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPlay.Models;

namespace MusicPlay.Controllers
{
    public class MusicsController : Controller
    {
        private readonly MusicPlayContext _context;

        public MusicsController(MusicPlayContext context)
        {
            _context = context;
        }

        // GET: Musics
        public async Task<IActionResult> Index(string musicGenre, string searchString)
        {
            //var musics = from m in _context.Music select m;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    musics = musics.Where(s => s.Title.Contains(searchString));
            //}
            //return View(await musics.ToListAsync());

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Music
                                            orderby m.Genre
                                            select m.Genre;

            var musics = from m in _context.Music
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                musics = musics.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(musicGenre))
            {
                musics = musics.Where(x => x.Genre == musicGenre);
            }

            var musicGenreVM = new MusicGenreViewModel();
            musicGenreVM.genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            musicGenreVM.musics = await musics.ToListAsync();

            return View(musicGenreVM);
        }

        // GET: Musics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music
                .SingleOrDefaultAsync(m => m.ID == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // GET: Musics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Rating")] Music music)
        {
            if (ModelState.IsValid)
            {
                _context.Add(music);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        // GET: Musics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music.SingleOrDefaultAsync(m => m.ID == id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }

        // POST: Musics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Rating")] Music music)
        {
            if (id != music.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(music);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicExists(music.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        // GET: Musics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music
                .SingleOrDefaultAsync(m => m.ID == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var music = await _context.Music.SingleOrDefaultAsync(m => m.ID == id);
            _context.Music.Remove(music);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicExists(int id)
        {
            return _context.Music.Any(e => e.ID == id);
        }
    }
}
