//$('.modal-trigger').leanModal();
$('select').material_select();
$(".button-collapse").sideNav();

/*$('.datepicker').pickadate({
selectMonths: true,
selectYears: 150,
format: 'yyyy-mm-dd'
});*/

$('.datepicker_pdate').pickadate({
    minDate: 0,
    selectMonths: true, // Creates a dropdown to control month
    selectYears: 100,
    format: 'yyyy-mm-dd',
    min: new Date()
});

$('.datepicker_dob').pickadate({
    maxDate: 0,
    selectMonths: true, // Creates a dropdown to control month
    selectYears: 100,
    format: 'yyyy-mm-dd',
    max: new Date()
});

$('.datepicker_dod').pickadate({
    maxDate: 0,
    selectMonths: true, // Creates a dropdown to control month
    selectYears: 100,
    format: 'yyyy-mm-dd',
    max: new Date()
});






$(function () {
    $('#txt_name').keydown(function (e) {
        if (e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                e.preventDefault();
            }
        }
    });

    $('#txt_occup').keydown(function (e) {
        if (e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                e.preventDefault();
            }
        }
    });

    var pattern = /^[a-zA-Z!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]*$/
    $('#txt_knok').keyup(function (e) {
        var v = this.value;
        if (!v.match(pattern)) {
            //chop off the last char entered
            this.value = this.value.slice(0, -1);
        }
    });
});



//Function to allow only numbers to textbox
function validate(key) {
    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    var phn = document.getElementById('txt_nok');
    //comparing pressed keycodes
    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
        return false;
    }
    else {
        //Condition to check textbox contains ten numbers or not
        if (phn.value.length < 10) {
            return true;
        }
        else {
            return false;
        }
    }
}


$("#txt_cont").focusout(function () {
    var textbox = document.getElementById("txt_cont");
    if (textbox.value.length <= 8 && textbox.value.length >= 8) {
        //alert("success");
    }
    else {
        Materialize.toast('Please enter valid mobile number', 2000);
        $("#txt_cont").focus();
    }
});


$("#txt_nok").focusout(function () {
    var txtbox = document.getElementById("txt_nok");
    if (txtbox.value.length <= 8 && txtbox.value.length >= 8) {
        //alert("success");
    }
    else {
        Materialize.toast('Please enter valid mobile number', 2000);
        $("#txt_nok").focus();
    }
});

$("#txt_NRIC").focusout(function () {
    var txbox = document.getElementById("txt_NRIC");
    if (txbox.value.length <= 9 && txbox.value.length >= 9) {
        //alert("success");
    }
    else {
        Materialize.toast('Please enter valid NRIC value', 2000);
        $("#txt_NRIC").focus();
    }
});

//Function to allow only numbers to textbox
function validate1(key) {
    var keycode = (key.which) ? key.which : key.keyCode;
    var phn = document.getElementById('txt_cont');
    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
        return false;
    }
    else {
        if (phn.value.length < 10) {
            return true;
        }
        else {
            return false;
        }
    }
}

$(function () {

    $('#txt_NRIC').keyup(function () {
        var yourInput = $(this).val();
        re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
        var isSplChar = re.test(yourInput);
        if (isSplChar) {
            var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
            $(this).val(no_spl_char);
        }
    });
    $('#txt_name').keyup(function () {
        var yourInput = $(this).val();
        re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
        var isSplChar = re.test(yourInput);
        if (isSplChar) {
            var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
            $(this).val(no_spl_char);
        }
    });


});


//file upload works
$(function () {
    $('#nricF').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#nricF').val() != '') {
            var file = $('#nricF')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#nricF").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                console.log(binimage);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveNRICfront",
                    data: "{ 'Based64BinaryString' :'" + binimage + "', 'admision_slno' :'" + admision_slno + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        $("#nricFT").empty();
                        $("#nricFT").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        alert("File size is too large");
                        $("#nricF").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#nricF").val("");
        }
    });

    $('#nricB').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#nricB').val() != '') {
            var file = $('#nricB')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#nricB").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveNRICback",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        $("#nricBT").empty();
                        $("#nricBT").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#nricB").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#nricB").val("");
        }
    });

    //file 1 to 10
    $('#f1').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f1').val() != '') {
            var file = $('#f1')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f1").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile1",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        $("#f1T").empty();
                        $("#f1T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f1").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f1").val("");
        }
    });

    $('#f2').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f2').val() != '') {
            var file = $('#f2')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f2").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile2",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f2T").empty();
                        $("#f2T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f2").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f2").val("");
        }
    });

    $('#f3').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f3').val() != '') {
            var file = $('#f3')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f3").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile3",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f3T").empty();
                        $("#f3T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f3").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f3").val("");
        }
    });

    $('#f4').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f4').val() != '') {
            var file = $('#f4')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f4").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile4",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f4T").empty();
                        $("#f4T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f4").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f4").val("");
        }
    });

    $('#f5').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f5').val() != '') {
            var file = $('#f5')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f5").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile5",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f5T").empty();
                        $("#f5T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f5").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f5").val("");
        }
    });

    $('#f6').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f6').val() != '') {
            var file = $('#f6')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f6").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile6",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f6T").empty();
                        $("#f6T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f6").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f6").val("");
        }
    });

    $('#f7').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f7').val() != '') {
            var file = $('#f7')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f7").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile7",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f7T").empty();
                        $("#f7T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f7").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f7").val("");
        }
    });

    $('#f8').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f8').val() != '') {
            var file = $('#f8')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f8").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile8",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f8T").empty();
                        $("#f8T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f8").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f8").val("");
        }
    });

    $('#f9').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f9').val() != '') {
            var file = $('#f9')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f9").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile9",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f9T").empty();
                        $("#f9T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f9").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f9").val("");
        }
    });

    $('#f10').change(function () {
        var admision_slno = localStorage.getItem('totApp');
        if ($('#f10').val() != '') {
            var file = $('#f10')[0].files[0];
            var fileName = file.name;
            var fileExt = '.' + fileName.split('.').pop();
        }
        else {
            alert('Please select a file.')
        }

        if (fileExt == ".png" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".PNG" || fileExt == ".PDF" || fileExt == ".JPG" || fileExt == ".JPEG") {
            var file = $("#f10").get(0).files[0];
            var r = new FileReader();
            r.onload = function () {
                var binimage = r.result;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Functions.aspx/SaveFile10",
                    data: "{ 'Based64BinaryString' :'" + binimage + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        //alert(res);
                        $("#f10T").empty();
                        $("#f10T").append(res);
                        Materialize.toast('Upload success..!', 3000);
                    },
                    error: function (result) {
                        //alert(result.d);
                        alert("File size is too large");
                        $("#f10").val("");
                    }
                });
            };
            r.readAsDataURL(file);
        } else {
            alert("Please select correct file...!");
            $("#f10").val("");
        }
    });


});
//file upload end





