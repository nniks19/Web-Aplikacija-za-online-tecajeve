var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblNarudzba').DataTable({
        "ajax": {
            "url": "/Kreator_Tecaja/UpravljanjeNarudzbama/GetAll"
        },
        "columns": [
            { "data": "id", "width": "18%" },
            { "data": "tecaj.naziv", "width": "18%" },
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "15%", },
            { "data": "status_narudzbe.naziv", "width":"18%"},
            { "data": "nacin_placanja.naziv", "width": "18%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Kreator_Tecaja/UpravljanjeNarudzbama/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
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