$(document).ready(function () {
    $('#tableData').DataTable();
});

$(document).ready(function () {
    $('#gridData').DataTable({
        paging: false,
        searching: false,
        ordering: false,
        info: false
    });
});