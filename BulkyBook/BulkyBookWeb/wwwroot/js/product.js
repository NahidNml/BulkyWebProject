var dataTable;

$(document).ready(function () {

    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "Width": "15%" },
            { "data": "isbn", "Width": "15%" },
            { "data": "price", "Width": "15%" },
            { "data": "author", "Width": "15%" },
            { "data": "category.name", "Width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>Delete
                            </a>
                        </div>
                           `
                },
                "Width": "15%"
            }
        ]
    });
}