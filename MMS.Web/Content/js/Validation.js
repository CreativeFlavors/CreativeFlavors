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
    var filter = /^[0-9]+(\.[0-9]+)?$/;
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
    var filter = /^[0-9]+(\.[0-9]+)?$/;
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

function poinfun() {
    var City = $('#IndentNumber').val();
    if (City === "" || City === null) {
        $('#inerror').text("Please select Indent No ");
    } else {
        $('#inerror').text("");
    }
}
function suppofun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#sperror').text("Please select Supplier Name");
    } else {
        $('#sperror').text("");
    }
}
function profun() {
    var City = $('#ProductId').val();
    if (City === "" || City === null) {
        $('#proerror').text("Please select Product Name ");
    } else {
        $('#proerror').text("");
    }
}

function pocon() {
    poinfun();
    suppofun();
    profun();
}
function ProductTypes() {
    var Prod = $('#ProductType').val();
    if (Prod === "") {
        $('#error1').text("Please select Product Type ");
    } else {
        $('#error1').text("");
    }
}
function Category() {
    var City1 = $('#MaterialCategoryMasterId').val();
    if (City1 === "" || City1 === null) {
        $('#error2').text("Please select Category");
    } else {
        $('#error2').text("");
    }
} function ProdCod() {
    var City2 = $('#ProductCode').val();
    if (City2 === "" || City2 === null) {
        $('#error3').text("Please Enter Product Code");
    } else {
        $('#error3').text("");
    }
} function ProdName() {
    var City = $('#ProductName').val();
    if (City === "" || City === null) {
        $('#error4').text("Please Enter Product Name ");
    } else {
        $('#error4').text("");
    }
} function ProdDes() {
    var City = $('#ProductDesc').val();
    if (City === "" || City === null) {
        $('#error5').text("Please Enter Product Description ");
    } else {
        $('#error5').text("");
    }
}
function Costs() {
    var number = $('#cost').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error6').text("Please Enter Cost");
    }
    else if (filter.test(number)) {
        $('#error6').text("");
    }
    else {
        $('#error6').text("Please enter valid number");

    }
} function Prices() {
    var number = $('#Price').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error7').text("Please Enter Price");
    }
    else if (filter.test(number)) {
        $('#error7').text("");
    }
    else {
        $('#error7').text("Please enter valid number");

    }
} function proweights() {
    var number = $('#weight').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error8').text("Please Enter the Weight");
    }
    else if (filter.test(number)) {
        $('#error8').text("");
    }
    else {
        $('#error8').text("Please enter valid number");

    }
}
function Uom() {
    var City = $('#UomMasterId').val();
    if (City === "" || City === null) {
        $('#error9').text("Please Select UOM");
    } else {
        $('#error9').text("");
    }
} function Tax() {
    var City = $('#TaxMasterId').val();
    if (City === "" || City === null) {
        $('#error10').text("Please Select Tax Type");
    } else {
        $('#error10').text("");
    }
} function Store() {
    var City = $('#StoreId').val();
    if (City === "" || City === null) {
        $('#error12').text("Please Select Store Name");
    } else {
        $('#error12').text("");
    }
}
function productions() {
    var City = $('#productionperday').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (City === "" || City === null) {
        $('#error13').text("Please Enter Production Per-day");
    }
    else if (filter.test(City)) {
        $('#error13').text("");
    } else
    {
        $('#error13').text("Please enter valid number");
    }
}
function MaxSt() {
    var number = $('#MaxStock').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error15').text("Please Enter Max Stock");
    }
    else if (filter.test(number)) {
        $('#error15').text("");
    }
    else {
        $('#error15').text("Please enter valid number");

    }
}
function MinSt() {
    var number = $('#MinStock').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error14').text("Please Enter Min Stock");
    }
    else if (filter.test(number)) {
        $('#error14').text("");
    }
    else {
        $('#error14').text("Please enter valid number");

    }
}


function productfun() {
    MinSt();
    MaxSt();
    productions();
    Store();
    Tax();
    Uom();
    proweights();
    ProdDes();
    Costs();
    Prices();
    ProductTypes();
    Category();
    ProdCod();
    ProdName();
}