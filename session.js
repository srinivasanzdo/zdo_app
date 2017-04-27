function getSessionName(user_id) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Functions.aspx/GetSessionAgent",
        data: "{dagent_id:'" + user_id + "'}",
        dataType: "json",
        //async: false,
        success: function (data) {
            $("#SesName").empty();
            $("#SesName").append(data.d[0].agent_name);

            $("#SesName1").empty();
            $("#SesName1").append(data.d[0].agent_name);

            var agPh = data.d[0].agent_name;

            localStorage.setItem('agentPh', agPh);


        },
        error: function (result) {
            //alert("Error......");
        }
    });
}

function myHead() {
    $("#head").empty();
    $("#head").append('<nav class="blue lighten-1" role="navigation">'
        + '<div class="nav-wrapper"><a id="logo-container" href="#" class="brand-logo" style="margin-left:55px;">ZDO</a>'
        + '<ul class="right hide-on-med-and-down" style="margin-right:55px;">'
        + '<li><a href="dashboad.html">DASHBOAD</a></li>'
        + '<li><a href="client_list.html">CLIENTS</a></li>'
        + '<li>'
        + '<a class="dropdown-button" data-activates="Sales_agent" data-beloworigin="true" href="javascript:;"><span id="SesName"></span><i class="material-icons right">arrow_drop_down</i></a>'
        + '<ul id="Sales_agent" class="dropdown-content" style="width: 191px; position: absolute; top: 64px; left: 619.875px; opacity: 1; display: none;">'
        + '<li><a href="index.html">LOG OUT</a></li>'
        + '</ul>'
        + '</li>'
        + '</ul>'

        + '<ul id="nav-mobile" class="side-nav">'
        + '<li><a href="#"><span id="SesName1"></span></a></li>'
        + '<li><a href="#">DASHBOAD</a></li>'
        + '<li><a href="client_list.html">CLIENTS</a></li>'
        + '<li><a href="index.html">LOGOUT</a></li>'
        + '</ul>'
        + '<a href="#" data-activates="nav-mobile" class="button-collapse"><i class="material-icons">menu</i></a>'
        + '</div>'
        + '</nav>');
}

function spinnerShow() {
    $("#gifer").append('<center><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />'
    + '<div class="preloader-wrapper small active">'
    + '<div class="spinner-layer spinner-green-only">'
    + '<div class="circle-clipper left">'
    + '<div class="circle"></div>'
    + '</div><div class="gap-patch">'
    + '<div class="circle"></div>'
    + '</div><div class="circle-clipper right">'
    + '<div class="circle"></div>'
    + '</div>'
    + '</div>'
    + '</div>'
    + '</center><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />');
}

function spinnerHide() {
    $("#gifer").empty();
}