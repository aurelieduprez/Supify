using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supify.Data;
using Supify.Models;

namespace Supify.Controllers
{
    [Authorize]
    public class SongController : Controller
    {
        private readonly ApplicationDbContext _database;

        public SongController(ApplicationDbContext database)
        {
            _database = database;

        }

        [HttpGet, Route("addSong")]
        public IActionResult addSong()
        {
            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            ViewData["Playlists"] = playlists;
            return View();
        }

        [HttpPost, Route("addSong")]
        public IActionResult addSong(IFormFile file, string name, int playlistId)
        {
            var songPath = "/music/"  + name + ".mp3";

            Song song = new Song()
            {
                Name = name,
                PlaylistId = playlistId,
                Path = songPath
            };
            _database.Song.Add(song);
            _database.SaveChanges();


            using (var fileStream = new FileStream("./wwwroot" + songPath, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            return Redirect("/");
        }


        [HttpGet, Route("deleteSong")]
        public IActionResult deleteSong()
        {
            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            var songs = _database.Song.Where(song => song.PlaylistId.Equals(playlists[0].Id)).ToList();

            ViewData["Playlists"] = playlists;
            ViewData["Songs"] = songs;

            return View();
        }



        [HttpPost, Route("deleteSong")]
        public IActionResult deleteSong(int Id)
        {

                var song = _database.Song.First(s => s.Id == Id);
                _database.Song.Attach(song);
                _database.Song.Remove(song);
                _database.SaveChanges();
            

            return Redirect("/Player");
        }

        [HttpGet, Route("player")]
        public IActionResult Player()
        {
            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            var songs = _database.Song.Where(song => song.PlaylistId.Equals(playlists[0].Id)).ToList();

            ViewData["Playlists"] = playlists;
            ViewData["Songs"] = songs;
            return View();
        }
    }
}