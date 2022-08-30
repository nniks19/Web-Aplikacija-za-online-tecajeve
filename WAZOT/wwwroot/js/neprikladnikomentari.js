var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblNeprikladniKomentari').DataTable({
        "ajax": {
            "url": "/Administrator/NeprikladniKomentari/GetAll"
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
                    return `<a href="/Administrator/NeprikladniKomentari/Unmark?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="fa fa-check"></i></a>`
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Administrator/NeprikladniKomentari/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>`
                },
                "width": "5%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [5,6] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}