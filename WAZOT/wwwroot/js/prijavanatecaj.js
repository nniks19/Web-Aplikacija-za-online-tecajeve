var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblPrijava').DataTable({
        "ajax": {
            "url": "/Administrator/PrijavaNaTecaj/GetAll"
        },
        "columns": [
            { "data": "id", "width": "22.5%" },
            { "data": "tecaj.naziv", "width": "22.5%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "22.5%", },
            { "data": "status_prijave.naziv", "width":"22.5%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Administrator/PrijavaNaTecaj/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>`
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Administrator/PrijavaNaTecaj/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>`
                },
                "width": "5%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [4,5] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}