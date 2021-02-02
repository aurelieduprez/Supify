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
        public PlaylistController(ApplicationDbContext database)  //fetch db context in _database
        {
            _database = database;
        }

        // post request to create a playlist
        public IActionResult createPlaylist(Playlist playlist) //with the model Playlist as an argument
        {

            if (User.Identity.IsAuthenticated == false) // if the user is not connected, don't go further
            {
                return Redirect("/Home");

            }
            else
            {
                if (playlist.Name != null) //checks if the name is not null
                { 
                playlist.User = User.Identity.Name; //catches info about user 
                _database.Playlist.Add(playlist);  // adds the playlist to db
                _database.SaveChanges();           // makes sure to save changes in db 
                }

            ViewData["Playlist"] = playlist; //send this var to the view, so we can use it there

                return View();
            }

        }
        
        //post requets to delete a playlist
        public IActionResult deletePlaylist(Playlist playlist) //same argument
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                _database.Playlist.Attach(playlist); //fetch the concerned playlist
                _database.Playlist.Remove(playlist);  // and remove it
                _database.SaveChanges();            // save the changes

                // fetch from db every playlists belonging to this user, after removing the one above, it's like a refresh
                var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
                ViewData["Playlists"] = playlists;  //sends var to view
                return View();      // and return the view with the same name as this function since there is not argument (deletePlaylist)
            }


        }
        public IActionResult allPlaylist(Playlist playlist) //same argument
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                //fetch every playlist from this user, this route is no longer used but useful
                var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
                ViewData["Playlists"] = playlists;
                return View();

            }


        }

        [HttpGet, Route("get-songs")]
        public IActionResult Get(string id)
        {
            // Check if we get a playlist
            try
            {
                var playlist = _database.Playlist.First(p => p.Id == Int32.Parse(id));
                if (playlist != null)
                {
                    var songs = _database.Song.Where(song => song.PlaylistId.Equals(playlist.Id));
                    if (songs.Any())
                    {
                        return Ok(Json(songs));
                    }
                    else
                    {
                        return NotFound("empty");
                    }
                }
            }
            catch (Exception e)
            {
                return NotFound("playlist does not exists");
            }
            return NotFound();
        }
    }
}