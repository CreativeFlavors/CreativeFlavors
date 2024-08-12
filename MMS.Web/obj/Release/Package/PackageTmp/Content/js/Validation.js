//salesorder Validation
function quantityvalidate() {
    var number = $('#quantity').val();
    var filter = /^[0-9]+$/;
    if (number == "") {
        $('#error1').text("Please Enter the Qty");

    }
    else if (filter.test(number)) {
        $('#error1').text("");
        $('#quantity').css('border', '#fff');
    }
    else {
        $('#quantity').css('border', '3px solid red');
        $('#error1').text("Please enter valid number");
    }
}
function disvalidate() {
    var number = $('#discountperid').val();
    var filter = /^[0-9]+$/;
    if (filter.test(number) || number === "") {
        $('#error2').text("");
        $('#discountperid').css('border', '#fff');
    }
    else {
        $('#discountperid').css("border", "3px solid red");
        $('#error2').text("Please enter valid number");
    }
}
function BuyerNamefun() {
    var City = $('#BuyerName').val();
    if (City === "" || City === null) {
        $('#berror').text("Please select Buyer");
    } else {
        $('#berror').text("");
    }
}
function ProductNamefun() {
    var City = $('#ProductName').val();
    if (City === "" || City === null) {
        $('#perror').text("Please select Product");
    } else {
        $('#perror').text("");
    }
}
function order() {
    quantityvalidate();
    BuyerNamefun();
    ProductNamefun();
    disvalidate();
}
function conforder() {
    BuyerNamefun();
    ProductNamefun();
}
//Indent Validation
function matfun() {
    var City = $('#MaterialId').val();
    if (City === "" || City === null) {
        $('#materror').text("Please select Raw Material");
    } else {
        $('#materror').text("");

    }
}
function quantityINvalidate() {
    var number = $('#RequiredQty').val();
    var filter = /^[0-9]+$/;
    if (number == "") {
        $('#inerror').text("Please Enter the Qty");
        $('#RequiredQty').css('border', '2px solid red');
    }
    else if (filter.test(number)) {
        $('#inerror').text("");
        $('#RequiredQty').css('border', '#fff');
    }
    else {
        $('#RequiredQty').css('border', '2px solid red');
        $('#inerror').text("Please enter valid number");
    }
}
function addindent() {
    quantityINvalidate();
    matfun();
}
//BOM Validation 
function quantitybom() {
    var number = $('#Requiredqty').val();
    var filter = /^[0-9]+$/;
    if (number == "") {
        $('#bomerror').text("Please Enter the Qty");
    }
    else if (filter.test(number)) {
        $('#bomerror').text("");
    }
    else {
        $('#bomerror').text("Please enter valid number");
    }
}
function subquantitybom() {
    var number = $('#subRequiredqty').val();
    var filter = /^[0-9]+$/;
    if (number == "") {
        $('#subbomerror').text("Please Enter the Qty");
    }
    else if (filter.test(number)) {
        $('#subbomerror').text("");
    }
    else {
        $('#subbomerror').text("Please enter valid number");
    }
}
function materialfun() {
    var City = $('#Productid').val();
    if (City === "" || City === null) {
        $('#materror').text("Please select Raw Material");
    } else {
        $('#materror').text("");
    }
}
function submaterialfun() {
    var City = $('#ProductSUBid').val();
    if (City === "" || City === null) {
        $('#suberror').text("Please select Sub Assembly Product");
    } else {
        $('#suberror').text("");

    }
}
function addbom() {
    quantitybom();
    materialfun();
}
function addsubbom() {
    subquantitybom();
    submaterialfun();
}
//GRN Validxation
function GRNUnitPrice() {
    var number = $('#UnitPrice').val();
    var filter = /^[0-9]+$/;
    if (number == "") {
        alert("Please Enter the UnitPrice");
    }
    else if (filter.test(number)) {
    }
    else {
        alert("Please enter valid number");

    }
}
function supplierfun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#sperror').text("Please select Raw Material");
    } else {
        $('#sperror').text("");
    }
}
function ponofun() {
    var City = $('#PoHeaderId').val();
    if (City === "" || City === null) {
        $('#poheerror').text("Please select PONO");
    } else {
        $('#poheerror').text("");
    }
}
function Inreffun() {
    var City = $('#RefInvoiceNumber').val();
    if (City === "" || City === null) {
        $('#reinerror').text("Please Enter Ref-Invoice no ");
    } else {
        $('#reinerror').text("");
    }
}
function indatefun() {
    var City = $('#RefInvoiceDate').val();
    if (City === "" || City === null) {
        $('#indateerror').text("Please select Date");
    } else {
        $('#indateerror').text("");
    }
}


function confirsmGRN() {
    indatefun();
    Inreffun();
    ponofun();
    supplierfun();
}

