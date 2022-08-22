var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblPrijava').DataTable({
        "ajax": {
            "url": "/Korisnik/Pregled/GetAll"
        },
        "columns": [
            { "data": "id", "width": "25%" },
            { "data": "tecaj.naziv", "width": "25%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "25%", },
            { "data": "status_prijave.naziv", "width":"25%"},
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}