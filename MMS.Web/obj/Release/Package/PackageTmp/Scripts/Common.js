$(function () {
    $("#empTable").tablesorter({ widthFixed: true })
    .tablesorterPager({ container: $("#pager") });
    $("#empTable").bind("sortStart", function () {
        $("#sortMsg").html('Sorting');
    }).bind("sortEnd", function () {
        $("#sortMsg").html('');
    });
    //Hide/delete row on click.Used live() to apply this click event for all the matching rows.
    $("#empTable img").live("click", function () {
        tablesorter_remove($(this).parent().parent(), $('#empTable'));
    });
});
//Start of Page Size adjust on Row Delete
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

//repopulate table with data from rowCache
function repopulateTableBody(tbl) {
    //aka cleanTableBody from TableSorter code
    if ($.browser.msie) {
        function empty() {
            while (this.firstChild) this.removeChild(this.firstChild);
        }
        empty.apply(tbl.tBodies[0]);
    } else {
        tbl.tBodies[0].innerHTML = "";
    }
    jQuery.each(tbl.config.rowsCopy, function () {
        tbl.tBodies[0].appendChild(this.get(0));
    });
}
//removes the passed in row and updates the tablesorter+pager
function tablesorter_remove(tr, table) {
    //pager modifies actual DOM table to have only #pagesize TR's
    //thus we need to repopulate from the cache first
    repopulateTableBody(table.get(0));
    var index = $("tr", table).index(tr);
    var c = table.get(0).config;
    tr.remove();

    //remove row from cache too
    c.rowsCopy.remove(index);
    c.totalRows = c.rowsCopy.length;
    c.totalPages = Math.ceil(c.totalRows / config.size);

    //now update
    table.trigger("update");
    table.trigger("appendCache");

    //simulate user switches page to get pager to update too
    index = c.page < c.totalPages - 1;
    $(".next").trigger("click");
    if (index)
        $(".prev").trigger("click");
}
//End of Page Size adjust on Row Delete

$(document).on("click", function (e) {

    var eltar = $(e.target).attr("class");
    if (eltar != "left-side-bar" && $(".left-side-bar").find($(e.target)).length != 1) {
        //console.log("asdsa");
        $(".submenu").hide();
        $(".left-side-bar li").removeClass("active");
    }

    else {  }
     

});