var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblTecaj').DataTable({
        "ajax": {
            "url": "/Kreator_Tecaja/PopisTecaja/GetAll"
        },
        "columns": [
            { "data": "id", "width": "18%" },
            { "data": "naziv", "width": "18%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "18%", },
            { "targets": 1, "data": "opis", "render": function (data) { return data.slice(0, 14) + "..."; }, "width": "18%" },
            { "data": "kategorija.naziv", "width": "18%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Kreator_Tecaja/PopisTecaja/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a> `
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Kreator_Tecaja/PopisTecaja/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>`
                },
                "width": "5%"
            },
        ],
        "columnDefs": [
            { bSortable: false, targets: [5,6] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}