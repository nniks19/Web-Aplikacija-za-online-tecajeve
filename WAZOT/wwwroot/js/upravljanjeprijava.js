var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblPrijava').DataTable({
        "ajax": {
            "url": "/Kreator_Tecaja/UpravljanjePrijavama/GetAll"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "tecaj.naziv", "width": "20%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "19%", },
            { "data": "status_prijave.naziv", "width":"20%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Kreator_Tecaja/UpravljanjePrijavama/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                    </div > `
                },
                "width": "18%"
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