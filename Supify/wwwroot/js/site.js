﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



// buttons to manage the music player
var buttons = {
    back: document.getElementById('btn-back'),
    backward: document.getElementById('btn-backward'),
    playpause: document.getElementById('btn-playpause'),
    forward: document.getElementById('btn-forward'),
    next: document.getElementById('btn-next'),
    mute: document.getElementById('btn-mute')
}

var playlistSelect = document.getElementById('playlistId');
var isPlaying = false;

// create the container ane waveform parameters
var wavesurfer = WaveSurfer.create({
    container: '#waveform',
    barWidth: 5,
    barRadius: 1,
    cursorWidth: 0,
    height: 200,
    hideScrollbar: true
});


// volume cursor
document.querySelector('#volume').value = wavesurfer.backend.getVolume();
var volumeInput = document.querySelector('#volume');
var onChangeVolume = function (e) {
    wavesurfer.setVolume(e.target.value);
    console.log(e.target.value);
};
volumeInput.addEventListener('input', onChangeVolume);

// pause button
function playpause() {
    if (isPlaying) { wavesurfer.pause(); }
    else { wavesurfer.play(); }
    buttons.forward.disabled = !buttons.forward.disabled;
    buttons.backward.disabled = !buttons.backward.disabled;
    isPlaying = !isPlaying;
}
buttons.playpause.addEventListener("click", playpause);

// mute button
buttons.mute.addEventListener("click", () => { wavesurfer.setMute(!wavesurfer.getMute()) });

// forward button
buttons.forward.addEventListener("click", function () { wavesurfer.skipForward(5); });

// backward button
buttons.backward.addEventListener("click", function () { wavesurfer.skipBackward(5); });

// setting parameters for the waveform
wavesurfer.on('ready', function () {
    buttons.playpause.disabled = false;
    wavesurfer.setProgressColor("#415a77");
    wavesurfer.setWaveColor("#778da9");
    wavesurfer.setVolume(0.2);
    wavesurfer.zoom(1);
});


// load the right song and activate buttons
function loadToPlayer(path) {
    buttons.backward.disabled = true;
    buttons.forward.disabled = true;
    buttons.playpause.disabled = true;
    isPlaying = false;
    wavesurfer.load(path);
}

// if playlist id is changed
playlistSelect.onchange = function () {
    var playlistId = this.value;
    // to know which song is selected
    songs = document.getElementById("songs");

    // if there is no song
    while (songs.firstChild) {
        songs.removeChild(songs.firstChild);
    }

    // list of songs, will fetch the songs of the playlist with its id, and put it in a json
fetch(`/get-songs?id=${playlistId}`, { method: 'GET' }) //using my get request in the playlistController to get the right songs
        .then(response => response.json()) 
        .then(result => {
            result.value.forEach(song => //Decompose the json into each song 
            {

                var li = document.createElement("li"); //this are the elements in the HTML to add for each song to make a list out of them
                var p = document.createElement("p");
                var button = document.createElement("button");

                p.className = 'name';
                p.innerText = song.name;

                button.addEventListener("click", () => { loadToPlayer(song.path) }); //using the function declared above here loadToPlayer
                button.innerText = 'Load';
                button.className = 'btn btn-outline-dark'

                li.appendChild(p);
                li.appendChild(button);

                songs.appendChild(li);
            });
        })
        .catch(error => console.log('error', error));
}

//this is a failed attempt on dark mode
$('#switch').click(
    () => {   
    location.reload();}


    
);


