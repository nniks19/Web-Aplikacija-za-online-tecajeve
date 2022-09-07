$(document).ready(function () {
    loadDataCards();
});

function loadDataCards() {
    $.ajax({
        method: "GET",
        url: "/Korisnik/TecajPrijava/getall"
    })
        .done(function (data) {
            if (data.data.length == 0) {
                $("#kontenjer_tecajevi").html(`<h2 class="mt-4 mb-4 text-center">Nemate ni jedan tečaj koji možete pretpregledati!</h2>`);
            } else {
                //console.log(data.data);
                for (var i = 0; i < data.data.length; i++) {
                    console.log(data.data[i]);
                    $("#kontenjer_tecajevi").append(buildCard(data.data[i].slika, data.data[i].naziv, data.data[i].opis, data.data[i].id, data.data[i].prosjecna_ocjena));
                    if (i + 1 == data.data.length) {
                        $("#kontenjer_tecajevi").append("</div>");
                    }
                }
                
            }
        })
        .fail(function () {
            alert("Dogodila se pogreška!");
        })
}
function buildCard(slika, naziv, opis, id, prosjecna_ocjena) {
    return `
        <div class="col-md-6 col-lg-4">
            <!--Bootstrap 5 card box-->
        <div class="card-box">
            <div class="card-thumbnail">
                <img src="` + slika + `" class="img-fluid" alt="` + naziv + `">
                </div>
                <h3><a href="/Korisnik/TecajPrijava/Preview?id=${id}" class="mt-2 card-title">` + naziv + `</a></h3>
                <p class="card-text">` + opis.slice(0, 134) + `...</p>
              <div class="btn-wrapper text-center d-flex justify-content-between">
                <a  href="/Korisnik/TecajPrijava/Preview?id=${id}" class="btn btn-secondary  btn-sm d-flex align-items-center">Pregledaj tečaj</a>
                <a class="btn btn-warning" style="">Prosječna ocjena ${prosjecna_ocjena}</a>
              </div>
            </div>
        </div>
    `;
}
