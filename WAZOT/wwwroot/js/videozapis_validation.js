function checkAndFilterVideos() {
    var userFileImg = document.getElementById('file');
    var destOrignalFile = userFileImg.value;
    var allowedExtensions = /(\.mp4)$/i;
    if (!allowedExtensions.exec(destOrignalFile)) {
        alert('Možete samo prenijeti .mp4 videozapise!');
        userFileImg.value = '';
        return false;
    }
}