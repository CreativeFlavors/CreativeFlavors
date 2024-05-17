$(function () {
  $(".my_picker").datepicker({ 
      autoclose: true,
      todayHighlight: true,
      format: "dd/mm/yyyy"
    }).datepicker('update', new Date());


    //$(".my_picker").each(function () {
    //    $(this).datepicker({
    //        autoclose: true,
    //        todayHighlight: true,
    //        format: "dd/mm/yyyy"
    //    }).datepicker('update', new Date());
    //});
});
