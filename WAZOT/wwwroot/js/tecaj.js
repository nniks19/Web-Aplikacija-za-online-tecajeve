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
            { "data": "id", "width": "12%" },
            { "data": "naziv", "width": "13%" },
            { "data": "cijena", "width": "12%"},
            { "targets": 2, "data": "osoba", "render": function (data) { return data.ime + ' ' + data.prezime;} ,"width": "13%",  },
            { "data": "opis", "width": "12%" },
            { "data": "prosjecna_ocjena", "width": "12%" },
            { "data": "kategorija.naziv", "width": "13%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Administrator/Tecaj/Edit?id=${data}"
                    class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                        <a href="/Administrator/Tecaj/Delete?id=${data}"
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i></a>
                    </div > `
                },
                "width": "13%"
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