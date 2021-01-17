$(document).ready(function () {
    //$("#message").hide();
    $("#txtname").keyup(function () {
        $("#message").hide();
    });
    $('#btnSave').click(function () {
        var TName = $('#txtname').val();
        if (TName == "" ) {
            $("#message").show();
            return false;
        }
        debugger;
        var data = $('#formid').serialize();
        $.ajax({
            type: "GET",
            url: "/Job/cat?op=Create",

            data: data,
            success: function () {

                $("#TableID").load("/Job/allcats")

                $('#formid').hide();
                $("#formid")[0].reset();
                $("#message").hide();

            }
        });

    })
    $('#open').click(function () {
        $('#formid').show();
    })
    $('#btnClose').click(function () {
        $("#message").hide();
        $("#formid")[0].reset();
        $('#formid').hide();
    })
    $('#btnUpdate').click(function () {
        
        var TName = $('#txtname').val();
      
        if (TName == "" ) {
            $("#message").show();
            return false;

        }
        debugger;
        var data = $('#formid').serialize();
        var id = $('#hdnID').val();
        $.ajax({
            type: "GET",
            url: "/Job/cat?op=Edit&id=" + id,
            data: data,
            success: function () {

                $("#TableID").load("/Job/allcats")
                $("#formid")[0].reset();
                $('#formid').hide();

            }
        });
    })
})


function Edit(id, name) {
    $('#formid').show();
    $('#txtname').val(name);
    $('#hdnID').val(id);
    $('#btnUpdate').show();
    $('#btnSave').hide();

}
function Delete(val) {
    debugger;
    var variable = val;
    $.ajax({
        type: "GET",
        url: "/Job/cat?op=Delete&id=" + variable,
        success: function () {
            debugger;

            $("#TableID").load("/Job/allcats")
            $("#formid")[0].reset();
        }
    });
}

function ModBtn(id) {
    debugger;
    var ID = id;

    $('#myModal').show();
    $('#Close').click(function () {
        $('#myModal').hide();

    })
    $('#Cancel').click(function () {
        $('#myModal').hide();

    })
    $('#ButtonDelete').click(function () {

        var val = ID;
        debugger;
        $.ajax({
            type: "GET",
            url: "/Job/cat?op=Delete&id=" + val,
            success: function () {
                alert('hello');

                debugger;

                $("#TableID").load("/Job/allcats")
                $("#formid")[0].reset();
            }
        });

    })


}


