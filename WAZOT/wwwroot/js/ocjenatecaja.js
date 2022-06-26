var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblOcjenaTecaja').DataTable({
        "ajax": {
            "url": "/Administrator/OcjenaTecaja/GetAll"
        },
        "columns": [
            { "data": "id", "width": "18%" },
            { "data": "komentar", "width": "18%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime;} ,"width": "18%",  },
            { "data": "ocjena", "width": "18%" },
            { "data": "tecaj.naziv", "width": "18%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Administrator/OcjenaTecaja/Edit?id=${data}"
                    class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                        <a href="/Administrator/OcjenaTecaja/Delete?id=${data}"
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i></a>
                    </div > `
                },
                "width": "10%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [5] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}