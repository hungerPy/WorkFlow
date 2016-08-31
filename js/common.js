$(function () {
    var pathname = window.location.pathname;
    var start = pathname.lastIndexOf("/");
    var end = pathname.lastIndexOf(".aspx");
    start = parseInt(start, 10) + 1;
    end = parseInt(end, 10) + 5;
    var res = pathname.substring(start, end);
    if (res.indexOf("-") != -1) {
        res = res.substring(parseInt(res.indexOf("-"), 10) + 1);
    }
    res = res.toLowerCase();
    var ele1 = $('.rootmenu a[href="' + res + '"]');
    if ($(ele1).length == 1) {
        $(ele1).closest(".rootmenu").addClass("active");
    }

});
function SelectAllChk() {
    var count = 0;
    $(".table tr").find("input:checkbox").each(function () {
        if ($(this).parent().attr('class') == 'headercheckbox') {
            if (this.checked)
            { count = 1; }
        }
        if (count == 1) {
            if ($(this).attr('disabled') == true) {
                $(this).attr('checked', false);
            }
            else {
                $(this).attr('checked', true);
                
            }
        }
        else {
            $(this).attr('checked', false);
        }
    }
 )
}


function UnSelectMainChk() {
    $(".table tr").find("input:checkbox").each(function () {
        if (!this.checked) {
            $(".headercheckbox > input:checkbox").attr('checked', false);
        }
    }) 
}


function ItemSelect() {
    var count = 0;
    $(".table tr").find("input:checkbox").each(function () {
        if (this.checked) {
            count++;
        }
    });
    if (count == 0) {
        alert('Please select at least single record');
        return false;
    }
    else {
        return confirm('Are you sure?'); 
    } 
}


