var playlistSelect = document.getElementById('playlistId');
var songSelect = document.getElementById('Id');

playlistSelect.onchange = function () {
    // Retrive the playlist id
    var playlistId = this.value;

    // Empty the song select
    while (songSelect.options.length > 0) {
        songSelect.remove(0);
    }

    // Put new items into the song select
    fetch(`/get-songs?id=${playlistId}`, { method: 'GET' })
        .then(response => response.json())
        .then(result => {
            console.log(result);
            result.value.forEach(song => {

                var option = document.createElement("option");
                option.text = song.name;
                option.value = song.id;

                songSelect.add(option);
            });
        })
        .catch(error => console.log('error', error));
}