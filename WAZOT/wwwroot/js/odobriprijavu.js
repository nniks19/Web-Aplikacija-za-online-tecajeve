var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblOsoba').DataTable({
        "ajax": {
            "url": "/Administrator/OdobriPrijavu/GetAll"
        },
        "columns": [
            { "data": "oib", "width": "15%" },
            { "data": "ime", "width": "15%" },
            { "data": "prezime", "width": "15%" },
            { "data": "razina_Prava.naziv", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "targets": 1, "data": "odobreno", "render": function (data) { if (data == 0) { return "<div class='text-center'>❌</div>"; } if (data == 1) { return "<div class='text-center'>✔️</div>"; } }, "width": "15%" },
            {
                "data": "oib",
                "render": function (data) {
                    return `<div class="w-75">
                        <a href="/Administrator/OdobriPrijavu/Edit?oib=${data}"
                        class="btn btn-outline-primary mx-2"> <i class="bi bi-check-circle-fill"></i></a>
                    </div> `
                },
                "width": "10%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [6] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}

