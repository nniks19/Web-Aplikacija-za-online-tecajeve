var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblStatusPrijave').DataTable({
        "ajax": {
            "url": "/Administrator/StatusPrijave/GetAll"
        },
        "columns": [
            { "data": "id", "width": "45%" },
            { "data": "naziv", "width": "45%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Administrator/StatusPrijave/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>`
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Administrator/StatusPrijave/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>`
                },
                "width": "5%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [2,3] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}
