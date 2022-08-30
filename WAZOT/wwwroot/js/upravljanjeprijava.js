var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblPrijava').DataTable({
        "ajax": {
            "url": "/Kreator_Tecaja/UpravljanjePrijavama/GetAll"
        },
        "columns": [
            { "data": "id", "width": "22%" },
            { "data": "tecaj.naziv", "width": "22%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "22%", },
            { "data": "status_prijave.naziv", "width": "22%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Kreator_Tecaja/UpravljanjePrijavama/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2"> <i class="bi bi-pencil-square"></i></a>`
                },
                "width": "12%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [4] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}