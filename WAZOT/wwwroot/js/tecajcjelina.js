var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblTecaj').DataTable({
        "ajax": {
            "url": "/Administrator/CjelinaTecaja/GetAll"
        },
        "columns": [
            { "data": "id", "width": "19%" },
            { "data": "naziv", "width": "19%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime;} ,"width": "19%",  },
            { "targets": 1, "data": "opis", "render": function (data) { return data.slice(0, 14) + "..."; }, "width": "19%" },
            { "data": "kategorija.naziv", "width": "19%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Administrator/CjelinaTecaja/Cjelina?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-list"></i></a>
                    `
                },
                "width": "5%"
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