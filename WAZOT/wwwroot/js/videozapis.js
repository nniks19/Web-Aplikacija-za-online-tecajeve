﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblVideozapis').DataTable({
        "ajax": {
            "url": "/Administrator/Videozapis/GetAll"
        },
        "columns": [
            { "data": "id", "width": "18%" },
            { "data": "videozapis_naziv", "width":"18%"},
            { "data": "tecaj.naziv", "width": "18%" },
            { "data": "videozapis_putanja", "width": "18%" },
            { "data": "videozapis_tip", "width": "18%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Administrator/Videozapis/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                        <a href="/Administrator/Videozapis/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>
                    </div > `
                },
                "width": "10%"
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