var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblRazgovoriAdmin').DataTable({
        "ajax": {
            "url": "/Administrator/RazgovorAdministrator/GetAll"
        },
        "columns": [
            { "data": "id", "width": "24%" },
            { "data": "naziv", "width": "24%" },
            { "targets": 2, "data": "primateljosoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "24%", },
            { "targets": 2, "data": "posiljateljosoba", "render": function (data) { return data.ime + ' ' + data.prezime; }, "width": "24%", },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Administrator/RazgovorAdministrator/Pregled?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                    `
                },
                "width": "4%"
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
