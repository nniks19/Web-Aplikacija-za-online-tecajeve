var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblNarudzba').DataTable({
        "ajax": {
            "url": "/Korisnik/Pregled/GetAll"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "tecaj.naziv", "width": "20%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "20%", },
            { "data": "status_narudzbe.naziv", "width":"20%"},
            { "data": "nacin_placanja.naziv", "width": "20%" },
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}