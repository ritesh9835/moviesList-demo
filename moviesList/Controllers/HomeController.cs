using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moviesList.DbEntities;
using moviesList.Models;
using moviesList.ViewModel;

namespace moviesList.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public HomeController(MovieContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            if (_context.Movies.Count() == 0)
            {
                //just to create DB
            }
        }

        public async Task<IActionResult> Index()
        {
            var moviesFrmDb = await _context.Movies.ToListAsync();
            var moviesList = new List<MoviesList>();
            foreach (var val in moviesFrmDb)
            {
                var movies = new MoviesList();
                movies.Name = val.Name;
                movies.Id = val.Id;
                movies.ReleaseYear = val.ReleaseDate.Year;
                moviesList.Add(movies);
            }
            return View(moviesList);
        }


        public PartialViewResult _ManageMovie(int id)
        {
            ManageMovie model = new ManageMovie();
            if (id != 0)
            {
                Movies movie = _context.Set<Movies>().SingleOrDefault(c => c.Id == id);
                if (movie != null)
                {
                    model.Id = movie.Id;
                    model.Name = movie.Name;
                    model.Plot = movie.Plot;
                    model.Poster = "/uploads/img/" + movie.Poster;
                    model.ReleaseDate = movie.ReleaseDate;
                }
            }
            return PartialView("~/Views/Home/_ManageMovie.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _ManageMovie(ManageMovie model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = model.Id == 0 ? true : false;
                    Movies movieModel = isNew ? new Movies
                    {
                        CreatedOn = DateTime.Now
                    } : _context.Set<Movies>().SingleOrDefault(s => s.Id == model.Id);


                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        foreach (var Image in files)
                        {
                            if (Image != null && Image.Length > 0)
                            {
                                var file = Image;
                                //There is an error here
                                var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads/img");
                                if (file.Length > 0)
                                {
                                    
                                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                                    //delete previous file
                                    if (System.IO.File.Exists(@Path.Combine(_appEnvironment.WebRootPath, "uploads/img/") + movieModel.Poster))
                                    {
                                        System.IO.File.Delete(@Path.Combine(_appEnvironment.WebRootPath, "uploads/img/") + movieModel.Poster);
                                    }
                                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                    {
                                        await file.CopyToAsync(fileStream);
                                        movieModel.Poster = fileName;
                                    }


                                }
                            }
                        }
                    }

                    movieModel.Name = model.Name;
                    movieModel.ReleaseDate = model.ReleaseDate;
                    movieModel.Plot = model.Plot;
                    // movieModel.Poster = model.Poster;
                    movieModel.CreatedOn = DateTime.Now;

                    if (isNew)
                    {
                        _context.Add(movieModel);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction("Index");
        }

        public IActionResult _DeleteMovie(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            ManageMovie model = new ManageMovie();
            if (id.HasValue)
            {
                Movies movie = _context.Set<Movies>().SingleOrDefault(c => c.Id == id.Value);
                if (movie != null)
                {
                    model.Id = movie.Id;
                    model.Name = movie.Name;
                }
            }
            return PartialView("~/Views/Home/_DeleteMovie.cshtml", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult _DeleteMovie(ManageMovie model)
        {
            Movies movies = _context.Set<Movies>().SingleOrDefault(c => c.Id == model.Id);
            _context.Entry(movies).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
