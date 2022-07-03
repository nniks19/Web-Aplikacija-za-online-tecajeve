var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblTecaj').DataTable({
        "ajax": {
            "url": "/Administrator/Tecaj/GetAll"
        },
        "columns": [
            { "data": "id", "width": "14%" },
            { "data": "naziv", "width": "14%" },
            { "data": "cijena", "width": "14%"},
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime;} ,"width": "14%",  },
            { "targets": 1, "data": "opis", "render": function (data) { return data.slice(0, 14) + "..."; }, "width": "14%" },
            { "data": "kategorija.naziv", "width": "14%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Administrator/Tecaj/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                        <a href="/Administrator/Tecaj/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>
                    </div > `
                },
                "width": "14%"
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