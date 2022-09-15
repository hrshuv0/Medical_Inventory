let dataTable;

$(document).ready(function (){
    $.ajax({
        url:"api/Products",
        cache:false,
        method:"GET",
        success:function (data){
            updateDataTable(data.data);
        }
    });
});

$("#filterId").change(function (){
    let categoryId = document.getElementById('filterId').value;

    $.ajax({
        url:"api/Products?id="+categoryId,
        cache:false,
        method:"GET",
        success:function (data){
            updateDataTable(data.data);
        }
    });
});

function updateDataTable(data) {
    console.log("load data table working!")
    console.log(data);
    
    dataTable = $('#tableId').DataTable({
        "bDestroy": true,
        order: [[5, "ASC"]],
        data:data,
        "columns":[
            {"data":"name", "width":"20%"},
            {"data":"strength"},
            {"data":"generic.name", "sDefaultContent":"" },
            {"data":"category.name", "sDefaultContent":"" },
            {"data": "company.name", "sDefaultContent": "" },
            {"data": "updatedTime", "sDefaultContent": "" },
            {"data":"id",
                "render":function (data)
                {
                    return `
                        <div class="btn-group" role="group">
                             <a href="/Products/Edit?id=${data}"
                                class="btn btn-outline-secondary m-2">
                                     <i class="bi bi-pencil-square w-25"></i> Edit </a>
                                                            
                             <a href="/Products/Details?id=${data}"
                                class="btn btn-outline-info m-2">
                                     <i class="bi bi-pencil-square w-25"></i> Details </a>

                             <a href="/Products/Delete?id=${data}"
                                class="btn btn-outline-warning m-2 w-25"
                                     <i class="bi bi-trash-fill"></i> Delete </a>
                        </div>
                    
                    `
                }
            }            
        ],
        columnDefs: [
            {
                targets: 0,
                className: 'dt-head-center',
            },
            {
                targets: 2,
                className: 'dt-head-center',
            },
            {
                targets: 3,
                className: 'dt-head-center',
            },
            {
                targets: 4,
                className: 'dt-head-center',
            },
            {
                targets: 1,
                className: 'dt-center',
            },
            {
                targets: 5,
                className: 'dt-center',
            },
            
        ],
        language:{
            processing:"loading.....",
            search:"Search "
        }
    });

}