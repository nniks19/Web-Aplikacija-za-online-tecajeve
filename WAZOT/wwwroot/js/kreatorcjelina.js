var dataTable;
$(document).ready(function () {
    loadDataTable();
    $(".idzaputanju").attr('href', '/Kreator_Tecaja/KreatorCjelinaTecaja/Create?id=' + getUrlParameter("id"));
});

function loadDataTable() {
    dataTable = $('#tblKreatorCjelina').DataTable({
        "ajax": {
            "url": "/Kreator_Tecaja/KreatorCjelinaTecaja/GetCjeline/" + getUrlParameter("id")
        },
        "columns": [
            { "data": "id", "width": "47%" },
            { "data": "naziv_cjeline", "width": "47%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Kreator_Tecaja/KreatorCjelinaTecaja/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                   <a href="/Kreator_Tecaja/KreatorCjelinaTecaja/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a></div> `
                },
                "width": "6%"
            }
        ],
        "columnDefs": [
            { bSortable: false, targets: [2] }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.12.0/i18n/hr.json"
        }
    });
}
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
    return false;
};