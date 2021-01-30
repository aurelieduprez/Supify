using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supify.Models;
using Supify.Data;

namespace Supify.Controllers
{
    [Authorize]
    public class PlaylistController : Controller
    {
        private readonly ApplicationDbContext _database;
        public PlaylistController(ApplicationDbContext database)
        {
            _database = database;
        }

 /*       [HttpGet, Route("createPlaylist")]
        public IActionResult createPlaylist()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                return View();
            }

        }

        [HttpPost, Route("createPlaylist")]*/
        public IActionResult createPlaylist(Playlist playlist)
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                if (playlist.Name != null) 
                { 
                playlist.User = User.Identity.Name;
                _database.Playlist.Add(playlist);
                _database.SaveChanges();
                }

            ViewData["Playlist"] = playlist;

                return View();
            }

        }
        
        public IActionResult deletePlaylist(Playlist playlist)
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                _database.Playlist.Attach(playlist);
                _database.Playlist.Remove(playlist);
                _database.SaveChanges();

                // Retrive all user's playlist from the database
                var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
                ViewData["Playlists"] = playlists;
                return View();
            }


        }
        public IActionResult allPlaylist(Playlist playlist)
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
                ViewData["Playlists"] = playlists;
                return View();

            }


        }
    }
}