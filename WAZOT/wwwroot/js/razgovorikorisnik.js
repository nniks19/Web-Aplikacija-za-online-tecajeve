var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblRazgovoriKorisnik').DataTable({
        "ajax": {
            "url": "/Korisnik/RazgovorKorisnik/GetAll"
        },
        "columns": [
            { "data": "id", "width": "30%" },
            { "targets": 2, "data": "primateljOsoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "30%", },
            { "targets": 2, "data": "posiljateljOsoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "30%", },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Korisnik/RazgovorKorisnik/Pregled?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="fa fa-envelope"></i></a>
                    `
                },
                "width": "10%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [3] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}
