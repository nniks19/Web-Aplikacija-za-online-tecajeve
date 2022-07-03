function checkAndFilterImages() {
    var userFileImg = document.getElementById('file');
    var destOrignalFile = userFileImg.value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (!allowedExtensions.exec(destOrignalFile)) {
        alert('Možete samo prenijeti .jpeg/.jpg/.png/ slike!');
        userFileImg.value = '';
        return false;
    }
}
