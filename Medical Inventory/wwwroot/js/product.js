let dataTable;

$(document).ready(function (){
    loadDataTable();
});

function loadDataTable() {
    console.log("load data table working!")
    
    dataTable = $('#tableId').DataTable({
        "ajax":{
            "url":"/api/Products"
        },
        "columns":[
            {"data":"name", "width":"20%"},
            {"data":"strength"},
            {"data":"generic.name"},
            {"data":"category.name"},
            {"data":"id",
                "render":function (data)
                {
                    return `
                        <div class="w-75 btn-group" role="group">
                             <a href="/Products/Edit?id=${data}"
                                class="btn btn-secondary m-2">
                                     <i class="bi bi-pencil-square"></i> Edit </a>
                                                            
                             <a href="/Products/Details?id=${data}"
                                class="btn btn-info m-2">
                                     <i class="bi bi-pencil-square"></i> Details </a>

                             <a href="/Products/Delete?id=${data}"
                                class="btn btn-warning m-2"
                                     <i class="bi bi-trash-fill"></i> Delete </a>
                        </div>
                    
                    `
                }
            }
            
        ]
    });
    console.log(dataTable);

}