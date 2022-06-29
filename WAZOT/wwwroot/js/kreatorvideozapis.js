var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblKreatorVideozapis').DataTable({
        "ajax": {
            "url": "/Kreator_Tecaja/KreatorVideozapis/GetAll"
        },
        "columns": [
            { "data": "id", "width": "18%" },
            { "data": "videozapis_naziv", "width":"18%"},
            { "data": "tecaj.naziv", "width": "18%" },
            { "data": "videozapis_putanja", "width": "18%" },
            { "data": "videozapis_tip", "width": "18%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Kreator_Tecaja/KreatorVideozapis/Edit?id=${data}"
                    class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                        <a href="/Kreator_Tecaja/KreatorVideozapis/Delete?id=${data}"
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i></a>
                    </div > `
                },
                "width": "10%"
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