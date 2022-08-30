var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblVideozapis').DataTable({
        "ajax": {
            "url": "/Administrator/Videozapis/GetAll"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "videozapis_naziv", "width":"15%"},
            { "data": "tecaj.naziv", "width": "15%" },
            { "data": "videozapis_putanja", "width": "15%" },
            { "data": "videozapis_tip", "width": "15%" },
            { "data": "cjelinaTecaja.naziv_cjeline", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Administrator/Videozapis/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>`
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Administrator/Videozapis/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>`
                },
                "width": "5%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [6,7] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}