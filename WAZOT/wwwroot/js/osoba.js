var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblOsoba').DataTable({
        "ajax": {
            "url": "/Administrator/Osoba/GetAll"
        },
        "columns": [
            { "data": "oib", "width": "15%" },
            { "data": "ime", "width": "15%" },
            { "data": "prezime", "width": "15%" },
            { "data": "razina_Prava.naziv", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "lozinka", "width": "15%" },
            {
                "data": "oib",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Administrator/Osoba/Edit?oib=${data}"
                    class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                        <a href="/Administrator/Osoba/Delete?oib=${data}"
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i></a>
                    </div > `
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

/*
 * @foreach(var obj in Model)
{
    <tr>
        <td width="50%">
            @obj.Id
        </td>
        <td width="30%">
            @obj.Naziv
        </td>
        <td>
            <div class="w-75 btn-group" role="group">
                <a asp-controller="RazinePrava" asp-action="Edit" asp-route-id="@obj.Id"><i class="bi bi-pencil-square"></i> Uredi</a>
            </div>
        </td>
        <td>
            <div class="w-75 btn-group" role="group">
                <a asp-controller="RazinePrava" asp-action="Delete" asp-route-id="@obj.Id"><i class="bi bi-trash3"></i> Obriši</a>
            </div>
        </td>
    </tr>
}
*/