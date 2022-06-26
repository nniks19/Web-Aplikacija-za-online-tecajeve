var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblRazinaPrava').DataTable({
        "ajax": {
            "url": "/Administrator/RazinePrava/GetAll"
        },
        "columns": [
            { "data": "id", "width": "45%" },
            { "data": "naziv", "width": "45%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Administrator/RazinePrava/Edit?id=${data}"
                    class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                        <a href="/Administrator/RazinePrava/Delete?id=${data}"
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i></a>
                    </div > `
                },
                "width": "10%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [2] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}
