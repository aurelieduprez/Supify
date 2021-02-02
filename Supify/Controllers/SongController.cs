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

        public SongController(ApplicationDbContext database) //fetch db context
        {
            _database = database;

        }

        [HttpGet, Route("addSong")] //indicates the type of request for this route
        public IActionResult addSong() // since there is no argument here it becomes a "get" request
        {
            //fetch every playlists of this user
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            ViewData["Playlists"] = playlists; //sends them to the view to make a list out of them
            return View(); //return view
        }

        [HttpPost, Route("addSong")]// indicates its the POST request of this route
        public IActionResult addSong(IFormFile file, string name, int playlistId) //see, here there is an argument so it's a POST. It receives all the infos entered in the view
        {
            var songPath = "/music/"  + name + ".mp3"; //every path will be composed like this

            Song song = new Song() // new song from model SongModel
            {
                Name = name, //get all of this information from the view and associate them to the song Model
                PlaylistId = playlistId,
                Path = songPath
            };
            _database.Song.Add(song); //add this song thanks to the model
            _database.SaveChanges();  //and save db changes


            using (var fileStream = new FileStream("./wwwroot" + songPath, FileMode.Create, FileAccess.Write)) 
            {
                file.CopyTo(fileStream); //copy the file that was sent to this path
            }

            return Redirect("/");
        }


        [HttpGet, Route("deleteSong")]  //indicates its the GET request of the deleteSong route
        public IActionResult deleteSong()
        {
            //fetch every playlists of this user
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            //fetch every songs of this user
            List<Song> songs;
            if (playlists.Count > 0) {            
                songs = _database.Song.Where(song => song.PlaylistId.Equals(playlists[0].Id)).ToList(); 
            }
            else
            {
                songs = new List<Song>() ; 
            }


            ViewData["Playlists"] = playlists;  //send every playlist var to the view
            ViewData["Songs"] = songs;          //send every song frome every playlist of this user to the view

            return View();
        }



        [HttpPost, Route("deleteSong")] //concerning the POST request of this route : 
        public IActionResult deleteSong(int Id) //receiving this id of the song to delete
        {

                var song = _database.Song.First(s => s.Id == Id); //select the matching song to delete and put it in the song var
                _database.Song.Attach(song); //select
                _database.Song.Remove(song); //delete
                _database.SaveChanges();     //save changes in db
            

            return Redirect("/Player"); //once it's done return to the player view
        }

        [HttpGet, Route("Player")]
        public IActionResult Player()
        {
            //get all playlist of this user
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name) ).ToList();
            //get all songs from every playlist of this user
            var songs = _database.Song.Where(song => song.PlaylistId.Equals(playlists[0].Id)).ToList();
            /*List<Song> songs;
            if (playlists.Count > 0)
            {

            }
            else
            {
                songs = new List<Song>();
            }*/
            //send the playlists and songs to the view
            ViewData["Playlists"] = playlists;
            ViewData["Songs"] = songs;
            return View();
        }


    }
}