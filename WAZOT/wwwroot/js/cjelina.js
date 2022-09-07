var dataTable;
$(document).ready(function () {
    loadDataTable();
    $(".idzaputanju").attr('href', '/Administrator/CjelinaTecaja/Create?id=' + getUrlParameter("id"));
});

function loadDataTable() {
    dataTable = $('#tblCjelina').DataTable({
        "ajax": {
            "url": "/Administrator/CjelinaTecaja/GetCjeline/" + getUrlParameter("id")
        },
        "columns": [
            { "data": "id", "width": "45%" },
            { "data": "naziv_cjeline", "width": "45%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Administrator/CjelinaTecaja/Edit?id=${data}"
                    class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i></a>`
                },
                "width": "5%",
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Administrator/CjelinaTecaja/Delete?id=${data}"
                        class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i></a>`
                },
                "width": "5%"
            },
        ],
        "columnDefs": [
            { bSortable: false, targets: [2,3] }
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