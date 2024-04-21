$(document).ready(function () {
    let _id; 

    $(".btn-delete").click(function () {
        _id = $(this).data("id");


        $("#deleteConfirmationModal").modal("show");
    });

 
    $("#confirmDeleteBtn").click(function () {
        $.ajax({
            url: "/admin/blogs/delete",
            type: "POST",
            data: { id: _id },
            success: function (result) {
                if (result) {
                    $("#tr_" + _id).remove();
                    toastr.success('Xóa thành công');
                } else {
                    toastr.error('Xóa không thành công');
                }
            }


        });

        $("#deleteConfirmationModal").modal("hide");
    });

   
});

